using System;
using System.Collections.Generic;
using Akka.Actor;
using MovieStreaming.Messages;

namespace MovieStreaming.Actors
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class UserCoordinatorActor : ReceiveActor
    {
        private readonly Dictionary<int, IActorRef> _users;
        
        public UserCoordinatorActor()
        {
            _users = new Dictionary<int, IActorRef>();
            
            Console.WriteLine("Creating a UserCoordinatorActor.");

            Receive<PlayMovieMessage>(message =>
            {
                CreateChildUserIfNotExists(message.UserId);
                var user = _users[message.UserId];
                user.Tell(message);
            });
            Receive<StopMovieMessage>(message =>
            {
                CreateChildUserIfNotExists(message.UserId);
                var user = _users[message.UserId];
                user.Tell(message);
            });
        }

        private void CreateChildUserIfNotExists(int userId)
        {
            if (!_users.ContainsKey(userId))
            {
                var user = Context.ActorOf(Props.Create(() => new UserActor(userId)), $"User{userId}");
                _users.Add(userId, user);
                
                ColorConsole.WriteLineCyan($"UserCoordinatorActor created a new child UserActor with id: {userId}" +
                                           $" (Total Users: {_users.Count})");
            }
            
            // Otherwise, do nothing.
        }

        protected override void PreStart()
        {
            ColorConsole.WriteLineCyan("UserCoordinatorActor PreStart()");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineCyan("UserCoordinatorActor PostStop()");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineCyan($"UserCoordinatorActor PreRestart because: {reason}.");
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineCyan($"UserCoordinatorActor PostRestart because: {reason}.");
            base.PostRestart(reason);
        }
    }
}