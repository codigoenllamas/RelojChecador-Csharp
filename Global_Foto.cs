/*
 * Created by SharpDevelop.
 * User: pcaula
 * Date: 09/09/2013
 * Time: 10:55 p.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing.Imaging;

namespace PersonalNet
{
	/// <summary>
	/// Description of Global_Foto.
	/// </summary>
	public class Global_Foto
	{
		
		private static byte[] m_globalbyteArray=null;
      public static byte[] GlobalFoto
		{
      	 get { return m_globalbyteArray; }
         set { m_globalbyteArray = value; }
		}
	}
}




