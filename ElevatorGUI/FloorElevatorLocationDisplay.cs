using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StateMachines;
using StateObjects;

namespace ElevatorGUI
{
    public class FloorElevatorLocationDisplay: Label
    {
        private Label ElevatorId = new Label();
        private int margin = 10;
        public void Initialize(Elevator elevator)
        {
            ElevatorMonitoring = elevator;
            elevator.Changed += Elevator_Changed;
            ElevatorId.Text = elevator.Name;
            myContainer.Controls.Add(ElevatorId);
            FormatMe();
            AlignElevatorID();
        }
        
        private void FormatMe()
        {
            this.BorderStyle = BorderStyle.Fixed3D;
            this.FlatStyle = FlatStyle.Popup;
            this.AutoSize = true;
            //this.label1.Font = new System.Drawing.Font("Modern No. 20", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //this.label1.ForeColor = System.Drawing.Color.Teal;
            this.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.DarkRed;
            this.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        }
        private void AlignElevatorID()
        {
            ElevatorId.Text = ElevatorMonitoring.Name;
            ElevatorId.Top = this.Bottom + margin;
            ElevatorId.AutoSize = true;
            ElevatorId.Left = this.Left;
        }
        public Panel myContainer { get; set; }
        private void Elevator_Changed(object sender, ElevatorStateChangedEventArgs<ElevatorStates> e)
        {
           this.Text = e.Elevator.CurrentFloor.ListNode.EnumValue.ToString();
            AlignElevatorID();
        }

        public Elevator ElevatorMonitoring { get; set; }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FloorElevatorLocationDisplay
            // 
            this.AutoSize = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(100, 50);
            this.Padding = new System.Windows.Forms.Padding(20);
            this.Size = new System.Drawing.Size(100, 50);
            this.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ResumeLayout(false);

        }
    }
}
