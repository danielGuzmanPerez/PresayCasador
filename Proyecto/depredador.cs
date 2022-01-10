/*
 * Created by SharpDevelop.
 * User: vdgp_
 * Date: 06/06/2020
 * Time: 01:08 a. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Proyecto
{
	/// <summary>
	/// Description of depredadores.
	/// </summary>
	public class depredador
	{
		
		public List<Point> coordenadas;
		public int inicio,fin;
		public List <int> vertices;
		public bool verificar;
		public List<candidato> listaDijkstra;
		public Point posicionActual;
		public int velocidad;
		public int presaPerseguida;
		public int finalAnterior;
		public bool seLoVoliVioAEncontrar;
		public depredador( int inicio_)
		{
			inicio=inicio_;
			listaDijkstra= new List<candidato>();
			vertices= new List<int>();
			coordenadas= new List<Point>();
			velocidad=1;
			presaPerseguida=0;
			seLoVoliVioAEncontrar=false;
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
			if(coordenadas.Count>=1){
				if(velocidad==1){
				posicionActual= coordenadas[0];
				coordenadas.RemoveAt(0);
				}else{
					if(coordenadas.Count>1){
						posicionActual= coordenadas[1];
						coordenadas.RemoveAt(0);
						coordenadas.RemoveAt(0);
					}else{
						posicionActual= coordenadas[0];
						coordenadas.RemoveAt(0);
					}
				}
		}
			return posicionActual;
		}
		public int getInicio(){
			return inicio;
		}
		public void setInicio(int inicio_){
			inicio=inicio_;
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
			if(coordenadas.Count==0){
				//vertices.RemoveAt(0);
				return true;}
			return false;
		}
		public bool terminaronCoordenadas2(){
			if(coordenadas.Count==0){
				//vertices.RemoveAt(0);
				return true;}
			return false;
		}
		public int getPresaperseguida(){
			return presaPerseguida;
		}
		public void setPresaPerseguida(int presa){
			presaPerseguida=presa;
		}
		
	}
}
