/*
 * Created by SharpDevelop.
 * User: vdgp_
 * Date: 01/06/2020
 * Time: 04:39 p. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Proyecto
{
	/// <summary>
	/// Description of presa.
	/// </summary>
	public class presa
	{
		public List<Point> coordenadas;
		public int inicio,fin;
		public List <int> vertices;
		public bool verificar;
		public List<candidato> listaDijkstra;
		public Point posicionActual;
		public int ID;
		public bool perseguida;
		public bool estoyEnVertice;
		public int verticeAnterior;
		public bool meRegrese;
		public presa( int inicio_,int ID_)
		{
			inicio=inicio_;
			ID=ID_;
			listaDijkstra= new List<candidato>();
			vertices= new List<int>();
			coordenadas= new List<Point>();
			perseguida=false;
			estoyEnVertice=true;
			meRegrese=false;
		}
		public List<candidato>getListaDijkstra(){
			return listaDijkstra;
		}
		public void setListaDijkstra(List<candidato>Dijkstra){
			listaDijkstra=Dijkstra;
		}
		public void setVertices(List<int>vertices_){
			vertices=vertices_;
		}
		public bool termino(){
				if(coordenadas.Count==0 && vertices[1]==fin)
				return true;
			return false;
		}
		public Point avanzar(){
			posicionActual= coordenadas[0];
			coordenadas.RemoveAt(0);
			return posicionActual;
		}
		public int getInicio(){
			return inicio;
		}
		public void setInicio(int inicio_){
			inicio=inicio_;
			verticeAnterior=inicio;
		}
		public int getFin(){
			return fin;
		}
		public void setFin(int f){
			fin=f;
		}
		public void setVerificar(bool ver){
			verificar=ver;
		}
		public bool getVerificar(){
			return verificar;
		}
		public void setCoordenadas(List<Point>cord){
			coordenadas=cord;
		}
		public List<Point> getCoordenadas(){
			return coordenadas;
		}
		public int getFirstCooredanada(){
			return listaDijkstra[0].id;
		}
		public int getSegundaCoordenada(){
			return listaDijkstra[1].id;
		}
		public bool terminaronCoordenadas(){
			if(coordenadas.Count==1)
				coordenadas.Clear();
			if(coordenadas.Count==0){
				vertices.RemoveAt(0);
				return true;}
			return false;
		}
		public bool terminaronCoordenadas2(){
			if(coordenadas.Count==0){
				//vertices.RemoveAt(0);
				estoyEnVertice=true;
				return true;}
			estoyEnVertice=true;
			return false;
		}
		public int getId(){
			return ID;
		}
		public void setPerseguida(bool p){
			perseguida=p;
		}
		public bool getPerseguida(){
			return perseguida;
		}
		
	}
}
