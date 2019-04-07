using System;
using Akka.Actor;
using MovieStreaming.Actors;
using MovieStreaming.Messages;

namespace MovieStreaming
{
    class Program
    {
        private static ActorSystem _movieStreamingActorSystem;
            
        static void Main()
        {
            _movieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");
            Console.WriteLine("Actor system created.");

            Console.WriteLine("Creating a UserActor *asynchronously*.");
            var userActorProps = Props.Create<UserActor>();
            var userActorRef = _movieStreamingActorSystem.ActorOf(userActorProps, "UserActor");
            
            Pause();
            
            Console.WriteLine("Sending a PlayMovieMessage (Codenan the Destroyer).");
            userActorRef.Tell(new PlayMovieMessage("Codenan the Destroyer", 42));

            Pause();
            
            Console.WriteLine("Sending another PlayMovieMessage (Boolean Lies).");
            userActorRef.Tell(new PlayMovieMessage("Boolean Lies", 42));
            
            Pause();
            
            Console.WriteLine("Sending a StopMovieMessage.");
            userActorRef.Tell(new StopMovieMessage());
            
            Pause();
            
            Console.WriteLine("Sending a StopMovieMessage.");
            userActorRef.Tell(new StopMovieMessage());
            
            Pause();
            
            _movieStreamingActorSystem.Terminate().Wait();
            Console.WriteLine("Actor system shutdown.");
            
            Pause();
        }

        private static void Pause(string prompt = "Press any key to continue...")
        {
            Console.WriteLine(prompt);
            Console.ReadKey();
        }
    }
}