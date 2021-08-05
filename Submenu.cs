/*
 * Created by SharpDevelop.
 * User: AccesoReloj
 * Date: 05/01/2016
 * Time: 03:19 p.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PersonalNet
{
	/// <summary>
	/// Description of Submenu.
	/// </summary>
	public partial class Submenu : Form
	{
		public Submenu()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		public static Form IsFormAlreadyOpen2(Type FormType)
		{
		   foreach (Form OpenForm in Application.OpenForms)
		   {
		      if (OpenForm.GetType() == FormType)
		         return OpenForm;
		   }
		
		   return null;
		}			
		
		
		
		
		void BtnMovHrClick(object sender, EventArgs e)
		{
			 this.Hide(); 
				  	            
				  	           MoviForm frmOpen = null;
											if ((frmOpen = (MoviForm)IsFormAlreadyOpen2(typeof(MoviForm))) == null)
												{
									 			frmOpen = new MoviForm();
									 			frmOpen.Show();
												
												}
		}
	}
}
