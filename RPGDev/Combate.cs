using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RPGDev
{
    internal class Combate
    {
        public Monstros mob1;
        public Player p1;
        int turn;
        public int[] LeveisPlayer = new int[5] { 1,2,3,4,5 };

        public Combate()
        {
            turn = 0;
        }

        public bool RealizarCombat(Player player,Monstros mob)
        {
            p1 = player;
            mob1 = mob;

            bool playerDead = false;
            bool mobDead = false;
            while (!playerDead && !mobDead)
            {
                Console.Write("\n Digite 1 para Atacar ⚔" +
                              "\n Digite 2 para Defender 🛡" +
                              "\n Opção -> ");

                // validar opção
                int opcao = int.Parse(Console.ReadLine());
                if (opcao > 2 || opcao <= 0)
                {
                    Console.WriteLine("Não tente se safar da luta, Escolha uma opção válida");
                }
                else if (opcao == 1)
                {
                    if (player.ClasseLuta == "GUERREIRO")
                    {
                        Console.WriteLine($" Você usa {player.Habilidade.NomeHabilidade}!");
                        Atacar();
                        Console.WriteLine("\n {0} realiza um ataque!", mob1.Nome);
                        MobAtaca();
                    }

                    else if (player.ClasseLuta == "MAGO")
                    {
                        Console.WriteLine($" Você usa {player.Habilidade.NomeHabilidade}!");
                        Atacar();
                        Console.WriteLine("\n{0} realiza um ataque!", mob1.Nome);
                        MobAtaca();
                    }
                    else if (player.ClasseLuta == "RANGER")
                    {
                        Console.WriteLine($" Você usa {player.Habilidade.NomeHabilidade}!");
                        Atacar();
                        Console.WriteLine("\n{0} realiza um ataque!", mob1.Nome);
                        MobAtaca();
                    }
                }

                if (opcao == 2)
                {
                    if (p1.Defesa > 0)
                    {
                        p1.Defesa -= 1;
                        p1.HP += 5;
                        Console.WriteLine($"\n Você se defendeu e curou 5 de vida! Restam " +
                                          $"{p1.Defesa} defesa(s)" +
                                          $"\n Vida {p1.HP}");
                    }

                    else if (p1.Defesa == 0)
                    {
                        Console.WriteLine($"\n Você não possui defesa(s)!" +
                                          $"\n {mob1.Nome} realiza um ataque!");
                        MobAtaca();
                    }
                }
                playerDead = IsDead(p1);
                mobDead = IsDead(mob1);
            }

            // verificar lógica
            if (playerDead) { return false; }
            if (mobDead) { return true; }
            return true;
        }

        public void Combat()
        {
        }

        public void Atacar()
        {

            mob1.HP -= p1.Ataque;
            Console.WriteLine($" O Seu ataque causou {p1.CalcularDano()} de dano");
        }

        public void Defender()
        {
            mob1.HP -= p1.Ataque;
        }

        public void MobAtaca()
        {
            Random rd = new Random();
            var list = new List<string>
            { "Mordida", "Dentada Infecciosa", "Arranhão", "Cauda Espinhenta", "Agarrão Fedorento" };
            int index = rd.Next(list.Count);
            p1.HP -= mob1.Ataque;
            Console.WriteLine
                ($" {mob1.Nome} usou {list[index]} e causou {mob1.CalcularDano()} de Dano");
        }

        public bool IsDead(Personagem p1)
        {
            if (p1.HP <= 0)
            {
                Console.WriteLine($" Os pontos de vida de {p1.Nome} acabaram");
                return true;
            }
            return false;
        }
    }
}
