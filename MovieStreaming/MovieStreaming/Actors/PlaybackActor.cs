using System;
using Akka.Actor;
using MovieStreaming.Messages;

namespace MovieStreaming.Actors
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class PlaybackActor : ReceiveActor
    {
        public PlaybackActor()
        {
            Console.WriteLine("Creating a PlaybackActor.");

            Receive<PlayMovieMessage>(message => HandlePlayMovieMessage(message));
        }

        private static void HandlePlayMovieMessage(PlayMovieMessage message)
        {
            ColorConsole.WriteLineYellow($"Playing movie, \"{message.MovieTitle},\" for user, {message.UserId}.");
        }
    }
}