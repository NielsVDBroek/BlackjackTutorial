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
            Total += NewCard.Value;
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
        public Boolean HasAceToBeDecided { get; set; }
        public Card PlayerAce { get; set; }


        public Player(string name, int StartBalance)
        {
            PlayerName = name;
            PlayerHand = new Hand();
            PlayerBalance = StartBalance;
            HasBlackjack = false;
            IsBusted = false;
            HasAceToBeDecided = false;
            PlayerAce = null;
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
            PlayerBalance -= PlayerBet;
        }

        public void AdjustPlayerBalance(double CurrentPlayerBet)
        {
            PlayerBalance += CurrentPlayerBet;
        }

        public void ResetPlayer()
        {
            PlayerHand.Total = 0;
            PlayerHand.HandCards = new List<Card>();
            PlayerBet = 0;
            HasBlackjack = false;
            IsBusted = false;
            HasAceToBeDecided = false;
            PlayerAce = null;
        }


        public void DrawCard(Deck deck)
        {
            Console.WriteLine($"{PlayerName} drew a card.");
            Card drawnCard = deck.DrawCard();
            if (drawnCard.Rank.Name == "Ace" && PlayerHand.Total <= 10 && HasAceToBeDecided == false)
            {
                HasAceToBeDecided = true;
                Console.WriteLine("player has ace");
                PlayerAce = drawnCard;

            } else
            {
                PlayerHand.AddCard(drawnCard);
                if (HasAceToBeDecided && PlayerHand.Total > 10)
                {
                    PlayerHand.AddCard(PlayerAce);
                    HasAceToBeDecided = false;
                    PlayerAce = null;
                }
            }
            Console.WriteLine();
            ShowHand();
        }

        public void ShowHand()
        {
            Console.WriteLine($"{PlayerName} hand: ");
            foreach (Card card in PlayerHand.HandCards)
            {
                Console.WriteLine($"{card.Name} of {card.Suit} (Value: {card.Value})");
            }
            if (HasAceToBeDecided == true)
            {
                Console.WriteLine($"{PlayerAce.Name} of {PlayerAce.Suit} (Value: {PlayerAce.Value})");
                Console.WriteLine($"Hand total: {PlayerHand.Total + 1} | {PlayerHand.Total + 11}");
            }
            else
            {
                Console.WriteLine($"Hand total: {PlayerHand.Total}");
            }
        }

        public Boolean PlayerHitOrNot(int playerTotal)
        {
            Random random = new Random();
            int RandomPercentage = random.Next(0, 100);
            int percentage;
            if (HasAceToBeDecided)
            {
                percentage = (((playerTotal + 11) - 11) * 11);
            }
            else
            {
                percentage = ((playerTotal - 11) * 11);
            }
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

        public void PlayerStandsWithAce()
        {
            if (HasAceToBeDecided)
            {
                if (PlayerHand.Total <= 10)
                {
                    PlayerHand.AddCard(PlayerAce);
                    PlayerHand.Total += 10;
                }
                else
                {
                    PlayerHand.AddCard(PlayerAce);
                }
            }
        }
    }
}
