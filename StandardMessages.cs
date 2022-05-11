namespace Blackjack
{
    public class StandardMessages
    {
        public static void InvalidInput(string message)
        {
            Console.WriteLine($" {message} Try again.");
            Console.ReadKey();
        }

        public static void ShowRoundResult(string message, int playerScore, int dealerScore)
        {
            Console.WriteLine($" {message}");
            Console.WriteLine($" Player total earnings: {playerScore}");
            Console.WriteLine($" Dealer total earnings: {dealerScore}");
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

        public static void MoveTurn(string who, Hand hand)
        {
            Console.WriteLine($" {who} turn");
            hand.ShowHands(hand.Count);
            Console.WriteLine($" {who} hand total score: {(who.Equals("Player") ? hand.PlayerTotalHandScore : hand.DealerTotalHandScore)}");
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
    }
}
