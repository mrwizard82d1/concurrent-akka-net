using System;
using Akka.Actor;
using MovieStreaming.Messages;

namespace MovieStreaming.Actors
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class PlaybackActor : UntypedActor
    {
        public PlaybackActor()
        {
            Console.WriteLine("Creating a PlaybackActor.");
        }
        
        protected override void OnReceive(object message)
        {
            if (message is PlaybackMoveMessage)
            {
                var typedMessage = (PlaybackMoveMessage) message;
                Console.WriteLine($"Received movie title: \"{typedMessage.MovieTitle}\".");
                Console.WriteLine($"Received user ID: \"{typedMessage.UserId    }\".");
            }
            else
            {
                // Report unhandled message to base class.
                Unhandled(message);
            }
        }
    }
}