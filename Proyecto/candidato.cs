/*
 * Created by SharpDevelop.
 * User: vdgp_
 * Date: 02/06/2020
 * Time: 12:10 a. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
namespace Proyecto
{
	/// <summary>
	/// Description of candidatos.
	/// </summary>
public class candidato{
		public int id;
		public int peso;
		public bool definitivo;
		public int proveniente;
		public List<opcion>opciones;
		public candidato(int id_, int peso_,List<opcion> opc){
			id=id_;
			peso=peso_;
			definitivo=false;
			proveniente=0;
			opciones=opc;
			
		}
		public void setPeso(int peso_ ){
			peso=peso_;
		}
		public int getPeso(){
			return peso;
		}
		public void setDefinitivo(bool def){
			definitivo=def;
		}
		public bool getDefinitivo(){
			return definitivo;
		}
		public void setProveniente(int prov){
			proveniente=prov;
		}
		public int getProveniente(){
			return proveniente;
		}
			
	}
}
