using System;
using Akka.Actor;

namespace MovieStreaming
{
    class Program
    {
        private static ActorSystem _movieStreamingActorSystem;
            
        static void Main()
        {
            _movieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");
            
            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            _movieStreamingActorSystem.Terminate().Wait();
        }
    }
}