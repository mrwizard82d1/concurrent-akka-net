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

            Console.WriteLine("Creating a PlaybackActor *asynchronously*.");
            var playbackActorProps = Props.Create<PlaybackActor>();
            var playbackActorRef = _movieStreamingActorSystem.ActorOf(playbackActorProps, "PlaybackActor");
            
            playbackActorRef.Tell(new PlaybackMoveMessage("Akka.NET: The Moview", 42));
            playbackActorRef.Tell("Akka.NET: The Movie");
            playbackActorRef.Tell(42);
            
            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            _movieStreamingActorSystem.Terminate().Wait();
        }
    }
}