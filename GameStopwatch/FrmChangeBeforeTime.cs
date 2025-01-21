namespace GameStopwatch
{
    public partial class FrmChangeBeforeTime : Form
    {
        public FrmChangeBeforeTime()
        {
            InitializeComponent();

            numMinutesBefore.Select(0, 10);
        }

        public int MinutesBefore
        {
            get { return (int)numMinutesBefore.Value; }
            set { numMinutesBefore.Value = value; }
        }

        private void NumMinutesBefore_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                DialogResult = (e.KeyCode == Keys.Enter) ? DialogResult.OK : DialogResult.Cancel;
            }
        }
    }
}
