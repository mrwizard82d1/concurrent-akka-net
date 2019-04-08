using System;
using Akka.Actor;

namespace MovieStreaming.Actors
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class PlaybackMovieStatisticsActor : ReceiveActor
    {
        public PlaybackMovieStatisticsActor()
        {
            Console.WriteLine("Creating a PlaybackMovieStatisticsActor.");
        }

        protected override void PreStart()
        {
            ColorConsole.WriteLineWhite("PlaybackMovieStatisticsActor PreStart()");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineWhite("PlaybackMovieStatisticsActor PostStop()");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineWhite($"PlaybackMovieStatisticsActor PreRestart because: {reason}.");
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineWhite($"PlaybackMovieStatisticsActor PostRestart because: {reason}.");
            base.PostRestart(reason);
        }
    }
}