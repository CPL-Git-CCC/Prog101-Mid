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
            // Randomly determine the type of reward or penalty
            int outcomeType = _random.Next(6); // 0-2: Positive, 3-5: Negative
            int value = _random.Next(1, 6); // Random value between 1 and 5

            switch (outcomeType)
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
                case 3: // Decrease health
                    character.Health -= value;
                    Console.WriteLine($"{character.GetType().Name} loses {value} health!");
                    break;
                case 4: // Decrease strength
                    character.Strength -= value;
                    Console.WriteLine($"{character.GetType().Name} loses {value} strength!");
                    break;
                case 5: // Decrease luck
                    character.Luck -= value;
                    Console.WriteLine($"{character.GetType().Name} loses {value} luck!");
                    break;
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