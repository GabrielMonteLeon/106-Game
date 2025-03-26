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
            mapBox = new GroupBox();
            mapPanel = new Panel();
            scrollBarHorizontal = new HScrollBar();
            scrollBarVertical = new VScrollBar();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            newLevelButton = new ToolStripMenuItem();
            saveFileButton = new ToolStripMenuItem();
            saveAsButton = new ToolStripMenuItem();
            loadFileButton = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            undoButton = new ToolStripMenuItem();
            redoButton = new ToolStripMenuItem();
            resizeGridToolStripMenuItem = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            zoomToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripMenuItem();
            toolStripMenuItem4 = new ToolStripMenuItem();
            toolStripMenuItem5 = new ToolStripMenuItem();
            toolStripMenuItem6 = new ToolStripMenuItem();
            toolStripMenuItem7 = new ToolStripMenuItem();
            toolStripMenuItem8 = new ToolStripMenuItem();
            palletteGroup.SuspendLayout();
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
            pColor6.FlatStyle = FlatStyle.Flat;
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
            pColor5.FlatStyle = FlatStyle.Flat;
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
            pColor4.FlatStyle = FlatStyle.Flat;
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
            pColor3.FlatStyle = FlatStyle.Flat;
            pColor3.Location = new Point(3, 66);
            pColor3.Name = "pColor3";
            pColor3.Size = new Size(45, 45);
            pColor3.TabIndex = 2;
            pColor3.UseVisualStyleBackColor = false;
            pColor3.Click += SelectColor;
            // 
            // pColor2
            // 
            pColor2.BackColor = Color.FromArgb(192, 0, 0);
            pColor2.FlatStyle = FlatStyle.Flat;
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
            pColor1.FlatStyle = FlatStyle.Flat;
            pColor1.Location = new Point(3, 22);
            pColor1.Name = "pColor1";
            pColor1.Size = new Size(45, 45);
            pColor1.TabIndex = 0;
            pColor1.UseVisualStyleBackColor = false;
            pColor1.Click += SelectColor;
            // 
            // mapBox
            // 
            mapBox.Controls.Add(mapPanel);
            mapBox.Controls.Add(scrollBarHorizontal);
            mapBox.Controls.Add(scrollBarVertical);
            mapBox.Location = new Point(131, 32);
            mapBox.Name = "mapBox";
            mapBox.Size = new Size(417, 424);
            mapBox.TabIndex = 4;
            mapBox.TabStop = false;
            mapBox.Text = "Map";
            // 
            // mapPanel
            // 
            mapPanel.Location = new Point(6, 19);
            mapPanel.Name = "mapPanel";
            mapPanel.Size = new Size(384, 384);
            mapPanel.TabIndex = 2;
            // 
            // scrollBarHorizontal
            // 
            scrollBarHorizontal.LargeChange = 4;
            scrollBarHorizontal.Location = new Point(6, 404);
            scrollBarHorizontal.Maximum = 16;
            scrollBarHorizontal.Name = "scrollBarHorizontal";
            scrollBarHorizontal.Size = new Size(383, 17);
            scrollBarHorizontal.TabIndex = 1;
            scrollBarHorizontal.Scroll += scrollBarHorizontal_Scroll;
            // 
            // scrollBarVertical
            // 
            scrollBarVertical.Enabled = false;
            scrollBarVertical.LargeChange = 4;
            scrollBarVertical.Location = new Point(391, 19);
            scrollBarVertical.Maximum = 16;
            scrollBarVertical.Name = "scrollBarVertical";
            scrollBarVertical.ScaleScrollBarForDpiChange = false;
            scrollBarVertical.Size = new Size(17, 384);
            scrollBarVertical.TabIndex = 0;
            scrollBarVertical.Scroll += scrollBarVertical_Scroll;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, viewToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(559, 24);
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
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { undoButton, redoButton, resizeGridToolStripMenuItem });
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
            // resizeGridToolStripMenuItem
            // 
            resizeGridToolStripMenuItem.Name = "resizeGridToolStripMenuItem";
            resizeGridToolStripMenuItem.Size = new Size(144, 22);
            resizeGridToolStripMenuItem.Text = "Resize Grid";
            resizeGridToolStripMenuItem.Click += resizeGridToolStripMenuItem_Click;
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { zoomToolStripMenuItem });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new Size(44, 20);
            viewToolStripMenuItem.Text = "View";
            // 
            // zoomToolStripMenuItem
            // 
            zoomToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem2, toolStripMenuItem3, toolStripMenuItem4, toolStripMenuItem5, toolStripMenuItem6, toolStripMenuItem7, toolStripMenuItem8 });
            zoomToolStripMenuItem.Name = "zoomToolStripMenuItem";
            zoomToolStripMenuItem.Size = new Size(180, 22);
            zoomToolStripMenuItem.Text = "Zoom";
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(180, 22);
            toolStripMenuItem2.Text = "25%";
            toolStripMenuItem2.Click += AdjustZoom;
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new Size(180, 22);
            toolStripMenuItem3.Text = "37.5%";
            toolStripMenuItem3.Click += AdjustZoom;
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new Size(180, 22);
            toolStripMenuItem4.Text = "50%";
            toolStripMenuItem4.Click += AdjustZoom;
            // 
            // toolStripMenuItem5
            // 
            toolStripMenuItem5.Name = "toolStripMenuItem5";
            toolStripMenuItem5.Size = new Size(180, 22);
            toolStripMenuItem5.Text = "75%";
            toolStripMenuItem5.Click += AdjustZoom;
            // 
            // toolStripMenuItem6
            // 
            toolStripMenuItem6.Name = "toolStripMenuItem6";
            toolStripMenuItem6.Size = new Size(180, 22);
            toolStripMenuItem6.Text = "100%";
            toolStripMenuItem6.Click += AdjustZoom;
            // 
            // toolStripMenuItem7
            // 
            toolStripMenuItem7.Name = "toolStripMenuItem7";
            toolStripMenuItem7.Size = new Size(180, 22);
            toolStripMenuItem7.Text = "150%";
            toolStripMenuItem7.Click += AdjustZoom;
            // 
            // toolStripMenuItem8
            // 
            toolStripMenuItem8.Name = "toolStripMenuItem8";
            toolStripMenuItem8.Size = new Size(180, 22);
            toolStripMenuItem8.Text = "200%";
            toolStripMenuItem8.Click += AdjustZoom;
            // 
            // EditorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(559, 468);
            Controls.Add(mapBox);
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
        private HScrollBar scrollBarHorizontal;
        private VScrollBar scrollBarVertical;
        private Panel mapPanel;
        private ToolStripMenuItem resizeGridToolStripMenuItem;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem zoomToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolStripMenuItem toolStripMenuItem4;
        private ToolStripMenuItem toolStripMenuItem5;
        private ToolStripMenuItem toolStripMenuItem6;
        private ToolStripMenuItem toolStripMenuItem7;
        private ToolStripMenuItem toolStripMenuItem8;
    }
}