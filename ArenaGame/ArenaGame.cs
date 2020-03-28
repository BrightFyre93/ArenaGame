using System;

namespace ArenaGame
{
    class ArenaGame
    {
        readonly static Random random_number_generator = new Random();
        readonly static int[] snake = new int[] { 40, 60, 8, 12, 15, 25}; //First index is Lower Bound Health, Second Index is Upper Bound Health, Third index is Lower Bound Attack, Fourth Index is Upper Bound Attack, Fifth index is Lower Bound EXP, Sixth Index is Upper Bound EXP
        readonly static int[] dragon = new int[] { 70, 90, 6, 10, 25, 35}; 
        readonly static int[] scorpion = new int[] { 30, 50, 15, 25, 20, 30};

        static void Main()
        {
            int exp_hero = 0;
            int level_hero = 1;
            int level_monster;
            while (true)
            {
                Console.WriteLine("Would you like to start a new game? Input Y/Yes or N/No");
                string input_start = Console.ReadLine();
                if (input_start == "Yes" || input_start == "Y")
                {
                    int[] hero = SetHero(level_hero);
                    Console.WriteLine("Pick Level for Monster from 1 - 100:");
                    for (;;)
                    { 
                        try
                        {
                            level_monster = Convert.ToInt32(Console.ReadLine());
                            if (level_monster < 1 || level_monster > 100)
                            {
                                Console.WriteLine("Please input a number from 1 - 100.");
                            }
                            else
                            {
                                break;
                            }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Please enter a number between 1 - 100.");
                        }
                    }
                    
                    int[] monster = PickMonster(level_monster);
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
                            exp_hero += monster[2];
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
                int level_requirements = level_hero / 2 * ( 2 * 100 + (level_hero - 1) * 50); // Calculation of Sum of Arithmetic Progression with first term = 100 EXP and increase in EXP = 50 EXP
                if(level_requirements < exp_hero)
                {
                    exp_hero -= level_requirements;
                    level_hero += 1;
                }
            }
        }
        static int[] SetHero(int level)
         {
            int[] hero = new int[] { 100 * level, 10 * level }; //First index is Health, Second is Attack
            return hero;
         }

        static int[] PickMonster(int level)
        {
            int[] monster = new int[] { 0, 0, 0 }; //First index is Health, Second is Attack, Third is EXP
            while (true)
            {
                Console.WriteLine("Pick a Monster: ");
                Console.WriteLine($"No. - Name     - Health - Attack - Monster EXP  - For Level {level}");
                Console.WriteLine($"1   - Snake    - {snake[0]*level}     - {snake[1] * level} - {snake[2] * level}");
                Console.WriteLine($"2   - Dragon   - {dragon[0] * level}     - {dragon[1] * level} - {dragon[2] * level}");
                Console.WriteLine($"3   - Scorpion - {scorpion[0] * level}     - {scorpion[1] * level} - {scorpion[2] * level}");
                Console.WriteLine("Enter a number between 1 - 3:");
                try
                {
                    int selected_monster = Convert.ToInt32(Console.ReadLine());
                    if (selected_monster < 1 || selected_monster > 3)
                    {
                        Console.WriteLine("Please input a number from 1 - 3.");
                    }
                    else if(selected_monster == 1) //Snake
                    {
                        monster[0] = random_number_generator.Next(snake[0], snake[1]) * level;
                        monster[1] = random_number_generator.Next(snake[2], snake[3]) * level;
                        monster[2] = random_number_generator.Next(snake[4], snake[5]) * level;
                        break;
                    }
                    else if (selected_monster == 2) //Dragon
                    {
                        monster[0] = random_number_generator.Next(dragon[0], dragon[1]) * level;
                        monster[1] = random_number_generator.Next(dragon[2], dragon[3]) * level;
                        monster[2] = random_number_generator.Next(dragon[4], dragon[5]) * level;
                        break;
                    }
                    else if (selected_monster == 3) //Scorpion
                    {
                        monster[0] = random_number_generator.Next(scorpion[0], scorpion[1]) * level;
                        monster[1] = random_number_generator.Next(scorpion[2], scorpion[3]) * level;
                        monster[2] = random_number_generator.Next(scorpion[4], scorpion[5]) * level;
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
