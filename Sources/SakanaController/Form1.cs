using System;
using System.IO.Ports;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.AxHost;

namespace SakanaController
{
    public partial class SakanaController : Form
    {
        private SerialPort serialPort;
        private Calibration calibration = new Calibration();
        private Curve curve = new Curve();
        private bool isCalibration = false;
        private bool isMeasurement = false;
        public SakanaController()
        {
            InitializeComponent();
            InitializeChart();
        }

        private void InitializeChart()
        {
            chartMain.Series.Clear();
            var dataCalibration = new Series
            {
                Name = "dataCalibration",
                ChartType = SeriesChartType.Line,
                BorderWidth = 5,
                Color = System.Drawing.ColorTranslator.FromHtml("#418cf0")
            };
            var dataMeasurement = new Series
            {
                Name = "dataMeasurement",
                ChartType = SeriesChartType.Line,
                BorderWidth = 5,
                Color = System.Drawing.ColorTranslator.FromHtml("#fcb441")
            };
            chartMain.Series.Add(dataCalibration);
            chartMain.Series.Add(dataMeasurement);
            chartMain.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
            chartMain.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
        }

        private void RefreshCOMs()
        {
            string[] ports = SerialPort.GetPortNames();
            lstCOMs.SelectedIndex = -1;
            lstCOMs.Items.Clear();
            foreach (string port in ports)
            {
                lstCOMs.Items.Add(port);
            }
        }

        private void SakanaController_Load(object sender, EventArgs e)
        {
            RefreshCOMs();
        }

        private void btnRefreshCOM_Click(object sender, EventArgs e)
        {
            RefreshCOMs();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (serialPort != null && serialPort.IsOpen)
                serialPort.Close();
            if (lstCOMs.SelectedItem != null)
            {
                string selectedPort = lstCOMs.SelectedItem.ToString();
                serialPort = new SerialPort(selectedPort, 115200);
                try
                {
                    serialPort.Open();
                    serialPort.DataReceived += SerialPort_DataReceived;
                    btnCalibrate.Enabled = true;
                    btnStart.Enabled = true;
                    lblConnection.Text = "Connected to " + selectedPort;
                }
                catch
                {
                    MessageBox.Show("Connection refused. Maybe the selected COM device is invalid. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a COM device.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCalibrate_Click(object sender, EventArgs e)
        {
            ClearChart();
            chartMain.ChartAreas[0].AxisX.Title = "E_output (V)";
            chartMain.ChartAreas[0].AxisY.Title = "-E_work (V)";
            calibration.Clear();
            chartMain.Series["dataCalibration"].Points.Clear();
            try
            {
                serialPort.Write("cali");
                isCalibration = true;
                btnStopCalibration.Enabled = true;
                btnCalibrate.Enabled = false;
                btnSave.Enabled = false;
                lblMeasurement.Text = "Calibrating";
                lblSave.Text = "NOT SAVED";
            }
            catch
            {
                MessageBox.Show("Failed to communicate.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string buffer = "";
        private List<string> ExtractBuffer(string input)
        {
            buffer += input;
            // a full response should be $xxx%
            string fragmentData = string.Empty;
            List<string> responds = new List<string>();
            fragmentData = string.Empty;
            int startIndex = 0;
            int length = input.Length;
            while (startIndex < length)
            {
                int dollarIndex = input.IndexOf('$', startIndex);
                if (dollarIndex == -1)
                {
                    //fragmentData = input.Substring(startIndex);
                    break;
                }
                int percentIndex = input.IndexOf('%', dollarIndex + 1);
                if (percentIndex == -1)
                {
                    fragmentData = input.Substring(dollarIndex);//the remaining part should be a segmented response.
                    break;
                }
                responds.Add(input.Substring(dollarIndex + 1, percentIndex - dollarIndex - 1));
                startIndex = percentIndex + 1;
            }
            buffer = fragmentData;
            return responds;
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string data = ((SerialPort)sender).ReadExisting();
            buffer += data;
            //a correct command should be like '$a%'
            this.Invoke(new Action(() =>
            {
                try
                {
                    if (isCalibration)
                    {
                        foreach (string response in ExtractBuffer(data))
                        {
                            ProcessReceivedDataForCalibration(response);
                        }
                    }
                    if (isMeasurement)
                    {
                        foreach (string response in ExtractBuffer(data))
                        {
                            ProcessReceivedDataForMeasurement(response);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to resolve message from Pico: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }));
        }

        private void ProcessReceivedDataForCalibration(string data)
        {
            data = data.Trim();
            if (data == "fini")
            {
                try
                {
                    btnCalibrate.Enabled = true;
                    btnStopCalibration.Enabled = false;
                    doFitting();
                    Thread.Sleep(100);
                    serialPort.Write($"par {this.calibration.Slope:F3} {this.calibration.Intercept:F3}");
                    isCalibration = false;
                    lblMeasurement.Text = "";
                    return;
                }
                catch
                {
                    MessageBox.Show("Failed to communicate.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
            string[] parts = data.Split(' ');
            double x = double.Parse(parts[0]);
            double y = double.Parse(parts[1]);
            chartMain.Series["dataCalibration"].Points.AddXY(x, y);
            calibration.AddDataPoint(x, y); // Add data point to calibration

        }

        private void ProcessReceivedDataForMeasurement(string data)
        {
            data = data.Trim();
            if (data == "fini")
            {
                btnStart.Enabled = true;
                btnStop.Enabled = false;
                isMeasurement = false;
                lblMeasurement.Text = "";
                btnSave.Enabled = true;
                return;
            }
            string[] parts = data.Split(' ');
            double x = double.Parse(parts[0]);
            double y = double.Parse(parts[1]);
            var smoothedPoint = curve.SmoothDataPoint(x, y);
            chartMain.Series["dataMeasurement"].Points.AddXY(smoothedPoint.x, smoothedPoint.y);
        }

        private void doFitting()
        {
            try
            {
                this.calibration.Fitting();
                MessageBox.Show("Calibration finished. Slope: " + this.calibration.Slope.ToString()
                    + "; Intercept: " + this.calibration.Intercept.ToString());
                lblCalibration.Text = "Calibrated: " + this.calibration.Slope.ToString()
                    + "; " + this.calibration.Intercept.ToString();
            }
            catch
            {
                MessageBox.Show("Failed to do fitting for calibration.");
            }
        }
        private void btnStopCalibration_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort.Write("stopcali");
                btnCalibrate.Enabled = true;
                btnStopCalibration.Enabled = false;
                isCalibration = false;
                doFitting();
                Thread.Sleep(100);
                serialPort.Write($"par {this.calibration.Slope:F3} {this.calibration.Intercept:F3}");
                lblMeasurement.Text = "";
            }
            catch
            {
                MessageBox.Show("Failed to stop calibration.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ClearChart()
        {
            foreach (var series in chartMain.Series)
            {
                series.Points.Clear();
            }
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (isCalibration)
            {
                MessageBox.Show("Logics failed: it is calibrating now.");
                return;
            }
            if (!double.TryParse(txtStartE.Text, out double startE) ||
                !double.TryParse(txtEndE.Text, out double endE) ||
                !double.TryParse(txtScanRate.Text, out double scanRate))
            {
                MessageBox.Show("Input parameters invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            curve.StartE = startE;
            curve.EndE = endE;
            curve.ScanRate = scanRate;

            if (!checkVolt(curve.StartE) || !checkVolt(curve.EndE))
            {
                MessageBox.Show("Voltage out of range. I will not proceed.");
                return;
            }

            string command = $"meas {startE:F3} {endE:F3} {scanRate:F3}";
            try
            {
                serialPort.Write(command);
                btnStart.Enabled = false;
                btnStop.Enabled = true;
                btnSave.Enabled = false;
                isMeasurement = true;
                chartMain.ChartAreas[0].AxisX.Title = "-E (V)";
                chartMain.ChartAreas[0].AxisY.Title = "-I (uA)";
                ClearChart();
                curve.Clear();
                lblMeasurement.Text = "Recording";
                lblSave.Text = "NOT SAVED";
            }
            catch
            {
                MessageBox.Show("Failed to communicate.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort.Write("stopmeas");
                isMeasurement = false;
                btnStart.Enabled = true;
                btnStop.Enabled = false;
                btnSave.Enabled = true;
                lblMeasurement.Text = "";
            }
            catch
            {
                MessageBox.Show("Failed to communicate.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                serialPort.Write($"range {lstRange.SelectedIndex}");
            }
            catch
            {
                MessageBox.Show("Failed to stop calibration.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                saveFileDialog.DefaultExt = "txt";
                saveFileDialog.AddExtension = true;
                saveFileDialog.Title = "Save Data";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;
                    try
                    {
                        curve.Save(filePath);
                        lblSave.Text = "SAVED";
                        MessageBox.Show("Data saved successfully.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to save data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnSetVolt_Click(object sender, EventArgs e)
        {
            if(!double.TryParse(txtVolt.Text, out double Vset))
            {
                MessageBox.Show("Voltage invalid.");
                return;
            }
            if (!checkVolt(Vset))
                MessageBox.Show("Voltage out of range. I will not proceed.");
            else
                serialPort.Write("set " + txtVolt.Text);
        }

        private bool checkVolt(double v)
        {
            if(this.calibration.Slope == 0)
                return false;
            else
            {
                double v_mcp4725 = (v - this.calibration.Intercept) / this.calibration.Slope;
                if (v_mcp4725 > 5 || v_mcp4725 < 0)
                    return false;
                else
                    return true;
            }
        }
    }
}
