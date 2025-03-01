using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        private VideoCapture _capture;
        private Timer _frameTimer;

        public Form1()
        {
            Console.WriteLine("Form1 Constructor Called");  // Debugging message
            InitializeComponent();

            // Ensure TrackBars are not null before assigning event handlers
            if (laneThresholdBar != null) laneThresholdBar.Scroll += FilterUpdated;
            if (redThresholdBar != null) redThresholdBar.Scroll += FilterUpdated;
            if (edgeThresholdBar != null) edgeThresholdBar.Scroll += FilterUpdated;
            if (blurKernelBar != null) blurKernelBar.Scroll += FilterUpdated;

            Console.WriteLine("Event Handlers Attached");

            _capture = new VideoCapture(0);
            Console.WriteLine("Video Capture Initialized");

            _frameTimer = new Timer { Interval = 100 };
            _frameTimer.Tick += FrameTimer_Tick;
            _frameTimer.Start();
            Console.WriteLine("Frame Timer Started");

            // Ensure form appears
            this.WindowState = FormWindowState.Normal;
            this.Show();
        }

        private void FrameTimer_Tick(object sender, EventArgs e)
        {
            Mat frame = _capture.QueryFrame();
            if (frame != null && !frame.IsEmpty)
            {
                CvInvoke.Flip(frame, frame, FlipType.Horizontal);
                DisplayImage(frame, pictureBoxRaw);
                ProcessFilters(frame);
            }
        }

        private void DisplayImage(Mat image, PictureBox pictureBox)
        {
            Image<Bgr, Byte> resizedImage = image.ToImage<Bgr, Byte>()
                .Resize(pictureBox.Width, pictureBox.Height, Inter.Linear);

            pictureBox.Image?.Dispose();
            pictureBox.Image = resizedImage.ToBitmap();
        }

        private void ProcessFilters(Mat frame)
        {
            Image<Gray, Byte> laneLines = DetectLaneLines(frame);
            Image<Gray, Byte> redLine = DetectRedLine(frame);
            Image<Gray, Byte> edges = DetectEdges(frame);
            Image<Bgr, Byte> blurred = ApplyBlur(frame);

            pictureBoxLaneLines.Image?.Dispose();
            pictureBoxLaneLines.Image = laneLines.ToBitmap();

            pictureBoxRedLine.Image?.Dispose();
            pictureBoxRedLine.Image = redLine.ToBitmap();

            pictureBoxEdges.Image?.Dispose();
            pictureBoxEdges.Image = edges.ToBitmap();

            pictureBoxBlurred.Image?.Dispose();
            pictureBoxBlurred.Image = blurred.ToBitmap();

            AnalyzeLanePosition(laneLines, redLine);
        }

        private Image<Gray, Byte> DetectLaneLines(Mat frame)
        {
            laneThresholdLabel.Text = $"Lane Threshold: {laneThresholdBar.Value}";
            return frame.ToImage<Bgr, Byte>().Convert<Hsv, Byte>()
                        .InRange(new Hsv(0, 0, laneThresholdBar.Value), new Hsv(180, 50, 255));
        }

        private Image<Gray, Byte> DetectRedLine(Mat frame)
        {
            redThresholdLabel.Text = $"Red Threshold: {redThresholdBar.Value}";
            return frame.ToImage<Bgr, Byte>().Convert<Hsv, Byte>()
                        .InRange(new Hsv(0, redThresholdBar.Value, 100), new Hsv(10, 255, 255));
        }

        private Image<Gray, Byte> DetectEdges(Mat frame)
        {
            edgeThresholdLabel.Text = $"Edge Threshold: {edgeThresholdBar.Value}";
            Mat edges = new Mat();
            CvInvoke.Canny(frame, edges, edgeThresholdBar.Value, edgeThresholdBar.Value * 2);
            return edges.ToImage<Gray, Byte>();
        }

        private Image<Bgr, Byte> ApplyBlur(Mat frame)
        {
            blurKernelLabel.Text = $"Blur Kernel: {blurKernelBar.Value}";
            Mat blurred = new Mat();
            CvInvoke.GaussianBlur(frame, blurred, new Size(blurKernelBar.Value, blurKernelBar.Value), 0);
            return blurred.ToImage<Bgr, Byte>();
        }

        private void AnalyzeLanePosition(Image<Gray, Byte> laneImage, Image<Gray, Byte> redLineImage)
        {
            int width = laneImage.Width;
            int sliceWidth = width / 5;
            int[] whiteCounts = new int[5];

            for (int i = 0; i < 5; i++)
            {
                Rectangle sliceRect = new Rectangle(i * sliceWidth, 0, sliceWidth, laneImage.Height);
                Image<Gray, Byte> slice = laneImage.GetSubRect(sliceRect);

                whiteCounts[i] = CvInvoke.CountNonZero(slice);
            }

            DetermineDirection(whiteCounts, redLineImage);
        }

        private void DetermineDirection(int[] whiteCounts, Image<Gray, Byte> redLineImage)
        {
            if (CvInvoke.CountNonZero(redLineImage) > 500)
            {
                UpdateDirectionLabel("STOP");
                return;
            }

            int maxIndex = Array.IndexOf(whiteCounts, whiteCounts.Max());

            string direction;
            switch (maxIndex)
            {
                case 0:
                    direction = "Sharp Left";
                    break;
                case 1:
                    direction = "Mild Left";
                    break;
                case 2:
                    direction = "Straight";
                    break;
                case 3:
                    direction = "Mild Right";
                    break;
                case 4:
                    direction = "Sharp Right";
                    break;
                default:
                    direction = "Unknown";
                    break;
            }

            UpdateDirectionLabel(direction);
        }

        private void UpdateDirectionLabel(string direction)
        {
            this.Invoke(new Action(() =>
            {
                directionLabel.Text = $"Direction: {direction}";
            }));
        }

        private void FilterUpdated(object sender, EventArgs e) => ProcessFilters(_capture.QueryFrame());
    }
}
