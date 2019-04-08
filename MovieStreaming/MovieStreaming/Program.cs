using System;
using System.Threading.Tasks;
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
            Console.WriteLine("Creating MovieStreamingActorSystem.");
            _movieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");
            
            Console.WriteLine("Asynchronously creating actor supervisory hierarchy.");
            _movieStreamingActorSystem.ActorOf(Props.Create<PlaybackActor>(), "Playback");

            do
            {
                ShortPause();
                
                Console.WriteLine();
                ColorConsole.WriteLineGray("Enter a command followed by pressing the ENTER key...");
                var command = Console.ReadLine();

                if (command == null)
                {
                    // do nothing and read again
                }
                else if (command.StartsWith("play"))
                {
                    var userId = int.Parse(command.Split(',')[1]);
                    var movieTitle = command.Split(',')[2];
                    
                    var message = new PlayMovieMessage(movieTitle, userId);
                    _movieStreamingActorSystem.ActorSelection("/user/Playback/UserCoordinator").Tell(message);
                }
                else if (command.StartsWith("stop"))
                {
                    var userId = int.Parse(command.Split(',')[1]);
                    
                    var message = new StopMovieMessage(userId);
                    _movieStreamingActorSystem.ActorSelection("/user/Playback/UserCoordinator").Tell(message);
                }
                else if (command.StartsWith("exit"))
                {
                    _movieStreamingActorSystem.Terminate().Wait();
                    ColorConsole.WriteLineGray("Actor system shutdown.");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
            } while (true);
        }

        private static async void ShortPause()
        {
            await Task.Delay(1000);
        }
    }
}