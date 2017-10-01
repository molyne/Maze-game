﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Lab4
{
    class ExitRoom : Square , IPrintSymbol, IRoomInfo
    {
        public void PrintRoomInfo()
        {
            Console.WriteLine("You found your way out of the maze!");

          
        }

        public override string PrintSymbol()
        {
            return "X";
        }

        public void PrintHighScore (int counter)
        {
            if (counter <= 34)
            {
                Console.WriteLine("You are amazing, u took the shortest way possible!");
            }
            else if (counter > 34 && counter < 40)
            {
                Console.WriteLine("It's good, but you can find a shorter way. Try again!");
            }
            else
                Console.WriteLine("Do you have a bad local sense? Because your score is reeeeally bad. Try again!");

            Console.WriteLine($"You took {counter} steps.");
        }

     

    }
}
