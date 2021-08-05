using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using FirebirdSql.Data.FirebirdClient;
using System.Runtime.InteropServices;  // DllImport()

namespace PersonalNet
{
    public partial class frmVerification : Form 
    {
         private DPFP.Gui.Verification.VerificationControl VerificationControl;
    	public FingersData Data { get; set; }
        public frmVerification()
        {
            InitializeComponent();

        }
        
   void Santoral()
		{
			dougScrollingTextCtrl1.Text=" Queremos Pastel ¡FELICIDADES!!!: ";
			 //limpa o controle ListBox
           // lstClientes.Items.Clear();

            //define os objetos connection e datareader
             enlacedb db = new enlacedb();
             FbDataReader readerEmpleado = null ;
            FbConnection conexion = null; 
			//EMPEZAMOS
             			
			
			conexion = new FbConnection(db.connectionString);
           
			try {
            	 DateTime FechaHoy=DateTime.Now;
            	 	int mes = FechaHoy.Month;
               conexion = new FbConnection(db.connectionString);
			   conexion.Open();
			   FbCommand cmdFecha = new FbCommand("SELECT TBEMPLEADO.EMP_NOM ,TBEMPLEADO.EMP_FECNAC, extract(DAY from cast(TBEMPLEADO.EMP_FECNAC as date)) FROM TBEMPLEADO  WHERE  extract(MONTH from cast(TBEMPLEADO.EMP_FECNAC as date))=@MesCumple AND TBEMPLEADO.ACTIVO='SI' AND  EMP_DBHUELLA IS NOT NULL ORDER BY TBEMPLEADO.EMP_FECNAC ", conexion);
	            FbParameter param = new FbParameter();
                param.ParameterName = "@MesCumple";
                param.Value = mes;
                cmdFecha.Parameters.Add(param);
			                  // obtem os dados
                readerEmpleado = cmdFecha.ExecuteReader();

                // exibe no listbox os dados para o codigo do cliente, empresa e nome do contato
                while (readerEmpleado.Read())
                {
                 dougScrollingTextCtrl1.Text=dougScrollingTextCtrl1.Text.ToString() + readerEmpleado[2].ToString()+"-" +readerEmpleado[0].ToString()+"; ";
                                 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
            finally
            {
                // fecha o leitor
                if (readerEmpleado != null)
                {
                    readerEmpleado.Close();
                }

                // fecha a conexão
                if (conexion != null)
                {
                    conexion.Close();
                }
		              
            }
			
		}
        
        

        public void OnComplete(object Control, DPFP.FeatureSet FeatureSet, ref DPFP.Gui.EventHandlerStatus Status)
        {
            if (Data == null) {
                Status = DPFP.Gui.EventHandlerStatus.Failure;
                return;
            }

        	DPFP.Template unahuella;
        	unahuella= TomarHuellaBd();
            
        	DPFP.Verification.Verification ver = new DPFP.Verification.Verification();
            DPFP.Verification.Verification.Result res = new DPFP.Verification.Verification.Result();

            // Compare feature set with all stored templates.
           // foreach (DPFP.Template template in Data.Templates)
           // {
                // Get template from storage.
                if (unahuella != null)
                {
                    // Compare feature set with particular template.
                    ver.Verify(FeatureSet, unahuella, ref res);
                   // Data.IsFeatureSetMatched = res.Verified;
                   // Data.FalseAcceptRate = res.FARAchieved;
                }
                
                if (res.Verified)
                   { //    break; // success aqui checa el empleado
                	disparareloj(NumEmpleado);
                	//MessageBox.Show("entrada");
                  }
            //}
            

            if (!res.Verified)
                Status = DPFP.Gui.EventHandlerStatus.Failure;

            //Data.Update();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmVerification_Load(object sender, EventArgs e)
        {
                //using (FileStream fs = File.OpenRead(open.FileName)
				//	DPFP.Template template = new DPFP.Template(fs);
				ActiveControl = Txt_idEmpleado;
			    Txt_idEmpleado.Focus();		
				FechaLargo.Text=	DateTime.Now.ToLongDateString();
				Santoral();
				
				
        }
        
        private DPFP.Template TomarHuellaBd()
			{
				// Verifica la Huella desde una base de Datos
		
				bool Bandera=true;
			try { // Validar Numero
				 int idEmpleado =int.Parse(Global_ip.Globalip.ToString());
				 } catch {
					MessageBox.Show("Error Numero de Empleado ", Global_ip.Globalip.ToString());
    	        	Bandera=false;
    	        	//
    	        	return null;
    	        	//
				}
			try
				{
				NumEmpleado=int.Parse(Global_ip.Globalip.ToString());
				enlacedb db = new enlacedb();
				FbConnection conexion2 = new FbConnection(db.connectionString);
				conexion2.Open();
				FbCommand Frda = new FbCommand("SELECT TBEMPLEADO.IDEMPLEADO, TBEMPLEADO.EMP_NOM, TBEMPLEADO.EMP_DBHUELLA, TBEMPLEADO.EMP_FOTO FROM TBEMPLEADO  WHERE  TBEMPLEADO.IDEMPLEADO=@IDEMPLEADO AND TBEMPLEADO.ACTIVO=@ACTIVO AND  EMP_DBHUELLA IS NOT NULL ", conexion2);
						Frda.Parameters.Add("@IDEMPLEADO",SqlDbType.Int).Value =  NumEmpleado;
						Frda.Parameters.Add("@ACTIVO",SqlDbType.Char).Value = "SI";
						FbDataReader leerF = Frda.ExecuteReader();
						bool fseek= leerF.Read();
							if (fseek) { 
							Byte[] imageFoto = new Byte[Convert.ToInt32 ((leerF.GetBytes(3, 0,null, 0, Int32.MaxValue)))];
						    leerF.GetBytes(3, 0, imageFoto, 0, imageFoto.Length);
							MemoryStream ms = new MemoryStream();
							ms.Write(imageFoto,0,imageFoto.GetUpperBound(0)+1);
							Image imgFoto = System.Drawing.Image.FromStream(ms);
							Global_Foto.GlobalFoto =  Util.imageToByteArray(imgFoto);
							ms.Close();
							//LblNombre.Text= Global_Nombre.GlobalNombre;
							Global_Nombre.GlobalNombre= leerF.GetValue(1).ToString();
							Byte[] imageF = new Byte[Convert.ToInt32 ((leerF.GetBytes(2, 0,null, 0, Int32.MaxValue)))];
								leerF.GetBytes(2, 0, imageF, 0, imageF.Length);
								MemoryStream memfpt = new MemoryStream(imageF);
								DPFP.Template template = new DPFP.Template(memfpt);
								memfpt.Close();
								conexion2.Close();
								return template;
						        }
						else
						{
							MessageBox.Show("Validar su Huella en el Sistema ó Pasar con el Administrador");
							Txt_idEmpleado.Text="";
						return null;
						}
						conexion2.Close();
				}
				catch (Exception err2) {
								MessageBox.Show(err2.ToString());
						}
			return null;
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
 
 	public class WSounds
  {
    [DllImport("WinMM.dll")]
    public static extern bool  PlaySound(string fname, int Mod, int flag);
 
    // these are the SoundFlags we are using here, check mmsystem.h for more
    public int SND_ASYNC    = 0x0001;     // play asynchronously
    public int SND_FILENAME = 0x00020000; // use file name
    public int SND_PURGE    = 0x0040;     // purge non-static events
  
    public void Play(string fname, int SoundFlags)
    {
      PlaySound(fname, 0, SoundFlags);
    }
 
    public void StopPlay()
    {
      PlaySound(null, 0, SND_PURGE);
    }
  }
 
 
 
 void tocartema(string eltema)
		{
			
		string mRuta = "C://Reloj2021//Sonido";
			
		string 	tocarmusica="";
		string extwav=".wav";
		
		tocarmusica= mRuta.ToString() + "/" + eltema.ToString() + extwav.ToString()  ;
			
			WSounds ws = new WSounds();
      ws.Play(tocarmusica, ws.SND_FILENAME|ws.SND_ASYNC);
		}
 
 
 
 
 
 
    void TextBox1TextChanged(object sender, EventArgs e)
        {
        	Global_ip.Globalip=Txt_idEmpleado.Text;
        	
        }
    
    
    void disparareloj(int id_trabajador)
    {
    
    try
				{
	            enlacedb db3 = new enlacedb();
				FbCommand cmdCheck = new FbCommand("CHECK_DIA",new FbConnection(db3.connectionString));
				cmdCheck.CommandType= CommandType.StoredProcedure;
				cmdCheck.Parameters.Add("@IDEMPLEADO",FirebirdSql.Data.FirebirdClient.FbDbType.Integer).Value =id_trabajador;
							//retorno de numero de numero de sesion que tiene el SP
							FbParameter Output =new FbParameter();
							Output.Direction=ParameterDirection.ReturnValue;
							Output.FbDbType=FbDbType.VarChar;
							Output.ParameterName="STATUS";
							cmdCheck.Parameters.Add(Output);
							
							//Almacenar Datos de la sesion de Alumnos
				
				cmdCheck.Connection.Open();
				//FbTransaction transx3=cmdCheck.Connection. BeginTransaction();
				//cmdCheck.Transaction=transx3;
				cmdCheck.ExecuteNonQuery();
				//transx3.Commit();
				//transx3.Dispose();
				
				/*
				int result = (int)cmdCheck.ExecuteScalar();
				if (result>0){
				   MessageBox.Show("Huella Actualizado");
				}
				*/
				string valorstatus=(string)Output.Value;
				LblEvento.Text=valorstatus;
			 
				
				LblNombre.Text=Global_Nombre.GlobalNombre;
				
				  
			     string imgPath = System.IO.Path.Combine("C:\\Reloj2021\\Imagenes\\",LblEvento.Text.ToString()+".jpg" );
                    Bitmap b = (Bitmap)Image.FromFile(imgPath , true);
			          
			  
				pictureBox1.Image=Util.ByteArrayToImage(Global_Foto.GlobalFoto);
				dataGridView1.Rows.Add((Bitmap)b ,valorstatus,Global_Nombre.GlobalNombre,DateTime.Now.ToString("hh:mm tt"));
				tocartema(valorstatus.ToString());
				Txt_idEmpleado.Text=" ";
				
				valorstatus=" ";
				//cmdCheck.Dispose();
				cmdCheck.Connection.Close();
					
			}
			
				catch (Exception err2) {
						MessageBox.Show(err2.ToString());
				}
							
			   

    
    }
    	
    int NumEmpleado;
    
        
        void PictureBox1Click(object sender, EventArgs e)
        {
        	
        }
        
        void Timer1Tick(object sender, EventArgs e)
        {
        	 LblReloj.Text = DateTime.Now.ToLongTimeString();
        	 
        }
    }
}
