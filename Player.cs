using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BlackjackTutorial
{
    public class Hand
    {
        public List<Card> HandCards;
        public int Total { get; set; }

        public Hand()
        {
            HandCards = new List<Card>();
        }

        public void AddCard(Card NewCard)
        {
            Total = Total + NewCard.Value;
            HandCards.Add(NewCard);
        }
    }

    public class Player
    {
        public string PlayerName { get; private set; }
        public Hand PlayerHand { get; private set; }
        public int PlayerBalance { get; private set; }

        public Player(string name, int StartBalance)
        {
            PlayerName = name;
            PlayerHand = new Hand();
            PlayerBalance = StartBalance;
        }

        public void resetPlayer()
        {
            PlayerHand.Total = 0;
            PlayerHand.HandCards = new List<Card>();
        }


        public void drawCard(Deck deck)
        {
            Console.WriteLine($"{PlayerName} drew a card.");
            Card drawnCard = deck.DrawCard();
            PlayerHand.AddCard(drawnCard);
            Console.WriteLine();
            Console.WriteLine($"{PlayerName} hand: ");
            foreach (Card card in PlayerHand.HandCards)
            {
                Console.WriteLine($"{card.Name} of {card.Suit} (Value: {card.Value})");
            }
            Console.WriteLine($"Hand total: {PlayerHand.Total}");
        }

        public Boolean playerHitOrNot(int playerTotal)
        {
            Random random = new Random();
            int RandomPercentage = random.Next(0, 100);
            int percentage = ((playerTotal - 11) * 10);

            if (RandomPercentage > percentage)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
