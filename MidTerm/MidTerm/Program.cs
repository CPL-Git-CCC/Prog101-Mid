// Program.cs
using System;
using System.Numerics;

namespace AdventureGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Adventure Game!");
            Console.Write("Enter your name: ");
            string playerName = Console.ReadLine();

            Player player = new Player(playerName, 100, 10, 5);
            Game game = new Game(player);
            game.Start();
        }
    }
}