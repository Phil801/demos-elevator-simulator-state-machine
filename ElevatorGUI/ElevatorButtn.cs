using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StateObjects;
using StateMachines;

namespace ElevatorGUI
{
    public enum ElevatorButtonTypes { Up, Down }
    public class ElevatorBtton : Button
    {
        private int margin = 10;
        public ElevatorCallButton ElevatorButton { get; private set; }
        public Floor curFloor { get; set; }
        public ElevatorButtonTypes ButtonType { get; private set; }
        public void Initialize(Floor floor, ElevatorCallButton button, ElevatorButtonTypes type)
        {
            ButtonType = type;
            ElevatorButton = button;
            curFloor = floor;
            Click += ElevatorButton_Click;
            button.Changed += Button_Changed;
            SetButtonText();
            SetButtonColor();
        }
        private void ElevatorButton_Click(object sender, EventArgs e)
        {
            ElevatorButton.PushButton();
        }
        private void Button_Changed(object sender, StateChangeEventArgs<ElevatorCallButtonStates> e)
        {
            switch (e.NewState)
            {
                case ElevatorCallButtonStates.On:
                    SetButtonColor();
                    break;
                case ElevatorCallButtonStates.Off:
                    SetButtonColor();
                    break;
                default:
                    break;
            }
        }
        private void SetButtonColor()
        {
            switch (ElevatorButton.CurrentState)
            {
                case ElevatorCallButtonStates.On:
                    this.BackColor = Color.Green;
                    break;
                case ElevatorCallButtonStates.Off:
                    this.BackColor = Color.Yellow;
                    break;
                default:
                    break;
            }
        }
        private void SetButtonText()
        {
            switch (ButtonType)
            {
                case ElevatorButtonTypes.Up:
                    this.Text = "Up";
                    break;
                case ElevatorButtonTypes.Down:
                    this.Text = "Down";
                    break;
            }
        }
    }
}

