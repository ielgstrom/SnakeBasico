using System;

namespace SnakeBasico
{
    class Program
    {
        static void Main(string[] args)
        {
            int dimension = 13;
            int posicionX0 = dimension / 2;
            int posicionY0 = dimension / 2;
            string[,] tablero = tablaInicial(dimension);
            int[,] posicionesSerpiente = snakeIncial(dimension);
            bool continueGame = true;
            string direccionSnake = "Up";
            while (continueGame)
            {
                colocarElementoRandom(tablero, dimension);
                PrintTabla(tablero);
                Tuple<string[,], int, int, int, string> nuevaTabla = movimiento(tablero, posicionX0, posicionY0, dimension, direccionSnake);
                posicionX0 = nuevaTabla.Item2;
                posicionY0 = nuevaTabla.Item3;
                direccionSnake = nuevaTabla.Item5;
                //Console.Clear();
                if (puedeHacerAlgoMas(posicionX0, posicionY0, tablero, dimension)) continue;
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                }
            }
            Console.WriteLine("Has acabado el juego\nPulsa Cualquier tecla para salir");


            //REFERENCIA DE COMO HACER UN TIMEOUT DE GETKEY------------------------------------
            //DateTime beginWait = DateTime.Now;
            //while (!Console.KeyAvailable && DateTime.Now.Subtract(beginWait).TotalSeconds < 10)
            //    System.Threading.Thread.Sleep(2500);

            //if (!Console.KeyAvailable)
            //    Console.WriteLine("You didn't press anything!");
            //else
            //    Console.WriteLine("You pressed: {0}", Console.ReadKey().KeyChar);
            //----------------------------------------------------------------------------------



        }
        public static void PrintTabla(string[,] tablaMomentanea)
        {

            Console.Write(" ");

            Console.Write("\n");
            for (int i = 1; i < tablaMomentanea.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < tablaMomentanea.GetLength(0) - 1; j++)
                {
                    if (tablaMomentanea[i, j] == " X") Console.ForegroundColor = ConsoleColor.Green;
                    else if(tablaMomentanea[i, j] == " A") Console.ForegroundColor = ConsoleColor.Red;
                    else Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(tablaMomentanea[i, j]);
                }
                Console.Write("\n");
            }
        }

        public static Tuple<string[,], int, int, int, string> movimiento(string[,] tablaprecedente, int newX, int newY, int dimension, string direccionSnake)
        {

            DateTime beginWait = DateTime.Now;
            while (!Console.KeyAvailable && DateTime.Now.Subtract(beginWait).TotalSeconds < 0.5)
                System.Threading.Thread.Sleep(250);

            if (!Console.KeyAvailable)
            {
                switch (direccionSnake)
                {
                    case "Up":
                        tablaprecedente[newX - 1, newY] = " X";
                        newX--;
                        break;
                    case "Down":
                        tablaprecedente[newX + 1, newY] = " X";
                        newX++;
                        break;
                    case "Left":
                        tablaprecedente[newX, newY - 1] = " X";
                        newY--;
                        break;
                    case "Right":
                        tablaprecedente[newX, newY + 1] = " X";
                        newY++;
                        break;
                }
                Console.Clear();
                return new Tuple<string[,], int, int, int, string>(tablaprecedente, newX, newY, dimension, direccionSnake);
            }
            else
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (isInsideBox(newX, newY - 1, dimension) && tablaprecedente[newX, newY - 1] == " o")
                        {
                            tablaprecedente[newX, newY - 1] = " X";
                            newY--;
                            direccionSnake = "Left";
                        }
                        else
                        {
                            Console.WriteLine("no puedes ir ahí. ves a otra posicion");
                            movimiento(tablaprecedente, newX, newY, dimension, direccionSnake);
                        }
                        break;

                    case ConsoleKey.RightArrow:
                        if (isInsideBox(newX, newY + 1, dimension) && tablaprecedente[newX, newY + 1] == " o")
                        {
                            tablaprecedente[newX, newY + 1] = " X";
                            newY++;
                            direccionSnake = "Right";
                        }
                        else
                        {
                            Console.WriteLine("no puedes ir ahí. ves a otra posicion");
                            movimiento(tablaprecedente, newX, newY, dimension, direccionSnake);
                        }
                        break;

                    case ConsoleKey.UpArrow:
                        if (isInsideBox(newX - 1, newY, dimension) && tablaprecedente[newX - 1, newY] == " o")
                        {
                            tablaprecedente[newX - 1, newY] = " X";
                            newX--;
                            direccionSnake = "Up";
                        }
                        else
                        {
                            Console.WriteLine("no puedes ir ahí. ves a otra posicion");
                            movimiento(tablaprecedente, newX, newY, dimension, direccionSnake);
                        }

                        break;

                    case ConsoleKey.DownArrow:
                        if (isInsideBox(newX + 1, newY, dimension) && tablaprecedente[newX + 1, newY] == " o")
                        {
                            tablaprecedente[newX + 1, newY] = " X";
                            newX++;
                            direccionSnake = "Down";
                        }
                        else
                        {
                            Console.WriteLine("no puedes ir ahí. ves a otra posicion");
                            movimiento(tablaprecedente, newX, newY, dimension, direccionSnake);
                        }
                        break;

                    default:
                        Console.WriteLine("\ncomando no valido. utiliza solo las felchas");
                        movimiento(tablaprecedente, newX, newY, dimension, direccionSnake);
                        break;
                }
                Console.Clear();
                return new Tuple<string[,], int, int, int, string>(tablaprecedente, newX, newY, dimension, direccionSnake);
            }
        }

        public static string[,] tablaInicial(int dimension)
        {
            string[,] tabla0 = new string[dimension, dimension];
            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    tabla0[i, j] = " o";
                }
            }
            tabla0[dimension / 2, dimension / 2] = " X";
            return tabla0;

        }
        public static int[,] snakeIncial(int dimension)
        {
            int[,] arraySnake = new int[,] { { dimension / 2, dimension / 2 }, { dimension / 2, (dimension / 2) - 1 }, { dimension / 2, (dimension / 2) - 2 } };
            return arraySnake;
        }

        public static bool isInsideBox(int x, int y, int dimension)
        {
            if (x == 0 || x == dimension || y == 0 || y == dimension) return false;
            else return true;
        }
        public static bool puedeHacerAlgoMas(int x, int y, string[,] tablaMomentanea, int dimension)
        {
            int checkFourSides = 0;
            if (tablaMomentanea[x + 1, y] == " X") checkFourSides++;
            if (tablaMomentanea[x - 1, y] == " X") checkFourSides++;
            if (tablaMomentanea[x, y + 1] == " X") checkFourSides++;
            if (tablaMomentanea[x, y - 1] == " X") checkFourSides++;
            if (x - 1 == 0) checkFourSides++;
            if (y - 1 == 0) checkFourSides++;
            if (x + 1 == dimension - 1) checkFourSides++;
            if (y + 1 == dimension - 1) checkFourSides++;
            if (checkFourSides == 4)
            {
                Console.WriteLine("Te has quedado atrapado");
                return false;
            }
            else return true;
        }

        public static string[,] colocarElementoRandom(string[,] tablaMomentanea, int dimension)
        {
            for (var i =1; i < dimension - 1; i++)
            {
                for (var j = 1; j < dimension - 1; j++)
                {
                    if (tablaMomentanea[i, j] == " A") return tablaMomentanea;
                }
            }
            Random rd = new Random();
            Random rd2 = new Random();
            int rand1 = rd.Next(1, dimension - 1);
            int rand2 = rd2.Next(1, dimension - 1);
            if (tablaMomentanea[rand1, rand2] == " o")
            {
                tablaMomentanea[rand1, rand2] = " A";
                return tablaMomentanea;
            }
            else colocarElementoRandom(tablaMomentanea, dimension);
            return tablaMomentanea;

        }
    }
}

