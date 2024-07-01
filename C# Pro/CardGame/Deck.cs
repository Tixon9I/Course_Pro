namespace CardGame
{
    struct Card
    {
        public readonly Title Title;
        public readonly Suit Suit;

        public Card(Suit suit, Title title)
        {
            Title = title;
            Suit = suit;
        }
    }

    /// <summary>
    /// The class is designed to save, generate, shuffle, find and percentage a deck of cards.
    /// </summary>
    internal class Deck
    {
        private List<Card> _cards = new List<Card>();

        public Deck()
        {
            OrganizedCards();
        }
        
        private void OrganizedCards()
        {
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Title title in Enum.GetValues(typeof(Title)))
                    _cards.Add(new Card(suit, title));
            }
        }

        public void MixCards()
        {
            Random random = new Random();
            for (var i = _cards.Count - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                var temp = _cards[i];
                _cards[i] = _cards[j];
                _cards[j] = temp;
            }
        }

        public int[] FoundingForAllAces()
        {
            var positionAces = new int[4];

            var index = 0;
            for (var i = 0; i < _cards.Count; i++)
                if (_cards[i].Title.Equals(Title.Ace))
                    positionAces[index++] += i;

            return positionAces;
        }

        public void SpadeCardsToStart()
        {
            var index = 0;
            var spadeCards = 0;

            for (var i = 0; i < _cards.Count; i++)
                if (spadeCards == 9)
                {
                    break;
                }
                else if (_cards[i].Suit.Equals(Suit.S))
                {
                    spadeCards++;

                    var temp = _cards[i];
                    _cards[i] = _cards[index];
                    _cards[index] = temp;

                    index++;
                }
        }

        public void SortingCards()
        {
            var indexSuit = 0;
            var indexTitle = 6;
            var positionCards = 0;
            var fourOfAKindCounter = 0;

            for (var i = 0; i < _cards.Count; i++)
            {
                if (fourOfAKindCounter == 4)
                {
                    fourOfAKindCounter = 0;
                    indexSuit = 0;
                    indexTitle++;
                }
                if ((_cards[i].Suit.Equals((Suit)indexSuit) && _cards[i].Title.Equals((Title)indexTitle)))
                {
                    fourOfAKindCounter++;
                    var temp = _cards[i];
                    _cards[i] = _cards[positionCards];
                    _cards[positionCards] = temp;

                    indexSuit++;
                    positionCards++;

                    i = positionCards - 1;
                }
            }
        }

        public void PrintDeck()
        {
            Console.Write("Deck: ");
            foreach (var card in _cards)
                Console.Write($"{card.Suit}{card.Title.ToString()} ");
        }

        public List<string> ConvertDeckToStringList()
        {
            List<string> cardStrings = new List<string>();

            foreach (var card in _cards)
            {
                string cardString = $"{card.Suit}{card.Title.ToString()}";
                cardStrings.Add(cardString);
            }

            return cardStrings;
        }
    }
}
