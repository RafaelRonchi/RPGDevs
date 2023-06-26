using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RPGDev
{
    internal class Mapa
    {
        private int alturaM = 10;
        private int larguraM = 10;
        public int[,] formatoMapa;
        private int[] mobSpaw;
        public int[] Entrada { get; private set; }
        
        private int[] saida;

        public Mapa()
        {
            Console.Write("\n Gerando Mapa ");
            formatoMapa = new int[alturaM,larguraM];
            mobSpaw = new int[] { 0,1,2,3 };
            MontarMapa();
            
            Console.Write(".");
            Thread.Sleep(700);
            Console.Write(".");
            Thread.Sleep(700);
            Console.Write(".");
            Thread.Sleep(700);
            Console.WriteLine("\n Mapa Gerado");
        }

        private void MontarMapa()
        {
            Random rdn = new Random();
            for (int i = 0; i < alturaM; i++)
            {
                for (int j = 0; j < larguraM; j++)
                {
                    formatoMapa[i,j] = mobSpaw[rdn.Next(0,3)];
                }
            }
            int xEntrada = rdn.Next(0,10);
            int yEntrada = rdn.Next(0,10);
            formatoMapa[xEntrada,yEntrada] = 0;
            Entrada = new int[] { xEntrada, xEntrada };
            
            int xSaida = rdn.Next(0,10);
            int ySaida = rdn.Next(0,10);
            formatoMapa[xSaida,ySaida] = 4;
            saida = new int[] { xSaida,ySaida };
        }
        public void MostrarMapa(List<int> passos )
        {
           
            int[] pontoInicial = new int[] { Entrada[0] , Entrada[1] };
            int[] ultimapegada = new int[] { pontoInicial[0], pontoInicial[1] };
            string mapaComeco = "E";
            string mapaSaida = "S";
            string mappassos = "□";
            string mapDesconhecido = "▩";
            string[,] MapaMostrar = new string[alturaM, larguraM];
            MapaMostrar[ultimapegada[0], ultimapegada[1]] = mapaComeco;
            MapaMostrar[saida[0], saida[1]] = mapaSaida;
            
            for (int i = 0; i < passos.Count; i++)
            {
                if(passos[i] == 1) {

                    ultimapegada[0]--;
                    MapaMostrar[ultimapegada[0], ultimapegada[1]] = mappassos;
                   
                    
                }
                else if (passos[i] == 2)
                {
                    ultimapegada[0]++;
                    MapaMostrar[ultimapegada[0], ultimapegada[1]] = mappassos;
                   

                }
                else if(passos[i] == 3)
                {
                    ultimapegada[1]++;
                    MapaMostrar[ultimapegada[0], ultimapegada[1]] = mappassos;
                   
                }
                else if (passos[i] == 4)
                {
                    ultimapegada[1]--;
                    MapaMostrar[ultimapegada[0], ultimapegada[1]] = mappassos;
                    
                }


            }
            Console.WriteLine("Mapa percorrido:");
            Console.WriteLine("C = Entrada S = Saida");
            Console.WriteLine("----------");
            for (int i = 0; i < larguraM; i++)
            {
                for (int j = 0; j < alturaM; j++)
                {
                    if(MapaMostrar[i,j] != mappassos&& MapaMostrar[i, j] != mapaComeco && MapaMostrar[i, j] != mapaSaida)
                    { MapaMostrar[i, j] = mapDesconhecido; }
                   
                    Console.Write(MapaMostrar[i, j]+" ");
                    
                }
                Console.Write("\n");
            }
            Console.WriteLine("----------");

        }
    }
}
