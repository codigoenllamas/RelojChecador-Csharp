/*
 * Created by SharpDevelop.
 * User: pcaula
 * Date: 22/12/2014
 * Time: 09:12 p.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace PersonalNet
{
	/// <summary>
	/// Description of Global_ip.
	/// </summary>
	static class ClassTime
	{
      private static string m_globalReg = "";
      private static string m_globalIdEmple = "";
      private static string m_globalMin = ""; 
	  private static string m_globalHora = "";
            
     public static string GlobalReg
      	 {
        get { return m_globalReg; }
         set { m_globalReg = value; }
        } 
     
  public static string GlobalIdEmple
      	 {
        get { return m_globalIdEmple; }
         set { m_globalIdEmple = value; }
        }      
      
      
      public static string GlobalHora
      	 {
        get { return m_globalHora; }
         set { m_globalHora = value; }
        }
		
     public static string GlobalMin
      	 {
        get { return m_globalMin; }
         set { m_globalMin = value; }
        }
        
      
		}
	}

 