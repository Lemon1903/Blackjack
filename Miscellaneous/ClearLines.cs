namespace Blackjack
{
    public class ClearLines
    {
        public static void Clear(int position)
        {
            Console.SetCursorPosition(0, Console.CursorTop - position);
            for (int i = 0; i < position; i++)
                Console.WriteLine(new string(' ', Console.BufferWidth - 1));
            Console.SetCursorPosition(0, Console.CursorTop - position);
        }
    }
}
