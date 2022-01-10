/*
 * Created by SharpDevelop.
 * User: vdgp_
 * Date: 31/05/2020
 * Time: 04:53 p. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Proyecto
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		String imagen;
		String imagenAnterior;
		Bitmap bmpBackGround,bmpImage;
		Color c;
		List<circulo> Lista;
		graph Grafo; 
		Graphics gra;
		int opcionBotones;
		List<presa>presas;
		List<depredador> depredadores;
		//List<candidato> listaDijkstra;
		int orig;
		int presaActual;
		bool banderaObjetivo;
		bool banderaObjetivoTermino;
		bool objetivoNuevo;
		int depredadorActual;
		Point objetivo;
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			Lista=new List<circulo>();
			opcionBotones=0;
			presas= new List<presa>();
			banderaObjetivo=false;
			imagenAnterior="0";
			imagen="2";//esto solo lo pongo para comparar con imagenAnterior pero no importa el valor inicial
			presas= new List<presa>();
			banderaObjetivoTermino= false;
			objetivoNuevo=true;
			depredadores= new List<depredador>();
		}
		
		void FindCircle(){
			
			for(int y=0;y<bmpBackGround.Height;y++){
				for(int x=0; x<bmpBackGround.Width;x++){
					c=bmpBackGround.GetPixel(x,y);
					if(c.R==0 && c.G==0 && c.B== 0){
						Lista.Add( SaveCircle(x,y));
			
					}
				}
			}
		 }
		circulo SaveCircle(int x, int y){
			int j=x;
			int i=y;
			//coordenadas del centro del circulo
			int puntoX;
			int puntoY;
			// guardan la cantidad de pixeles que hay del centro hasta la orilla
			int orillaDer;
			int orillaInf;
			//valor de pixel de cada limite
			int izqX;
			int derX;
			int arribaY;
			int abajoY;
			
			//Encontrar el punto medio de  arriba x
			while(j<bmpBackGround.Width ){
				c= bmpBackGround.GetPixel(j,i);
				if(c.R !=0 || c.G !=0 || c.B !=0){
					c= bmpBackGround.GetPixel(j+4,i);
					if(c.R !=255 || c.G !=255 || c.B !=255){
						bmpBackGround.SetPixel(j,i,Color.Black);
						
					}else{
						bmpBackGround.SetPixel(j,i,Color.White);
						break;
					}
				}else{
					j++;
				}
				
			}
			j=x+(j-x)/2;
			puntoX=j;
			arribaY=i;
			
			//Encontrar el medio de abajo  y centro
			while(i<bmpBackGround.Height ){
				c= bmpBackGround.GetPixel(j,i);
				if(c.R !=0 || c.G !=0 || c.B !=0){
					break;
				}else{
					i++;
				}
				
			}
			puntoY =(y+i)/2 ;
			//encontrar orilla derecha
			while(j<bmpBackGround.Width){
				c= bmpBackGround.GetPixel(j,puntoY);
				if(c.R !=0 || c.G !=0 || c.B !=0){
					break;
				}else{
					j++;
				}
				
			}
			derX=j;
			orillaDer=j-puntoX;
			orillaInf=i-puntoY;
			
			//encontrar limite izquierda
			j--;
			while(j>1 ){
				c= bmpBackGround.GetPixel(j,puntoY);
				if(c.R !=0 || c.G !=0 || c.B !=0){
					break;
				}else{
					j--;
				}
				
			}
			izqX=j;
			j=puntoY;//
			//encontrar limite abajo
			while(j<bmpBackGround.Height ){
				c= bmpBackGround.GetPixel(puntoX,j);
				if(c.R !=0 || c.G !=0 || c.B !=0){
					break;
				}else{
					j++;
				}
				
			}
			abajoY=j;
			pintarCirculo(puntoX,puntoY,izqX-4,arribaY-4,derX+4,abajoY+4);
			int radio=orillaDer;
			if(orillaInf>orillaDer){
				radio=orillaInf;
			}
			
				
			return new circulo(puntoX,puntoY,radio+3);
				
			
		}
		void pintarCirculo(int puntox,int puntoy,int izquierdax,int arribay,int derechax,int abajoy){
			
			//pintar 1/4
			for(int j=puntox; j>izquierdax;j--){
				for(int i=puntoy;i>arribay;i--){
					c=bmpBackGround.GetPixel(j,i);
						if(c.G!=255 && c.B!=255 && c.R!=255){
							bmpBackGround.SetPixel(j,i,Color.Red);
							
					}else{
						break;
					}
				}
			}
			//pintar 2/4
			for(int j=puntox; j<derechax;j++){
				for(int i=puntoy;i>arribay;i--){
					c=bmpBackGround.GetPixel(j,i);
						if(c.G!=255 && c.B!=255 && c.R!=255){
							bmpBackGround.SetPixel(j,i,Color.Red);
					}else{
						break;
					}
				}
			}
			//pintar 3/4
			for(int j=puntox; j<derechax;j++){
				for(int i=puntoy+1;i<abajoy;i++){
					c=bmpBackGround.GetPixel(j,i);
						if(c.G!=255 && c.B!=255 && c.R!=255){
							bmpBackGround.SetPixel(j,i,Color.Red);
					}else{
						break;
					}
				}
			}
			
			//pintar 4/4
			for(int j=puntox; j>izquierdax;j--){
				for(int i=puntoy+1;i<abajoy;i++){
					c=bmpBackGround.GetPixel(j,i);
						if(c.G!=255 && c.B!=255 && c.R!=255){
							bmpBackGround.SetPixel(j,i,Color.Red);
					}else{
						break;
					}
				}
			}
		}
		void Button1Click(object sender, EventArgs e)
		{
			if(imagen!="2"){
					opcionBotones=1;
					label1.Text="Selecciona los vertices para las presas";
					objetivoNuevo=false;
			}else
				label1.Text="Inserta una imagen primero";
		}
		void Button2Click(object sender, EventArgs e)
		{	if(presas.Count==0){
				label1.Text="Primero Selecciona una presa";
				return;
			}
			label1.Text="Selecciona los vertices para los Depredadores";
			opcionBotones=2;
		}
		void Button3Click(object sender, EventArgs e)
		{
			if(presas.Count!=0 ){
				label1.Text="Selecciona el vertice para el objetivo";
				opcionBotones=3;
				objetivoNuevo=true;
				if(banderaObjetivoTermino){
					for (int i = 0; i <presas.Count; i++) {
						reCrearPresa(i);
					}
				}
			}else{
				label1.Text="Primero debes elegír las presas";
				opcionBotones=0;
			}
		}
		void Button4Click(object sender, EventArgs e) //BotonInicar   //Aquí se anima
		{
			if(objetivoNuevo==false){
				label1.Text="Debes seleccionar el objetivo, puede ser el mismo";
				return;
			}
			 if(presas.Count==0 && imagen=="2")
				label1.Text="Debes tener una presa y una imágen para iniciar";
			else{
			 	int p=0; //presa
				Point particulaPresas,particulaDepredadores;
				particulaPresas= new Point(0,0);
				Graphics g =Graphics.FromImage(bmpImage);
				Brush bPresas1 = new SolidBrush(Color.Green);
				Brush bPresas2 = new SolidBrush(Color.Blue);
				Brush bDepredadores1 = new SolidBrush(Color.Orange);
				Brush bDepredadoresRadar2 =new SolidBrush(Color.FromArgb(30, 255, 0,0));
				Brush bDepredadoresRadar = new SolidBrush(Color.FromArgb(30, 128, 128,128));
				while(presas.Count!=0){
					g.Clear(Color.Transparent);//Limpiar el bitmap
					for (int i = 0; i <presas.Count; i++) {//for de las presas
						if(presas[i].vertices.Count<2){
							presas[i].vertices.Add(presas[i].vertices[presas[i].vertices.Count-1]);
						}
						if(presas[i].termino()&&presas[i].posicionActual.X<=objetivo.X+15 && presas[i].posicionActual.X>=objetivo.X-15 && presas[i].posicionActual.Y<=objetivo.Y+15 && presas[i].posicionActual.Y>=objetivo.Y-15){
							banderaObjetivoTermino=true;
							for (int j = 0; j < depredadores.Count; j++) {
								if(depredadores[j].presaPerseguida==presas[i].ID){
									depredadores[j].presaPerseguida=0;
									depredadores[j].listaDijkstra.Clear();
									depredadores[j].vertices.Clear();
								}
							}
							presas.RemoveAt(i);
							banderaObjetivo=false;
							continuarAnimando();
							for (int j = 0; j < presas.Count; j++) {//este for recrea la lista dijkstra de todas las presas que queden
								presas[j].setInicio(presas[j].vertices[1]);
								reCrearPresa(j);
							}
							recrearCaminoPresas();
							button1.Enabled=true;
							return;}
						//presas[i].coordenadas.Clear();
						bool perseguida=false;
						if(presas[i].terminaronCoordenadas()){
							llenarCooredenadasPresas(i);
						}  
						
						if(presas[i].estoyEnVertice){
							for (int j = 0; j < depredadores.Count; j++) {
								if(depredadores[j].posicionActual.X<=presas[i].posicionActual.X+150 && depredadores[j].posicionActual.X>=presas[i].posicionActual.X-150 && depredadores[j].posicionActual.Y<=presas[i].posicionActual.Y+150 && depredadores[j].posicionActual.Y>=presas[i].posicionActual.Y-150  ){
									perseguida=true;
									presas[i].perseguida=false;
									particulaPresas=presas[i].posicionActual;
								}else{
									perseguida=false;
									presas[i].estoyEnVertice=false;
								}
						}
							//llenarCooredenadasPresas(i);
						}
						if(presas[i].meRegrese){
							if(presas[i].vertices.Count<2){
									presas[i].vertices.Clear();
									presas[i].vertices.Add(presas[i].verticeAnterior);
									presas[i].vertices.Add(presas[i].fin);
								}
							for (int j = 0; j < depredadores.Count; j++) {
								if(depredadores[j].posicionActual.X>=presas[i].posicionActual.X+150 && depredadores[j].posicionActual.X<=presas[i].posicionActual.X-150 && depredadores[j].posicionActual.Y>=presas[i].posicionActual.Y+150 && depredadores[j].posicionActual.Y<=presas[i].posicionActual.Y-150  ){
									perseguida=true;
									presas[i].perseguida=false;
									particulaPresas=presas[i].posicionActual;
								}else{
									perseguida=false;
									presas[i].estoyEnVertice=false;
									llenarCooredenadasPresas(i);
									presas[i].meRegrese=false;
									presas[i].meRegrese=false;
								}
						}
						}
						
						for (int j = 0; j <depredadores.Count; j++) {//este for es para que se regrese al vertice 0 si alguien se dirije al vertice 1
							
							try{
								if(depredadores[j].presaPerseguida!=0){
									if(depredadores[j].vertices.Count>1){
										if(depredadores[j].vertices[1]==presas[i].vertices[1] && depredadores[j].vertices[0]!=presas[i].vertices[0]){
											presas[i].coordenadas.Clear();
											presas[i].setCoordenadas(AgregarListaPuntos(presas[i].posicionActual.X,presas[i].posicionActual.Y,Lista[presas[i].vertices[0]-1].getX(),Lista[presas[i].vertices[0]-1].getY()));
											presas[i].meRegrese=true;
										}
											
										}
								}else{
										if(depredadores[j].getInicio()==presas[i].vertices[1] && depredadores[j].fin==presas[i].vertices[0]){
											presas[i].coordenadas.Clear();
											presas[i].setCoordenadas(AgregarListaPuntos(presas[i].posicionActual.X,presas[i].posicionActual.Y,Lista[presas[i].vertices[0]-1].getX(),Lista[presas[i].vertices[0]-1].getY()));
											presas[i].meRegrese=true;
										}
									if(depredadores[j].getFin()==presas[i].vertices[1] && depredadores[j].getInicio()!=presas[i].vertices[0]){
										presas[i].coordenadas.Clear();
											presas[i].setCoordenadas(AgregarListaPuntos(presas[i].posicionActual.X,presas[i].posicionActual.Y,Lista[presas[i].vertices[0]-1].getX(),Lista[presas[i].vertices[0]-1].getY()));
											presas[i].meRegrese=true;
									}
								}
								presas[i].verticeAnterior=presas[i].vertices[0];
							}catch{}
							
						}
						
						if(!perseguida && presas[i].coordenadas.Count>0)
							particulaPresas=presas[i].avanzar();
						
							if(presas[i].getPerseguida()){//si esta siendo perseguida se pinta de un color distinto
								g.FillEllipse(bPresas2,particulaPresas.X-22,particulaPresas.Y-22,45,45);
							}else g.FillEllipse(bPresas1,particulaPresas.X-15,particulaPresas.Y-15,30,30);
					}
					for (int i = 0; i < depredadores.Count; i++) {//for de los depredadores
						if(depredadores[i].terminaronCoordenadas() && depredadores[i].getPresaperseguida()==0){
							depredadores[i].setInicio(depredadores[i].getFin());
							if(depredadores[i].getPresaperseguida()==0)//este if es para que no busque vertices aleatorios si ya esta persiguiendo a alguien
								//depredadores[i].setInicio(depredadores[i].getFin());
								depredadores[i].inicio=depredadores[i].fin;
								elegirVerticeDepredador(i);
						}
						//este if es por si esta en un vertice y tiene que seguir persiguiendolo 
						if(depredadores[i].terminaronCoordenadas() && depredadores[i].getPresaperseguida()!=0 && depredadores[i].vertices[1]==depredadores[i].fin){
							depredadores[i].seLoVoliVioAEncontrar=true;
								depredadores[i].setInicio(depredadores[i].getFin());
								depredadores[i].setFin(presas[p].vertices[1]);
								depredadorActual=i;
								depredadores[i].listaDijkstra.Clear();
								depredadores[i].vertices.Clear();
								inicializarListaDijkstraDepredadores(depredadores[i].getInicio());
								depredadores[i].vertices.Add(depredadores[i].getInicio());
								dijkstraDepredadores();
								depredadores[i].setVertices(GuardarCaminoDepredadores(depredadores[i].getInicio(),depredadores[i].getFin()));
						}
						//este if es por si ya no encontró a su presa
						if(depredadores[i].terminaronCoordenadas() && depredadores[i].getPresaperseguida()!=0 && !depredadores[i].seLoVoliVioAEncontrar){
							if(depredadores[i].vertices.Count==1){
								depredadores[i].setInicio(depredadores[i].vertices[0]);
								elegirVerticeDepredador(i);
								depredadores[i].setPresaPerseguida(0);
								depredadores[i].velocidad=1;
								presas[depredadores[i].presaPerseguida].setPerseguida(false);
								
							}else{
								depredadores[i].setCoordenadas(AgregarListaPuntos(Lista[depredadores[i].vertices[0]-1].getX(),Lista[depredadores[i].vertices[0]-1].getY(),Lista[depredadores[i].vertices[1]-1].getX(),Lista[depredadores[i].vertices[1]-1].getY()));
								depredadores[i].vertices.RemoveAt(0);
							}
						}
						if(depredadores[i].getPresaperseguida()==0){
							for (int j = 0; j < presas.Count; j++) {//aqui se busca si hay presas en el radio de 300 pixeles de los depredadores
								if(presas[j].posicionActual.X<=depredadores[i].posicionActual.X+145 && presas[j].posicionActual.X>=depredadores[i].posicionActual.X-145 && presas[j].posicionActual.Y<=depredadores[i].posicionActual.Y+145 && presas[j].posicionActual.Y>=depredadores[i].posicionActual.Y-145 && presas[j].getPerseguida()==false && presas[j].coordenadas.Count>4 &&!presas[i].estoyEnVertice){
									depredadores[i].setPresaPerseguida(presas[j].getId());
									presas[j].setPerseguida(true);
									depredadores[i].velocidad=2;
									depredadores[i].finalAnterior=depredadores[i].getFin();
									if(depredadores[i].coordenadas.Count<4 && presas[j].vertices[1]==depredadores[i].fin){
										depredadores[i].setCoordenadas(AgregarListaPuntos(Lista[depredadores[i].getFin()].getX(),Lista[depredadores[i].getFin()].getY(),Lista[presas[j].vertices[0]-1].getX(),Lista[presas[j].vertices[0]-1].getY()));
									}
									break;
								}
						  }
						}else{//este else significa que ya esta persiguiendo a una presa
							
							
							p=depredadores[i].presaPerseguida-1;//este if vuelve  a hacer el dijsktra del depredador si se encuntra a su presa pero que ya va en otro camino
						/*if(presas[p].posicionActual.X<=depredadores[i].posicionActual.X+140 && presas[p].posicionActual.X>=depredadores[i].posicionActual.X-140 && presas[p].posicionActual.Y<=depredadores[i].posicionActual.Y+140 && presas[p].posicionActual.Y>=depredadores[i].posicionActual.Y-140 &&presas[p].vertices[1]!=depredadores[i].getFin()&& presas[p].coordenadas.Count>4 ){
							depredadores[i].seLoVoliVioAEncontrar=true;
								depredadores[i].setInicio(depredadores[i].getFin());
								depredadores[i].setFin(presas[p].vertices[1]);
								depredadorActual=i;
								depredadores[i].listaDijkstra.Clear();
								depredadores[i].vertices.Clear();
								inicializarListaDijkstraDepredadores(depredadores[i].getInicio());
								depredadores[i].vertices.Add(depredadores[i].getInicio());
								dijkstraDepredadores();
								depredadores[i].setVertices(GuardarCaminoDepredadores(depredadores[i].getInicio(),depredadores[i].getFin()));
						}else
							depredadores[i].seLoVoliVioAEncontrar=false;*/
							
							
							if(depredadores[i].listaDijkstra.Count==0){
								p=-1;
								for (int j = 0; j <presas.Count; j++) {//este for sirve para buscar la posicion de la presa que esta persiguienso el depredador
									if(presas[j].getId()== depredadores[i].getPresaperseguida()){
										p=j;
										break;
									}	
								/*	if(p==-1){//por si la presa que estaba persiguiendo ya termino
										depredadores[i].velocidad=1;
										depredadores[i].listaDijkstra.Clear();
										//depredadores[i].setFin(depredadores[i].vertices[0]);
										depredadores[i].setInicio(depredadores[i].vertices[1]);
									}*/
								}
								//este es para crear el dijsktra del depredador
								depredadores[i].setInicio(depredadores[i].getFin());
								depredadores[i].setFin(presas[p].vertices[1]);
								depredadorActual=i;
								inicializarListaDijkstraDepredadores(depredadores[i].getInicio());
								depredadores[i].vertices.Add(depredadores[i].getInicio());
								dijkstraDepredadores();
								depredadores[i].setVertices(GuardarCaminoDepredadores(depredadores[i].getInicio(),depredadores[i].getFin()));}
							//aquí es lo que va a pasar si la presa sigue en el radar del depredador //la cantidad de coordenadas es para verificar que no este en un vertice
							p=depredadores[i].presaPerseguida-1;
							if(presas[p].posicionActual.X<=depredadores[i].posicionActual.X+140 && presas[p].posicionActual.X>=depredadores[i].posicionActual.X-140 && presas[p].posicionActual.Y<=depredadores[i].posicionActual.Y+140 && presas[p].posicionActual.Y>=depredadores[i].posicionActual.Y-140 &&presas[p].coordenadas.Count>4 ){
								//Esto es cuando ya se comió a la presa
								if(presas[p].posicionActual.X<=depredadores[i].posicionActual.X+10 && presas[p].posicionActual.X>=depredadores[i].posicionActual.X-10 && presas[p].posicionActual.Y<=depredadores[i].posicionActual.Y+10 && presas[p].posicionActual.Y>=depredadores[i].posicionActual.Y-10 ){
									presas.RemoveAt(p);
									depredadores[i].setPresaPerseguida(0);
									depredadores[i].velocidad=1;
									depredadores[i].listaDijkstra.Clear();
									//depredadores[i].inicio=depredadores[i].vertices[1];
									depredadores[i].setFin(depredadores[i].finalAnterior);
									depredadores[i].vertices.RemoveAt(0);
									depredadores[i].seLoVoliVioAEncontrar=false;
									//elegirVerticeDepredador(i);
								}
								if(depredadores[i].coordenadas.Count==0 && depredadores[i].presaPerseguida!=0){//este es para agregar cordenadas de dijkstra
									if(depredadores[i].vertices.Count<2){
										if(presas[depredadores[i].getPresaperseguida()-1].vertices.Count>2){
											//if(depredadores[i].getPresaperseguida()-1].vertices[1])
											depredadores[i].vertices.Add(presas[depredadores[i].getPresaperseguida()-1].vertices[2]);
										}else depredadores[i].vertices.Add(presas[depredadores[i].getPresaperseguida()-1].vertices[0]);
									}
									//depredadores[i].vertices.RemoveAt(0);
									if(depredadores[i].inicio==depredadores[i].finalAnterior)
										depredadores[i].finalAnterior=depredadores[i].fin;
									//depredadores[i].vertices.Add(presas[depredadores[i].getPresaperseguida()].vertices[0]);
									depredadores[i].setCoordenadas(AgregarListaPuntos(Lista[depredadores[i].vertices[0]-1].getX(),Lista[depredadores[i].vertices[0]-1].getY(),Lista[depredadores[i].vertices[1]-1].getX(),Lista[depredadores[i].vertices[1]-1].getY()));
								}
							}else{
								p=depredadores[i].presaPerseguida-1;
								// aqui se deja de perseguir si esta fuera del radar
								//p=depredadores[i].presaPerseguida-1;
								//depredadores[i].setPresaPerseguida(0);
								//depredadores[i].velocidad=1;
								presas[p].setPerseguida(false);
								//depredadores[i].listaDijkstra.Clear();
							}
					}
							particulaDepredadores=depredadores[i].avanzar(); //Aquí se animan los depredadores
						if(depredadores[i].getPresaperseguida()==0){
							g.FillEllipse(bDepredadoresRadar,particulaDepredadores.X-150,particulaDepredadores.Y-150,300,300);
						}else {
							g.FillEllipse(bDepredadoresRadar2,particulaDepredadores.X-150,particulaDepredadores.Y-150,300,300);
						}
						g.FillEllipse(bDepredadores1,particulaDepredadores.X-15,particulaDepredadores.Y-15,30,30);
						}
				pictureBox1.Refresh();	
			}
		}
		}
		void Button5Click(object sender, EventArgs e)
		{
			if(openFileDialog1.ShowDialog()==DialogResult.OK){
				if(bmpBackGround!=null){
					bmpBackGround.Dispose();
					pictureBox1.BackgroundImage.Dispose();
				}
				imagen= openFileDialog1.FileName;
				bmpBackGround = new Bitmap(imagen);
				 pictureBox1.BackgroundImage=bmpBackGround;
				 opcionBotones=0;
				 //
				System.Drawing.Imaging.PixelFormat format = bmpBackGround.PixelFormat;
				RectangleF cloneRect = new RectangleF(0, 0, bmpBackGround.Width, bmpBackGround.Height);
				 bmpImage=bmpBackGround.Clone(cloneRect,format);
				 gra=Graphics.FromImage(bmpImage);
				 gra.Clear(Color.Transparent);//
				 if(imagenAnterior!=imagen){
				 	Lista.Clear();
					FindCircle();
					Grafo= new graph(Lista,bmpBackGround);
					bmpBackGround=Grafo.getBmp();
					presas.Clear();
					depredadores.Clear();
				 }
				 button1.Enabled=true;
					pictureBox1.Image=bmpImage;
					button4.Text="Iniciar";
					banderaObjetivoTermino=false;
			}
		}
		void PictureBox1MouseClick(object sender, MouseEventArgs e)
			{
				float imageAspect = (float)bmpBackGround.Width / bmpBackGround.Height;
			    float controlAspect = (float)pictureBox1.Width / pictureBox1.Height;
			    float newX = e.X;
			    float newY = e.Y;
			    if(imageAspect>controlAspect)
			    { 
			        float ratioWidth = (float)bmpBackGround.Width / pictureBox1.Height;
			        newX *= ratioWidth;
			        float scale = (float)pictureBox1.Width / bmpBackGround.Width;
			        float displayHeight = scale * bmpBackGround.Height;
			        float diffHeight = pictureBox1.Height - displayHeight;
			        diffHeight /= 2;
			        newY -= diffHeight;
				        newY /= scale;
			    }
			    else
			    { 
			        float ratioHeight = (float)bmpBackGround.Height / pictureBox1.Height;
			        newY *= ratioHeight;
			        float scale = (float)pictureBox1.Height / bmpBackGround.Height;
			        float displayWidth = scale * bmpBackGround.Width;
			        float diffWidth = pictureBox1.Width - displayWidth;
			        diffWidth /= 2;
			        newX -= diffWidth;
			        newX /= scale;
			    
			    }
			   switch (opcionBotones) {
			    	case 1: 
			    		crearPresa((int)newX,(int)newY);
			    		break;
			    	case 2: crearDepredador((int)newX,(int)newY);
			    		break;
			    	case 3: if(!banderaObjetivo){
			    					agregarFinPresas((int)newX,(int)newY);
			    					banderaObjetivo=true;
			    				}
			    		break;
			    		default: return;
			   }
		}
		//***********************************Funciones que ambos comparten********************************************
		public int verificarVertice(int x, int y){
			for (int i = 0; i <Lista.Count; i++) {
				if(x<=Lista[i].x+Lista[i].r+10 && x>=Lista[i].x-Lista[i].r-10 && y<=Lista[i].y+Lista[i].r+10 && y>=Lista[i].y-Lista[i].r-10){
					return i+1;
				}
			}
			return -1;
		}
		List<Point>AgregarListaPuntos(int x1_,int y1_,int x2_,int y2_ ){
			int x1=x1_;
			int x2=x2_;
			int y1=y1_;
			int y2=y2_;
			List<Point>mandar=new List<Point>();
			int xAnterior=x1_;
			int yAnterior=y1_;
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
					if(x1<=x2+4 && x1>=x2-4)break;
					if(xAnterior< x1){
						x1+=4;//
					}else x1-=4;
					mandar.Add(new Point(x1,y1));
					xAnterior=x1;
					yAnterior=y1;
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
					//acelerar el proceso
					if(y1<=y2+4 && y1>=y2-4)break;
					if(yAnterior< y1){
						y1+=4;//
					}else y1-=4;//
					xAnterior=x1;
					yAnterior=y1;
					mandar.Add(new Point(x1,y1));
				}
			}
			return mandar;
		}
		
		void continuarAnimando(){
			Point particulaPresa,particulaDepredadores;
			Graphics g =Graphics.FromImage(bmpImage);
				Brush b1 = new SolidBrush(Color.Green);
				Brush bDepredadores = new SolidBrush(Color.Orange);
				Brush bDepredadoresRadar = new SolidBrush(Color.FromArgb(50, 255, 0,0));
			while(!revisarSiYaTerminaronLasPresas()){
					if(presas.Count==0)
						return;
					g.Clear(Color.Transparent);//Limpiar el bitmap
					for (int i = 0; i < presas.Count; i++) {//for presas
						if(!presas[i].terminaronCoordenadas2()){
							particulaPresa=presas[i].avanzar();
						}else
							particulaPresa= presas[i].posicionActual;
						g.FillEllipse(b1,particulaPresa.X-15,particulaPresa.Y-15,30,30);
					}
					for (int i = 0; i < depredadores.Count; i++) {//for de los depredadores
						if(presas.Count==0)
							break;
						if(depredadores[i].terminaronCoordenadas()){
							depredadores[i].setInicio(depredadores[i].getFin());
							elegirVerticeDepredador(i);
						}
						particulaDepredadores=depredadores[i].avanzar(); //Aquí se animan los depredadores
						//g.FillEllipse(bDepredadoresRadar,particulaDepredadores.X-150,particulaDepredadores.Y-150,300,300);
						g.FillEllipse(bDepredadores,particulaDepredadores.X-15,particulaDepredadores.Y-15,30,30);
					}
					
					pictureBox1.Refresh();
			}
				button4.Text="Continuar";
		}
		//***********************************************presas***************************************************
		//*********************************************************************************************************
		bool revisarSiYaTerminaronLasPresas(){
			bool enviar=true;
			for (int i = 0; i < presas.Count; i++) {
				if(!presas[i].terminaronCoordenadas2()){
					enviar=false;
				}
			}
			return enviar;
		}
		
		void agregarPresa(int inicioPresa){
			presa p= new presa(inicioPresa,presas.Count+1);
			presas.Add(p);
			Graphics g =Graphics.FromImage(bmpImage);
			Brush b1 = new SolidBrush(Color.Green);
			g.FillEllipse(b1,Lista[inicioPresa-1].x-15,Lista[inicioPresa-1].y-15,35,35);
			pictureBox1.Image=bmpImage;
			pictureBox1.Refresh();
		}
		
		public void crearPresa(int x_, int y_){//se crea todo lo referente a las presas
			int x=x_;
			int y=y_;
			int validar=verificarVertice( x,  y);
			if(validar>0){
				agregarPresa(validar);
				presas[presas.Count-1].posicionActual= new Point(x_,y_);
				presaActual=presas.Count-1;
				//listaDijkstra= new List<candidato>();
				inicializarListaDijkstraPresas(presas[presas.Count-1].getInicio());
				orig=presas[presas.Count-1].getInicio();
				presas[presas.Count-1].vertices.Add(orig);//se agrega el vertice inicial
				dijkstraPresas(presas[presas.Count-1].getInicio());
			
			}			}
		
		public void reCrearPresa(int presa){
			presaActual=presa;
			presas[presa].listaDijkstra.Clear();
			presas[presa].vertices.Clear();
			ReinicializarListaDijkstraPresas(presas[presa].getInicio(),presa);
			orig=presas[presas.Count-1].getInicio();
			presas[presa].vertices.Add(orig);//se agrega el vertice inicial
			dijkstraPresas(presas[presa].getInicio());
		}
		void recrearCaminoPresas(){
				for (int i = 0; i <presas.Count; i++) {
					presaActual=i;
					presas[i].setVertices(GuardarCaminoPresas(presas[i].getInicio(),presas[i].getFin()));
				}
				for (int i = 0; i < presas.Count; i++) {
					llenarCooredenadasPresas(i);
				}
		}
		
		void agregarFinPresas(int x_, int y_){//agrega el vertice final a las presas
			int x=x_;
			int y=y_;
			int validar=verificarVertice( x,  y);
			if(validar>0){
				for (int i = 0; i < presas.Count; i++) {
					presas[i].setFin(validar);
				}
				for (int i = 0; i <presas.Count; i++) {
					presaActual=i;
					presas[i].setVertices(GuardarCaminoPresas(presas[i].getInicio(),presas[i].getFin()));
				}
				for (int i = 0; i < presas.Count; i++) {
					llenarCooredenadasPresas(i);
				}
				//Se pinta el objetivo
				objetivo=new Point(x_,y_);
			Graphics g =Graphics.FromImage(bmpBackGround);
			Brush b1 = new SolidBrush(Color.Purple);
			g.FillEllipse(b1,Lista[validar-1].getX()-15,Lista[validar-1].getY()-15,35,35);
			//pictureBox1.Image=bmpImage;
			pictureBox1.Refresh();
			button1.Enabled=false;
			}
		}
		//***************************************************Dijkstra presas*********************************************
		void inicializarListaDijkstraPresas(int origen){
			candidato opc;
			opcion op;
			for (int i = 0; i < Grafo.v1.Count; i++) {
				List<opcion>ids= new List<opcion>();
				for (int j = 0; j <Grafo.v1[i].eL.Count; j++) {
					op= new opcion(Grafo.v1[i].eL[j].v_d.id,(int)Math.Round(Grafo.v1[i].eL[j].distancia));
					ids.Add(op);
				}
				if(Grafo.v1[i].id== origen)
				 opc= new candidato(Grafo.v1[i].id,0,ids);
				else
					opc= new candidato(Grafo.v1[i].id,int.MaxValue-1,ids);
				
				presas[presas.Count-1].listaDijkstra.Add(opc);
			}
		}
		void ReinicializarListaDijkstraPresas(int origen, int presa){//esta hace lo mismpo que la de arriba pero esta no guarda los datos en el ultimo arreglo de presas
			candidato opc;
			opcion op;
			for (int i = 0; i < Grafo.v1.Count; i++) {
				List<opcion>ids= new List<opcion>();
				for (int j = 0; j <Grafo.v1[i].eL.Count; j++) {
					op= new opcion(Grafo.v1[i].eL[j].v_d.id,(int)Math.Round(Grafo.v1[i].eL[j].distancia));
					ids.Add(op);
				}
				if(Grafo.v1[i].id== origen)
				 opc= new candidato(Grafo.v1[i].id,0,ids);
				else
					opc= new candidato(Grafo.v1[i].id,int.MaxValue-1,ids);
				
				//presas[presas.Count-1].listaDijkstra.Add(opc);
				presas[presa].listaDijkstra.Add(opc);
					
			}
		}
		void dijkstraPresas(int origen){
			int definitivo;
			//listaDijkstra[origen].definitivo=true;
			while(!terminarPresas()){
				
				definitivo=seleccionarDefinitivoPresas();//elegir el menor(posición)
				actualizaVDPresas(definitivo);//actualiza los valores
				
			}
				
			}
		bool terminarPresas(){//revisa si ya todas las opciones son definitivos
			bool mandar=true;
			for (int i = 0; i < presas[presaActual].listaDijkstra.Count; i++) {
				if(!presas[presaActual].listaDijkstra[i].definitivo)
					mandar=false;
			}
			return mandar;
		}
		int seleccionarDefinitivoPresas(){//busca el menor valor y lo hace definitivo
			int minimo=int.MaxValue;
			int x=0;
			for (int i = 0; i <presas[presaActual].listaDijkstra.Count; i++) {
				if(presas[presaActual].listaDijkstra[i].peso<minimo && !presas[presaActual].listaDijkstra[i].definitivo){
					minimo=presas[presaActual].listaDijkstra[i].peso;
					x=i;
				}
			}
			presas[presaActual].listaDijkstra[x].setDefinitivo(true);
			return x;
		}
		/*void actualizaVD(int definitivo){
			int valor=0;
			int n=0;
			int posicion=0;
			for (int i = 0; i < presas[presas.Count-1].listaDijkstra[definitivo].opciones.Count; i++) {
				valor=presas[presas.Count-1].listaDijkstra[definitivo].peso+presas[presas.Count-1].listaDijkstra[definitivo].opciones[i].distancia;
				posicion=presas[presas.Count-1].listaDijkstra[definitivo].opciones[i].ID-1;
				if( valor<presas[presas.Count-1].listaDijkstra[posicion].peso && !presas[presas.Count-1].listaDijkstra[posicion].definitivo){
					//n=listaDijkstra[definitivo].opciones[posicion].ID-1;
					presas[presas.Count-1].listaDijkstra[posicion].peso=valor;
					presas[presas.Count-1].listaDijkstra[posicion].setProveniente(definitivo+1);
				}
				
			}
		}*/
		void actualizaVDPresas(int definitivo){
			int valor=0;
			int posicion=0;
			for (int i = 0; i < presas[presaActual].listaDijkstra[definitivo].opciones.Count; i++) {
				valor=presas[presaActual].listaDijkstra[definitivo].peso+presas[presaActual].listaDijkstra[definitivo].opciones[i].distancia;
				posicion=presas[presaActual].listaDijkstra[definitivo].opciones[i].ID-1;
				if( valor<presas[presaActual].listaDijkstra[posicion].peso && !presas[presaActual].listaDijkstra[posicion].definitivo){
					//n=listaDijkstra[definitivo].opciones[posicion].ID-1;
					presas[presaActual].listaDijkstra[posicion].peso=valor;
					presas[presaActual].listaDijkstra[posicion].setProveniente(definitivo+1);
				}
				
			}
		}
		List<int> GuardarCaminoPresas(int origen,int destino){//Aquí se guardan los vertices que se van a visitar
			List<int>list= new List<int>();
			int dest=destino;
			list.Add(destino);
			while(true){
				if(dest==origen)
					break;
				dest=presas[presaActual].listaDijkstra[dest-1].proveniente;
				list.Add(dest);
			}
			list.Reverse();
			//list.RemoveAt(0);
			return list;
				
		}
		void llenarCooredenadasPresas(int presa){
			int x1,x2,y1,y2;
			if(presas[presa].vertices.Count<2)
				return;
			x1=Lista[presas[presa].vertices[0]-1].getX();
 			y1=Lista[presas[presa].vertices[0]-1].getY();
			x2=Lista[presas[presa].vertices[1]-1].getX();
			y2=Lista[presas[presa].vertices[1]-1].getY();
			presas[presa].setCoordenadas(AgregarListaPuntos(x1,y1,x2,y2));
						
		}
		//********************************************Depresadores****************************************************
		//************************************************************************************************************
		public void crearDepredador(int x_, int y_){//se crea todo lo referente a los depredadores
			int x=x_;
			int y=y_;
			int validar=verificarVertice( x,  y);
			if(validar>0){
				agregarDepredador(validar);
				depredadores[depredadores.Count-1].posicionActual=new Point(x_,y_);
				orig=depredadores[depredadores.Count-1].getInicio();
				depredadores[depredadores.Count-1].vertices.Add(orig);//se agrega el vertice inicial
				elegirVerticeDepredador(depredadores.Count-1);
			}			}
		
		void agregarDepredador(int id){
			depredador p= new depredador(id);
			depredadores.Add(p);
			Graphics g =Graphics.FromImage(bmpImage);
			Brush b1 = new SolidBrush(Color.Orange);
			g.FillEllipse(b1,Lista[id-1].x-15,Lista[id-1].y-15,35,35);
			pictureBox1.Image=bmpImage;
			pictureBox1.Refresh();
		}
		void elegirVerticeDepredador(int depred){//aqui se agrega el vertice final y las coordenadas
			List<int>vertices=new List<int>();
			Random rnd = new Random();
			for(int i=0; i<Grafo.v1[depredadores[depred].getInicio()-1].eL.Count;i++){
			vertices.Add(Grafo.v1[depredadores[depred].getInicio()-1].eL[i].v_d.id);//Aquí se agregan todos los vertices que tienen concatenacion con el inicial
			}
			depredadores[depred].setFin(vertices[rnd.Next(0,vertices.Count)]);
			depredadores[depred].setCoordenadas(AgregarListaPuntos(Lista[depredadores[depred].getInicio()-1].getX(),Lista[depredadores[depred].getInicio()-1].getY(),Lista[depredadores[depred].getFin()-1].getX(),Lista[depredadores[depred].getFin()-1].getY()));//se agregan las coordenandas
		}
		
		//*********************************************DijkstraDepredadores*******************************************
		void dijkstraDepredadores(){
			int definitivo;
			//listaDijkstra[origen].definitivo=true;
			while(!terminarDepredadores()){
				
				definitivo=seleccionarDefinitivoDepredadores();//elegir el menor(posición)
				actualizaVDDepredadores(definitivo);//actualiza los valores
				
			}
				
			}
		bool terminarDepredadores(){//revisa si ya todas las opciones son definitivos
			bool mandar=true;
			for (int i = 0; i < depredadores[depredadorActual].listaDijkstra.Count; i++) {
				if(!depredadores[depredadorActual].listaDijkstra[i].definitivo)
					mandar=false;
			}
			return mandar;
		}
		int seleccionarDefinitivoDepredadores(){//busca el menor valor y lo hace definitivo
			int minimo=int.MaxValue;
			int x=0;
			for (int i = 0; i <depredadores[depredadorActual].listaDijkstra.Count; i++) {
				if(depredadores[depredadorActual].listaDijkstra[i].peso<minimo && !depredadores[depredadorActual].listaDijkstra[i].definitivo){
					minimo=depredadores[depredadorActual].listaDijkstra[i].peso;
					x=i;
				}
			}
			depredadores[depredadorActual].listaDijkstra[x].setDefinitivo(true);
			return x;
		}
		void actualizaVDDepredadores(int definitivo){
			int valor=0;
			int posicion=0;
			for (int i = 0; i < depredadores[depredadorActual].listaDijkstra[definitivo].opciones.Count; i++) {
				valor=depredadores[depredadorActual].listaDijkstra[definitivo].peso+depredadores[depredadorActual].listaDijkstra[definitivo].opciones[i].distancia;
				posicion=depredadores[depredadorActual].listaDijkstra[definitivo].opciones[i].ID-1;
				if( valor<depredadores[depredadorActual].listaDijkstra[posicion].peso && !depredadores[depredadorActual].listaDijkstra[posicion].definitivo){
					//n=listaDijkstra[definitivo].opciones[posicion].ID-1;
					depredadores[depredadorActual].listaDijkstra[posicion].peso=valor;
					depredadores[depredadorActual].listaDijkstra[posicion].setProveniente(definitivo+1);
				}
				
			}
		}
		void inicializarListaDijkstraDepredadores(int origen){
			candidato opc;
			opcion op;
			for (int i = 0; i < Grafo.v1.Count; i++) {
				List<opcion>ids= new List<opcion>();
				for (int j = 0; j <Grafo.v1[i].eL.Count; j++) {
					op= new opcion(Grafo.v1[i].eL[j].v_d.id,(int)Math.Round(Grafo.v1[i].eL[j].distancia));
					ids.Add(op);
				}
				if(Grafo.v1[i].id== origen)
				 opc= new candidato(Grafo.v1[i].id,0,ids);
				else
					opc= new candidato(Grafo.v1[i].id,int.MaxValue-1,ids);
				
				depredadores[depredadorActual].listaDijkstra.Add(opc);
			}
		}
		List<int> GuardarCaminoDepredadores(int origen,int destino){//Aquí se guardan los vertices que se van a visitar
			List<int>list= new List<int>();
			int dest=destino;
			list.Add(destino);
			while(true){
				if(dest==origen)
					break;
				dest=depredadores[depredadorActual].listaDijkstra[dest-1].proveniente;
				list.Add(dest);
			}
			if(list.Count==1)
				list.Add(origen);
			list.Reverse();
			//list.RemoveAt(0);
			return list;
				
		}
		void MainFormLoad(object sender, EventArgs e)
		{
	
		}
		void PictureBox1Click(object sender, EventArgs e)
		{
	
		}
	
	}
}


