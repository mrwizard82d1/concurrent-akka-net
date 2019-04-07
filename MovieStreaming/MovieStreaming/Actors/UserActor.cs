using System;
using Akka.Actor;
using MovieStreaming.Messages;

namespace MovieStreaming.Actors
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class UserActor : ReceiveActor
    {
        private string _currentlyWatching;
        
        public UserActor()
        {
            Console.WriteLine("Creating a UserActor.");

            Receive<PlayMovieMessage>(message => HandlePlayMovieMessage(message));
            Receive<StopMovieMessage>(_ => HandleStopMovieMessage());
        }

        private void HandlePlayMovieMessage(PlayMovieMessage message)
        {
            if (_currentlyWatching != null)
            {
                ColorConsole.WriteLineRed($"Error: cannot start movie before stopping, \"{_currentlyWatching}\".");
            }
            else
            {
                StartPlayingMovie(message.MovieTitle);
            }
        }

        private void StartPlayingMovie(string movieTitle)
        {
            _currentlyWatching = movieTitle;
            
            ColorConsole.WriteLineYellow($"User currently watching, \"{movieTitle}\".");
        }

        private void HandleStopMovieMessage()
        {
            if (_currentlyWatching == null)
            {
                ColorConsole.WriteLineRed($"Error: cannot stop if nothing is playing.");
            }
            else
            {
                StopPlayingCurrentMovie();
            }
        }

        private void StopPlayingCurrentMovie()
        {
            ColorConsole.WriteLineYellow($"User has stopped watching, \"{_currentlyWatching}\".");
            
            _currentlyWatching = null;
        }

        protected override void PreStart()
        {
            ColorConsole.WriteLineGreen("PlaybackActor PreStart()");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineGreen("PlaybackActor PostStart()");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineGreen($"PlaybackActor Restart(): {reason}.");
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineGreen($"PlaybackActor PostRestart(): {reason}.");
            base.PostRestart(reason);
        }
    }
}