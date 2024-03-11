using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

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
        private Random random = new Random();

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

        public void Shuffle()
        {
            int i = cards.Count;
            while (i > 1)
            {
                i--;
                int y = random.Next(i + 1);
                Card value = cards[y];
                cards[y] = cards[i];
                cards[i] = value;
            }
        }

        public void DrawCard()
        {
            Card CurrentCard;

            CurrentCard = cards[0];

            Console.WriteLine("You have drawn a card:");
            Console.WriteLine($"{CurrentCard.Rank} of {CurrentCard.Suit} (Value: {CurrentCard.Value})");

            cards.RemoveAt(0);
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

            var cards = deck.GetCards();
            deck.Shuffle();

            foreach (var card in cards)
            {
                Console.WriteLine($"{card.Rank} of {card.Suit} (Value: {card.Value})");
            }

            while (true)
            {
                string input = Console.ReadLine().ToLower();

                if (input == "hit")
                {
                    deck.DrawCard();
                } else if (input == "stand") {
                    break;
                }
            }

        }
    }
}
