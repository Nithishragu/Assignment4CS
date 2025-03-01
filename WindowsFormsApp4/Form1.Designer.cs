namespace WindowsFormsApp4
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.PictureBox pictureBoxRaw;
        private System.Windows.Forms.PictureBox pictureBoxLaneLines;
        private System.Windows.Forms.PictureBox pictureBoxRedLine;
        private System.Windows.Forms.PictureBox pictureBoxEdges;
        private System.Windows.Forms.PictureBox pictureBoxBlurred;
        private System.Windows.Forms.Label directionLabel;

        private System.Windows.Forms.TrackBar laneThresholdBar;
        private System.Windows.Forms.TrackBar redThresholdBar;
        private System.Windows.Forms.TrackBar edgeThresholdBar;
        private System.Windows.Forms.TrackBar blurKernelBar;

        private System.Windows.Forms.Label laneThresholdLabel;
        private System.Windows.Forms.Label redThresholdLabel;
        private System.Windows.Forms.Label edgeThresholdLabel;
        private System.Windows.Forms.Label blurKernelLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pictureBoxRaw = new System.Windows.Forms.PictureBox();
            this.pictureBoxLaneLines = new System.Windows.Forms.PictureBox();
            this.pictureBoxRedLine = new System.Windows.Forms.PictureBox();
            this.pictureBoxEdges = new System.Windows.Forms.PictureBox();
            this.pictureBoxBlurred = new System.Windows.Forms.PictureBox();
            this.directionLabel = new System.Windows.Forms.Label();

            this.laneThresholdBar = new System.Windows.Forms.TrackBar();
            this.redThresholdBar = new System.Windows.Forms.TrackBar();
            this.edgeThresholdBar = new System.Windows.Forms.TrackBar();
            this.blurKernelBar = new System.Windows.Forms.TrackBar();

            this.laneThresholdLabel = new System.Windows.Forms.Label();
            this.redThresholdLabel = new System.Windows.Forms.Label();
            this.edgeThresholdLabel = new System.Windows.Forms.Label();
            this.blurKernelLabel = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRaw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLaneLines)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRedLine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEdges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBlurred)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.laneThresholdBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.redThresholdBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edgeThresholdBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blurKernelBar)).BeginInit();
            this.SuspendLayout();

            // PictureBoxes
            this.pictureBoxRaw.Location = new System.Drawing.Point(20, 20);
            this.pictureBoxRaw.Size = new System.Drawing.Size(300, 200);
            this.pictureBoxRaw.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;

            this.pictureBoxLaneLines.Location = new System.Drawing.Point(340, 20);
            this.pictureBoxLaneLines.Size = new System.Drawing.Size(300, 200);
            this.pictureBoxLaneLines.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;

            this.pictureBoxRedLine.Location = new System.Drawing.Point(660, 20);
            this.pictureBoxRedLine.Size = new System.Drawing.Size(300, 200);
            this.pictureBoxRedLine.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;

            this.pictureBoxEdges.Location = new System.Drawing.Point(20, 250);
            this.pictureBoxEdges.Size = new System.Drawing.Size(300, 200);
            this.pictureBoxEdges.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;

            this.pictureBoxBlurred.Location = new System.Drawing.Point(340, 250);
            this.pictureBoxBlurred.Size = new System.Drawing.Size(300, 200);
            this.pictureBoxBlurred.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;

            // Labels
            this.directionLabel.Location = new System.Drawing.Point(660, 260);
            this.directionLabel.Size = new System.Drawing.Size(200, 30);
            this.directionLabel.Text = "Direction: Unknown";

            // TrackBars for filter adjustments
            this.laneThresholdBar.Location = new System.Drawing.Point(20, 500);
            this.laneThresholdBar.Minimum = 50;
            this.laneThresholdBar.Maximum = 255;
            this.laneThresholdBar.Value = 200;
            this.laneThresholdBar.TickFrequency = 10;

            this.redThresholdBar.Location = new System.Drawing.Point(340, 500);
            this.redThresholdBar.Minimum = 50;
            this.redThresholdBar.Maximum = 255;
            this.redThresholdBar.Value = 100;
            this.redThresholdBar.TickFrequency = 10;

            this.edgeThresholdBar.Location = new System.Drawing.Point(660, 500);
            this.edgeThresholdBar.Minimum = 50;
            this.edgeThresholdBar.Maximum = 255;
            this.edgeThresholdBar.Value = 100;
            this.edgeThresholdBar.TickFrequency = 10;

            this.blurKernelBar.Location = new System.Drawing.Point(20, 550);
            this.blurKernelBar.Minimum = 1;
            this.blurKernelBar.Maximum = 15;
            this.blurKernelBar.Value = 9;
            this.blurKernelBar.SmallChange = 2;
            this.blurKernelBar.LargeChange = 2;
            this.blurKernelBar.TickFrequency = 2;

            // Labels for TrackBars
            this.laneThresholdLabel.Location = new System.Drawing.Point(20, 530);
            this.laneThresholdLabel.Size = new System.Drawing.Size(200, 20);
            this.laneThresholdLabel.Text = "Lane Threshold: 200";

            this.redThresholdLabel.Location = new System.Drawing.Point(340, 530);
            this.redThresholdLabel.Size = new System.Drawing.Size(200, 20);
            this.redThresholdLabel.Text = "Red Threshold: 100";

            this.edgeThresholdLabel.Location = new System.Drawing.Point(660, 530);
            this.edgeThresholdLabel.Size = new System.Drawing.Size(200, 20);
            this.edgeThresholdLabel.Text = "Edge Threshold: 100";

            this.blurKernelLabel.Location = new System.Drawing.Point(20, 580);
            this.blurKernelLabel.Size = new System.Drawing.Size(200, 20);
            this.blurKernelLabel.Text = "Blur Kernel: 9";

            // Adding Controls to Form
            this.Controls.Add(this.pictureBoxRaw);
            this.Controls.Add(this.pictureBoxLaneLines);
            this.Controls.Add(this.pictureBoxRedLine);
            this.Controls.Add(this.pictureBoxEdges);
            this.Controls.Add(this.pictureBoxBlurred);
            this.Controls.Add(this.directionLabel);

            this.Controls.Add(this.laneThresholdBar);
            this.Controls.Add(this.redThresholdBar);
            this.Controls.Add(this.edgeThresholdBar);
            this.Controls.Add(this.blurKernelBar);

            this.Controls.Add(this.laneThresholdLabel);
            this.Controls.Add(this.redThresholdLabel);
            this.Controls.Add(this.edgeThresholdLabel);
            this.Controls.Add(this.blurKernelLabel);

            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRaw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLaneLines)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRedLine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEdges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBlurred)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.laneThresholdBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.redThresholdBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edgeThresholdBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blurKernelBar)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
