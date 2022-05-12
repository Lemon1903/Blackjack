namespace Blackjack
{
    public class DisplayMessages
    {
        public static void InvalidInput(string message)
        {
            Console.WriteLine($" {message} Try again.");
            Console.ReadKey();
        }

        public static void ShowRoundResult(string message, Player player)
        {
            Console.WriteLine($" {message}");
            Console.WriteLine($" [Player {player.Name}] total earnings: {player.TotalWinnings}\n");
        }

        public static void BlackJack(string who)
        {
            Console.WriteLine($" {who} hand is a Blakcjack.");
            Console.ReadKey();
        }

        public static void HandBust(string who)
        {
            Console.WriteLine($" {who} hand is a bust.");
            Console.ReadKey();
        }

        public static void MoveTurn(string who, Player[] players, Hands dealerHand, bool isDealerTurn)
        {
            int size = GetMaxSize(players, dealerHand);
            int n = 8 * players.Length + 7; 

            Console.WriteLine($" {who} turn:");
            Console.WriteLine(new string('-', n));
            foreach (var player in players)
            {
                Console.Write($" #{player.Name,-4} |");
            }
            Console.WriteLine(" Dealer");
            Console.WriteLine(new string('-', n));

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < players.Length; j++)
                    Console.Write($" {(i < players[j].Hand.HandCount ? players[j].Hand.Hand[i] : " "),-5} |");

                if (!isDealerTurn && i == 1)
                    Console.WriteLine($" {(i < dealerHand.HandCount ? "XX" : " "),-5}");
                else
                    Console.WriteLine($" {(i < dealerHand.HandCount ? dealerHand.Hand[i] : " "),-5}");
            }
        }

        public static void Loading(string text)
        {
            for (int i = 0; i < 4; i++)
            {
                Console.Write($" {text}\r");
                text += ".";
                Thread.Sleep(500);
            }

            Console.Write($"{new string(' ', Console.BufferWidth)}\r");
        }

        private static int GetMaxSize(Player[] players, Hands dealerHand)
        {
            List<int> counts = new() { dealerHand.HandCount };

            foreach (var player in players)
            {
                counts.Add(player.Hand.Hand.Count);
            }

            return counts.Max();
        }
    }
}
