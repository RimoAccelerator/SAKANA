namespace SakanaController
{
    partial class SakanaController
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            chartMain = new System.Windows.Forms.DataVisualization.Charting.Chart();
            txtStartE = new TextBox();
            txtEndE = new TextBox();
            btnStart = new Button();
            btnStop = new Button();
            label1 = new Label();
            label2 = new Label();
            txtScanRate = new TextBox();
            label3 = new Label();
            btnCalibrate = new Button();
            lstCOMs = new ComboBox();
            label4 = new Label();
            btnSave = new Button();
            btnRefreshCOM = new Button();
            btnStopCalibration = new Button();
            grpCalibration = new GroupBox();
            btnConnect = new Button();
            statusStrip = new StatusStrip();
            lblConnection = new ToolStripStatusLabel();
            lblCalibration = new ToolStripStatusLabel();
            lblSave = new ToolStripStatusLabel();
            lblMeasurement = new ToolStripStatusLabel();
            lstRange = new ComboBox();
            label5 = new Label();
            grpMeasurement = new GroupBox();
            radioLSV = new RadioButton();
            radioCV = new RadioButton();
            btnSetVolt = new Button();
            txtVolt = new TextBox();
            grpConnection = new GroupBox();
            saveFileDialog = new SaveFileDialog();
            label6 = new Label();
            groupBox1 = new GroupBox();
            btnSaveIT = new Button();
            btnStopIT = new Button();
            ((System.ComponentModel.ISupportInitialize)chartMain).BeginInit();
            grpCalibration.SuspendLayout();
            statusStrip.SuspendLayout();
            grpMeasurement.SuspendLayout();
            grpConnection.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // chartMain
            // 
            chartArea1.Name = "ChartArea1";
            chartMain.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chartMain.Legends.Add(legend1);
            chartMain.Location = new Point(23, 90);
            chartMain.Margin = new Padding(2);
            chartMain.Name = "chartMain";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chartMain.Series.Add(series1);
            chartMain.Size = new Size(1169, 549);
            chartMain.TabIndex = 0;
            chartMain.Text = "chart1";
            // 
            // txtStartE
            // 
            txtStartE.Location = new Point(60, 14);
            txtStartE.Margin = new Padding(2);
            txtStartE.Name = "txtStartE";
            txtStartE.Size = new Size(102, 23);
            txtStartE.TabIndex = 1;
            // 
            // txtEndE
            // 
            txtEndE.Location = new Point(60, 44);
            txtEndE.Margin = new Padding(2);
            txtEndE.Name = "txtEndE";
            txtEndE.Size = new Size(102, 23);
            txtEndE.TabIndex = 2;
            // 
            // btnStart
            // 
            btnStart.Enabled = false;
            btnStart.Location = new Point(176, 43);
            btnStart.Margin = new Padding(2);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(75, 25);
            btnStart.TabIndex = 3;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // btnStop
            // 
            btnStop.Enabled = false;
            btnStop.Location = new Point(255, 44);
            btnStop.Margin = new Padding(2);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(75, 25);
            btnStop.TabIndex = 4;
            btnStop.Text = "Stop";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(8, 18);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(46, 17);
            label1.TabIndex = 0;
            label1.Text = "Start E";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(8, 48);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(41, 17);
            label2.TabIndex = 5;
            label2.Text = "End E";
            // 
            // txtScanRate
            // 
            txtScanRate.Location = new Point(284, 19);
            txtScanRate.Margin = new Padding(2);
            txtScanRate.Name = "txtScanRate";
            txtScanRate.Size = new Size(102, 23);
            txtScanRate.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(176, 19);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(104, 17);
            label3.TabIndex = 7;
            label3.Text = "Scan rate (mV/s)";
            // 
            // btnCalibrate
            // 
            btnCalibrate.Enabled = false;
            btnCalibrate.Location = new Point(12, 20);
            btnCalibrate.Margin = new Padding(2);
            btnCalibrate.Name = "btnCalibrate";
            btnCalibrate.Size = new Size(75, 25);
            btnCalibrate.TabIndex = 8;
            btnCalibrate.Text = "Start";
            btnCalibrate.UseVisualStyleBackColor = true;
            btnCalibrate.Click += btnCalibrate_Click;
            // 
            // lstCOMs
            // 
            lstCOMs.FormattingEnabled = true;
            lstCOMs.Location = new Point(56, 19);
            lstCOMs.Margin = new Padding(2);
            lstCOMs.Name = "lstCOMs";
            lstCOMs.Size = new Size(123, 25);
            lstCOMs.TabIndex = 9;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 19);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(38, 17);
            label4.TabIndex = 10;
            label4.Text = "COM";
            // 
            // btnSave
            // 
            btnSave.Enabled = false;
            btnSave.Location = new Point(334, 44);
            btnSave.Margin = new Padding(2);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 25);
            btnSave.TabIndex = 11;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnRefreshCOM
            // 
            btnRefreshCOM.Location = new Point(209, 48);
            btnRefreshCOM.Margin = new Padding(2);
            btnRefreshCOM.Name = "btnRefreshCOM";
            btnRefreshCOM.Size = new Size(75, 25);
            btnRefreshCOM.TabIndex = 12;
            btnRefreshCOM.Text = "Refresh";
            btnRefreshCOM.UseVisualStyleBackColor = true;
            btnRefreshCOM.Click += btnRefreshCOM_Click;
            // 
            // btnStopCalibration
            // 
            btnStopCalibration.Enabled = false;
            btnStopCalibration.Location = new Point(12, 49);
            btnStopCalibration.Margin = new Padding(2);
            btnStopCalibration.Name = "btnStopCalibration";
            btnStopCalibration.Size = new Size(75, 25);
            btnStopCalibration.TabIndex = 13;
            btnStopCalibration.Text = "Stop";
            btnStopCalibration.UseVisualStyleBackColor = true;
            btnStopCalibration.Click += btnStopCalibration_Click;
            // 
            // grpCalibration
            // 
            grpCalibration.Controls.Add(btnCalibrate);
            grpCalibration.Controls.Add(btnStopCalibration);
            grpCalibration.Location = new Point(317, 3);
            grpCalibration.Margin = new Padding(2);
            grpCalibration.Name = "grpCalibration";
            grpCalibration.Padding = new Padding(2);
            grpCalibration.Size = new Size(100, 78);
            grpCalibration.TabIndex = 14;
            grpCalibration.TabStop = false;
            grpCalibration.Text = "Calibration";
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(209, 19);
            btnConnect.Margin = new Padding(2);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(75, 25);
            btnConnect.TabIndex = 15;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // statusStrip
            // 
            statusStrip.ImageScalingSize = new Size(32, 32);
            statusStrip.Items.AddRange(new ToolStripItem[] { lblConnection, lblCalibration, lblSave, lblMeasurement });
            statusStrip.Location = new Point(0, 623);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new Padding(0, 0, 7, 0);
            statusStrip.Size = new Size(1209, 22);
            statusStrip.TabIndex = 16;
            // 
            // lblConnection
            // 
            lblConnection.Name = "lblConnection";
            lblConnection.Size = new Size(115, 17);
            lblConnection.Text = "NOT CONNECTED";
            // 
            // lblCalibration
            // 
            lblCalibration.Name = "lblCalibration";
            lblCalibration.Size = new Size(112, 17);
            lblCalibration.Text = "NOT CALIBRATED";
            // 
            // lblSave
            // 
            lblSave.Name = "lblSave";
            lblSave.Size = new Size(0, 17);
            // 
            // lblMeasurement
            // 
            lblMeasurement.Name = "lblMeasurement";
            lblMeasurement.Size = new Size(78, 17);
            lblMeasurement.Text = "NOT SAVED";
            // 
            // lstRange
            // 
            lstRange.FormattingEnabled = true;
            lstRange.Items.AddRange(new object[] { "100 uA", "10 uA", "1 uA" });
            lstRange.Location = new Point(56, 48);
            lstRange.Margin = new Padding(2);
            lstRange.Name = "lstRange";
            lstRange.Size = new Size(123, 25);
            lstRange.TabIndex = 17;
            lstRange.SelectedIndexChanged += lstRange_SelectedIndexChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(7, 51);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(45, 17);
            label5.TabIndex = 18;
            label5.Text = "Range";
            // 
            // grpMeasurement
            // 
            grpMeasurement.Controls.Add(radioLSV);
            grpMeasurement.Controls.Add(radioCV);
            grpMeasurement.Controls.Add(btnStart);
            grpMeasurement.Controls.Add(txtStartE);
            grpMeasurement.Controls.Add(txtEndE);
            grpMeasurement.Controls.Add(btnStop);
            grpMeasurement.Controls.Add(label1);
            grpMeasurement.Controls.Add(label2);
            grpMeasurement.Controls.Add(txtScanRate);
            grpMeasurement.Controls.Add(btnSave);
            grpMeasurement.Controls.Add(label3);
            grpMeasurement.Location = new Point(422, 3);
            grpMeasurement.Name = "grpMeasurement";
            grpMeasurement.Size = new Size(533, 78);
            grpMeasurement.TabIndex = 19;
            grpMeasurement.TabStop = false;
            grpMeasurement.Text = "Measurement";
            // 
            // radioLSV
            // 
            radioLSV.AutoSize = true;
            radioLSV.Location = new Point(425, 43);
            radioLSV.Name = "radioLSV";
            radioLSV.Size = new Size(47, 21);
            radioLSV.TabIndex = 13;
            radioLSV.Text = "LSV";
            radioLSV.UseVisualStyleBackColor = true;
            radioLSV.CheckedChanged += radioLSV_CheckedChanged;
            // 
            // radioCV
            // 
            radioCV.AutoSize = true;
            radioCV.Checked = true;
            radioCV.Location = new Point(425, 19);
            radioCV.Name = "radioCV";
            radioCV.Size = new Size(42, 21);
            radioCV.TabIndex = 12;
            radioCV.TabStop = true;
            radioCV.Text = "CV";
            radioCV.UseVisualStyleBackColor = true;
            // 
            // btnSetVolt
            // 
            btnSetVolt.Location = new Point(9, 50);
            btnSetVolt.Name = "btnSetVolt";
            btnSetVolt.Size = new Size(70, 23);
            btnSetVolt.TabIndex = 13;
            btnSetVolt.Text = "Start";
            btnSetVolt.UseVisualStyleBackColor = true;
            btnSetVolt.Click += btnSetVolt_Click;
            // 
            // txtVolt
            // 
            txtVolt.Location = new Point(124, 20);
            txtVolt.Margin = new Padding(2);
            txtVolt.Name = "txtVolt";
            txtVolt.Size = new Size(71, 23);
            txtVolt.TabIndex = 12;
            // 
            // grpConnection
            // 
            grpConnection.Controls.Add(lstCOMs);
            grpConnection.Controls.Add(label4);
            grpConnection.Controls.Add(label5);
            grpConnection.Controls.Add(btnRefreshCOM);
            grpConnection.Controls.Add(lstRange);
            grpConnection.Controls.Add(btnConnect);
            grpConnection.Location = new Point(23, 3);
            grpConnection.Name = "grpConnection";
            grpConnection.Size = new Size(289, 78);
            grpConnection.TabIndex = 20;
            grpConnection.TabStop = false;
            grpConnection.Text = "Connection";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(9, 23);
            label6.Name = "label6";
            label6.Size = new Size(110, 17);
            label6.TabIndex = 14;
            label6.Text = "Set constant E (V)";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnSaveIT);
            groupBox1.Controls.Add(btnStopIT);
            groupBox1.Controls.Add(btnSetVolt);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(txtVolt);
            groupBox1.Location = new Point(961, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(236, 78);
            groupBox1.TabIndex = 21;
            groupBox1.TabStop = false;
            groupBox1.Text = "Potentiostat";
            // 
            // btnSaveIT
            // 
            btnSaveIT.Location = new Point(161, 51);
            btnSaveIT.Name = "btnSaveIT";
            btnSaveIT.Size = new Size(70, 23);
            btnSaveIT.TabIndex = 16;
            btnSaveIT.Text = "Save";
            btnSaveIT.UseVisualStyleBackColor = true;
            btnSaveIT.Click += btnSaveIT_Click;
            // 
            // btnStopIT
            // 
            btnStopIT.Enabled = false;
            btnStopIT.Location = new Point(85, 51);
            btnStopIT.Name = "btnStopIT";
            btnStopIT.Size = new Size(70, 23);
            btnStopIT.TabIndex = 15;
            btnStopIT.Text = "Stop";
            btnStopIT.UseVisualStyleBackColor = true;
            btnStopIT.Click += btnStopIT_Click;
            // 
            // SakanaController
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1209, 645);
            Controls.Add(groupBox1);
            Controls.Add(grpConnection);
            Controls.Add(grpMeasurement);
            Controls.Add(statusStrip);
            Controls.Add(grpCalibration);
            Controls.Add(chartMain);
            Margin = new Padding(2);
            Name = "SakanaController";
            Text = "SakanaController";
            Load += SakanaController_Load;
            ((System.ComponentModel.ISupportInitialize)chartMain).EndInit();
            grpCalibration.ResumeLayout(false);
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            grpMeasurement.ResumeLayout(false);
            grpMeasurement.PerformLayout();
            grpConnection.ResumeLayout(false);
            grpConnection.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartMain;
        private TextBox txtStartE;
        private TextBox txtEndE;
        private Button btnStart;
        private Button btnStop;
        private Label label1;
        private Label label2;
        private TextBox txtScanRate;
        private Label label3;
        private Button btnCalibrate;
        private ComboBox lstCOMs;
        private Label label4;
        private Button btnSave;
        private Button btnRefreshCOM;
        private Button btnStopCalibration;
        private GroupBox grpCalibration;
        private Button btnConnect;
        private StatusStrip statusStrip;
        private ComboBox lstRange;
        private Label label5;
        private GroupBox grpMeasurement;
        private GroupBox grpConnection;
        private ToolStripStatusLabel lblConnection;
        private ToolStripStatusLabel lblCalibration;
        private ToolStripStatusLabel lblSave;
        private ToolStripStatusLabel lblMeasurement;
        private SaveFileDialog saveFileDialog;
        private Button btnSetVolt;
        private TextBox txtVolt;
        private Label label6;
        private GroupBox groupBox1;
        private Button btnStopIT;
        private Button btnSaveIT;
        private RadioButton radioLSV;
        private RadioButton radioCV;
    }
}
