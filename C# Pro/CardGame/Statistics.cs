namespace CardGame
{
    /// <summary>
    /// The class responsible for saving game statistics.
    /// </summary>
    internal class Statistics
    {
        public static List<string> statistics { get; set; }

        public Statistics()
        {
            statistics = new List<string>();
            statistics.Capacity = 10;
        }

        public List<string> ViewStatisticsGames(string result = " ")
        {
            switch (result)
            {
                case "You won!": statistics.Add($"Player > Computer\n"); break;
                case "Computer won!": statistics.Add($"Player < Computer\n"); break;
                case "It's a draw!": statistics.Add($"Player = Computer\n"); break;
            }

            return statistics;
        }
    }
}
