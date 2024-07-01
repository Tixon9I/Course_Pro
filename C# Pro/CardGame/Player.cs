using System.Text;

namespace CardGame
{
    /// <summary>
    /// The class responsible for saving player results during the game.
    /// </summary>
    internal class Player
    {
        public int _sumPlayer { get; set; }
        public List<string> _cardsPlayer { get; set; }
        
        public Player()
        {
            _cardsPlayer = new List<string>();
            _cardsPlayer.Capacity = 6;
            
        }

        public void PrintInfo()
        {
            var builder = new StringBuilder();

            foreach (var card in _cardsPlayer)
                builder.Append(" " + card.ToString());

            Console.WriteLine($"Cards:{builder.ToString()}. Sum = {_sumPlayer}");
        }
    }
}
