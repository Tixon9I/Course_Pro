namespace CardGame
{
    /// <summary>
    /// The class responsible for saving game statistics.
    /// </summary>
    internal class Statistics
    {
        private static List<string> _statistics = new List<string>();
        private const string _filePath = @"C:\D\C# Pro\C# Pro\CardGame\statistics.txt";
        private static int _gameCount = 0;
        private static int _wins = 0;
        private static int _losses = 0;
        private static int _draws = 0;

        static Statistics()
        {
            //ClearFile();
            LoadStatistics();
        }

        public static void AddGameToStatistics(string result)
        {
            _gameCount++;
            string gameResult = $"{_gameCount}. ";

            switch (result)
            {
                case "You won!":
                    gameResult += "Player > Computer";
                    _wins++;
                    break;
                case "Computer won!":
                    gameResult += "Player < Computer";
                    _losses++;
                    break;
                case "It's a draw!":
                    gameResult += "Player = Computer";
                    _draws++;
                    break;
                default:
                    gameResult += "Unknown result";
                    break;
            }

            _statistics.Add(gameResult);
            SaveStatistics();
        }

        public static void ViewStatisticsGames()
        {
            Console.WriteLine($"Your last game/s:");
            foreach (var game in _statistics)
                Console.WriteLine(game);

            Console.WriteLine($"Wins: {_wins}, Losses: {_losses}, Draws: {_draws}");
            Console.WriteLine();
        }

        private static void SaveStatistics()
        {
            try
            {
                File.WriteAllLines(_filePath, _statistics);
            }
            catch (IOException e)
            {
                Console.WriteLine($"Error saving statistics: {e.Message}");
            }
        }

        private static void LoadStatistics()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    _statistics = File.ReadAllLines(_filePath).ToList();
                    _gameCount = _statistics.Count;

                    _wins = _statistics.Count(s => s.Contains("Player > Computer"));
                    _losses = _statistics.Count(s => s.Contains("Player < Computer"));
                    _draws = _statistics.Count(s => s.Contains("Player = Computer"));
                }
            }
            catch (IOException e)
            {
                Console.WriteLine($"Error loading statistics: {e.Message}");
            }
        }

        public static void ClearFile()
        {
            File.WriteAllText(_filePath, string.Empty);
            _gameCount = 0;
            _wins = 0;
            _losses = 0;
            _draws = 0;
        }
    }
}
