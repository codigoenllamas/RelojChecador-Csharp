/*
 * Created by SharpDevelop.
 * User: pcaula
 * Date: 24/08/2013
 * Time: 07:20 p.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;

namespace PersonalNet
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		
		public FingersData FingersData { get; set; } 
		private static frmVerification _frmverificar = null;
			
       
        [STAThread]  
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			
			
			Application.Run(new MainForm());
			//}
			 
		   
		} 
		
	
        
    
		
	
	
	 
	}
}
