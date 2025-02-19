// Game.cs
using System;

namespace AdventureGame
{
    public class Game
    {
        private Player _player;
        private Random _random;
        private bool _gameOver;
        private bool _victory;

        public Game(Player player)
        {
            _player = player;
            _random = new Random();
            _gameOver = false;
            _victory = false;
        }

        public void Start()
        {
            Console.WriteLine($"\nWelcome, {_player.Name}! Your adventure begins...");
            _player.DisplayStats();

            while (!_gameOver && !_victory)
            {
                Console.WriteLine("You find yourself in a dark forest. There are two paths ahead.");
                Console.WriteLine("1. Take the left path.");
                Console.WriteLine("2. Take the right path.");

                int choice = GetChoice(2);

                if (choice == 1)
                {
                    LeftPath();
                }
                else
                {
                    RightPath();
                }

                CheckGameEnd();
            }
        }

        private void LeftPath()
        {
            Console.WriteLine("\nYou take the left path and encounter a wild bear!");
            Console.WriteLine("1. Fight the bear.");
            Console.WriteLine("2. Try to run away.");

            int choice = GetChoice(2);

            if (choice == 1)
            {
                FightBear();
            }
            else
            {
                RunFromBear();
            }
        }

        private void RightPath()
        {
            Console.WriteLine("\nYou take the right path and find a treasure chest!");
            Console.WriteLine("1. Open the chest.");
            Console.WriteLine("2. Ignore the chest and keep walking.");

            int choice = GetChoice(2);

            if (choice == 1)
            {
                OpenChest();
            }
            else
            {
                Console.WriteLine("You decide to ignore the chest and continue your journey.");
                Console.WriteLine("You wander the forest, but the bear is still out there...");
            }
        }

        private void FightBear()
        {
            Console.WriteLine("\nYou decide to fight the bear!");
            int bearStrength = _random.Next(5, 15);

            if (_player.Strength > bearStrength)
            {
                Console.WriteLine("You defeat the bear with your strength!");
                Console.WriteLine("You have saved the forest and become a hero!");
                _victory = true;
            }
            else
            {
                Console.WriteLine("The bear overpowers you and you lose 30 health.");
                _player.Health -= 30;
                _player.DisplayStats();

                if (_player.Health <= 0)
                {
                    _gameOver = true;
                }
            }
        }

        private void RunFromBear()
        {
            Console.WriteLine("\nYou try to run away from the bear!");
            int escapeChance = _random.Next(1, 10);

            if (_player.Luck > escapeChance)
            {
                Console.WriteLine("You successfully escape the bear!");
                Console.WriteLine("However, the bear is still out there...");
            }
            else
            {
                Console.WriteLine("The bear catches you and you lose 20 health.");
                _player.Health -= 20;
                _player.DisplayStats();

                if (_player.Health <= 0)
                {
                    _gameOver = true;
                }
            }
        }

        private void OpenChest()
        {
            Console.WriteLine("\nYou open the chest...");
            int trapChance = _random.Next(1, 10);

            if (_player.Luck > trapChance)
            {
                Console.WriteLine("You find a treasure and gain 50 strength!");
                _player.Strength += 50;
                _player.DisplayStats();
                Console.WriteLine("You feel stronger, but the bear is still out there...");
            }
            else
            {
                Console.WriteLine("The chest was a trap! You lose 40 health.");
                _player.Health -= 40;
                _player.DisplayStats();

                if (_player.Health <= 0)
                {
                    _gameOver = true;
                }
            }
        }

        private void CheckGameEnd()
        {
            if (_gameOver)
            {
                Console.WriteLine("\nYou have been defeated. Game over.");
            }
            else if (_victory)
            {
                Console.WriteLine("\nCongratulations, you have defeated the bear and won the game!");
            }
        }

        private int GetChoice(int maxChoice)
        {
            int choice = 0;
            while (choice < 1 || choice > maxChoice)
            {
                Console.Write("Enter your choice: ");
                int.TryParse(Console.ReadLine(), out choice);
            }
            return choice;
        }
    }
}