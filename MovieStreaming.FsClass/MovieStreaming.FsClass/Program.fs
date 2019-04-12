// Learn more about F# at http://fsharp.org

open System
open Akka.Actor

module Actors =
    type PlaybackActor() =
        inherit UntypedActor()
        
        do printfn "Creating a PlaybackActor."
        
        override this.OnReceive(message:obj) =
            raise <| NotImplementedException()

[<EntryPoint>]
let main argv =
    let movieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem")
    printfn "Actor system created."
    
    let playbackActorProps = Props.Create<Actors.PlaybackActor>()
    let playbackActorRef = movieStreamingActorSystem.ActorOf(playbackActorProps, "PlaybackActor")
    
    printfn "Press ENTER to continue..."
    Console.ReadLine() |> ignore
    
    movieStreamingActorSystem.Terminate() |> Async.AwaitTask |> ignore
    
    0 // return an integer exit code
