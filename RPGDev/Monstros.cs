using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGDev
{
    internal class Monstros : Personagem
    {

        public int IdMonstro { get; set; }
        public string TipoMonstro { get; set; }
        public int MobExperiencia { get; set; }
        public double Multiplicador { get; set; }

        public Monstros Mob01(double mult)
        {
            IdMonstro = 1;
            Nome = "Ratão do esgoto";
            TipoMonstro = "COMUM";
            MobExperiencia = (int)(10 * mult);
            HP = (int)(15 * mult);
            Defesa = (int)(10 * mult);
            Ataque = (int)(3 * mult);
            return this;
        }
        
        public Monstros Mob02(double mult)
        {
            IdMonstro = 1;
            Nome = "Lider Rato";
            TipoMonstro = "RARO";
            MobExperiencia = (int)(15 * mult);
            HP = (int)(20 * mult);
            Defesa = (int)(15 * mult);
            Ataque = (int)(5 * mult);
            return this;
        }
        
        public Monstros Mob03(double mult)
        {
            IdMonstro = 1;
            Nome = "Lider Ratão";
            TipoMonstro = "CHEFE";
            MobExperiencia = (int)(20 * mult);
            HP = (int)(30 * mult);
            Defesa = (int)(20 * mult);
            Ataque = (int)(5 * mult);
            return this;
        }

        public override int CalcularDano()
        {
            Random random = new Random();
            int numeroSorteado = random.Next(1, 5);
            if (numeroSorteado == 1) return (base.CalcularDano() * 2);
            return Ataque;
        }
    }
}
