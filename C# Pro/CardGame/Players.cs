using System.Text;

namespace CardGame
{
    /// <summary>
    /// The class responsible for saving player results during the game.
    /// </summary>
    internal class Players
    {
        public int sumPlayer { get; set; }
        public int sumComputer { get; set; }

        public List<string> cardsPlayer { get; set; }
        public List<string> cardsComputer { get; set; }

        public Players()
        {
            cardsPlayer = new List<string>();
            cardsComputer = new List<string>();
            cardsPlayer.Capacity = 6;
            cardsComputer.Capacity = 6;
        }

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
    }
}
