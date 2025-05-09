using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;

namespace YellowLineTracker
{
    public partial class Form1 : Form
    {
        private VideoCapture _capture;
        private bool _isRunning = false;
        private SerialPort _serialPort;

        public Form1()
        {
            InitializeComponent();
            _serialPort = new SerialPort("COM9", 9600);
            try { _serialPort.Open(); }
            catch (Exception ex) { MessageBox.Show("Serial Port Error: " + ex.Message); }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            labelStatus.Text = "Ready";
        }

        private void StartCapture()
        {
            _capture = new VideoCapture(0);
            _capture.ImageGrabbed += ProcessFrame;
            _isRunning = true;
            _capture.Start();
        }

        private void StopCapture()
        {
            _isRunning = false;
            if (_capture != null)
            {
                _capture.ImageGrabbed -= ProcessFrame;
                _capture.Dispose();
            }
        }

        private void ProcessFrame(object sender, EventArgs e)
        {
            if (!_isRunning || _capture == null) return;

            Mat frame = new Mat();
            _capture.Retrieve(frame);
            var image = frame.ToImage<Bgr, byte>();












































            var hsv = image.Convert<Hsv, byte>();

            // Improved yellow detection range
            var yellowMask = hsv.InRange(new Hsv(15, 70, 70), new Hsv(40, 255, 255));

            // Draw contours around yellow regions
            var contours = new VectorOfVectorOfPoint();
            CvInvoke.FindContours(yellowMask.Clone(), contours, null, RetrType.External, ChainApproxMethod.ChainApproxSimple);
            for (int i = 0; i < contours.Size; i++)
            {
                CvInvoke.DrawContours(image, contours, i, new MCvScalar(0, 255, 255), 2);
            }

            // White and red detection
            var whiteMask = hsv.InRange(new Hsv(0, 0, 200), new Hsv(180, 30, 255));
            var redMask = hsv.InRange(new Hsv(0, 100, 100), new Hsv(10, 255, 255))
                            .Or(hsv.InRange(new Hsv(160, 100, 100), new Hsv(180, 255, 255)));

            Rectangle redZone = new Rectangle(0, redMask.Height - 60, redMask.Width, 40);
            var redBottom = redMask.GetSubRect(redZone);

            var moments = yellowMask.GetMoments(true);
            int centerX = (int)(moments.M10 / (moments.M00 + 1e-5));
            int width = yellowMask.Width;

            var leftBoundary = whiteMask.GetSubRect(new Rectangle(0, whiteMask.Height / 2, whiteMask.Width / 5, whiteMask.Height / 2));
            var rightBoundary = whiteMask.GetSubRect(new Rectangle(whiteMask.Width * 4 / 5, whiteMask.Height / 2, whiteMask.Width / 5, whiteMask.Height / 2));
            int whiteLeft = CvInvoke.CountNonZero(leftBoundary);
            int whiteRight = CvInvoke.CountNonZero(rightBoundary);

            string direction;

            // === CONTROL DECISION ===
            if (CvInvoke.CountNonZero(redBottom) > 500)
            {
                direction = "STOP - RED LINE";
                SendCommand('x');
            }
            else if (moments.M00 < 1e-5)
            {
                direction = "YELLOW NOT FOUND - STOP";
                SendCommand('x');
            }
            else
            {
                // White boundary recovery
                if (whiteLeft < 200 && centerX > width / 2)
                {
                    direction = "OUT LEFT - TURN RIGHT TO RETURN";
                    SendCommand('R');
                }
                else if (whiteRight < 200 && centerX < width / 2)
                {
                    direction = "OUT RIGHT - TURN LEFT TO RETURN";
                    SendCommand('L');
                }
                else
                {
                    // Yellow line based motion
                    if (centerX < width / 5)
                    {
                        direction = "SHARP LEFT";
                        SendCommand('A');
                    }
                    else if (centerX < 2 * width / 5)
                    {
                        direction = "LEFT";
                        SendCommand('L');
                    }
                    else if (centerX < 3 * width / 5)
                    {
                        direction = "STRAIGHT";
                        SendCommand('s');
                    }
                    else if (centerX < 4 * width / 5)
                    {
                        direction = "RIGHT";
                        SendCommand('r');
                    }
                    else
                    {
                        direction = "SHARP RIGHT";
                        SendCommand('R');
                    }
                }
            }

            // Overlay direction
            CvInvoke.PutText(image, direction, new Point(20, 40), FontFace.HersheySimplex, 1.0, new MCvScalar(0, 255, 0), 2);

            UpdateUI(image, yellowMask, whiteMask, redMask, direction);
        }

        private void UpdateUI(Image<Bgr, byte> img, Image<Gray, byte> yellow, Image<Gray, byte> white, Image<Gray, byte> red, string status)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)(() =>
                {
                    pictureBoxCamera.Image = img.ToBitmap();
                    pictureBoxYellow.Image = yellow.ToBitmap();
                    pictureBoxWhite.Image = white.ToBitmap();
                    pictureBoxRed.Image = red.ToBitmap();
                    labelStatus.Text = status;
                }));
            }
            else
            {
                pictureBoxCamera.Image = img.ToBitmap();
                pictureBoxYellow.Image = yellow.ToBitmap();
                pictureBoxWhite.Image = white.ToBitmap();
                pictureBoxRed.Image = red.ToBitmap();
                labelStatus.Text = status;
            }
        }

        private void SendCommand(char c)
        {
            if (_serialPort.IsOpen)
            {
                try { _serialPort.Write(c.ToString()); }
                catch { }
            }
        }

        private void buttonStart_Click(object sender, EventArgs e) => StartCapture();
        private void buttonStop_Click(object sender, EventArgs e) => StopCapture();

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            StopCapture();
            if (_serialPort.IsOpen) _serialPort.Close();
            base.OnFormClosing(e);
        }
    }
}