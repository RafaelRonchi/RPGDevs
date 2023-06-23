using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RPGDev
{
    internal class Itens
    {
        public string NomeItem { get; set; }
        public int TipoItem { get; set; }
        public int PoderItem { get; set; }
        public int LocalItem { get; set; }
        public int Ataque { get; set; }
        public int Defesa { get; set; }

        // Itens 1 = poções 2 = armas 3 = escudos
        public Itens()
        {

        }

        public Itens Pocao()
        {
            NomeItem = "Pocao Pequena";
            TipoItem = 1;
            PoderItem = 10;
            return this;
        }
        
        public Itens Espada()
        {
            NomeItem = "Essencia da Espada";
            TipoItem = 2;
            Ataque = 3;
            return this;
        }
        
        public Itens Escudo()
        {
            NomeItem = "Essencia da Protecao";
            TipoItem = 3;
            Defesa = 3;
            return this;
        }
        
        public Itens Loot()
        {
            List<Itens> lootList = new List<Itens>();
            lootList.Add(Escudo());
            lootList.Add(Pocao());
            lootList.Add(Espada());
            Random rdn = new Random();
            int rdnloot = rdn.Next(0,lootList.Count);
            return lootList[1];
        }
    }
}
