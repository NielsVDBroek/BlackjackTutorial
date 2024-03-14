using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Numerics;

namespace BlackjackTutorial
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Deck deck = new Deck();
            Hand hand = new Hand();
            deck.Shuffle();

            var cards = deck.GetCards();

            Console.WriteLine("Here is the deck:");
            foreach (var card in cards)
            {
                Console.WriteLine($"{card.Name} of {card.Suit} (Value: {card.Value})");

            }

            Console.WriteLine("Start of game");

            Boolean GameEnd = false;
            while (!GameEnd)
            {
                int TotalPlayers = 0;
                int MaxPlayers = 4;

                List<Player> players;
                players = new List<Player>();

                do
                {
                    Console.WriteLine("How many players are playing? Minimum of 1, Maximum of 4 players:");
                    string TotalPlayersInput = Console.ReadLine();

                    try
                    {
                        TotalPlayers = Convert.ToInt32(TotalPlayersInput);

                        if (TotalPlayers < 1 || TotalPlayers > MaxPlayers)
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

                } while (TotalPlayers < 1 || TotalPlayers > MaxPlayers);



                Console.WriteLine($"Total players: {TotalPlayers}");
                string input = "";
                string NameInput = "";

                for (int i = 0; i < TotalPlayers; i++)
                {
                    while (input != "yes" && input != "no")
                    {
                        Console.WriteLine("Does Player" + (i+1) + " want to enter a name?");
                        Console.WriteLine("Yes or No");
                        input = Console.ReadLine().ToLower();
                        if (input == "yes")
                        {
                            Console.WriteLine();
                            Console.WriteLine("Enter name:");
                            NameInput = Console.ReadLine();
                        }
                        else if (input == "no")
                        {
                            NameInput = ("Player" + (i+1));
                        }
                        else
                        {
                            Console.WriteLine("Invalid input");
                        }
                        Thread.Sleep(500);
                        Console.WriteLine();
                    }

                    players.Add(new Player(NameInput, 100));
                    Console.WriteLine(NameInput + " " + "added");

                    Console.WriteLine();
                    input = "";
                    NameInput = "";
                }

                Console.WriteLine("Here are all the players");
                foreach (Player player in players) {
                    Console.WriteLine(player.PlayerName);
                }

                for (int i = 0; i < 2; i++)
                {
                    foreach (Player player in players)
                    {
                        if (player != null)
                        {
                            player.drawCard(deck);
                        }
                        else
                        {
                            Console.WriteLine("Player1 not found.");
                        }
                        Thread.Sleep(500);
                        Console.WriteLine();
                    }
                }

                string rematchInput = "";
                Console.WriteLine("Rematch?");
                while (rematchInput != "yes" && rematchInput != "no")
                {
                    Console.WriteLine("Yes or No");
                    rematchInput = Console.ReadLine().ToLower();
                    if (rematchInput == "no")
                    {
                        Console.WriteLine("Game Ended");
                        GameEnd = true;
                    }
                    else if (rematchInput == "yes")
                    {

                    }
                    else
                    {
                        Console.WriteLine("Invalid input");
                    }
                }
            }
        }
    }
}