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
            Dealer dealer = new Dealer("Dealer", 1000);
            Deck deck = new Deck();

            int DealerStandAmount = 17;

            int TotalPlayers = 0;
            int MaxPlayers = 4;

            List<Player> players;
            players = new List<Player>();

            TotalPlayers = Dealer.askForTotalPlayers(MaxPlayers);

            Console.WriteLine($"Total players: {TotalPlayers}");
            string input = "";
            string NameInput = "";

            for (int i = 0; i < TotalPlayers; i++)
            {
                while (input != "yes" && input != "no")
                {
                    Console.WriteLine("Does Player" + (i + 1) + " want to enter a name?");
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
                        NameInput = ("Player" + (i + 1));
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
            foreach (Player player in players)
            {
                Console.WriteLine(player.PlayerName);
            }
            Console.WriteLine();

            Console.WriteLine("Start of game");

            Boolean PlayGame = true;
            while (PlayGame)
            {
                string DealerInput = "";

                while (DealerInput != "shuffle")
                {
                    Console.WriteLine("Dealer please enter action:");
                    DealerInput = Console.ReadLine().ToLower();

                    if (DealerInput != "shuffle")
                    {
                        Console.WriteLine("Incorrect action!");
                    }
                    else
                    {
                        Console.WriteLine("Correct action!");
                        deck = new Deck();
                        deck.Shuffle();
                        Console.WriteLine("Cards have been shuffled!");
                    }
                }

                DealerInput = "";

                while (DealerInput != "deal cards")
                {
                    Console.WriteLine("Dealer please enter action:");
                    DealerInput = Console.ReadLine().ToLower();

                    if (DealerInput != "deal cards")
                    {
                        Console.WriteLine("Incorrect action!");
                    }
                    else
                    {
                        Console.WriteLine("Correct action!");
                        var cards = deck.GetCards();

                        for (int i = 0; i < 2; i++)
                        {
                            foreach (Player player in players)
                            {
                                player.drawCard(deck);
                                Thread.Sleep(500);
                                Console.WriteLine();
                            }

                            dealer.drawCard(deck);
                            Console.WriteLine();
                            Console.WriteLine();
                        }
                    }
                }

                foreach (Player player in players)
                {
                    Boolean playerPlaying = true;
                    Boolean playerBusted = false;

                    while (playerPlaying)
                    {
                        Boolean DealerContinue = false;
                        string DealerInputHandlePlayer = "";

                        player.showHand();
                        Console.WriteLine();

                        while (!DealerContinue)
                        {
                            if (player.PlayerHand.Total == 21)
                            {
                                while (DealerInputHandlePlayer != "blackjack")
                                {
                                    Console.WriteLine("Dealer please enter action:");
                                    DealerInputHandlePlayer = Console.ReadLine().ToLower();

                                    if (DealerInputHandlePlayer != "blackjack")
                                    {
                                        Console.WriteLine("Incorrect action!");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Correct action");
                                        Console.WriteLine($"{player.PlayerName} has Blackjack!");
                                        playerPlaying = false;
                                        DealerContinue = true;
                                    }
                                }
                            }
                            else if (player.PlayerHand.Total > 21)
                            {
                                while (DealerInputHandlePlayer != "busted")
                                {
                                    Console.WriteLine("Dealer please enter action:");
                                    DealerInputHandlePlayer = Console.ReadLine().ToLower();

                                    if (DealerInputHandlePlayer != "busted")
                                    {
                                        Console.WriteLine("Incorrect action!");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"{player.PlayerName} is Busted!");
                                        playerPlaying = false;
                                        playerBusted = true;
                                        DealerContinue = true;
                                    }
                                }
                            }
                            else
                            {
                                DealerInputHandlePlayer = "";
                                while (DealerInputHandlePlayer != "ask player")
                                {
                                    Console.WriteLine("Dealer please enter action:");
                                    DealerInputHandlePlayer = Console.ReadLine().ToLower();

                                    if (DealerInputHandlePlayer != "ask player")
                                    {
                                        Console.WriteLine("Incorrect action!");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"{player.PlayerName} Hit or stand?");
                                        if (player.playerHitOrNot(player.PlayerHand.Total))
                                        {
                                            Console.WriteLine($"{player.PlayerName}: Hit");
                                            player.drawCard(deck);
                                        }
                                        else
                                        {
                                            Console.WriteLine($"{player.PlayerName}: Stand");

                                            DealerInput = "";

                                            while (DealerInput != "next player")
                                            {
                                                Console.WriteLine("Dealer please enter action:");
                                                DealerInput = Console.ReadLine().ToLower();

                                                if (DealerInput != "next player")
                                                {
                                                    Console.WriteLine("Incorrect action!");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Next player.");
                                                    Console.WriteLine();
                                                    playerPlaying = false;
                                                    DealerContinue = true;
                                                }

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            PlayGame = Dealer.askForAnotherGame();
            if (PlayGame)
            {
                foreach (Player player in players)
                {
                    player.resetPlayer();
                }
                
                dealer.resetPlayer();
            }
        }
    }
}