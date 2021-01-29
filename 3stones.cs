using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApp16
{
    class Program
    {
        static void Main(string[] args)
        {


            Console.SetWindowSize(78, 20);

            Console.SetCursorPosition(20, 3);
            string stones = @" 
       ██████╗     ███████╗████████╗ ██████╗ ███╗   ██╗███████╗███████╗
       ╚════██╗    ██╔════╝╚══██╔══╝██╔═══██╗████╗  ██║██╔════╝██╔════╝
        █████╔╝    ███████╗   ██║   ██║   ██║██╔██╗ ██║█████╗  ███████╗
        ╚═══██╗    ╚════██║   ██║   ██║   ██║██║╚██╗██║██╔══╝  ╚════██║
       ██████╔╝    ███████║   ██║   ╚██████╔╝██║ ╚████║███████╗███████║
       ╚═════╝     ╚══════╝   ╚═╝    ╚═════╝ ╚═╝  ╚═══╝╚══════╝╚══════╝
                                                                
";
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(stones);
            Console.SetCursorPosition(21, 13);
            Console.WriteLine("   Press 'ENTER' to play the game");
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Enter)
            {


                baslat bs = new baslat();
                Console.BackgroundColor = ConsoleColor.DarkCyan;
                Console.Clear();
                Thread game = new Thread(new ThreadStart(bs.dogame));
                game.Start();




            }
        }
    }
    class baslat
    {
        public void dogame()
        {
            int game = 0;
            Console.WriteLine("Press 1  for classic game : ");
            Console.WriteLine("Press 2  for deluxe game : ");
            game = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            if (game == 1)
            {

                Console.SetWindowSize(45, 10);
                //satır ve süütnları dolaşarak count tut 2 li ardışık bulursan etrafına koy,(dışarıya çıkmamasını engelle),kazanmasını sağlamaya çalış
                int[,] walls = new int[8, 8] {{0,0,0,0,0,0,0,0 },//0=dot
                                          {0,0,0,0,0,0,0,0 },//1=wall
                                          {0,0,0,0,0,0,0,0 },//2=Human
                                          {0,0,0,0,0,0,0,0 },//3=Computer
                                          {0,0,0,0,0,0,0,0 },//4 = işleme girmeyen h
                                          {0,0,0,0,0,0,0,0 },//5= işleme girmeyen c
                                          {0,0,0,0,0,0,0,0 },
                                          {0,0,0,0,0,0,0,0 }};

                int a = 0;//x coordinate of walls
                int b = 0;//y coordinate of walls

                int x = 1;//x coordinate of cursor
                int y = 1;//y coordinate of cursor

                int randomcursorx = 0;//random x cordinate for computer
                int randomcursory = 0;//random y cordinate for computer

                int step = 0;
                int sıra = 0;

                int accept = 0;//engelleme ihtimaline bakıyor
                int accept2 = 0;//sol sağ atama kontrolü

                int scorehuman = 0;
                int scorecomputer = 0;

                int leftorright = 0;
                int ortanokta = 0;

                Random rnd = new Random();

                for (int i = 0; i < 8; i++)//randomly walls
                {
                    do
                    {
                        a = rnd.Next(0, 8);
                        b = rnd.Next(0, 8);

                    } while (walls[a, b] == 1);
                    walls[a, b] = 1;

                }

                for (int i = 0; i < walls.GetLength(0); i++)//write for dots
                {
                    for (int j = 0; j < walls.GetLength(1); j++)
                    {
                        Console.SetCursorPosition(i + 1, j + 1);
                        Console.Write(".");
                    }
                }

                for (int i = 0; i < 8; i++)//find walls and write walls on screen
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (walls[i, j] == 1)
                        {
                            Console.SetCursorPosition(j + 1, i + 1);
                            Console.Write("#");
                        }
                    }
                }

                Console.SetCursorPosition(0, 0);
                Console.WriteLine("+--------+");

                for (int i = 0; i < 8; i++)
                {
                    Console.WriteLine("|");
                }

                Console.Write("+--------+");

                for (int i = 0; i < 8; i++)
                {
                    Console.SetCursorPosition(9, i + 1);
                    Console.WriteLine("|");
                }
                Console.SetCursorPosition(x, y);
                Console.SetCursorPosition(20, 0);
                Console.Write("Step : " + step);
                Console.SetCursorPosition(x, y);

                sıra = rnd.Next(1, 4);//1 computer-2human

                if (sıra == 1)
                {

                    Console.SetCursorPosition(20, 1);
                    Console.Write("Turn : C");
                    Console.SetCursorPosition(x, y);

                    do
                    {
                        a = rnd.Next(0, 8);
                        b = rnd.Next(0, 8);

                    } while (walls[a, b] != 0);
                    walls[a, b] = 3;
                    Console.SetCursorPosition(b + 1, a + 1);
                    Console.Write("C");
                    Console.SetCursorPosition(x, y);
                }

                Console.SetCursorPosition(20, 1);
                Console.Write("Turn : H");
                Console.SetCursorPosition(x, y);


                while (scorehuman != 1 && scorecomputer != 1)//move to somewhere on screen
                {
                    ConsoleKeyInfo input;

                    if (Console.KeyAvailable == true)
                    {
                        input = Console.ReadKey();

                        if (input.Key == ConsoleKey.DownArrow)//DOWN
                        {
                            if (y + 1 <= 8)
                            {
                                Console.SetCursorPosition(x, y + 1);
                                y++;
                            }
                        }

                        else if (input.Key == ConsoleKey.UpArrow)//UP
                        {
                            if (y > 1)
                            {
                                Console.SetCursorPosition(x, y - 1);
                                y--;
                            }
                        }

                        else if (input.Key == ConsoleKey.RightArrow)//RİGHT
                        {
                            if (x < 8)
                            {

                                Console.SetCursorPosition(x + 1, y);
                                x++;
                            }
                        }

                        else if (input.Key == ConsoleKey.LeftArrow)//LEFT
                        {
                            if (x > 1)
                            {
                                Console.SetCursorPosition(x - 1, y);
                                x--;
                            }
                        }

                        if (input.Key == ConsoleKey.Spacebar)//SPACE
                        {

                            Console.SetCursorPosition(20, 1);
                            Console.Write("Turn : H");
                            Console.SetCursorPosition(x, y);

                            if (walls[y - 1, x - 1] == 0)
                            {
                                walls[y - 1, x - 1] = 2;

                                if (x > 1 && x < 8 && walls[y - 1, x] == 2 && walls[y - 1, x - 2] == 2)//sol sağ kontolü
                                {
                                    walls[y - 1, x] = 4; walls[y - 1, x - 2] = 4; walls[y - 1, x - 1] = 4;
                                    scorehuman++;
                                }

                                if (x > 2 && walls[y - 1, x - 2] == 2 && walls[y - 1, x - 3] == 2)//iki sol
                                {
                                    walls[y - 1, x - 3] = 4; walls[y - 1, x - 2] = 4; walls[y - 1, x - 1] = 4;
                                    scorehuman++;
                                }

                                if (x < 7 && walls[y - 1, x] == 2 && walls[y - 1, x + 1] == 2)//iki sağ kontrolü
                                {
                                    walls[y - 1, x] = 4; walls[y - 1, x + 1] = 4; walls[y - 1, x - 1] = 4;
                                    scorehuman++;
                                }//-------------puanlama

                                if (walls[y - 1, x - 1] == 2 && y < 8 && walls[y, x - 1] == 2 && y != 1 && walls[y - 2, x - 1] == 2)//h ortada ise
                                {
                                    walls[y - 1, x - 1] = 4; walls[y, x - 1] = 4; walls[y - 2, x - 1] = 4;
                                    scorehuman++;
                                }

                                if (walls[y - 1, x - 1] == 2 && y > 2 && walls[y - 2, x - 1] == 2 && walls[y - 3, x - 1] == 2)//en allttaki h
                                {
                                    walls[y - 1, x - 1] = 4; walls[y - 2, x - 1] = 4; walls[y - 3, x - 1] = 4;
                                    scorehuman++;
                                }

                                if (walls[y - 1, x - 1] == 2 && y < 7 && walls[y, x - 1] == 2 && walls[y + 1, x - 1] == 2)//en üstteki h ise
                                {
                                    walls[y - 1, x - 1] = 4; walls[y, x - 1] = 4; walls[y + 1, x - 1] = 4;
                                    scorehuman++;
                                }//------------sağ çapraz

                                if (walls[y - 1, x - 1] == 2 && y < 7 && x < 7 && walls[y, x] == 2 && walls[y + 1, x + 1] == 2)//en üst
                                {
                                    walls[y - 1, x - 1] = 4; walls[y, x] = 4; walls[y + 1, x + 1] = 4;
                                    scorehuman++;
                                }

                                if (x != 1 && x != 8 && y != 1 && y != 8 && walls[y - 1, x - 1] == 2 && walls[y, x] == 2 && walls[y - 2, x - 2] == 2)//orta
                                {
                                    walls[y - 1, x - 1] = 4; walls[y, x] = 4; walls[y - 2, x - 2] = 4;
                                    scorehuman++;
                                }

                                if (x > 2 && y > 2 && walls[y - 1, x - 1] == 2 && walls[y - 3, x - 3] == 2 && walls[y - 2, x - 2] == 2)//en alt
                                {
                                    walls[y - 1, x - 1] = 4; walls[y - 3, x - 3] = 4; walls[y - 2, x - 2] = 4;
                                    scorehuman++;
                                }//sol çapraz

                                if (x > 2 && y < 7 && walls[y - 1, x - 1] == 2 && walls[y, x - 2] == 2 && walls[y + 1, x - 3] == 2)//en üst
                                {
                                    walls[y - 1, x - 1] = 4; walls[y, x - 2] = 4; walls[y + 1, x - 3] = 4;
                                    scorehuman++;
                                }

                                if (x != 1 && x != 8 && y != 1 && y != 8 && walls[y - 1, x - 1] == 2 && walls[y - 2, x] == 2 && walls[y, x - 2] == 2)//orta
                                {
                                    walls[y - 1, x - 1] = 4; walls[y - 2, x] = 4; walls[y, x - 2] = 4;
                                    scorehuman++;
                                }

                                if (y > 2 && x < 7 && walls[y - 1, x - 1] == 2 && walls[y - 2, x] == 2 && walls[y - 3, x + 1] == 2)//en alt
                                {
                                    walls[y - 1, x - 1] = 4; walls[y - 2, x] = 4; walls[y - 3, x + 1] = 4;
                                    scorehuman++;
                                }

                                if (accept == 0)//accept = kazanma ihtimali
                                {
                                    for (int i = 0; i < walls.GetLength(0); i++)
                                    {
                                        for (int j = 0; j < walls.GetLength(1); j++)
                                        {
                                            if (walls[i, j] == 3 && j < 7 && walls[i, j + 1] == 3)//find 2 c -----ARKA ARKAYA-----
                                            {
                                                if (j != 0 && j < 6 && accept2 == 0 && walls[i, j - 1] == 0 && walls[i, j + 2] == 0)//sol sağ dışarı çıkma kontroluü
                                                {
                                                    leftorright = rnd.Next(1, 3);
                                                    if (leftorright == 1)
                                                    {
                                                        walls[i, j - 1] = 3;
                                                        walls[i, j] = 5; walls[i, j + 1] = 5; walls[i, j - 1] = 5;//W
                                                        accept++;
                                                    }
                                                    else
                                                    {
                                                        walls[i, j + 2] = 3;
                                                        walls[i, j] = 5; walls[i, j + 1] = 5; walls[i, j + 2] = 5;//W
                                                        accept++;
                                                    }
                                                    accept2 = 1;
                                                    scorecomputer++;
                                                }

                                                if (j != 0 && accept2 == 0 && walls[i, j - 1] == 0)//solun dışarı çıkma ihtimali
                                                {
                                                    walls[i, j - 1] = 3;
                                                    walls[i, j] = 5; walls[i, j + 1] = 5; walls[i, j - 1] = 5;//W
                                                    accept++;
                                                    accept2 = 1;
                                                    scorecomputer++;
                                                }

                                                if (j < 6 && accept2 == 0 && walls[i, j + 2] == 0)//sağın dışarı çıkma ihtimali
                                                {
                                                    walls[i, j + 2] = 3;
                                                    walls[i, j] = 5; walls[i, j + 1] = 5; walls[i, j + 2] = 5;//W
                                                    accept++;
                                                    accept2 = 1;
                                                    scorecomputer++;
                                                }
                                            }

                                            if (walls[i, j] == 3 && j < 6 && walls[i, j + 2] == 3 && accept2 == 0 && walls[i, j + 1] == 0)//find 2 c -----arasın boşluk-----
                                            {
                                                walls[i, j + 1] = 3;
                                                walls[i, j] = 5; walls[i, j + 1] = 5; walls[i, j + 2] = 5;//W
                                                accept++;
                                                accept2 = 1;
                                                scorecomputer++;
                                            }

                                            if (walls[i, j] == 3 && i < 6 && walls[i + 1, j] == 3 && i != 0 && walls[i - 1, j] == 0 && walls[i + 2, j] == 0 && accept2 == 0)//find 2 Hc -----ALT ALTA-----üst alt boş
                                            {
                                                leftorright = rnd.Next(1, 3);
                                                if (leftorright == 1)
                                                {
                                                    walls[i - 1, j] = 3;//W
                                                    walls[i, j] = 5; walls[i - 1, j] = 5; walls[i + 1, j] = 5;
                                                    accept++;
                                                }
                                                else
                                                {
                                                    walls[i, j + 2] = 3;
                                                    walls[i, j] = 5; walls[i, j + 1] = 5; walls[i, j + 2] = 5;//W
                                                    accept++;
                                                }
                                                accept2 = 1;
                                                scorecomputer++;
                                            }

                                            if (walls[i, j] == 3 && i < 6 && walls[i + 1, j] == 3 && walls[i + 2, j] == 0 && accept2 == 0)//aşşağısı boş
                                            {
                                                walls[i + 2, j] = 3;
                                                walls[i, j] = 5; walls[i + 1, j] = 5; walls[i + 2, j] = 5;
                                                accept++;
                                                accept2 = 1;
                                                scorecomputer++;
                                            }

                                            if (walls[i, j] == 3 && y < 7 && i < 7 && walls[i + 1, j] == 3 && i != 0 && walls[i - 1, j] == 0 && accept2 == 0)//yukarısı boş
                                            {
                                                walls[i - 1, j] = 3;
                                                walls[i, j] = 5; walls[i - 1, j] = 5; walls[i + 1, j] = 5;
                                                accept++;
                                                accept2 = 1;
                                                scorecomputer++;
                                            }

                                            if (walls[i, j] == 3 && i < 6 && walls[i + 2, j] == 3 && accept2 == 0 && walls[i + 1, j] == 0)
                                            {
                                                walls[i + 1, j] = 3;
                                                walls[i, j] = 5; walls[i + 2, j] = 5; walls[i + 1, j] = 5;
                                                accept++;
                                                accept2 = 1;
                                                scorecomputer++;
                                            }//sağ çapraz

                                            if (i > 0 && i < 6 && j < 0 && j < 6 && walls[i, j] == 3 && walls[i + 1, j + 1] == 3 && walls[i - 1, j - 1] == 0 && walls[i + 2, j + 2] == 0 && accept2 == 0)//iki taraf boş
                                            {
                                                leftorright = rnd.Next(1, 3);
                                                if (leftorright == 1)
                                                {
                                                    walls[i - 1, j - 1] = 3;
                                                    walls[i, j] = 5; walls[i + 1, j + 1] = 5; walls[i - 1, j - 1] = 5;
                                                    accept2 = 1;
                                                    accept++;
                                                    scorecomputer++;
                                                }
                                                else
                                                {
                                                    walls[i + 2, j + 2] = 3;
                                                    walls[i, j] = 5; walls[i + 1, j + 1] = 5; walls[i + 2, j + 2] = 5;
                                                    accept2 = 1;
                                                    accept++;
                                                    scorecomputer++;
                                                }
                                            }

                                            if (i > 0 && i < 6 && j < 0 && j < 6 && walls[i, j] == 3 && walls[i + 1, j + 1] == 3 && walls[i - 1, j - 1] != 0 && walls[i + 2, j + 2] == 0 && accept2 == 0)//sağ alt boş
                                            {
                                                walls[i + 2, j + 2] = 3;
                                                walls[i, j] = 5; walls[i + 1, j + 1] = 5; walls[i + 2, j + 2] = 5;
                                                accept2 = 1;
                                                accept++;
                                                scorecomputer++;
                                            }

                                            if (i > 0 && i < 6 && j < 0 && j < 6 && walls[i, j] == 3 && walls[i + 1, j + 1] == 3 && walls[i - 1, j - 1] == 0 && walls[i + 2, j + 2] != 0 && accept2 == 0)//sol üst boş
                                            {
                                                walls[i - 1, j - 1] = 3;
                                                walls[i, j] = 5; walls[i + 1, j + 1] = 5; walls[i - 1, j - 1] = 5;
                                                accept2 = 1;
                                                accept++;
                                                scorecomputer++;
                                            }

                                            if (i > 0 && i < 7 && j > 0 && j < 7 && walls[i, j] == 0 && walls[i - 1, j - 1] == 3 && walls[i + 1, j + 1] == 3 && accept2 == 0)//ortada
                                            {
                                                walls[i, j] = 3;
                                                walls[i, j] = 5; walls[i - 1, j - 1] = 5; walls[i + 1, j + 1] = 5;
                                                accept2 = 1;
                                                accept++;
                                                scorecomputer++;
                                            }//SOL ÇAPRAZZ

                                            if (i > 0 && i < 6 && j > 1 && j < 7 && walls[i, j] == 3 && walls[i + 1, j - 1] == 3 && walls[i - 1, j + 1] == 0 && walls[i + 2, j - 2] == 0 && accept2 == 0)//2 side boş
                                            {
                                                leftorright = rnd.Next(1, 3);
                                                if (leftorright == 1)
                                                {
                                                    walls[i - 1, j + 1] = 3;
                                                    walls[i, j] = 5; walls[i + 1, j - 1] = 5; walls[i - 1, j + 1] = 5;
                                                    accept2 = 1;
                                                    accept++;
                                                    scorecomputer++;
                                                }
                                                else
                                                {
                                                    walls[i + 2, j - 2] = 3;
                                                    walls[i, j] = 5; walls[i + 1, j - 1] = 5; walls[i + 2, j - 2] = 5;
                                                    accept2 = 1;
                                                    accept++;
                                                    scorecomputer++;
                                                }
                                            }

                                            if (i > 0 && i < 6 && j > 1 && j < 7 && walls[i, j] == 3 && walls[i + 1, j - 1] == 3 && walls[i - 1, j + 1] != 0 && walls[i + 2, j - 2] == 0 && accept2 == 0)//sol alt boş
                                            {
                                                walls[i + 2, j - 2] = 3;
                                                walls[i, j] = 5; walls[i + 1, j - 1] = 5; walls[i + 2, j - 2] = 5;
                                                accept2 = 1;
                                                accept++;
                                                scorecomputer++;
                                            }

                                            if (i > 0 && i < 6 && j > 1 && j < 7 && walls[i, j] == 3 && walls[i + 1, j - 1] == 3 && walls[i - 1, j + 1] != 0 && walls[i + 2, j - 2] != 0 && accept2 == 0)//sağ üst boş
                                            {
                                                walls[i - 1, j + 1] = 3;
                                                walls[i, j] = 5; walls[i + 1, j - 1] = 5; walls[i - 1, j + 1] = 5;
                                                accept2 = 1;
                                                accept++;
                                                scorecomputer++;
                                            }

                                            if (i < 6 && j > 1 && walls[i, j] == 3 && walls[i + 1, j - 1] == 0 && walls[i + 2, j - 2] == 3 && accept2 == 0)//arası boş
                                            {
                                                walls[i + 1, j - 1] = 3;
                                                walls[i, j] = 5; walls[i + 1, j - 1] = 5; walls[i + 2, j - 2] = 5;
                                                accept2 = 1;
                                                accept++;
                                                scorecomputer++;
                                            }

                                        }
                                    }
                                }

                                if (accept == 0)//accept = önleme ihtimali
                                {
                                    for (int i = 0; i < walls.GetLength(0); i++)
                                    {
                                        for (int j = 0; j < walls.GetLength(1) - 1; j++)
                                        {
                                            if (walls[i, j] == 2 && walls[i, j + 1] == 2)//find 2 H -----ARKA ARKAYA-----
                                            {
                                                if (j != 0 && j < 6 && accept2 == 0 && walls[i, j - 1] == 0 && walls[i, j + 2] == 0)//sol sağ dışarı çıkma kontroluü
                                                {
                                                    leftorright = rnd.Next(1, 3);
                                                    if (leftorright == 1)
                                                    {
                                                        walls[i, j - 1] = 3;
                                                        accept2 = 1;
                                                        accept++;
                                                    }
                                                    else
                                                    {
                                                        walls[i, j + 2] = 3;
                                                        accept2 = 1;
                                                        accept++;
                                                    }
                                                }

                                                if (j != 0 && accept2 == 0 && walls[i, j - 1] == 0)//solun dışarı çıkma ihtimali
                                                {
                                                    walls[i, j - 1] = 3;
                                                    accept++;
                                                    accept2 = 1;
                                                }

                                                if (j < 6 && accept2 == 0 && walls[i, j + 2] == 0)//sağın dışarı çıkma ihtimali
                                                {
                                                    walls[i, j + 2] = 3;
                                                    accept++;
                                                    accept2 = 1;
                                                }
                                            }

                                            if (walls[i, j] == 2 && j < 6 && walls[i, j + 2] == 2 && accept2 == 0 && walls[i, j + 1] == 0)//find 2 h -----arasın boşluk-----
                                            {
                                                walls[i, j + 1] = 3;
                                                accept++;
                                                accept2 = 1;
                                            }//----------------------------

                                            if (walls[i, j] == 2 && i < 6 && walls[i + 1, j] == 2 && i != 0 && walls[i - 1, j] == 0 && walls[i + 2, j] == 0 && accept2 == 0)//find 2 H -----ALT ALTA-----üst alt boş
                                            {
                                                leftorright = rnd.Next(1, 3);
                                                if (leftorright == 1)
                                                {
                                                    walls[i - 1, j] = 3;//W                                                
                                                    accept++;
                                                }
                                                else
                                                {
                                                    walls[i + 2, j] = 3;
                                                    accept++;
                                                }
                                                accept2 = 1;
                                            }

                                            if (walls[i, j] == 2 && i < 6 && walls[i + 1, j] == 2 && walls[i + 2, j] == 0 && accept2 == 0)//aşşağısı boş
                                            {
                                                walls[i + 2, j] = 3;
                                                accept++;
                                                accept2 = 1;
                                            }

                                            if (walls[i, j] == 2 && i < 7 && walls[i + 1, j] == 2 && i != 0 && walls[i - 1, j] == 0 && accept2 == 0)//yukarısı boş
                                            {
                                                walls[i - 1, j] = 3;
                                                accept++;
                                                accept2 = 1;
                                            }

                                            if (walls[i, j] == 2 && i < 6 && walls[i + 2, j] == 2 && accept2 == 0 && walls[i + 1, j] == 0)
                                            {
                                                walls[i + 1, j] = 3;
                                                accept++;
                                                accept2 = 1;
                                            }//sağ çapraz

                                            if (i > 0 && i < 6 && j < 0 && j < 6 && walls[i, j] == 2 && walls[i + 1, j + 1] == 2 && walls[i - 1, j - 1] == 0 && walls[i + 2, j + 2] == 0 && accept2 == 0)//iki taraf boş
                                            {
                                                leftorright = rnd.Next(1, 3);
                                                if (leftorright == 1)
                                                {
                                                    walls[i - 1, j - 1] = 3;
                                                    accept2 = 1;
                                                    accept++;
                                                }
                                                else
                                                {
                                                    walls[i + 2, j + 2] = 3;
                                                    accept2 = 1;
                                                    accept++;
                                                }
                                            }

                                            if (i > 0 && i < 6 && j < 0 && j < 6 && walls[i, j] == 2 && walls[i + 1, j + 1] == 2 && walls[i - 1, j - 1] != 0 && walls[i + 2, j + 2] == 0 && accept2 == 0)//sağ alt boş
                                            {
                                                walls[i + 2, j + 2] = 3;
                                                accept2 = 1;
                                                accept++;
                                            }

                                            if (i > 0 && i < 6 && j < 0 && j < 6 && walls[i, j] == 2 && walls[i + 1, j + 1] == 2 && walls[i - 1, j - 1] == 0 && walls[i + 2, j + 2] != 0 && accept2 == 0)//sol üst boş
                                            {
                                                walls[i - 1, j - 1] = 3;
                                                accept2 = 1;
                                                accept++;
                                            }

                                            if (i > 0 && i < 7 && j > 0 && j < 7 && walls[i, j] == 0 && walls[i - 1, j - 1] == 2 && walls[i + 1, j + 1] == 2 && accept2 == 0)//ortada
                                            {
                                                walls[i, j] = 3;
                                                accept2 = 1;
                                                accept++;
                                            }//sol çaprazlar

                                            if (i > 0 && i < 6 && j > 1 && j < 7 && walls[i, j] == 2 && walls[i + 1, j - 1] == 2 && walls[i - 1, j + 1] == 0 && walls[i + 2, j - 2] == 0 && accept2 == 0)//2 side boş
                                            {
                                                leftorright = rnd.Next(1, 3);
                                                if (leftorright == 1)
                                                {
                                                    walls[i - 1, j + 1] = 3;
                                                    accept2 = 1;
                                                    accept++;
                                                }
                                                else
                                                {
                                                    walls[i + 2, j - 2] = 3;
                                                    accept2 = 1;
                                                    accept++;
                                                }
                                            }

                                            if (i > 0 && i < 6 && j > 1 && j < 7 && walls[i, j] == 2 && walls[i + 1, j - 1] == 2 && walls[i - 1, j + 1] != 0 && walls[i + 2, j - 2] == 0 && accept2 == 0)//sol alt boş
                                            {
                                                walls[i + 2, j - 2] = 3;
                                                accept2 = 1;
                                                accept++;
                                            }

                                            if (i > 0 && i < 6 && j > 1 && j < 7 && walls[i, j] == 2 && walls[i + 1, j - 1] == 2 && walls[i - 1, j + 1] != 0 && walls[i + 2, j - 2] != 0 && accept2 == 0)//sağ üst boş
                                            {
                                                walls[i - 1, j + 1] = 3;
                                                accept2 = 1;
                                                accept++;
                                            }

                                            if (j > 1 && i < 6 && walls[i, j] == 2 && walls[i + 1, j - 1] == 0 && walls[i + 2, j - 2] == 2 && accept2 == 0)//arası boş
                                            {
                                                walls[i + 1, j - 1] = 3;
                                                accept2 = 1;
                                                accept++;
                                            }

                                        }
                                    }
                                }

                                if (accept == 0)//mecbur randoma kalıyor
                                {
                                    do//random for computer
                                    {
                                        randomcursorx = rnd.Next(x - 1, x + 2);
                                        randomcursory = rnd.Next(y - 1, y + 2);

                                    } while (randomcursorx == 0 || randomcursorx == 9 || randomcursory == 0 || randomcursory == 9 || walls[randomcursory - 1, randomcursorx - 1] != 0);//oyun dışına çıkma ve dolu kutu denk gelme konrolü

                                    walls[randomcursory - 1, randomcursorx - 1] = 3;
                                }

                                accept = 0;
                                accept2 = 0;
                                step++;

                                Console.SetCursorPosition(20, 0);
                                Console.Write("Step : " + step);
                                Console.SetCursorPosition(20, 5);
                                Console.Write("Computer : " + scorecomputer);
                                Console.SetCursorPosition(20, 6);
                                Console.Write("Human : " + scorehuman);
                                Console.SetCursorPosition(x, y);
                            }
                        }

                        for (int i = 0; i < walls.GetLength(0); i++)//WRİTE
                        {
                            for (int j = 0; j < walls.GetLength(1); j++)
                            {
                                if (walls[i, j] == 0)
                                {
                                    Console.SetCursorPosition(j + 1, i + 1);
                                    Console.Write(".");
                                }

                                else if (walls[i, j] == 1)
                                {
                                    Console.SetCursorPosition(j + 1, i + 1);
                                    Console.Write("#");
                                }

                                else if (walls[i, j] == 2 || walls[i, j] == 4)
                                {
                                    Console.SetCursorPosition(j + 1, i + 1);
                                    Console.Write("H");
                                }

                                else if (walls[i, j] == 3 || walls[i, j] == 5)
                                {
                                    Console.SetCursorPosition(j + 1, i + 1);
                                    Console.Write("C");
                                }
                            }
                        }
                        Console.SetCursorPosition(x, y);
                    }
                }

                if (scorehuman == 1)
                {
                    Console.SetWindowSize(78, 10);

                    Console.SetCursorPosition(10, 3);
                    Console.Clear();
                    string end1 = @"
██╗  ██╗██╗   ██╗███╗   ███╗ █████╗ ███╗   ██╗    ██╗    ██╗██╗███╗   ██╗██╗
██║  ██║██║   ██║████╗ ████║██╔══██╗████╗  ██║    ██║    ██║██║████╗  ██║██║
███████║██║   ██║██╔████╔██║███████║██╔██╗ ██║    ██║ █╗ ██║██║██╔██╗ ██║██║
██╔══██║██║   ██║██║╚██╔╝██║██╔══██║██║╚██╗██║    ██║███╗██║██║██║╚██╗██║╚═╝
██║  ██║╚██████╔╝██║ ╚═╝ ██║██║  ██║██║ ╚████║    ╚███╔███╔╝██║██║ ╚████║██╗
╚═╝  ╚═╝ ╚═════╝ ╚═╝     ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝     ╚══╝╚══╝ ╚═╝╚═╝  ╚═══╝╚═╝
                                                                            
";
                    Console.WriteLine(end1);
                }

                else if (scorecomputer == 1)
                {
                    Console.SetWindowSize(105, 10);

                    Console.SetCursorPosition(10, 3);
                    Console.Clear();
                    string end2 = @"
 ██████╗ ██████╗ ███╗   ███╗██████╗ ██╗   ██╗████████╗███████╗██████╗     ██╗    ██╗██╗███╗   ██╗██╗
██╔════╝██╔═══██╗████╗ ████║██╔══██╗██║   ██║╚══██╔══╝██╔════╝██╔══██╗    ██║    ██║██║████╗  ██║██║
██║     ██║   ██║██╔████╔██║██████╔╝██║   ██║   ██║   █████╗  ██████╔╝    ██║ █╗ ██║██║██╔██╗ ██║██║
██║     ██║   ██║██║╚██╔╝██║██╔═══╝ ██║   ██║   ██║   ██╔══╝  ██╔══██╗    ██║███╗██║██║██║╚██╗██║╚═╝
╚██████╗╚██████╔╝██║ ╚═╝ ██║██║     ╚██████╔╝   ██║   ███████╗██║  ██║    ╚███╔███╔╝██║██║ ╚████║██╗
 ╚═════╝ ╚═════╝ ╚═╝     ╚═╝╚═╝      ╚═════╝    ╚═╝   ╚══════╝╚═╝  ╚═╝     ╚══╝╚══╝ ╚═╝╚═╝  ╚═══╝╚═╝
                                                                                                    
";
                    Console.WriteLine(end2);
                }
                Console.ReadLine();
            }
            else if (game == 2)
            {
                int bitis = 0;
                int wallsnumber = 0;
                Console.Write("Determine the game end score : ");
                bitis = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                Console.Write("How many walls do you want : ");
                wallsnumber = Convert.ToInt32(Console.ReadLine());
                if (wallsnumber < 64)
                {
                    Console.Clear();
                    Console.SetWindowSize(45, 10);
                    //satır ve süütnları dolaşarak count tut 2 li ardışık bulursan etrafına koy,(dışarıya çıkmamasını engelle),kazanmasını sağlamaya çalış
                    int[,] walls = new int[8, 8] {{0,0,0,0,0,0,0,0 },//0=dot
                                          {0,0,0,0,0,0,0,0 },//1=wall
                                          {0,0,0,0,0,0,0,0 },//2=Human
                                          {0,0,0,0,0,0,0,0 },//3=Computer
                                          {0,0,0,0,0,0,0,0 },//4 = işleme girmeyen h
                                          {0,0,0,0,0,0,0,0 },//5= işleme girmeyen c
                                          {0,0,0,0,0,0,0,0 },
                                          {0,0,0,0,0,0,0,0 }};

                    int a = 0;//x coordinate of walls
                    int b = 0;//y coordinate of walls

                    int x = 1;//x coordinate of cursor
                    int y = 1;//y coordinate of cursor

                    int randomcursorx = 0;//random x cordinate for computer
                    int randomcursory = 0;//random y cordinate for computer

                    int step = 0;
                    int sıra = 0;

                    int accept = 0;//engelleme ihtimaline bakıyor
                    int accept2 = 0;//sol sağ atama kontrolü

                    int scorehuman = 0;
                    int scorecomputer = 0;

                    int leftorright = 0;
                    int ortanokta = 0;

                    Random rnd = new Random();

                    for (int i = 0; i < wallsnumber; i++)//randomly walls
                    {
                        do
                        {
                            a = rnd.Next(0, 8);
                            b = rnd.Next(0, 8);

                        } while (walls[a, b] == 1);
                        walls[a, b] = 1;

                    }

                    for (int i = 0; i < walls.GetLength(0); i++)//write for dots
                    {
                        for (int j = 0; j < walls.GetLength(1); j++)
                        {
                            Console.SetCursorPosition(i + 1, j + 1);
                            Console.Write(".");
                        }
                    }

                    for (int i = 0; i < 8; i++)//find walls and write walls on screen
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (walls[i, j] == 1)
                            {
                                Console.SetCursorPosition(j + 1, i + 1);
                                Console.Write("#");
                            }
                        }
                    }

                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("+--------+");

                    for (int i = 0; i < 8; i++)
                    {
                        Console.WriteLine("|");
                    }

                    Console.Write("+--------+");

                    for (int i = 0; i < 8; i++)
                    {
                        Console.SetCursorPosition(9, i + 1);
                        Console.WriteLine("|");
                    }
                    Console.SetCursorPosition(x, y);
                    Console.SetCursorPosition(20, 0);
                    Console.Write("Step : " + step);
                    Console.SetCursorPosition(x, y);

                    sıra = rnd.Next(1, 4);//1 computer-2human

                    if (sıra == 1)
                    {

                        Console.SetCursorPosition(20, 1);
                        Console.Write("Turn : C");
                        Console.SetCursorPosition(x, y);

                        do
                        {
                            a = rnd.Next(0, 8);
                            b = rnd.Next(0, 8);

                        } while (walls[a, b] != 0);
                        walls[a, b] = 3;
                        Console.SetCursorPosition(b + 1, a + 1);
                        Console.Write("C");
                        Console.SetCursorPosition(x, y);
                    }

                    Console.SetCursorPosition(20, 1);
                    Console.Write("Turn : H");
                    Console.SetCursorPosition(x, y);


                    while (scorehuman != bitis && scorecomputer != bitis)//move to somewhere on screen
                    {
                        ConsoleKeyInfo input;

                        if (Console.KeyAvailable == true)
                        {
                            input = Console.ReadKey();

                            if (input.Key == ConsoleKey.DownArrow)//DOWN
                            {
                                if (y + 1 <= 8)
                                {
                                    Console.SetCursorPosition(x, y + 1);
                                    y++;
                                }
                            }

                            else if (input.Key == ConsoleKey.UpArrow)//UP
                            {
                                if (y > 1)
                                {
                                    Console.SetCursorPosition(x, y - 1);
                                    y--;
                                }
                            }

                            else if (input.Key == ConsoleKey.RightArrow)//RİGHT
                            {
                                if (x < 8)
                                {

                                    Console.SetCursorPosition(x + 1, y);
                                    x++;
                                }
                            }

                            else if (input.Key == ConsoleKey.LeftArrow)//LEFT
                            {
                                if (x > 1)
                                {
                                    Console.SetCursorPosition(x - 1, y);
                                    x--;
                                }
                            }

                            if (input.Key == ConsoleKey.Spacebar)//SPACE
                            {

                                Console.SetCursorPosition(20, 1);
                                Console.Write("Turn : H");
                                Console.SetCursorPosition(x, y);

                                if (walls[y - 1, x - 1] == 0)
                                {
                                    walls[y - 1, x - 1] = 2;

                                    if (x > 1 && x < 8 && walls[y - 1, x] == 2 && walls[y - 1, x - 2] == 2)//sol sağ kontolü
                                    {
                                        walls[y - 1, x] = 4; walls[y - 1, x - 2] = 4; walls[y - 1, x - 1] = 4;
                                        scorehuman++;
                                    }

                                    if (x > 2 && walls[y - 1, x - 2] == 2 && walls[y - 1, x - 3] == 2)//iki sol
                                    {
                                        walls[y - 1, x - 3] = 4; walls[y - 1, x - 2] = 4; walls[y - 1, x - 1] = 4;
                                        scorehuman++;
                                    }

                                    if (x < 7 && walls[y - 1, x] == 2 && walls[y - 1, x + 1] == 2)//iki sağ kontrolü
                                    {
                                        walls[y - 1, x] = 4; walls[y - 1, x + 1] = 4; walls[y - 1, x - 1] = 4;
                                        scorehuman++;
                                    }//-------------puanlama

                                    if (walls[y - 1, x - 1] == 2 && y < 8 && walls[y, x - 1] == 2 && y != 1 && walls[y - 2, x - 1] == 2)//h ortada ise
                                    {
                                        walls[y - 1, x - 1] = 4; walls[y, x - 1] = 4; walls[y - 2, x - 1] = 4;
                                        scorehuman++;
                                    }

                                    if (walls[y - 1, x - 1] == 2 && y > 2 && walls[y - 2, x - 1] == 2 && walls[y - 3, x - 1] == 2)//en allttaki h
                                    {
                                        walls[y - 1, x - 1] = 4; walls[y - 2, x - 1] = 4; walls[y - 3, x - 1] = 4;
                                        scorehuman++;
                                    }

                                    if (walls[y - 1, x - 1] == 2 && y < 7 && walls[y, x - 1] == 2 && walls[y + 1, x - 1] == 2)//en üstteki h ise
                                    {
                                        walls[y - 1, x - 1] = 4; walls[y, x - 1] = 4; walls[y + 1, x - 1] = 4;
                                        scorehuman++;
                                    }//------------sağ çapraz

                                    if (walls[y - 1, x - 1] == 2 && y < 7 && x < 7 && walls[y, x] == 2 && walls[y + 1, x + 1] == 2)//en üst
                                    {
                                        walls[y - 1, x - 1] = 4; walls[y, x] = 4; walls[y + 1, x + 1] = 4;
                                        scorehuman++;
                                    }

                                    if (x != 1 && x != 8 && y != 1 && y != 8 && walls[y - 1, x - 1] == 2 && walls[y, x] == 2 && walls[y - 2, x - 2] == 2)//orta
                                    {
                                        walls[y - 1, x - 1] = 4; walls[y, x] = 4; walls[y - 2, x - 2] = 4;
                                        scorehuman++;
                                    }

                                    if (x > 2 && y > 2 && walls[y - 1, x - 1] == 2 && walls[y - 3, x - 3] == 2 && walls[y - 2, x - 2] == 2)//en alt
                                    {
                                        walls[y - 1, x - 1] = 4; walls[y - 3, x - 3] = 4; walls[y - 2, x - 2] = 4;
                                        scorehuman++;
                                    }//sol çapraz

                                    if (x > 2 && y < 7 && walls[y - 1, x - 1] == 2 && walls[y, x - 2] == 2 && walls[y + 1, x - 3] == 2)//en üst
                                    {
                                        walls[y - 1, x - 1] = 4; walls[y, x - 2] = 4; walls[y + 1, x - 3] = 4;
                                        scorehuman++;
                                    }

                                    if (x != 1 && x != 8 && y != 1 && y != 8 && walls[y - 1, x - 1] == 2 && walls[y - 2, x] == 2 && walls[y, x - 2] == 2)//orta
                                    {
                                        walls[y - 1, x - 1] = 4; walls[y - 2, x] = 4; walls[y, x - 2] = 4;
                                        scorehuman++;
                                    }

                                    if (y > 2 && x < 7 && walls[y - 1, x - 1] == 2 && walls[y - 2, x] == 2 && walls[y - 3, x + 1] == 2)//en alt
                                    {
                                        walls[y - 1, x - 1] = 4; walls[y - 2, x] = 4; walls[y - 3, x + 1] = 4;
                                        scorehuman++;
                                    }

                                    if (accept == 0)//accept = kazanma ihtimali
                                    {
                                        for (int i = 0; i < walls.GetLength(0); i++)
                                        {
                                            for (int j = 0; j < walls.GetLength(1); j++)
                                            {
                                                if (walls[i, j] == 3 && j < 7 && walls[i, j + 1] == 3)//find 2 c -----ARKA ARKAYA-----
                                                {
                                                    if (j != 0 && j < 6 && accept2 == 0 && walls[i, j - 1] == 0 && walls[i, j + 2] == 0)//sol sağ dışarı çıkma kontroluü
                                                    {
                                                        leftorright = rnd.Next(1, 3);
                                                        if (leftorright == 1)
                                                        {
                                                            walls[i, j - 1] = 3;
                                                            walls[i, j] = 5; walls[i, j + 1] = 5; walls[i, j - 1] = 5;//W
                                                            accept++;
                                                        }
                                                        else
                                                        {
                                                            walls[i, j + 2] = 3;
                                                            walls[i, j] = 5; walls[i, j + 1] = 5; walls[i, j + 2] = 5;//W
                                                            accept++;
                                                        }
                                                        accept2 = 1;
                                                        scorecomputer++;
                                                    }

                                                    if (j != 0 && accept2 == 0 && walls[i, j - 1] == 0)//solun dışarı çıkma ihtimali
                                                    {
                                                        walls[i, j - 1] = 3;
                                                        walls[i, j] = 5; walls[i, j + 1] = 5; walls[i, j - 1] = 5;//W
                                                        accept++;
                                                        accept2 = 1;
                                                        scorecomputer++;
                                                    }

                                                    if (j < 6 && accept2 == 0 && walls[i, j + 2] == 0)//sağın dışarı çıkma ihtimali
                                                    {
                                                        walls[i, j + 2] = 3;
                                                        walls[i, j] = 5; walls[i, j + 1] = 5; walls[i, j + 2] = 5;//W
                                                        accept++;
                                                        accept2 = 1;
                                                        scorecomputer++;
                                                    }
                                                }

                                                if (walls[i, j] == 3 && j < 6 && walls[i, j + 2] == 3 && accept2 == 0 && walls[i, j + 1] == 0)//find 2 c -----arasın boşluk-----
                                                {
                                                    walls[i, j + 1] = 3;
                                                    walls[i, j] = 5; walls[i, j + 1] = 5; walls[i, j + 2] = 5;//W
                                                    accept++;
                                                    accept2 = 1;
                                                    scorecomputer++;
                                                }

                                                if (walls[i, j] == 3 && i < 6 && walls[i + 1, j] == 3 && i != 0 && walls[i - 1, j] == 0 && walls[i + 2, j] == 0 && accept2 == 0)//find 2 Hc -----ALT ALTA-----üst alt boş
                                                {
                                                    leftorright = rnd.Next(1, 3);
                                                    if (leftorright == 1)
                                                    {
                                                        walls[i - 1, j] = 3;//W
                                                        walls[i, j] = 5; walls[i - 1, j] = 5; walls[i + 1, j] = 5;
                                                        accept++;
                                                    }
                                                    else
                                                    {
                                                        walls[i, j + 2] = 3;
                                                        walls[i, j] = 5; walls[i, j + 1] = 5; walls[i, j + 2] = 5;//W
                                                        accept++;
                                                    }
                                                    accept2 = 1;
                                                    scorecomputer++;
                                                }

                                                if (walls[i, j] == 3 && i < 6 && walls[i + 1, j] == 3 && walls[i + 2, j] == 0 && accept2 == 0)//aşşağısı boş
                                                {
                                                    walls[i + 2, j] = 3;
                                                    walls[i, j] = 5; walls[i + 1, j] = 5; walls[i + 2, j] = 5;
                                                    accept++;
                                                    accept2 = 1;
                                                    scorecomputer++;
                                                }

                                                if (walls[i, j] == 3 && y < 7 && i < 7 && walls[i + 1, j] == 3 && i != 0 && walls[i - 1, j] == 0 && accept2 == 0)//yukarısı boş
                                                {
                                                    walls[i - 1, j] = 3;
                                                    walls[i, j] = 5; walls[i - 1, j] = 5; walls[i + 1, j] = 5;
                                                    accept++;
                                                    accept2 = 1;
                                                    scorecomputer++;
                                                }

                                                if (walls[i, j] == 3 && i < 6 && walls[i + 2, j] == 3 && accept2 == 0 && walls[i + 1, j] == 0)
                                                {
                                                    walls[i + 1, j] = 3;
                                                    walls[i, j] = 5; walls[i + 2, j] = 5; walls[i + 1, j] = 5;
                                                    accept++;
                                                    accept2 = 1;
                                                    scorecomputer++;
                                                }//sağ çapraz

                                                if (i > 0 && i < 6 && j < 0 && j < 6 && walls[i, j] == 3 && walls[i + 1, j + 1] == 3 && walls[i - 1, j - 1] == 0 && walls[i + 2, j + 2] == 0 && accept2 == 0)//iki taraf boş
                                                {
                                                    leftorright = rnd.Next(1, 3);
                                                    if (leftorright == 1)
                                                    {
                                                        walls[i - 1, j - 1] = 3;
                                                        walls[i, j] = 5; walls[i + 1, j + 1] = 5; walls[i - 1, j - 1] = 5;
                                                        accept2 = 1;
                                                        accept++;
                                                        scorecomputer++;
                                                    }
                                                    else
                                                    {
                                                        walls[i + 2, j + 2] = 3;
                                                        walls[i, j] = 5; walls[i + 1, j + 1] = 5; walls[i + 2, j + 2] = 5;
                                                        accept2 = 1;
                                                        accept++;
                                                        scorecomputer++;
                                                    }
                                                }

                                                if (i > 0 && i < 6 && j < 0 && j < 6 && walls[i, j] == 3 && walls[i + 1, j + 1] == 3 && walls[i - 1, j - 1] != 0 && walls[i + 2, j + 2] == 0 && accept2 == 0)//sağ alt boş
                                                {
                                                    walls[i + 2, j + 2] = 3;
                                                    walls[i, j] = 5; walls[i + 1, j + 1] = 5; walls[i + 2, j + 2] = 5;
                                                    accept2 = 1;
                                                    accept++;
                                                    scorecomputer++;
                                                }

                                                if (i > 0 && i < 6 && j < 0 && j < 6 && walls[i, j] == 3 && walls[i + 1, j + 1] == 3 && walls[i - 1, j - 1] == 0 && walls[i + 2, j + 2] != 0 && accept2 == 0)//sol üst boş
                                                {
                                                    walls[i - 1, j - 1] = 3;
                                                    walls[i, j] = 5; walls[i + 1, j + 1] = 5; walls[i - 1, j - 1] = 5;
                                                    accept2 = 1;
                                                    accept++;
                                                    scorecomputer++;
                                                }

                                                if (i > 0 && i < 7 && j > 0 && j < 7 && walls[i, j] == 0 && walls[i - 1, j - 1] == 3 && walls[i + 1, j + 1] == 3 && accept2 == 0)//ortada
                                                {
                                                    walls[i, j] = 3;
                                                    walls[i, j] = 5; walls[i - 1, j - 1] = 5; walls[i + 1, j + 1] = 5;
                                                    accept2 = 1;
                                                    accept++;
                                                    scorecomputer++;
                                                }//SOL ÇAPRAZZ

                                                if (i > 0 && i < 6 && j > 1 && j < 7 && walls[i, j] == 3 && walls[i + 1, j - 1] == 3 && walls[i - 1, j + 1] == 0 && walls[i + 2, j - 2] == 0 && accept2 == 0)//2 side boş
                                                {
                                                    leftorright = rnd.Next(1, 3);
                                                    if (leftorright == 1)
                                                    {
                                                        walls[i - 1, j + 1] = 3;
                                                        walls[i, j] = 5; walls[i + 1, j - 1] = 5; walls[i - 1, j + 1] = 5;
                                                        accept2 = 1;
                                                        accept++;
                                                        scorecomputer++;
                                                    }
                                                    else
                                                    {
                                                        walls[i + 2, j - 2] = 3;
                                                        walls[i, j] = 5; walls[i + 1, j - 1] = 5; walls[i + 2, j - 2] = 5;
                                                        accept2 = 1;
                                                        accept++;
                                                        scorecomputer++;
                                                    }
                                                }

                                                if (i > 0 && i < 6 && j > 1 && j < 7 && walls[i, j] == 3 && walls[i + 1, j - 1] == 3 && walls[i - 1, j + 1] != 0 && walls[i + 2, j - 2] == 0 && accept2 == 0)//sol alt boş
                                                {
                                                    walls[i + 2, j - 2] = 3;
                                                    walls[i, j] = 5; walls[i + 1, j - 1] = 5; walls[i + 2, j - 2] = 5;
                                                    accept2 = 1;
                                                    accept++;
                                                    scorecomputer++;
                                                }

                                                if (i > 0 && i < 6 && j > 1 && j < 7 && walls[i, j] == 3 && walls[i + 1, j - 1] == 3 && walls[i - 1, j + 1] != 0 && walls[i + 2, j - 2] != 0 && accept2 == 0)//sağ üst boş
                                                {
                                                    walls[i - 1, j + 1] = 3;
                                                    walls[i, j] = 5; walls[i + 1, j - 1] = 5; walls[i - 1, j + 1] = 5;
                                                    accept2 = 1;
                                                    accept++;
                                                    scorecomputer++;
                                                }

                                                if (i < 6 && j > 1 && walls[i, j] == 3 && walls[i + 1, j - 1] == 0 && walls[i + 2, j - 2] == 3 && accept2 == 0)//arası boş
                                                {
                                                    walls[i + 1, j - 1] = 3;
                                                    walls[i, j] = 5; walls[i + 1, j - 1] = 5; walls[i + 2, j - 2] = 5;
                                                    accept2 = 1;
                                                    accept++;
                                                    scorecomputer++;
                                                }

                                            }
                                        }
                                    }

                                    if (accept == 0)//accept = önleme ihtimali
                                    {
                                        for (int i = 0; i < walls.GetLength(0); i++)
                                        {
                                            for (int j = 0; j < walls.GetLength(1) - 1; j++)
                                            {
                                                if (walls[i, j] == 2 && walls[i, j + 1] == 2)//find 2 H -----ARKA ARKAYA-----
                                                {
                                                    if (j != 0 && j < 6 && accept2 == 0 && walls[i, j - 1] == 0 && walls[i, j + 2] == 0)//sol sağ dışarı çıkma kontroluü
                                                    {
                                                        leftorright = rnd.Next(1, 3);
                                                        if (leftorright == 1)
                                                        {
                                                            walls[i, j - 1] = 3;
                                                            accept2 = 1;
                                                            accept++;
                                                        }
                                                        else
                                                        {
                                                            walls[i, j + 2] = 3;
                                                            accept2 = 1;
                                                            accept++;
                                                        }
                                                    }

                                                    if (j != 0 && accept2 == 0 && walls[i, j - 1] == 0)//solun dışarı çıkma ihtimali
                                                    {
                                                        walls[i, j - 1] = 3;
                                                        accept++;
                                                        accept2 = 1;
                                                    }

                                                    if (j < 6 && accept2 == 0 && walls[i, j + 2] == 0)//sağın dışarı çıkma ihtimali
                                                    {
                                                        walls[i, j + 2] = 3;
                                                        accept++;
                                                        accept2 = 1;
                                                    }
                                                }

                                                if (walls[i, j] == 2 && j < 6 && walls[i, j + 2] == 2 && accept2 == 0 && walls[i, j + 1] == 0)//find 2 h -----arasın boşluk-----
                                                {
                                                    walls[i, j + 1] = 3;
                                                    accept++;
                                                    accept2 = 1;
                                                }//----------------------------

                                                if (walls[i, j] == 2 && i < 6 && walls[i + 1, j] == 2 && i != 0 && walls[i - 1, j] == 0 && walls[i + 2, j] == 0 && accept2 == 0)//find 2 H -----ALT ALTA-----üst alt boş
                                                {
                                                    leftorright = rnd.Next(1, 3);
                                                    if (leftorright == 1)
                                                    {
                                                        walls[i - 1, j] = 3;//W                                                
                                                        accept++;
                                                    }
                                                    else
                                                    {
                                                        walls[i + 2, j] = 3;
                                                        accept++;
                                                    }
                                                    accept2 = 1;
                                                }

                                                if (walls[i, j] == 2 && i < 6 && walls[i + 1, j] == 2 && walls[i + 2, j] == 0 && accept2 == 0)//aşşağısı boş
                                                {
                                                    walls[i + 2, j] = 3;
                                                    accept++;
                                                    accept2 = 1;
                                                }

                                                if (walls[i, j] == 2 && i < 7 && walls[i + 1, j] == 2 && i != 0 && walls[i - 1, j] == 0 && accept2 == 0)//yukarısı boş
                                                {
                                                    walls[i - 1, j] = 3;
                                                    accept++;
                                                    accept2 = 1;
                                                }

                                                if (walls[i, j] == 2 && i < 6 && walls[i + 2, j] == 2 && accept2 == 0 && walls[i + 1, j] == 0)
                                                {
                                                    walls[i + 1, j] = 3;
                                                    accept++;
                                                    accept2 = 1;
                                                }//sağ çapraz

                                                if (i > 0 && i < 6 && j < 0 && j < 6 && walls[i, j] == 2 && walls[i + 1, j + 1] == 2 && walls[i - 1, j - 1] == 0 && walls[i + 2, j + 2] == 0 && accept2 == 0)//iki taraf boş
                                                {
                                                    leftorright = rnd.Next(1, 3);
                                                    if (leftorright == 1)
                                                    {
                                                        walls[i - 1, j - 1] = 3;
                                                        accept2 = 1;
                                                        accept++;
                                                    }
                                                    else
                                                    {
                                                        walls[i + 2, j + 2] = 3;
                                                        accept2 = 1;
                                                        accept++;
                                                    }
                                                }

                                                if (i > 0 && i < 6 && j < 0 && j < 6 && walls[i, j] == 2 && walls[i + 1, j + 1] == 2 && walls[i - 1, j - 1] != 0 && walls[i + 2, j + 2] == 0 && accept2 == 0)//sağ alt boş
                                                {
                                                    walls[i + 2, j + 2] = 3;
                                                    accept2 = 1;
                                                    accept++;
                                                }

                                                if (i > 0 && i < 6 && j < 0 && j < 6 && walls[i, j] == 2 && walls[i + 1, j + 1] == 2 && walls[i - 1, j - 1] == 0 && walls[i + 2, j + 2] != 0 && accept2 == 0)//sol üst boş
                                                {
                                                    walls[i - 1, j - 1] = 3;
                                                    accept2 = 1;
                                                    accept++;
                                                }

                                                if (i > 0 && i < 7 && j > 0 && j < 7 && walls[i, j] == 0 && walls[i - 1, j - 1] == 2 && walls[i + 1, j + 1] == 2 && accept2 == 0)//ortada
                                                {
                                                    walls[i, j] = 3;
                                                    accept2 = 1;
                                                    accept++;
                                                }//sol çaprazlar

                                                if (i > 0 && i < 6 && j > 1 && j < 7 && walls[i, j] == 2 && walls[i + 1, j - 1] == 2 && walls[i - 1, j + 1] == 0 && walls[i + 2, j - 2] == 0 && accept2 == 0)//2 side boş
                                                {
                                                    leftorright = rnd.Next(1, 3);
                                                    if (leftorright == 1)
                                                    {
                                                        walls[i - 1, j + 1] = 3;
                                                        accept2 = 1;
                                                        accept++;
                                                    }
                                                    else
                                                    {
                                                        walls[i + 2, j - 2] = 3;
                                                        accept2 = 1;
                                                        accept++;
                                                    }
                                                }

                                                if (i > 0 && i < 6 && j > 1 && j < 7 && walls[i, j] == 2 && walls[i + 1, j - 1] == 2 && walls[i - 1, j + 1] != 0 && walls[i + 2, j - 2] == 0 && accept2 == 0)//sol alt boş
                                                {
                                                    walls[i + 2, j - 2] = 3;
                                                    accept2 = 1;
                                                    accept++;
                                                }

                                                if (i > 0 && i < 6 && j > 1 && j < 7 && walls[i, j] == 2 && walls[i + 1, j - 1] == 2 && walls[i - 1, j + 1] != 0 && walls[i + 2, j - 2] != 0 && accept2 == 0)//sağ üst boş
                                                {
                                                    walls[i - 1, j + 1] = 3;
                                                    accept2 = 1;
                                                    accept++;
                                                }

                                                if (j > 1 && i < 6 && walls[i, j] == 2 && walls[i + 1, j - 1] == 0 && walls[i + 2, j - 2] == 2 && accept2 == 0)//arası boş
                                                {
                                                    walls[i + 1, j - 1] = 3;
                                                    accept2 = 1;
                                                    accept++;
                                                }

                                            }
                                        }
                                    }

                                    if (accept == 0)//mecbur randoma kalıyor
                                    {
                                        do//random for computer
                                        {
                                            randomcursorx = rnd.Next(x - 1, x + 2);
                                            randomcursory = rnd.Next(y - 1, y + 2);

                                        } while (randomcursorx == 0 || randomcursorx == 9 || randomcursory == 0 || randomcursory == 9 || walls[randomcursory - 1, randomcursorx - 1] != 0);//oyun dışına çıkma ve dolu kutu denk gelme konrolü

                                        walls[randomcursory - 1, randomcursorx - 1] = 3;
                                    }

                                    accept = 0;
                                    accept2 = 0;
                                    step++;

                                    Console.SetCursorPosition(20, 0);
                                    Console.Write("Step : " + step);
                                    Console.SetCursorPosition(20, 5);
                                    Console.Write("Computer : " + scorecomputer);
                                    Console.SetCursorPosition(20, 6);
                                    Console.Write("Human : " + scorehuman);
                                    Console.SetCursorPosition(x, y);
                                }
                            }

                            for (int i = 0; i < walls.GetLength(0); i++)//WRİTE
                            {
                                for (int j = 0; j < walls.GetLength(1); j++)
                                {
                                    if (walls[i, j] == 0)
                                    {
                                        Console.SetCursorPosition(j + 1, i + 1);
                                        Console.Write(".");
                                    }

                                    else if (walls[i, j] == 1 || walls[i, j] == 4 || walls[i, j] == 5)
                                    {
                                        Console.SetCursorPosition(j + 1, i + 1);
                                        Console.Write("#");
                                    }

                                    else if (walls[i, j] == 2)
                                    {
                                        Console.SetCursorPosition(j + 1, i + 1);
                                        Console.Write("H");
                                    }

                                    else if (walls[i, j] == 3)
                                    {
                                        Console.SetCursorPosition(j + 1, i + 1);
                                        Console.Write("C");
                                    }
                                }
                            }
                            Console.SetCursorPosition(x, y);
                        }
                    }

                    if (scorehuman == 1)
                    {
                        Console.SetWindowSize(78, 10);

                        Console.SetCursorPosition(10, 3);
                        Console.Clear();
                        string end1 = @"
██╗  ██╗██╗   ██╗███╗   ███╗ █████╗ ███╗   ██╗    ██╗    ██╗██╗███╗   ██╗██╗
██║  ██║██║   ██║████╗ ████║██╔══██╗████╗  ██║    ██║    ██║██║████╗  ██║██║
███████║██║   ██║██╔████╔██║███████║██╔██╗ ██║    ██║ █╗ ██║██║██╔██╗ ██║██║
██╔══██║██║   ██║██║╚██╔╝██║██╔══██║██║╚██╗██║    ██║███╗██║██║██║╚██╗██║╚═╝
██║  ██║╚██████╔╝██║ ╚═╝ ██║██║  ██║██║ ╚████║    ╚███╔███╔╝██║██║ ╚████║██╗
╚═╝  ╚═╝ ╚═════╝ ╚═╝     ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝     ╚══╝╚══╝ ╚═╝╚═╝  ╚═══╝╚═╝
                                                                            
";
                        Console.WriteLine(end1);
                    }

                    else if (scorecomputer == 1)
                    {
                        Console.SetWindowSize(105, 10);

                        Console.SetCursorPosition(10, 3);
                        Console.Clear();
                        string end2 = @"
 ██████╗ ██████╗ ███╗   ███╗██████╗ ██╗   ██╗████████╗███████╗██████╗     ██╗    ██╗██╗███╗   ██╗██╗
██╔════╝██╔═══██╗████╗ ████║██╔══██╗██║   ██║╚══██╔══╝██╔════╝██╔══██╗    ██║    ██║██║████╗  ██║██║
██║     ██║   ██║██╔████╔██║██████╔╝██║   ██║   ██║   █████╗  ██████╔╝    ██║ █╗ ██║██║██╔██╗ ██║██║
██║     ██║   ██║██║╚██╔╝██║██╔═══╝ ██║   ██║   ██║   ██╔══╝  ██╔══██╗    ██║███╗██║██║██║╚██╗██║╚═╝
╚██████╗╚██████╔╝██║ ╚═╝ ██║██║     ╚██████╔╝   ██║   ███████╗██║  ██║    ╚███╔███╔╝██║██║ ╚████║██╗
 ╚═════╝ ╚═════╝ ╚═╝     ╚═╝╚═╝      ╚═════╝    ╚═╝   ╚══════╝╚═╝  ╚═╝     ╚══╝╚══╝ ╚═╝╚═╝  ╚═══╝╚═╝
                                                                                                    
";
                        Console.WriteLine(end2);
                    }
                }
                else
                    Console.Write("Wrong entry");
                Console.ReadLine();
            }
        }
    }
}
