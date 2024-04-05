using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
        public double PlayerBalance { get; private set; }

        public double PlayerBet {  get; set; }
        public Boolean HasBlackjack { get; set; }
        public Boolean IsBusted { get; set; }
        public Boolean HasAce { get; set; }


        public Player(string name, int StartBalance)
        {
            PlayerName = name;
            PlayerHand = new Hand();
            PlayerBalance = StartBalance;
            HasBlackjack = false;
            IsBusted = false;
            HasAce = false;
        }

        public void SetBet()
        {
            if (PlayerBalance <= 10)
            {
                PlayerBet = PlayerBalance;
            }
            else
            {
                PlayerBet = (PlayerBalance * 0.2);
            }
            PlayerBalance = PlayerBalance - PlayerBet;
        }

        public void AdjustPlayerBalance(double CurrentPlayerBet)
        {
            PlayerBalance = PlayerBalance + CurrentPlayerBet;
        }

        public void resetPlayer()
        {
            PlayerHand.Total = 0;
            PlayerHand.HandCards = new List<Card>();
            PlayerBet = 0;
            HasBlackjack = false;
            IsBusted = false;
            HasAce = false;
        }


        public void drawCard(Deck deck)
        {
            Console.WriteLine($"{PlayerName} drew a card.");
            Card drawnCard = deck.DrawCard();
            if (drawnCard.Rank.Name == "Ace")
            {
                HasAce = true;
            }
            PlayerHand.AddCard(drawnCard);
            Console.WriteLine();
            showHand();
        }

        public void showHand()
        {
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
            if (percentage < 0 ) {
                percentage = 0;
            }

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
