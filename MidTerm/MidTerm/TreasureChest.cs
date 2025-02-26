// TreasureChest.cs
using System;

namespace AdventureGame
{
    public class TreasureChest
    {
        private Random _random;

        public TreasureChest()
        {
            _random = new Random();
        }

        public void Open(Player player)
        {
            Console.WriteLine("\nYou open the chest...");

            // Randomly determine the type of reward or penalty
            int outcomeType = _random.Next(6); // 0-2: Positive, 3-5: Negative
            int value = _random.Next(10, 31); // Random value between 10 and 30

            switch (outcomeType)
            {
                case 0: // Increase health
                    player.Health += value;
                    Console.WriteLine($"You find a healing potion and gain {value} health!");
                    break;
                case 1: // Increase strength
                    player.Strength += value;
                    Console.WriteLine($"You find a strength elixir and gain {value} strength!");
                    break;
                case 2: // Increase luck
                    player.Luck += value;
                    Console.WriteLine($"You find a lucky charm and gain {value} luck!");
                    break;
                case 3: // Decrease health
                    player.Health -= value;
                    Console.WriteLine($"You trigger a trap and lose {value} health!");
                    break;
                case 4: // Decrease strength
                    player.Strength -= value;
                    Console.WriteLine($"You consume a cursed potion and lose {value} strength!");
                    break;
                case 5: // Decrease luck
                    player.Luck -= value;
                    Console.WriteLine($"You are cursed and lose {value} luck!");
                    break;
            }

            // Ensure stats don't go below 0
            if (player.Health < 0) player.Health = 0;
            if (player.Strength < 0) player.Strength = 0;
            if (player.Luck < 0) player.Luck = 0;

            player.DisplayStats();

            // Check if the player has been defeated by a health penalty
            if (player.Health <= 0)
            {
                Console.WriteLine("You have been defeated by the trap. Game over.");
                Environment.Exit(0);
            }
        }
    }
}