﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Lab4
{

    public class Map 
    {

        const int COLUMNS = 8, ROWS = 20;

        private int row;
        private int column;

        public int Row { get { return row; } set { row = value; } }
        public int Column { get { return column; } set { column = value; } }

        public Square [,] squares = new Square [COLUMNS, ROWS];

      

        Player p = new Player();

        int positionXPlayer = 1; //spelarens position
        int positionYPlayer = 1; //spelaren position
        bool updateMap = true;

        //int counter = 0;
        int keys = 0;
 
        //private bool gotKey = false;

        RoomWithMonster m = new RoomWithMonster();
        EmptyRoom e = new EmptyRoom();
        RoomWithKey k = new RoomWithKey();

        public void PrintMap()
        {


            do
            {

                for (int column = 0; column < squares.GetLength(0); column++)
                {
                    for (int row = 0; row < squares.GetLength(1); row++)
                    {

                        if (row == 4 && column == 4 || row == 13 && column == 4 || column == 6 && row == 8)
                        {
                            squares[column, row] = new Door();
                           
                        }

                        else if (row == 0 || row == ROWS -1 || column == 0 || column == COLUMNS -1 || row == 8 || column ==4)
                        {

                            squares[column, row] = new Wall();
                        }  
                        else if (row == 18 && column== 1)
                        {
                            squares[column, row] = new ExitRoom();
                        }
                        else if (row == 13 && column == 1)
                        {
                            squares[column, row] = new EmptyRoom();
                        }
                        else if (row ==1 && column == 3 || row == 1 && column==6 || row == 14 && column == 6)
                        {
                            squares[column, row] = new RoomWithKey();
                        }
                        else if (row == 13 && column ==6 || row ==6 && column==5 || row == 11 && column ==2)
                        {
                            squares[column, row] = new RoomWithMonster();
                        }
           
                        else
                        {
                            squares[column, row] = new Floor(); 
                        }
                        if (positionXPlayer == row && positionYPlayer == column)
                        {
                            Player p = new Player();

                           Console.Write( p.PrintSymbol());

                            if (squares[column, row] is RoomWithMonster)
                            {
                                
                                m.EnterMonsterRoom= true;

                                //enterMonsterRoom = true;                          
                            }
                            if (squares[column, row] is EmptyRoom)
                            {

                                e.EnterEmptyRoom = true;
                            }
                            if (squares[column, row] is RoomWithKey)
                            {
                                k.EnterRoomWithKey = true;
                                keys = 1;
                                
                            }

                        }

                        else
                            Console.Write(squares[column, row].PrintSymbol());
                        
                    }

                    Console.WriteLine(" ");
                }
                
                Console.Write("Moves: "+m.Counter); //här kan man lägga grejer utan att det försvinner
                Console.Write(" Keys: "+keys);
                Console.WriteLine();


                m.CheckRoom();
                e.CheckRoom();
                k.CheckRoom();
                

           
                ConsoleKeyInfo move = Console.ReadKey();

                switch (move.Key) //lägg till hinder här också och dörrar och tomma rum. I en metod typ ispossible to move?
                {
                    case ConsoleKey.D:
                        {
                            if (squares[positionYPlayer, positionXPlayer +1] is Wall)

                                break;
                            if (squares[positionYPlayer, positionXPlayer +1] is Door)

                            {
                                if (k.GotKey)
                                    positionXPlayer += 1;
                                k.GotKey = false;
                                keys = 0;
                                break;
                            }
                            else
                                positionXPlayer += 1;
                            m.Counter++;
                            break;
                        }

                    case ConsoleKey.A:

                        {
                            if (squares[positionYPlayer, positionXPlayer - 1] is Wall)

                                break;

                            else
                                positionXPlayer -= 1;
                                m.Counter++;
                        }
                        break;

                    case ConsoleKey.W:

                        if (squares[positionYPlayer-1, positionXPlayer] is Wall)
                        {
                            break;
                        }

                        if (squares[positionYPlayer + -1, positionXPlayer] is Door)

                        {
                            if (k.GotKey)
                                positionYPlayer -= 1;
                            k.GotKey = false;
                            keys = 0;
                            break;
                        }


                        else
                            positionYPlayer -= 1;
                            m.Counter++;
                        break;

                    case ConsoleKey.S:

                        if (squares[positionYPlayer + 1, positionXPlayer] is Wall)

                            break;
                        if (squares[positionYPlayer + 1, positionXPlayer] is Door)

                        {   if (k.GotKey)
                                positionYPlayer += 1;
                            k.GotKey = false;
                            keys = 0;
                            break;
                        }
                        else
                            positionYPlayer += 1;
                             m.Counter++;
                        break;
                }

                Console.Clear();
            } while (!(squares[positionYPlayer , positionXPlayer] is ExitRoom));

            Console.WriteLine("You found your way out of the maze!");

            if (m.Counter <= 34)
            {
                Console.WriteLine("You are amazing, u took the shortest way possible!");
            }
            else if (m.Counter > 34 && m.Counter<40)
            {
                Console.WriteLine("It's good, but you can find a shorter way. Try again!");
            }
            else
                Console.WriteLine("Do you have a bad local sense? Because your score is reeeeally bad. Try again!");

            Console.WriteLine($"You took {m.Counter} steps.");
            Console.ReadKey();
        }
    }
}
