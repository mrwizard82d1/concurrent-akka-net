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
            
            ColorConsole.WriteLineCyan("Setting initial behavior to Stopped.");
            Stopped();
        }

        private void Playing()
        {
            Receive<PlayMovieMessage>(_ => 
                ColorConsole.WriteLineRed("Error: cannot start playing a movie before stopping existing one."));
            Receive<StopMovieMessage>(_ => StopPlayingCurrentMovie());
            
            ColorConsole.WriteLineCyan("UserActor has now become Playing.");
            
        }

        private void Stopped()
        {
            Receive<PlayMovieMessage>(message => StartPlayingMovie(message.MovieTitle));
            Receive<StopMovieMessage>(message =>
                ColorConsole.WriteLineRed("Error: cannot stop if nothing is playing."));
            
            ColorConsole.WriteLineCyan("UserActor has now become Stopped.");
        }

        private void StartPlayingMovie(string movieTitle)
        {
            _currentlyWatching = movieTitle;
            
            ColorConsole.WriteLineYellow($"User currently watching, \"{movieTitle}\".");
            
            Become(Playing);
        }

        private void StopPlayingCurrentMovie()
        {
            ColorConsole.WriteLineYellow($"User has stopped watching, \"{_currentlyWatching}\".");
            
            _currentlyWatching = null;
            
            Become(Stopped);
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