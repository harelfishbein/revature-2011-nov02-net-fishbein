using System;
using System.Threading;
using System.Collections.Generic;

namespace rockPaperScissors
{   
    enum Move
    {
        rock,
        paper,
        scissors
    }
    internal interface IStrategy { public Move choose(); }
    class RockStrat : IStrategy {
        public Move choose() {
            return Move.rock;
        }
    }
    class PaperStrat : IStrategy {
        public Move choose() {
            return Move.paper;
        }
    }
    class ScissorsStrat : IStrategy {
        public Move choose() {
            return Move.scissors;
        }
    }
    class RandomStrat : IStrategy {
        private Random rgen;
        private static int count;

        public RandomStrat() {
            this.rgen = new Random();
            RandomStrat.count = Enum.GetNames(typeof(Move)).Length;
        }
        public Move choose() {
            var index = rgen.Next(RandomStrat.count);
            return (Move) index;
        }
    }

    class Game {
        static List<IStrategy> strategies = new List<IStrategy> {new RockStrat(), new PaperStrat(), new ScissorsStrat(), new RockStrat()};
        public uint wins {get; internal set;}
        public uint losses {get; internal set;}

        internal Game () {
            wins = 0;
            losses = 0;
        }

        bool? score(Move computerChoice, Move playerChoice) {
            int compare = (int) computerChoice - (int) playerChoice;
            if (compare < 0) compare+=3;
            switch (compare) {
                case 1: // computer wins
                    return false;
                case 2: // player wins
                    return true;
            }
            return null;
        }
        internal void run() {
            var index = new Random().Next(strategies.Count);
            this.run(strategies[index]);
        }
        void run(IStrategy strat) {
            var computerChoice = strat.choose();
            Console.WriteLine("Rock,");
            Thread.Sleep(1000);
            Console.WriteLine("Paper,");
            Thread.Sleep(1000);
            Console.WriteLine("Scissors!");
            var playerChoice = (Move) Convert.ToInt32(Console.ReadLine());
            var result = score(computerChoice, playerChoice);
            switch (result) {
                case true:
                    System.Console.WriteLine("You win the round.");
                    this.wins++;
                    break;
                case false:
                    System.Console.WriteLine("You lose the round.");
                    this.losses++;
                    break;
                default:
                    System.Console.WriteLine("Its a tie, or invalid input.");
                    break;
            }
        }
    }
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Lets play rock, paper, scissors!");
            Console.WriteLine("Enter 0 for rock, 1 for paper or 2 for scissors.");
            var game = new Game();
            string navigation = "play";
            do {
                switch (navigation) {
                    case "stop":
                        return;
                    case "score":
                        Console.WriteLine($"Wins: {game.wins}, Losses: {game.losses}.");
                        break;
                    case "play":
                        game.run();
                        break;
                    default:
                        Console.WriteLine("Not a valid option.");
                        break;
                }
                Console.WriteLine("Do you want to (play) again, check (score) or (stop)?");
                navigation = Console.ReadLine();

            } while (navigation != "stop");
            

        }
    }
}
