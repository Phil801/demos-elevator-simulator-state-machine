using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateMachines;

namespace StateObjects
{
    public enum ElevatorCallButtonStates { On,Off}
    public delegate void ElevatorCallButtonChangedEventHandler(object sender, StateChangeEventArgs<ElevatorCallButtonStates> e);
    public class ElevatorCallButton : StateMachine<ElevatorCallButtonStates>
    {
        public event ElevatorCallButtonChangedEventHandler Changed;
        public override void OnStateChanged(StateChangeEventArgs<ElevatorCallButtonStates> e)
        {
            if (Changed != null)
                Changed(this, e);
        }
        public ElevatorCallButton(string name, IOutput output, int value)            
        {
            this.Output = output;
            this.Name = name;
            this.EnumValue = value;
            this.Transitions.Add(new Transition<ElevatorCallButtonStates>(ElevatorCallButtonStates.Off, ElevatorCallButtonStates.On, new baseAction<ElevatorCallButtonStates>(ElevatorCallButtonStates.On)), ElevatorCallButtonStates.On);
            this.Transitions.Add(new Transition<ElevatorCallButtonStates>(ElevatorCallButtonStates.On, ElevatorCallButtonStates.Off, new baseAction<ElevatorCallButtonStates>(ElevatorCallButtonStates.Off)), ElevatorCallButtonStates.Off);
            this.CurrentState = ElevatorCallButtonStates.Off;
            this.PreviousState = ElevatorCallButtonStates.Off;
            this.BringOnline();
        }
        public int EnumValue { get; set; }
        public ElevatorCallButtonStates PushButton()
        {
            return this.ChangeStateTo(ElevatorCallButtonStates.On);
        }
    }
}
