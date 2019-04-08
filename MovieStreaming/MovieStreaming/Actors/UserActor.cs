using System;
using Akka.Actor;
using MovieStreaming.Messages;

namespace MovieStreaming.Actors
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class UserActor : ReceiveActor
    {
        private int _userId;
        private string _currentlyWatching;
        
        public UserActor(int userId)
        {
            _userId = userId;
            
            Console.WriteLine("Creating a UserActor.");
            
            Stopped();
        }

        private void Playing()
        {
            Receive<PlayMovieMessage>(_ =>
                ColorConsole.WriteLineRed(
                    $"Error: user {_userId} cannot start playing a movie before stopping existing one."));
            Receive<StopMovieMessage>(_ => StopPlayingCurrentMovie());
            
            ColorConsole.WriteLineYellow("UserActor has now become Playing.");
            
        }

        private void Stopped()
        {
            Receive<PlayMovieMessage>(message => StartPlayingMovie(message.MovieTitle));
            Receive<StopMovieMessage>(message =>
                ColorConsole.WriteLineRed("Error: cannot stop if nothing is playing."));
            
            ColorConsole.WriteLineYellow("UserActor has now become Stopped.");
        }

        private void StartPlayingMovie(string movieTitle)
        {
            _currentlyWatching = movieTitle;
            
            ColorConsole.WriteLineYellow($"User {_userId} currently watching, \"{movieTitle}\".");
            
            Become(Playing);
        }

        private void StopPlayingCurrentMovie()
        {
            ColorConsole.WriteLineYellow($"User {_userId} has stopped watching, \"{_currentlyWatching}\".");
            
            _currentlyWatching = null;
            
            Become(Stopped);
        }

        protected override void PreStart()
        {
            ColorConsole.WriteLineYellow($"UserActor {_userId} PreStart()");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineYellow($"UserActor {_userId} PostStop()");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineYellow($"UserActor {_userId} Restart(): {reason}.");
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineYellow($"UserActor {_userId} PostRestart(): {reason}.");
            base.PostRestart(reason);
        }
    }
}