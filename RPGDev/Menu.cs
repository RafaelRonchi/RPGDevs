using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGDev
{
    internal class Menu
    {
        public int MenuInicial()
        {
            Console.WriteLine("Seja Bem Vindo a RataZona");
            Console.Write($"\n Criação de Personagem" +
                              $"\n Digite uma opção para começar" +
                              $"\n 1 - Criar Personagem" +
                              $"\n 2 - Sair" +
                              $"\n Opção -> ");
            string opcao = Console.ReadLine();
            switch (opcao)
            {
                case "1":
                    Console.Write($"\n Escolha a Classe do seu Personagem" +
                                      $"\n Digite a opção desejada" +
                                      $"\n 1 - Guerreiro" +
                                      $"\n 2 - Mago" +
                                      $"\n 3 - Ranger" +
                                      $"\n Opção -> ");
                    string classe = Console.ReadLine();
                    while (classe != "1" && classe != "2" && classe != "3")
                    {
                        Console.Write($"\n Caro Jogador, digite uma opção valida: ");
                        classe = Console.ReadLine();
                    }

                    switch (classe)
                    {
                        case "1":
                            return 1;
                        case "2":
                            return 2;
                        case "3":
                            return 3;
                    }
                    return 0;

                case "2":
                    return 4;

                default:
                    Console.WriteLine("\nOpção inválida! Tente novamente.");
                    return MenuInicial();
            }
        }
    }
}

