// Maze.cs
using System;
using System.Collections.Generic;

namespace AdventureGame
{
    public class Maze
    {
        private const int GridSize = 10; // 10x10 grid
        private Player _player;
        private Bear _bear;
        private List<TreasureChest> _treasureChests;
        private Random _random;
        private int _playerX, _playerY; // Player's position
        private int _bearX, _bearY;     // Bear's position
        private List<(int, int)> _chestPositions; // Positions of treasure chests

        public Maze(Player player, Bear bear)
        {
            _player = player;
            _bear = bear;
            _treasureChests = new List<TreasureChest>();
            _chestPositions = new List<(int, int)>();
            _random = new Random();

            // Initialize player and bear positions
            _playerX = _random.Next(GridSize);
            _playerY = _random.Next(GridSize);

            do
            {
                _bearX = _random.Next(GridSize);
                _bearY = _random.Next(GridSize);
            } while (_bearX == _playerX && _bearY == _playerY); // Ensure bear is not at player's position

            // Randomly generate the number of treasure chests (between 3 and 7)
            int numberOfChests = _random.Next(3, 8);

            // Initialize treasure chests
            for (int i = 0; i < numberOfChests; i++)
            {
                int chestX, chestY;
                do
                {
                    chestX = _random.Next(GridSize);
                    chestY = _random.Next(GridSize);
                } while ((chestX == _playerX && chestY == _playerY) || (chestX == _bearX && chestY == _bearY) || _chestPositions.Contains((chestX, chestY))); // Ensure chest is not at player's, bear's, or another chest's position

                _chestPositions.Add((chestX, chestY));
                _treasureChests.Add(new TreasureChest());
            }

            Console.WriteLine($"There are {numberOfChests} treasure chests hidden in the maze!");
        }

        public void Explore()
        {
            while (true)
            {
                Console.WriteLine($"\nYou are at position ({_playerX}, {_playerY}).");
                Console.WriteLine("Where would you like to move?");
                Console.WriteLine("1. Up");
                Console.WriteLine("2. Down");
                Console.WriteLine("3. Left");
                Console.WriteLine("4. Right");

                int choice = GetChoice(4);

                switch (choice)
                {
                    case 1: MovePlayer(0, -1); break; // Up
                    case 2: MovePlayer(0, 1); break;  // Down
                    case 3: MovePlayer(-1, 0); break; // Left
                    case 4: MovePlayer(1, 0); break;  // Right
                }

                MoveBear(); // Bear moves after the player (hidden from the player)
                CheckForEvents();
            }
        }

        private void MovePlayer(int deltaX, int deltaY)
        {
            int newX = _playerX + deltaX;
            int newY = _playerY + deltaY;

            if (newX >= 0 && newX < GridSize && newY >= 0 && newY < GridSize)
            {
                _playerX = newX;
                _playerY = newY;
                Console.WriteLine($"You move to position ({_playerX}, {_playerY}).");
            }
            else
            {
                Console.WriteLine("You cannot move outside the maze!");
            }
        }

        private void MoveBear()
        {
            // Bear moves randomly in one of four directions
            int direction = _random.Next(4);

            int newBearX = _bearX;
            int newBearY = _bearY;

            switch (direction)
            {
                case 0: newBearY--; break; // Up
                case 1: newBearY++; break; // Down
                case 2: newBearX--; break; // Left
                case 3: newBearX++; break; // Right
            }

            // Ensure the bear stays within the grid
            if (newBearX >= 0 && newBearX < GridSize && newBearY >= 0 && newBearY < GridSize)
            {
                _bearX = newBearX;
                _bearY = newBearY;
            }
        }

        private void CheckForEvents()
        {
            if (_playerX == _bearX && _playerY == _bearY)
            {
                EncounterBear();
            }
            else
            {
                CheckForTreasureChests();
            }
        }

        private void EncounterBear()
        {
            Console.WriteLine("\nYou encounter a wild bear!");
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

        private void FightBear()
        {
            Console.WriteLine("\nYou decide to fight the bear!");
            int playerDamage = _player.Strength;
            int bearDamage = _bear.Strength;

            Console.WriteLine($"You attack the bear and deal {playerDamage} damage!");
            _bear.Health -= playerDamage;

            if (_bear.Health > 0)
            {
                Console.WriteLine($"The bear attacks you and deals {bearDamage} damage!");
                _player.Health -= bearDamage;
                _player.DisplayStats();
                _bear.DisplayStats();

                if (_player.Health <= 0)
                {
                    Console.WriteLine("You have been defeated by the bear. Game over.");
                    Environment.Exit(0);
                }
            }
            else
            {
                Console.WriteLine("You have defeated the bear!");
                Console.WriteLine("Congratulations, you have won the game!");
                Environment.Exit(0);
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
                    Console.WriteLine("You have been defeated by the bear. Game over.");
                    Environment.Exit(0);
                }
            }
        }

        private void CheckForTreasureChests()
        {
            for (int i = 0; i < _chestPositions.Count; i++)
            {
                if (_playerX == _chestPositions[i].Item1 && _playerY == _chestPositions[i].Item2)
                {
                    Console.WriteLine("\nYou find a treasure chest!");
                    Console.WriteLine("1. Open the chest.");
                    Console.WriteLine("2. Ignore the chest and keep walking.");

                    int choice = GetChoice(2);

                    if (choice == 1)
                    {
                        _treasureChests[i].Open(_player);
                        _chestPositions.RemoveAt(i); // Remove the chest after it's opened
                        _treasureChests.RemoveAt(i);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("You decide to ignore the chest and continue your journey.");
                    }
                }
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