namespace Blackjack
{
    public class Game
    {
        // fields and properties
        private Deck deck;
        private Hand hand;
        private int dealerTotal;
        public static bool IsStand { get; private set; }

        // constructor
        public Game()
        {
            deck = new Deck();
            hand = new Hand(deck);
            dealerTotal = 0;
        }

        // main method for the Round
        public void PlayRound(Player player)
        {
            while (true)
            {
                EnterBet(player);
                PlayerTurn(player);
                DealerTurn();
                TallyResults(player);
                ResetRound();
            }
        }

        // method for entering user bet
        private static void EnterBet(Player player)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n Welcome to Blackjack Player!\n");
                player.Bet = Prompt.ForNumber("Enter your bet");

                if (player.Bet is > 0 and < 500)
                    break;

                StandardMessages.InvalidInput("Bet too big.");
            }

            Console.WriteLine();
        }

        // method for checking user move
        private void CheckMove(Player player)
        {
            if (player.Move.Equals("hit", StringComparison.OrdinalIgnoreCase))
                deck.DrawCard(hand, "player");
            else if (player.Move.Equals("stand", StringComparison.OrdinalIgnoreCase))
                IsStand = true;
        }

        // method for the player turn
        private void PlayerTurn(Player player)
        {
            while (true)
            {
                int start = Console.CursorTop;

                StandardMessages.MoveTurn("Player", hand);

                if (Hand.IsBlackjack(hand.PlayerTotalHandScore))
                {
                    StandardMessages.BlackJack("Player");
                    break;
                }
                else if (Hand.IsBusted(hand.PlayerTotalHandScore))
                {
                    StandardMessages.HandBust("Player");
                    break;
                }

                player.Move = Prompt.ForMove();
                CheckMove(player);

                if (IsStand) break;

                ClearLines.Clear(Console.CursorTop - start);
            }

            Console.WriteLine();
        }

        private void DealerTurn()
        {
            while (true)
            {
                int start = Console.CursorTop;

                StandardMessages.MoveTurn("Dealer", hand);

                if (hand.DealerTotalHandScore < 17)
                    deck.DrawCard(hand, "dealer");
                else if (hand.DealerTotalHandScore is >= 17 and < 21)
                    break;
                else if (Hand.IsBlackjack(hand.DealerTotalHandScore))
                {
                    StandardMessages.BlackJack("Dealer");
                    break;
                }
                else if (Hand.IsBusted(hand.DealerTotalHandScore))
                {
                    StandardMessages.HandBust("Dealer");
                    break;
                }

                StandardMessages.Loading("Dealer draws card");
                ClearLines.Clear(Console.CursorTop - start);
            }
        }

        private void TallyResults(Player player)
        {
            Console.WriteLine();

            if (!Hand.IsBusted(hand.PlayerTotalHandScore) && (Hand.IsBusted(hand.DealerTotalHandScore)
                || (!Hand.IsBusted(hand.DealerTotalHandScore) && hand.PlayerTotalHandScore > hand.DealerTotalHandScore))
                || (Hand.IsBlackjack(hand.PlayerTotalHandScore) && !Hand.IsBlackjack(hand.DealerTotalHandScore)))
            {
                player.TotalWinnings += Hand.IsBlackjack(hand.PlayerTotalHandScore) && !Hand.IsBlackjack(hand.DealerTotalHandScore) ? player.Bet * 2 : player.Bet;
                dealerTotal -= Hand.IsBlackjack(hand.PlayerTotalHandScore) && !Hand.IsBlackjack(hand.DealerTotalHandScore) ? player.Bet * 2 : player.Bet;
                StandardMessages.ShowRoundResult("Player won the round.", player.TotalWinnings, dealerTotal);
            }
            else if (!Hand.IsBusted(hand.DealerTotalHandScore) && (Hand.IsBusted(hand.PlayerTotalHandScore)
                || (!Hand.IsBusted(hand.PlayerTotalHandScore) && hand.PlayerTotalHandScore < hand.DealerTotalHandScore))
                || (!Hand.IsBlackjack(hand.PlayerTotalHandScore) && Hand.IsBlackjack(hand.DealerTotalHandScore))
                || (hand.IsBothBusted))
            {
                player.TotalWinnings -= player.Bet;
                dealerTotal += player.Bet;
                StandardMessages.ShowRoundResult("Dealer won the round.", player.TotalWinnings, dealerTotal);
            }
            else
            {
                StandardMessages.ShowRoundResult("It's a tie.", player.TotalWinnings, dealerTotal);
            }
        }

        private void ResetRound()
        {
            hand = new Hand(deck);
            IsStand = false;

            if (deck.Cards.Count < 10)
                deck = new Deck();

            Console.Write("\n Press any key to continue to next round.");
            Console.ReadKey();
        }
    }
}
