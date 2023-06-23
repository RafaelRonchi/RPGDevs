using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGDev
{
    internal class Mapa
    {
        public int alturaM = 10;
        public int larguraM = 10;
        public int[,] formatoMapa;
        public int[] mobSpaw;
        public int[] entrada;
        public int[] saida;

        public Mapa()
        {
            Console.WriteLine("\nGerando Mapa");
            formatoMapa = new int[alturaM,larguraM];
            mobSpaw = new int[] { 0,1,2,3 };
            MontarMapa();
            Console.WriteLine("Mapa Gerado");
        }

        public void MontarMapa()
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
            entrada = new int[] { xEntrada,yEntrada };
            int xSaida = rdn.Next(0,10);
            int ySaida = rdn.Next(0,10);
            formatoMapa[xSaida,ySaida] = 4;
            saida = new int[] { xSaida,ySaida };
        }
    }
}
