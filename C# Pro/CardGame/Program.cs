namespace CardGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CardGame _game = new CardGame();

            var stopLoop = true;
            var keyContinue = false;
            
            Console.Write(@"Welcome to the card game ""21""
Rules:
- Each card has its own value: Ace - 11 points; King - 4 points; Queen - 3 points; Jack - 2 points; Other cards at their face value.
- You will play against the computer.
- Goal of the game is to score 21 points.
- You and the computer get 2 cards at once.
- It is randomly chosen who will start first. Depending on who is first, they choose to either take another card or not.
- If one of you scores 21 points or 2 aces, the game is over and the player with 21 points or 2 aces wins.
- If one of the players scores more than 21 points, the game ends, but at the end of the round.
- If you both have more than 21 points, the game is over and the player with the lower number of points wins.

If everything is clear, enter ""C"" to start the game or exit with ""E"": ");

            while (stopLoop)
            {
                var key = Console.ReadLine();

                if (key.Equals("C"))
                {
                    Console.Clear();

                    _game.MainCycleGame();

                    keyContinue = true;
                }
                else if (key.Equals("E"))
                {
                    Console.Clear();
                    stopLoop = false;
                    Console.WriteLine("We hope you enjoyed the game!");
                }
                else if (key.Equals("S") && keyContinue == true)
                {
                    Console.Clear();
                    keyContinue = false;
                    Statistics.ViewStatisticsGames();
                    
                    Console.Write("Please enter \"C\" to start next game or exit with \"E\": ");
                }
                else
                    Console.Write("Please enter \"C\" to start the game or exit with \"E\": ");

                if (keyContinue)
                {
                    Console.Write(@"We hope you enjoyed the game! 
You can now view the game statistics (press ""S""). 
Press ""C"" to continue playing or ""E"" to quit or ""S"" to view statistics: ");
                }
            }
        }
    }
}
