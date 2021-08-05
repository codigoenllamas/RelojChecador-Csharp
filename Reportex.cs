/*
 * Created by SharpDevelop.
 * User: pcaula
 * Date: 28/06/2013
 * Time: 11:18 p.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PersonalNet
{
	/// <summary>
	/// Description of Reportex.
	/// </summary>
	public partial class Reportex : Form
	{
		public Reportex()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		public static Form IsFormAlreadyOpen1(Type FormType)
		{
		   foreach (Form OpenForm in Application.OpenForms)
		   {
		      if (OpenForm.GetType() == FormType)
		         return OpenForm;
		   }
		
		   return null;
		}
		
		
		
		
		void Button1Click(object sender, EventArgs e)
		{
			//Application.Exit();

			ReporteTodos UForm = null;
			if ((UForm = (ReporteTodos)IsFormAlreadyOpen1(typeof(ReporteTodos))) == null)
				{
				
				   UForm = new ReporteTodos();
				   UForm.Show();
				
				}
				
				else
				
				{
					this.Close();
				    UForm.Select();
				    UForm.Show();
				
				}
		}
	}
}
