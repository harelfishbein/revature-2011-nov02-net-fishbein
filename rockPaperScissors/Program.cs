using System;
using System.Threading.Tasks;

namespace rockPaperScissors
{   
    enum Move
    {
        rock,
        paper,
        scissors;
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
    class Game {
        static List<Strategy> strategies = {};
        void run() {

        }
        void run(IStrategy strat) {
            var choice = strat.choose();
            Console.WriteLine("Rock,");
            Thread.Sleep(1000);
            Console.WriteLine("Paper,");
            Thread.Sleep(1000);
            Console.WriteLine("Scissors!");
            var playerChoice = Convert(Console.ReadLine(), NumberStyles.Int);
            
        }
    }
    interface IStrategy
    {
        Move choose();
    } 
    class RockStat : IStrategy {
        Move choose() {
            return rock;
        }
    }
}
