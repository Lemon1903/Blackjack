namespace Blackjack
{
    public class Hands
    {
        public List<string> Hand { get; set; } = new();
        public int TotalHandScore
        {
            get
            {
                List<string> aces = new();
                int total = 0, aceCount = 0;

                foreach (string card in Hand)
                {
                    if (card.StartsWith("A"))
                        aces.Add(card[..1]);
                    else if (card.StartsWith("J") || card.StartsWith("Q") || card.StartsWith("K") || card[..2] == "10")
                        total += 10;
                    else
                        total += int.Parse(card[..1]);
                }

                aceCount = aces.Count;

                while (aceCount > 0)
                {
                    total = total < 11 ? total + 11 : total + 1;
                    aceCount--;
                }

                return total;
            }
        }
        public int HandCount => Hand.Count;
        public bool IsBusted => TotalHandScore > 21;
        public bool IsBlackJack => TotalHandScore == 21;

        public Hands() { }

        public Hands (Deck deck)
        {
            Hand.Clear();

            for (int i = 0; i < 2; i++)
            {
                Hand.Add(deck.Cards.Dequeue());
            }
        }
    }
}
