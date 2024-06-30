namespace CardGame
{
    /// <summary>
    /// The class is designed to save, generate, shuffle, find and percentage a deck of cards.
    /// </summary>
    internal class Cards
    {
        
        public readonly string[] title = new string[] { "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
        public readonly string[] suit = new string[] { "D", "H", "C", "S" };

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

        public List<string> MixCards(List<string> cards)
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

        public string[] FoundingForAllAces(List<string> cards)
        {
            var positionAces = new string[4];
            var index = 0;
            for (var i = 0; i < cards.Count; i++)
                if (cards[i][1].Equals('A'))
                    positionAces[index++] += i;

            return positionAces;
        }

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

        public List<string> SortingCards(List<string> cards)
        {
            var indexSuit = 0;
            var indexTitle = 0;
            var positionCards = 0;
            var fourOfAKindCounter = 0;

            for (var i = 0; i < cards.Count; i++)
            {
                if (fourOfAKindCounter == 4)
                {
                    fourOfAKindCounter = 0;
                    indexSuit = 0;
                    indexTitle++;
                    i = positionCards;
                }
                if ((cards[i][0].ToString().Equals(suit[indexSuit]) && cards[i][1].ToString().Equals(title[indexTitle])) || (cards[i].Length == 3 && cards[i][0].ToString().Equals(suit[indexSuit]) && indexTitle == 4))
                {
                    fourOfAKindCounter++;
                    var temp = cards[i];
                    cards[i] = cards[positionCards];
                    cards[positionCards] = temp;

                    indexSuit++;
                    positionCards++;

                    i = positionCards;
                }
            }

            return cards;
        }
    }
}
