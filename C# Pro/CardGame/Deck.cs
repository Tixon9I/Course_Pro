namespace CardGame
{
    enum Title
    {
        Six = 6,
        Seven,
        Eight,
        Nine,
        Ten,
        J,
        Q,
        K,
        A
    }

    enum Suit
    {
        D,
        H,
        C,
        S
    }

    struct Card
    {
        public readonly Title _title;
        public readonly Suit _suit;

        public Card(Suit suit, Title title)
        {
            _title = title;
            _suit = suit;
        }
    }

    /// <summary>
    /// The class is designed to save, generate, shuffle, find and percentage a deck of cards.
    /// </summary>
    internal class Deck
    {
        public List<Card> _cards = new List<Card>();

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
                if (_cards[i]._title.Equals(Title.A))
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
                else if (_cards[i]._suit.Equals(Suit.S))
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
                if ((_cards[i]._suit.Equals((Suit)indexSuit) && _cards[i]._title.Equals((Title)indexTitle)))
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
            {
                string titleString;
                if ((int)card._title >= 6 && (int)card._title <= 10)
                {
                    titleString = ((int)card._title).ToString();
                }
                else
                {
                    titleString = card._title.ToString();
                }

                Console.Write($"{card._suit}{titleString} ");
            }
        }

        public List<string> ConvertDeckToStringList()
        {
            List<string> cardStrings = new List<string>();

            foreach (var card in _cards)
            {
                string titleString;
                if (card._title >= Title.Six && card._title <= Title.Ten)
                {
                    titleString = ((int)card._title).ToString();
                }
                else
                {
                    titleString = card._title.ToString();
                }

                string cardString = $"{card._suit}{titleString}";
                cardStrings.Add(cardString);
            }

            return cardStrings;
        }
    }
}
