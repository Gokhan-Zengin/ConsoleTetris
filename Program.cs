using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
namespace ConsoleGameProj
{
    class Program
    {
        static bool gameRunning = true;
        public static int score = 0;
        public static List<Block> allBlocks = new List<Block>();
        public static GameObject gameObject;
        public static bool collided;
        static int width = 12, height = 17;

        static void Main(string[] args)
        {
            //Console.SetWindowSize(width, height);
            Console.WriteLine("Press Any Key To Start");
            Console.ReadKey();
            for (int y = 0; y < height; y++)
            {
                new Block(0, y, '#');
                new Block(width - 1, y, '#');
                if (y == height - 1)
                {
                    for (int x = 0; x < width; x++)
                    {
                        new Block(x, y, '#');
                    }
                }
            }

            ConsoleKeyInfo cki = new ConsoleKeyInfo();

            gameObject = new GameObject(new Random().Next(7));

            RenderBlocks();

            int countToGoDown = 0;

            while (gameRunning)//loop
            {
                if (Console.KeyAvailable && !collided)
                {
                    cki = Console.ReadKey(true);
                    if (cki.Key == ConsoleKey.A)
                        gameObject.translate(Direction.LEFT);
                    else if (cki.Key == ConsoleKey.D)
                        gameObject.translate(Direction.RIGHT);
                    else if (cki.Key == ConsoleKey.W)
                        gameObject.Rotate();
                }


                if (countToGoDown == 30 && !collided)
                {
                    gameObject.translate(Direction.DOWN);
                    countToGoDown = 0;
                }

                countToGoDown++;
                Thread.Sleep(10);
            }
            Console.Clear();
            Console.WriteLine("Score : " + score);
            Console.ReadKey();


        }
        public static void OnGameObjectCollidesFloor(GameObject collidedGameObject)
        {
            for (int i = 0; i < collidedGameObject.blocks.Length; i++)
            {
                if (collidedGameObject.blocks[i].y == 0)
                {
                    OnGameEnd();
                    return;
                }
                var checkedLine =
                    from x in allBlocks
                    where x.y == collidedGameObject.blocks[i].y && x.character != '#'
                    select x;
                if (checkedLine.Count() == width - 2)
                {
                    int translateY = collidedGameObject.blocks[i].y;
                    allBlocks = allBlocks.Except(checkedLine).ToList();
                    score += 100;
                    allBlocks.ForEach(x => { if (x.y < translateY && x.character != '#') x.y++; });
                }


            }
            gameObject = new GameObject(new Random().Next(7));
        }
        static void OnGameEnd()
        {
            gameRunning = false;
        }
        public static void RenderBlocks()
        {
            Console.Clear();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var selectedObj =
                        from obj in allBlocks.Distinct()
                        where obj.x == x && obj.y == y
                        select obj;
                    var charList = selectedObj.ToArray();
                    if (selectedObj.Any())
                    {
                        Console.ForegroundColor = charList[0].color;
                        Console.Write(charList[0].character);
                    }
                    else
                        Console.Write(" ");
                }

                Console.Write(Environment.NewLine);

            }
        }
    }

    enum Direction
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }

}
