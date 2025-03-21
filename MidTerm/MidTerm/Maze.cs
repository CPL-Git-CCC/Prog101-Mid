﻿// Maze.cs
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

            // Randomly generate the number of treasure chests (between 1 and 5)
            int numberOfChests = _random.Next(1, 6);

            // Initialize treasure chests
            for (int i = 0; i < numberOfChests; i++)
            {
                AddNewChest();
            }

            Console.WriteLine($"There are {numberOfChests} treasure chests hidden in the maze!");
        }

        private void AddNewChest()
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

        public void Explore()
        {
            while (true)
            {
                DisplayMaze();
                Console.WriteLine("\nUse WASD or Arrow Keys to move.");

                ConsoleKeyInfo keyInfo = Console.ReadKey(true); // Read key input without displaying it
                HandleMovement(keyInfo);

                MoveBear(); // Bear moves after the player
                CheckForEvents();
            }
        }

        private void HandleMovement(ConsoleKeyInfo keyInfo)
        {
            int deltaX = 0, deltaY = 0;

            switch (keyInfo.Key)
            {
                case ConsoleKey.W:        // W key
                case ConsoleKey.UpArrow:  // Up arrow
                    deltaY = -1;
                    break;
                case ConsoleKey.S:        // S key
                case ConsoleKey.DownArrow:// Down arrow
                    deltaY = 1;
                    break;
                case ConsoleKey.A:        // A key
                case ConsoleKey.LeftArrow:// Left arrow
                    deltaX = -1;
                    break;
                case ConsoleKey.D:        // D key
                case ConsoleKey.RightArrow:// Right arrow
                    deltaX = 1;
                    break;
                default:
                    Console.WriteLine("Invalid key. Use WASD or Arrow Keys to move.");
                    return;
            }

            MovePlayer(deltaX, deltaY);
        }

        private void DisplayMaze()
        {
            Console.WriteLine("\nMaze Layout:");
            for (int y = 0; y < GridSize; y++)
            {
                for (int x = 0; x < GridSize; x++)
                {
                    if (x == _playerX && y == _playerY)
                    {
                        Console.Write("P "); // Player
                    }
                    else if (x == _bearX && y == _bearY)
                    {
                        Console.Write("B "); // Bear
                    }
                    else if (_chestPositions.Contains((x, y)))
                    {
                        Console.Write("T "); // Treasure Chest
                    }
                    else
                    {
                        Console.Write(". "); // Empty space
                    }
                }
                Console.WriteLine();
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
                Console.WriteLine($"The bear moves to position ({_bearX}, {_bearY}).");

                // Check if the bear lands on a treasure chest
                CheckForBearChestInteraction();
            }
        }

        private void CheckForBearChestInteraction()
        {
            for (int i = 0; i < _chestPositions.Count; i++)
            {
                if (_bearX == _chestPositions[i].Item1 && _bearY == _chestPositions[i].Item2)
                {
                    _treasureChests[i].Open(_bear);
                    _chestPositions.RemoveAt(i); // Remove the chest after it's opened
                    _treasureChests.RemoveAt(i);
                    AddNewChest(); // Add a new chest to the map
                    break;
                }
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

            // Teleport both the player and the bear to new random positions
            TeleportPlayerAndBear();
        }

        private void TeleportPlayerAndBear()
        {
            // Teleport the player
            do
            {
                _playerX = _random.Next(GridSize);
                _playerY = _random.Next(GridSize);
            } while (_chestPositions.Contains((_playerX, _playerY))); // Ensure the new position is not on a chest

            // Teleport the bear
            do
            {
                _bearX = _random.Next(GridSize);
                _bearY = _random.Next(GridSize);
            } while ((_bearX == _playerX && _bearY == _playerY) || _chestPositions.Contains((_bearX, _bearY))); // Ensure the bear is not on the player or a chest

            Console.WriteLine("\nYou and the bear have been teleported to new locations in the maze!");
            DisplayMaze();
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
                        AddNewChest(); // Add a new chest to the map
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