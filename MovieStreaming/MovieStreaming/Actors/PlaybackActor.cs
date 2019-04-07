using System;
using Akka.Actor;

namespace MovieStreaming.Actors
{
    public class PlaybackActor : UntypedActor
    {
        public PlaybackActor()
        {
            Console.WriteLine("Creating a PlaybackActor.");
        }
        
        protected override void OnReceive(object message)
        {
            if (message is string)
            {
                Console.WriteLine($"Received movie title: \"{message}\".");
            }
            else if (message is int)
            {
                Console.WriteLine($"Received user ID: {message}.");
            }
            else
            {
                // Report unhandled message to base class.
                Unhandled(message);
            }
        }
    }
}