using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BlackjackTutorial
{
    //Hand class
    public class Hand
    {
        public List<Card> HandCards;
        public int Total { get; set; }

        public int PlayerBet { get; set; }
        public Boolean HasBlackjack { get; set; }
        public Boolean IsBusted { get; set; }
        public Boolean HasAceToBeDecided { get; set; }

        public Boolean HasDoubledDown { get; set; }

        public Card PlayerAce { get; set; }

        public Hand()
        {
            HandCards = new List<Card>();
            HasBlackjack = false;
            IsBusted = false;
            HasAceToBeDecided = false;
            PlayerAce = null;
            HasDoubledDown = false;
        }

        public void AddCard(Card newCard)
        {
            Total += newCard.Value;
            HandCards.Add(newCard);
        }
    }

    //Player class
    public class Player
    {
        public string PlayerName { get; private set; }
        public List<Hand> Hands { get; private set; }
        public Boolean HasSplit {  get; private set; }
        public int PlayerBalance { get; private set; }

        public Player(string name, int StartBalance)
        {
            PlayerName = name;
            Hands = new List<Hand> { new Hand() };
            PlayerBalance = StartBalance;
        }

        //Retrieves boolean if player can split their hand
        public Boolean CanSplit()
        {   if (Hands[0].PlayerBet <= PlayerBalance)
            {
                return Hands[0].HandCards.Count == 2 && Hands[0].HandCards[0].Rank == Hands[0].HandCards[1].Rank;
            }
            else
            {
                return false;
            }
        }

        //Determins if a player will split or not
        public Boolean PlayerSplitOrNot()
        {
            Random random = new Random();
            int RandomPercentage = random.Next(0, 100);
            int percentage = 20;

            if (RandomPercentage > percentage)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Allows the hand to be split
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

        //Player bet set
        public int SetBet()
        {
            if (PlayerBalance <= 10)
            {
                Hands[0].PlayerBet = PlayerBalance;
            }
            else
            {
                Hands[0].PlayerBet = (int)Math.Ceiling(PlayerBalance * 0.2);
            }
            PlayerBalance -= Hands[0].PlayerBet;
            return Hands[0].PlayerBet;
        }

        //Adjusts the players balance based on the bet
        public void AdjustPlayerBalance(int CurrentPlayerBet)
        {
            PlayerBalance += CurrentPlayerBet;
        }

        //Resets the player
        public void ResetPlayer()
        {
            Hands = new List<Hand> { new Hand() };
            HasSplit = false;
        }

        //Draws a card from deck and adds it to their hand
        public void DrawCard(Deck deck, int handIndex)
        {
            Console.WriteLine($"{PlayerName} drew a card.");
            Card drawnCard = deck.DrawCard();
            //Checks if the card is an ace
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

        //Shows the hand of the player
        public void ShowHand(int handIndex)
        {
            Console.WriteLine($"{PlayerName} hand {handIndex + 1}: ");
            foreach (Card card in Hands[handIndex].HandCards)
            {
                Console.WriteLine($"{card.Name} of {card.Suit} (Value: {card.Value})");
            }//Checks if the hand has an ace that hasn't been decided
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

        //Determins if the player will hit or not based on their hand value
        public Boolean PlayerHitOrNot(int playerTotal, int handIndex)
        {
            Random random = new Random();
            int RandomPercentage = random.Next(0, 100);
            int percentage;
            //If the players ace can be 11, that will be taken into acountability
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

        //Determins if a player will double down, and doubles their bet
        public Boolean DoubleDown(int playerTotal, int handIndex)
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
                if (playerTotal > 6)
                {
                    percentage = ((playerTotal - 11) * 11);
                }
                else
                {
                    return false;
                }
            }
            if (percentage < 0)
            {
                percentage = 0;
            }

            if (RandomPercentage > percentage)
            {
                Hands[handIndex].HasDoubledDown = true;
                PlayerBalance -= Hands[handIndex].PlayerBet;
                Hands[handIndex].PlayerBet = (Hands[handIndex].PlayerBet * 2);
                return true;
            }
            else
            {
                return false;
            }
        }

        //Determins what happens with the ace when player stands
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
