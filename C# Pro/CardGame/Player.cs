using System.Text;

namespace CardGame
{
    /// <summary>
    /// The class responsible for saving player results during the game.
    /// </summary>
    internal class Player
    {
        public int SumPlayer { get; set; }
        public List<string> CardsPlayer { get; }
        
        public Player()
        {
            CardsPlayer = new List<string>();
            CardsPlayer.Capacity = 6;
        }

        public void Clear()
        {
            CardsPlayer.Clear();
        }

        public void PrintInfo()
        {
            var builder = new StringBuilder();

            foreach (var card in CardsPlayer)
                builder.Append(" " + card.ToString());

            Console.WriteLine($"Cards:{builder.ToString()}. Sum = {SumPlayer}");
        }

        public void GetCardValue(List<string> cards)
        {
            SumPlayer = 0;

            foreach (var card in cards)
            {
                var cardTitle = card.Substring(1);

                if (Enum.TryParse(cardTitle, true, out CardValue cardValue))
                    SumPlayer += (int)cardValue;
                
            }
        }
    }
}
