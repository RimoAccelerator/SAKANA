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
            txtMaxCycle = new TextBox();
            label7 = new Label();
            radioLSV = new RadioButton();
            radioCV = new RadioButton();
            btnSetVolt = new Button();
            txtVolt = new TextBox();
            grpConnection = new GroupBox();
            saveFileDialog = new SaveFileDialog();
            label6 = new Label();
            btnSaveIT = new Button();
            btnStopIT = new Button();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            tabPage3 = new TabPage();
            btnDPVSave = new Button();
            btnDPVStop = new Button();
            btnDPVStart = new Button();
            txtDPVPeriod = new TextBox();
            label14 = new Label();
            txtDPVSampwith = new TextBox();
            txtDPVPulwidth = new TextBox();
            label13 = new Label();
            label12 = new Label();
            txtDPVEpul = new TextBox();
            label11 = new Label();
            txtDPVIncre = new TextBox();
            label10 = new Label();
            txtDPVEndE = new TextBox();
            txtDPVStartE = new TextBox();
            label9 = new Label();
            label8 = new Label();
            ((System.ComponentModel.ISupportInitialize)chartMain).BeginInit();
            grpCalibration.SuspendLayout();
            statusStrip.SuspendLayout();
            grpConnection.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
            SuspendLayout();
            // 
            // chartMain
            // 
            chartArea1.Name = "ChartArea1";
            chartMain.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chartMain.Legends.Add(legend1);
            chartMain.Location = new Point(23, 149);
            chartMain.Margin = new Padding(2);
            chartMain.Name = "chartMain";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chartMain.Series.Add(series1);
            chartMain.Size = new Size(1169, 490);
            chartMain.TabIndex = 0;
            chartMain.Text = "chart1";
            // 
            // txtStartE
            // 
            txtStartE.Location = new Point(118, 5);
            txtStartE.Margin = new Padding(2);
            txtStartE.Name = "txtStartE";
            txtStartE.Size = new Size(102, 23);
            txtStartE.TabIndex = 1;
            // 
            // txtEndE
            // 
            txtEndE.Location = new Point(118, 42);
            txtEndE.Margin = new Padding(2);
            txtEndE.Name = "txtEndE";
            txtEndE.Size = new Size(102, 23);
            txtEndE.TabIndex = 2;
            // 
            // btnStart
            // 
            btnStart.Enabled = false;
            btnStart.Location = new Point(418, 10);
            btnStart.Margin = new Padding(2);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(70, 25);
            btnStart.TabIndex = 3;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // btnStop
            // 
            btnStop.Enabled = false;
            btnStop.Location = new Point(418, 41);
            btnStop.Margin = new Padding(2);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(70, 25);
            btnStop.TabIndex = 4;
            btnStop.Text = "Stop";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(8, 14);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(66, 17);
            label1.TabIndex = 0;
            label1.Text = "Start E (V)";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(8, 44);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(61, 17);
            label2.TabIndex = 5;
            label2.Text = "End E (V)";
            // 
            // txtScanRate
            // 
            txtScanRate.Location = new Point(118, 75);
            txtScanRate.Margin = new Padding(2);
            txtScanRate.Name = "txtScanRate";
            txtScanRate.Size = new Size(102, 23);
            txtScanRate.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(8, 78);
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
            lstCOMs.Size = new Size(105, 25);
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
            btnSave.Location = new Point(418, 70);
            btnSave.Margin = new Padding(2);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(70, 25);
            btnSave.TabIndex = 11;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnRefreshCOM
            // 
            btnRefreshCOM.Location = new Point(56, 77);
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
            btnStopCalibration.Location = new Point(12, 97);
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
            grpCalibration.Location = new Point(198, 3);
            grpCalibration.Margin = new Padding(2);
            grpCalibration.Name = "grpCalibration";
            grpCalibration.Padding = new Padding(2);
            grpCalibration.Size = new Size(100, 137);
            grpCalibration.TabIndex = 14;
            grpCalibration.TabStop = false;
            grpCalibration.Text = "Calibration";
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(56, 106);
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
            statusStrip.Location = new Point(0, 646);
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
            lstRange.Size = new Size(105, 25);
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
            // txtMaxCycle
            // 
            txtMaxCycle.Location = new Point(293, 17);
            txtMaxCycle.Margin = new Padding(2);
            txtMaxCycle.Name = "txtMaxCycle";
            txtMaxCycle.Size = new Size(66, 23);
            txtMaxCycle.TabIndex = 15;
            txtMaxCycle.Text = "1";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(363, 21);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(44, 17);
            label7.TabIndex = 14;
            label7.Text = "Cycles";
            // 
            // radioLSV
            // 
            radioLSV.AutoSize = true;
            radioLSV.Location = new Point(246, 42);
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
            radioCV.Location = new Point(246, 17);
            radioCV.Name = "radioCV";
            radioCV.Size = new Size(42, 21);
            radioCV.TabIndex = 12;
            radioCV.TabStop = true;
            radioCV.Text = "CV";
            radioCV.UseVisualStyleBackColor = true;
            // 
            // btnSetVolt
            // 
            btnSetVolt.Location = new Point(55, 52);
            btnSetVolt.Name = "btnSetVolt";
            btnSetVolt.Size = new Size(70, 23);
            btnSetVolt.TabIndex = 13;
            btnSetVolt.Text = "Start";
            btnSetVolt.UseVisualStyleBackColor = true;
            btnSetVolt.Click += btnSetVolt_Click;
            // 
            // txtVolt
            // 
            txtVolt.Location = new Point(139, 19);
            txtVolt.Margin = new Padding(2);
            txtVolt.Name = "txtVolt";
            txtVolt.Size = new Size(107, 23);
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
            grpConnection.Size = new Size(170, 137);
            grpConnection.TabIndex = 20;
            grpConnection.TabStop = false;
            grpConnection.Text = "Connection";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(24, 22);
            label6.Name = "label6";
            label6.Size = new Size(110, 17);
            label6.TabIndex = 14;
            label6.Text = "Set constant E (V)";
            // 
            // btnSaveIT
            // 
            btnSaveIT.Location = new Point(249, 52);
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
            btnStopIT.Location = new Point(148, 52);
            btnStopIT.Name = "btnStopIT";
            btnStopIT.Size = new Size(70, 23);
            btnStopIT.TabIndex = 15;
            btnStopIT.Text = "Stop";
            btnStopIT.UseVisualStyleBackColor = true;
            btnStopIT.Click += btnStopIT_Click;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Location = new Point(303, 4);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(889, 140);
            tabControl1.TabIndex = 22;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(txtMaxCycle);
            tabPage1.Controls.Add(label7);
            tabPage1.Controls.Add(label3);
            tabPage1.Controls.Add(radioLSV);
            tabPage1.Controls.Add(btnSave);
            tabPage1.Controls.Add(radioCV);
            tabPage1.Controls.Add(txtScanRate);
            tabPage1.Controls.Add(btnStart);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(txtStartE);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(txtEndE);
            tabPage1.Controls.Add(btnStop);
            tabPage1.Location = new Point(4, 26);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(881, 110);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "CV/LSV";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(btnSaveIT);
            tabPage2.Controls.Add(txtVolt);
            tabPage2.Controls.Add(btnStopIT);
            tabPage2.Controls.Add(label6);
            tabPage2.Controls.Add(btnSetVolt);
            tabPage2.Location = new Point(4, 26);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(881, 110);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "IT";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(btnDPVSave);
            tabPage3.Controls.Add(btnDPVStop);
            tabPage3.Controls.Add(btnDPVStart);
            tabPage3.Controls.Add(txtDPVPeriod);
            tabPage3.Controls.Add(label14);
            tabPage3.Controls.Add(txtDPVSampwith);
            tabPage3.Controls.Add(txtDPVPulwidth);
            tabPage3.Controls.Add(label13);
            tabPage3.Controls.Add(label12);
            tabPage3.Controls.Add(txtDPVEpul);
            tabPage3.Controls.Add(label11);
            tabPage3.Controls.Add(txtDPVIncre);
            tabPage3.Controls.Add(label10);
            tabPage3.Controls.Add(txtDPVEndE);
            tabPage3.Controls.Add(txtDPVStartE);
            tabPage3.Controls.Add(label9);
            tabPage3.Controls.Add(label8);
            tabPage3.Location = new Point(4, 26);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(881, 110);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "DPV";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnDPVSave
            // 
            btnDPVSave.Location = new Point(473, 71);
            btnDPVSave.Name = "btnDPVSave";
            btnDPVSave.Size = new Size(75, 23);
            btnDPVSave.TabIndex = 16;
            btnDPVSave.Text = "Save";
            btnDPVSave.UseVisualStyleBackColor = true;
            btnDPVSave.Click += btnDPVSave_Click;
            // 
            // btnDPVStop
            // 
            btnDPVStop.Enabled = false;
            btnDPVStop.Location = new Point(521, 39);
            btnDPVStop.Name = "btnDPVStop";
            btnDPVStop.Size = new Size(75, 23);
            btnDPVStop.TabIndex = 15;
            btnDPVStop.Text = "Stop";
            btnDPVStop.UseVisualStyleBackColor = true;
            btnDPVStop.Click += btnDPVStop_Click;
            // 
            // btnDPVStart
            // 
            btnDPVStart.Location = new Point(426, 38);
            btnDPVStart.Name = "btnDPVStart";
            btnDPVStart.Size = new Size(75, 23);
            btnDPVStart.TabIndex = 14;
            btnDPVStart.Text = "Start";
            btnDPVStart.UseVisualStyleBackColor = true;
            btnDPVStart.Click += btnDPVStart_Click;
            // 
            // txtDPVPeriod
            // 
            txtDPVPeriod.Location = new Point(496, 6);
            txtDPVPeriod.Name = "txtDPVPeriod";
            txtDPVPeriod.Size = new Size(100, 23);
            txtDPVPeriod.TabIndex = 13;
            txtDPVPeriod.Text = "0.5";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(426, 9);
            label14.Name = "label14";
            label14.Size = new Size(64, 17);
            label14.TabIndex = 12;
            label14.Text = "Period (s)";
            // 
            // txtDPVSampwith
            // 
            txtDPVSampwith.Location = new Point(306, 71);
            txtDPVSampwith.Name = "txtDPVSampwith";
            txtDPVSampwith.Size = new Size(100, 23);
            txtDPVSampwith.TabIndex = 11;
            txtDPVSampwith.Text = "0.016";
            // 
            // txtDPVPulwidth
            // 
            txtDPVPulwidth.Location = new Point(306, 36);
            txtDPVPulwidth.Name = "txtDPVPulwidth";
            txtDPVPulwidth.Size = new Size(100, 23);
            txtDPVPulwidth.TabIndex = 10;
            txtDPVPulwidth.Text = "0.050";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(185, 74);
            label13.Name = "label13";
            label13.Size = new Size(115, 17);
            label13.TabIndex = 9;
            label13.Text = "Sampling width (s)";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(184, 41);
            label12.Name = "label12";
            label12.Size = new Size(91, 17);
            label12.TabIndex = 8;
            label12.Text = "Pulse width (s)";
            // 
            // txtDPVEpul
            // 
            txtDPVEpul.Location = new Point(306, 7);
            txtDPVEpul.Name = "txtDPVEpul";
            txtDPVEpul.Size = new Size(100, 23);
            txtDPVEpul.TabIndex = 7;
            txtDPVEpul.Text = "0.050";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(186, 10);
            label11.Name = "label11";
            label11.Size = new Size(124, 17);
            label11.TabIndex = 6;
            label11.Text = "Pulse magnitude (V)";
            // 
            // txtDPVIncre
            // 
            txtDPVIncre.Location = new Point(105, 71);
            txtDPVIncre.Name = "txtDPVIncre";
            txtDPVIncre.Size = new Size(76, 23);
            txtDPVIncre.TabIndex = 5;
            txtDPVIncre.Text = "0.005";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(13, 74);
            label10.Name = "label10";
            label10.Size = new Size(86, 17);
            label10.TabIndex = 4;
            label10.Text = "Increment (V)";
            // 
            // txtDPVEndE
            // 
            txtDPVEndE.Location = new Point(81, 39);
            txtDPVEndE.Name = "txtDPVEndE";
            txtDPVEndE.Size = new Size(100, 23);
            txtDPVEndE.TabIndex = 3;
            // 
            // txtDPVStartE
            // 
            txtDPVStartE.Location = new Point(81, 6);
            txtDPVStartE.Name = "txtDPVStartE";
            txtDPVStartE.Size = new Size(100, 23);
            txtDPVStartE.TabIndex = 2;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(13, 45);
            label9.Name = "label9";
            label9.Size = new Size(61, 17);
            label9.TabIndex = 1;
            label9.Text = "End E (V)";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(13, 10);
            label8.Name = "label8";
            label8.Size = new Size(66, 17);
            label8.TabIndex = 0;
            label8.Text = "Start E (V)";
            // 
            // SakanaController
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1209, 668);
            Controls.Add(tabControl1);
            Controls.Add(grpConnection);
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
            grpConnection.ResumeLayout(false);
            grpConnection.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
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
        private GroupBox grpConnection;
        private ToolStripStatusLabel lblConnection;
        private ToolStripStatusLabel lblCalibration;
        private ToolStripStatusLabel lblSave;
        private ToolStripStatusLabel lblMeasurement;
        private SaveFileDialog saveFileDialog;
        private Button btnSetVolt;
        private TextBox txtVolt;
        private Label label6;
        private Button btnStopIT;
        private Button btnSaveIT;
        private RadioButton radioLSV;
        private RadioButton radioCV;
        private Label label7;
        private TextBox txtMaxCycle;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private Label label9;
        private Label label8;
        private TextBox txtDPVEndE;
        private TextBox txtDPVStartE;
        private TextBox txtDPVEpul;
        private Label label11;
        private TextBox txtDPVIncre;
        private Label label10;
        private TextBox txtDPVSampwith;
        private TextBox txtDPVPulwidth;
        private Label label13;
        private Label label12;
        private TextBox txtDPVPeriod;
        private Label label14;
        private Button btnDPVSave;
        private Button btnDPVStop;
        private Button btnDPVStart;
    }
}
