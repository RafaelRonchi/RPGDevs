using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RPGDev
{
    internal class Controler
    {
        public Menu menu;
        public Player P1 { get; set; }
        public Monstros Mob { get; set; }
        public Mapa Mp1 { get; set; }
        public Itens Item { get; set; }


        public Controler()
        {
            Console.OutputEncoding = Encoding.UTF8;
            menu = new Menu();

            int menuOpcao = menu.MenuInicial();
            if (menuOpcao == 4)
            {
                Environment.Exit(0);
            }

            else
            {
                VerificarTipo(menuOpcao);
            }

            Mp1 = new Mapa();
            P1.Localização[0] = Mp1.Entrada[0];
            P1.Localização[1] = Mp1.Entrada[1];
            Console.Clear();

            string m1 = "\n Você entra em uma Masmorra Escura";
            foreach (char letra in m1) { Console.Write(letra); Thread.Sleep(70); }
            Console.WriteLine();
            string m2 = " Depois de andar um pouco, você olha pra trás e percebe que se perdeu";
            foreach (char letra in m2) { Console.Write(letra); Thread.Sleep(70); }
            Console.WriteLine();
            string m3 = " Agora perdido, você precisa decidir:";
            foreach (char letra in m3) { Console.Write(letra); Thread.Sleep(70); }
            Console.WriteLine();
            OpcoesMap();
            Console.ReadKey();
        }

        public void VerificarTipo(int opcao)
        {
            // validar nome do personagem
            Console.Write("\n Digite o Nome do seu Personagem: ");
            string nome = Console.ReadLine();


            if (opcao == 1)
            {
                P1 = new Player(nome,"GUERREIRO",new Habilidade("Trespassar","Combate",13));
                //Guerreiro guerreiro = new Guerreiro(nome);
            }
            else if (opcao == 2)
            {
                P1 = new Player(nome,"MAGO",new Habilidade("Misseis Mágicos","Metamagica",14));
            }
            else
            {
                P1 = new Player(nome,"RANGER",new Habilidade("Tiro Certeiro","Arquearia",15));
            }
        }

        public void OpcoesMap()
        {
            Console.Write($"\n Digite 1 para ir para o Norte \u2191" +
                          $"\n Digite 2 para ir para o Sul \u2193" +
                          $"\n Digite 3 para ir para o Leste \u2192" +
                          $"\n Digite 4 para ir para o Oeste \u2190" +
                          $"\n Digite 5 para ver status" +
                          $"\n Digite 6 para ver Mochila" +
                          $"\n Digite 7 para ver Mapa" +
                          $"\n Opção -> ");
            string valida = Console.ReadLine();
            int opcao = 0;
            if (valida.Length == 1 && Char.IsNumber(valida[0]))
            {
                opcao = int.Parse(valida);
                if (opcao <= 0 || opcao >= 8)
                {
                    Console.WriteLine($"\n Caro Jogador, digite uma opção valida: ");
                    OpcoesMap();
                }
            }

            else
            {
                Console.WriteLine($"\n Caro Jogador, digite uma opção valida: ");
                OpcoesMap();
            }

            if (ChecarCaminho(opcao) && opcao < 5)
            {
                Movimentar(opcao);
                ChecarMapa();
            }

            else if (opcao == 5)
            {
                MostrarStatus();
                OpcoesMap();
            }

            else if (opcao == 6)
            {
                MostrarMochila();
                OpcoesMap();
            }
            else if (opcao == 7)
            {
                Mp1.MostrarMapa(P1.Passos);
                OpcoesMap();
            }

            else
            {
                Console.WriteLine($"\n Nenhum caminho disponivel para este lado" +
                                  $"\n Pressione qualquer tecla para continuar");
                Console.ReadKey();
                Console.Clear();
                OpcoesMap();
            };
        }

        private void MostrarMochila()
        {
            Console.WriteLine($"\n Mochila do jogador" +
                              $"\n Mochila Contem {P1.Itens.Count}");
            int cont = 1;
            foreach (Itens i in P1.Itens)
            {
                string value = $" {cont} - {i.NomeItem}";
                Console.WriteLine(value);
            }

            // validar opção
            Console.Write($"\n Digite o número no item para utiliza-lo!" +
                          $"\n Digite 0 para retornar a aventura!" +
                          $"\n Opção -> ");

            int opcao = int.Parse(Console.ReadLine());
            if (opcao == 0) { OpcoesMap(); }
            else if (opcao >= 1 && opcao <=3) { UtilizarItem(opcao - 1); }
            else if (opcao < 0 || opcao > 3)
            {
                Console.WriteLine("Você não encontra nada nessa parte da sua mochila");
                Console.WriteLine("Procure em outra parte");
            }

            Console.WriteLine(" Digite qualquer tecla para retorna ao mapa");
            Console.ReadKey();
            Console.Clear();
        }

        private void UtilizarItem(int opcao)
        {
            P1.SetNivel();
            if (P1.Itens[opcao].TipoItem == 1)
            {
                P1.HP += P1.Itens[opcao].PoderItem;
                Console.WriteLine($" Você Utilizou {P1.Itens[opcao].NomeItem}" +
                                  $" e recuperou {P1.Itens[opcao].PoderItem} de vida!");
                P1.Itens.RemoveAt(opcao);
            }
            else if (P1.Itens[opcao].TipoItem == 2)
            {
                P1.Ataque += P1.Itens[opcao].Ataque;
                Console.WriteLine($" Você Utilizou {P1.Itens[opcao].NomeItem}" +
                                  $" e Aumentou seu ataque em  {P1.Itens[opcao].PoderItem} !");
                P1.Itens.RemoveAt(opcao);
            }
            else if (P1.Itens[opcao].TipoItem == 3)
            {
                P1.Defesa += P1.Itens[opcao].Defesa;
                Console.WriteLine($" Você Utilizou {P1.Itens[opcao].NomeItem}" +
                                  $" Aumentou sua defesa em  {P1.Itens[opcao].PoderItem} !");
                P1.Itens.RemoveAt(opcao);
            };
        }

        private void MostrarStatus()
        {
            P1.SetNivel();
            string status = $@"
            Status do jogador:
            Nome - {P1.Nome}
            Vida - {P1.HP}
            Ataque - {P1.Ataque}
            Defesa - {P1.Defesa}
            Experiência - {P1.Experiencia}
            Level - {P1.Nivel}
            Pressione qualquer tecla para retornar ao mapa
            ";

            Console.WriteLine(status);
            Console.ReadKey();
            Console.Clear();
        }

        private void ChecarMapa()
        {
            int ocupante = Mp1.formatoMapa[P1.Localização[0],P1.Localização[1]];
            if (ocupante == 0)
            {
                Console.WriteLine("\n Tudo parece vazio" +
                                  "\n Não tem ninguem por perto");
                OpcoesMap();
            }

            if (ocupante == 4)
            {
                Console.WriteLine($"\n Você vê um brilho a frente");
                Thread.Sleep(1500);
                Console.WriteLine(" Apressado você corre em direção a luz");
                Console.Clear();
                Console.WriteLine(" Parabéns");
                Console.WriteLine(" Voce achou a saída");
                Console.WriteLine($" Você terminou com {P1.Experiencia} de experiência");
                Console.WriteLine(" Pressione Qualquer Tecla pra Fechar");
                Console.ReadKey();
            }

            if (ocupante == 1 || ocupante == 2 || ocupante == 3)
            {
                Console.WriteLine("\n Você encontrou um monstro");
                Monstros criadormob = new Monstros();

                if (ocupante == 2) { Mob = criadormob.Mob02(P1.Nivel); }
                else if (ocupante == 3) { Mob = criadormob.Mob03(P1.Nivel); }
                else { Mob = criadormob.Mob01(P1.Nivel); }

                OpcoesCombat();
            }
        }

        public void OpcoesCombat()
        {
            Console.WriteLine($"\n Você entrou em combate com o {Mob.Nome} ");
            Combate cbt = new Combate();
            Console.Write("\n Digite 1 para ir para Batalha ⚔" +
                              "\n Digite 2 para tentar Fugir \U0001F3C3" +
                              "\n Opção -> ");

            int op = int.Parse(Console.ReadLine());
            if (op == 2) TentarFugir();
            if (op == 1)
            {
                if (cbt.RealizarCombat(P1,Mob))
                {
                    P1.Experiencia += Mob.MobExperiencia;

                    Console.WriteLine($"\n Você ganhou {Mob.MobExperiencia} de experiência" +
                                      $"\n Seu nível é {P1.SetNivel()}");
                    ChanceLoot();
                    Console.WriteLine($"\n Pressione qualquer tecla para continuar sua aventura");
                    Console.ReadKey();
                    Console.Clear();
                    OpcoesMap();
                }

                else
                {
                    Console.WriteLine("\nVocê Faleceu! ");
                    Console.WriteLine("Pressione qualquer tecla para encerrar");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
            }
            Console.ReadKey();
        }

        private void ChanceLoot()
        {
            Random rdn = new Random();
            if (rdn.Next(0,1) == 0)
            {
                Itens item01 = new Itens().Loot();
                Console.WriteLine($"\n Parabéns!!! Você Achou um {item01.NomeItem}");
                P1.Itens.Add(item01);
            }
        }

        public bool ChecarCaminho(int opcao)
        {
            if (opcao == 1 && P1.Localização[1] <= 1) { return false; }
            if (opcao == 2 && P1.Localização[1] >= 9) { return false; }
            if (opcao == 3 && P1.Localização[0] >= 9) { return false; }
            if (opcao == 4 && P1.Localização[0] <= 1) { return false; }
            return true;
        }

        public void Movimentar(int opcao)
        {
            P1.Passos.Add(opcao);
            if (opcao == 1) { P1.Localização[0] --; return; }
            if (opcao == 2) { P1.Localização[0] ++; return; }
            if (opcao == 3) { P1.Localização[1] ++; return; }
            if (opcao == 4) { P1.Localização[1] --; return; }
        }

        public void TentarFugir()
        {
            Random random = new Random();
            int i = random.Next(1,3);
            if (i == 1)
            {
                Console.WriteLine("\nVocê conseguiu fugir do monstro! Retornou ao mapa.");
                OpcoesMap();
            }
            if (i == 2)
            {
                Console.WriteLine("\nVocê não conseguiu fugir. Termine a batalha.");
                Console.WriteLine($"Você entrou em combate com o {Mob.Nome} ");
                Combate cbt = new Combate();
                if (cbt.RealizarCombat(P1,Mob))
                {
                    P1.Experiencia += Mob.MobExperiencia;
                    P1.SetNivel();
                    Console.WriteLine($"Você ganhou {Mob.MobExperiencia} de experiência");
                    Console.WriteLine($"Você esta no nivel {P1.Nivel}");
                    ChanceLoot();
                    Console.WriteLine($"Pressione qualquer tecla para continuar sua aventura");
                    Console.ReadKey();
                    Console.Clear();
                    OpcoesMap();
                }

                else
                {
                    Console.WriteLine(" Você Faleceu!");
                    Console.WriteLine(" Pressione qualquer tecla para encerrar");
                    Console.ReadKey();
                    Environment.Exit(0);
                }

            }

        }
    }
}
