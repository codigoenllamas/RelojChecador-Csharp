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
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using System.IO;
using System.Drawing.Printing;
using System.Collections.Generic;
using System.Text;


namespace PersonalNet
{
	/// <summary>
	/// Description of Hijo2.
	/// </summary>
	public partial class FormDatos : Form
	 {
		 private bool _isDirty = false;
		 private bool _isRol = false;
		 private int id_consecutivo =0;
		 private string ruta_archivo= " ";
		 private string ruta_archivo2= " ";
		  string[] textolargo = new string[4];
		  public string sSelectedClient;
		  public string sSelectedClient2;
		  public string sSelectedRol;
		  public string sSelectedRol2;
		  public string mypassw;
		  public string passw;
		  public string myrol;
		  

		public FormDatos()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		// code de ficha
		// para dividir el largo de una cadena de texto
		public static string[] StringSplitWrap(string sentence, int MaxLength)
{
			
        List<string> parts = new List<string>();
       // string sentence = "Silver badges are awarded for longer term goals. Silver badges are uncommon.";

        string[] pieces = sentence.Split(' ');
        StringBuilder tempString = new StringBuilder("");

        foreach (var piece in pieces)
        {
            if (piece.Length + tempString.Length + 1 > MaxLength)
            {
                parts.Add(tempString.ToString());
                tempString.Clear();
            }
            tempString.Append((tempString.Length == 0 ? "" : " ") + piece);
        }

        if (tempString.Length>0)
            parts.Add(tempString.ToString());

        return parts.ToArray();
    }
		
	// SECCION DE MODULOS COMPARTIDOS
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


		
private void LimpiarTextBox2()
	 {
     // hace un chequeo por todos los textbox del formulario
       lbl_claveid2.Text="";
       Txt_Nombre2.Text="";
       Txt_celular2.Text="";
       Txt_curp2.Text="";
       Txt_Mail2.Text="";
       Txt_Nombre2.Text="";
       Txt_Puesto2.Text="";
       Txt_telCasa2.Text="";
       //aqui debe ir el otro combo
       Txt_Depto2.Text="";
     // cmbRol2.SelectedItem = null;
     //cmbStatus2.SelectedIndex = 1; 
     //cmbRol2.SelectedIndex = 1;
     Rch_Comentario2.Text=String.Empty;
     Rch_Direccion2.Text=String.Empty;
     rb_Hombre2.Checked = false;
     rb_Hombre2.TabStop = true;
     rb_Mujer2.Checked = false;
     rb_Mujer2.TabStop = true;
     Pic_Foto2.Image=null;
     
      }
	 
	 private void LimpiarTextBox(Control.ControlCollection controls)
	 {
     // hace un chequeo por todos los textbox del formulario
     foreach (Control oControls in this.Controls){
            if (oControls is TextBox){
             oControls.Text =String.Empty; // eliminar el texto
             }
         }
     cmbStatus.SelectedIndex = 1; 
     cmbRol.SelectedIndex = 1;
     Rch_Comentario.Text=String.Empty;
     Rch_Direccion.Text=String.Empty;
     Txt_Nombre.Text="";
     Txt_curp.Text="";
     Txt_telCasa.Text="";
     Txt_celular.Text="";
     Txt_Mail.Text="";
     Txt_Depto.Text="";
     Cbx_Txt_Depto.SelectedIndex=0;
     Txt_Puesto.Text="";
     rb_Hombre.Checked = false;
     rb_Hombre.TabStop = true;
     rb_Mujer.Checked = false;
     rb_Mujer.TabStop = true;
     Pic_Foto.Image=null;
     Pic_Foto2.Image=null;
     cbx_lista.Focus();
      }
				
	
	 void DepurarEmpleado( int NumEmp)
	 {
	  // abrir BAse de datos
		// 	
		enlacedb  dbempleado =new enlacedb();
			    FbConnection ConBase= new FbConnection(dbempleado.connectionString);	
  			    ConBase.Open();
  			   
		      try
				{
					// Open two connections.
					// ojo a ver si requiere del Famoso Commit Transantion
				FbCommand SqlBorrar = new FbCommand("DELETE FROM TBEMPLEADO   WHERE  TBEMPLEADO.IDEMPLEADO = @IDEMPLEADO", ConBase);
				SqlBorrar.Parameters.AddWithValue("@IDEMPLEADO", NumEmp);
				if(SqlBorrar.ExecuteNonQuery()==1)
					MessageBox.Show("Empleado (a); Eliminado!");
                ConBase.Close();

					}
		
				catch (Exception err1) {
						MessageBox.Show(err1.ToString());
					}	

	 
	 
	 
	 }
	 
	 
	 
	 
	 
	 
	 
	 
	 
	//SECCION DE AGREGAR REGISTRO NUEVO Y COMPONENTES Y/O OBJETOS DETAPCONTROL1
	// BOTON NUEVO REGISTRO
	void Btn_NuevoClick(object sender, EventArgs e)
		{
			
			
			Btn_Ficha.Enabled = false;
			cbx_lista.Enabled = false;
			Btn_Nuevo.Enabled = false;
			//cmbStatus.Enabled =	false;
		   	//cmbRol.Enabled =	false;
			Btn_guardar.Enabled = true;
			Btn_ruta.Enabled = true;
			Btn_guardar.Text = "Guardar Empleado";
			//btnSelector.Enabled = false;			
			enlacedb db3 = new enlacedb();
			
			FbConnection conectime = new FbConnection(db3.connectionString);
			id_consecutivo=0;
			try
				{
					conectime.Open();
					//leer ultimo registro de idempleado
					FbCommand da = new FbCommand("select gen_id(GD_ADD_REGISTRO, 0) from rdb$database;", conectime);
					FbDataReader reader = da.ExecuteReader();
					bool hasrow= reader.Read();
					if(hasrow) {
					id_consecutivo=reader.GetInt32(0);
					id_consecutivo ++;
					
					lbl_claveid.BackColor = Color.Cyan;
					lbl_claveid.Text= "Escribir sus datos del empeado(a): Num. -> "+id_consecutivo.ToString();
					}
				}
			catch (FbException error)
				{
				MessageBox.Show(error.Message);
				}
			finally
				{
				conectime.Close();
				conectime.Dispose();
				} //fin try

			
			
			
		}
	
	// BOTON SUBIR FOTO AL PICTURE
	void Btn_rutaClick(object sender, EventArgs e)
		{
	     using (OpenFileDialog dlg = new OpenFileDialog())
		    {
		        dlg.Title = "Abrir Image";
		        dlg.Filter = "Archivo Jpg (*.jpg)|*.jpg";
		
		        if (dlg.ShowDialog() == DialogResult.OK)
		        {
		            //PictureBox PictureBox1 = new PictureBox();
		            ruta_archivo = dlg.FileName;
		            Pic_Foto.Image = new Bitmap(dlg.FileName);
		
		            // Add the new control to its parent's controls collection
		            //this.Controls.Add(PictureBox1);
		        }
		    }
		}
	
	
	// BOTON GUARDAR
		void Btn_guardarClick(object sender, EventArgs e)
		{
								
			
			if(Txt_Nombre.Text.Length<=0)
			{
				Txt_Nombre.Focus();
				MessageBox.Show(this,"Agregar Nombre","Campo Incompleto",MessageBoxButtons.OK,MessageBoxIcon.Hand);
				return;
			}
			if(!rb_Hombre.Checked && !rb_Mujer.Checked)
			{
				groupBox2.Focus();
				groupBox2.BackColor =Color.Aquamarine;
				MessageBox.Show(this,"Tipo de Genero","Seleccionar",MessageBoxButtons.OK,MessageBoxIcon.Hand);
				return;
			}
		    if(Rch_Direccion.Text.Length<=0)
			{
				Rch_Direccion.Focus();
				MessageBox.Show(this,"Agregar la Dirección","Campo Incompleto",MessageBoxButtons.OK,MessageBoxIcon.Hand);
				return;
			}
			if(Txt_celular.Text.Length<=0)
			{
				Txt_celular.Focus();
				MessageBox.Show(this,"Agregar Celular","Campo Incompleto",MessageBoxButtons.OK,MessageBoxIcon.Hand);
				return;
			}
			if(Pic_Foto.Image == null)
				{
				Btn_ruta.Focus();
				MessageBox.Show(this,"Agregar Fotografia","No Hay Imagen",MessageBoxButtons.OK,MessageBoxIcon.Hand);
				return;
			}
			
			Btn_guardar.Text = "Guardando...";
            Btn_guardar.Enabled = false;
            Btn_ruta.Enabled = false;
            
            
            
			    
			try
			 {
				// Guardar Foto
				FileStream fs = new FileStream(@ruta_archivo, FileMode.Open, FileAccess.Read);
				Byte[] blob = new Byte[fs.Length];
				fs.Read(blob, 0, blob.Length);
				int totaljpg =(int) fs.Length;
				
				
				string sSexo=" ";
				if(rb_Hombre.Checked)sSexo="H";
				if(rb_Mujer.Checked)sSexo="M";
				    enlacedb db2 = new enlacedb();
					FbConnection  conexion2 = new FbConnection(db2.connectionString);
			// agregar dos campos el tipo de empleado y establecer acceso al usuario en cero 
					FbCommand cmdsesion = new FbCommand("ADD_EMPLEADOS",new FbConnection(db2.connectionString));
					cmdsesion.CommandType= CommandType.StoredProcedure;
					cmdsesion.Parameters.Add("@EMP_NOM",SqlDbType.VarChar).Value =Txt_Nombre.Text;
					cmdsesion.Parameters.Add("@EMP_SEX",SqlDbType.Char).Value =sSexo;
					cmdsesion.Parameters.Add("@EMP_FECNAC",SqlDbType.Date).Value =dateTimePicker1.Value.ToLongDateString();
					cmdsesion.Parameters.Add("@EMP_CURP",SqlDbType.VarChar).Value =Txt_curp.Text;
					cmdsesion.Parameters.Add("@EMP_DIREC",SqlDbType.VarChar).Value =Rch_Direccion.Text;
					cmdsesion.Parameters.Add("@EMP_TEL",SqlDbType.VarChar).Value =Txt_telCasa.Text;
					cmdsesion.Parameters.Add("@EMP_CEL",SqlDbType.VarChar).Value =Txt_celular.Text;
				    cmdsesion.Parameters.Add("@EMP_DEPTO",SqlDbType.VarChar).Value =Txt_Depto.Text;
				    cmdsesion.Parameters.Add("@EMP_PUESTO",SqlDbType.VarChar).Value =Txt_Puesto.Text;
					cmdsesion.Parameters.Add("@EMP_COMEN",SqlDbType.VarChar).Value =Rch_Comentario.Text;
					cmdsesion.Parameters.Add("@EMP_FOTO",SqlDbType.Binary).Value =blob;
					cmdsesion.Parameters.Add("@EMP_SZFOTO",SqlDbType.Date).Value =totaljpg;
					//cmdsesion.Parameters.Add("@EMP_NIVELUS",SqlDbType.VarChar).Value ="EMPLEADO";
					//cmdsesion.Parameters.Add("@SUSPENDIDO",SqlDbType.Int).Value =0;
					cmdsesion.Connection.Open();
					FbTransaction transx=cmdsesion.Connection. BeginTransaction();
					cmdsesion.Transaction=transx;
					cmdsesion.ExecuteNonQuery();
					transx.Commit();
					transx.Dispose();
					cmdsesion.Connection.Close();  
					MessageBox.Show(this,"Almacenado","Empleado",MessageBoxButtons.OK,MessageBoxIcon.Hand);
					LimpiarTextBox(this.Controls);
			
			}
			catch (Exception err2){
			MessageBox.Show(err2.ToString(),"Revisar esta Linea");
			Close();
			}
			
			  Btn_guardar.Text = "Almacenado...";
            Btn_Nuevo.Enabled = true; 
            // pasar el formulario
            _isDirty = false;
           LimpiarTextBox(this.Controls);
			
	  }
	
   	//SECCION DE TAPCONTROL2 MODULO ACTUALIZAR Y/O IMPRIMIR
   	  	
   	// SUBIR FOTO DEL PICTUREBOX DE TAPCONTROL2
    void Button3Click(object sender, EventArgs e)
		{
			using (OpenFileDialog dlg = new OpenFileDialog())
		    {
		        dlg.Title = "Abrir Image";
		        dlg.Filter = "Archivo Jpg (*.jpg)|*.jpg";
		
		        if (dlg.ShowDialog() == DialogResult.OK)
		        {
		            //PictureBox PictureBox1 = new PictureBox();
		            ruta_archivo2 = dlg.FileName;
		            Pic_Foto2.Image = new Bitmap(dlg.FileName);
		
		            // Add the new control to its parent's controls collection
		            //this.Controls.Add(PictureBox1);
		        }
		    }
		}
		

   	
   	// SELCCION DEL COMBOX DEL EMPLEADO
   	void Cbx_listaSelectedIndexChanged(object sender, EventArgs e)
		{
		string[] words2 = cbx_lista.Text.Split(new char[] { '-'});
		string apuntareg2 =words2[0];
		enlacedb dbsearch2 = new enlacedb();
		FbConnection cnxsearch2 = new FbConnection(dbsearch2.connectionString);
			try {
				
				cnxsearch2.Open();
				FbCommand mdafoto2 = new FbCommand("SELECT a.IDEMPLEADO, a.EMP_NOM ,a.EMP_SEX ,a.EMP_FECNAC ,a.EMP_CURP ,a.EMP_RFC ,a.EMP_DIREC ,a.EMP_TEL, a.EMP_CEL, a.EMP_MAIL, a.EMP_DEPTO, a.EMP_PUESTO, a.EMP_COMEN, a.EMP_FECALT, a.EMP_FOTO, a.ACTIVO, a.EMP_NIVELUS, a.EMP_PWD  FROM TBEMPLEADO a WHERE IDEMPLEADO=@IDEMPLEADO", cnxsearch2);
					mdafoto2.Parameters.Add("@IDEMPLEADO",SqlDbType.VarChar).Value = apuntareg2.ToString();
					FbDataReader reader2 = mdafoto2.ExecuteReader();
			                  	//reader.HasRows
			                bool hasrow2= reader2.Read();
							if(hasrow2) 	{
			                   lbl_claveid2.Text=reader2["IDEMPLEADO"].ToString();
			                   Txt_Nombre2.Text= reader2["EMP_NOM"].ToString();
			                   mypassw=reader2["EMP_PWD"].ToString();
			                   rb_Hombre2.Checked=false;
							   rb_Mujer2.Checked=false;		                   
			                   if(reader2["EMP_SEX"].ToString()=="H")
			                   {
			                    rb_Hombre2.Checked=true;
			                   }
			                   else
			                   {
			                   rb_Mujer2.Checked=true;
			                   }
			                   // ver si no este vacio 
			                   //if (!string.IsNullOrEmpty(comboBox4.Text))
                               //       {
			                   //Combox Actualizar el combo del registro de BD, localiar y visualizar su contenido
			                   
			                   int index  = CmbActivo.FindString(reader2["ACTIVO"].ToString());
								    if (index < 0)
								    {
								        MessageBox.Show("Item not found.");
								        //textBox1.Text = String.Empty;
								    }
								    else
								    {
								    CmbActivo.SelectedIndex = index;
								    }
								    //leer segundo combo
			                   int index2  = cmbRol2.FindString(reader2["EMP_NIVELUS"].ToString());
								    if (index2 < 0)
								    {
								        MessageBox.Show("Item not found.");
								        //textBox1.Text = String.Empty;
								    }
								    else
								    {
								    cmbRol2.SelectedIndex = index2;
								    }
			                   
									
			                   //cmbRol2.SelectedIndex = (int)CmbActivo.FindStringExact(reader2["EMP_NIVELUS"].ToString().Trim());
			                   //value is selected
			                   myrol=reader2["EMP_NIVELUS"].ToString();
				               //sSelectedRol2  = (string) cmbRol2.SelectedItem;
			                   
			                   	              	
								dateTimePicker1.Value = (DateTime)reader2["EMP_FECNAC"];
								Txt_curp2.Text=reader2["EMP_CURP"].ToString();
							    Rch_Direccion2.Text=reader2["EMP_DIREC"].ToString();
								Txt_telCasa2.Text=reader2["EMP_TEL"].ToString();
								Txt_celular2.Text=reader2["EMP_CEL"].ToString();
								Txt_Mail2.Text=reader2["EMP_MAIL"].ToString();
								Txt_Depto2.Text=reader2["EMP_DEPTO"].ToString();
								Txt_Puesto2.Text=reader2["EMP_PUESTO"].ToString();
								Rch_Comentario2.Text=reader2["EMP_COMEN"].ToString();
  								//  EMP_FOTO Blob sub_type 0,
  			                	Byte[] img2 = new Byte[Convert.ToInt32 ((reader2.GetBytes(14, 0,null, 0, Int32.MaxValue)))]; 
								reader2.GetBytes(14, 0, img2, 0, img2.Length);	
								FileStream fs2 = new FileStream (@"c:\intel\txps.jpg", FileMode.Create, FileAccess.ReadWrite); 
								for(int i=0;i<img2.Length;i++)
							    fs2.WriteByte(img2[i]);
								fs2.Close();
								MemoryStream ms2 = new MemoryStream();
								ms2.Write(img2,0,img2.GetUpperBound(0)+1);
								//pic_docente
								Pic_Foto2.Image=System.Drawing.Image.FromStream(ms2);
								ms2.Close();
								Btn_guardar.Enabled=false;
								btnActual.Enabled=true;
			                	Btn_ruta.Enabled=true;
			                	btnSelector.Enabled = false;
			                  	}
			                  			
			
			        cnxsearch2.Close();
					}
		
				catch (Exception err1) {
						MessageBox.Show(err1.ToString());
					}
			
		}
		
   	//BOTON VER EMPLEADO
   	void BtnSelectorClick(object sender, EventArgs e)
		{
			
			Btn_Ficha.Enabled=true;
			Btn_ruta2.Enabled=true;
			cbx_lista.Enabled=true;
			cmbRol2.Enabled =	true;
		   	CmbActivo.Enabled = true;
			//ACCESO DE DATABASE
		       enlacedb  db =new enlacedb();
			    FbConnection conexiongpos= new FbConnection(db.connectionString);	
  			    conexiongpos.Open();
		      try
				{
					// Open two connections.
 				
			  	FbCommand llenargrupos = new FbCommand("SELECT TBEMPLEADO.IDEMPLEADO, TBEMPLEADO.EMP_NOM FROM TBEMPLEADO WHERE TBEMPLEADO.ACTIVO <> 'NO' ORDER BY IDEMPLEADO" ,conexiongpos);
				FbDataAdapter relgpos = new FbDataAdapter(llenargrupos);
				DataTable losgpos = new DataTable();
				relgpos.Fill(losgpos);
				cbx_lista.Items.Clear();
					foreach (DataRow dr in losgpos.Rows) {
							cbx_lista.Items.Add(dr["IDEMPLEADO"].ToString()+'-'+dr["EMP_NOM"].ToString()); 
				   }
				//comboBox1.SelectedIndex=0;
		     	conexiongpos.Close();
		        conexiongpos.Dispose();
		        lblSeleccionar.Visible= true;
		        cbx_lista.SelectedIndex=0;
				}
			catch (Exception error) {
			    	MessageBox.Show(error.Message); }
			finally { 
			    	// dr.Dispose();
					//	cmd.Dispose();
					//	conn.Dispose();
		    	
			    	conexiongpos.Close(); conexiongpos.Dispose();
		      }
				  				  				
			
		   //ACCESSO DE DATABASE
			
		}
		
   		
   	
   	// BOTON ACTUALIZAR
   	void BtnActualClick(object sender, EventArgs e)
		{
			if(Txt_Nombre2.Text.Length<=0)
			{
				Txt_Nombre2.Focus();
				MessageBox.Show(this,"Agregar Nombre","Campo Incompleto",MessageBoxButtons.OK,MessageBoxIcon.Hand);
				return;
			}
			if(!rb_Hombre2.Checked && !rb_Mujer2.Checked)
			{
				groupBox2.Focus();
				groupBox2.BackColor =Color.Aquamarine;
				MessageBox.Show(this,"Tipo de Genero","Seleccionar",MessageBoxButtons.OK,MessageBoxIcon.Hand);
				return;
			}
		    if(Rch_Direccion2.Text.Length<=0)
			{
				Rch_Direccion2.Focus();
				MessageBox.Show(this,"Agregar la Dirección","Campo Incompleto",MessageBoxButtons.OK,MessageBoxIcon.Hand);
				return;
			}
			if(Txt_celular2.Text.Length<=0)
			{
				Txt_celular2.Focus();
				MessageBox.Show(this,"Agregar Celular","Campo Incompleto",MessageBoxButtons.OK,MessageBoxIcon.Hand);
				return;
			}
			if(Pic_Foto2.Image == null)
				{
				Btn_ruta2.Focus();
				MessageBox.Show(this,"Agregar Fotografia","No Hay Imagen",MessageBoxButtons.OK,MessageBoxIcon.Hand);
				return;
			}

			
			 if(CmbActivo.SelectedIndex==-1)
				{
				//its mean no value selected
				MessageBox.Show(this," Empleado Activo.?  SI/NO ","Seleccionar",MessageBoxButtons.OK,MessageBoxIcon.Hand);
				return;
				}
				else
				{
				//value is selected
				sSelectedClient2  = (string) CmbActivo.SelectedItem;
				}
			
			
			
			//cheka si sellecion el rol del empleado
			 if(cmbRol2.SelectedIndex==-1)
				{
				//its mean no value selected
				MessageBox.Show(this," al Emplado ","Asignar Rol",MessageBoxButtons.OK,MessageBoxIcon.Hand);
				return;
				}
				else
				{
				//value is selected
				sSelectedRol2  = (string) cmbRol2.SelectedItem;
				}
				//detecta si es SYSADMIN o si cambio de rol pone el password en blanco
				string passw=mypassw;
				if(_isRol)
				{
					passw=Inputbox.Show("Para Esta Cuenta la", "Clave secreta (4 ó 5 Dígitos)", FormStartPosition.CenterScreen);
					if (passw==null){return;}
					if (passw.Length==0){return;}
					//if(!validarmatrix(n)){return;}
					//cadenapassw="1234";
					_isRol=false;
				}
				else
				{
					if(sSelectedRol2=="EMPLEADO")
					{
					passw="";
					}
				
				}
			 			
			
			
			if(ruta_archivo2.ToString() !=" ")
			{
			    try
			   {
				
				// Guardar Foto
				FileStream fs = new FileStream(@ruta_archivo2, FileMode.Open, FileAccess.Read);
				Byte[] blob = new Byte[fs.Length];
				fs.Read(blob, 0, blob.Length);
				int totaljpg =(int) fs.Length;
				
				
				string sSexo=" ";
				if(rb_Hombre2.Checked)sSexo="H";
				if(rb_Mujer2.Checked)sSexo="M";
				    enlacedb db3 = new enlacedb();
					FbConnection  conexion3 = new FbConnection(db3.connectionString);
			        conexion3.Open();
					FbCommand cmdsesion3 = new FbCommand("UPDATE TBEMPLEADO SET " +
					                                     "TBEMPLEADO.EMP_NOM=@EMP_NOM, TBEMPLEADO.EMP_SEX=@EMP_SEX, TBEMPLEADO.EMP_FECNAC=@EMP_FECNAC, TBEMPLEADO.EMP_CURP=@EMP_CURP, " +
					                                    "TBEMPLEADO.EMP_DIREC=@EMP_DIREC, TBEMPLEADO.EMP_TEL=@EMP_TEL, TBEMPLEADO.EMP_CEL=@EMP_CEL, TBEMPLEADO.EMP_MAIL=@EMP_MAIL, TBEMPLEADO.EMP_DEPTO=@EMP_DEPTO, TBEMPLEADO.EMP_PUESTO=@EMP_PUESTO, " +
					                                    "TBEMPLEADO.EMP_COMEN=@EMP_COMEN, TBEMPLEADO.EMP_FOTO=@EMP_FOTO, TBEMPLEADO.EMP_SZFOTO=@EMP_SZFOTO, TBEMPLEADO.ACTIVO=@ACTIVO, TBEMPLEADO.EMP_NIVELUS=@EMP_NIVELUS,  TBEMPLEADO.EMP_PWD=@EMP_PWD where TBEMPLEADO.IDEMPLEADO=@IDEMPLEADO" ,conexion3);
					cmdsesion3.Parameters.AddWithValue("@IDEMPLEADO",lbl_claveid2.Text);
					cmdsesion3.Parameters.AddWithValue("@EMP_NOM",Txt_Nombre2.Text.Trim());
					cmdsesion3.Parameters.AddWithValue("@EMP_SEX",sSexo);
					cmdsesion3.Parameters.AddWithValue("@EMP_FECNAC",dateTimePicker2.Value.ToLongDateString());
					cmdsesion3.Parameters.AddWithValue("@EMP_CURP",Txt_curp2.Text.Trim());
					cmdsesion3.Parameters.AddWithValue("@EMP_DIREC",Rch_Direccion2.Text.Trim());
					cmdsesion3.Parameters.AddWithValue("@EMP_TEL",Txt_telCasa2.Text.Trim());
					cmdsesion3.Parameters.AddWithValue("@EMP_CEL",Txt_celular2.Text.Trim());
					cmdsesion3.Parameters.AddWithValue("@EMP_MAIL",Txt_Mail2.Text.Trim());
					cmdsesion3.Parameters.AddWithValue("@EMP_DEPTO",Txt_Depto2.Text.Trim());
					cmdsesion3.Parameters.AddWithValue("@EMP_PUESTO",Txt_Puesto2.Text.Trim());
					cmdsesion3.Parameters.AddWithValue("@EMP_COMEN",Rch_Comentario2.Text.Trim());
					cmdsesion3.Parameters.AddWithValue("@EMP_FOTO",blob);
					cmdsesion3.Parameters.AddWithValue("@EMP_SZFOTO",totaljpg);
					cmdsesion3.Parameters.AddWithValue("@ACTIVO",sSelectedClient2.ToString());
					cmdsesion3.Parameters.AddWithValue("@EMP_NIVELUS",sSelectedRol2.ToString()  );
					cmdsesion3.Parameters.AddWithValue("@EMP_PWD",passw.ToString() );
				
					int registro=cmdsesion3.ExecuteNonQuery();
				  if(registro==1){
					MessageBox.Show(this,"Actualizado...","Empleado",MessageBoxButtons.OK,MessageBoxIcon.None);
					LimpiarTextBox(this.Controls);
                    }
					cmdsesion3.Dispose();
					
					
				
			}
						
				catch(Exception err1){
			        	MessageBox.Show(err1.ToString(),"Revisar esta Linea");
					Close();
			         }	 
						
		}// Fin si tiene ruta de archivo
		else
		{
			 try
			   {
				
							
				string sSexo2=" ";
				if(rb_Hombre2.Checked)sSexo2="H";
				if(rb_Mujer2.Checked)sSexo2="M";
				    enlacedb db3 = new enlacedb();
					FbConnection  conexion3 = new FbConnection(db3.connectionString);
			        conexion3.Open();
					FbCommand cmdsesion3 = new FbCommand("UPDATE TBEMPLEADO SET " +
					                                     "TBEMPLEADO.EMP_NOM=@EMP_NOM, " +
					                                     "TBEMPLEADO.EMP_SEX=@EMP_SEX, " +
					                                     "TBEMPLEADO.EMP_FECNAC=@EMP_FECNAC, " +
					                                     "TBEMPLEADO.EMP_CURP=@EMP_CURP, " +
					                                    "TBEMPLEADO.EMP_DIREC=@EMP_DIREC, " +
					                                    "TBEMPLEADO.EMP_TEL=@EMP_TEL, " +
					                                    "TBEMPLEADO.EMP_CEL=@EMP_CEL, " +
					                                    "TBEMPLEADO.EMP_MAIL=@EMP_MAIL, " +
					                                    "TBEMPLEADO.EMP_DEPTO=@EMP_DEPTO, " +
					                                    "TBEMPLEADO.EMP_PUESTO=@EMP_PUESTO, " +
					                                    "TBEMPLEADO.EMP_COMEN=@EMP_COMEN, " +
					                                    "TBEMPLEADO.ACTIVO=@ACTIVO, " +
					                                    "TBEMPLEADO.EMP_NIVELUS=@EMP_NIVELUS, " +
					                                    "TBEMPLEADO.EMP_PWD=@EMP_PWD " +
					                                    "where TBEMPLEADO.IDEMPLEADO=@IDEMPLEADO" ,conexion3);
					cmdsesion3.Parameters.AddWithValue("@IDEMPLEADO",lbl_claveid2.Text);
					cmdsesion3.Parameters.AddWithValue("@EMP_NOM",Txt_Nombre2.Text.Trim());
					cmdsesion3.Parameters.AddWithValue("@EMP_SEX",sSexo2);
					cmdsesion3.Parameters.AddWithValue("@EMP_FECNAC",dateTimePicker2.Value.ToLongDateString());
					cmdsesion3.Parameters.AddWithValue("@EMP_CURP",Txt_curp2.Text.Trim());
					cmdsesion3.Parameters.AddWithValue("@EMP_DIREC",Rch_Direccion2.Text.Trim());
					cmdsesion3.Parameters.AddWithValue("@EMP_TEL",Txt_telCasa2.Text.Trim());
					cmdsesion3.Parameters.AddWithValue("@EMP_CEL",Txt_celular2.Text.Trim());
					cmdsesion3.Parameters.AddWithValue("@EMP_MAIL",Txt_Mail2.Text.Trim());
					cmdsesion3.Parameters.AddWithValue("@EMP_DEPTO",Txt_Depto2.Text.Trim());
					cmdsesion3.Parameters.AddWithValue("@EMP_PUESTO",Txt_Puesto2.Text.Trim());
					cmdsesion3.Parameters.AddWithValue("@EMP_COMEN",Rch_Comentario2.Text.Trim());
					cmdsesion3.Parameters.AddWithValue("@ACTIVO",sSelectedClient2.ToString());
					cmdsesion3.Parameters.AddWithValue("@EMP_NIVELUS",sSelectedRol2.ToString());
					//se encontro el error la variable era null passw
					cmdsesion3.Parameters.AddWithValue("@EMP_PWD",passw.ToString() );
					int registro=cmdsesion3.ExecuteNonQuery();
				  if(registro==1){
					MessageBox.Show(this,"Actualizado...","Empleado",MessageBoxButtons.OK,MessageBoxIcon.None);
					LimpiarTextBox2();
                    }
					cmdsesion3.Dispose();
					
					
				
			}
						
				catch(Exception err1){
			        	MessageBox.Show(err1.ToString(),"Revisar esta Linea");
					Close();
			         }	
		
		}
			
			
			
			
			btnActual.Text = "Guardando...";
            btnActual.Enabled = false;
            Btn_ruta.Enabled = false;
            //btnSelector.Enabled =  false;
            btnActual.Text = "Almacenado...";
           Btn_Nuevo.Enabled = true; 
           // pasar el formulario
           _isDirty = false;
           LimpiarTextBox2();
			
		}
   	
   	 	
   	
   	
   	//impresion de registro
	private void imprime_imagen(object sender,  PrintPageEventArgs e)
		{
			
		Font font19 = new	Font("Arial", 18,FontStyle.Underline|FontStyle.Bold);
        e.Graphics.DrawString("REGISTRO DE PERSONAL",font19,Brushes.Black,220,45);	
		//dibujar un cuadro pequeño para la foto	
		Pen myPen = new Pen(Color.Black);//lado izquierdo,arriba,lado derecho, hacia abajo
		Rectangle  border = new Rectangle(40, 80, 740, 180);
        e.Graphics.DrawRectangle(Pens.Black, border);
        //impresion de una linea horizontal
        
        e.Graphics.DrawLine(myPen, 200, 120, 780,120); 
        e.Graphics.DrawLine(myPen, 200, 165, 780,165); 
        e.Graphics.DrawLine(myPen, 200, 210, 780,210);
        //impresion de una linea VERTICAL
        
        e.Graphics.DrawLine(myPen, 200, 80, 200,260); 
        e.Graphics.DrawLine(myPen, 660, 80, 660,260);   
        e.Graphics.DrawLine(myPen, 530, 80, 530,120); 
        //e.Graphics.DrawLine(myPen, 560, 210, 560,260); 
        //e.Graphics.DrawLine(myPen, 460, 210, 460,260); 
        
        
	  if (this.Pic_Foto2.Image != null)
	{
	
	e.Graphics.DrawImage(Pic_Foto2.Image, 40, 82, this.Pic_Foto2.Size.Width, this.Pic_Foto2.Size.Height);
	//e.Graphics.DrawImage(pictureBox2.Image, 30, 20, this.pictureBox2.Size.Width, this.pictureBox2.Size.Height);
	}
  //
 
  
// modulo para imprimir por linea en una hoja
Font printfuente = new	Font("Arial", 8);
Font fontsubrayado = new	Font("Aparajita", 8,FontStyle.Bold);
Graphics g= e.Graphics;
g.DrawString("ID PERS:",printfuente,Brushes.Black,662,82);
g.DrawString(lbl_claveid.Text,fontsubrayado,Brushes.Black,740,82);

dateTimePicker1.Format = DateTimePickerFormat.Custom;
dateTimePicker1.CustomFormat = "dd.MMM.yyyy hh:mm:ss";



g.DrawString(dateTimePicker1.Value.ToShortDateString(),fontsubrayado,Brushes.Black,662,130);
//g.DrawLine(myPen, 600, 91, 750,91); 
g.DrawString("NOMBRE:",printfuente,Brushes.Black,200,82);
g.DrawString(Txt_Nombre.Text+" ",fontsubrayado,Brushes.Black,200,95);
//" FECHA DE NAC. "+xFecha.Text
//g.DrawLine(myPen, 170, 107, 600,107); 
g.DrawString("CURP:",printfuente,Brushes.Black,530,82);
g.DrawString(Txt_curp.Text,fontsubrayado,Brushes.Black,530,92);
g.DrawString("DIRECCION:",printfuente,Brushes.Black,200,120);

textolargo =StringSplitWrap(Rch_Direccion.Text, 60);
int numlinea =(int)textolargo.Length;

switch (numlinea)
{
   case 1:
		g.DrawString(textolargo[0],fontsubrayado,Brushes.Black,200,130);
		break;
   case 2:
		g.DrawString(textolargo[0],fontsubrayado,Brushes.Black,200,130);
		g.DrawString(textolargo[1],fontsubrayado,Brushes.Black,200,137);
	    break;
   case 3:
    	g.DrawString(textolargo[0],fontsubrayado,Brushes.Black,200,130);
    	g.DrawString(textolargo[1],fontsubrayado,Brushes.Black,200,137);
		g.DrawString(textolargo[2],fontsubrayado,Brushes.Black,200,144);
   		break;
}

//g.DrawLine(myPen, 170, 119, 600,119);
g.DrawString("FEC. NAC.:",printfuente,Brushes.Black,662,120);
g.DrawString("CELULAR: "+ Txt_celular.Text,fontsubrayado,Brushes.Black,200,170);
g.DrawString("     CASA: "+ Txt_telCasa.Text,fontsubrayado,Brushes.Black,200,180);
g.DrawString("     MAIL: "+Txt_Mail.Text,fontsubrayado,Brushes.Black,200,190);
g.DrawString("OBSERVACION:",fontsubrayado,Brushes.Black,200,212);

Font fontsubrayado1 = new	Font("Arial", 6,FontStyle.Underline|FontStyle.Bold);

g.DrawString(Rch_Comentario.Text,fontsubrayado1,Brushes.Black,182,240);


//g.DrawString("TELEFONO:",printfuente,Brushes.Black,635,682);

// impresion del segundo borde
/*
Font font18 = new	Font("Arial", 18,FontStyle.Underline|FontStyle.Bold);
g.DrawString("REGISTRO DE INCIDENCIA",font18,Brushes.Black,260,260);
Rectangle  border2 = new Rectangle(30, 300, 800, 600);
    e.Graphics.DrawRectangle(Pens.Black, border2);
*/


	//int PosicionColumna=15;
	int PosicionFila=210;
	int i=0;

      Font f = new Font("Courier New",7);
      Font bf = new Font(f, FontStyle.Bold);
      StringFormat sf = new StringFormat();
    
      float nfilas=0;
   
  /*
   foreach (DataGridViewRow row  in dataGridView1.Rows) {
     string linea =" ";
   	string fechaincide =  (string)dataGridView1.Rows[i].Cells[0].Value ;
	string descrinci =  (string)dataGridView1.Rows[i].Cells[1].Value;
	string status = (string)dataGridView1.Rows[i].Cells[2].Value;
	string  asesor= (string)dataGridView1.Rows[i].Cells[3].Value;
	
	string subtotales = (string)dataGridView1.Rows[i].Cells[4].Value;
	
	
	  // Create a new pen that we shall use for drawing the line
    	if (descrinci != null){
	//g.DrawRectangle(Pens.Black,new Rectangle(1,(int)nfilas,500,20));
	  g.DrawString(fechaincide, f, Brushes.Black, 30, 320 + nfilas, sf);
	  g.DrawString(descrinci, bf, Brushes.Black, 180, 320 + nfilas, sf);
	  g.DrawString(status , f, Brushes.Black, 600, 320 + nfilas, sf);
	  g.DrawString(asesor, f, Brushes.Black, 640, 320 + nfilas, sf);
	  //g.DrawString(fechaincide, f, Brushes.Black, 30, 320 + nfilas, sf);
	  //impresion de una linea
	    g.DrawLine(myPen, 30, 330 + nfilas, 80, 330 + nfilas); 
	 }
	nfilas =nfilas  +f.Height;
	i++;
	}
   //FIN fOREACH
   */
   
  
  
  /*
    StringFormat sf2 = new StringFormat();
   //derterminar el espacio entre las columnas y son 4 columnas
   float[] ts2 = {60.0f};
   sf2.SetTabStops(0.0f, ts2);
   //posicion de los caracteres {0}\t -> alinea a la izquierda , {1,6}\t-> alinea a la derecha
   // ALINEA DIEZ CARACTERES
   string fs2 = "{0,10}";
    //
   string stotal =  lbl_total.Text;
   double ftotal=Convert.ToDouble(stotal);
          stotal=ftotal.ToString("$##,##0.00");
    string siva= lbl_iva.Text;
    double fiva=Convert.ToDouble(siva);
    		siva=fiva.ToString("$##,##0.00");
    string sneta= lbl_neto.Text;
    double fneta=Convert.ToDouble(sneta);
    		sneta=fneta.ToString("$##,##0.00");		
    
   string linea2 =string.Format(fs2,stotal);
   string linea3 =string.Format(fs2,siva);
   string linea4 =string.Format(fs2,sneta);
   
  g.DrawString(linea2, f, Brushes.Black, 550, 670, sf2);
  g.DrawString(linea3, f, Brushes.Black, 550, 670+f.Height, sf2); 
  g.DrawString(linea4, f, Brushes.Black, 550, 670+f.Height+bf.Height, sf2); 
   
   //
   
	//g.DrawString(lbl_total.Text,printfuente,Brushes.Black,650,660);
	//g.DrawString(lbl_iva.Text,printfuente,Brushes.Black,650,680);
	//g.DrawString(lbl_neto.Text,printfuente,Brushes.Black,650,700);
	g.DrawString(lbl_letras.Text,printfuente,Brushes.Black,200,720);
	//conector esta instruccion se corta la hoja y se pasa a otra
	//e.HasMorePages=false;
	*/
	
	  f.Dispose();
      bf.Dispose();
}
		
	void CmbActivoSelectedIndexChanged(object sender, EventArgs e)
		{
			
		}
	
	
	void Btn_cancelarClick(object sender, EventArgs e)
		{
				// pasar el formulario
         lbl_claveid.Text = "Pulse, NUEVO para Ingresar Datos";
		 LimpiarTextBox(this.Controls);
         //lblSeleccionar.Visible = false;
         //Btn_Ficha.Enabled = false;
		 Btn_Nuevo.Enabled = true;
		 Btn_guardar.Enabled = false;
		 Btn_ruta.Enabled = false;
		 //btnActual.Enabled = false;
		 //cbx_lista.SelectedIndex=0;
		 //cbx_lista.Enabled = false;
		 dateTimePicker1.Value = DateTime.Now;
		 //
		 //btnSelector.Enabled = true;
		 
		}
		
		void CmbRol2SelectedIndexChanged(object sender, EventArgs e)
		{
			sSelectedRol  = (string)cmbRol2.SelectedItem;
			
			if(sSelectedRol.ToString()=="SYSADMIN")
				_isRol=true;
			else
				_isRol=false;
		}
		
		//fin seccion2
		
		
		
		
		
     //input box
	public static class Inputbox
        {

         // Conserva esta cabecera
        //  Emperorxdevil 2007

            static Form f;
            static Label l;
            static TextBox t; // Elementos necesarios
            static Button b1;
            static Button b2;
            static string Valor;

            /// <summary>
            /// Objeto Estático que muestra un pequeño diálogo para introducir datos
            /// </summary>
            /// <param name="title">Título del diálogo</param>
            /// <param name="prompt">Texto de información</param>
            /// <param name="posicion">Posición de inicio</param>
            /// <returns>Devuelve la escrito en la caja de texto como string</returns>
            public static string Show(string title, string prompt, FormStartPosition posicion)
            {

                f = new Form();
                f.Text = title;
                f.ShowIcon = false;
                f.Icon = null;
                f.KeyPreview=true;
                f.ShowInTaskbar = false;
                f.MaximizeBox = false;
                f.MinimizeBox = false;
                f.Width = 200;
                f.FormBorderStyle = FormBorderStyle.FixedDialog;
                f.Height = 120;
                f.StartPosition = posicion;
                f.KeyPress += new KeyPressEventHandler(f_KeyPress);
                
                l = new Label();
                l.AutoSize = true;
                l.Text = prompt;


                //crea textbox y formatea a modalidad password
                t = new TextBox();
                t.PasswordChar='*';
                t.Width = 182;
                t.Top = 40;

                b1 = new Button();
                b1.Text = "Aceptar";
                b1.Click += new EventHandler(b1_Click);


                b2 = new Button();
                b2.Text = "Cancelar";
                b2.Click += new EventHandler(b2_Click);

                f.Controls.Add(l);
                f.Controls.Add(t);
                f.Controls.Add(b1);
                f.Controls.Add(b2);

                l.Top = 10;
                t.Left = 5;
                t.Top = 30;

                b1.Left = 5;
                b1.Top = 60;

                b2.Left = 112;
                b2.Top = 60;

                f.ShowDialog();
                return (Valor);
            }

            static void f_KeyPress(object sender, KeyPressEventArgs e)
            {
                switch(Convert.ToChar(e.KeyChar)) {

                    case ('\r'):
                            Acepta();
                    break;;

                    case (''):
                            Cancela();
                    break;;
                }
            }
            static void b2_Click(object sender, EventArgs e)
            {
                Cancela();
            }
            static void b1_Click(object sender, EventArgs e)
            {
                Acepta();
            }
            private static string Val
            {

                get { return (Valor); }
                set { Valor = value; }
            }
            private static void Acepta() {
                Val = t.Text;
                f.Dispose();
            }
            private static void Cancela() {
                Val=null;
                f.Dispose();
            }


        }	


		
		void Button4Click(object sender, EventArgs e)
		{
			// pasar el formulario
         lbl_claveid.Text = "Pulse, NUEVO para Ingresar Datos";
		 LimpiarTextBox(this.Controls);
         lblSeleccionar.Visible = false;
         Btn_Ficha.Enabled = false;
		 Btn_Nuevo.Enabled = true;
		 Btn_guardar.Enabled = false;
		 Btn_ruta.Enabled = false;
		 btnActual.Enabled = false;
		 //cbx_lista.SelectedIndex=0;
		 cbx_lista.Enabled = false;
		 dateTimePicker1.Value = DateTime.Now;
		 //btnSelector.Enabled = true;
		 
		
         
		}
		
private void CheckKeys(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			
		
		
		if (lbl_claveid.ToString().Length> 0 && Pic_Foto.Image == null	)
		   {
			//Btn_gbr_foto.Enabled=Btn_ruta.Enabled=Btn_guardar.Enabled;
			
		    }
		
				if (lbl_claveid.ToString().Length> 0 && Pic_Foto.Image != null	)
		   {
			//Btn_gbr_foto.Enabled=Btn_ruta.Enabled=Btn_Priv.Enabled=Btn_Ficha.Enabled=Btn_guardar.Enabled;
			
		    }	
			
		}
		
		
		
		
		
		void Lbl_claveidTextChanged(object sender, EventArgs e)
		{
			//this.lbl_claveid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CheckKeys);	
			
			Btn_ruta.Enabled = Btn_guardar.Enabled = lbl_claveid.Text.Length != 0;
		
			
			
		}
		
		
	public static Form IsFormAlreadyOpen(Type FormType)
		{
		   foreach (Form OpenForm in Application.OpenForms)
		   {
		      if (OpenForm.GetType() == FormType)
		         return OpenForm;
		   }
		
		   return null;
		}		
		
		
		
		void Btn_FichaClick(object sender, System.EventArgs e)
		{
			if(Txt_Nombre.Text.Length<=0)
			{
				Txt_Nombre.Focus();
				MessageBox.Show(this,"Nombre","Campo Incompleto",MessageBoxButtons.OK,MessageBoxIcon.Hand);
				return;
			}
			if(!rb_Hombre.Checked && !rb_Mujer.Checked)
			{
				groupBox2.Focus();
				groupBox2.BackColor =Color.Aquamarine;
				MessageBox.Show(this,"Tipo de Genero","Campo Incompleto",MessageBoxButtons.OK,MessageBoxIcon.Hand);
				return;
			}
		    if(Rch_Direccion.Text.Length<=0)
			{
				Rch_Direccion.Focus();
				MessageBox.Show(this,"Dirección","Campo Incompleto",MessageBoxButtons.OK,MessageBoxIcon.Hand);
				return;
			}
			if(Txt_celular.Text.Length<=0)
			{
				Txt_celular.Focus();
				MessageBox.Show(this,"Celular","Campo Incompleto",MessageBoxButtons.OK,MessageBoxIcon.Hand);
				return;
			}
			if(Pic_Foto2.Image == null)
				{
				Btn_ruta.Focus();
				MessageBox.Show(this,"Fotografia","No Hay Imagen",MessageBoxButtons.OK,MessageBoxIcon.Hand);
				return;
			}
			
			PrintDocument PD =new PrintDocument();
			PD.PrintPage += new PrintPageEventHandler( this.imprime_imagen);
				// Allocate a print preview dialog object.
				PrintPreviewDialog dlg = new PrintPreviewDialog();
				//dlg.StartPosition= FormStartPosition.CenterScreen;
				dlg.Document = PD;
				DialogResult result = dlg.ShowDialog();
			
			MessageBox.Show("impresion realizada"); 
		}
		
	
		
		void FormDatosLoad(object sender, EventArgs e)
		{
	
/*
			tabControl1.Selecting += new TabControlCancelEventHandler(tabControl1_Selecting);

   void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
    {
        TabPage current = (sender as TabControl).SelectedTab;
        //validate the current page, to cancel the select use:
        e.Cancel = true;
    }
			
*/			
           

             // OCULTAR UN TABPAGE EN 3 y 4 el de borrar y asignar grupo
             tabControl1.TabPages.Remove(tabPage3);
             
             if(Global_Nombre.GlobalNombre != "SISTEMAS")
             	tabControl1.TabPages.Remove(tabPage4);
            
             
             
			dataGridView1.Rows.Clear();
			dataGridView1.RowsDefaultCellStyle.BackColor = Color.Bisque;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.MediumAquamarine;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;

            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Red;
            //alinear el texto
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToResizeColumns = false;

           
        //agregar boton de eliminar
        DataGridViewButtonColumn dt = new DataGridViewButtonColumn();
                dt.HeaderText = "Borrar";
                dt.Text = "Borrar";
                dt.Name = "Borrar";
                dt.ToolTipText = "Borrar este Empleado";
                dt.Width = 55;
                dt.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(dt);
       /*
        DataGridViewButtonColumn btn_eliminar = new DataGridViewButtonColumn();
		btn_eliminar.Name = "Eliminar"; //este es el identificador del Button
		btn_eliminar.Text = "Boton Eliminar"; //este es el label del button
		btn_eliminar.HeaderText="Eliminar";
		btn_eliminar.Width=120;
		//Esta línea carga la columna “Eliminar” del DataSet
		btn_eliminar.DataPropertyName = "Eliminar";
		dataGridView1.Columns.Insert(2,btn_eliminar);
		*/
         // abrir BAse de datos
		// 	
		enlacedb  dbempleado =new enlacedb();
			    FbConnection cnxempleado= new FbConnection(dbempleado.connectionString);	
  			    cnxempleado.Open();
		      try
				{
					// Open two connections.
				FbCommand mdaEmpleado = new FbCommand("SELECT TBEMPLEADO.IDEMPLEADO, TBEMPLEADO.EMP_NOM FROM TBEMPLEADO WHERE TBEMPLEADO.IDEMPLEADO <> 1 ORDER BY IDEMPLEADO", cnxempleado);
				FbDataAdapter refill = new FbDataAdapter(mdaEmpleado);
				DataTable TodoEmpleado = new DataTable();
				refill.Fill(TodoEmpleado);
				foreach (DataRow dr in TodoEmpleado.Rows) {
					dataGridView1.Rows.Add(dr["IDEMPLEADO"].ToString(),dr["EMP_NOM"].ToString());
				   }
				dataGridView1.Focus();
		        cnxempleado.Close();
		        
     
					}
		
				catch (Exception err1) {
						MessageBox.Show(err1.ToString());
					}	



        	Btn_Ficha.Enabled = false;
		    Btn_guardar.Enabled = false;
		    Btn_ruta.Enabled = false;
		   	btnActual.Enabled = false;
		   	//cmbStatus.Enabled =	false;
		   	//cmbRol.Enabled =	false;
		   	
		   	
		   	
		   	
	      	   
			
		}
		

		
		
		
		
		
		void Txt_NombreTextChanged(object sender, EventArgs e)
		{
			_isDirty = true;
		}
		
		void FormDatosFormClosing(object sender, FormClosingEventArgs e)
		{
			if (_isDirty)
        {
            DialogResult result
                = (MessageBox.Show(
                   "¿Desea Cerrar?"
                   , "Ventana de Captura"
                   , MessageBoxButtons.YesNo
                   , MessageBoxIcon.Question));

            switch (result)
            {
                case DialogResult.Yes:
                    // save the document
                    //SaveMyDocument();
                    
                    break;

                case DialogResult.No:
                    // just allow the form to close
                    // without saving
                    break;

                case DialogResult.Cancel:
                    // cancel the close
                    e.Cancel = true;
                    break;
            }
        }
		}
		
		
		
		void CmbRolSelectedIndexChanged(object sender, EventArgs e)
		{
			sSelectedRol  = (string) cmbRol.SelectedItem;
			
			if(sSelectedRol.ToString()=="SYSADMIN")
				_isRol=true;
			else
				_isRol=false;
			
			
		}
		
		
		
		
		void Btn_BuscarClick(object sender, EventArgs e)
		{
		//ACCESO DE DATABASE
		
		enlacedb  db =new enlacedb();
			    FbConnection conexiongpos= new FbConnection(db.connectionString);	
  			    conexiongpos.Open();
		      try
				{
					// Open two connections.
 				
			  	FbCommand llenargrupos = new FbCommand("SELECT a.ID_EMPLEADO, a.NOM_EMPLEADO FROM REGHUMAN a ORDER BY a.NOM_EMPLEADO" ,conexiongpos);
				FbDataAdapter relgpos = new FbDataAdapter(llenargrupos);
				DataTable losgpos = new DataTable();
				relgpos.Fill(losgpos);
				cbx_lista.Items.Clear();
					foreach (DataRow dr in losgpos.Rows) {
							cbx_lista.Items.Add(dr["ID_EMPLEADO"].ToString()+'-'+dr["NOM_EMPLEADO"].ToString()); 
				   }
				
				//comboBox1.DisplayMember = "GPOPERIODO.ID_GPOPER";
				//comboBox1.ValueMember = "GPOPERIODO.CARRERA_ALUM";
				//comboBox1.DataSource = losgpos;
				
				
				//comboBox1.SelectedIndex=0;
		     	conexiongpos.Close();
		        conexiongpos.Dispose();
				}
			catch (Exception error) {
			    	MessageBox.Show(error.Message); }
			finally { 
			    	// dr.Dispose();
					//	cmd.Dispose();
					//	conn.Dispose();
		    	
			    	conexiongpos.Close(); conexiongpos.Dispose();
		      }
				  				  				
			
		   //ACCESSO DE DATABASE
			
					
              /*
			string n;
			n=Inputbox.Show("INTRODUCIR", "NUMERO DE EMPLEADO", FormStartPosition.CenterScreen);
			if (n==null){return;}
			if (n.Length==0){return;}
			if(!validarmatrix(n)){return;}
			
			string[] splitmatrix = n.Split(new char[] { '-'});
			//if(splitmatrix[1].Length!=0){return;}validar que sea numero con clave de validacion
			string id_matrix =splitmatrix[0];
			string id_code=splitmatrix[1];
						
			    string db5 = conectarbase();
				FbConnection conexiongpos5= ne  w FbConnection(db5);	
  			    
		      try
				{
				conexiongpos5.Open();
				FbCommand da = new FbCommand("SELECT ALUMNOS243.ID_GPOPER7 FROM ALUMNOS243 WHERE ID_MATRIX=@ID_MATRIX AND COD_VERIF=@COD_VERIF", conexiongpos5);
				da.Parameters.Add("@ID_MATRIX",SqlDbType.Char).Value = id_matrix;
				da.Parameters.Add("@COD_VERIF",SqlDbType.Char).Value = id_code;
				FbDataReader reader = da.ExecuteReader();
				bool hasrow= reader.Read();
				if(hasrow) 	{
					string elgrupo=reader["ID_GPOPER7"].ToString();	
					 string su_grupo= elgrupo.Substring(5, 4);
					 MessageBox.Show("         "+su_grupo.ToString(),"Este Alumno(a), Pertenece al Grupo");
					
					
					
					}
				else{
					MessageBox.Show("Alumno no encontrado");
					id_matrix=" ";
					id_code=" ";
					}
				
				//conexiongpos.Dispose();
			}
				catch(Exception err1){
			        	MessageBox.Show(err1.ToString(),"Revisar esta Linea");
					Close();
					}
		}
		
		void Btn_FichaClick(object sender, EventArgs e)
		{
							
			Horarios frmOpen = null;
				if ((frmOpen = (Horarios)IsFormAlreadyOpen(typeof(Horarios))) == null)
					{
						frmOpen = new Horarios();
					    frmOpen.Show();
					}
				else
					{
					
				
					  //  frmOpen.   DoWhatever(); // may be UForm.Select();
					}
			*/				
		}
		
		
				void Btn_cerrarClick(object sender, EventArgs e)
		{
			   this.Close();
		}
		
		
		
	
		
		void Button2Click(object sender, EventArgs e)
		{
			//btnActual.Text = "Guardando...";
            btnActual.Enabled = true;
            Btn_ruta.Enabled = true;
            //btnSelector.Enabled =  false;
            btnActual.Text = "Actualizar";
           //Btn_Nuevo.Enabled = true; 
           // pasar el formulario
           _isDirty = false;
           LimpiarTextBox2();
			
		}
		
		void DataGridView1CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			
		}
		
		void DataGridView1RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
		{
			/*
			 if (e.RowIndex == 2)
    {
        // Calculate the bounds of the row
        int rowHeaderWidth = dataGridView1.RowHeadersVisible ?  dataGridView1.RowHeadersWidth : 0;
        Rectangle rowBounds = new Rectangle( rowHeaderWidth, e.RowBounds.Top, this.dataGridView1.Columns.GetColumnsWidth( DataGridViewElementStates.Visible) – this.dataGridView1.HorizontalScrollingOffset + 1, e.RowBounds.Height);

        // Paint the border
        ControlPaint.DrawBorder(e.Graphics, rowBounds, 
                         Color.Red, ButtonBorderStyle.Solid);

        // Paint the background color
        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.BlanchedAlmond;
    } 
    */
		}
		
		void DataGridView1CellClick(object sender, DataGridViewCellEventArgs e)
		{
		try
            {
                if (!(e.RowIndex < 0 || e.ColumnIndex < 0))
                {
                    DataGridViewButtonCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell;
                    if (cell != null)
                    {
                        string strTest = cell.OwningColumn.HeaderText;
                        if (strTest == "Borrar" || strTest == "Update")
                        {
                            if (strTest == "Borrar")
                            {
                               string NombreID = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString();
                            	DialogResult result;
                            	result = MessageBox.Show("¿Seguro que quieres eliminar, "+NombreID.ToString(), "Borrar Empleado ?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                                if (result == DialogResult.OK)
                                {
                                    string ID = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex - 2].Value.ToString();
                                    int IdClv=Convert.ToInt32(ID);
                                     this.dataGridView1.Rows.RemoveAt(cell.RowIndex);
                                     DepurarEmpleado(IdClv);
                                     
                                     
                                }
                            }
 

 
                        }
                    }
                    dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
                }
 
            }
            catch { }
		}
		
		
		
		void Cbx_Txt_DeptoSelectedIndexChanged(object sender, EventArgs e)
		{
			Txt_Depto.Text=Cbx_Txt_Depto.Text;
		}
		
		void Button5Click(object sender, EventArgs e)
		{
			//ACCESO DE DATABASE
		       enlacedb  dbx =new enlacedb();
			    FbConnection conexiongposx= new FbConnection(dbx.connectionString);	
  			    conexiongposx.Open();
		      try
				{
					// Open two connections.
 				
			  	FbCommand llenargruposx = new FbCommand("SELECT TBEMPLEADO.IDEMPLEADO, TBEMPLEADO.EMP_NOM FROM TBEMPLEADO WHERE (TBEMPLEADO.ACTIVO <> 'NO') AND (TBEMPLEADO.EMP_NIVELUS='SYSADMIN') ORDER BY IDEMPLEADO" ,conexiongposx);
				FbDataAdapter relgposx = new FbDataAdapter(llenargruposx);
				DataTable losgposx = new DataTable();
				relgposx.Fill(losgposx);
			    comboBox2.Items.Clear();
					foreach (DataRow drx in losgposx.Rows) {
							comboBox2.Items.Add(drx["IDEMPLEADO"].ToString()+'-'+drx["EMP_NOM"].ToString()); 
				   }
				//comboBox1.SelectedIndex=0;
		     	conexiongposx.Close();
		        conexiongposx.Dispose();
		        comboBox2.SelectedIndex=0;
				}
			catch (Exception errorx) {
			    	MessageBox.Show(errorx.Message); }
			finally { 
			    	// dr.Dispose();
					//	cmd.Dispose();
					//	conn.Dispose();
		    	
			    	conexiongposx.Close(); conexiongposx.Dispose();
		      }
				  				  				
			
		   //ACCESSO DE DATABASE
		}
		
		void ComboBox2SelectedIndexChanged(object sender, EventArgs e)
		{
			Global_ip.Globalip="";
			lbl_RolNombre.Text=comboBox2.Text;
			string[] words2x = comboBox2.Text.Split(new char[] { '-'});
		     string apuntareg2x =words2x[0];
		     Global_ip.Globalip=apuntareg2x;
		
		}
		
		void ComboBox1SelectedIndexChanged(object sender, EventArgs e)
		{
			lbl_TipoRol.Text=comboBox1.Text;
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			
		}
		
		void Button6Click(object sender, EventArgs e)
		{
			  

			try
			{
			enlacedb db3x = new enlacedb();
					FbConnection  conexion3x = new FbConnection(db3x.connectionString);
			        conexion3x.Open();
					FbCommand cmdsesion3x = new FbCommand("UPDATE TBEMPLEADO SET TBEMPLEADO.PERMISOGPO=@PERMISOGPO where TBEMPLEADO.IDEMPLEADO=@IDEMPLEADO" ,conexion3x);
					cmdsesion3x.Parameters.AddWithValue("@IDEMPLEADO",Global_ip.Globalip);
					cmdsesion3x.Parameters.AddWithValue("@PERMISOGPO",lbl_TipoRol.Text.Trim());
				
					int registrox=cmdsesion3x.ExecuteNonQuery();
				  if(registrox==1){
					MessageBox.Show(this,"Actualizado...","Rol de Grupo",MessageBoxButtons.OK,MessageBoxIcon.None);
					
                    }
					cmdsesion3x.Dispose();
						
			}
						
				catch(Exception err1m){
			        	MessageBox.Show(err1m.ToString(),"Revisar esta Linea");
					Close();
			         }	
			Global_ip.Globalip="";
		}
	}
}
