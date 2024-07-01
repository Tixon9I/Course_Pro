namespace CardGame
{
    /// <summary>
    /// Class that contains the main cycle of the card game "21"
    /// </summary>
    internal class CardGame
    {
        private readonly List<KeyValuePair<string, int>> _cardValue;
        private Player _player = new Player();
        private Player _computer = new Player();
        private Deck _deck = new Deck();
        
        public CardGame()
        {
            _cardValue = new List<KeyValuePair<string, int>>()
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

            _cardValue.Capacity = 9;
        }

        /// <summary>
        /// The main cycle of the game.
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="leadPlayer"></param>
        public void MainCycleGame()
        {
            var rng = new Random();

            var leadPlayer = rng.Next(0, 2);

            _deck.MixCards();

            var cards = _deck.ConvertDeckToStringList();

            //foreach (var card in cards)
            //    Console.Write(card + " ");

            InitialGame(cards, leadPlayer);

            var stopLoop = true;
            var countRound = 0;
            var skipCounter = 0;
            Player winner = null;

            while (stopLoop)
            {
                Console.WriteLine($"\n----Round #{++countRound}----");

                Console.WriteLine("You:");
                _player.PrintInfo();
      
                Console.WriteLine("\nComputer:");
                _computer.PrintInfo();

                Console.Write("\nNow choose an action: take another card or not (Y or N): ");
                var key = Console.ReadLine();

                if (key.Equals("Y"))
                {
                    ChooseCard(leadPlayer, cards, skipCounter, "player");

                    winner = CheckResultPlayers();
                }
                else if (key.Equals("N"))
                {
                    skipCounter = 1;

                    skipCounter = ChooseCard(leadPlayer, cards, skipCounter);

                    winner = CheckResultPlayers();
                }
                else
                    countRound--;

                if (_player._sumPlayer >= 21 || _computer._sumPlayer >= 21 || skipCounter == 2)
                {
                    var resultGame = string.Empty;

                    if (winner == _player)
                    {
                        Console.Clear();
                        Console.WriteLine("You won!");
                        resultGame = "You won!";
                        stopLoop = false;
                    }
                    else if (winner == _computer)
                    {
                        Console.Clear();
                        Console.WriteLine("Computer won!");
                        resultGame = "Computer won!";
                        stopLoop = false;
                    }
                    else if (winner == null)
                    {
                        Console.Clear();
                        Console.WriteLine("It's a draw!");
                        resultGame = "It's a draw!";
                        stopLoop = false;
                    }

                    Console.WriteLine("\nYou:");
                    _player.PrintInfo();
                    Console.WriteLine("\nComputer:");
                    _computer.PrintInfo();

                    Statistics.AddGameToStatistics(resultGame);
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
            _player._cardsPlayer.Clear();
            _computer._cardsPlayer.Clear();

            var index = 0;
            
            if (leadPlayer == 0)
            {
                Console.WriteLine("Randomly determined who is first: you");

                // Player
                _player._sumPlayer = GetCardValue(cards[index]) + GetCardValue(cards[++index]);
                Console.WriteLine($"\nYou have received {cards[--index]} and {cards[++index]}. You have a total of {_player._sumPlayer} points");
                _player._cardsPlayer.Add(cards[--index]);
                _player._cardsPlayer.Add(cards[++index]);

                // Computer
                _computer._sumPlayer = GetCardValue(cards[++index]) + GetCardValue(cards[++index]);
                Console.WriteLine($"\nComputer has received {cards[--index]} and {cards[++index]}. The computer has a total of {_computer._sumPlayer} points");
                _computer._cardsPlayer.Add(cards[--index]);
                _computer._cardsPlayer.Add(cards[++index]);

            }
            else
            {
                Console.WriteLine("Randomly determined who is first: computer");

                // Computer
                _computer._sumPlayer = GetCardValue(cards[index]) + GetCardValue(cards[++index]);
                Console.WriteLine($"\nComputer has received {cards[--index]} and {cards[++index]}. The computer has a total of {_computer._sumPlayer} points");
                _computer._cardsPlayer.Add(cards[--index]);
                _computer._cardsPlayer.Add(cards[++index]);

                // Player
                _player._sumPlayer = GetCardValue(cards[++index]) + GetCardValue(cards[++index]);
                Console.WriteLine($"\nYou have received {cards[--index]} and {cards[++index]}. You have a total of {_player._sumPlayer} points");
                _player._cardsPlayer.Add(cards[--index]);
                _player._cardsPlayer.Add(cards[++index]);
            }
        }

        private int GetCardValue(string card)
        {
            var cardTitle = card.Substring(1);
            var cardV = _cardValue.Find(k => k.Key.Equals(cardTitle));

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

        // The variable "index" represents the position of a card that the player can choose, with its initial value set to 4.
        private int index = 4;

        private int ChooseCard(int leadPlayer, List<string> cards, int skipCounter, string playerModifier = "computer")
        {
            if (leadPlayer == 0 && playerModifier.Equals("player"))
            {
                _player._sumPlayer += GetCardValue(cards[index]);
                _player._cardsPlayer.Add(cards[index]);
                index++;
            }

            if (leadPlayer == 0 && (Math.Abs(21 - _computer._sumPlayer)) >= 4)
            {
                _computer._sumPlayer += GetCardValue(cards[index]);
                _computer._cardsPlayer.Add(cards[index]);
                index++;
            }
            else if (leadPlayer == 0 && (Math.Abs(21 - _computer._sumPlayer)) < 4)
                skipCounter++;


            if (leadPlayer == 1 && (Math.Abs(21 - _computer._sumPlayer)) >= 4)
            {
                _computer._sumPlayer += GetCardValue(cards[index]);
                _computer._cardsPlayer.Add(cards[index]);
                index++;
            }
            else if (leadPlayer == 1 && (Math.Abs(21 - _computer._sumPlayer)) < 4)
                skipCounter++;

            if (leadPlayer == 1 && playerModifier.Equals("player"))
            {
                _player._sumPlayer += GetCardValue(cards[index]);
                _player._cardsPlayer.Add(cards[index]);
                index++;
            }

            return skipCounter;
        }

        /// <summary>
        /// Checks whether the conditions for completing the game are met.
        /// </summary>
        /// <returns></returns>
        private Player CheckResultPlayers()
        {
            return CheckTwoAces() ?? Check21Points() ?? CheckBust() ?? ComparePoints();
        }

        private Player CheckTwoAces()
        {
            var countAcesPlayer = _player._cardsPlayer.Count(card => card[1].Equals('A'));
            var countAcesComputer = _computer._cardsPlayer.Count(card => card[1].Equals('A'));

            if (countAcesPlayer == 2)
                return _player;
               
            if (countAcesComputer == 2)
                return _computer;
          
            return null;
        }


        private Player Check21Points()
        {
            if (_player._sumPlayer == 21)
                return _player;
            
            if (_computer._sumPlayer == 21)
                return _computer;
            
            return null;
        }

        private Player CheckBust()
        {
            bool playerBust = _player._sumPlayer > 21;
            bool computerBust = _computer._sumPlayer > 21;

            if (playerBust && computerBust)
            {
                if (_player._sumPlayer > _computer._sumPlayer)
                    return _computer;

                if (_player._sumPlayer < _computer._sumPlayer)
                    return _player;
            }
               

            if (playerBust && !computerBust)
                return _computer;
            
            if (!playerBust && computerBust)
                return _player;
            
            return null;
        }

        private Player ComparePoints()
        {
            if (_player._sumPlayer > _computer._sumPlayer)
                return _player;
            
            if (_player._sumPlayer < _computer._sumPlayer)
                return _computer;
            
            return null;
        }
    }
}
