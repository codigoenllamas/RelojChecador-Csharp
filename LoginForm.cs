using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;

namespace PersonalNet
{
   public partial class LoginForm : Form
    {
        //private ILoginFormParent _parent;
        //private DatabaseInterface _dbInterface;
        //private String _requestedRole;

        public LoginForm(/*ILoginFormParent parent, DatabaseInterface dbInterface, String requestedRole*/)
        {
            InitializeComponent();
            //_parent = parent;
            //_dbInterface = dbInterface;
           // _requestedRole = requestedRole;
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
 
        
        
        
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {                     
          
        	
			enlacedb dblogin = new enlacedb();	
        	//FbCommand cmdCheck = new FbCommand("CHECK_DIA",new FbConnection(dblogin.connectionString));
			//	cmdCheck.CommandType= CommandType.StoredProcedure;
        	
        if(userID.Text.Length>=1 && password.Text.Length>=4){
			
		  DateTime sesionhoy=DateTime.Today;
			DateTime horaentrada=DateTime.Now;
			Global_Nombre.GlobalNombre="";
			  try
				{
	            
			            FbConnection conexionLogin= new FbConnection(dblogin.connectionString);
		                 conexionLogin.Open();
			  			FbCommand chekarLogin= new FbCommand("SELECT a.IDEMPLEADO, a.EMP_PWD, a.EMP_DEPTO FROM TBEMPLEADO a WHERE a.IDEMPLEADO=@IDEMPLEADO and a.EMP_PWD=@EMP_PWD" ,conexionLogin);
						chekarLogin.Parameters.Add("@IDEMPLEADO",FbDbType.VarChar).Value=userID.Text;
						chekarLogin.Parameters.Add("@EMP_PWD",FbDbType.VarChar).Value=password.Text;
						FbDataReader readerlogin = chekarLogin.ExecuteReader();
						bool chkRowPsp= readerlogin.Read();
     		                    if(chkRowPsp) 
		                          {
										//detectar		
										//logear el acceso del usuario total 		11:11 p.m. 10/02/2014	
										 lblBandera.Text="1";
										 if (userID.Text=="1")
										 {
										 	Global_Nombre.GlobalNombre=readerlogin.GetString(2);
										 }
         		                    }
          
               
                //MessageBox.Show("Login Succesful",lblBandera.Text);
                this.Close();            
           }
			  	catch (Exception err2) {
						MessageBox.Show(err2.ToString());
				}		  
			  
	}
			
			
            //else
            //{
            //    MessageBox.Show("Credentials Invalid");
           // }
                  
        }

        
    }
}
