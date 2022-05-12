namespace Blackjack
{
    public class Prompt
    {
        public static int ForOption()
        {
            while (true)
            {
                int start = Console.CursorTop;

                Console.Write(" Enter your option: ");
                var input = Console.ReadLine();

                if (int.TryParse(input, out int option) && option is >= 1 and <= 3)
                    return option;

                DisplayMessages.InvalidInput("Invalid option!");
                ClearLines.Clear(Console.CursorTop - start);
            }
        }

        public static int ForPlayerBet(int playerNumber)
        {
            while (true)
            {
                int start = Console.CursorTop;

                Console.Write($" Enter your bet [Player {playerNumber}]: ");
                var input = Console.ReadLine();

                if (!int.TryParse(input, out int bet))
                    DisplayMessages.InvalidInput("Invalid Bet!");
                else if (bet is < 1 or > 500)
                    DisplayMessages.InvalidInput("Bet too big!");
                else
                    return bet;

                ClearLines.Clear(Console.CursorTop - start);
            }
        }

        public static int ForNumberOfPlayers()
        {
            while (true)
            {
                int start = Console.CursorTop;

                Console.Write(" Enter number of players (1-7): ");
                var input = Console.ReadLine();

                if (int.TryParse(input, out int players) && players is >= 1 and <= 7)
                {
                    ClearLines.Clear(Console.CursorTop - start);
                    return players;
                }

                DisplayMessages.InvalidInput("Invalid number of players!");
                ClearLines.Clear(Console.CursorTop - start);
            }
        }

        public static string ForMove()
        {
            while (true)
            {
                int start = Console.CursorTop;
                Console.Write(" Enter your move: ");
                var input = Console.ReadLine();

                if (input.Equals("hit", StringComparison.InvariantCultureIgnoreCase))
                    return input;
                else if (input.Equals("stand", StringComparison.InvariantCultureIgnoreCase))
                    return input;

                DisplayMessages.InvalidInput("Invalid Input");
                int end = Console.CursorTop;
                ClearLines.Clear(end - start);
            }
        }
    }
}
