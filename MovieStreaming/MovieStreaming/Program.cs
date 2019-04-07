using System;
using Akka.Actor;
using MovieStreaming.Actors;

namespace MovieStreaming
{
    class Program
    {
        private static ActorSystem _movieStreamingActorSystem;
            
        static void Main()
        {
            _movieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");
            Console.WriteLine("Actor system created.");

            Console.WriteLine("Creating a PlaybackActor *asynchronously*.");
            var playbackActorProps = Props.Create<PlaybackActor>();
            var playbackActorRef = _movieStreamingActorSystem.ActorOf(playbackActorProps, "PlaybackActor");
            
            playbackActorRef.Tell("Akka.NET: The Movie");
            playbackActorRef.Tell(42);
            playbackActorRef.Tell('c');
            
            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            _movieStreamingActorSystem.Terminate().Wait();
        }
    }
}