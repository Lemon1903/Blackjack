namespace Blackjack
{
    public class Hand
    {
        public List<string> PlayerHand { get; set; } = new();
        public List<string> DealerHand { get; set; } = new();
        public int PlayerTotalHandScore => GetTotal(PlayerHand);
        public int DealerTotalHandScore => GetTotal(DealerHand);
        public int Count => Math.Max(PlayerHand.Count, DealerHand.Count);
        public bool IsBothBusted => PlayerTotalHandScore > 21 && DealerTotalHandScore > 21;

        public Hand (Deck deck)
        {
            PlayerHand.Clear();
            DealerHand.Clear();

            for (int i = 0; i < 2; i++)
            {
                PlayerHand.Add(deck.Cards.Dequeue());
                DealerHand.Add(deck.Cards.Dequeue());
            }
        }

        public void ShowHands(int handSize)
        {
            Console.WriteLine($" {new string('-', 20)}");
            Console.WriteLine($" {"Player",-10} | {"Dealer",-10}");
            Console.WriteLine($" {new string('-', 20)}");

            for (int i = 0; i < handSize; i++)
            {
                Console.Write($" {(i < PlayerHand.Count ? PlayerHand[i] : " "),-10}");
                Console.Write(" | ");

                if (!Game.IsStand && !IsBusted(PlayerTotalHandScore) && !IsBlackjack(PlayerTotalHandScore) && i == 1)
                    Console.Write($" {(i < DealerHand.Count ? "XX" : " "),-10}\n");
                else
                    Console.Write($" {(i < DealerHand.Count ? DealerHand[i] : " "),-10}\n");
            }

            Console.WriteLine();
        }

        public static bool IsBusted(int score)
        {
            return score > 21;
        }

        public static bool IsBlackjack(int score)
        {
            return score == 21;
        }

        private static int GetTotal(List<string> hand)
        {
            List<string> aces = new();
            int total = 0, aceCount = 0;

            foreach (string card in hand)
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
}
