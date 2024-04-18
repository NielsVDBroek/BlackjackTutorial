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

        public double PlayerBet { get; set; }
        public Boolean HasBlackjack { get; set; }
        public Boolean IsBusted { get; set; }
        public Boolean HasAceToBeDecided { get; set; }

        public Card PlayerAce { get; set; }

        public Hand()
        {
            HandCards = new List<Card>();
            HasBlackjack = false;
            IsBusted = false;
            HasAceToBeDecided = false;
            PlayerAce = null;
        }

        public void AddCard(Card newCard)
        {
            Total += newCard.Value;
            HandCards.Add(newCard);
        }
    }

    public class Player
    {
        public string PlayerName { get; private set; }
        public List<Hand> Hands { get; private set; }
        public Boolean HasSplit {  get; private set; }
        public double PlayerBalance { get; private set; }

        public Player(string name, int StartBalance)
        {
            PlayerName = name;
            Hands = new List<Hand> { new Hand() };
            PlayerBalance = StartBalance;
        }
        public bool CanSplit()
        {   if (Hands[0].PlayerBet <= PlayerBalance)
            {
                return Hands[0].HandCards.Count == 2 && Hands[0].HandCards[0].Rank == Hands[0].HandCards[1].Rank;
            }
            else
            {
                return false;
            }
        }

        public void Split()
        {
            if (!CanSplit()) return;

            var hand = Hands[0];
            var newHand = new Hand();
            var cardToMove = hand.HandCards[1];
            hand.HandCards.RemoveAt(1);
            newHand.AddCard(cardToMove);
            hand.Total -= cardToMove.Value;
            Hands.Add(newHand);

            PlayerBalance -= Hands[0].PlayerBet;
            Console.WriteLine($"{PlayerName} has Split, and bets {Hands[0].PlayerBet} for the new hand.");
            HasSplit = true;
        }

        public void SetBet()
        {
            if (PlayerBalance <= 10)
            {
                Hands[0].PlayerBet = PlayerBalance;
            }
            else
            {
                Hands[0].PlayerBet = (PlayerBalance * 0.2);
            }
            PlayerBalance -= Hands[0].PlayerBet;
        }

        public void AdjustPlayerBalance(double CurrentPlayerBet)
        {
            PlayerBalance += CurrentPlayerBet;
        }

        public void ResetPlayer()
        {
            Hands = new List<Hand> { new Hand() };
            Hands[0].PlayerBet = 0;
            Hands[0].HasBlackjack = false;
            Hands[0].IsBusted = false;
            Hands[0].HasAceToBeDecided = false;
            HasSplit = false;
            Hands[0].PlayerAce = null;
        }


        public void DrawCard(Deck deck, int handIndex)
        {
            Console.WriteLine($"{PlayerName} drew a card.");
            Card drawnCard = deck.DrawCard();
            if (drawnCard.Rank.Name == "Ace" && Hands[handIndex].Total <= 10 && Hands[handIndex].HasAceToBeDecided == false)
            {
                Hands[handIndex].HasAceToBeDecided = true;
                Console.WriteLine("player has ace");
                Hands[handIndex].PlayerAce = drawnCard;

            } else
            {
                Hands[handIndex].AddCard(drawnCard);
                if (Hands[handIndex].HasAceToBeDecided && Hands[handIndex].Total > 10)
                {
                    Hands[handIndex].AddCard(Hands[handIndex].PlayerAce);
                    Hands[handIndex].HasAceToBeDecided = false;
                    Hands[handIndex].PlayerAce = null;
                }
            }
            Console.WriteLine();
            ShowHand(handIndex);
        }

        public void ShowHand(int handIndex)
        {
            Console.WriteLine($"{PlayerName} hand {handIndex}: ");
            foreach (Card card in Hands[handIndex].HandCards)
            {
                Console.WriteLine($"{card.Name} of {card.Suit} (Value: {card.Value})");
            }
            if (Hands[handIndex].HasAceToBeDecided == true)
            {
                Console.WriteLine($"{Hands[handIndex].PlayerAce.Name} of {Hands[handIndex].PlayerAce.Suit} (Value: {Hands[handIndex].PlayerAce.Value})");
                Console.WriteLine($"Hand total: {Hands[handIndex].Total + 1} | {Hands[handIndex].Total + 11}");
            }
            else
            {
                Console.WriteLine($"Hand total: {Hands[handIndex].Total}");
            }
        }

        public Boolean PlayerHitOrNot(int playerTotal, int handIndex)
        {
            Random random = new Random();
            int RandomPercentage = random.Next(0, 100);
            int percentage;
            if (Hands[handIndex].HasAceToBeDecided)
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

        public void PlayerStandsWithAce(int handIndex)
        {
            if (Hands[handIndex].HasAceToBeDecided)
            {
                if (Hands[handIndex].Total <= 10)
                {
                    Hands[handIndex].AddCard(Hands[handIndex].PlayerAce);
                    Hands[handIndex].Total += 10;
                }
                else
                {
                    Hands[handIndex].AddCard(Hands[handIndex].PlayerAce);
                }
            }
        }
    }
}
