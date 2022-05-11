using Blackjack;

TitleScreen.Options();
Console.Clear();

switch (TitleScreen.ChosenOption)
{
    case 1:
        Player player = new Player();
        Game game = new();
        game.PlayRound(player);
        break;
    case 2:
        Environment.Exit(0);
        break;
    case 3:
        break;
}

