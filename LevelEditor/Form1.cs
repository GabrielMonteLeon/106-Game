namespace MEDULevelEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void CreateNewLevel(object sender, EventArgs e)
        {
            int width;
            int height;

            bool error = false;
            string errorMessage = "Errors:";

            if(!int.TryParse(widthTextBox.Text, out width))
            {
                error = true;
                errorMessage += "\n- Width must be an integer.";
            }
            else if(width < 10)
            {
                error = true;
                errorMessage += "\n- Width too small. Must be at least 10.";
            }
            else if(width > 30)
            {
                error = true;
                errorMessage += "\n- Width too big. Must be at most 30.";
            }


            if (!int.TryParse(heightTextBox.Text, out height))
            {
                error = true;
                errorMessage += "\n- Height must be an integer.";
            }
            else if (height < 10)
            {
                error = true;
                errorMessage += "\n- Height too small. Must be at least 10.";
            }
            else if (height > 30)
            {
                error = true;
                errorMessage += "\n- Height too big. Must be at most 30.";
            }

            if(error)
            {
                MessageBox.Show(errorMessage, "Error creating map", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                EditorForm editor = new EditorForm();
                editor.InitNewMap(width, height);
                editor.ShowDialog();
            }
        }

        private void LoadLevelFile(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Load Level File";
            dialog.Filter = "Level Files (*.level)|*.level";
            DialogResult result = dialog.ShowDialog();

            if (result != DialogResult.OK)
                return;

            EditorForm editor = new EditorForm();
            if (editor.LoadMap(dialog.FileName))
                editor.ShowDialog();
        }
    }
}
