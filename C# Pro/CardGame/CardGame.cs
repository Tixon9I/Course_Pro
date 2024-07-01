namespace CardGame
{
    /// <summary>
    /// Class that contains the main cycle of the card game "21"
    /// </summary>
    internal class CardGame
    {
        private Player _player = new Player();
        private Player _computer = new Player();
        private Deck _deck = new Deck();
        
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

                if (_player.SumPlayer >= 21 || _computer.SumPlayer >= 21 || skipCounter == 2)
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
            _player.Clear();
            _computer.Clear();

            var index = 0;
            
            if (leadPlayer == 0)
            {
                Console.WriteLine("Randomly determined who is first: you");

                // Player
                _player.CardsPlayer.Add(cards[index]);
                _player.CardsPlayer.Add(cards[++index]);
                _player.GetCardValue(_player.CardsPlayer);
                Console.WriteLine($"\nYou have received {cards[--index]} and {cards[++index]}. You have a total of {_player.SumPlayer} points");

                // Computer
                _computer.CardsPlayer.Add(cards[++index]);
                _computer.CardsPlayer.Add(cards[++index]);
                _computer.GetCardValue(_computer.CardsPlayer);
                Console.WriteLine($"\nComputer has received {cards[--index]} and {cards[++index]}. The computer has a total of {_computer.SumPlayer} points");

            }
            else
            {
                Console.WriteLine("Randomly determined who is first: computer");

                // Computer
                _computer.CardsPlayer.Add(cards[index]);
                _computer.CardsPlayer.Add(cards[++index]);
                _computer.GetCardValue(_computer.CardsPlayer);
                Console.WriteLine($"\nComputer has received {cards[--index]} and {cards[++index]}. The computer has a total of {_computer.SumPlayer} points");
                
                // Player
                _player.CardsPlayer.Add(cards[++index]);
                _player.CardsPlayer.Add(cards[++index]);
                _player.GetCardValue(_player.CardsPlayer);
                Console.WriteLine($"\nYou have received {cards[--index]} and {cards[++index]}. You have a total of {_player.SumPlayer} points");
            }
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
                _player.CardsPlayer.Add(cards[index]);
                _player.GetCardValue(_player.CardsPlayer);
                index++;
            }

            if (leadPlayer == 0 && (Math.Abs(21 - _computer.SumPlayer)) >= 4)
            {
                _computer.CardsPlayer.Add(cards[index]);
                _computer.GetCardValue(_computer.CardsPlayer);
                index++;
            }
            else if (leadPlayer == 0 && (Math.Abs(21 - _computer.SumPlayer)) < 4)
                skipCounter++;


            if (leadPlayer == 1 && (Math.Abs(21 - _computer.SumPlayer)) >= 4)
            {
                _computer.CardsPlayer.Add(cards[index]);
                _computer.GetCardValue (_computer.CardsPlayer);
                index++;
            }
            else if (leadPlayer == 1 && (Math.Abs(21 - _computer.SumPlayer)) < 4)
                skipCounter++;

            if (leadPlayer == 1 && playerModifier.Equals("player"))
            {
                _player.CardsPlayer.Add(cards[index]);
                _player.GetCardValue(_player.CardsPlayer);
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
            var countAcesPlayer = _player.CardsPlayer.Count(card => card[1].Equals("Ace"));
            var countAcesComputer = _computer.CardsPlayer.Count(card => card[1].Equals("Ace"));

            if (countAcesPlayer == 2)
                return _player;
               
            if (countAcesComputer == 2)
                return _computer;
          
            return null;
        }


        private Player Check21Points()
        {
            if (_player.SumPlayer == 21)
                return _player;
            
            if (_computer.SumPlayer == 21)
                return _computer;
            
            return null;
        }

        private Player CheckBust()
        {
            bool playerBust = _player.SumPlayer > 21;
            bool computerBust = _computer.SumPlayer > 21;

            if (playerBust && computerBust)
            {
                if (_player.SumPlayer > _computer.SumPlayer)
                    return _computer;

                if (_player.SumPlayer < _computer.SumPlayer)
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
            if (_player.SumPlayer > _computer.SumPlayer)
                return _player;
            
            if (_player.SumPlayer < _computer.SumPlayer)
                return _computer;
            
            return null;
        }
    }
}
