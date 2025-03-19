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
            ApplyRewardOrPenalty(player);
        }

        public void Open(Bear bear)
        {
            Console.WriteLine("\nThe bear opens the chest...");
            ApplyRewardOrPenalty(bear);
        }

        private void ApplyRewardOrPenalty(dynamic character)
        {
            // Determine the chance of getting a boost based on Luck (0-10)
            int luckChance = character.Luck * 10; // Luck ranges from 0 to 10, so luckChance ranges from 0% to 100%
            int outcomeType = _random.Next(100); // Random number between 0 and 99

            // If outcomeType is less than luckChance, it's a positive outcome (boost)
            bool isBoost = outcomeType < luckChance;

            int value = _random.Next(1, 6); // Random value between 1 and 5

            if (isBoost)
            {
                // Positive outcomes (boost)
                int boostType = _random.Next(3); // 0: Health, 1: Strength, 2: Luck
                switch (boostType)
                {
                    case 0: // Increase health
                        character.Health += value;
                        Console.WriteLine($"{character.GetType().Name} gains {value} health!");
                        break;
                    case 1: // Increase strength
                        character.Strength += value;
                        Console.WriteLine($"{character.GetType().Name} gains {value} strength!");
                        break;
                    case 2: // Increase luck
                        character.Luck += value;
                        Console.WriteLine($"{character.GetType().Name} gains {value} luck!");
                        break;
                }
            }
            else
            {
                // Negative outcomes (penalty)
                int penaltyType = _random.Next(3); // 0: Health, 1: Strength, 2: Luck
                switch (penaltyType)
                {
                    case 0: // Decrease health
                        character.Health -= value;
                        Console.WriteLine($"{character.GetType().Name} loses {value} health!");
                        break;
                    case 1: // Decrease strength
                        character.Strength -= value;
                        Console.WriteLine($"{character.GetType().Name} loses {value} strength!");
                        break;
                    case 2: // Decrease luck
                        character.Luck -= value;
                        Console.WriteLine($"{character.GetType().Name} loses {value} luck!");
                        break;
                }
            }

            // Ensure stats don't go below 0
            if (character.Health < 0) character.Health = 0;
            if (character.Strength < 0) character.Strength = 0;
            if (character.Luck < 0) character.Luck = 0;

            character.DisplayStats();

            // Check if the character has been defeated by a health penalty
            if (character.Health <= 0)
            {
                if (character is Player)
                {
                    Console.WriteLine("You have been defeated by the trap. Game over.");
                    Environment.Exit(0);
                }
                else if (character is Bear)
                {
                    Console.WriteLine("The bear has been defeated by the trap!");
                }
            }
        }
    }
}