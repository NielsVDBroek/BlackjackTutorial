using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackTutorial
{
    //Dealer inherits player
    public class Dealer : Player
    {
        public Dealer(string name, int startBalance) : base(name, startBalance)
        {
            string dealerName = name;
            int dealerBalance = startBalance;
        }
        
        //The dealers hidden card
        Card hiddenCard;

        //Draws the dealers hidden card
        public void DrawCardHidden(Deck deck)
        {
            Console.WriteLine("Dealer drew a card.");
            hiddenCard = deck.DrawCardHidden();
            Console.WriteLine($"Dealers card is hidden. Hand total: {Hands[0].Total}");
        }

        //Reveals the dealers hidden card
        public void RevealHiddenCard()
        {
            Console.WriteLine($"Dealer reveals hidden card: {hiddenCard.Name} of {hiddenCard.Suit} (Value: {hiddenCard.Value}");
            Hands[0].AddCard(hiddenCard);
            Console.WriteLine();
            ShowHand(0);
        }


        //Shuffles the DealerActions array
        public void ShuffleArray(string[] array)
        {
            Random random = new Random();
            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1); // Random index from 0 to i
                string temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
        }

        //Asks the dealer for another game
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
        
        //Asks the dealer for the amount of decks to be used
        public static int AskForTotalDecks(int MaxDecksInput)
        {
            string TotalDecksInputString = "";
            int TotalDecksInput = 0;
            do
            {
                Console.WriteLine($"How many decks would be used? Minimum of 1, Maximum of {MaxDecksInput} decks:");
                TotalDecksInputString = Console.ReadLine();

                try
                {
                    TotalDecksInput = Convert.ToInt32(TotalDecksInputString);

                    if (TotalDecksInput < 1 || TotalDecksInput > MaxDecksInput)
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

            } while (TotalDecksInput < 1 || TotalDecksInput > MaxDecksInput);

            return TotalDecksInput;
        }

        //Asks the dealer how many players are playing  
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
