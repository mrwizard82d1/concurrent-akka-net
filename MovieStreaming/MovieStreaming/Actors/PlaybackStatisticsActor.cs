using System;
using Akka.Actor;

namespace MovieStreaming.Actors
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class PlaybackStatisticsActor : ReceiveActor
    {
        public PlaybackStatisticsActor()
        {
            Console.WriteLine("Creating a PlaybackStatisticsActor.");

            Context.ActorOf(Props.Create<PlaybackMovieStatisticsActor>(), "PlaybackMovieStatisticsActor");
        }

        protected override void PreStart()
        {
            ColorConsole.WriteLineWhite("PlaybackStatisticsActor PreStart()");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineWhite("PlaybackStatisticsActor PostStop()");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineWhite($"PlaybackStatisticsActor PreRestart because: {reason}.");
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineWhite($"PlaybackStatisticsActor PostRestart because: {reason}.");
            base.PostRestart(reason);
        }
    }
}