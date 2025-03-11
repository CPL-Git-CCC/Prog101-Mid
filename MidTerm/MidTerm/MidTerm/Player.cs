// Player.cs
namespace AdventureGame
{
    public class Player
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Strength { get; set; }
        public int Luck { get; set; }

        public Player(string name, int health, int strength, int luck)
        {
            Name = name;
            Health = health;
            Strength = strength;
            Luck = luck;
        }

        public void DisplayStats()
        {
            Console.WriteLine($"\n{Name}'s Stats:");
            Console.WriteLine($"Health: {Health}");
            Console.WriteLine($"Strength: {Strength}");
            Console.WriteLine($"Luck: {Luck}\n");
        }
    }
}