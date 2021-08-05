using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Drawing;
using System.IO;
using System.Data;
using System.Reflection;
using FirebirdSql.Data.FirebirdClient;




namespace PersonalNet
{
    public class Util
    {
	  FbException Err2;
        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public static byte[] LoadImage(string FilePath)
        {
            try
            {
                FileStream fs = new FileStream(FilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                byte[] Image = new byte[fs.Length];
                fs.Read(Image, 0, Convert.ToInt32(fs.Length));
                fs.Close();
                return Image;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Image CropImage(Bitmap bmpSource, int width, int height)
        {
            try
            {
                Rectangle cropRect = new Rectangle(0, 0, width, height);


                //resize
                Bitmap _resizedImg = new Bitmap(width, height);
                Graphics g = Graphics.FromImage(_resizedImg);
                g.DrawImage(bmpSource, cropRect, new Rectangle(0, 0, bmpSource.Width, bmpSource.Height), GraphicsUnit.Pixel);

                //crop
                Bitmap _CropImg = new Bitmap(270, height - 10);
                g = Graphics.FromImage(_CropImg);
                g.DrawImage(_resizedImg, cropRect, new Rectangle(18, 10, width, height), GraphicsUnit.Pixel);

                return (Image)_CropImg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetConfigFileName
        {
            get
            {
                return "zsi.PhotoFingCapture.exe.config";
            }
        }
 
        public static string GetWebServiceURL
        {
            get
            {

                XmlDocument _doc;
                XmlNode _node;
                string AppConfigFile = Util.GetConfigFileName;
                _doc = new XmlDocument();
                _doc.Load(AppConfigFile);
                XmlNodeList _nodes;
                _nodes = _doc.SelectNodes("//applicationSettings");
                _node = _nodes.Item(0).ChildNodes.Item(0).ChildNodes.Item(0).ChildNodes.Item(0);
                return _node.InnerText;
            }
        }


        public static byte[] StreamToByte(System.IO.Stream stream)
        {
            long originalPosition = stream.Position;
            stream.Position = 0;

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                stream.Position = originalPosition;
            }
        }
        
        // Mis Recursos
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


        private void submitTemplate()

        {
            MemoryStream ms = new MemoryStream();

           // _enroller.Template.Serialize(ms);

            byte[] data = new byte[ms.Length];

            ms.Position = 0;

            ms.Read(data, 0, (int)ms.Length);

            submitTemplateForProcessing("1", data);            

        }


public void submitTemplateForProcessing(String workstationID, byte[] templateData)

        {
bool bandera = false;
	if (/* enlacedb() == null */ !bandera )

                return;

            try

            {

             //   SQLiteConnection cnn = new SQLiteConnection(sqliteConnectionString);

                //cnn.Open();



               // SQLiteParameter pUsername = new SQLiteParameter("@userID", DbType.String);

               // pUsername.Value = username;



              //  SQLiteParameter pWorkstationID = new SQLiteParameter("@workstationID", DbType.String);

                //pWorkstationID.Value = workstationID;



                //SQLiteParameter pTemplateData = new SQLiteParameter("@fingerprintTemplate", DbType.Binary);

                //pTemplateData.Value = templateData;



                String query = "INSERT INTO ToProcess (userID, workstationID, fingerprintTemplate)";

                query += " VALUES (@userID, @workstationID, @fingerprintTemplate)";



                //SQLiteCommand insertCommand = new SQLiteCommand(query, cnn);

                //insertCommand.Parameters.Add(pUsername);

                //insertCommand.Parameters.Add(pWorkstationID);

                //insertCommand.Parameters.Add(pTemplateData);              

                //insertCommand.ExecuteNonQuery();



                //cnn.Clone();

            }

            catch (Exception err)

            {

                Console.WriteLine(err.ToString());

            }

        }

        
public static bool UpdateDataPeople(string IdPeople, byte[] DedoBinario)
{
	int IdNumero = 0;		
			
			try
				{
				IdNumero= int.Parse(IdPeople);
				enlacedb db = new enlacedb();
				FbCommand cmdinciden = new FbCommand("UPDATE_HUELLA",new FbConnection(db.connectionString));
				cmdinciden.CommandType= CommandType.StoredProcedure;
				cmdinciden.Parameters.Add("@IDEMPLEADO", FbDbType.Integer).Value =IdNumero;
				cmdinciden.Parameters.Add("@EMP_DBHUELLA",FbDbType.Binary).Value =DedoBinario;
                cmdinciden.Connection.Open();
				FbTransaction transx5=cmdinciden.Connection. BeginTransaction();
				cmdinciden.Transaction=transx5;
				cmdinciden.ExecuteNonQuery();
				transx5.Commit();
				transx5.Dispose();
				cmdinciden.Connection.Close(); 
               return true;
						
			}
			
				catch (Exception ex ) {
					//	MessageBox.Show(err2.ToString());
					
					 throw ex;
				}
			//return false;
			
	}	


public static byte[] imageToByteArray(System.Drawing.Image imageIn)
{
 MemoryStream ms = new MemoryStream();
 imageIn.Save(ms,System.Drawing.Imaging.ImageFormat.Gif);
 return  ms.ToArray();
}
           
        
        
        
    }
}
