using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateMachines;

namespace StateObjects
{
    public enum FloorStates { Inactive, ElevatorArriving, ElevatorArrived, ElevatorLeaving, WaitingForElevator }
    public delegate void FloorChangedEventHandler(object sender, StateChangeEventArgs<FloorStates> e);
    public class Floor : StateMachine<FloorStates>
    {
        public Floor(string Name, string greeting, string leaving, IOutput output, int value)
        {
            this.Name = Name;
            this.Output = output;
            this.FloorGreeting = greeting;
            this.FloorLeaving = leaving;
            this.EnumValue = value;
            this.Transitions.Add(new Transition<FloorStates>(FloorStates.WaitingForElevator, FloorStates.ElevatorArriving, new baseAction<FloorStates>(FloorStates.ElevatorArriving)), FloorStates.ElevatorArriving);
            this.Transitions.Add(new Transition<FloorStates>(FloorStates.ElevatorArriving, FloorStates.ElevatorArrived, new baseAction<FloorStates>(FloorStates.ElevatorArrived)), FloorStates.ElevatorArrived);
            this.Transitions.Add(new Transition<FloorStates>(FloorStates.ElevatorArrived, FloorStates.ElevatorLeaving, new baseAction<FloorStates>(FloorStates.ElevatorLeaving)), FloorStates.ElevatorLeaving);
            this.Transitions.Add(new Transition<FloorStates>(FloorStates.ElevatorLeaving, FloorStates.Inactive, new baseAction<FloorStates>(FloorStates.Inactive)), FloorStates.Inactive);
            this.Transitions.Add(new Transition<FloorStates>(FloorStates.ElevatorLeaving, FloorStates.WaitingForElevator, new baseAction<FloorStates>(FloorStates.WaitingForElevator)), FloorStates.WaitingForElevator);
            this.Transitions.Add(new Transition<FloorStates>(FloorStates.Inactive, FloorStates.WaitingForElevator, new baseAction<FloorStates>(FloorStates.WaitingForElevator)), FloorStates.WaitingForElevator);
            this.Transitions.Add(new Transition<FloorStates>(FloorStates.Inactive, FloorStates.ElevatorArriving, new baseAction<FloorStates>(FloorStates.ElevatorArriving)), FloorStates.ElevatorArriving);

            this.CurrentState = FloorStates.Inactive;
            this.PreviousState = FloorStates.Inactive;

            this.UpButton = new ElevatorCallButton(this.Name + " Up Button", this.Output, 1);
            this.DownButton = new ElevatorCallButton(this.Name + " Down Button", this.Output, 2);
            UpButton.Changed += new ElevatorCallButtonChangedEventHandler(UpButtonChanged);
            DownButton.Changed += new ElevatorCallButtonChangedEventHandler(DownButtonChanged);

            this.BringOnline();
        }
        static async Task<Boolean> DelayResult(TimeSpan delay)
        {
            await Task.Delay(delay);
            return true;
        }

        public event FloorChangedEventHandler Changed;
        public int EnumValue { get; set; }
        public string FloorGreeting { get; set; }
        public string FloorLeaving { get; set; }

        public async override void OnStateChanged(StateChangeEventArgs<FloorStates> e)
        {
            if (Changed != null)
                Changed(this, e);
            switch (e.NewState)
            {
                case FloorStates.Inactive:
                    await DelayResult(new TimeSpan(0, 0, Globals.STANDARD_TRANSITION_TIMEOUT));
                    break;
                case FloorStates.ElevatorArriving:
                    await DelayResult(new TimeSpan(0, 0, Globals.STANDARD_TRANSITION_TIMEOUT));
                    break;
                case FloorStates.ElevatorArrived:
                    await DelayResult(new TimeSpan(0, 0, Globals.STANDARD_TRANSITION_TIMEOUT));
                    break;
                case FloorStates.ElevatorLeaving:
                    await DelayResult(new TimeSpan(0, 0, Globals.STANDARD_TRANSITION_TIMEOUT));
                    break;
                case FloorStates.WaitingForElevator:
                    break;
                default:
                    break;
            }
        }

        public void Arriving(Elevator elevator)
        {
            ChangeStateTo(FloorStates.ElevatorArriving);
        }
        public void Arrived(Elevator elevator)
        {
            if (this.WaitingForDown && elevator.Direction.CurrentState == ElevatorDirectionStates.Down)
            {
                DownButton.ChangeStateTo(ElevatorCallButtonStates.Off);
            }
            if (this.WaitingForUp && elevator.Direction.CurrentState == ElevatorDirectionStates.Up)
            {
                UpButton.ChangeStateTo(ElevatorCallButtonStates.Off);
            }
            ChangeStateTo(FloorStates.ElevatorArrived);
            Output.OutputLine(FloorGreeting);
        }

        public void Leaving(Elevator elevator)
        {
            ChangeStateTo(FloorStates.ElevatorLeaving);
            Left(elevator);
        }

        public void Left(Elevator elevator)
        {
            if (elevator.Direction.CurrentState == ElevatorDirectionStates.Up && WaitingForDown)
            {
                this.ChangeStateTo(FloorStates.WaitingForElevator);
            }
            if (elevator.Direction.CurrentState == ElevatorDirectionStates.Down && WaitingForUp)
            {
                this.ChangeStateTo(FloorStates.WaitingForElevator);
            }
            if (!WaitingForDown && !WaitingForUp)
            {
                this.ChangeStateTo(FloorStates.Inactive);
            }
        }
        private void UpButtonChanged(object sender, StateChangeEventArgs<ElevatorCallButtonStates> e)
        {
            Output.OutputLine("Up Button changed to " + e.NewState);
            ChangeStateTo(FloorStates.WaitingForElevator);
        }
        public void DownButtonChanged(object sender, StateChangeEventArgs<ElevatorCallButtonStates> e)
        {
            Output.OutputLine("Down Button changed to " + e.NewState);
            ChangeStateTo(FloorStates.WaitingForElevator);
        }

        public ElevatorCallButton UpButton { get; set; }
        public ElevatorCallButton DownButton { get; set; }
        public bool WaitingForUp
        {
            get
            {
                if (UpButton.CurrentState == ElevatorCallButtonStates.On)
                    return true;
                else
                    return false;
            }
        }
        public bool WaitingForDown
        {
            get
            {
                if (DownButton.CurrentState == ElevatorCallButtonStates.On)
                    return true;
                else
                    return false;
            }
        }
    }
}
