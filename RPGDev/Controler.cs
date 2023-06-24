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

            menu = new Menu();
            int menuOpcao = menu.MenuInicial();
            if (menuOpcao == 1)
            {
                VerificarTipo(menuOpcao);
            }
            else if (menuOpcao == 2)
            {
                VerificarTipo(menuOpcao);
            }
            else if (menuOpcao == 3)
            {
                VerificarTipo(menuOpcao);
            }
            Mp1 = new Mapa();
            P1.Localização = Mp1.entrada;
            Console.Clear();

            Console.WriteLine("Você entra em uma Masmorra Escura");
            Thread.Sleep(1500);
            Console.WriteLine("Depois de andar um pouco, você olha pra trás e percebe que se perdeu");
            Thread.Sleep(1500);
            Console.WriteLine("Agora perdido, você precisa decidir");
            Thread.Sleep(1500);
            OpcoesMap();
            Console.ReadKey();
        }

        public void VerificarTipo(int opcao)
        {
            Console.WriteLine("\nDigite o Nome");
            string nome = Console.ReadLine();
            Console.WriteLine("Seu Personagem será: 1- Atacante/ 2- Defensor/ 3- Misto");
            int tipo = int.Parse(Console.ReadLine());
            if (tipo == 1) { P1 = new Player(nome, "GUERREIRO", tipo); }
            else if (tipo == 2) { P1 = new Player(nome, "MAGO", tipo); }
            else if (tipo == 3) { P1 = new Player(nome, "RANGER", tipo); }
        }

        public void OpcoesMap()
        {
            Console.WriteLine("\nDigite 1 para ir para o Norte");
            Console.WriteLine("Digite 2 para ir para o Sul");
            Console.WriteLine("Digite 3 para ir para o Leste");
            Console.WriteLine("Digite 4 para ir para o Oeste");
            Console.WriteLine("Digite 5 para ver status");
            Console.WriteLine("Digite 6 para ver Mochila");
            int opcao = int.Parse(Console.ReadLine());
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

            else
            {
                Console.WriteLine("Nenhum caminho disponivel para este lado");
                Console.WriteLine("Pressione qualquer tecla para continuar");
                Console.ReadKey();
                Console.Clear();
                OpcoesMap();
            };
        }

        private void MostrarMochila()
        {
            Console.WriteLine($"Mochila do jogador");
            Console.WriteLine($"Mochila Contem {P1.itens.Count}");
            int cont = 1;
            foreach (Itens i in P1.itens)
            {
                Console.WriteLine($"{cont} - {i.NomeItem}");
            }

            Console.WriteLine("Digite o numero no item para utiliza-lo!");
            Console.WriteLine("Digite 0 para retorna a aventura!");

            int opcao = int.Parse(Console.ReadLine());
            if (opcao == 0) { OpcoesMap(); }
            else if (opcao != 0) { UtilizarItem(opcao - 1); }

            Console.WriteLine("Digite qualquer tecla para retorna ao mapa");
            Console.ReadKey();
            Console.Clear();
        }

        private void UtilizarItem(int opcao)
        {
            if (P1.itens[opcao].TipoItem == 1)
            {
                P1.HP += P1.itens[opcao].PoderItem;
                Console.WriteLine($"Voce Utilizou {P1.itens[opcao].NomeItem} e recuperou {P1.itens[opcao].PoderItem} de vida!");
                P1.itens.RemoveAt(opcao);
            }
            else if (P1.itens[opcao].TipoItem == 2)
            {
                P1.Ataque += P1.itens[opcao].Ataque;
                Console.WriteLine($"Voce Utilizou {P1.itens[opcao].NomeItem} e Aumentou seu ataque em  {P1.itens[opcao].PoderItem} !");
                P1.itens.RemoveAt(opcao);
            }
            else if (P1.itens[opcao].TipoItem == 3)
            {
                P1.Defesa += P1.itens[opcao].Defesa;
                Console.WriteLine($"Voce Utilizou {P1.itens[opcao].NomeItem} Aumentou sua defesa em  {P1.itens[opcao].PoderItem} !");
                P1.itens.RemoveAt(opcao);
            };
        }

        private void MostrarStatus()
        {
            Console.WriteLine($"Status do jogador");
            Console.WriteLine($"Nome - {P1.Nome}");
            Console.WriteLine($"Vida - {P1.HP}");
            Console.WriteLine($"Ataque - {P1.Ataque}");
            Console.WriteLine($"Defesa - {P1.Defesa}");
            Console.WriteLine($"Experiência - {P1.Experiencia}");
            Console.WriteLine($"Level - {P1.Nivel}");
            Console.WriteLine($"Pressione qualquer tecla para retornar ao mapa");
            Console.ReadKey();
            Console.Clear();
        }

        private void ChecarMapa()
        {
            int ocupante = Mp1.formatoMapa[P1.Localização[0], P1.Localização[1]];
            if (ocupante == 0)
            {
                Console.WriteLine("Tudo parece vazio");
                Console.WriteLine("Não tem ninguem por perto");
                OpcoesMap();
            }

            if (ocupante == 4)
            {
                Console.WriteLine("Você vê um brilho a frente");
                Thread.Sleep(1500);
                Console.WriteLine("Apressado você corre em direção a luz");
                Console.Clear();
                Console.WriteLine("Parabéns");
                Console.WriteLine("Voce achou a saída");
                Console.WriteLine($"Você terminou com {P1.Experiencia} de experiência");
                Console.WriteLine("Pressione Qualquer Tecla pra Fechar");
                Console.ReadKey();
            }

            if (ocupante == 1 || ocupante == 2 || ocupante == 3)
            {
                Console.WriteLine("Você encontrou um monstro");
                Monstros criadormob = new Monstros();

                if (ocupante == 2) { Mob = criadormob.Mob02(P1.Nivel); }
                else if (ocupante == 3) { Mob = criadormob.Mob03(P1.Nivel); }
                else { Mob = criadormob.Mob01(P1.Nivel); }

                OpcoesCombat();
            }
        }

        public void OpcoesCombat()
        {
            Console.WriteLine($"\nVocê entrou em combate com o {Mob.Nome} ");
            Combate cbt = new Combate();
            Console.WriteLine("digite 1 para ir para Batalha");
            Console.WriteLine("digite 2 para tentar Fugir");
            int op = int.Parse(Console.ReadLine());
            if (op == 2) TentarFugir();
            if (op == 1)
            {
                if (cbt.RealizarCombat(P1, Mob))
                {
                    P1.Experiencia += Mob.MobExperiencia;

                    Console.WriteLine($"\nVocê ganhou {Mob.MobExperiencia} de experiência");
                    Console.WriteLine($"Seu nível é {P1.SetNivel()}");
                    ChanceLoot();
                    Console.WriteLine($"\nPressione qualquer tecla para continuar sua aventura");
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
            if (rdn.Next(0, 1) == 0)
            {
                Itens item01 = new Itens().Loot();
                Console.WriteLine($"\nParabéns!!! Você Achou um {item01.NomeItem}");
                P1.itens.Add(item01);
            }
        }

        public bool ChecarCaminho(int opcao)
        {
            if (opcao == 1 && P1.Localização[1] >= 9) { return false; }
            if (opcao == 2 && P1.Localização[1] <= 1) { return false; }
            if (opcao == 3 && P1.Localização[0] >= 9) { return false; }
            if (opcao == 4 && P1.Localização[0] <= 1) { return false; }
            return true;
        }

        public void Movimentar(int opcao)
        {
            if (opcao == 1) { P1.Localização[1] = P1.Localização[1] + 1; return; }
            if (opcao == 2) { P1.Localização[1] = P1.Localização[1] - 1; return; }
            if (opcao == 3) { P1.Localização[0] = P1.Localização[0] + 1; return; }
            if (opcao == 4) { P1.Localização[0] = P1.Localização[0] - 1; return; }
        }

        public void TentarFugir()
        {
            Random random = new Random();
            int i = random.Next(1, 3);
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
                if (cbt.RealizarCombat(P1, Mob))
                {
                    P1.Experiencia += Mob.MobExperiencia;

                    Console.WriteLine($"Você ganhou {Mob.MobExperiencia}");
                    ChanceLoot();
                    Console.WriteLine($"Pressione qualquer tecla para continuar sua aventura");
                    Console.ReadKey();
                    Console.Clear();
                    OpcoesMap();
                }

                else
                {
                    Console.WriteLine(" Você Faleceu!");
                    Console.WriteLine("Pressione qualquer tecla para encerrar");
                    Console.ReadKey();
                    Environment.Exit(0);
                }

            }

        }
    }
}
