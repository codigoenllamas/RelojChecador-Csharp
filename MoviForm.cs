/*
 * Created by SharpDevelop.
 * User: pcaula
 * Date: 08/07/2013
 * Time: 01:17 a.m.
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
using Microsoft.Office.Interop.Excel;
using System.Net;
using System.Runtime.InteropServices;
using System.Drawing.Printing;


namespace PersonalNet
{
	/// <summary>
	/// Description of ReporteTodos.
	/// </summary>
	public partial class MoviForm : Form
	{
		
		public System.Data.DataTable dt=null; 
		private int idEmple = 0;
		
		
		public MoviForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
	////

	/// DataGridView que contiene los datos a exportar

	
    ///	
		
	//llamar otra winform y devolver el estado anterior
				
	public static Form IsFormAlreadyOpen(Type FormType)
		{
		   foreach (Form OpenForm in System.Windows.Forms.Application.OpenForms)
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
	
		
		
		
		
		void ReporteTodosLoad(object sender, EventArgs e)
		{
			try {
		enlacedb db = new enlacedb();
		FbConnection conexion = new FbConnection(db.connectionString);
			
				//string cmatricula
				conexion.Open();
				dt = new System.Data.DataTable("EMP_NOM");
				// Load Data into DataGrid
				FbDataAdapter da=new FbDataAdapter("SELECT IDEMPLEADO, EMP_NOM FROM TBEMPLEADO ORDER BY IDEMPLEADO",conexion);
				da.Fill(dt);
				foreach (DataRow drw in dt.Rows) {
					CmbEmpleado.Items.Add(drw["IDEMPLEADO"].ToString()+" | "+(drw["EMP_NOM"].ToString()).ToString());
				}
				
			
			}
			catch (Exception err) {
						MessageBox.Show(err.ToString());
					}
			CmbEmpleado.SelectedIndex=0;
			//ClearControl();
		}
		
		void BtnSalirClick(object sender, EventArgs e)
		{
			this.Close();
		}
		
		void BtnBorrarClick(object sender, EventArgs e)
		{
			DgvMovimiento.Rows.Clear();
		}
		
		void BtnBuscarClick(object sender, EventArgs e)
		{
			DgvMovimiento.Rows.Clear();
        int idEmp =int.Parse(Global_ip.Globalip.ToString());			
     	string  fecha_ini=(string)dateTimePicker1.Value.ToString("dd.MM.yyyy");
     	string fecha_fin=(string)dateTimePicker2.Value.ToString("dd.MM.yyyy");
     	FbParameter f1 =new FbParameter();
     	FbParameter f2 =new FbParameter();
     	FbParameter f3 =new FbParameter();
     	
     	enlacedb db3 = new enlacedb();
	    FbConnection conexiongpos= new FbConnection(db3.connectionString);	
     try{
		
		DataSet ds = new DataSet();
  		string tiporeporte=	"REPORTADO";
   		FbCommand SqlAlumno = new FbCommand("SELECT REGIS_DIARIO.IDCONTROL, REGIS_DIARIO.IDEMPLEADO, cast(REGIS_DIARIO.CHK_FECHA as date), TBEMPLEADO.EMP_NOM, REGIS_DIARIO.CONTROL, REGIS_DIARIO.MOVIMIENTO, CASE  WHEN cast(left(cast(CHK_FECHA AS TIME),2) AS INTEGER)>12      THEN cast(cast(left(cast(CHK_FECHA AS TIME),2) AS INTEGER)-12 AS VARCHAR(2)) || cast(right(left(cast(CHK_FECHA AS TIME),5),3) AS VARCHAR(3))||'-PM' ELSE cast(cast(left(cast(CHK_FECHA AS TIME),2) AS INTEGER) AS VARCHAR(2)) || cast(right(left(cast(CHK_FECHA AS TIME),5),3) AS VARCHAR(3))||'-AM' END as Tiempo, extract (weekday from REGIS_DIARIO.CHK_FECHA) FROM REGIS_DIARIO  inner join TBEMPLEADO ON TBEMPLEADO.IDEMPLEADO=REGIS_DIARIO.IDEMPLEADO WHERE REGIS_DIARIO.IDEMPLEADO=@f3 AND CAST(REGIS_DIARIO.CHK_FECHA AS DATE) between CAST(@f1 AS DATE) AND  CAST(@f2 AS DATE) ORDER BY REGIS_DIARIO.IDCONTROL",conexiongpos);
  		f1 = SqlAlumno.Parameters.Add("@F1", SqlDbType.VarChar);
		f2 = SqlAlumno.Parameters.Add("@F2", SqlDbType.VarChar);
		f3 = SqlAlumno.Parameters.Add("@F3", SqlDbType.Int);
		
        f1.Value =fecha_ini;
        f2.Value =fecha_fin;
        f3.Value = idEmp;
        
		conexiongpos.Open();
		FbDataReader leeralumnos = SqlAlumno.ExecuteReader(CommandBehavior.SequentialAccess);
		int num=1;
		while (leeralumnos.Read()) {
		     string diadelasemana=" ";
			switch (leeralumnos.GetString(7).Trim()) {
				case "0" : diadelasemana="DOM";
						break;
				case "1" : diadelasemana="LUN";
						break;		
				case "2" : diadelasemana="MAR";
						break;		
				case "3" : diadelasemana="MIE";
						break;		
				case "4" : diadelasemana="JUE";
						break;		
				case "5" : diadelasemana="VIE";
						break;		
				case "6" : diadelasemana="SAB";
						break;		
				
			}
			
		     DgvMovimiento.Rows.Add(leeralumnos.GetString(0).ToString(),leeralumnos.GetString(2).Trim().Substring(0,10),diadelasemana.ToString(),leeralumnos.GetString(3).Trim(),leeralumnos.GetString(5).Trim(),leeralumnos.GetString(6).Trim());
				  num++;
			    }//fin while
		leeralumnos.Dispose();
		conexiongpos.Close();
	   }
  		 catch (Exception err1) {
				MessageBox.Show(err1.ToString());
			}

        	
		}
		
		
		
		void ComboBox1SelectedIndexChanged(object sender, EventArgs e)
		{
			string[] wmod = CmbEmpleado.Text.Split(new char[] { '|'});
				//lbl_modulo.Text= wmod[1];
				idEmple = int.Parse(wmod[0].ToString());
				Global_ip.Globalip= wmod[0].ToString();
			
		}
		
	
		
		void CmbEmpleadoSelectedIndexChanged(object sender, EventArgs e)
		{
			string[] wmod = CmbEmpleado.Text.Split(new char[] { '|'});
				//lbl_modulo.Text= wmod[1];
				idEmple = int.Parse(wmod[0].ToString());
				Global_ip.Globalip= wmod[0].ToString();
		}
		
		void MoviFormLoad(object sender, EventArgs e)
		{
//agregar boton de Actualizar
        DataGridViewButtonColumn dtw = new DataGridViewButtonColumn();
                dtw.HeaderText = "Update";
                dtw.Text = "Update";
                dtw.Name = "Update";
                dtw.ToolTipText = "Actualizar este Empleado";
                dtw.Width = 55;
                dtw.UseColumnTextForButtonValue = true;
                DgvMovimiento.Columns.Add(dtw);

			try {
		enlacedb db = new enlacedb();
		FbConnection conexion = new FbConnection(db.connectionString);
			
				//string cmatricula
				conexion.Open();
				dt = new System.Data.DataTable("EMP_NOM");
				// Load Data into DataGrid
				FbDataAdapter da=new FbDataAdapter("SELECT IDEMPLEADO, EMP_NOM FROM TBEMPLEADO ORDER BY IDEMPLEADO",conexion);
				da.Fill(dt);
				foreach (DataRow drw in dt.Rows) {
					CmbEmpleado.Items.Add(drw["IDEMPLEADO"].ToString()+" | "+(drw["EMP_NOM"].ToString()).ToString());
				}
				
			
			}
			catch (Exception err) {
						MessageBox.Show(err.ToString());
					}
			CmbEmpleado.SelectedIndex=0;
			//ClearControl();
		}
		
		void DgvMovimientoCellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			try
            {
                if (!(e.RowIndex < 0 || e.ColumnIndex < 0))
                {
                    DataGridViewButtonCell cell = DgvMovimiento.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell;
                    if (cell != null)
                    {
                        string strTest = cell.OwningColumn.HeaderText;
                        if (strTest == "Borrar" || strTest == "Update")
                        {
                            if (strTest == "Update")
                            {
                               //leer CELDA datagridview la columna(CTRL) 1. ES EL NUM 6, COLUMNA( 2 EL NUMERO 5, COLUMNA 3 EL NUMERO 4
                               
                            	string NombreID = DgvMovimiento.Rows[e.RowIndex].Cells[e.ColumnIndex - 3].Value.ToString();
                            	string ColCtrl = DgvMovimiento.Rows[e.RowIndex].Cells[e.ColumnIndex - 6].Value.ToString();
                                string ColFecha = DgvMovimiento.Rows[e.RowIndex].Cells[e.ColumnIndex - 5].Value.ToString();
                                string  ColDia = DgvMovimiento.Rows[e.RowIndex].Cells[e.ColumnIndex - 4].Value.ToString();
                                string ColMov = DgvMovimiento.Rows[e.RowIndex].Cells[e.ColumnIndex - 2 ].Value.ToString();
                                string ColHora = DgvMovimiento.Rows[e.RowIndex].Cells[e.ColumnIndex - 1 ].Value.ToString();
                                       
                            	
                            	DialogResult result;
                            	result = MessageBox.Show("¿Seguro de Actualizar, "+NombreID.ToString(), "Actualizar Empleado ?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                                if (result == DialogResult.OK)
                                {
                                	
                                	string[] TiempoCheco = ColHora.Split(new char[] { '-'});
									String THora=Convert.ToDateTime(TiempoCheco[0]).ToString("HH");
									String TMin=Convert.ToDateTime(TiempoCheco[0]).ToString("mm");	
									ClassTime.GlobalHora=THora.ToString();
									ClassTime.GlobalMin=TMin.ToString();
									ClassTime.GlobalIdEmple=NombreID;
                                	ClassTime.GlobalReg=ColCtrl;
                                   // int IdClv=Convert.ToInt32(ID);
                                    // this.DgvMovimiento.Rows.RemoveAt(cell.RowIndex);
                                     
                                    // MessageBox.Show("Modulo en Mantto. para Actualizar...");
                                    DgvMovimiento.Rows.Clear();
                                     Form newForm = new FormHora();
									  newForm.Owner= this;
						  			   newForm.Show();
						  				
                                    
										
										
                                     
                                }
                            }
 

 
                        }
                    }
                    DgvMovimiento.EditMode = DataGridViewEditMode.EditOnEnter;
                }
 
            }
            catch { }
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			
		}
		
		void DgvMovimientoCellClick(object sender, DataGridViewCellEventArgs e)
		{
			 if (DgvMovimiento.CurrentCell.ColumnIndex.Equals(1) && e.RowIndex != -1){
        if (DgvMovimiento.CurrentCell != null && DgvMovimiento.CurrentCell.Value != null)
            MessageBox.Show(DgvMovimiento.CurrentCell.Value.ToString());
                    	
		}
		}
	}
}
