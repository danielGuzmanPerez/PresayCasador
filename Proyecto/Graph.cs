/*
 * Created by SharpDevelop.
 * User: vdgp_
 * Date: 31/05/2020
 * Time: 05:09 p. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
 using System;
using System.Collections.Generic;
using System.Drawing;


namespace Proyecto
{
	/// <summary>
	/// Description of Graph.
	/// </summary>
	public class Edge{
		public vertex v_d;
		public float distancia;
		public Edge( vertex v_){
	        v_d = v_;
	    }
		public Edge(){
			
		}
		public void  setVd(vertex v){
			v_d=v;
		}
	    vertex getV(){
	    	return v_d;
		}
		public int getX(){
			return v_d.circulo.x;
		}
		public int getY(){
			return v_d.circulo.y;
		}
	}
	
	
	
	public class vertex{
	    public List<Edge> eL;
		public circulo circulo;
		//public List<vertex> vert;
		public int id;
	public vertex(	circulo c, int id){
		this.circulo= c;
		this.id=id;
	}
    public vertex(	){
		
	}
	public int getEdgesCount(){
		return eL.Count;
	}
	//public ciculo getCircle(){}
	public void llenarEdge(List<vertex> ve){
		eL=new List<Edge>();
		for(int i=0; i<ve.Count;i++)
			eL.Add(new Edge(ve[i]));
	}
	public int getX(){
		return circulo.x;
	}
	public int getY(){
		return circulo.y;
	}
	
	public List<Edge> getEdge(){
		return eL;
	}
	
	
	}
	public class graph{
		public List<vertex> v1;
		List<circulo> circulos;
		List<vertex> opciones;
		vertex v;
		Bitmap bmp;
		Color c;
		int x1,x2,y1,y2;
		
		public graph(List<circulo> c, Bitmap bmp_)
		{
			
			bmp=bmp_;
			v1= new List<vertex>();
			circulos=c;
			Graphics gra;
			Brush b1 = new SolidBrush(Color.White);
			Brush b2 = new SolidBrush(Color.Red);
			RectangleF cloneRect = new RectangleF(0, 0, bmp.Width, bmp.Height);
			for(int j =0;j<c.Count; j++){
					v= new vertex(c[j],j+1);
					v1.Add(v);
				}
			for(int i=0; i<v1.Count;i++){
				x1=v1[i].getX();
				y1=v1[i].getY();
				opciones=new List<vertex>();
				for(int j=0; j<v1.Count; j++){
					if(i!=j){
					x2=v1[j].getX();
					y2=v1[j].getY();
					if(verificar(x1,y1,x2,y2,v1[j].circulo.r)==true){
						opciones.Add(v1[j]);     
					}
				   }
					
				}	
				v1[i].llenarEdge(opciones);	
			}
			obtenerDistancia();
			//Ingresar id a los circulos
				 Font drawFont = new Font("Arial", 20);
   				 SolidBrush drawBrush = new SolidBrush(Color.Black);
   				 StringFormat drawFormat = new StringFormat();
    			 drawFormat.FormatFlags = StringFormatFlags.DisplayFormatControl;
    			 gra = Graphics.FromImage(bmp);
				for(int i=0;i<v1.Count;i++){
    			 	gra.DrawString(v1[i].id.ToString(), drawFont, drawBrush, v1[i].getX(),v1[i].getY(), drawFormat);
				}
			
	//pintar las lineas		
			for(int i=0; i<v1.Count;i++){
				//Esta lista tiene las opciones de cada circulo
				List<Edge> edge= v1[i].eL;
				x1=v1[i].getX();
				y1=v1[i].getY();
					
				for(int j=0; j<edge.Count; j++){
					if(j!=i){
					x2=edge[j].getX();
					y2=edge[j].getY();
					linea(x1,y1,x2,y2,Color.Black);
					}
				}
			}
			
		}
	
		public int getVertexCount(){
			return v1.Count;
		}
		bool verificar(int x1_, int y1_, int x2_, int y2_,int radio){
			int x1=x1_;
			int x2=x2_;
			int y1=y1_;
			int y2=y2_;
			float deltax=x2-x1;
			float deltay = y2-y1;
			int band=0;
			if(Math.Abs(deltax)> Math.Abs(deltay)){  // pendiente <1
				float m= (float)deltay/(deltax);
				float b= y1-m*x1;
				if(deltax<0)
					deltax=-1;
				else
					deltax=1;
				while(x1!= x2){
					x1+=(int)deltax;
					y1=(int)Math.Round((double)m*(double)x1+(double)b);
					c=bmp.GetPixel(x1,y1);
					if( band ==0){
						if(c.R!=255 || c.G != 0 || c.B!= 0){
							band=1;
						}
					}else{
						if(band==1){
							if(c.R!=255 || c.G != 255 || c.B!= 255){
								band=2;
							}
						}
						if(band==2){
							if((x1>=x2-radio-5) && (x1<=x2+radio+5) && (y1>=y2-radio-5) && (y1<=y2+radio+5)){
								return true;
							}
							return false;
						}
					}
			}
			}else
				if(deltay!=0){   //pendiente >=1
				float m=(float)deltax/(float)deltay;
				float b= x1-m*y1;
				if(deltay<0)
					deltay=-1;
				else
					deltay=1;
				while(y1!=y2){
					y1+= (int)deltay;
					x1=(int)Math.Round((double)m*(double)y1+(double)b);
					c=bmp.GetPixel(x1,y1);
					if( band ==0){
						if(c.R!=255 || c.G != 0 || c.B!= 0){
							band=1;
						}
					}else{
						if(band==1){
							if(c.R!=255 || c.G != 255 || c.B!= 255){
								band=2;
							}
						}
						if(band==2){
							if((x1>=x2-radio-5) && (x1<=x2+radio+5) && (y1>=y2-radio-5) && (y1<=y2+radio+5)){
								return true;
							}
							return false;
						}
					}
				}
			}
				
			
		
			return true;
		}
		void linea(int x1, int y1, int x2, int y2,Color co){
			float deltax=x2-x1;
			float deltay = y2-y1;
			if(Math.Abs(deltax)> Math.Abs(deltay)){  // pendiente <1
				float m= (float)deltay/(deltax);
				float b= y1-m*x1;
				if(deltax<0)
					deltax=-1;
				else
					deltax=1;
				while(x1!= x2){
					x1+=(int)deltax;
					y1=(int)Math.Round((double)m*(double)x1+(double)b);
					bmp.SetPixel(x1,y1,co);
					
			}
			}else
				if(deltay!=0){   //pendiente >=1
				float m=(float)deltax/(float)deltay;
				float b= x1-m*y1;
				if(deltay<0)
					deltay=-1;
				else
					deltay=1;
				while(y1!=y2){
					y1+= (int)deltay;
					x1=(int)Math.Round((double)m*(double)y1+(double)b);
					bmp.SetPixel(x1,y1,co);
					
				}
			}
		}
		public Bitmap getBmp(){
			return bmp;
		}
		public float ClosetPairPoints(){
			int x1,x2,y1,y2;
			int catetoA=0,catetoB=0;
			float distancia=10000;
			int x1g=0,y1g=0,x2g=0,y2g=0;
			//Obtener las distancias entre los puntos
			
			for(int i=0; i<circulos.Count;i++){
				x1=circulos[i].getX();
				y1=circulos[i].getY();
				for(int j=i; j<circulos.Count;j++){
						if(j!=i){
							x2=circulos[j].getX();
							y2=circulos[j].getY();
							if(y1==y2){
								if(x1>x2)
									catetoA=x1-x2;
								else{
									catetoA=x2-x1;
								}
								if(catetoA<distancia){
									distancia=catetoA;
									x1g=x1;
									y1g=y1;
									x2g=x2;
									y2g=y2;
								}
							}if(x1==x2){
								if(y1>y2)
									catetoA=y1-y2;
								else{
									catetoA=y2-y1;
								}
								if(catetoA<distancia){
									distancia=catetoA;
									x1g=x1;
									y1g=y1;
									x2g=x2;
									y2g=y2;
								}
								}
							if(x1!=x2 && y1 !=y2){
								if(x1>x2){
									 catetoA = x1-x2;
								}else
									 catetoA= x2-x1;
								if(y1>y2){
									 catetoB= y1-y2;
								}else{
									catetoB = y2-y1;
								}
								catetoA=(int)Math.Pow((double)catetoA,(double)2);
								catetoB=(int)Math.Pow((double)catetoB,(double)2);
								if((int)Math.Sqrt((Double)catetoA+(Double)catetoB)<=distancia){
									distancia=(float)Math.Sqrt((Double)catetoA+(Double)catetoB);
									x1g=x1;
									y1g=y1;
									x2g=x2;
									y2g=y2;
							}	
									
							}
							
				}
			}
		}	
			linea(x1g,y1g,x2g,y2g,Color.Red);
			return distancia;
			
		}
		public List<vertex>getList(){
			return v1;
		}
		void obtenerDistancia(){
			int x1,x2,y1,y2;
			int catetoA=0,catetoB=0;
			float distancia=0;
			for(int i=0; i<v1.Count;i++){
				for(int j=0; j<v1[i].eL.Count;j++){
							x1=v1[i].getX();
							y1=v1[i].getY();
							x2=v1[i].eL[j].getX();
							y2=v1[i].eL[j].getY();
							//distancias iguales en y
							if(y1==y2){
								if(x1>x2)
									catetoA=x1-x2;
								else{
									catetoA=x2-x1;
								}
								
									distancia=catetoA;
							}
							//distancia igual en x
							if(x1==x2){
								if(y1>y2)
									catetoA=y1-y2;
								else{
									catetoA=y2-y1;
								}
									distancia=catetoA;
							}
							if(x1!=x2 && y1!=y2){
								if(x1>x2){
									 catetoA = x1-x2;
								}else
									 catetoA= x2-x1;
								if(y1>y2){
									 catetoB= y1-y2;
								}else{
									catetoB = y2-y1;
								}
								catetoA=(int)Math.Pow((double)catetoA,(double)2);
								catetoB=(int)Math.Pow((double)catetoB,(double)2);
									distancia=(float)Math.Sqrt((Double)catetoA+(Double)catetoB);	
									
							}
					v1[i].eL[j].distancia=distancia;
			}
				
		}
		}
		}
		
}

