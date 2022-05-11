namespace Blackjack
{
    public class Prompt
    {
        public static int ForNumber(string prompt)
        {
            Console.Write($" {prompt}: ");

            if (int.TryParse(Console.ReadLine(), out int option))
                return option;

            StandardMessages.InvalidInput("Invalid Input!");
            return 0;
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

                StandardMessages.InvalidInput("Invalid Input");
                int end = Console.CursorTop;
                ClearLines.Clear(end - start);
            }
        }
    }
}
