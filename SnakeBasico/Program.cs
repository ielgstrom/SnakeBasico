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
            int[,] snake0 = snakeIncial(dimension);
            while (continueGame)
            {
                Tuple<string[,], string, bool> ElemRand= colocarElementoRandom(tablero, dimension);
                PrintTabla(ElemRand.Item1, ElemRand.Item2, ElemRand.Item3);
                //nuevaSnake(snake0, posicionX0, posicionY0);
                Tuple<string[,], int, int, int, string, int[,]> nuevaTabla = movimiento(ElemRand.Item1, posicionX0, posicionY0, dimension, direccionSnake, snake0);
                posicionX0 = nuevaTabla.Item2;
                posicionY0 = nuevaTabla.Item3;
                direccionSnake = nuevaTabla.Item5;
                snake0 = nuevaTabla.Item6;
                tablero = nuevaTabla.Item1;
                //Console.Clear();
                if (puedeHacerAlgoMas(posicionX0, posicionY0, tablero, dimension)) continue;
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                }
            }
            Console.WriteLine("Has acabado el juego\nPulsa Cualquier tecla para salir");
        }
        public static void PrintTabla(string[,] tablaMomentanea, string LetraColor, bool HaCambiado)
        {

            Console.Write(" ");

            Console.Write("\n");
            for (int i = 1; i < tablaMomentanea.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < tablaMomentanea.GetLength(0) - 1; j++)
                {
                    if (tablaMomentanea[i, j] == " X")
                    {
                        if (HaCambiado)
                        {
                            if (LetraColor == " A") Console.ForegroundColor = ConsoleColor.Blue;
                            else if (LetraColor == " B") Console.ForegroundColor = ConsoleColor.Red;
                            else if (LetraColor == " C") Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                    }
                    else if (tablaMomentanea[i, j] == LetraColor && LetraColor == " A") Console.ForegroundColor = ConsoleColor.Blue;
                    else if (tablaMomentanea[i, j] == LetraColor && LetraColor == " B") Console.ForegroundColor = ConsoleColor.Red;
                    else if (tablaMomentanea[i, j] == LetraColor && LetraColor == " C") Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    else Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(tablaMomentanea[i, j]);
                }
                Console.Write("\n");
            }
        }

        public static Tuple<string[,], int, int, int, string, int[,]> movimiento(string[,] tablaprecedente, int newX, int newY, int dimension, string direccionSnake, int[,] snake0)
        {
            DateTime beginWait = DateTime.Now;
            while (!Console.KeyAvailable && DateTime.Now.Subtract(beginWait).TotalSeconds < 0.4)
                System.Threading.Thread.Sleep(250);

            int[,] newSnake;
            if (!Console.KeyAvailable)
            {
                switch (direccionSnake)
                {
                    case "Up":
                        tablaprecedente[snake0[0,0] - 1, snake0[0,1]] = " X";
                        tablaprecedente[snake0[1, 0],snake0[1, 1]] = " X";
                        tablaprecedente[snake0[2, 0],snake0[2, 1]] = " o";
                        //newSnake = nuevaSnake(snake0, newX-1, newY);
                        newX--;
                        break;
                    case "Down":
    
                        tablaprecedente[snake0[0, 0] + 1, snake0[0, 1]] = " X";
                        tablaprecedente[snake0[1, 0], snake0[1, 1]] = " X";
                        tablaprecedente[snake0[2, 0], snake0[2, 1]] = " o";
                        newX++;
                        
                        break;
                    case "Left":
              
                        tablaprecedente[snake0[0, 0], snake0[0, 1]-1] = " X";
                        tablaprecedente[snake0[1, 0], snake0[1, 1]] = " X";
                        tablaprecedente[snake0[2, 0], snake0[2, 1]] = " o";
                        newY--;
                        
                        break;
                    case "Right":
                
                        tablaprecedente[snake0[0, 0], snake0[0, 1]+1] = " X";
                        tablaprecedente[snake0[1, 0], snake0[1, 1]] = " X";
                        tablaprecedente[snake0[2, 0], snake0[2, 1]] = " o";
                        newY++;
                        
                        break;
                }
                Console.Clear();
                newSnake = nuevaSnake(snake0, newX, newY);
                return new Tuple<string[,], int, int, int, string, int[,]>(tablaprecedente, newX, newY, dimension, direccionSnake, newSnake);
            }
            else
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (isInsideBox(newX, newY - 1, dimension) && tablaprecedente[newX, newY - 1] != " X")
                        {
                            tablaprecedente[snake0[0, 0], snake0[0, 1] - 1] = " X";
                            tablaprecedente[snake0[1, 0], snake0[1, 1]] = " X";
                            tablaprecedente[snake0[2, 0], snake0[2, 1]] = " o";
                            newY--;
                            direccionSnake = "Left";
                        }
                        else
                        {
                            Console.WriteLine("no puedes ir ahí. ves a otra posicion 0");
                            movimiento(tablaprecedente, newX, newY, dimension, direccionSnake, snake0);
                        }
                        break;

                    case ConsoleKey.RightArrow:
                        if (isInsideBox(newX, newY + 1, dimension) && tablaprecedente[newX, newY + 1] != " X")
                        {
                            tablaprecedente[snake0[0, 0], snake0[0, 1] + 1] = " X";
                            tablaprecedente[snake0[1, 0], snake0[1, 1]] = " X";
                            tablaprecedente[snake0[2, 0], snake0[2, 1]] = " o";
                            newY++;
                            direccionSnake = "Right";
                        }
                        else
                        {
                            Console.WriteLine("no puedes ir ahí. ves a otra posicion 1");
                            movimiento(tablaprecedente, newX, newY, dimension, direccionSnake, snake0);
                        }
                        break;

                    case ConsoleKey.UpArrow:
                        if (isInsideBox(newX - 1, newY, dimension) && tablaprecedente[newX - 1, newY] != " X")
                        {
                            tablaprecedente[snake0[0, 0] - 1, snake0[0, 1]] = " X";
                            tablaprecedente[snake0[1, 0], snake0[1, 1]] = " X";
                            tablaprecedente[snake0[2, 0], snake0[2, 1]] = " o";
                            newX--;
                            direccionSnake = "Up";
                        }
                        else
                        {
                            Console.WriteLine("no puedes ir ahí. ves a otra posicion 2");
                            movimiento(tablaprecedente, newX, newY, dimension, direccionSnake, snake0);
                        }

                        break;

                    case ConsoleKey.DownArrow:
                        if (isInsideBox(newX + 1, newY, dimension) && tablaprecedente[newX + 1, newY] != " X")
                        {
                            tablaprecedente[snake0[0, 0] + 1, snake0[0, 1]] = " X";
                            tablaprecedente[snake0[1, 0], snake0[1, 1]] = " X";
                            tablaprecedente[snake0[2, 0], snake0[2, 1]] = " o";
                            newX++;
                            direccionSnake = "Down";
                        }
                        else
                        {
                            Console.WriteLine("no puedes ir ahí. ves a otra posicion 3");
                            movimiento(tablaprecedente, newX, newY, dimension, direccionSnake, snake0);
                        }
                        break;

                    default:
                        Console.WriteLine("\ncomando no valido. utiliza solo las felchas");
                        movimiento(tablaprecedente, newX, newY, dimension, direccionSnake, snake0);
                        break;
                }
                Console.Clear();
                newSnake = nuevaSnake(snake0, newX, newY);
                return new Tuple<string[,], int, int, int, string, int[,]>(tablaprecedente, newX, newY, dimension, direccionSnake, newSnake);
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
            int[,] arraySnake = new int[,] { { dimension / 2, dimension / 2 }, { (dimension / 2)+1, (dimension / 2) }, {( dimension / 2)+2, (dimension / 2) } };
            return arraySnake;
        }

        public static bool isInsideBox(int x, int y, int dimension)
        {
            if (x == 0 || x == dimension-1 || y == 0 || y == dimension-1) return false;
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

        public static Tuple<string[,],string, bool> colocarElementoRandom(string[,] tablaMomentanea, int dimension)
        {
            for (var i =1; i < dimension - 1; i++)
            {
                for (var j = 1; j < dimension - 1; j++)
                {
                    if (tablaMomentanea[i, j] != " o" && tablaMomentanea[i, j] != " X") return new Tuple<string[,], string, bool>(tablaMomentanea, tablaMomentanea[i,j], true);
                    
                }
            }
            
            Random rd = new Random();
            Random rd2 = new Random();
            Random colorRand = new Random();
            string[] letras = new string[] { " A", " B", " C" };
            int index = colorRand.Next(letras.Length);
            int rand1 = rd.Next(1, dimension - 1);
            int rand2 = rd2.Next(1, dimension - 1);
            if (tablaMomentanea[rand1, rand2] == " o")
            {
                tablaMomentanea[rand1, rand2] = letras[index];
                return new Tuple<string[,], string, bool>(tablaMomentanea, letras[index], true);
            }
            else colocarElementoRandom(tablaMomentanea, dimension);
             return new Tuple <string[,], string, bool>(tablaMomentanea, " o", false);

        }

        public static int[,] nuevaSnake(int[,] snakeMomentanea, int newX, int newY)
        { 
            int[,] siguienteSnake = new int[3,2] { {newX, newY },{snakeMomentanea[0,0],snakeMomentanea[0,1] },{snakeMomentanea[1, 0], snakeMomentanea[1, 1] } };
            return siguienteSnake;
        }
    }
}

