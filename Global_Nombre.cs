/*
 * Created by SharpDevelop.
 * User: pcaula
 * Date: 14/07/2013
 * Time: 01:38 a.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace PersonalNet
{
	/// <summary>
	/// Description of Global_Nombre.
	/// </summary>
	public class Global_Nombre
	{
		private static string m_globalNombre = "";
      public static string GlobalNombre
      	 {
        get { return m_globalNombre; }
         set { m_globalNombre = value; }
        }
		
	}
}
