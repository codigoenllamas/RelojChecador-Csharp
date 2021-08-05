/*
 * Created by SharpDevelop.
 * User: pcaula
 * Date: 28/06/2013
 * Time: 11:17 p.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using System.IO;

using DPFP;
using DPFP.Processing;
using DPFP.Capture;


namespace PersonalNet
{
	/// <summary>
	/// Description of Kapture.
	/// </summary>
	public partial class Kapture : Form
	{
		
		
		public string UserId { get; set; }
        public string ProfileNo{get;set;}
        public FingersData FingersData { get; set; }
        public DPFP.Template[] Templates = new DPFP.Template[10];
       // private WebCamera WebCam { get; set; }
		private LoginForm _loginForm = null;
		public DataTable dt=null; 
		private int idEmple = 0;
// CODE PERSONALIZADO PARA GRABAR






// TERMINA GRABACION


		
		public Kapture()
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
		
		private void InitFingerPrintSettings()
		{
            this.FingersData = new FingersData();	
            this.FingersData.DataChanged += new OnChangeHandler(OnFingersDataChange);  
       }
		
	 private void OnFingersDataChange() {
            int _registeredFingers = CountRegisteredFingers(this.FingersData);
            if (_registeredFingers > 0 && string.IsNullOrEmpty(this.ProfileNo)==false ) btn_guardarhuella.Enabled = true; else btn_guardarhuella.Enabled = false;
        

        }

        private int CountRegisteredFingers(FingersData data) {
            int _result=0;
            foreach(DPFP.Template item in data.Templates)
            {
                if (item != null) _result += 1;
            }
            return _result;
        }	
		
		
		private void ClearControl()
		{
		   
		   cmbEmpleado.SelectedIndex=0;
		   btn_guardarhuella.Enabled = false;
		   Btn_RegHuella.Enabled = false;
			
		}
		
				
	public class enlacedb
	{
		public string    connectionString =
				  	"User=SYSDBA;" +
					"Password=masterkey;" +
				    "Database=C://Reloj2021//DBCHECKNET.FDB;" +"DataSource=localhost;" +
					"Port=3050;" +
					"Dialect=3;" +
					"Charset=WIN1252;" +
					"Role=;" +
				    "Connection lifetime=15;" +
				    "Pooling=true;" +
					"MinPoolSize=0;" +
				 	"MaxPoolSize=50;" +
					"Packet Size=8192";
							
				}	
		
		
		
		
		
		private DPFP.Template Template;
		private DPFP.Template myTemplate;
		
		void Button8Click(object sender, EventArgs e)
		{
			
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			
			
			
		}
		
		

		void LoadButtonClick(object sender, EventArgs e)
		{
			OpenFileDialog open = new OpenFileDialog();
			open.Filter = "Fingerprint Template File (*.fpt)|*.fpt";
			if (open.ShowDialog() == DialogResult.OK) {
				using (FileStream fs = File.OpenRead(open.FileName)) {
					DPFP.Template template = new DPFP.Template(fs);
					OnTemplate(template);
				}
			}	
		}
		
			private void OnTemplate(DPFP.Template template)
		{
			this.Invoke(new Function(delegate()
			{
				Template = template;
				//VerifyButton.Enabled = SaveButton.Enabled = (Template != null);
				if (Template != null)
					MessageBox.Show("The fingerprint template is ready for fingerprint verification.", "Fingerprint Enrollment");
				else
					MessageBox.Show("The fingerprint template is not valid. Repeat fingerprint enrollment.", "Fingerprint Enrollment");
			}));
		}
		
		
		
		void Btn_RegHuellaClick(object sender, EventArgs e)
		{
			   frmScanFinger scanf = new frmScanFinger(sender,this.FingersData, 9);
               scanf.ShowDialog();
                        
               
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			this.Close();
		}
		
		void Btn_limpiarClick(object sender, EventArgs e)
		{
			
			ClearControl();
			 
		}
		
		void KaptureLoad(object sender, EventArgs e)
		{
			try {
		enlacedb db = new enlacedb();
		FbConnection conexion = new FbConnection(db.connectionString);
			
				//string cmatricula
				conexion.Open();
				dt = new DataTable("EMP_NOM");
				// Load Data into DataGrid
				FbDataAdapter da=new FbDataAdapter("SELECT TBEMPLEADO.IDEMPLEADO, TBEMPLEADO.EMP_NOM FROM TBEMPLEADO WHERE TBEMPLEADO.ACTIVO <> 'NO' ORDER BY IDEMPLEADO",conexion);
				da.Fill(dt);
				foreach (DataRow drw in dt.Rows) {
					cmbEmpleado.Items.Add(drw["IDEMPLEADO"].ToString()+" | "+(drw["EMP_NOM"].ToString()).ToString());
				}
				
			
			}
			catch (Exception err) {
						MessageBox.Show(err.ToString());
					}
			cmbEmpleado.SelectedIndex=0;
			ClearControl();
		}
		
		void CmbEmpleadoSelectedIndexChanged(object sender, EventArgs e)
		{
				string[] wmod = cmbEmpleado.Text.Split(new char[] { '|'});
				//lbl_modulo.Text= wmod[1];
				idEmple = int.Parse(wmod[0].ToString());
				Global_ip.Globalip= wmod[0].ToString();
				Btn_RegHuella.Enabled= true;
				
		}
	}
}
