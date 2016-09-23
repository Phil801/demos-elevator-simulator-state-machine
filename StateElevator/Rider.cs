using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateMachines;
using StateObjects;

namespace StateObjects
{
    public enum RiderStates { InHall, WaitingForElevator, OnElevator, RidingElevator, OutsideBuilding }
    public enum RiderOptionTypes { PressDownButton, PressUpButton, GetOnElevator, GetOffElevator, PressElevatorButton, Yell, Dance, Sneeze }
    public delegate void ActionInvokeHandler();
        
    public delegate void RiderStateChangedEventHandler(object sender, StateChangeEventArgs<RiderStates> e);
    public class Rider : StateMachine<RiderStates>
    {
        public event RiderStateChangedEventHandler Changed;
        public Rider(string name, IOutput output, Building buildingMgr)
        {
            this.Name = name;
            this.Output = output;
            this.InBuilding = buildingMgr;
            this.Transitions.Add(new Transition<RiderStates>(RiderStates.OutsideBuilding, RiderStates.InHall, new baseAction<RiderStates>(RiderStates.InHall)), RiderStates.InHall);
            this.Transitions.Add(new Transition<RiderStates>(RiderStates.InHall, RiderStates.WaitingForElevator, new baseAction<RiderStates>(RiderStates.WaitingForElevator)), RiderStates.WaitingForElevator);
            this.Transitions.Add(new Transition<RiderStates>(RiderStates.WaitingForElevator, RiderStates.OnElevator, new baseAction<RiderStates>(RiderStates.OnElevator)), RiderStates.OnElevator);
            this.Transitions.Add(new Transition<RiderStates>(RiderStates.OnElevator, RiderStates.RidingElevator, new baseAction<RiderStates>(RiderStates.RidingElevator)), RiderStates.RidingElevator);
            this.Transitions.Add(new Transition<RiderStates>(RiderStates.OnElevator, RiderStates.InHall, new baseAction<RiderStates>(RiderStates.InHall)), RiderStates.InHall);
            this.Transitions.Add(new Transition<RiderStates>(RiderStates.RidingElevator, RiderStates.OnElevator, new baseAction<RiderStates>(RiderStates.OnElevator)), RiderStates.OnElevator);
            CurrentState = RiderStates.OutsideBuilding;
            PreviousState = RiderStates.OutsideBuilding;
            BuildAllOptions();
        }
        public override void OnStateChanged(StateChangeEventArgs<RiderStates> e)
        {
            if (Changed != null)
                Changed(this, e);
        }

        public void GotoLobby()
        {
            if (CurrentState == RiderStates.OutsideBuilding)
            {
                ChangeStateTo(RiderStates.InHall);
            }
        }
        private Dictionary<RiderOptionTypes, RiderOption> AllOptions { get; set; }

        public Dictionary<RiderOptionTypes, RiderOption> CurrentOptions
        {
            get
            {
                return BuildOptions();
            }
        }

        public Building InBuilding { get; set; }

        private Floor curFloor;
        public Floor OnFloor
        {
            get
            {
                return curFloor;
            }
            set
            {
                if(curFloor != null)
                {
                    curFloor.Changed -= CurFloor_Changed;
                }
                curFloor = value;
                curFloor.Changed += CurFloor_Changed;
            }
        }       

        private Elevator curElevator;
        public Elevator InElevator
        {
            get { return curElevator; }
            set
            {
                if (curElevator != null)
                {
                    curElevator.Changed -= CurElevator_Changed;
                }
                curElevator = value;
                curElevator.Changed += CurElevator_Changed;
            }
        }

        private void CurElevator_Changed(object sender, ElevatorStateChangedEventArgs<ElevatorStates> e)
        {
            switch (e.NewState)
            {
                case ElevatorStates.Starting:
                    ChangeStateTo(RiderStates.RidingElevator);
                    break;
                case ElevatorStates.Stopping:
                    break;
                case ElevatorStates.ChangingFloors:
                    break;
                case ElevatorStates.OnFloor:
                    this.curElevator = e.Elevator;
                    this.curFloor = e.Elevator.CurrentFloor.ListNode;
                    break;
                case ElevatorStates.Inactive:
                    ChangeStateTo(RiderStates.OnElevator);
                    break;
                case ElevatorStates.Parked:
                    break;
                default:
                    break;
            }
        }
        private void CurFloor_Changed(object sender, StateChangeEventArgs<FloorStates> e)
        {
            switch (e.NewState)
            {
                case FloorStates.Inactive:
                    ChangeStateTo(RiderStates.InHall);
                    break;
                case FloorStates.ElevatorArriving:
                    break;
                case FloorStates.ElevatorArrived:
                    break;
                case FloorStates.ElevatorLeaving:
                    break;
                case FloorStates.WaitingForElevator:
                    ChangeStateTo(RiderStates.WaitingForElevator);
                    break;
                default:
                    break;
            }
        }

        private Dictionary<RiderOptionTypes, RiderOption> BuildOptions()
        {
            Dictionary<RiderOptionTypes, RiderOption> retVal = new Dictionary<RiderOptionTypes, RiderOption>();
            switch (this.CurrentState)
            {
                case RiderStates.InHall:
                    retVal.Add(RiderOptionTypes.PressUpButton, AllOptions.First(k => k.Key == RiderOptionTypes.PressUpButton).Value);
                    retVal.Add(RiderOptionTypes.PressDownButton, AllOptions.First(k => k.Key == RiderOptionTypes.PressDownButton).Value);
                    retVal.Add(RiderOptionTypes.Dance, AllOptions.First(k => k.Key == RiderOptionTypes.Dance).Value);
                    break;
                case RiderStates.WaitingForElevator:
                    retVal.Add(RiderOptionTypes.Dance, AllOptions.First(k => k.Key == RiderOptionTypes.Dance).Value);
                    retVal.Add(RiderOptionTypes.Sneeze, AllOptions.First(k => k.Key == RiderOptionTypes.Sneeze).Value);
                    if(curElevator != null &&  curElevator.CurrentState == ElevatorStates.Inactive)
                    {
                        retVal.Add(RiderOptionTypes.GetOnElevator, AllOptions.First(k => k.Key == RiderOptionTypes.GetOnElevator).Value);
                    }
                    break;
                case RiderStates.OnElevator:
                    retVal.Add(RiderOptionTypes.PressElevatorButton, AllOptions.First(k => k.Key == RiderOptionTypes.PressElevatorButton).Value);
                    retVal.Add(RiderOptionTypes.Sneeze, AllOptions.First(k => k.Key == RiderOptionTypes.Sneeze).Value);
                    retVal.Add(RiderOptionTypes.GetOffElevator, AllOptions.First(k => k.Key == RiderOptionTypes.GetOffElevator).Value);
                    break;
                case RiderStates.RidingElevator:
                    retVal.Add(RiderOptionTypes.Yell, AllOptions.First(k => k.Key == RiderOptionTypes.Yell).Value);
                    retVal.Add(RiderOptionTypes.Sneeze, AllOptions.First(k => k.Key == RiderOptionTypes.Sneeze).Value);
                    break;
                default:
                    break;
            }

            return retVal;
        }
        
        protected void PressDownButton()
        {
            OnFloor.DownButton.PushButton();            
        }
        protected void PressUpButton()
        {
            OnFloor.UpButton.PushButton();
        }
        protected void GetOnElevator()
        {
            ChangeStateTo(RiderStates.OnElevator);
        }
        protected void GetOffElevator()
        {
            ChangeStateTo(RiderStates.InHall);
        }
        protected void PressElevatorFloorButton()
        {
            InElevator.PushFloorButton(ElevatorButtonToPress);
        }
        public int ElevatorButtonToPress { get; set; }
        private void Yell()
        {
            Output.OutputLine("AAAAARRRRRRGGGGHH!!!!!!");
        }
        protected void Dance()
        {
            Output.OutputLine("Dance, Dance, Dance, Boogie!");
        }
        protected void Sneeze()
        {
            Output.OutputLine("aaa aaaa aaaaAAAACHHHOOOOOOOOO!!!");
        }

        private void BuildAllOptions()
        {
            AllOptions = new Dictionary<RiderOptionTypes, RiderOption>();

            AllOptions.Add(RiderOptionTypes.PressDownButton, new RiderOption
            {
                DisplayName = "Press Down Button",
                RiderOptionType = RiderOptionTypes.PressDownButton,
                Callback = PressDownButton
            });

            AllOptions.Add(RiderOptionTypes.PressUpButton, new RiderOption
            {
                DisplayName = "Press Up Button",
                RiderOptionType = RiderOptionTypes.PressUpButton,
                Callback = PressUpButton
            });

            AllOptions.Add(RiderOptionTypes.PressElevatorButton, new RiderOption
            {
                DisplayName = "Press Elevator Button",
                RiderOptionType = RiderOptionTypes.PressElevatorButton,
                Callback = PressElevatorFloorButton
            });

            AllOptions.Add(RiderOptionTypes.GetOffElevator, new RiderOption
            {
                DisplayName = "Get off the Elevator",
                RiderOptionType = RiderOptionTypes.GetOffElevator,
                Callback = GetOffElevator
            });

            AllOptions.Add(RiderOptionTypes.GetOnElevator, new RiderOption
            {
                DisplayName = "Get on the Elevator",
                RiderOptionType = RiderOptionTypes.GetOnElevator,
                Callback = GetOnElevator
            });

            AllOptions.Add(RiderOptionTypes.Sneeze, new RiderOption
            {
                DisplayName = "Sneeze!",
                RiderOptionType = RiderOptionTypes.Sneeze,
                Callback = Sneeze
            });

            AllOptions.Add(RiderOptionTypes.Dance, new RiderOption
            {
                DisplayName = "Dance!",
                RiderOptionType = RiderOptionTypes.Dance,
                Callback = Dance
            });

            AllOptions.Add(RiderOptionTypes.Yell, new RiderOption
            {
                DisplayName = "Yell!",
                RiderOptionType = RiderOptionTypes.Yell,
                Callback = Yell
            });
        }
    }
}
