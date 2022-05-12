namespace Blackjack
{
    public class Deck
    {
        private readonly List<string> cardList = new()
        {
            "A♠", "2♠", "3♠", "4♠", "5♠", "6♠", "7♠", "8♠", "9♠", "10♠", "J♠", "Q♠", "K♠",
            "A♥", "2♥", "3♥", "4♥", "5♥", "6♥", "7♥", "8♥", "9♥", "10♥", "J♥", "Q♥", "K♥",
            "A♦", "2♦", "3♦", "4♦", "5♦", "6♦", "7♦", "8♦", "9♦", "10♦", "J♦", "Q♦", "K♦",
            "A♣", "2♣", "3♣", "4♣", "5♣", "6♣", "7♣", "8♣", "9♣", "10♣", "J♣", "Q♣", "K♣"
        };
        private readonly Random random = new();
        public Queue<string> Cards { get; } = new();

        public Deck()
        {
            var cardListCopy = cardList;

            Cards.Clear();
            while (cardListCopy.Count > 0)
            {
                int index = random.Next(cardListCopy.Count);
                Cards.Enqueue(cardListCopy[index]);
                cardListCopy.Remove(cardListCopy[index]);
            }
        }

        public void DrawCard(Hands hand)
        {
            hand.Hand.Add(Cards.Dequeue());
        }
    }
}
