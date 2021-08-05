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



namespace PersonalNet
{
	/// <summary>
	/// Description of ReporteTodos.
	/// </summary>
	public partial class ReporteTodos : Form
	{
		
		public System.Data.DataTable dt=null; 
		private int idEmple = 0;
		
		
		public ReporteTodos()
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

	public static Form IsFormAlreadyOpen(Type FormType)
{
   foreach (Form OpenForm in System.Windows.Forms.Application.OpenForms)
   {
      if (OpenForm.GetType() == FormType)
         return OpenForm;
   }

   return null;
}		

private void MergeCellsInRow(int row, int col1, int col2)
{
    Graphics g = dgv.CreateGraphics();
    Pen p = new Pen(dgv.GridColor);
    System.Drawing.Rectangle r1 = dgv.GetCellDisplayRectangle(col1, row, true);
    System.Drawing.Rectangle r2 = dgv.GetCellDisplayRectangle(col2, row, true);
 
    int recWidth = 0;
    string recValue = string.Empty;
    for (int i = col1; i <= col2; i++)
    {
    recWidth += dgv.GetCellDisplayRectangle(i, row, true).Width;
    if (dgv[i, row].Value != null)
        recValue += dgv[i, row].Value.ToString() + " ";
    }
    System.Drawing.Rectangle newCell = new System.Drawing.Rectangle(r1.X, r1.Y, recWidth, r1.Height);
    g.FillRectangle(new SolidBrush(dgv.DefaultCellStyle.BackColor), newCell);
    g.DrawRectangle(p, newCell);
    g.DrawString(recValue, dgv.DefaultCellStyle.Font, new SolidBrush(dgv.DefaultCellStyle.ForeColor), newCell.X + 3, newCell.Y + 3);
}
 
private void MergeCellsInColumn(int col, int row1, int row2)
{
    Graphics g = dgv.CreateGraphics();
    Pen p = new Pen(dgv.GridColor);
    System.Drawing.Rectangle r1 = dgv.GetCellDisplayRectangle(col, row1, true);
    System.Drawing.Rectangle r2 = dgv.GetCellDisplayRectangle(col, row2, true);
 
    int recHeight = 0;
    string recValue = string.Empty;
    for (int i = row1; i <= row2; i++)
    {
    recHeight += dgv.GetCellDisplayRectangle(col, i, true).Height;
    if (dgv[col, i].Value != null)
        recValue += dgv[col, i].Value.ToString() + " ";
    }
    System.Drawing.Rectangle newCell = new System.Drawing.Rectangle(r1.X, r1.Y, r1.Width, recHeight);
    g.FillRectangle(new SolidBrush(dgv.DefaultCellStyle.BackColor), newCell);
    g.DrawRectangle(p, newCell);
    g.DrawString(recValue, dgv.DefaultCellStyle.Font, new SolidBrush(dgv.DefaultCellStyle.ForeColor), newCell.X + 3, newCell.Y + 3);
}	
	
private void sacarfoto(int idFoto)
{

		enlacedb dbsearch3 = new enlacedb();
		FbConnection cnxsearch3 = new FbConnection(dbsearch3.connectionString);
			try {
				
				cnxsearch3.Open();
				FbCommand mdafoto3 = new FbCommand("SELECT a.IDEMPLEADO, a.EMP_FOTO FROM TBEMPLEADO a WHERE IDEMPLEADO=@IDEMPLEADO", cnxsearch3);
				mdafoto3.Parameters.Add("@IDEMPLEADO",SqlDbType.VarChar).Value = idFoto.ToString();
					FbDataReader reader3 = mdafoto3.ExecuteReader();
			                  	//reader.HasRows
			                bool hasrow3 = reader3.Read();
					if(hasrow3)
 	                            {
					     
  			                	Byte[] img3 = new Byte[Convert.ToInt32 ((reader3.GetBytes(1, 0,null, 0, Int32.MaxValue)))]; 
								reader3.GetBytes(1, 0, img3, 0, img3.Length);	
								FileStream fs3 = new FileStream (@"c:\\reloj2021\\txps.jpg", FileMode.Create, FileAccess.ReadWrite); 
								for(int i=0;i<img3.Length;i++)
							        fs3.WriteByte(img3[i]);
								fs3.Close();
								MemoryStream ms3 = new MemoryStream();
								ms3.Write(img3,0,img3.GetUpperBound(0)+1);
								//pic_docente
								
								ms3.Close();
								
			                  	}
			                  			
			           
			        cnxsearch3.Close();
			        
					}
		
				catch (Exception err1) {
						MessageBox.Show(err1.ToString());
					}

}
	
	/// DataGridView que contiene los datos a exportar
private void ExportarDataGridViewExcel(DataGridView grd)
{
    SaveFileDialog fichero = new SaveFileDialog();
    fichero.Filter = "Excel (*.xls)|*.xls";
    if (fichero.ShowDialog() == DialogResult.OK)
    {
        Microsoft.Office.Interop.Excel.Application aplicacion;
        Microsoft.Office.Interop.Excel.Workbook libros_trabajo;
        Microsoft.Office.Interop.Excel.Worksheet hoja_trabajo;
        aplicacion = new Microsoft.Office.Interop.Excel.Application();
        libros_trabajo = aplicacion.Workbooks.Add();
        hoja_trabajo = 
            (Microsoft.Office.Interop.Excel.Worksheet)libros_trabajo.Worksheets.get_Item(1);
        //Recorremos el DataGridView rellenando la hoja de trabajo
        for (int i = 0; i < grd.Rows.Count - 1; i++)
        {
            for (int j = 0; j < grd.Columns.Count; j++)
            {
                hoja_trabajo.Cells[i + 1, j + 1] = grd.Rows[i].Cells[j].Value.ToString();
            }
        }
        libros_trabajo.SaveAs(fichero.FileName, 
            Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);
        libros_trabajo.Close(true);
        aplicacion.Quit();
    }
}
	
    ///	
		
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
         //dgv.AutoGenerateColumns = false;

			try {
		enlacedb db = new enlacedb();
		FbConnection conexion = new FbConnection(db.connectionString);
			
				//string cmatricula
				conexion.Open();
				dt = new System.Data.DataTable("EMP_NOM");
				// Load Data into DataGrid
				FbDataAdapter da=new FbDataAdapter("SELECT TBEMPLEADO.IDEMPLEADO, TBEMPLEADO.EMP_NOM FROM TBEMPLEADO WHERE TBEMPLEADO.ACTIVO <> 'NO' ORDER BY IDEMPLEADO",conexion);
				da.Fill(dt);
				foreach (DataRow drw in dt.Rows) {
					comboBox1.Items.Add(drw["IDEMPLEADO"].ToString()+" | "+(drw["EMP_NOM"].ToString()).ToString());
				}
				
			
			}
			catch (Exception err) {
						MessageBox.Show(err.ToString());
					}
			comboBox1.SelectedIndex=0;
			//ClearControl();
		}
		
		void BtnSalirClick(object sender, EventArgs e)
		{
			this.Close();
		}
		
		void BtnBorrarClick(object sender, EventArgs e)
		{
			dgv.Rows.Clear();
		}
		
		void BtnBuscarClick(object sender, EventArgs e)
		{
			dgv.Rows.Clear();
        int idEmp =int.Parse(Global_ip.Globalip.ToString());			
     	string  fecha_ini=(string)dateTimePicker1.Value.ToString("dd.MM.yyyy");
     	string fecha_fin=(string)dateTimePicker2.Value.ToString("dd.MM.yyyy");
     	FbParameter f1 =new FbParameter();
     	FbParameter f2 =new FbParameter();
     	FbParameter f3 =new FbParameter();
     	//FbParameter f4 =new FbParameter();
     	enlacedb db3 = new enlacedb();
	    FbConnection conexiongpos= new FbConnection(db3.connectionString);	
     try{
		
		DataSet ds = new DataSet();
  		string tiporeporte=	"REPORTADO";
   		FbCommand SqlAlumno = new FbCommand("SELECT REGIS_DIARIO.IDEMPLEADO, cast(REGIS_DIARIO.CHK_FECHA as date), TBEMPLEADO.EMP_NOM, REGIS_DIARIO.CONTROL, REGIS_DIARIO.MOVIMIENTO, CASE  WHEN cast(left(cast(CHK_FECHA AS TIME),2) AS INTEGER)>12      THEN cast(cast(left(cast(CHK_FECHA AS TIME),2) AS INTEGER)-12 AS VARCHAR(2)) || cast(right(left(cast(CHK_FECHA AS TIME),5),3) AS VARCHAR(3))||'-PM' ELSE cast(cast(left(cast(CHK_FECHA AS TIME),2) AS INTEGER) AS VARCHAR(2)) || cast(right(left(cast(CHK_FECHA AS TIME),5),3) AS VARCHAR(3))||'-AM' END as Tiempo, extract (weekday from REGIS_DIARIO.CHK_FECHA), TBEMPLEADO.EMP_FOTO FROM REGIS_DIARIO  inner join TBEMPLEADO ON TBEMPLEADO.IDEMPLEADO=REGIS_DIARIO.IDEMPLEADO WHERE REGIS_DIARIO.IDEMPLEADO=@f3 AND CAST(REGIS_DIARIO.CHK_FECHA AS DATE) between CAST(@f1 AS DATE) AND  CAST(@f2 AS DATE)  ORDER BY REGIS_DIARIO.CHK_FECHA",conexiongpos);
  		f1 = SqlAlumno.Parameters.Add("@F1", SqlDbType.VarChar);
		f2 = SqlAlumno.Parameters.Add("@F2", SqlDbType.VarChar);
		f3 = SqlAlumno.Parameters.Add("@F3", SqlDbType.Int);
		//f4 = SqlAlumno.Parameters.Add("@F4", SqlDbType.VarChar);
		
        f1.Value =fecha_ini;
        f2.Value =fecha_fin;
        f3.Value = idEmp;
        //f4.Value = Global_Nombre.GlobalNombre;
        
		conexiongpos.Open();
		FbDataReader leeralumnos = SqlAlumno.ExecuteReader(CommandBehavior.SequentialAccess);
		int num=1;
				  
		while (leeralumnos.Read()) {
		    string diadelasemana=" ";
			switch (leeralumnos.GetString(6).Trim()) {
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
		  
               	
		dgv.Rows.Add(leeralumnos.GetString(1).Trim().Substring(0,10),diadelasemana.ToString(),leeralumnos.GetString(2).Trim(),leeralumnos.GetString(4).Trim(),leeralumnos.GetString(5).Trim());
				  num++;
			    }//fin while
		dgv.AllowUserToAddRows = false;
		//dgv.CellBorderStyle = DataGridViewCellBorderStyle.None;
		leeralumnos.Dispose();
		conexiongpos.Close();
		sacarfoto(idEmp);
                         
	   }
  		 catch (Exception err1) {
				MessageBox.Show(err1.ToString());
			}

           
		}
		
		void BtnImprimirClick(object sender, EventArgs e)
		{
				 // Calling DataGridView Printing
            PrintDGV.Print_DataGridView(dgv);
		}
		
		void ComboBox1SelectedIndexChanged(object sender, EventArgs e)
		{
			string[] wmod = comboBox1.Text.Split(new char[] { '|'});
				//lbl_modulo.Text= wmod[1];
				idEmple = int.Parse(wmod[0].ToString());
				Global_ip.Globalip= wmod[0].ToString();
			
		}
		
		void Button1Click(object sender, EventArgs e)
		{
		
			//Exportar Datos
			dgv.Rows.Clear();
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
				System.Data.DataTable dt3=new System.Data.DataTable();
		  		//string tiporeporte=	"REPORTADO";
		  		
		FbCommand SqlExport = new FbCommand("SELECT REGIS_DIARIO.IDCONTROL, REGIS_DIARIO.IDEMPLEADO, TBEMPLEADO.EMP_NOM, REGIS_DIARIO.DIA," +
		"cast(EXTRACT(year FROM REGIS_DIARIO.CHK_FECHA)||'/'|| EXTRACT(month FROM REGIS_DIARIO.CHK_FECHA)||'/'|| EXTRACT(day FROM REGIS_DIARIO.CHK_FECHA) AS Date), REGIS_DIARIO.MOVIMIENTO,REGIS_DIARIO.CHK_HORA "+
		 	 "FROM REGIS_DIARIO  " +
		     "inner join TBEMPLEADO ON TBEMPLEADO.IDEMPLEADO=REGIS_DIARIO.IDEMPLEADO " +
		     "WHERE REGIS_DIARIO.IDEMPLEADO=@f3 AND CAST(REGIS_DIARIO.CHK_FECHA AS DATE) " +
		     "between CAST(@f1 AS DATE) AND  CAST(@f2 AS DATE) " +
		     "ORDER BY REGIS_DIARIO.IDCONTROL",conexiongpos);
		 	f1 = SqlExport.Parameters.Add("@F1", SqlDbType.VarChar);
			f2 = SqlExport.Parameters.Add("@F2", SqlDbType.VarChar);
			f3 = SqlExport.Parameters.Add("@F3", SqlDbType.Int);
				
		        f1.Value =fecha_ini;
		        f2.Value =fecha_fin;
		        f3.Value = idEmp;
					
			conexiongpos.Open();
			FbDataReader leerchekeo = SqlExport.ExecuteReader(CommandBehavior.SequentialAccess);
			
			if (leerchekeo.HasRows) {
						dt3.Load(leerchekeo);
						leerchekeo.Close();
							var result = new StringBuilder();
							for (int i = 0; i < dt3.Columns.Count; i++)
							{
							    result.Append(dt3.Columns[i].ColumnName);
							    result.Append(i == dt3.Columns.Count - 1 ? "\n" : ",");
							}
							foreach (DataRow row in dt3.Rows)
							{
							    for (int i = 0; i < dt3.Columns.Count; i++)
							    {
							        //aqui recibe los datos
							    	result.Append(row[i].ToString());
							        result.Append(i == dt3.Columns.Count - 1 ? "\n" : ",");
							    }
							}
							//MessageBox.Show("Aqui save Archivo");
				SaveFileDialog saveFileDialog1 = new SaveFileDialog();
		        saveFileDialog1.Filter = "Datos CSV (*.csv)|*.csv|All files (*.*)|*.*";            
		        saveFileDialog1.RestoreDirectory = true;
		        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
		        {
		               File.WriteAllText(saveFileDialog1.FileName, result.ToString());
		        }    
							
				
					}
			     
					
		}// Exportar
		
  
  		 catch (Exception err1) {
				MessageBox.Show(err1.ToString());
			}	 	
			
			
         
		}// fin exportar datos
		
		
		void Button2Click(object sender, EventArgs e)
		{
			 
				  	            Reportex frmOpen = null;
											if ((frmOpen = (Reportex)IsFormAlreadyOpen(typeof(Reportex))) == null)
												{
									 			frmOpen = new Reportex();
									 			//frmOpen.xuser_psp.Text=txtUser.Text;
									 			//frmOpen.lbl_iduser.Text=txtUser.Text;
									 			//frmOpen.PicPsp.Image = (Bitmap) Bitmap.FromStream(new System.IO.MemoryStream(img, true),true);
							  	           	    //frmOpen.label5.Text=Nom_Docente;
							  	           	    //txt_pwd.Text=" ";
				  	            				//txtUser.Text=" ";
							  	           	    frmOpen.Show();
												
												}
		}
	}
}
