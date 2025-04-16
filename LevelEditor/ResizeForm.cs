namespace MEDULevelEditor
{
    public partial class ResizeForm : Form
    {
        public int LeftChange => (int)leftValue.Value;
        public int RightChange => (int)rightValue.Value;
        public int TopChange => (int)topValue.Value;
        public int BottomChange => (int)bottomValue.Value;
        public int FinalWidth => currentWidth + LeftChange + RightChange;
        public int FinalHeight => currentHeight + TopChange + BottomChange;
        public DialogResult Result { get; private set; }

        private int currentWidth;
        private int currentHeight;

        public ResizeForm(int width, int height)
        {
            InitializeComponent();
            Reset(width, height);
        }

        public void Reset(int currentWidth, int currentHeight)
        {
            this.currentWidth = currentWidth;
            this.currentHeight = currentHeight;
            leftValue.Value = 0;
            rightValue.Value = 0;
            topValue.Value = 0;
            bottomValue.Value = 0;
            Result = DialogResult.None;
            UpdateText(null!, null!);
        }

        private void UpdateText(object sender, EventArgs e)
        {
            widthText.Text = FinalWidth.ToString();
            heightText.Text = FinalHeight.ToString();
        }

        private void Submit(object sender, EventArgs e)
        {
            if(LeftChange == 0 && RightChange == 0 && TopChange == 0 && BottomChange == 0)
            {
                Cancel(sender, e);
            }
            if (MessageBox.Show("Are you sure you'd like to resize the canvas?\nThis action can not be undone.", "Confirm Resize", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Result = DialogResult.OK;
                Close();
            }
        }

        private void Cancel(object sender, EventArgs e)
        {
            Result = DialogResult.Cancel;
            Close();
        }
    }
}
