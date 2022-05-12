namespace Blackjack
{
    public class Game
    {
        // fields and properties
        private readonly Player[] _players;
        private Deck _deck;
        private Hands _dealerHand;
        private int _dealerTotal;
        private bool _isStand, _isDealerTurn;

        // constructor
        public Game(int noOfPlayers)
        {
            _players = new Player[noOfPlayers];
            _deck = new Deck();
            _dealerHand = new Hands(_deck);

            for (int i = 0; i < noOfPlayers; i++)
            {
                _players[i] = new();
                _players[i].Name = i + 1;
                _players[i].Hand = new Hands(_deck);
            }
        }

        // methods
        public void PlayRound()
        {
            while (true)
            {
                int start = Console.CursorTop;

                foreach (var player in _players)
                    EnterBet(player);

                foreach (var player in _players)
                    PlayerTurn(player);

                DealerTurn();
                TallyResults(_players);
                ResetRound();

                ClearLines.Clear(Console.CursorTop - start);
            }
        }

        // needs to be fixed
        private static void EnterBet(Player player)
        {
            player.Bet = Prompt.ForPlayerBet(player.Name);
        }

        private void CheckMove(Player player)
        {
            if (player.Move.Equals("hit", StringComparison.OrdinalIgnoreCase))
                _deck.DrawCard(player.Hand, "player");
            else if (player.Move.Equals("stand", StringComparison.OrdinalIgnoreCase))
                _isStand = true;
        }

        private void PlayerTurn(Player player)
        {
            _isStand = false;
            Console.WriteLine();

            while (true)
            {
                int start = Console.CursorTop;

                // visuals needs to be fixed
                DisplayMessages.MoveTurn($"Player {player.Name}", _players, _dealerHand, _isDealerTurn);
                Console.WriteLine($"\n Player hand total score: {player.Hand.TotalHandScore}");

                if (player.Hand.IsBlackJack)
                {
                    DisplayMessages.BlackJack("Player");
                    break;
                }
                else if (player.Hand.IsBusted)
                {
                    DisplayMessages.HandBust("Player");
                    break;
                }

                player.Move = Prompt.ForMove();
                CheckMove(player);

                if (_isStand) break;

                ClearLines.Clear(Console.CursorTop + 1 - start);
            }
        }

        private void DealerTurn()
        {
            _isDealerTurn = true;
            Console.WriteLine();

            while (true)
            {
                int start = Console.CursorTop;

                DisplayMessages.MoveTurn("Dealer", _players, _dealerHand, _isDealerTurn);
                Console.WriteLine($"\n Dealer hand total score: {_dealerHand.TotalHandScore}");

                if (_dealerHand.TotalHandScore < 17)
                    _deck.DrawCard(_dealerHand, "dealer");
                else if (_dealerHand.TotalHandScore is >= 17 and < 21)
                    break;
                else if (_dealerHand.IsBlackJack)
                {
                    DisplayMessages.BlackJack("Dealer");
                    break;
                }
                else if (_dealerHand.IsBusted)
                {
                    DisplayMessages.HandBust("Dealer");
                    break;
                }

                DisplayMessages.Loading("Dealer draws card");
                ClearLines.Clear(Console.CursorTop - start);
            }
        }

        private void TallyResults(Player[] players)
        {
            Console.WriteLine("\n Results:");

            foreach (var player in players)
            {
                bool isBothBusted = player.Hand.TotalHandScore > 21 && _dealerHand.TotalHandScore > 21;
                bool isPlayerOnlyBlackjack = player.Hand.IsBlackJack && !_dealerHand.IsBlackJack;

                if (!player.Hand.IsBusted && (_dealerHand.IsBusted || (!_dealerHand.IsBusted && player.Hand.TotalHandScore > _dealerHand.TotalHandScore))
                    || isPlayerOnlyBlackjack)
                {
                    player.TotalWinnings += isPlayerOnlyBlackjack ? player.Bet * 2 : player.Bet;
                    _dealerTotal -= isPlayerOnlyBlackjack ? player.Bet * 2 : player.Bet;
                    DisplayMessages.ShowRoundResult($"[Player {player.Name}] won the round.", player);
                }
                else if (!_dealerHand.IsBusted && (player.Hand.IsBusted || (!player.Hand.IsBusted && player.Hand.TotalHandScore < _dealerHand.TotalHandScore))
                    || (!player.Hand.IsBlackJack && _dealerHand.IsBlackJack) || (isBothBusted))
                {
                    player.TotalWinnings -= player.Bet;
                    _dealerTotal += player.Bet;
                    DisplayMessages.ShowRoundResult("Dealer won the round.", player);
                }
                else
                {
                    DisplayMessages.ShowRoundResult($"[Player {player.Name}] pushes", player);
                }
            }

            Console.WriteLine($" Dealer total earnings: {_dealerTotal}\n");
        }

        private void ResetRound()
        {
            _dealerHand = new Hands(_deck);
            _isStand = _isDealerTurn = false;

            if (_deck.Cards.Count < 10)
                _deck = new Deck();

            for (int i = 0; i < _players.Length; i++)
                _players[i].Hand = new Hands(_deck);

            Console.Write(" Press any key to continue to next round.");
            Console.ReadKey();
        }
    }
}
