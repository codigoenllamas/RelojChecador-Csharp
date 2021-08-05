using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonalNet
{
    public class FingerCapture:DPFP.Capture.EventHandler
    {
        #region EventHandler Members:

        public void OnComplete(object Capture, string ReaderSerialNumber, DPFP.Sample Sample)
        {
           // MakeReport("The fingerprint sample was captured.");
           // SetPrompt("Scan the same fingerprint again.");
          //  Process(Sample);
        }

        public void OnFingerGone(object Capture, string ReaderSerialNumber)
        {
           // MakeReport("The finger was removed from the fingerprint reader.");
        }

        public void OnFingerTouch(object Capture, string ReaderSerialNumber)
        {
            //MakeReport("The fingerprint reader was touched.");
        }

        public void OnReaderConnect(object Capture, string ReaderSerialNumber)
        {
           // MakeReport("The fingerprint reader was connected.");
        }

        public void OnReaderDisconnect(object Capture, string ReaderSerialNumber)
        {
           // MakeReport("The fingerprint reader was disconnected.");
        }

        public void OnSampleQuality(object Capture, string ReaderSerialNumber, DPFP.Capture.CaptureFeedback CaptureFeedback)
        {
           // if (CaptureFeedback == DPFP.Capture.CaptureFeedback.Good)
           //     MakeReport("The quality of the fingerprint sample is good.");
          //  else
           //     MakeReport("The quality of the fingerprint sample is poor.");
        }
        #endregion
    }
}
