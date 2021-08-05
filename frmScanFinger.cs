using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using DPFP;
using DPFP.Processing;
using DPFP.Capture;



namespace PersonalNet
{
    delegate void Function();
    public partial class frmScanFinger : Form ,DPFP.Capture.EventHandler
    {
        private DPFP.Processing.Enrollment Enroller;
        private DPFP.Capture.Capture Capturer;
        private FingersData Data { get; set; }
        private DPFP.Sample[]      Samples{ get; set; }
        private int FingerPosition { get; set; }
        private Control Control { get; set; }
        private bool IsComplete { get; set; }
        private bool IsGrabo { get; set; }    
        public frmScanFinger(Object sender,FingersData Data,int FingerPosition)
        {
            this.FingerPosition = FingerPosition;
            this.Data = Data;
            this.Control = (Control)sender;
            InitializeComponent();
            

        }
        protected void Process(DPFP.Sample Sample)
        {
            this.Data.Samples[this.FingerPosition] = Sample;
            DrawPicture(ConvertSampleToBitmap(Sample));
            // Process the sample and create a feature set for the enrollment purpose.
            DPFP.FeatureSet features = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Enrollment);
            // Check quality of the sample and add to enroller if it's good
            if (features != null) try
                {
                    MakeReport("The fingerprint feature set was created.");
                    Enroller.AddFeatures(features);		// Add feature set to template.
                }
                finally
                {
                    UpdateStatus();

                    // Check if template has been created.
                    switch (Enroller.TemplateStatus)
                    {
                        case DPFP.Processing.Enrollment.Status.Ready:	// report success and stop capturing
                            //OnTemplate(Enroller.Template); aqui grabar huella Click Close, and then click Fingerprint Verification.
                            this.IsGrabo=false;
                            this.Data.Templates[this.FingerPosition] = Enroller.Template;
                          
		                        System.IO.MemoryStream _MemoryStream = new System.IO.MemoryStream();
		                        Stream _stream = this.Data.Templates[this.FingerPosition].Serialize(_MemoryStream);
		                        byte[] _byte = Util.StreamToByte(_MemoryStream);
		                        this.IsGrabo=Util.UpdateDataPeople(Global_ip.Globalip,_byte);
		                     this.IsComplete = true;
                            SetPrompt("Cerrar Finalizado, Huella Digital.");
                            Stop();
                            break;

                        case DPFP.Processing.Enrollment.Status.Failed:	// report failure and restart capturing
                            Enroller.Clear();
                            Stop();
                            UpdateStatus();
                            //OnTemplate(null);
                            Start();
                            break;
                    }
                }
        }

        
        private void frmRegisterFP_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Stop();
        }
      
        private void frmRegisterFP_Load(object sender, EventArgs e)
        {
            
            Enroller = new DPFP.Processing.Enrollment();
            Init();
            Start();
        }
        protected void Init()
        {
            try
            {
                Capturer = new DPFP.Capture.Capture();				// Create a capture operation.

               if (null != Capturer)
                  Capturer.EventHandler = this;					// Subscribe for capturing events.
    
            }
            catch
            {
                MessageBox.Show("Can't initiate capture operation!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            
        }
        protected void Start()
        {
            if (null != Capturer)
            {
                try
                {
                    Capturer.StartCapture();
                    
                }
                catch
                {
                     
                }
            }
        }

        protected void Stop()
        {
            if (null != Capturer)
            {
                try
                {
                    Capturer.StopCapture();
                }
                catch
                {
                     
                }
            }
        }
      



        #region EventHandler Members:

        public void OnComplete(object Capture, string ReaderSerialNumber, DPFP.Sample Sample)
        {
             MakeReport("The fingerprint sample was captured.");
             SetPrompt("Scan the same fingerprint again.");
              Process(Sample);
        }

        public void OnFingerGone(object Capture, string ReaderSerialNumber)
        {
             MakeReport("The finger was removed from the fingerprint reader.");
        }

        public void OnFingerTouch(object Capture, string ReaderSerialNumber)
        {
             MakeReport("The fingerprint reader was touched.");
        }

        public void OnReaderConnect(object Capture, string ReaderSerialNumber)
        {
             MakeReport("The fingerprint reader was connected.");
        }

        public void OnReaderDisconnect(object Capture, string ReaderSerialNumber)
        {
             MakeReport("The fingerprint reader was disconnected.");
        }

        public void OnSampleQuality(object Capture, string ReaderSerialNumber, DPFP.Capture.CaptureFeedback CaptureFeedback)
        {
              if (CaptureFeedback == DPFP.Capture.CaptureFeedback.Good)
                  MakeReport("The quality of the fingerprint sample is good.");
               else
             MakeReport("The quality of the fingerprint sample is poor.");
        }
        #endregion

        private void CloseButton_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void CloseForm() {
            if (this.IsComplete == true)
            {
               // MessageBox.Show("aqui este el boton ");
            	this.Control.Enabled = false;
            }
            this.Close();
        }

        protected Bitmap ConvertSampleToBitmap(DPFP.Sample Sample)
        {
            DPFP.Capture.SampleConversion Convertor = new DPFP.Capture.SampleConversion();	// Create a sample convertor.
            Bitmap bitmap = null;												            // TODO: the size doesn't matter
            Convertor.ConvertToPicture(Sample, ref bitmap);									// TODO: return bitmap as a result
            return bitmap;
        }

        protected DPFP.FeatureSet ExtractFeatures(DPFP.Sample Sample, DPFP.Processing.DataPurpose Purpose)
        {
            DPFP.Processing.FeatureExtraction Extractor = new DPFP.Processing.FeatureExtraction();	// Create a feature extractor
            DPFP.Processing.Enrollment dd = new DPFP.Processing.Enrollment();

            DPFP.Capture.CaptureFeedback feedback = DPFP.Capture.CaptureFeedback.None;
            DPFP.FeatureSet features = new DPFP.FeatureSet();
            Extractor.CreateFeatureSet(Sample, Purpose, ref feedback, ref features);			// TODO: return features as a result?
            if (feedback == DPFP.Capture.CaptureFeedback.Good)
                return features;
            else
                return null;
        }
        private void UpdateStatus()
        {
            // Show number of samples needed.
            SetStatus(String.Format("Fingerprint samples needed: {0}", Enroller.FeaturesNeeded));
        }

        protected void SetStatus(string status)
        {
            this.Invoke(new Function(delegate()
            {
                StatusLine.Text = status;
            }));
        }

        protected void SetPrompt(string prompt)
        {
            this.Invoke(new Function(delegate()
            {
                Prompt.Text = prompt;
            }));
        }
        protected void MakeReport(string message)
        {
            this.Invoke(new Function(delegate()
            {
                StatusText.AppendText(message + "\r\n");
            }));
        }

        private void DrawPicture(Bitmap bitmap)
        {
            this.Invoke(new Function(delegate()
            {
                Picture.Image = new Bitmap(bitmap, Picture.Size);	// fit the image into the picture box
                      
        	        }));
        }

     
    
    }
}
