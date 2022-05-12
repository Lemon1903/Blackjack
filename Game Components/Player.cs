namespace Blackjack
{
    public class Player
    {
        public int Bet { get; set; }
        public int TotalWinnings { get; set; }
        public int Name { get; set; }
        public string Move { get; set; } = string.Empty;
        public Hands Hand { get; set; } = new();
    }
}
