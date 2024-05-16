namespace AirScanEmulator
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAddAirScan = new System.Windows.Forms.Button();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.btnStartCalibration = new System.Windows.Forms.Button();
            this.btnClearAllAirScans = new System.Windows.Forms.Button();
            this.speed = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.handSize = new System.Windows.Forms.TrackBar();
            this.btnAddHand = new System.Windows.Forms.Button();
            this.btnClearAllHands = new System.Windows.Forms.Button();
            this.chkEdge = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.interval = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.resolution = new System.Windows.Forms.TrackBar();
            this.chkPaint = new System.Windows.Forms.CheckBox();
            this.chkML = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.optNoPoints = new System.Windows.Forms.RadioButton();
            this.optPoint = new System.Windows.Forms.RadioButton();
            this.optLine = new System.Windows.Forms.RadioButton();
            this.chkHand = new System.Windows.Forms.CheckBox();
            this.chkCentroids = new System.Windows.Forms.CheckBox();
            this.chkTouchPoints = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.epsilon = new System.Windows.Forms.TrackBar();
            this.label6 = new System.Windows.Forms.Label();
            this.minPts = new System.Windows.Forms.TrackBar();
            this.label7 = new System.Windows.Forms.Label();
            this.rotate = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.speed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.handSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.interval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resolution)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epsilon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minPts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rotate)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddAirScan
            // 
            this.btnAddAirScan.Location = new System.Drawing.Point(12, 12);
            this.btnAddAirScan.Name = "btnAddAirScan";
            this.btnAddAirScan.Size = new System.Drawing.Size(114, 23);
            this.btnAddAirScan.TabIndex = 0;
            this.btnAddAirScan.Text = "Add AirScan";
            this.btnAddAirScan.UseVisualStyleBackColor = true;
            this.btnAddAirScan.Click += new System.EventHandler(this.btnAddAirScan_Click);
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.White;
            this.mainPanel.Location = new System.Drawing.Point(132, 12);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(656, 426);
            this.mainPanel.TabIndex = 1;
            this.mainPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.mainPanel_Paint);
            // 
            // btnStartCalibration
            // 
            this.btnStartCalibration.Location = new System.Drawing.Point(12, 415);
            this.btnStartCalibration.Name = "btnStartCalibration";
            this.btnStartCalibration.Size = new System.Drawing.Size(114, 23);
            this.btnStartCalibration.TabIndex = 2;
            this.btnStartCalibration.Text = "Star Calibration";
            this.btnStartCalibration.UseVisualStyleBackColor = true;
            this.btnStartCalibration.Click += new System.EventHandler(this.btnStartCalibration_Click);
            // 
            // btnClearAllAirScans
            // 
            this.btnClearAllAirScans.Location = new System.Drawing.Point(12, 41);
            this.btnClearAllAirScans.Name = "btnClearAllAirScans";
            this.btnClearAllAirScans.Size = new System.Drawing.Size(114, 23);
            this.btnClearAllAirScans.TabIndex = 3;
            this.btnClearAllAirScans.Text = "Clear All AriScans";
            this.btnClearAllAirScans.UseVisualStyleBackColor = true;
            this.btnClearAllAirScans.Click += new System.EventHandler(this.btnClearAllAirScans_Click);
            // 
            // speed
            // 
            this.speed.Location = new System.Drawing.Point(13, 364);
            this.speed.Minimum = 1;
            this.speed.Name = "speed";
            this.speed.Size = new System.Drawing.Size(104, 45);
            this.speed.TabIndex = 4;
            this.speed.Value = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 352);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Speed";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 295);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Hand Size";
            // 
            // handSize
            // 
            this.handSize.Location = new System.Drawing.Point(13, 307);
            this.handSize.Minimum = 1;
            this.handSize.Name = "handSize";
            this.handSize.Size = new System.Drawing.Size(104, 45);
            this.handSize.TabIndex = 6;
            this.handSize.Value = 1;
            // 
            // btnAddHand
            // 
            this.btnAddHand.Location = new System.Drawing.Point(12, 158);
            this.btnAddHand.Name = "btnAddHand";
            this.btnAddHand.Size = new System.Drawing.Size(114, 23);
            this.btnAddHand.TabIndex = 8;
            this.btnAddHand.Text = "Add Hand";
            this.btnAddHand.UseVisualStyleBackColor = true;
            this.btnAddHand.Click += new System.EventHandler(this.btnAddHand_Click);
            // 
            // btnClearAllHands
            // 
            this.btnClearAllHands.Location = new System.Drawing.Point(12, 186);
            this.btnClearAllHands.Name = "btnClearAllHands";
            this.btnClearAllHands.Size = new System.Drawing.Size(114, 23);
            this.btnClearAllHands.TabIndex = 9;
            this.btnClearAllHands.Text = "Clear All Hands";
            this.btnClearAllHands.UseVisualStyleBackColor = true;
            this.btnClearAllHands.Click += new System.EventHandler(this.btnClearAllHands_Click);
            // 
            // chkEdge
            // 
            this.chkEdge.AutoSize = true;
            this.chkEdge.Location = new System.Drawing.Point(12, 215);
            this.chkEdge.Name = "chkEdge";
            this.chkEdge.Size = new System.Drawing.Size(51, 17);
            this.chkEdge.TabIndex = 11;
            this.chkEdge.Text = "Edge";
            this.chkEdge.UseVisualStyleBackColor = true;
            this.chkEdge.CheckedChanged += new System.EventHandler(this.chkEdge_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 236);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Interval";
            // 
            // interval
            // 
            this.interval.Location = new System.Drawing.Point(13, 250);
            this.interval.Maximum = 1000;
            this.interval.Minimum = 10;
            this.interval.Name = "interval";
            this.interval.Size = new System.Drawing.Size(104, 45);
            this.interval.TabIndex = 12;
            this.interval.Value = 300;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Resolution";
            // 
            // resolution
            // 
            this.resolution.Location = new System.Drawing.Point(15, 88);
            this.resolution.Maximum = 10000;
            this.resolution.Minimum = 100;
            this.resolution.Name = "resolution";
            this.resolution.Size = new System.Drawing.Size(104, 45);
            this.resolution.TabIndex = 14;
            this.resolution.Value = 360;
            this.resolution.Scroll += new System.EventHandler(this.resolution_Scroll);
            // 
            // chkPaint
            // 
            this.chkPaint.AutoSize = true;
            this.chkPaint.Checked = true;
            this.chkPaint.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPaint.Location = new System.Drawing.Point(68, 215);
            this.chkPaint.Name = "chkPaint";
            this.chkPaint.Size = new System.Drawing.Size(50, 17);
            this.chkPaint.TabIndex = 16;
            this.chkPaint.Text = "Paint";
            this.chkPaint.UseVisualStyleBackColor = true;
            // 
            // chkML
            // 
            this.chkML.AutoSize = true;
            this.chkML.Location = new System.Drawing.Point(13, 444);
            this.chkML.Name = "chkML";
            this.chkML.Size = new System.Drawing.Size(41, 17);
            this.chkML.TabIndex = 17;
            this.chkML.Text = "ML";
            this.chkML.UseVisualStyleBackColor = true;
            this.chkML.CheckedChanged += new System.EventHandler(this.chkML_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.optNoPoints);
            this.panel1.Controls.Add(this.optPoint);
            this.panel1.Controls.Add(this.optLine);
            this.panel1.Location = new System.Drawing.Point(0, 127);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(151, 29);
            this.panel1.TabIndex = 18;
            // 
            // optNoPoints
            // 
            this.optNoPoints.AutoSize = true;
            this.optNoPoints.Location = new System.Drawing.Point(87, 8);
            this.optNoPoints.Name = "optNoPoints";
            this.optNoPoints.Size = new System.Drawing.Size(51, 17);
            this.optNoPoints.TabIndex = 2;
            this.optNoPoints.TabStop = true;
            this.optNoPoints.Text = "None";
            this.optNoPoints.UseVisualStyleBackColor = true;
            // 
            // optPoint
            // 
            this.optPoint.AutoSize = true;
            this.optPoint.Location = new System.Drawing.Point(43, 8);
            this.optPoint.Name = "optPoint";
            this.optPoint.Size = new System.Drawing.Size(49, 17);
            this.optPoint.TabIndex = 1;
            this.optPoint.TabStop = true;
            this.optPoint.Text = "Point";
            this.optPoint.UseVisualStyleBackColor = true;
            // 
            // optLine
            // 
            this.optLine.AutoSize = true;
            this.optLine.Checked = true;
            this.optLine.Location = new System.Drawing.Point(3, 9);
            this.optLine.Name = "optLine";
            this.optLine.Size = new System.Drawing.Size(45, 17);
            this.optLine.TabIndex = 0;
            this.optLine.TabStop = true;
            this.optLine.Text = "Line";
            this.optLine.UseVisualStyleBackColor = true;
            // 
            // chkHand
            // 
            this.chkHand.AutoSize = true;
            this.chkHand.Checked = true;
            this.chkHand.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHand.Location = new System.Drawing.Point(60, 444);
            this.chkHand.Name = "chkHand";
            this.chkHand.Size = new System.Drawing.Size(52, 17);
            this.chkHand.TabIndex = 19;
            this.chkHand.Text = "Hand";
            this.chkHand.UseVisualStyleBackColor = true;
            this.chkHand.CheckedChanged += new System.EventHandler(this.chkHand_CheckedChanged);
            // 
            // chkCentroids
            // 
            this.chkCentroids.AutoSize = true;
            this.chkCentroids.Checked = true;
            this.chkCentroids.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCentroids.Location = new System.Drawing.Point(118, 444);
            this.chkCentroids.Name = "chkCentroids";
            this.chkCentroids.Size = new System.Drawing.Size(70, 17);
            this.chkCentroids.TabIndex = 20;
            this.chkCentroids.Text = "Centroids";
            this.chkCentroids.UseVisualStyleBackColor = true;
            // 
            // chkTouchPoints
            // 
            this.chkTouchPoints.AutoSize = true;
            this.chkTouchPoints.Checked = true;
            this.chkTouchPoints.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTouchPoints.Location = new System.Drawing.Point(194, 444);
            this.chkTouchPoints.Name = "chkTouchPoints";
            this.chkTouchPoints.Size = new System.Drawing.Size(89, 17);
            this.chkTouchPoints.TabIndex = 21;
            this.chkTouchPoints.Text = "Touch Points";
            this.chkTouchPoints.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(289, 441);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Epsilon";
            // 
            // epsilon
            // 
            this.epsilon.Location = new System.Drawing.Point(290, 453);
            this.epsilon.Maximum = 100;
            this.epsilon.Minimum = 5;
            this.epsilon.Name = "epsilon";
            this.epsilon.Size = new System.Drawing.Size(104, 45);
            this.epsilon.TabIndex = 22;
            this.epsilon.Value = 15;
            this.epsilon.Scroll += new System.EventHandler(this.epsilon_Scroll);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(400, 441);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "MinPts";
            // 
            // minPts
            // 
            this.minPts.Location = new System.Drawing.Point(401, 453);
            this.minPts.Maximum = 30;
            this.minPts.Minimum = 1;
            this.minPts.Name = "minPts";
            this.minPts.Size = new System.Drawing.Size(104, 45);
            this.minPts.TabIndex = 24;
            this.minPts.Value = 3;
            this.minPts.Scroll += new System.EventHandler(this.minPts_Scroll);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(683, 441);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "Rotate";
            // 
            // rotate
            // 
            this.rotate.Location = new System.Drawing.Point(684, 453);
            this.rotate.Maximum = 360;
            this.rotate.Name = "rotate";
            this.rotate.Size = new System.Drawing.Size(104, 45);
            this.rotate.TabIndex = 26;
            this.rotate.Scroll += new System.EventHandler(this.rotate_Scroll);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 489);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.rotate);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.minPts);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.epsilon);
            this.Controls.Add(this.chkTouchPoints);
            this.Controls.Add(this.chkCentroids);
            this.Controls.Add(this.chkHand);
            this.Controls.Add(this.chkML);
            this.Controls.Add(this.chkPaint);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.resolution);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.interval);
            this.Controls.Add(this.chkEdge);
            this.Controls.Add(this.btnClearAllHands);
            this.Controls.Add(this.btnAddHand);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.handSize);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.speed);
            this.Controls.Add(this.btnClearAllAirScans);
            this.Controls.Add(this.btnStartCalibration);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.btnAddAirScan);
            this.Controls.Add(this.panel1);
            this.Name = "frmMain";
            this.Text = "AirScan Emulator";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.speed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.handSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.interval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resolution)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epsilon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minPts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rotate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddAirScan;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Button btnStartCalibration;
        private System.Windows.Forms.Button btnClearAllAirScans;
        private System.Windows.Forms.TrackBar speed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar handSize;
        private System.Windows.Forms.Button btnAddHand;
        private System.Windows.Forms.Button btnClearAllHands;
        private System.Windows.Forms.CheckBox chkEdge;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar interval;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar resolution;
        private System.Windows.Forms.CheckBox chkPaint;
        private System.Windows.Forms.CheckBox chkML;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton optPoint;
        private System.Windows.Forms.RadioButton optLine;
        private System.Windows.Forms.CheckBox chkHand;
        private System.Windows.Forms.RadioButton optNoPoints;
        private System.Windows.Forms.CheckBox chkCentroids;
        private System.Windows.Forms.CheckBox chkTouchPoints;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar epsilon;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TrackBar minPts;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TrackBar rotate;
    }
}

