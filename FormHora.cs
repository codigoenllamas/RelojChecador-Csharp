/*
 * Created by SharpDevelop.
 * User: pcaula
 * Date: 15/12/2014
 * Time: 11:11 p.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using FirebirdSql.Data.FirebirdClient;

namespace PersonalNet
{
	/// <summary>
	/// Description of FormHora.
	/// </summary>
	public partial class FormHora : Form
	{
		public FormHora()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		
		//
		
		public static Form IsFormAlreadyOpen(Type FormType)
{
   foreach (Form OpenForm in Application.OpenForms)
   {
      if (OpenForm.GetType() == FormType)
         return OpenForm;
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
		
		
		
		
		//
		
		
		
		
		
		
		
		
		
		void Button1Click(object sender, EventArgs e)
		{
			
			
			
			
			
			
				enlacedb dbHora = new enlacedb();
	    FbConnection CnxHora= new FbConnection(dbHora.connectionString);	
     try{
	   int RegisValor = Convert.ToInt32(ClassTime.GlobalReg.ToString());
	   
      TimeSpan tUp = new TimeSpan(0, Convert.ToInt32(HoraDown.Value), Convert.ToInt32(MinutoDown.Value), Convert.ToInt32("0000"));
      string formattedTimeSpan = string.Format("{0:D2}, {1:D2}, {2:D2}", tUp.Hours, tUp.Minutes, tUp.Seconds);
     
      DateTime d=Convert.ToDateTime(lbl_fecha.Text);
	  DateTime LafechayHora = d.Add(tUp); 
	   CnxHora.Open();
	   FbCommand SqlUpdate = new FbCommand("UPDATE REGIS_DIARIO   SET REGIS_DIARIO.CHK_FECHA=@CHK_FECHA, REGIS_DIARIO.IDUSER=@IDUSER, REGIS_DIARIO.REF_DOCTO=@REF_DOCTO,REGIS_DIARIO.CHK_HORA=@CHK_HORA WHERE REGIS_DIARIO.IDCONTROL=@IDCONTROL" ,CnxHora);
       SqlUpdate.Parameters.AddWithValue("@IDCONTROL",RegisValor);
	   SqlUpdate.Parameters.AddWithValue("@CHK_FECHA",LafechayHora);
	   SqlUpdate.Parameters.AddWithValue("@IDUSER",Global_ip.Globalip);
       SqlUpdate.Parameters.AddWithValue("@REF_DOCTO","PREVIA AUTORIZACION DEL ADMINISTRADOR NUM.");
         SqlUpdate.Parameters.AddWithValue("@CHK_HORA",tUp);
	   int mReg=SqlUpdate.ExecuteNonQuery();
				  if(mReg==1){
					MessageBox.Show(this,"Actualizado...","Empleado",MessageBoxButtons.OK,MessageBoxIcon.None);
                    }
	   //LeerRegistro.Dispose();
		CnxHora.Close();
	    }
  		 catch (Exception err1) {
				MessageBox.Show(err1.ToString());
			}
							
			Close();
			
		}
		
		void FormHoraLoad(object sender, EventArgs e)
		{
			this.Owner.Enabled = false;
		  label3.Text=ClassTime.GlobalIdEmple.ToString();
		
     	
     	enlacedb dbHora = new enlacedb();
	    FbConnection CnxHora= new FbConnection(dbHora.connectionString);	
     try{
			int RegisId = Convert.ToInt32(ClassTime.GlobalReg.ToString());
        CnxHora.Open();
		FbCommand SqlHora = new FbCommand("SELECT REGIS_DIARIO.IDCONTROL, REGIS_DIARIO.IDEMPLEADO, cast(REGIS_DIARIO.CHK_FECHA as date), REGIS_DIARIO.MOVIMIENTO, REGIS_DIARIO.CHK_HORA FROM REGIS_DIARIO  WHERE REGIS_DIARIO.IDCONTROL=@IDCONTROL" ,CnxHora);
      SqlHora.Parameters.Add("@IDCONTROL",SqlDbType.Int).Value = RegisId;
	  FbDataReader leerHora = SqlHora.ExecuteReader();
	  	//reader.HasRows
	  bool rowhora= leerHora.Read();
	if(rowhora) 	{
  		 //jvf. Sacar solo la Hora del campo  
  		 		 
  		 lbl_fecha.Text=Convert.ToDateTime(leerHora.GetString(2)).ToString("D");
	  	string ModHora=leerHora.GetString(4).ToString();
	  	var segments = ModHora.Split(':');
	  	TimeSpan t = new TimeSpan(0, Convert.ToInt32(segments[0]), 
				               Convert.ToInt32(segments[1]), Convert.ToInt32(segments[2]));
	  	
	  	    HoraDown.Value=(int) t.TotalHours;
	  	    MinutoDown.Value=(int) t.Minutes;
	  	
		
		
		//LeerRegistro.Dispose();
		CnxHora.Close();
	  }
	   }
  		 catch (Exception err1) {
				MessageBox.Show(err1.ToString());
			}
			
			
			
			
		}
		
		void FormHoraFormClosed(object sender, FormClosedEventArgs e)
		{
			this.Owner.Enabled = true;
		}
	}
}
