/*
 * Created by SharpDevelop.
 * User: vdgp_
 * Date: 03/06/2020
 * Time: 05:12 p. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
 using System.Collections.Generic;
using System;

namespace Proyecto
{
	/// <summary>
	/// Description of listaCandidato.
	/// </summary>
		public class listaCandidato
	{
		public List<candidato> l;
		public List<int >ID;
		public listaCandidato()
		{
			l=new List<candidato>();
			ID=new List<int>();
		}
		public void addCandidato(candidato c)
		{
			l.Add(c);
		}
		public void addId(int i){
			ID.Add(i);
		}
	}
}