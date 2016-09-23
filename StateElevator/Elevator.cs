using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateMachines;
using System.Threading;


namespace StateObjects
{
    public enum ElevatorStates { Starting, Stopping, ChangingFloors, OnFloor, Inactive, Parked }

    public delegate void ElevatorChangedEventHandler(object sender, ElevatorStateChangedEventArgs<ElevatorStates> e);

    public class Elevator : StateMachine<ElevatorStates>
    {
        public Elevator(string Name, IOutput output, int floors, int value)
        {
            this.Name = Name;
            this.Output = output;
            this.EnumValue = value;
            this.Transitions.Add(new Transition<ElevatorStates>(ElevatorStates.Parked, ElevatorStates.Starting, new baseAction<ElevatorStates>(ElevatorStates.Starting)), ElevatorStates.Starting);
            this.Transitions.Add(new Transition<ElevatorStates>(ElevatorStates.Inactive, ElevatorStates.Starting, new baseAction<ElevatorStates>(ElevatorStates.Starting)), ElevatorStates.Starting);
            this.Transitions.Add(new Transition<ElevatorStates>(ElevatorStates.Starting, ElevatorStates.ChangingFloors, new baseAction<ElevatorStates>(ElevatorStates.ChangingFloors)), ElevatorStates.ChangingFloors);
            this.Transitions.Add(new Transition<ElevatorStates>(ElevatorStates.ChangingFloors, ElevatorStates.OnFloor, new baseAction<ElevatorStates>(ElevatorStates.OnFloor)), ElevatorStates.OnFloor);
            this.Transitions.Add(new Transition<ElevatorStates>(ElevatorStates.OnFloor, ElevatorStates.Stopping, new baseAction<ElevatorStates>(ElevatorStates.Stopping)), ElevatorStates.Stopping);
            this.Transitions.Add(new Transition<ElevatorStates>(ElevatorStates.OnFloor, ElevatorStates.ChangingFloors, new baseAction<ElevatorStates>(ElevatorStates.ChangingFloors)), ElevatorStates.ChangingFloors);
            this.Transitions.Add(new Transition<ElevatorStates>(ElevatorStates.Stopping, ElevatorStates.Inactive, new baseAction<ElevatorStates>(ElevatorStates.Inactive)), ElevatorStates.Inactive);
            this.Transitions.Add(new Transition<ElevatorStates>(ElevatorStates.Inactive, ElevatorStates.Parked, new baseAction<ElevatorStates>(ElevatorStates.Parked)), ElevatorStates.Parked);

            this.CurrentState = ElevatorStates.Parked;
            this.PreviousState = ElevatorStates.Parked;
            this.Door = new ElevatorDoor(Name + " Doors", this.Output);
            Door.Changed += Door_Changed;
            this.Direction = new ElevatorDirection(Name + " Direction", this.Output);

            Direction.Changed += Direction_Changed;
            BuildFloorButtons(floors);
            this.BringOnline();
        }

        private void Door_Changed(object sender, StateChangeEventArgs<ElevatorDoorStates> e)
        {
            switch (e.NewState)
            {
                case ElevatorDoorStates.Opening:
                    break;
                case ElevatorDoorStates.Open:
                    break;
                case ElevatorDoorStates.Closing:
                    break;
                case ElevatorDoorStates.Closed:
                    Go();
                    break;
                default:
                    break;
            }
        }

        static async Task<Boolean> DelayResult(TimeSpan delay)
        {
            await Task.Delay(delay);
            return true;
        }

        public event ElevatorChangedEventHandler Changed;
        
        private void Direction_Changed(object sender, StateChangeEventArgs<ElevatorDirectionStates> e)
        {}

        public override async void OnStateChanged(StateChangeEventArgs<ElevatorStates> e)
        {
            if (Changed != null)
            {
                var eArgs = new ElevatorStateChangedEventArgs<ElevatorStates>(e, this);
                Changed(this, eArgs);

                switch (e.NewState)
                {
                    case ElevatorStates.Starting:
                        Leaving();
                        CheckDirection();
                        await DelayResult(new TimeSpan(0, 0, Globals.STANDARD_TRANSITION_TIMEOUT));
                        ChangeStateTo(ElevatorStates.ChangingFloors);
                        break;                   
                    case ElevatorStates.ChangingFloors:
                        await DelayResult(new TimeSpan(0, 0, Globals.ELEVATOR_TRANSITION_TIMEOUT));
                        ChangeStateTo(ElevatorStates.OnFloor);
                        break;                    
                    case ElevatorStates.OnFloor:
                        await DelayResult(new TimeSpan(0, 0, Globals.STANDARD_TRANSITION_TIMEOUT));
                        SetCurrentFloor();
                        
                        if (ShouldStop())
                        {
                            ChangeStateTo(ElevatorStates.Stopping);
                        }
                        else
                        {
                            ChangeStateTo(ElevatorStates.ChangingFloors);
                        }
                        break;
                    case ElevatorStates.Stopping:
                        Arriving();
                        await DelayResult(new TimeSpan(0, 0, Globals.STANDARD_TRANSITION_TIMEOUT));
                        ChangeStateTo(ElevatorStates.Inactive);
                        break;
                    case ElevatorStates.Inactive:
                        await DelayResult(new TimeSpan(0, 0, Globals.STANDARD_TRANSITION_TIMEOUT));
                        Arrived();
                        break;
                    case ElevatorStates.Parked:
                        break;
                    default:
                        break;
                }
            }
        }
        public void PushFloorButton(int button)
        {
            this.FloorButtons.First(k => k.Key == button).Value.PushButton();
        }
        private void Button_Changed(object sender, StateChangeEventArgs<ElevatorCallButtonStates> e)
        {
            Go();
        }

        public ElevatorDirection Direction { get; private set; }
        
        public int EnumValue { get; set; }

        private DoubleLinkedListNode<Floor> curFloor;
        public DoubleLinkedListNode<Floor> CurrentFloor
        {
            get
            {
                return curFloor;
            }
            set
            {
                if (value == null)
                {
                    System.Diagnostics.Debugger.Break();
                }
                curFloor = value;
            }
        }
        
        public ElevatorDoor Door { get; set; }
        public Dictionary<int, ElevatorCallButton> FloorButtons { get; set; }

        private bool ShouldStop()
        {
            bool overall = CallButtonPushed() || FloorButtonPushed();
            if (CurrentFloor.NextNode == null || CurrentFloor.PreviousNode == null)  //On first or last floor, stop to redirect.
            {
                return true;  
            }
            return overall;
        }
        private bool CallButtonPushed()
        {
            if (Direction.CurrentState == ElevatorDirectionStates.Down && CurrentFloor.ListNode.DownButton.CurrentState == ElevatorCallButtonStates.On) return true;
            if (Direction.CurrentState == ElevatorDirectionStates.Up && CurrentFloor.ListNode.UpButton.CurrentState == ElevatorCallButtonStates.On) return true;
            return false;
        }
        private bool FloorButtonPushed()
        {
            var button = FloorButtons.First(k => k.Key == CurrentFloor.ListNode.EnumValue).Value;
            return button.CurrentState == ElevatorCallButtonStates.On;
        }
        private void SetCurrentFloor()
        {
            var oldFloor = CurrentFloor;
            if (Direction.CurrentState == ElevatorDirectionStates.Up)
            {
                if (oldFloor.NextNode == null)
                {
                    CurrentFloor = oldFloor;
                }
                else
                {
                    CurrentFloor = oldFloor.NextNode;
                }
            }
            else
            {
                if (oldFloor.PreviousNode == null)
                {
                    CurrentFloor = oldFloor;
                }
                else
                {
                    CurrentFloor = oldFloor.PreviousNode;
                }
            }
        }

        private void CheckDirection()
        {
            if(Direction.CurrentState == ElevatorDirectionStates.Up && CurrentFloor.NextNode == null)
            {
                Direction.ChangeDirection();
            }
            if(Direction.CurrentState == ElevatorDirectionStates.Down && CurrentFloor.PreviousNode == null)
            {
                Direction.ChangeDirection();
            }
        }
        private void Arriving()
        {
            CurrentFloor.ListNode.Arriving(this);
        }
        private void Leaving()
        {
            CurrentFloor.ListNode.Leaving(this);
        }
        private void Arrived()
        {
            CurrentFloor.ListNode.Arrived(this);
            var button = FloorButtons.First(k => k.Key == CurrentFloor.ListNode.EnumValue).Value;
            if (button.CurrentState == ElevatorCallButtonStates.On)
            {
                button.ChangeStateTo(ElevatorCallButtonStates.Off);
            }
            this.Door.Open();
        }
        private void BuildFloorButtons(int floors)
        {
            this.FloorButtons = new Dictionary<int, ElevatorCallButton>();
            for (int i = 1; i < floors + 1; i++)
            {
                var button = new ElevatorCallButton(string.Format("Floor {0} Call Button", i), this.Output, i);
                button.Changed += Button_Changed;
                FloorButtons.Add(i, button);
            }
        }
        public void Go()
        {
            ChangeStateTo(ElevatorStates.Starting);
        }
    }
}
