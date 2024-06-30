namespace CardGame
{
    /// <summary>
    /// Class that contains the main cycle of the card game "21"
    /// </summary>
    internal class CardGame
    {
        private readonly List<KeyValuePair<string, int>> cardValue;
        private Players players = new Players();
        private Cards crds = new Cards();
        private Statistics statistics = new Statistics();

        public CardGame()
        {
            cardValue = new List<KeyValuePair<string, int>>()
            {
                new KeyValuePair<string, int>("6", 6),
                new KeyValuePair<string, int>("7", 7),
                new KeyValuePair<string, int>("8", 8),
                new KeyValuePair<string, int>("9", 9),
                new KeyValuePair<string, int>("10", 10),
                new KeyValuePair<string, int>("J", 2),
                new KeyValuePair<string, int>("Q", 3),
                new KeyValuePair<string, int>("K", 4),
                new KeyValuePair<string, int>("A", 11)
            };

            cardValue.Capacity = 9;
        }

        /// <summary>
        /// The main cycle of the game.
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="leadPlayer"></param>
        public void MainCycleGame(List<string> cards, int leadPlayer)
        {
            var stopLoop = true;
            var countRound = 0;
            var skipCounter = 0;
            var result = string.Empty;

            InitialGame(cards, leadPlayer);

            while (stopLoop)
            {
                Console.WriteLine($"\n----Round #{++countRound}----");

                players.PrintInfo();

                Console.Write("\nNow choose an action: take another card or not (Y or N): ");
                var key = Console.ReadLine();

                if (key.Equals("Y"))
                {
                    ChooseCard(leadPlayer, cards, skipCounter, "player");

                    result = CheckResultPlayers();
                }
                else if (key.Equals("N"))
                {
                    skipCounter++;

                    skipCounter = ChooseCard(leadPlayer, cards, skipCounter);

                    if (skipCounter == 2)
                    {
                        Console.Clear();

                        stopLoop = false;

                        var exceptionResult = players.sumPlayer > players.sumComputer ? "You won!" : players.sumPlayer < players.sumComputer ? "Computer won!" : "It's a draw!";
                        
                        Console.WriteLine(exceptionResult);

                        players.PrintInfo();
                        statistics.ViewStatisticsGames(exceptionResult);

                        result = "Continue";
                    }
                    else
                        result = CheckResultPlayers();
                }
                else
                    countRound--;

                if (!result.Contains("Continue"))
                {
                    switch (result)
                    {
                        case "You won!": Console.Clear(); Console.WriteLine(result); stopLoop = false; break;
                        case "Computer won!": Console.Clear(); Console.WriteLine(result); stopLoop = false; break;
                        case "The game ended in a draw!": Console.Clear(); Console.WriteLine(result); stopLoop = false; break;
                    }

                    players.PrintInfo();
                    statistics.ViewStatisticsGames(result);
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Initialize the game leader and cards.
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="leadPlayer"></param>
        private void InitialGame(List<string> cards, int leadPlayer)
        {
            var index = 0;
            crds.MixCards(cards);

            if (leadPlayer == 0)
            {
                Console.WriteLine("Randomly determined who is first: you");

                // Player
                players.sumPlayer = GetCardValue(cards[index]) + GetCardValue(cards[++index]);
                Console.WriteLine($"\nYou have received {cards[--index]} and {cards[++index]}. You have a total of {players.sumPlayer} points");
                players.cardsPlayer.Add(cards[--index]);
                players.cardsPlayer.Add(cards[++index]);

                // Computer
                players.sumComputer = GetCardValue(cards[++index]) + GetCardValue(cards[++index]);
                Console.WriteLine($"\nComputer has received {cards[--index]} and {cards[++index]}. The computer has a total of {players.sumComputer} points");
                players.cardsComputer.Add(cards[--index]);
                players.cardsComputer.Add(cards[++index]);

            }
            else
            {
                Console.WriteLine("Randomly determined who is first: computer");

                // Computer
                players.sumComputer = GetCardValue(cards[index]) + GetCardValue(cards[++index]);
                Console.WriteLine($"\nComputer has received {cards[--index]} and {cards[++index]}. The computer has a total of {players.sumComputer} points");
                players.cardsComputer.Add(cards[--index]);
                players.cardsComputer.Add(cards[++index]);

                // Player
                players.sumPlayer = GetCardValue(cards[++index]) + GetCardValue(cards[++index]);
                Console.WriteLine($"\nYou have received {cards[--index]} and {cards[++index]}. You have a total of {players.sumPlayer} points");
                players.cardsPlayer.Add(cards[--index]);
                players.cardsPlayer.Add(cards[++index]);
            }
        }

        private int GetCardValue(string card)
        {
            var cardTitle = card.Substring(1);
            var cardV = cardValue.Find(k => k.Key.Equals(cardTitle));

            return cardV.Value;
        }

        /// <summary>
        /// Method for selecting a card from the deck.
        /// </summary>
        /// <param name="leadPlayer"></param>
        /// <param name="cards"></param>
        /// <param name="skipCounter">This variable tracks skips in card drawing. If it equals 2, the game ends.</param>
        /// <param name="playerModifier">Specifies the player or condition affecting card selection.</param>
        /// <returns></returns>
        private int ChooseCard(int leadPlayer, List<string> cards, int skipCounter, string playerModifier = "computer")
        {
            // The variable "index" represents the position of a card that the player can choose, with its initial value set to 4.
            var index = 4;
            
            if (leadPlayer == 0 && playerModifier.Equals("player"))
            {
                players.sumPlayer += GetCardValue(cards[index]);
                players.cardsPlayer.Add(cards[index]);
                index++;
            }

            if (leadPlayer == 0 && (Math.Abs(21 - players.sumComputer)) >= 4)
            {
                players.sumComputer += GetCardValue(cards[index]);
                players.cardsComputer.Add(cards[index]);
                index++;
            }
            else if (leadPlayer == 0 && (Math.Abs(21 - players.sumComputer)) < 4)
                skipCounter++;


            if (leadPlayer == 1 && (Math.Abs(21 - players.sumComputer)) >= 4)
            {
                players.sumComputer += GetCardValue(cards[index]);
                players.cardsComputer.Add(cards[index]);
                index++;
            }
            else if (leadPlayer == 1 && (Math.Abs(21 - players.sumComputer)) < 4)
                skipCounter++;

            if (leadPlayer == 1 && playerModifier.Equals("player"))
            {
                players.sumPlayer += GetCardValue(cards[index]);
                players.cardsPlayer.Add(cards[index]);
                index++;
            }

            return skipCounter;
        }

        /// <summary>
        /// Checks whether the conditions for completing the game are met.
        /// </summary>
        /// <returns></returns>
        private string CheckResultPlayers()
        {
            var countAcesPlayer = players.cardsPlayer.Count(card => card[1].ToString().Equals(crds.title[crds.title.Length - 1]));
            var countAcesComputer = players.cardsComputer.Count(card => card[1].ToString().Equals(crds.title[crds.title.Length - 1]));

            var winPlayer = "You won!";
            var winComputer = "Computer won!";
            var drawGame = "The game ended in a draw!";

       
            if (countAcesPlayer == 2)
                return winPlayer;
            else if (countAcesComputer == 2)
                return winComputer;

            
            if (players.sumPlayer == 21 && players.sumComputer == 21)
                return drawGame;
            else if (players.sumPlayer == 21)
                return winPlayer;
            else if (players.sumComputer == 21)
                return winComputer;

     
            bool playerBust = players.sumPlayer > 21;
            bool computerBust = players.sumComputer > 21;

            if (playerBust && !computerBust)
                return winComputer;
            else if (!playerBust && computerBust)
                return winPlayer;
            else if (playerBust && computerBust)
            {
                if (players.sumPlayer > players.sumComputer)
                    return winComputer;
                else if (players.sumPlayer < players.sumComputer)
                    return winPlayer;
            }

            return "Continue";
        }
    }
}
