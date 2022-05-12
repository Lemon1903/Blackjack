namespace Blackjack
{
    public class TitleScreen
    {
        public static int ChosenOption { get; private set; }

        public static void Options()
        {
            Console.WriteLine("\n Welcome to Blackjack Game!\n");
            Console.WriteLine(" What do you want to do?");
            Console.WriteLine(" 1. Play");
            Console.WriteLine(" 2. Quit");
            Console.WriteLine(" 3. Instructions\n");
            ChosenOption = Prompt.ForOption();
        }
    }
}
