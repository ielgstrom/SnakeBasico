using System;

namespace SnakeBasico
{
    class Program
    {
        static void Main(string[] args)
        {
            int dimension = 21;
            int posicionX0 = dimension / 2;
            int posicionY0 = dimension / 2;
            string[,] tablero = tablaInicial(dimension);
            bool continueGame = true;
            while(continueGame)
            {
                PrintTabla(tablero);
                Tuple<string[,], int, int, int> nuevaTabla = movimiento(tablero, posicionX0, posicionY0, dimension);
                posicionX0 = nuevaTabla.Item2;
                posicionY0 = nuevaTabla.Item3;
                //Console.Clear();
                if (puedeHacerAlgoMas(posicionX0, posicionY0, tablero, dimension))
                {
                System.Threading.Thread.Sleep(1000);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                }
            }
            Console.WriteLine("Has acabado el juego\nPulsa Cualquier tecla para salir");



        }
        public static void PrintTabla(string[,] tablaMomentanea)
        {

            Console.Write(" ");

            Console.Write("\n");
            for (int i = 1; i < tablaMomentanea.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < tablaMomentanea.GetLength(0) - 1; j++)
                {
                    if (tablaMomentanea[i, j] == " X")
                        Console.ForegroundColor = ConsoleColor.Green;
                    else Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(tablaMomentanea[i, j]);
                }
                Console.Write("\n");
            }
        }


        public static Tuple<string[,], int, int, int> movimiento(string[,] tablaPrecedente, int newX, int newY, int dimension)
        {
            System.Threading.Thread.Sleep(1000);
            while (Console.KeyAvailable)
            {

            }
            //var read = Console.ReadKey();
            var read = "D";
            if(read == null)
            {
                
                tablaPrecedente[newX, newY - 1] = " X";
                return new Tuple<string[,], int, int, int>(tablaPrecedente, newX, newY, dimension);

            }
            switch (read)
            {
                case ConsoleKey.LeftArrow:
                    if (isInsideBox(newX, newY - 1, dimension) && tablaPrecedente[newX, newY - 1] == " o")
                    {
                        tablaPrecedente[newX, newY - 1] = " X";
                        newY--;


                    }
                    else
                    {
                        Console.WriteLine("No puedes ir ahí. Ves a otra posicion");
                        movimiento(tablaPrecedente, newX, newY, dimension);


                    }
                    break;

                case ConsoleKey.RightArrow:
                    if (isInsideBox(newX, newY + 1, dimension) && tablaPrecedente[newX, newY + 1] == " o")
                    {
                        tablaPrecedente[newX, newY + 1] = " X";
                        newY++;

                    }
                    else
                    {
                        Console.WriteLine("No puedes ir ahí. Ves a otra posicion");
                        movimiento(tablaPrecedente, newX, newY, dimension);

                    }
                    break;

                case ConsoleKey.UpArrow:
                    if (isInsideBox(newX - 1, newY, dimension) && tablaPrecedente[newX - 1, newY] == " o")
                    {
                        tablaPrecedente[newX - 1, newY] = " X";
                        newX--;
                    }
                    else
                    {
                        Console.WriteLine("No puedes ir ahí. Ves a otra posicion");
                        movimiento(tablaPrecedente, newX, newY, dimension);
                    }

                    break;

                case ConsoleKey.DownArrow:
                    if (isInsideBox(newX + 1, newY, dimension) && tablaPrecedente[newX + 1, newY] == " o")
                    {
                        tablaPrecedente[newX + 1, newY] = " X";
                        newX++;
                    }
                    else
                    {
                        Console.WriteLine("No puedes ir ahí. Ves a otra posicion");
                        movimiento(tablaPrecedente, newX, newY, dimension);
                    }
                    break;

                default:
                    Console.WriteLine("\nComando no Valido. Utiliza solo las felchas");
                    movimiento(tablaPrecedente, newX, newY, dimension);
                    break;
            }
            Console.Clear();
            return new Tuple<string[,], int, int, int>(tablaPrecedente, newX, newY, dimension);
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
    }
}

