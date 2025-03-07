namespace MEDULevelEditor
{
    partial class EditorForm
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
            palletteGroup = new GroupBox();
            pColor6 = new Button();
            pColor5 = new Button();
            pColor4 = new Button();
            pColor3 = new Button();
            pColor2 = new Button();
            pColor1 = new Button();
            groupBox1 = new GroupBox();
            currentTileDisplay = new PictureBox();
            button1 = new Button();
            button2 = new Button();
            mapBox = new GroupBox();
            palletteGroup.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)currentTileDisplay).BeginInit();
            SuspendLayout();
            // 
            // palletteGroup
            // 
            palletteGroup.Controls.Add(pColor6);
            palletteGroup.Controls.Add(pColor5);
            palletteGroup.Controls.Add(pColor4);
            palletteGroup.Controls.Add(pColor3);
            palletteGroup.Controls.Add(pColor2);
            palletteGroup.Controls.Add(pColor1);
            palletteGroup.Location = new Point(19, 15);
            palletteGroup.Name = "palletteGroup";
            palletteGroup.Size = new Size(95, 161);
            palletteGroup.TabIndex = 0;
            palletteGroup.TabStop = false;
            palletteGroup.Text = "Pallette";
            // 
            // pColor6
            // 
            pColor6.BackColor = Color.Black;
            pColor6.Location = new Point(47, 110);
            pColor6.Name = "pColor6";
            pColor6.Size = new Size(45, 45);
            pColor6.TabIndex = 5;
            pColor6.UseVisualStyleBackColor = false;
            pColor6.Click += SelectColor;
            // 
            // pColor5
            // 
            pColor5.BackColor = Color.FromArgb(200, 50, 150);
            pColor5.Location = new Point(3, 110);
            pColor5.Name = "pColor5";
            pColor5.Size = new Size(45, 45);
            pColor5.TabIndex = 4;
            pColor5.UseVisualStyleBackColor = false;
            pColor5.Click += SelectColor;
            // 
            // pColor4
            // 
            pColor4.BackColor = SystemColors.ScrollBar;
            pColor4.Location = new Point(47, 66);
            pColor4.Name = "pColor4";
            pColor4.Size = new Size(45, 45);
            pColor4.TabIndex = 3;
            pColor4.UseVisualStyleBackColor = false;
            pColor4.Click += SelectColor;
            // 
            // pColor3
            // 
            pColor3.BackColor = Color.FromArgb(140, 90, 0);
            pColor3.Location = new Point(3, 66);
            pColor3.Name = "pColor3";
            pColor3.Size = new Size(45, 45);
            pColor3.TabIndex = 2;
            pColor3.UseVisualStyleBackColor = false;
            pColor3.Click += SelectColor;
            // 
            // pColor2
            // 
            pColor2.BackColor = Color.FromArgb(0, 200, 0);
            pColor2.Location = new Point(47, 22);
            pColor2.Name = "pColor2";
            pColor2.Size = new Size(45, 45);
            pColor2.TabIndex = 1;
            pColor2.UseVisualStyleBackColor = false;
            pColor2.Click += SelectColor;
            // 
            // pColor1
            // 
            pColor1.BackColor = Color.FromArgb(100, 140, 255);
            pColor1.Location = new Point(3, 22);
            pColor1.Name = "pColor1";
            pColor1.Size = new Size(45, 45);
            pColor1.TabIndex = 0;
            pColor1.UseVisualStyleBackColor = false;
            pColor1.Click += SelectColor;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(currentTileDisplay);
            groupBox1.Location = new Point(19, 182);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(95, 101);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Current Tile";
            // 
            // currentTileDisplay
            // 
            currentTileDisplay.BackColor = Color.FromArgb(100, 140, 255);
            currentTileDisplay.Location = new Point(18, 22);
            currentTileDisplay.Name = "currentTileDisplay";
            currentTileDisplay.Size = new Size(61, 61);
            currentTileDisplay.TabIndex = 0;
            currentTileDisplay.TabStop = false;
            // 
            // button1
            // 
            button1.Location = new Point(22, 289);
            button1.Name = "button1";
            button1.Size = new Size(92, 56);
            button1.TabIndex = 2;
            button1.Text = "Save File";
            button1.UseVisualStyleBackColor = true;
            button1.Click += SaveButtonPressed;
            // 
            // button2
            // 
            button2.Location = new Point(22, 359);
            button2.Name = "button2";
            button2.Size = new Size(92, 56);
            button2.TabIndex = 3;
            button2.Text = "Load File";
            button2.UseVisualStyleBackColor = true;
            button2.Click += LoadButtonPressed;
            // 
            // mapBox
            // 
            mapBox.Location = new Point(134, 15);
            mapBox.Name = "mapBox";
            mapBox.Size = new Size(400, 400);
            mapBox.TabIndex = 4;
            mapBox.TabStop = false;
            mapBox.Text = "Map";
            // 
            // EditorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(543, 424);
            Controls.Add(mapBox);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(groupBox1);
            Controls.Add(palletteGroup);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "EditorForm";
            Text = "EditorForm";
            FormClosing += OnClosing;
            MouseUp += RecordCurrentAction;
            palletteGroup.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)currentTileDisplay).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox palletteGroup;
        private Button pColor1;
        private Button pColor6;
        private Button pColor5;
        private Button pColor4;
        private Button pColor3;
        private Button pColor2;
        private GroupBox groupBox1;
        private PictureBox currentTileDisplay;
        private Button button1;
        private Button button2;
        private GroupBox mapBox;
    }
}