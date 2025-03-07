﻿namespace MEDULevelEditor
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
            mapBox = new GroupBox();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            newLevelButton = new ToolStripMenuItem();
            saveFileButton = new ToolStripMenuItem();
            saveAsButton = new ToolStripMenuItem();
            loadFileButton = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            undoButton = new ToolStripMenuItem();
            redoButton = new ToolStripMenuItem();
            palletteGroup.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)currentTileDisplay).BeginInit();
            mapBox.SuspendLayout();
            menuStrip1.SuspendLayout();
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
            palletteGroup.Location = new Point(21, 32);
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
            groupBox1.Location = new Point(21, 199);
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
            // mapBox
            // 
            mapBox.Controls.Add(scrollBarHorizontal);
            mapBox.Controls.Add(scrollBarVertical);
            mapBox.Location = new Point(131, 32);
            mapBox.Name = "mapBox";
            mapBox.Size = new Size(400, 400);
            mapBox.TabIndex = 4;
            mapBox.TabStop = false;
            mapBox.Text = "Map";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(543, 24);
            menuStrip1.TabIndex = 5;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newLevelButton, saveFileButton, saveAsButton, loadFileButton });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // newLevelButton
            // 
            newLevelButton.Name = "newLevelButton";
            newLevelButton.Size = new Size(195, 22);
            newLevelButton.Text = "New";
            // 
            // saveFileButton
            // 
            saveFileButton.Name = "saveFileButton";
            saveFileButton.ShortcutKeys = Keys.Control | Keys.S;
            saveFileButton.Size = new Size(195, 22);
            saveFileButton.Text = "Save";
            saveFileButton.Click += SaveButtonPressed;
            // 
            // saveAsButton
            // 
            saveAsButton.Name = "saveAsButton";
            saveAsButton.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
            saveAsButton.Size = new Size(195, 22);
            saveAsButton.Text = "Save As...";
            saveAsButton.Click += SaveAsButtonPressed;
            // 
            // loadFileButton
            // 
            loadFileButton.Name = "loadFileButton";
            loadFileButton.Size = new Size(195, 22);
            loadFileButton.Text = "Load...";
            loadFileButton.Click += LoadButtonPressed;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { undoButton, redoButton });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(39, 20);
            editToolStripMenuItem.Text = "Edit";
            // 
            // undoButton
            // 
            undoButton.Name = "undoButton";
            undoButton.ShortcutKeys = Keys.Control | Keys.Z;
            undoButton.Size = new Size(144, 22);
            undoButton.Text = "Undo";
            undoButton.Click += UndoButtonPressed;
            // 
            // redoButton
            // 
            redoButton.Name = "redoButton";
            redoButton.ShortcutKeys = Keys.Control | Keys.Y;
            redoButton.Size = new Size(144, 22);
            redoButton.Text = "Redo";
            redoButton.Click += RedoButtonPressed;
            // 
            // EditorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(543, 442);
            Controls.Add(mapBox);
            Controls.Add(groupBox1);
            Controls.Add(palletteGroup);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "EditorForm";
            Text = "EditorForm";
            FormClosing += OnClosing;
            MouseUp += RecordCurrentAction;
            palletteGroup.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)currentTileDisplay).EndInit();
            mapBox.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private GroupBox mapBox;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newLevelButton;
        private ToolStripMenuItem saveFileButton;
        private ToolStripMenuItem saveAsButton;
        private ToolStripMenuItem loadFileButton;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem undoButton;
        private ToolStripMenuItem redoButton;
    }
}