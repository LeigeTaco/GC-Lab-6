using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Lab6
{

    class Program
    {

        static int Rand100(int max, Random rng)
        {
            max++;

            int[] oneHundo = new int[1000];

            for (int i = 0; i < 1000; i++)
            {

                oneHundo[i] = rng.Next(1, max);

            }

            for (int i = 0; i < 1000; i+=10)
            {

                for (int j = 0; j < 10; j++)
                {

                    //Console.Write($"{oneHundo[i+j], -3} "); //($"|{x,-8}|{Square(x),-11}|{Cube(x), -12}")

                }

                //Console.WriteLine();                        //Uncomment these lines for some cool matrix lookin' output

            }

            return oneHundo[rng.Next(0,999)];

        }

        static int ValidIntry(string prompt)        //I think I'm funny
        {

            int output = 0;
            bool invalid = true;

            Console.WriteLine(prompt);

            while(invalid)
            {

                try
                {

                    output = int.Parse(Console.ReadLine());

                    if (output < 3)
                    {

                        Console.WriteLine("Error: Dice cannot have less than 3 sides!");
                        Console.WriteLine("Please try a different number.");

                    }
                    else
                    {

                        invalid = false;

                    }

                }
                catch(NullReferenceException e)
                {

                    Console.WriteLine("A Known Entry Error, maybe...");
                    Console.WriteLine("Good job entering null, use a number this time.");

                }
                catch(OverflowException e)
                {

                    Console.WriteLine("Error: Die too large! Please be more reallistic.");

                }
                catch(FormatException e)
                {

                    Console.WriteLine("Error: Monkey at keyboard. Let someone who knows numbers operate.");

                }
                catch(Exception e)
                {

                    Console.WriteLine("Congrats, A'Keem, you broked it. Try again though.");

                }

            }

            return output;

        }

        static bool ContinueRolling()
        {

            string cont = "";

            Regex affirm = new Regex(@"^[yn]$");
            Regex yes = new Regex(@"[y]");
            Regex no = new Regex(@"[n]");

            Console.WriteLine("Would you like to roll the dice? (Y/N)");
            do
            {

                cont = Console.ReadLine();

            } while (!affirm.IsMatch(cont.ToLower()));

            if(yes.IsMatch(cont.ToLower()))
            {

                return true;

            }
            else
            {

                return false;

            }
            


        }

        static int CryptoRoll(int upperBound, RNGCryptoServiceProvider rng)
        {

            //This is from Stack Overflow, thanks guys

            byte[] singleByteBuf = new byte[1];

            int max = Byte.MaxValue - Byte.MaxValue % upperBound;

            //while (true)
            //{

                rng.GetBytes(singleByteBuf);
                int b = singleByteBuf[0];
                if (b < max)
                {

                    return b % upperBound + 1;

                }

            return b;

            //}

        }

        static void Main(string[] args)
        {

            RNGCryptoServiceProvider rngcsp = new RNGCryptoServiceProvider();
            Random rng = new Random();
            Console.WriteLine("Hello, would you like to play risk free gambling?");
            Console.WriteLine("You will be rolling dice with varying size of your choice as many times as you want.");

            while (ContinueRolling())
            {

                Console.WriteLine("Would you like to roll the dice?");

                int diceSize = ValidIntry("How big are the dice?");
                int roll1 = Rand100(diceSize, rng);
                Console.WriteLine($"Your first roll is: {roll1}, press any key to roll your second die...");
                Console.ReadKey();
                int roll2 = Rand100(diceSize, rng);
                //int roll1 = CryptoRoll(diceSize, rngcsp);    //These act wonky past a certain point
                //int roll2 = CryptoRoll(diceSize, rngcsp);    //Luckily, there are not a lot of dice above d20

                if(roll1 == roll2)
                {

                    if(roll1 == 1)
                    {

                        Console.WriteLine("Nice, Snake Eyes.");

                    }
                    else if(roll1 == 6)
                    {

                        Console.WriteLine("Nice, Box Cars.");

                    }
                    else
                    {

                        Console.WriteLine("Craps?");

                    }

                }

                Console.WriteLine($"{roll1} {roll2}");

            }

        }

    }

}
