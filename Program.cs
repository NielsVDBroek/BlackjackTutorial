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

    public class Rank
    {
        public string Name { get; }
        public int Value { get; }

        public Rank(string name, int value)
        {
            Name = name;
            Value = value;
        }

        public static readonly Rank Ace = new Rank("Ace", 1);
        public static readonly Rank Two = new Rank("Two", 2);
        public static readonly Rank Three = new Rank("Three", 3);
        public static readonly Rank Four = new Rank("Four", 4);
        public static readonly Rank Five = new Rank("Five", 5);
        public static readonly Rank Six = new Rank("Six", 6);
        public static readonly Rank Seven = new Rank("Seven", 7);
        public static readonly Rank Eight = new Rank("Eight", 8);
        public static readonly Rank Nine = new Rank("Nine", 9);
        public static readonly Rank Ten = new Rank("Ten", 10);
        public static readonly Rank Jack = new Rank("Jack", 10);
        public static readonly Rank Queen = new Rank("Queen", 10);
        public static readonly Rank King = new Rank("King", 10);

        public static IEnumerable<Rank> Ranks => new[] { Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King };
    }

    public class Card
    {
        public Suit Suit { get; private set; }
        public Rank Rank { get; private set; }

        public Card(Suit suit, Rank rank)
        {
            Suit = suit;
            Rank = rank;
        }

        public int Value => Rank.Value;
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
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Rank.Ranks)
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
            Card CurrentCard = cards[0];

            Console.WriteLine("You have drawn a card:");
            Console.WriteLine($"{CurrentCard.Rank.Name} of {CurrentCard.Suit} (Value: {CurrentCard.Rank.Value})");

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
            deck.Shuffle();

            var cards = deck.GetCards();

            foreach (var card in cards)
            {
                Console.WriteLine($"{card.Rank.Name} of {card.Suit} (Value: {card.Value})");

            }

            Console.WriteLine("Start of game");

            while (true)
            {
                string input = Console.ReadLine().ToLower();

                if (input == "hit")
                {
                    deck.DrawCard();
                } else if (input == "stand") {
                    Console.WriteLine("Player stands");
                } else if(input == "break")
                {
                    break;
                }
            }

        }
    }
}
