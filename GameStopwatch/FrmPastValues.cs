using System.Data;
using System.Text.Json;

namespace GameStopwatch
{
    public partial class FrmPastValues : Form
    {
        public FrmPastValues(FrmMain frmMain)
        {
            InitializeComponent();
            this.frmMain = frmMain;
            dateMinutes = frmMain.Ds.DateMinutes;
        }

        private readonly FrmMain frmMain;
        private readonly Ds.DateMinutesDataTable dateMinutes;
        private IEnumerable<Ds.DateMinutesRow?> DateMinutesDisplayed
            => bs.List.Cast<DataRowView>().Select(it => it.Row as Ds.DateMinutesRow).Reverse();

        private void FrmPastValues_Load(object sender, EventArgs e)
        {
            try
            {
                foreach (var dm in dateMinutes)
                    if (dm.IsWeekdayNull())
                        dm.Weekday = dm.Date.DayOfWeek.ToString();

                bs.DataSource = dateMinutes;
                bs.Sort = "Date DESC";

                dgv.DataSource = bs;
                foreach (DataGridViewColumn col in dgv.Columns)
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                RefreshStatusBar();

                var htmlPath = Path.Combine(Application.StartupPath, "chart.html");
                webView.Source = new Uri(htmlPath);
                webView.NavigationCompleted += (sender, e) => UpdateChartData();

                cmbFilter.Items.Add(DayOfWeek.Monday);
                cmbFilter.Items.Add(DayOfWeek.Tuesday);
                cmbFilter.Items.Add(DayOfWeek.Wednesday);
                cmbFilter.Items.Add(DayOfWeek.Thursday);
                cmbFilter.Items.Add(DayOfWeek.Friday);
                cmbFilter.Items.Add(DayOfWeek.Saturday);
                cmbFilter.Items.Add(DayOfWeek.Sunday);
                cmbFilter.Items.Add("Weekdays");
                cmbFilter.Items.Add("Weekends");
                cmbFilter.Items.Add("Last 7 days");
                cmbFilter.Items.Add("Last 14 days");
                cmbFilter.Items.Add("Last 4 weeks");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void UpdateChartData()
        {
            if (webView.CoreWebView2 != null)
            {
                var dms = DateMinutesDisplayed;
                var chartData = new
                {
                    labels = dms.Select(it => it!.Date.ToShortDateString()),
                    values = dms.Select(it => it!.Minutes),
                    label = "Gaming Time by Dates"
                };
                var jsonData = JsonSerializer.Serialize(chartData);
                webView.CoreWebView2.ExecuteScriptAsync($"updateChart('{jsonData}')");
            }
        }

        private void WebView_Resize(object sender, EventArgs e)
            => webView.Reload();

        private void Dgv_SelectionChanged(object sender, EventArgs e)
            => RefreshStatusBar();

        private void RefreshStatusBar()
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
            if (dgv.SelectedRows.Count == 0)
            {
                var dms = DateMinutesDisplayed;
                count = dms.Count();
                sum = dms.Sum(it => it!.Minutes);
            }
            lblCount.Text = count.ToString();
            lblAvg.Text = count == 0 ? "/" : ((double)sum / count).ToString("0.0");
        }

        private void CmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbFilter.SelectedItem == null)
                    bs.RemoveFilter();
                else if (cmbFilter.SelectedIndex < 7)
                    bs.Filter = $"Weekday = '{cmbFilter.SelectedItem}'";
                else
                {
                    var selItem = cmbFilter.SelectedItem.ToString();
                    if (selItem == "Weekdays")
                        bs.Filter = "Weekday IN ('Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday')";
                    if (selItem == "Weekends")
                        bs.Filter = "Weekday IN ('Saturday', 'Sunday')";

                    var d = DateTime.Today;
                    if (selItem == "Last 7 days")
                        bs.Filter = $"Date >= '{(d.AddDays(-7)).ToShortDateString()}'";
                    if (selItem == "Last 14 days")
                        bs.Filter = $"Date >= '{(d.AddDays(-14)).ToShortDateString()}'";
                    if (selItem == "Last 4 weeks")
                        bs.Filter = $"Date >= '{(d.AddDays(-28)).ToShortDateString()}'";
                }
                RefreshStatusBar();
                webView.Reload();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void BtnFilterReset_Click(object sender, EventArgs e)
        {
            cmbFilter.SelectedItem = null;
        }

        private bool currentAdded = false;
        private void ChkIncludeCurrent_CheckedChanged(object sender, EventArgs e)
        {
            var dm = dateMinutes.FindByDate(frmMain.CurrentDate);
            if (chkIncludeCurrent.Checked && dm == null)
            {
                dateMinutes.AddDateMinutesRow(frmMain.CurrentDate, frmMain.CurrentDate.DayOfWeek.ToString()
                    , frmMain.GetMinutesTotal());
                currentAdded = true;
            }
            if (!chkIncludeCurrent.Checked && dm != null)
            {
                dateMinutes.RemoveDateMinutesRow(dm);
                currentAdded = false;
            }
            RefreshStatusBar();
            webView.Reload();
        }

        private void FrmPastValues_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (currentAdded)
                dateMinutes.RemoveDateMinutesRow(dateMinutes.FindByDate(frmMain.CurrentDate));
        }
    }
}
