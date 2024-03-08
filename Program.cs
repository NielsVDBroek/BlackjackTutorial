using System;
using System.Collections.Generic;

namespace BlackjackTutorial
{
    public enum Suit
    {
        Hearts,
        Diamonds,
        Clubs,
        Spades
    }

    public enum Rank
    {
        Ace = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 10,
        Queen = 10,
        King = 10
    }

    public class Card
    {
        public Suit Suit { get; private set; }
        public Rank Rank { get; private set; }
        public int Value { get; private set; }

        public Card(Suit suit, Rank rank)
        {
            Suit = suit;
            Rank = rank;
            Value = (int)rank;

        }
    }

    public class Deck
    {
        private List<Card> cards;

        public Deck()
        {
            cards = new List<Card>();
            CreateDeck();
        }

        private void CreateDeck()
        {
            foreach (Suit suit in System.Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in System.Enum.GetValues(typeof(Rank)))
                {
                    cards.Add(new Card(suit, rank));
                }
            }
        }

        public List<Card> GetCards()
        {
            return cards;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Here is the deck:");
            Deck deck = new Deck();
            deck.GetCards();
        }
    }
}
