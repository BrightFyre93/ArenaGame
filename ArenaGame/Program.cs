using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGame
{
    class Program
    {
        readonly static Random random_number_generator = new Random();
        readonly static int[] snake = new int[] { 50, 10 }; //First index is Health, Second is Attack
        readonly static int[] dragon = new int[] { 80, 8 }; //First index is Health, Second is Attack
        readonly static int[] scorpion = new int[] { 40, 20 }; //First index is Health, Second is Attack

        static void Main()
        {
            while (true)
            {
                Console.WriteLine("Would you like to start a new game? Input Y/Yes or N/No");
                string input_start = Console.ReadLine();
                if (input_start == "Yes" || input_start == "Y")
                {
                    int[] hero = ResetGame();
                    int[] monster = PickMonster();
                    int[] damages; //Array to save the damage being done to hero and monster
                    while (hero[0] > 0)
                    {
                        Console.WriteLine($"Current HP:\nHero - {hero[0]} \nMonster - {monster[0]}");
                        Console.WriteLine("Choose whether to Attack/attack or Heal/heal or Defend/defend or Retreat/retreat: ");
                        string input_function = Console.ReadLine();
                        if (input_function == "Attack" || input_function == "attack")
                        {
                            damages = FuncAttack(hero, monster);
                        }
                        else if (input_function == "Defend" || input_function == "defend")
                        {
                            damages = FuncDefend(hero, monster);
                        }
                        else if (input_function == "Heal" || input_function == "heal")
                        {
                            damages = FuncHeal(hero, monster);
                        }
                        else if (input_function == "Retreat" || input_function == "retreat")
                        {
                            if (random_number_generator.Next(101) > 5)
                            {
                                Console.WriteLine("Hero successfully retreated");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Hero retreat failed.");
                                damages = FuncFailedRetreat(hero, monster);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Please enter a valid function.");
                            continue;
                        }
                        hero[0] = damages[0];
                        monster[0] = damages[1];
                        if(monster[0]<=0)
                        {
                            Console.WriteLine("Congrats you've won!");
                            Console.ReadKey();
                            break;
                        }
                    }
                    
                }
                else if (input_start == "No" || input_start == "N")
                {
                    Console.WriteLine("Hope you have a good day!");
                    Console.ReadKey();
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter a valid input.");
                }
            }
        }
        static int[] ResetGame()
         {
            int[] hero = new int[] { 100, 10 }; //First index is Health, Second is Attack
            return hero;
         }

        static int[] PickMonster()
        {
            int[] monster = new int[] { 0, 0 }; //First index is Health, Second is Attack
            while (true)
            {
                Console.WriteLine("Pick a Monster: ");
                 Console.WriteLine("No. - Name     - Health - Attack");
                Console.WriteLine($"1   - Snake    - {snake[0]}     - {snake[1]}");
                Console.WriteLine($"2   - Dragon   - {dragon[0]}     - {dragon[1]}");
                Console.WriteLine($"3   - Scorpion - {scorpion[0]}     - {scorpion[1]}");
                Console.WriteLine("Enter a number between 1 - 3:");
                try
                {
                    int selected_monster = Convert.ToInt32(Console.ReadLine());
                    if (selected_monster < 1 || selected_monster > 3)
                    {
                        Console.WriteLine("Please input a number from 1-3.");
                    }
                    else if(selected_monster == 1) //Snake
                    {
                        monster[0] = snake[0];
                        monster[1] = snake[1];
                        break;
                    }
                    else if (selected_monster == 2) //Dragon
                    {
                        monster[0] = dragon[0];
                        monster[1] = dragon[1];
                        break;
                    }
                    else if (selected_monster == 3) //Scorpion
                    {
                        monster[0] = scorpion[0];
                        monster[1] = scorpion[1];
                        break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please input a number from 1-3.");
                }
            }
            return monster;
        }
        static int[] FuncAttack(int[] hero, int[] monster)
        {
            int[] damage_dealt = new int[] { hero[0] - monster[1], monster[0] - hero[1] }; // Index 1 - New Hero Health Index 2 - New Monster Health
            Console.WriteLine($"Hero did {hero[1]} damage to Monster.");
            Console.WriteLine($"Monster did {monster[1]} damage to Hero.");
            return damage_dealt;
        }
        static int[] FuncDefend(int[] hero, int[] monster)
        {
            int[] damage_dealt = new int[] { hero[0] - 1, monster[0] }; // Index 1 - New Hero Health Index 2 - New Monster Health
            Console.WriteLine($"Hero defended.");
            Console.WriteLine("Monster did 1 damage to Hero.");
            return damage_dealt;
        }
        static int[] FuncHeal(int[] hero, int[] monster)
        {
            int[] damage_dealt = new int[] { hero[0] + hero[1] - monster[1], monster[0]}; // Index 1 - New Hero Health Index 2 - New Monster Health
            Console.WriteLine($"Hero healed his/her health by {hero[1]} points.");
            Console.WriteLine($"Monster did {monster[1]} damage to Hero.");
            return damage_dealt;
        }
        static int[] FuncFailedRetreat(int[] hero, int[] monster)
        {
            int[] damage_dealt = new int[] { hero[0] -monster[1], monster[0] }; // Index 1 - New Hero Health Index 2 - New Monster Health
            Console.WriteLine($"Monster did {monster[1]} damage to Hero.");
            return damage_dealt;
        }
    }
}
