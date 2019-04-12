// Learn more about F# at http://fsharp.org

open System
open Akka.Actor

module Actors =

    type PlaybackActor() =
        inherit UntypedActor()
        
        do printfn "Creating a PlaybackActor."
        
        override this.OnReceive(message:obj) =
            match message with
            | :? string as movieTitle -> printfn "Received movie title, \"%s.\"" movieTitle
            | :? int as userId -> printfn "Received user id: %d." userId
            | _ -> this.Unhandled(message)
            

[<EntryPoint>]
let main argv =
    let movieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem")
    printfn "Actor system created."
    
    let playbackActorProps = Props.Create<Actors.PlaybackActor>()
    let playbackActorRef = movieStreamingActorSystem.ActorOf(playbackActorProps, "PlaybackActor")
    
    playbackActorRef.Tell("Akka.NET: The Movie")
    playbackActorRef.Tell(42)
    
    printfn "Press ENTER to continue..."
    Console.ReadLine() |> ignore
    
    movieStreamingActorSystem.Terminate() |> Async.AwaitTask |> ignore
    
    0 // return an integer exit code
