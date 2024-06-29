using System.Text;

namespace CardGame
{
    struct Cards
    {
        public readonly string[] title;
        public readonly string[] suit;

        public Cards()
        {
            title = new string[] { "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
            suit = new string[] { "D", "H", "C", "S" };
        }
    }

    internal class CardGame
    {

        private readonly string[] title;
        private readonly string[] suit;

        private readonly List<KeyValuePair<string, int>> cardValue;

        private int sumPlayer = 0;
        private int sumComputer = 0;

        private List<string> cardsPlayer;
        private List<string> cardsComputer;
        private List<string> statistics;

        public CardGame(Cards cards)
        {
            title = cards.title;
            suit = cards.suit;

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

            cardsPlayer = new List<string>();
            cardsComputer = new List<string>();
            cardsPlayer.Capacity = 6;
            cardsComputer.Capacity = 6;

            statistics = new List<string>();
            statistics.Capacity = 10;
        }

        // Згенерувати впорядковану колоду карт
        public List<string> OrganizedCards()
        {
            var listCards = new List<string>();
            listCards.Capacity = 36;

            for (var i = 0; i < 4; i++)
            {
                var ch = suit[i];

                for (var j = 0; j < 9; j++)
                    listCards.Add(ch + title[j]);
            }

            return listCards;
        }

        // Перемішати колоду карт
        public List<string> SwapCards(List<string> cards)
        {
            var random = new Random();

            for (var i = 0; i < cards.Count; i++)
            {
                var card = cards[i];
                var valueSwapOrNot = random.Next(0, 3);

                if (valueSwapOrNot > 0)
                    for (var j = cards.Count - 1; j >= 0; j--)
                    {
                        var temp = cards[j];
                        cards[j] = cards[i];
                        cards[i] = temp;
                    }
            }

            return cards;
        }

        // Знайти позиції всіх тузів у колоді
        public string[] FoundingForAllAces(List<string> cards)
        {
            var positionAces = new string[5];
            var index = 0;
            for (var i = 0; i < cards.Count; i++)
                if (cards[i][1].Equals('A'))
                    positionAces[index++] += i;

            return positionAces;
        }

        // Перемістити всі пікові карти на початок колоди
        public List<string> SpadeCardsToStart(List<string> cards)
        {
            var index = 0;
            var spadeCards = 0;

            for (var i = 0; i < cards.Count; i++)
                if (spadeCards == 9)
                {
                    break;
                }
                else if (cards[i][0].Equals('S'))
                {
                    spadeCards++;

                    var temp = cards[i];
                    cards[i] = cards[index];
                    cards[index] = temp;

                    index++;
                }

            return cards;
        }

        // Відсортувати колоду
        public List<string> SortingCards(List<string> cards)
        {
            var indexSuit = 0;
            var indexTitle = 0;
            var indexCards = 0;
            var sort = 0; // Чотири картки однієї цінності, але різних мастей

            for (var i = 0; i < cards.Count; i++)
            {
                if (sort == 4)
                {
                    sort = 0;
                    indexSuit = 0;
                    indexTitle++;
                    i = indexCards;
                }
                if ((cards[i][0].ToString().Equals(suit[indexSuit]) && cards[i][1].ToString().Equals(title[indexTitle])) || (cards[i].Length == 3 && cards[i][0].ToString().Equals(suit[indexSuit]) && indexTitle == 4))
                {
                    sort++;
                    var temp = cards[i];
                    cards[i] = cards[indexCards];
                    cards[indexCards] = temp;

                    indexSuit++;
                    indexCards++;

                    i = indexCards;
                }
            }

            return cards;
        }

        // Метод, де відбувається цикл гри
        public void GameThroughPlayer(List<string> cards, int number)
        {
            var stopLoop = true;
            var countRound = 0;
            var result = string.Empty;

            InitialGame(cards, number);

            while (stopLoop)
            {
                Console.WriteLine($"\n----Round #{++countRound}----");

                PrintInfo();

                Console.Write("\nNow choose an action: take another card or not (Y or N): ");
                var key = Console.ReadLine();

                if (key.Equals("Y"))
                {
                    ChooseCard(number, cards, 'p');

                    result = CheckResultPlayers();
                }
                else if (key.Equals("N"))
                {
                    var exception = ChooseCard(number, cards);

                    if (exception == 2)
                    {
                        Console.Clear();

                        stopLoop = false;

                        var exceptionResult = sumPlayer > sumComputer ? "You won!" : sumPlayer < sumComputer ? "Computer won!" : "It's a draw!";

                        Console.WriteLine(exceptionResult);

                        PrintInfo();
                        ViewStatisticsGames(exceptionResult);

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

                    PrintInfo();
                    ViewStatisticsGames(result);
                }

                Console.WriteLine();
            }
        }

        // Ініціалізує початкові картки для двох гравців
        private void InitialGame(List<string> cards, int number)
        {
            sumPlayer = 0;
            sumComputer = 0;

            cardsPlayer.Clear();
            cardsComputer.Clear();

            SwapCards(cards);

            foreach (var card in cards)
                Console.Write($"{card} ");

            Console.WriteLine();

            var index = 0;

            if (number == 0)
            {
                Console.WriteLine("Randomly determined who is first: you");

                // Player
                sumPlayer = GetCardValue(cards[index]) + GetCardValue(cards[++index]);
                Console.WriteLine($"\nYou have received {cards[--index]} and {cards[++index]}. You have a total of {sumPlayer} points");
                cardsPlayer.Add(cards[--index]);
                cardsPlayer.Add(cards[++index]);

                // Computer
                sumComputer = GetCardValue(cards[++index]) + GetCardValue(cards[++index]);
                Console.WriteLine($"\nComputer has received {cards[--index]} and {cards[++index]}. The computer has a total of {sumComputer} points");
                cardsComputer.Add(cards[--index]);
                cardsComputer.Add(cards[++index]);

            }
            else
            {
                Console.WriteLine("Randomly determined who is first: computer");

                // Computer
                sumComputer = GetCardValue(cards[index]) + GetCardValue(cards[++index]);
                Console.WriteLine($"\nComputer has received {cards[--index]} and {cards[++index]}. The computer has a total of {sumComputer} points");
                cardsComputer.Add(cards[--index]);
                cardsComputer.Add(cards[++index]);

                // Player
                sumPlayer = GetCardValue(cards[++index]) + GetCardValue(cards[++index]);
                Console.WriteLine($"\nYou have received {cards[--index]} and {cards[++index]}. You have a total of {sumPlayer} points");
                cardsPlayer.Add(cards[--index]);
                cardsPlayer.Add(cards[++index]);
            }
        }

        // Отримуємо значення картки (цінність)
        private int GetCardValue(string card)
        {
            var cardTitle = card.Substring(1);
            var cardV = cardValue.Find(k => k.Key.Equals(cardTitle));

            return cardV.Value;
        }

        // Зручний вигляд інформації про картки та сума їх для гравців
        public void PrintInfo()
        {
            var builder = new StringBuilder();

            foreach (var card in cardsPlayer)
                builder.Append(" " + card.ToString());

            Console.WriteLine($"\nYour cards:{builder.ToString()}. Your sum = {sumPlayer}");

            builder.Clear();

            foreach (var card in cardsComputer)
                builder.Append(" " + card.ToString());

            Console.WriteLine($"Computer cards:{builder.ToString()}. Computer sum = {sumComputer}");
        }

        private int ChooseCard(int number, List<string> cards, char comp = 'c')
        {
            var index = 4;
            var exception = 1; // Виняткова ситуація, коли двоє гравців відмовились від взяття картки

            if (number == 0 && comp.Equals('p'))
            {
                sumPlayer += GetCardValue(cards[index]);
                cardsPlayer.Add(cards[index]);
                index++;
            }

            if (number == 0 && (Math.Abs(21 - sumComputer)) >= 4)
            {
                sumComputer += GetCardValue(cards[index]);
                cardsComputer.Add(cards[index]);
                index++;
            }
            else if (number == 0 && (Math.Abs(21 - sumComputer)) < 4)
                exception++;


            if (number == 1 && (Math.Abs(21 - sumComputer)) >= 4)
            {
                sumComputer += GetCardValue(cards[index]);
                cardsComputer.Add(cards[index]);
                index++;
            }
            else if (number == 1 && (Math.Abs(21 - sumComputer)) < 4)
                exception++;

            if (number == 1 && comp.Equals('p'))
            {
                sumPlayer += GetCardValue(cards[index]);
                cardsPlayer.Add(cards[index]);
                index++;
            }

            return exception;
        }

        // Метод, де обробляються правила гри (точніше її завершення)
        private string CheckResultPlayers()
        {
            var countAcesPlayer = 0;
            var countAcesComputer = 0;
            var winPlayer = "You won!";
            var winComputer = "Computer won!";
            var drawGame = "The game ended in a draw!";

            foreach (var cardPlayer in cardsPlayer)
            {
                if (cardPlayer[0].ToString().Equals(title[title.Length - 1]))
                    countAcesPlayer++;
            }

            foreach (var cardComputer in cardsComputer)
            {
                if (cardsComputer[0].ToString().Equals(title[title.Length - 1]))
                    countAcesComputer++;
            }

            if (countAcesPlayer == 2)
                return winPlayer;
            else if (countAcesComputer == 2)
                return winComputer;
            else if (sumPlayer == 21)
                return winPlayer;
            else if (sumComputer == 21)
                return winComputer;
            else if (sumPlayer == 21 && sumComputer == 21)
                return drawGame;
            else if (sumPlayer > 21 && sumComputer < 21)
                return winComputer;
            else if (sumComputer > 21 && sumPlayer < 21)
                return winPlayer;
            else if (sumPlayer > 21 && sumComputer > 21 && sumPlayer > sumComputer)
                return winComputer;
            else if (sumPlayer > 21 && sumComputer > 21 && sumPlayer < sumComputer)
                return winPlayer;
            else
                return "Continue";
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
