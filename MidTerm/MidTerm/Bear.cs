// Bear.cs
namespace AdventureGame
{
    public class Bear
    {
        public int Health { get; set; }
        public int Strength { get; set; }
        public int Luck { get; set; }

        public Bear(int health, int strength, int luck)
        {
            Health = health;
            Strength = strength;
            Luck = luck;
        }

        public void DisplayStats()
        {
            Console.WriteLine("\nBear's Stats:");
            Console.WriteLine($"Health: {Health}");
            Console.WriteLine($"Strength: {Strength}");
            Console.WriteLine($"Luck: {Luck}\n");
        }
    }
}