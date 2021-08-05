/*
 * Created by SharpDevelop.
 * User: pcaula
 * Date: 29/09/2013
 * Time: 11:55 p.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PersonalNet
{
	/// <summary>
	/// Description of FormAbout.
	/// </summary>
	public partial class FAbout : Form
	{
		public FAbout()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
        
        void cancelButton_Click(object sender, EventArgs e)
        {
        	
        }
        
        void okButton_Click(object sender, EventArgs e)
        {
        	Close();
        }
        
        void Panel2Paint(object sender, PaintEventArgs e)
        {
        	
        }
	}
}
