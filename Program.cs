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
            //Dealer starts with 1000 chips
            Dealer dealer = new Dealer("Dealer", 1000);

            int DealerStandAmount = 17;

            int TotalPlayers;
            int MaxPlayers = 4;

            int MaxDecks = 4;

            //Player list
            List<Player> players;
            players = new List<Player>();

            //Asking dealer for amount of players
            TotalPlayers = Dealer.AskForTotalPlayers(MaxPlayers);

            int totalDecks = Dealer.AskForTotalDecks(MaxDecks);

            Console.WriteLine($"Total players: {TotalPlayers}");
            string input = "";
            string NameInput = "";

            //Asking if players want a name
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

            //Displaying all players
            Console.WriteLine("Here are all the players");
            foreach (Player player in players)
            {
                Console.WriteLine(player.PlayerName);
            }
            Console.WriteLine();

            //Game starts
            Console.WriteLine("Start of game");

            Boolean PlayGame = true;
            while (PlayGame)
            {
                string DealerInput = "";

                //Asking players how much they will bet
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

                Deck deck = new Deck(totalDecks);

                //Shuffle cards
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
                        deck.Shuffle();
                        Console.WriteLine("Cards have been shuffled!");
                    }
                }

                //Deal cards
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

                        //Each player and the dealer get 2 cards
                        for (int cardsDealt = 0; cardsDealt < 2; cardsDealt++)
                        {
                            foreach (Player player in players)
                            {
                                player.DrawCard(deck);
                                Thread.Sleep(500);
                                Console.WriteLine();
                            }
                            //Second card of the dealer needs to be hidden
                            if (cardsDealt == 0)
                            {
                                dealer.DrawCard(deck);
                                Console.WriteLine();
                                Console.WriteLine();
                            }
                            else
                            {
                                dealer.DrawCardHidden(deck);
                                Console.WriteLine();
                                Console.WriteLine();
                            }
                        }
                    }
                }

                //Players are busted or have blackjack or
                //Asking players individually for hit or stand
                foreach (Player player in players)
                {
                    Boolean playerPlaying = true;

                    while (playerPlaying)
                    {
                        Boolean DealerContinue = false;
                        string DealerInputHandlePlayer = "";

                        player.ShowHand();
                        Console.WriteLine();

                        while (!DealerContinue)
                        {
                            //If player has a blackjack or has a blackjack due to an ace
                            if (player.PlayerHand.Total == 21 || (player.PlayerHand.Total == 10 && player.HasAceToBeDecided))
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
                            //If player is busted
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
                                //If not busted or blackjack the dealer asks the player hit or stand
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
                                        //De computer bepaald of speler hit of stand op basis van de hand total.
                                        if (player.PlayerHitOrNot(player.PlayerHand.Total))
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
                                                    player.DrawCard(deck);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine($"{player.PlayerName}: Stand");
                                            //Als de speler stand met een ace wordt de waarde van de ace bepaald.
                                            //Als de hand 10 of lager is dan wordt ace 11.
                                            //Anders wordt de ace 1.
                                            if (player.HasAceToBeDecided == true)
                                            {
                                                player.PlayerStandsWithAce();
                                            }

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
                //Nadat alle spelers aan de beurt geweest zijn. Toont de dealer zijn kaart.
                dealer.ShowHand();
                dealer.RevealHiddenCard();
                //Zolang de dealer hand total lager is dan 17 moet de dealer kaarten blijven pakken.
                while (dealer.PlayerHand.Total < DealerStandAmount)
                {
                    if (dealer.HasAceToBeDecided && dealer.PlayerHand.Total > 5){
                        dealer.PlayerStandsWithAce();
                    } else
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
                                dealer.DrawCard(deck);
                                Console.WriteLine();
                            }
                        }
                    }
                }

                //Dealer hand checken
                if (dealer.PlayerHand.Total == 21)
                {
                    dealer.HasBlackjack = true;
                    Console.WriteLine("Dealer has blackjack!");
                } else if(dealer.PlayerHand.Total > 21)
                {
                    dealer.IsBusted = true;
                    Console.WriteLine($"Dealer is busted!");
                }
                else
                {
                    Console.WriteLine("Dealer stands still on 17");
                }

                //Bepalen of de spelers winnen, gelijk spelen of verliezen.
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
                            Console.WriteLine($"Dealer is busted, {player.PlayerName} wins!");
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


                //Spelers van de tafel verwijderen als ze geen balans meer hebben.
                foreach (Player player in players)
                {
                    if(player.PlayerBalance == 0)
                    {
                        Console.WriteLine($"{player.PlayerName} has been removed!");
                        players.Remove(player);
                        
                    }
                }

                //Dealer vragen om nog een spel te spelen.
                if (players.Count > 0)
                {
                    PlayGame = Dealer.AskForAnotherGame();
                    if (PlayGame)
                    {
                        foreach (Player player in players)
                        {
                            player.ResetPlayer();
                        }

                        dealer.ResetPlayer();
                    }
                }
                else
                {
                    Console.WriteLine("No players left");
                    PlayGame = false;
                }
            }
        }
    }
}