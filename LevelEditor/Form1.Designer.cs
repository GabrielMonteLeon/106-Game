namespace MEDULevelEditor
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            newLevelButton = new Button();
            NewMapBox = new GroupBox();
            heightTextBox = new TextBox();
            widthTextBox = new TextBox();
            label2 = new Label();
            label1 = new Label();
            loadButton = new Button();
            NewMapBox.SuspendLayout();
            SuspendLayout();
            // 
            // newLevelButton
            // 
            newLevelButton.Location = new Point(6, 22);
            newLevelButton.Name = "newLevelButton";
            newLevelButton.Size = new Size(188, 23);
            newLevelButton.TabIndex = 0;
            newLevelButton.Text = "Create New Level";
            newLevelButton.UseVisualStyleBackColor = true;
            newLevelButton.Click += CreateNewLevel;
            // 
            // NewMapBox
            // 
            NewMapBox.Controls.Add(heightTextBox);
            NewMapBox.Controls.Add(widthTextBox);
            NewMapBox.Controls.Add(label2);
            NewMapBox.Controls.Add(label1);
            NewMapBox.Controls.Add(newLevelButton);
            NewMapBox.Location = new Point(12, 12);
            NewMapBox.Name = "NewMapBox";
            NewMapBox.Size = new Size(200, 100);
            NewMapBox.TabIndex = 1;
            NewMapBox.TabStop = false;
            NewMapBox.Text = "New Map";
            // 
            // heightTextBox
            // 
            heightTextBox.Location = new Point(120, 73);
            heightTextBox.Name = "heightTextBox";
            heightTextBox.Size = new Size(74, 23);
            heightTextBox.TabIndex = 4;
            heightTextBox.TextAlign = HorizontalAlignment.Right;
            // 
            // widthTextBox
            // 
            widthTextBox.Location = new Point(120, 47);
            widthTextBox.Name = "widthTextBox";
            widthTextBox.Size = new Size(74, 23);
            widthTextBox.TabIndex = 3;
            widthTextBox.TextAlign = HorizontalAlignment.Right;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(10, 76);
            label2.Name = "label2";
            label2.Size = new Size(108, 15);
            label2.TabIndex = 2;
            label2.Text = "Grid Height (10-30)";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 50);
            label1.Name = "label1";
            label1.Size = new Size(104, 15);
            label1.TabIndex = 1;
            label1.Text = "Grid Width (10-30)";
            // 
            // loadButton
            // 
            loadButton.Location = new Point(12, 118);
            loadButton.Name = "loadButton";
            loadButton.Size = new Size(200, 23);
            loadButton.TabIndex = 2;
            loadButton.Text = "Load Level File";
            loadButton.UseVisualStyleBackColor = true;
            loadButton.Click += LoadLevelFile;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(224, 154);
            Controls.Add(loadButton);
            Controls.Add(NewMapBox);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            Text = "Form1";
            NewMapBox.ResumeLayout(false);
            NewMapBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button newLevelButton;
        private GroupBox NewMapBox;
        private TextBox heightTextBox;
        private TextBox widthTextBox;
        private Label label2;
        private Label label1;
        private Button loadButton;
    }
}
