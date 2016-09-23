using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StateMachines;
using StateObjects;

namespace ElevatorGUI
{
    public partial class BuildingFloor : Form
    {
        public BuildingFloor()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            if (FloorCount == 0) FloorCount = 4;
            if (ElevatorCount == 0) ElevatorCount = 3;
            if (OutputBox == null) OutputBox = this.textBox1;
            if (Output == null) Output = new TextBoxOutput(OutputBox);
            CreateBuilding();
        }

        private void CreateBuilding()
        {
            if (BuildingPlan == null)
            {
                BuildingPlan = new Building(FloorCount, ElevatorCount, Output);
                User = BuildingPlan.User;
                User.Changed += User_Changed;
                int displayRight = 0;
                int margin = 100;
                foreach (var item in BuildingPlan.Elevators.Elevators)
                {
                    var val = item.Value;
                    FloorElevatorLocationDisplay display = new FloorElevatorLocationDisplay();
                    ElevatorStatus.Controls.Add(display);
                    display.myContainer = ElevatorStatus;
                    display.Initialize(val);                    
                    display.Left = displayRight + margin;                    
                    displayRight += display.Right;
                    val.Changed += Elevator_Changed;
                }

                var floor = BuildingPlan.Floors.First;
                do
                {
                    floor.ListNode.Changed += Floor_Changed;
                    floor = floor.NextNode;
                } while (floor.NextNode != null);

                User.GotoLobby();
            }
        }

        public Floor CurFloor { get; private set; }
        public Elevator CurElevator { get; private set; }
        private void Floor_Changed(object sender, StateChangeEventArgs<FloorStates> e)
        {
            switch (e.NewState)
            {
                case FloorStates.Inactive:
                    FloorPicture.BackgroundImage = ElevatorGUI.Properties.Resources.elevators;
                    break;
                case FloorStates.ElevatorArriving:
                    break;
                case FloorStates.ElevatorArrived:
                    FloorPicture.BackgroundImage = ElevatorGUI.Properties.Resources.DoorOpen1;
                    break;
                case FloorStates.ElevatorLeaving:
                    FloorPicture.BackgroundImage = ElevatorGUI.Properties.Resources.elevators;
                    break;
                case FloorStates.WaitingForElevator:
                    FloorPicture.BackgroundImage = ElevatorGUI.Properties.Resources.elevators;
                    break;
                default:
                    break;
            }
            textBox2.Text = e.Name + " Changed To " + e.NewState.ToString() + System.Environment.NewLine + textBox2.Text;
            PopulateOptions();
        }

        private void Elevator_Changed(object sender, ElevatorStateChangedEventArgs<ElevatorStates> e)
        {
            switch (e.NewState)
            {
                case ElevatorStates.Starting:
                    ElevatorPicture.BackgroundImage = ElevatorGUI.Properties.Resources.InsideClosed;
                    break;
                case ElevatorStates.Stopping:
                    ElevatorPicture.BackgroundImage = ElevatorGUI.Properties.Resources.InsideClosed;
                    break;
                case ElevatorStates.ChangingFloors:
                    break;
                case ElevatorStates.OnFloor:
                    break;
                case ElevatorStates.Inactive:
                    ElevatorPicture.BackgroundImage = ElevatorGUI.Properties.Resources.InsideOpen;
                    break;
                case ElevatorStates.Parked:
                    break;
                default:
                    break;
            }
            textBox2.Text = e.Name + " Changed To " + e.NewState.ToString() + System.Environment.NewLine + textBox2.Text;
            PopulateOptions();
        }

        private void User_Changed(object sender, StateChangeEventArgs<RiderStates> e)
        {
            textBox2.Text = e.Name + " Changed To " + e.NewState.ToString() + System.Environment.NewLine + textBox2.Text;
            PopulateOptions();
            switch (e.NewState)
            {
                case RiderStates.InHall:
                    CurFloor = User.OnFloor;
                    BuildFloor();
                    break;
                case RiderStates.WaitingForElevator:
                    break;
                case RiderStates.OnElevator:
                    CurElevator = User.InElevator;
                    BuildElevator();
                    break;
                case RiderStates.RidingElevator:
                    break;
                case RiderStates.OutsideBuilding:
                    break;
                default:
                    break;
            }
        }

        private void BuildElevator()
        {
            ElevatorPanel.Visible = true;
            FloorPanel.Visible = false;
        }
        private void BuildFloor()
        {
            ElevatorPanel.Visible = false;
            FloorPanel.Visible = true; 

            FloorName.Text = CurFloor.Name;
            UpButton.Initialize(CurFloor, CurFloor.UpButton, ElevatorButtonTypes.Up);
            DownButton.Initialize(CurFloor, CurFloor.DownButton, ElevatorButtonTypes.Down);
        }

        private Dictionary<RiderOptionTypes, RiderOption> curOptions { get; set; }
        private void PopulateOptions()
        {
            curOptions = User.CurrentOptions;
            List<RiderOption> optionList = new List<RiderOption>();
            foreach (var kvp in curOptions)
            {
                optionList.Add(kvp.Value);
            }
            comboBox1.DataSource = optionList;
            comboBox1.DisplayMember = "DisplayName";
            comboBox1.ValueMember = "RiderOptionType";
        }

        private void BuildingFloor_Load(object sender, EventArgs e)
        {
            Initialize();

        }
        private int FloorCount { get; set; }
        private int ElevatorCount { get; set; }
        public TextBox OutputBox { get; set; }
        public TextBoxOutput Output { get; set; }
        public Building BuildingPlan { get; set; }
        public Rider User { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            var opt = (RiderOptionTypes)comboBox1.SelectedValue;
            RiderOption option = curOptions.First(p => p.Key == opt).Value;
            option.Callback();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.BuildingPlan.StartElevators();
        }
    }
}
