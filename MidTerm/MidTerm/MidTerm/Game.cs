// Game.cs
namespace AdventureGame
{
    public class Game
    {
        private Player _player;
        private Bear _bear;
        private Maze _maze;

        public Game(Player player, Bear bear)
        {
            _player = player;
            _bear = bear;
            _maze = new Maze(player, bear);
        }

        public void Start()
        {
            Console.WriteLine($"\nWelcome, {_player.Name}! Your adventure begins...");
            _player.DisplayStats();
            _bear.DisplayStats();

            _maze.Explore();
        }
    }
}