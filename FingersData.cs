using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalNet
{
	public delegate void OnChangeHandler();	
	
	public class FingersData
    {
        public event OnChangeHandler DataChanged; 		
        public const int MaxFingers = 10;

        public DPFP.Template[] Templates = new DPFP.Template[MaxFingers];
        public DPFP.Sample[] Samples = new DPFP.Sample[MaxFingers];
	}
}
