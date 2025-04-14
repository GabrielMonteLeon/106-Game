namespace MEDULevelEditor
{
    public partial class ResizeForm : Form
    {
        public int LeftChange => (int)leftValue.Value;
        public int RightChange => (int)rightValue.Value;
        public int TopChange => (int)topValue.Value;
        public int BottomChange => (int)bottomValue.Value;
        public DialogResult Result { get; private set; }

        private int currentWidth;
        private int currentHeight;

        public ResizeForm()
        {
            InitializeComponent();
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

        }
    }
}
