/*
 * Created by SharpDevelop.
 * User: vdgp_
 * Date: 02/06/2020
 * Time: 12:10 a. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Proyecto
{
	/// <summary>
	/// Description of opcion.
	/// </summary>
	public class opcion
	{
		public int ID;
		public int distancia;
		public opcion(int id_,int distancia_)
		{
			ID=id_;
			distancia=distancia_;
		}
		public int getId(){
			return ID;
		}
		public int GetDistancia(){
			return distancia;
		}
	}
}
