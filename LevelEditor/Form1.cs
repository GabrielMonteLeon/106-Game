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


            if (!int.TryParse(heightTextBox.Text, out height))
            {
                error = true;
                errorMessage += "\n- Height must be an integer.";
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
