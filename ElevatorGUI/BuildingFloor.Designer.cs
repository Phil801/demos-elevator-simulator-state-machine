namespace ElevatorGUI
{
    partial class BuildingFloor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ButtonPanel = new System.Windows.Forms.Panel();
            this.FloorPanel = new System.Windows.Forms.Panel();
            this.ElevatorStatus = new System.Windows.Forms.Panel();
            this.FloorName = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.ElevatorPanel = new System.Windows.Forms.Panel();
            this.ElevatorButtonPanel = new System.Windows.Forms.Panel();
            this.ElevatorName = new System.Windows.Forms.Label();
            this.ElevatorPicture = new System.Windows.Forms.PictureBox();
            this.FloorPicture = new System.Windows.Forms.PictureBox();
            this.DownButton = new ElevatorGUI.ElevatorBtton();
            this.UpButton = new ElevatorGUI.ElevatorBtton();
            this.ButtonPanel.SuspendLayout();
            this.FloorPanel.SuspendLayout();
            this.ElevatorPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ElevatorPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FloorPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(756, 25);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(464, 239);
            this.textBox1.TabIndex = 1;
            // 
            // ButtonPanel
            // 
            this.ButtonPanel.Controls.Add(this.DownButton);
            this.ButtonPanel.Controls.Add(this.UpButton);
            this.ButtonPanel.Location = new System.Drawing.Point(608, 312);
            this.ButtonPanel.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonPanel.Name = "ButtonPanel";
            this.ButtonPanel.Size = new System.Drawing.Size(89, 134);
            this.ButtonPanel.TabIndex = 4;
            // 
            // FloorPanel
            // 
            this.FloorPanel.Controls.Add(this.ElevatorPanel);
            this.FloorPanel.Controls.Add(this.FloorPicture);
            this.FloorPanel.Controls.Add(this.ElevatorStatus);
            this.FloorPanel.Controls.Add(this.FloorName);
            this.FloorPanel.Controls.Add(this.ButtonPanel);
            this.FloorPanel.Location = new System.Drawing.Point(10, 25);
            this.FloorPanel.Margin = new System.Windows.Forms.Padding(2);
            this.FloorPanel.Name = "FloorPanel";
            this.FloorPanel.Size = new System.Drawing.Size(701, 644);
            this.FloorPanel.TabIndex = 5;
            // 
            // ElevatorStatus
            // 
            this.ElevatorStatus.Location = new System.Drawing.Point(19, 47);
            this.ElevatorStatus.Name = "ElevatorStatus";
            this.ElevatorStatus.Size = new System.Drawing.Size(654, 84);
            this.ElevatorStatus.TabIndex = 6;
            // 
            // FloorName
            // 
            this.FloorName.AutoSize = true;
            this.FloorName.Font = new System.Drawing.Font("Modern No. 20", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FloorName.ForeColor = System.Drawing.Color.Teal;
            this.FloorName.Location = new System.Drawing.Point(293, 20);
            this.FloorName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.FloorName.Name = "FloorName";
            this.FloorName.Size = new System.Drawing.Size(69, 24);
            this.FloorName.TabIndex = 5;
            this.FloorName.Text = "label1";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(757, 273);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox2.Size = new System.Drawing.Size(464, 263);
            this.textBox2.TabIndex = 6;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(757, 642);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(231, 21);
            this.comboBox1.TabIndex = 7;
            this.comboBox1.Text = "Available Actions";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1005, 638);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 25);
            this.button1.TabIndex = 8;
            this.button1.Text = "Execute Action";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(757, 550);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 26);
            this.button2.TabIndex = 9;
            this.button2.Text = "Begin Simulation";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ElevatorPanel
            // 
            this.ElevatorPanel.Controls.Add(this.ElevatorButtonPanel);
            this.ElevatorPanel.Controls.Add(this.ElevatorPicture);
            this.ElevatorPanel.Controls.Add(this.ElevatorName);
            this.ElevatorPanel.Location = new System.Drawing.Point(3, 3);
            this.ElevatorPanel.Name = "ElevatorPanel";
            this.ElevatorPanel.Size = new System.Drawing.Size(708, 644);
            this.ElevatorPanel.TabIndex = 10;
            // 
            // ElevatorButtonPanel
            // 
            this.ElevatorButtonPanel.Location = new System.Drawing.Point(487, 271);
            this.ElevatorButtonPanel.Name = "ElevatorButtonPanel";
            this.ElevatorButtonPanel.Size = new System.Drawing.Size(172, 326);
            this.ElevatorButtonPanel.TabIndex = 0;
            // 
            // ElevatorName
            // 
            this.ElevatorName.AutoSize = true;
            this.ElevatorName.Font = new System.Drawing.Font("Modern No. 20", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ElevatorName.ForeColor = System.Drawing.Color.Teal;
            this.ElevatorName.Location = new System.Drawing.Point(288, 16);
            this.ElevatorName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ElevatorName.Name = "ElevatorName";
            this.ElevatorName.Size = new System.Drawing.Size(69, 24);
            this.ElevatorName.TabIndex = 6;
            this.ElevatorName.Text = "label1";
            // 
            // ElevatorPicture
            // 
            this.ElevatorPicture.BackgroundImage = global::ElevatorGUI.Properties.Resources.InsideClosed;
            this.ElevatorPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ElevatorPicture.Location = new System.Drawing.Point(18, 134);
            this.ElevatorPicture.Name = "ElevatorPicture";
            this.ElevatorPicture.Size = new System.Drawing.Size(664, 562);
            this.ElevatorPicture.TabIndex = 7;
            this.ElevatorPicture.TabStop = false;
            // 
            // FloorPicture
            // 
            this.FloorPicture.BackgroundImage = global::ElevatorGUI.Properties.Resources.DoorOpen1;
            this.FloorPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.FloorPicture.Location = new System.Drawing.Point(19, 150);
            this.FloorPicture.Name = "FloorPicture";
            this.FloorPicture.Size = new System.Drawing.Size(525, 468);
            this.FloorPicture.TabIndex = 7;
            this.FloorPicture.TabStop = false;
            // 
            // DownButton
            // 
            this.DownButton.curFloor = null;
            this.DownButton.Location = new System.Drawing.Point(9, 71);
            this.DownButton.Margin = new System.Windows.Forms.Padding(2);
            this.DownButton.Name = "DownButton";
            this.DownButton.Size = new System.Drawing.Size(73, 55);
            this.DownButton.TabIndex = 5;
            this.DownButton.Text = "Up";
            this.DownButton.UseVisualStyleBackColor = true;
            // 
            // UpButton
            // 
            this.UpButton.curFloor = null;
            this.UpButton.Location = new System.Drawing.Point(9, 7);
            this.UpButton.Margin = new System.Windows.Forms.Padding(2);
            this.UpButton.Name = "UpButton";
            this.UpButton.Size = new System.Drawing.Size(73, 60);
            this.UpButton.TabIndex = 4;
            this.UpButton.Text = "Up";
            this.UpButton.UseVisualStyleBackColor = true;
            // 
            // BuildingFloor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1227, 738);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.FloorPanel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "BuildingFloor";
            this.Text = "BuildingFloor";
            this.Load += new System.EventHandler(this.BuildingFloor_Load);
            this.ButtonPanel.ResumeLayout(false);
            this.FloorPanel.ResumeLayout(false);
            this.FloorPanel.PerformLayout();
            this.ElevatorPanel.ResumeLayout(false);
            this.ElevatorPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ElevatorPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FloorPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel ButtonPanel;
        private ElevatorBtton DownButton;
        private ElevatorBtton UpButton;
        private System.Windows.Forms.Panel FloorPanel;
        private System.Windows.Forms.Label FloorName;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel ElevatorStatus;
        private System.Windows.Forms.Panel ElevatorPanel;
        private System.Windows.Forms.Label ElevatorName;
        private System.Windows.Forms.Panel ElevatorButtonPanel;
        private System.Windows.Forms.PictureBox FloorPicture;
        private System.Windows.Forms.PictureBox ElevatorPicture;
    }
}