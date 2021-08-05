/*
 * Created by SharpDevelop.
 * User: pcaula
 * Date: 26/11/2015 - INCLUYE BUSQUEDA EN LA BASE DE DATOS Y UBICACION DE DIRECTORIO
 * Time: 07:20 p.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Data;
using System.Text;
using System.Threading;

using DPFP;
using DPFP.Processing;
using DPFP.Capture;


namespace PersonalNet
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	
	public partial class MainForm : Form
	{
		
		
		public string UserId { get; set; }
        public string ProfileNo{get;set;}
        public FingersData FingersData { get; set; }
        public DPFP.Template[] Templates = new DPFP.Template[10];
       // private WebCamera WebCam { get; set; }
		private LoginForm _loginForm = null;
		public string centinela;
		
       
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			 InitFingerPrintSettings();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		
		
		
	
		
		
		
		
		
		void btnReporteClick(object sender, EventArgs e)
		{
			
		}
		  
		private void InitFingerPrintSettings()
		{
            this.FingersData = new FingersData();	
            this.FingersData.DataChanged += new OnChangeHandler(OnFingersDataChange);  
       }
		
	 private void OnFingersDataChange() {
            int _registeredFingers = CountRegisteredFingers(this.FingersData);
            if (_registeredFingers > 0 && string.IsNullOrEmpty(this.ProfileNo)==false ) btnUploadFG.Enabled = true; else btnUploadFG.Enabled = false;
        

        }

        private int CountRegisteredFingers(FingersData data) {
            int _result=0;
            foreach(DPFP.Template item in data.Templates)
            {
                if (item != null) _result += 1;
            }
            return _result;
        }	
		
		
		
		
		void BtnUploadFGClick(object sender, EventArgs e)
		{
	    
        }
		
		
		void BtnVerifyFPClick(object sender, EventArgs e)
		{
			if( checkserial())
			{
			frmVerification _frmVerify = new frmVerification();
            _frmVerify.Data = this.FingersData;
            _frmVerify.ShowDialog();
			}
			else
			{
				FAbout Fb=new FAbout();
			}
		}
		
		
		
		
		
		
 void   validaruser()
			
		{
		
		   LoginForm _FrmLogin = new LoginForm();
			_FrmLogin.ShowDialog();
			centinela=_FrmLogin.lblBandera.Text;
			
			
		}
		
		
 private bool checkserial( )
 { // CANDADO USAR LECTOR AUTORIZADO
 	try
     {
 	
 	string idserial1="{C7EA0C13-4D9E-3C4F-B0DF-44BF0BA1C6B5}"; // Para uso de lector de prueba
 	string idserial2="{5BE2F7CE-C4E6-9542-8D7E-BB9923E29BA1}"; // Cliente Nuevo
 	DPFP.Capture.ReadersCollection rc = new DPFP.Capture.ReadersCollection();
	DPFP.Capture.ReaderDescription desc = rc[0];
	string serialnumber = desc.SerialNumber;
	if (serialnumber==idserial1 || serialnumber==idserial2 )
		{
		 return true;
	     }
 	}
	  catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                       
                    }
	
	 return false;
	
 }
		
		
		
		
		
		
		
		void MainFormLoad(object sender, EventArgs e)
		{
			System.Threading.Thread.Sleep(5700);
		}
		
		void MainFormFormClosed(object sender, FormClosedEventArgs e)
		{
		
		}
		
		void Cerrar_sessionClick(object sender, EventArgs e)
		{
			 //timer1.Stop();
             //this.logOut= true;
             this.Close();
		}
		
		public void ejecutarhuella()
		{
		
		     frmVerification _frmVerify = new frmVerification();
            _frmVerify.Data = this.FingersData;
            _frmVerify.ShowDialog();
		}
		
		void Panel1Paint(object sender, PaintEventArgs e)
		{
			
		}
		
		void Btn_EmpleadoClick(object sender, EventArgs e)
		{
			
			if( checkserial())
			{
			 centinela="0";
			 validaruser();
			 if(centinela=="1")
				{
			    FormDatos _frmdatos = new FormDatos();
	            _frmdatos.ShowDialog();
				}
			}
			else
			{
				FAbout Fb=new FAbout();
			}
            
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
		
		void Button3Click(object sender, EventArgs e)
		{
			Horarios _hEmpleado =new Horarios();
			 _hEmpleado.ShowDialog();
		}
		
		void BtnConfigurarClick(object sender, EventArgs e)
		{
			FormAbout _formabout =new FormAbout();
			 _formabout.ShowDialog();
		}
		
		void BtnMovimientoClick(object sender, EventArgs e)
		{
			centinela="0";
			validaruser();
			if(centinela=="1")
			{
			MoviTiempo _frmmovi = new MoviTiempo();
            _frmmovi.ShowDialog();
			}
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			if( checkserial())
			{
				centinela="0";
				validaruser();
				if(centinela=="1")
				{
			    	
				Kapture _kapture = new Kapture();
				 _kapture.ShowDialog();
				}
			}
			else
			{
				FAbout Fb=new FAbout();
			}
			
		}
		
		
		void BtnReportexClick(object sender, EventArgs e)
		{
			centinela="0";
			validaruser();
			if(centinela=="1")
			{
		    
			ReporteTodos _RptTodos=new ReporteTodos();
			             _RptTodos.ShowDialog();
			}
			
		}
		
		void ReporteTodoClick(object sender, EventArgs e)
		{
			
		}
	}
}
