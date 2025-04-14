namespace MEDULevelEditor
{
    partial class ResizeForm
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
            label2 = new Label();
            label1 = new Label();
            label3 = new Label();
            label4 = new Label();
            leftValue = new NumericUpDown();
            rightValue = new NumericUpDown();
            topValue = new NumericUpDown();
            bottomValue = new NumericUpDown();
            label5 = new Label();
            widthText = new TextBox();
            heightText = new TextBox();
            label6 = new Label();
            submitButton = new Button();
            cancelButton = new Button();
            ((System.ComponentModel.ISupportInitialize)leftValue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)rightValue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)topValue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bottomValue).BeginInit();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 92);
            label2.Name = "label2";
            label2.Size = new Size(35, 15);
            label2.TabIndex = 2;
            label2.Text = "Right";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 63);
            label1.Name = "label1";
            label1.Size = new Size(27, 15);
            label1.TabIndex = 1;
            label1.Text = "Left";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(16, 121);
            label3.Name = "label3";
            label3.Size = new Size(26, 15);
            label3.TabIndex = 3;
            label3.Text = "Top";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(15, 150);
            label4.Name = "label4";
            label4.Size = new Size(47, 15);
            label4.TabIndex = 4;
            label4.Text = "Bottom";
            // 
            // leftValue
            // 
            leftValue.Location = new Point(108, 61);
            leftValue.Name = "leftValue";
            leftValue.Size = new Size(38, 23);
            leftValue.TabIndex = 5;
            leftValue.ValueChanged += UpdateText;
            // 
            // rightValue
            // 
            rightValue.Location = new Point(108, 90);
            rightValue.Name = "rightValue";
            rightValue.Size = new Size(38, 23);
            rightValue.TabIndex = 6;
            rightValue.ValueChanged += UpdateText;
            // 
            // topValue
            // 
            topValue.Location = new Point(108, 119);
            topValue.Name = "topValue";
            topValue.Size = new Size(38, 23);
            topValue.TabIndex = 7;
            topValue.ValueChanged += UpdateText;
            // 
            // bottomValue
            // 
            bottomValue.Location = new Point(108, 148);
            bottomValue.Name = "bottomValue";
            bottomValue.Size = new Size(38, 23);
            bottomValue.TabIndex = 8;
            bottomValue.ValueChanged += UpdateText;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(14, 26);
            label5.Name = "label5";
            label5.Size = new Size(17, 15);
            label5.TabIndex = 9;
            label5.Text = "X:";
            // 
            // widthText
            // 
            widthText.Location = new Point(33, 23);
            widthText.Name = "widthText";
            widthText.ReadOnly = true;
            widthText.Size = new Size(44, 23);
            widthText.TabIndex = 10;
            // 
            // heightText
            // 
            heightText.Location = new Point(102, 23);
            heightText.Name = "heightText";
            heightText.ReadOnly = true;
            heightText.Size = new Size(44, 23);
            heightText.TabIndex = 12;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(83, 26);
            label6.Name = "label6";
            label6.Size = new Size(17, 15);
            label6.TabIndex = 11;
            label6.Text = "Y:";
            // 
            // submitButton
            // 
            submitButton.Location = new Point(7, 186);
            submitButton.Name = "submitButton";
            submitButton.Size = new Size(75, 23);
            submitButton.TabIndex = 13;
            submitButton.Text = "Submit";
            submitButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(88, 186);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 14;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // ResizeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(171, 221);
            Controls.Add(cancelButton);
            Controls.Add(submitButton);
            Controls.Add(heightText);
            Controls.Add(label6);
            Controls.Add(widthText);
            Controls.Add(label5);
            Controls.Add(bottomValue);
            Controls.Add(topValue);
            Controls.Add(rightValue);
            Controls.Add(leftValue);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(label2);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "ResizeForm";
            Text = "Resize Grid";
            ((System.ComponentModel.ISupportInitialize)leftValue).EndInit();
            ((System.ComponentModel.ISupportInitialize)rightValue).EndInit();
            ((System.ComponentModel.ISupportInitialize)topValue).EndInit();
            ((System.ComponentModel.ISupportInitialize)bottomValue).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label2;
        private Label label1;
        private Label label3;
        private Label label4;
        private NumericUpDown leftValue;
        private NumericUpDown rightValue;
        private NumericUpDown topValue;
        private NumericUpDown bottomValue;
        private Label label5;
        private TextBox widthText;
        private TextBox heightText;
        private Label label6;
        private Button submitButton;
        private Button cancelButton;
    }
}
