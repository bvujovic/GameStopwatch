using System.Data;

namespace GameStopwatch
{
    public partial class FrmPastValues : Form
    {
        public FrmPastValues()
        {
            InitializeComponent();
        }

        private void FrmPastValues_Load(object sender, EventArgs e)
        {
            try
            {
                bs.DataMember = "DateMinutes";
                bs.DataSource = FrmMain.Ds;
                bs.Sort = "Date DESC";

                dgv.DataSource = bs;
                dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Dgv_SelectionChanged(object sender, EventArgs e)
        {
            int sum = 0, count = 0;
            foreach (var row in dgv.SelectedRows)
            {
                if ((row as DataGridViewRow)!.DataBoundItem is not DataRowView drv)
                    continue;
                var dateMin = drv.Row as Ds.DateMinutesRow;
                sum += dateMin!.Minutes;
                count++;
            }
            lblCount.Text = count.ToString();
            lblAvg.Text = count == 0 ? "/" : ((double)sum/count).ToString("0.0");
        }
    }
}
