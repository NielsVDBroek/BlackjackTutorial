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

                //Player bets

                foreach (Player player in players)
                {
                    DealerInput = "";
                    while (DealerInput != "ask bet")
                    {
                        Console.WriteLine("Dealer please enter action:");
                        DealerInput = Console.ReadLine().ToLower();

                        if (DealerInput != "ask bet")
                        {
                            Console.WriteLine("Incorrect action!");
                            Thread.Sleep(500);
                        }
                        else
                        {
                            Console.WriteLine("Correct action!");
                            Thread.Sleep(500);
                            Console.WriteLine();
                            Console.WriteLine($"{player.PlayerName} how much would you like to bet?");
                            player.SetBet();
                            dealer.AdjustPlayerBalance(player.PlayerBet);
                            Console.WriteLine($"{player.PlayerName} bets {player.PlayerBet}");
                            Console.WriteLine($"{player.PlayerName} balance: {player.PlayerBalance}");
                            Console.WriteLine();
                        }
                    }
                }

                DealerInput = "";
                while (DealerInput != "shuffle")
                {
                    Console.WriteLine("Dealer please enter action:");
                    DealerInput = Console.ReadLine().ToLower();

                    if (DealerInput != "shuffle")
                    {
                        Console.WriteLine("Incorrect action!");
                        Thread.Sleep(500);
                    }
                    else
                    {
                        Console.WriteLine("Correct action!");
                        Thread.Sleep(500);
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
                        Thread.Sleep(500);
                    }
                    else
                    {
                        Console.WriteLine("Correct action!");
                        Thread.Sleep(500);
                        var cards = deck.GetCards();

                        for (int cardsDealt = 0; cardsDealt < 2; cardsDealt++)
                        {
                            foreach (Player player in players)
                            {
                                player.drawCard(deck);
                                Thread.Sleep(500);
                                Console.WriteLine();
                            }
                            if (cardsDealt == 0)
                            {
                                dealer.drawCard(deck);
                                Console.WriteLine();
                                Console.WriteLine();
                            }
                            else
                            {
                                dealer.drawCardHidden(deck);
                                Console.WriteLine();
                                Console.WriteLine();
                            }
                        }
                    }
                }

                foreach (Player player in players)
                {
                    Boolean playerPlaying = true;

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
                                        Thread.Sleep(500);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Correct action");
                                        Thread.Sleep(500);
                                        Console.WriteLine($"{player.PlayerName} has Blackjack!");
                                        Console.WriteLine();
                                        player.HasBlackjack = true;
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
                                        Thread.Sleep(500);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Correct action");
                                        Thread.Sleep(500);
                                        Console.WriteLine($"{player.PlayerName} is Busted!");
                                        Console.WriteLine();
                                        player.IsBusted = true;
                                        playerPlaying = false;
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
                                        Thread.Sleep(500);
                                    }
                                    else
                                    {
                                        Console.WriteLine($"{player.PlayerName} Hit or stand?");
                                        if (player.playerHitOrNot(player.PlayerHand.Total))
                                        {
                                            Console.WriteLine($"{player.PlayerName}: Hit");

                                            DealerInput = "";
                                            while (DealerInput != "give card")
                                            {
                                                Console.WriteLine("Dealer please enter action:");
                                                DealerInput = Console.ReadLine().ToLower();

                                                if (DealerInput != "give card")
                                                {
                                                    Console.WriteLine("Incorrect action!");
                                                    Thread.Sleep(500);
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Correct action");
                                                    Thread.Sleep(500);
                                                    Console.WriteLine();
                                                    player.drawCard(deck);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine($"{player.PlayerName}: Stand");

                                            DealerInput = "";

                                            while (DealerInput != "end turn")
                                            {
                                                Console.WriteLine("Dealer please enter action:");
                                                DealerInput = Console.ReadLine().ToLower();

                                                if (DealerInput != "end turn")
                                                {
                                                    Console.WriteLine("Incorrect action!");
                                                    Thread.Sleep(500);
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Correct action");
                                                    Thread.Sleep(500);
                                                    Console.WriteLine("Turn ended.");
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
                dealer.showHand();
                dealer.revealHiddenCard();
                while (dealer.PlayerHand.Total < 17)
                {
                    DealerInput = "";
                    while (DealerInput != "draw card")
                    {
                        Console.WriteLine("Dealer please enter action:");
                        DealerInput = Console.ReadLine().ToLower();

                        if (DealerInput != "draw card")
                        {
                            Console.WriteLine("Incorrect action!");
                        }
                        else
                        {
                            Console.WriteLine("Correct action");
                            Thread.Sleep(500);
                            dealer.drawCard(deck);
                            Console.WriteLine();
                        }
                    }
                }

                if (dealer.PlayerHand.Total == 21)
                {
                    dealer.HasBlackjack = true;
                    Console.WriteLine("Dealer has blackjack!");
                } else if(dealer.PlayerHand.Total > 21)
                {
                    dealer.IsBusted = true;
                    Console.WriteLine("Dealer is busted!");
                }
                else
                {
                    Console.WriteLine("Dealer stands still on 17");
                }

                foreach (Player player in players)
                {
                    if (player.IsBusted == true) 
                    {
                        Console.WriteLine($"{player.PlayerName} is busted! Dealer wins!");
                    }
                    else
                    {
                        if (dealer.HasBlackjack == true)
                        {
                            if (player.HasBlackjack == true)
                            {
                                player.AdjustPlayerBalance(player.PlayerBet);
                                dealer.AdjustPlayerBalance(-player.PlayerBet);
                                Console.WriteLine("Both have blackjack");
                            }
                            else
                            {
                                Console.WriteLine("Dealer has blackjack!");
                            }
                        }
                        else if (player.HasBlackjack == true){
                            Console.WriteLine($"{player.PlayerName} has blackjack!");
                            player.AdjustPlayerBalance((player.PlayerBet * 2.5));
                            dealer.AdjustPlayerBalance(-(player.PlayerBet * 2.5));
                        }
                        else if (dealer.IsBusted == true)
                        {
                            player.AdjustPlayerBalance((player.PlayerBet * 2));
                            dealer.AdjustPlayerBalance(-(player.PlayerBet * 2));
                            Console.WriteLine("Dealer is busted!");
                        }
                        else
                        {
                            if(dealer.PlayerHand.Total == player.PlayerHand.Total)
                            {
                                player.AdjustPlayerBalance(player.PlayerBet);
                                dealer.AdjustPlayerBalance(-player.PlayerBet);
                                Console.WriteLine($"{player.PlayerName} and Dealer have the same!");
                            }
                            else if (dealer.PlayerHand.Total < player.PlayerHand.Total){
                                player.AdjustPlayerBalance((player.PlayerBet * 2));
                                dealer.AdjustPlayerBalance(-(player.PlayerBet * 2));
                                Console.WriteLine($"{player.PlayerName} has higher then dealer!");
                            }
                            else
                            {
                                Console.WriteLine($"Dealer has higher then {player.PlayerName}! Dealer wins!");
                            }

                        }
                    }                    
                }

                foreach (Player player in players)
                {
                    if(player.PlayerBalance == 0)
                    {
                        Console.WriteLine($"{player.PlayerName} has been removed!");
                        players.Remove(player);
                        
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
}