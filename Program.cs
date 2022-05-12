using Blackjack;

Options:
TitleScreen.Options();
Console.Clear();

switch (TitleScreen.ChosenOption)
{
    case 1:
        Console.WriteLine("\n Welcome to Blackjack!\n");
        var noOfPlayers = Prompt.ForNumberOfPlayers();
        var game = new Game(noOfPlayers);
        game.PlayRound();
        break;
    case 2:
        Environment.Exit(0);
        break;
    case 3:
        Instructions();
        Console.ReadKey();
        Console.Clear();
        goto Options;
}

static void Instructions()
{
    Console.WriteLine("\n ---Nothing placed yet cause it's not yet finished with all the functionalities---");
}
