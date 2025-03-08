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
            => bs.List.Cast<DataRowView>().Where(it => !it.IsNew)
                .Select(it => it.Row as Ds.DateMinutesRow).Reverse();

        private void FrmPastValues_Load(object sender, EventArgs e)
        {
            try
            {
                foreach (var dm in dateMinutes)
                    if (dm.IsWeekdayNull())
                        dm.Weekday = dm.Date.DayOfWeek.ToString();

                bs.DataSource = dateMinutes;
                bs.Sort = "Date DESC";
                bs.RemoveFilter();

                dgv.DataSource = bs;
                foreach (DataGridViewColumn col in dgv.Columns)
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                RefreshStatusBar();

                //var htmlPath = Path.Combine(Application.StartupPath, "chart.html");
                //webView.Source = new Uri(htmlPath);
                //webView.NavigationCompleted += (sender, e) => UpdateChartData();

                cmbFilterWeekDays.Items.Add(NoFilterItem);
                cmbFilterWeekDays.Items.Add(DayOfWeek.Monday);
                cmbFilterWeekDays.Items.Add(DayOfWeek.Tuesday);
                cmbFilterWeekDays.Items.Add(DayOfWeek.Wednesday);
                cmbFilterWeekDays.Items.Add(DayOfWeek.Thursday);
                cmbFilterWeekDays.Items.Add(DayOfWeek.Friday);
                cmbFilterWeekDays.Items.Add(DayOfWeek.Saturday);
                cmbFilterWeekDays.Items.Add(DayOfWeek.Sunday);
                cmbFilterWeekDays.Items.Add("Weekdays");
                cmbFilterWeekDays.Items.Add("Weekends");
                cmbFilterWeekDays.SelectedIndex = 0;

                cmbFilterPeriod.Items.Add(NoFilterItem);
                cmbFilterPeriod.Items.Add("Last 7 days");
                cmbFilterPeriod.Items.Add("Last 14 days");
                cmbFilterPeriod.Items.Add("Last 4 weeks");
                cmbFilterPeriod.Items.Add("Last 2 months");
                cmbFilterPeriod.Items.Add("Last 3 months");
                cmbFilterPeriod.Items.Add("Last 6 months");

                cmbFilterWeekDays.SelectedIndexChanged += CmbFilter_SelectedIndexChanged;
                cmbFilterPeriod.SelectedIndexChanged += CmbFilter_SelectedIndexChanged;
                cmbFilterWeekDays.SelectedIndex = Properties.Settings.Default.IdxFilterWeekDays;
                cmbFilterPeriod.SelectedIndex = Properties.Settings.Default.IdxFilterPeriod;
                timFirstRefresh.Start();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void TimFirstRefresh_Tick(object sender, EventArgs e)
        {
            timFirstRefresh.Stop();
            var htmlPath = Path.Combine(Application.StartupPath, "chart.html");
            webView.Source = new Uri(htmlPath);
            webView.NavigationCompleted += (sender, e) => UpdateChartData();
        }

        private const string NoFilterItem = "-- All --";

        private void UpdateChartData()
        {
            if (webView.CoreWebView2 != null)
            {
                var dms = DateMinutesDisplayed;
                var fmt = dms.Count() < 8 ? "yyyy-MM-dd" : "MM-dd";
                var chartData = new
                {
                    labels = dms.Select(it => it!.Date.ToString(fmt)),
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

        private void CmbFilter_SelectedIndexChanged(object? sender, EventArgs e)
        {
            try
            {
                var filter = "";
                cmbFilterPeriod.SelectedItem ??= NoFilterItem;
                var selItem = cmbFilterPeriod.SelectedItem.ToString();
                var d = DateTime.Today;
                if (selItem == "Last 7 days")
                    filter = $"Date >= '{(d.AddDays(-7)).ToShortDateString()}'";
                if (selItem == "Last 14 days")
                    filter = $"Date >= '{(d.AddDays(-14)).ToShortDateString()}'";
                if (selItem == "Last 4 weeks")
                    filter = $"Date >= '{(d.AddDays(-28)).ToShortDateString()}'";
                if (selItem == "Last 2 months")
                    filter = $"Date >= '{(d.AddMonths(-2)).ToShortDateString()}'";
                if (selItem == "Last 3 months")
                    filter = $"Date >= '{(d.AddMonths(-3)).ToShortDateString()}'";
                if (selItem == "Last 6 months")
                    filter = $"Date >= '{(d.AddMonths(-6)).ToShortDateString()}'";

                cmbFilterWeekDays.SelectedItem ??= NoFilterItem;
                selItem = cmbFilterWeekDays.SelectedItem.ToString();
                if (selItem != NoFilterItem)
                {
                    if (filter != "")
                        filter += " AND ";
                    if (cmbFilterWeekDays.SelectedIndex < 8)
                        filter += $"Weekday = '{cmbFilterWeekDays.SelectedItem}'";
                    if (selItem == "Weekdays")
                        filter += "Weekday IN ('Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday')";
                    if (selItem == "Weekends")
                        filter += "Weekday IN ('Saturday', 'Sunday')";
                }
                bs.Filter = filter;

                RefreshStatusBar();
                if (webView.Source != null)
                    webView.Reload();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private List<Ds.DateMinutesRow> addedZeroFilledRows;
        private bool currentAdded = false;
        private void ChkIncludeCurrent_CheckedChanged(object sender, EventArgs e)
        {
            var dm = dateMinutes.FindByDate(frmMain.CurrentDate);
            if (chkIncludeCurrent.Checked && dm == null)
            {
                addedZeroFilledRows = frmMain.FillWithZeros();
                dateMinutes.AddDateMinutesRow(frmMain.CurrentDate, frmMain.CurrentDate.DayOfWeek.ToString()
                    , frmMain.GetMinutesTotal());
                currentAdded = true;
            }
            if (!chkIncludeCurrent.Checked && dm != null)
            {
                RemoveTempRows(dm);
                currentAdded = false;
            }
            RefreshStatusBar();
            webView.Reload();
        }

        private void RemoveTempRows(Ds.DateMinutesRow dm)
        {
            foreach (var r in addedZeroFilledRows)
                dateMinutes.RemoveDateMinutesRow(r);
            dateMinutes.RemoveDateMinutesRow(dm);
        }

        private void FrmPastValues_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (currentAdded)
                RemoveTempRows(dateMinutes.FindByDate(frmMain.CurrentDate));
            Properties.Settings.Default.IdxFilterPeriod = cmbFilterPeriod.SelectedIndex;
            Properties.Settings.Default.IdxFilterWeekDays = cmbFilterWeekDays.SelectedIndex;
        }
    }
}
