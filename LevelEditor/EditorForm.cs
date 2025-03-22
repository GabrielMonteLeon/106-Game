namespace MEDULevelEditor
{
    public partial class EditorForm : Form
    {
        /// <summary>
        /// Represents a single paint action (a single instance of clicking+releasing)
        /// </summary>
        private struct ActionData
        {
            public int newColorIndex;
            public List<(int x, int y, int prevColorIndex)> paintedCoords;

            public ActionData(int colorIndex)
            {
                newColorIndex = colorIndex;
                paintedCoords = new();
            }
        }

        private Color[] colorPalette;
        private Button[] paletteButtons;
        private int[,] tileMapData;
        private PictureBox[,] tileMapVisuals;
        private int selectedColorIndex;

        private int scrollX;
        private int scrollY;
        private int tileSize;

        private string currentFile;

        /// <summary>
        /// The list of actions that can be undone/redone
        /// </summary>
        private LinkedList<ActionData> undoQueue;
        /// <summary>
        /// The paint action currently in progress
        /// </summary>
        private ActionData? currentAction;
        /// <summary>
        /// The point in the undo queue where the map currently lies.
        /// All actions up to and including this action are currently applied to the map
        /// </summary>
        private LinkedListNode<ActionData>? undoPos;
        /// <summary>
        /// The point in the undo queue where the map was last saved.
        /// </summary>
        private LinkedListNode<ActionData>? savePos;

        private bool IsSaved => undoPos == savePos && currentAction == null;

        public EditorForm()
        {
            InitializeComponent();

            colorPalette = new Color[6];
            paletteButtons = [pColor1, pColor2, pColor3, pColor4, pColor5, pColor6];
            for (int i = 0; i < 6; i++)
            {
                colorPalette[i] = paletteButtons[i].BackColor;
            }
            selectedColorIndex = 0;
            currentFile = "";
            tileMapData = null!;
            tileMapVisuals = null!;
            undoQueue = new LinkedList<ActionData>();
            undoPos = null;
            savePos = null;
        }

        /// <summary>
        /// Initializes a new blank map.
        /// </summary>
        public void InitNewMap(int width, int height)
        {
            if (tileMapData != null)
            {
                Cleanup();
            }

            tileMapData = new int[width, height];
            tileMapVisuals = new PictureBox[width, height];
            SelectColor(0);
            currentFile = "";
            undoQueue = new LinkedList<ActionData>();
            undoPos = null;
            savePos = null;

            //calculate tile/map sizes
            tileSize = 32;
            int mapHeight = tileSize * height;
            int mapWidth = tileSize * width;
            //adjust scroll bars based on map size
            AdjustScrollBars();

            //Create map picture boxes;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    PictureBox tile = new PictureBox();
                    tileMapVisuals[x, y] = tile;
                    //the first color is the default color
                    tile.BackColor = colorPalette[0];
                    tile.Location = new Point(tileSize * x, tileSize * y);
                    tile.Size = new Size(tileSize, tileSize);
                    int currentX = x;
                    int currentY = y;
                    //these delegates would get the values of the variables at the time of being called
                    //x and y would have changed by then, so we need new variables that stay the same.
                    tile.MouseDown += delegate { OnTileClick(tile, currentX, currentY); };
                    tile.MouseEnter += delegate { OnTileMouseEnter(currentX, currentY); };
                    tile.MouseUp += RecordCurrentAction;
                    mapPanel.Controls.Add(tile);
                }
            }

            UpdateTitle();
            UpdateButtons();
        }

        private void LoadButtonPressed(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Load Level File";
            dialog.Filter = "Level Files (*.level)|*.level";
            DialogResult result = dialog.ShowDialog();

            if (result != DialogResult.OK)
                return;

            LoadMap(dialog.FileName);
        }

        /// <summary>
        /// Loads a map from a file.
        /// </summary>
        /// <returns>Returns true if file was successfully loaded, otherwise false</returns>
        public bool LoadMap(string filePath)
        {
            byte width = 0;
            byte height = 0;
            byte[] data = null!;
            BinaryReader reader = null!;
            try
            {
                reader = new BinaryReader(File.OpenRead(filePath));
                width = reader.ReadByte();
                height = reader.ReadByte();
                data = reader.ReadBytes(width * height);
                if (data.Length < width * height)
                {
                    MessageBox.Show("Unable to load due to corrupted data.", "Corrupted File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load due to the following error: " + ex.Message, "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
            InitNewMap(width, height);
            currentFile = filePath;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    PaintTile(x, y, data[x * height + y], false);
                }
            }
            UpdateTitle();
            UpdateButtons();

            MessageBox.Show("Successfully Loaded");
            return true;
        }

        private void OnClosing(object sender, FormClosingEventArgs e)
        {
            if (!CheckSaveOnExit())
            {
                e.Cancel = true;
                return;
            }
            Cleanup();
        }

        /// <summary>
        /// Removes the current map from display and cleans up all related components
        /// This should be called before creating/loading a new map
        /// </summary>
        private void Cleanup()
        {
            mapBox.Controls.Clear();
            //this signals that everything is cleaned up
            tileMapData = null!;

        }

        /// <summary>
        /// Checks whether the map has been saved and prompts the user if not.
        /// </summary>
        /// <returns>Returns false if the user cancelled the operation, otherwise true.</returns>
        private bool CheckSaveOnExit()
        {
            if (IsSaved)
                return true;

            DialogResult response = MessageBox.Show("You have unsaved changes. Would you like to save before closing?",
                "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            switch (response)
            {
                case DialogResult.Yes:
                    return Save();
                case DialogResult.No:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Saves the map to the current file.
        /// </summary>
        /// <returns>Returns false if the user cancelled the operation, otherwise true.</returns>
        public bool Save()
        {
            if (currentFile == "")
            {
                return SaveAs();
            }
            else
            {
                return SaveMap(currentFile);
            }
        }

        /// <summary>
        /// Saves the map to a new file.
        /// </summary>
        /// <returns>Returns false if the user cancelled the operation, otherwise true.</returns>
        public bool SaveAs()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Save Level File";
            dialog.Filter = "Level Files (*.level)|*.level";
            DialogResult result = dialog.ShowDialog();

            if (result != DialogResult.OK)
                return false;

            return SaveMap(dialog.FileName);
        }

        /// <summary>
        /// Saves the current map to a file.
        /// </summary>
        /// <returns>Returns true if the map was successfully saved, otherwise false.</returns>
        private bool SaveMap(string filePath)
        {
            BinaryWriter writer = null!;
            try
            {
                writer = new BinaryWriter(File.OpenWrite(filePath));
                //write dimensions
                writer.Write((byte)tileMapData.GetLength(0));
                writer.Write((byte)tileMapData.GetLength(1));
                for (int x = 0; x < tileMapData.GetLength(0); x++)
                {
                    for (int y = 0; y < tileMapData.GetLength(1); y++)
                    {
                        writer.Write((byte)tileMapData[x, y]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to save due to the following error: " + ex.Message, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }

            currentFile = filePath;
            savePos = undoPos;
            UpdateTitle();
            MessageBox.Show("Saved Successfully");
            return true;
        }

        private void OnTileClick(PictureBox sender, int x, int y)
        {
            //make sure the most recent paint action was recorded
            RecordCurrentAction(null!, null!);

            sender.Capture = false;
            PaintTile(x, y);
        }

        private void OnTileMouseEnter(int x, int y)
        {
            if (MouseButtons != 0)
                PaintTile(x, y);
            else
                //make sure the most recent paint action was recorded if the mouse was up
                RecordCurrentAction(null!, null!);
        }

        private void SelectColor(object sender, EventArgs e)
        {
            //make sure the most recent paint action was recorded
            RecordCurrentAction(null!, null!);

            int colorIndex = -1;
            for (int i = 0; i < 6; i++)
            {
                if (paletteButtons[i] == sender)
                {
                    colorIndex = i;
                    break;
                }
            }
            SelectColor(colorIndex);
        }

        private void SelectColor(int colorIndex)
        {
            for(int i = 0; i < paletteButtons.Length; i++)
            {
                if (i != colorIndex)
                    paletteButtons[i].FlatStyle = FlatStyle.Flat;
                else
                    paletteButtons[i].FlatStyle = FlatStyle.Standard;
            }
            selectedColorIndex = colorIndex;
        }

        /// <summary>
        /// Paints a tile on the map.
        /// </summary>
        /// <param name="x">The x coordinate of the tile to paint.</param>
        /// <param name="y">The y coordinate of the tile to paint.</param>
        /// <param name="colorIndex">The index of the color to paint. Defaults to the current selected color.</param>
        /// <param name="asAction">If true, this is recorded as part of an action to be placed in the undo queue.</param>
        private void PaintTile(int x, int y, int colorIndex = -1, bool asAction = true)
        {
            if (colorIndex < 0)
                colorIndex = selectedColorIndex;

            int prevColorIndex = tileMapData[x, y];
            if (prevColorIndex == colorIndex)
                return;

            if (asAction)
            {
                if (currentAction == null)
                {
                    currentAction = new ActionData(colorIndex);
                    UpdateTitle();
                    UpdateButtons();
                }
                currentAction.Value.paintedCoords.Add((x, y, prevColorIndex));
            }

            tileMapData[x, y] = colorIndex;
            tileMapVisuals[x, y].BackColor = colorPalette[colorIndex];
        }

        /// <summary>
        /// Record the current paint action and place it in the undo queue.
        /// </summary>
        private void RecordCurrentAction(object? sender, MouseEventArgs e)
        {
            //there's no easy way to check if the mouse is released at ANY location,
            //so this function is included in way more places than it really should be
            if (currentAction == null)
                return;

            System.Diagnostics.Debug.WriteLine($"\nRecording draw action with color {currentAction.Value.newColorIndex}.");

            //Delete undo history past this point
            while (undoPos != undoQueue.Last)
            {
                System.Diagnostics.Debug.WriteLine($"Deleting overwritten action.");
                undoQueue.RemoveLast();
            }

            undoQueue.AddLast(currentAction.Value);
            undoPos = undoQueue.Last;
            currentAction = null;

            UpdateTitle();
            UpdateButtons();
        }

        public void Undo()
        {
            if (undoPos == null)
                return;

            ActionData data = undoPos.Value;
            foreach ((int x, int y, int prevColorIndex) paintedCoord in data.paintedCoords)
            {
                PaintTile(paintedCoord.x, paintedCoord.y, paintedCoord.prevColorIndex, false);
            }
            undoPos = undoPos.Previous;

            UpdateTitle();
            UpdateButtons();
        }

        public bool CanUndo()
        {
            return undoPos != null;
        }

        public void Redo()
        {
            //Move undoPos one forward before doing the operation
            //This is formatted slightly weird so that the compiler won't give a "Dereference of a possible null reference" warning later
            if (undoPos == null)
            {
                undoPos = undoQueue.First;
                if (undoPos == null)
                    return;
            }
            else
            {
                if (undoPos.Next == null)
                    return;
                else
                    undoPos = undoPos.Next;
            }

            ActionData data = undoPos.Value;
            foreach ((int x, int y, int prevColorIndex) paintedCoord in data.paintedCoords)
            {
                PaintTile(paintedCoord.x, paintedCoord.y, data.newColorIndex, false);
            }

            UpdateTitle();
            UpdateButtons();
        }

        public bool CanRedo()
        {
            //if there's no actions performed at all...
            if (undoQueue.First == null)
                return false;
            //if there's been any actions, and all of them have been undone...
            else if (undoPos == null)
                return true;
            //if we're at the end of the undo queue...
            else if (undoPos.Next == null)
                return false;
            else
                return true;
        }

        private void UpdateTitle()
        {
            string titleText = "Map Editor";
            if (currentFile != "")
            {
                titleText += " - " + currentFile;
            }
            if (!IsSaved)
            {
                titleText += "*";
            }
            this.Text = titleText;
        }

        private void UpdateButtons()
        {
            saveFileButton.Enabled = !IsSaved || currentFile == null;
            undoButton.Enabled = CanUndo();
            redoButton.Enabled = CanRedo();
        }

        private void UpdateScroll()
        {
            for(int x = 0; x < tileMapVisuals.GetLength(0); x++)
            {
                for(int y = 0; y < tileMapVisuals.GetLength(1); y++)
                {
                    tileMapVisuals[x, y].Location = new Point((x - scrollX) * tileSize, (y - scrollY) * tileSize);
                }
            }
        }

        private void AdjustScrollBars()
        {
            int visibleTilesX = mapPanel.Width / 32;
            int visibleTilesY = mapPanel.Height / 32;

            //Set scroll bar sizes
            scrollBarHorizontal.LargeChange = visibleTilesX;
            scrollBarVertical.LargeChange = visibleTilesY;
            scrollBarHorizontal.Maximum = tileMapData.GetLength(0);
            scrollBarVertical.Maximum = tileMapData.GetLength(1);
            //Enable/Disable scroll bars
            scrollBarHorizontal.Enabled = scrollBarHorizontal.LargeChange < scrollBarHorizontal.Maximum;
            scrollBarVertical.Enabled = scrollBarVertical.LargeChange < scrollBarVertical.Maximum;
        }

        private void SaveButtonPressed(object sender, EventArgs e)
        {
            Save();
        }

        private void SaveAsButtonPressed(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void UndoButtonPressed(object sender, EventArgs e)
        {
            Undo();
        }

        private void RedoButtonPressed(object sender, EventArgs e)
        {
            Redo();
        }

        private void scrollBarHorizontal_Scroll(object sender, ScrollEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"Horizontal scroll to {e.NewValue}.");
            scrollX = e.NewValue;
            UpdateScroll();
        }

        private void scrollBarVertical_Scroll(object sender, ScrollEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"Vertical scroll to {e.NewValue}.");
            scrollY = e.NewValue;
            UpdateScroll();
        }
    }
}