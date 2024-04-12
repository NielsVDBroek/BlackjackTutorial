using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackTutorial
{
    public class Dealer : Player
    {
        public Dealer(string name, int startBalance) : base(name, startBalance)
        {
            string dealerName = name;
            int dealerBalance = startBalance;
        }

        Card hiddenCard;

        public void DrawCardHidden(Deck deck)
        {
            Console.WriteLine("Dealer drew a card.");
            hiddenCard = deck.DrawCardHidden();
            Console.WriteLine($"Dealers card is hidden. Hand total: {PlayerHand.Total}");
        }

        public void RevealHiddenCard()
        {
            Console.WriteLine($"Dealer reveals hidden card: {hiddenCard.Name} of {hiddenCard.Suit} (Value: {hiddenCard.Value}");
            //Check if ace
            PlayerHand.AddCard(hiddenCard);
            Console.WriteLine();
            ShowHand();
        }

        public void DealCards(Player player)
        {
            //Code voor dealen hier.
            
        }

        public static Boolean AskForAnotherGame()
        {
            string rematchInput = "";
            bool anotherMatch = false;
            Console.WriteLine("Rematch?");
            while (rematchInput != "yes" && rematchInput != "no")
            {
                Console.WriteLine("Yes or No");
                rematchInput = Console.ReadLine().ToLower();
                if (rematchInput == "no")
                {
                    Console.WriteLine("Game Ended");
                    anotherMatch = false;
                }
                else if (rematchInput == "yes")
                {
                    anotherMatch = true;
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }
            return anotherMatch;

        }

        public static int AskForTotalPlayers(int MaxPlayersInput)
        {
            string TotalPlayersInputString = "";
            int TotalPlayersInput = 0;
            do
            {
                Console.WriteLine("How many players are playing? Minimum of 1, Maximum of 4 players:");
                TotalPlayersInputString = Console.ReadLine();

                try
                {
                    TotalPlayersInput = Convert.ToInt32(TotalPlayersInputString);

                    if (TotalPlayersInput < 1 || TotalPlayersInput > MaxPlayersInput)
                    {
                        Console.WriteLine("Please enter a number between 1 and 4.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("That is not a valid number.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Please enter a number between 1 and 4.");
                }

                Console.WriteLine();

            } while (TotalPlayersInput < 1 || TotalPlayersInput > MaxPlayersInput);

            return TotalPlayersInput;
        }
    }
}
