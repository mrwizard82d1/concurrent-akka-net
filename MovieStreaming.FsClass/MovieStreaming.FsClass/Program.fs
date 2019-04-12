// Learn more about F# at http://fsharp.org

open System
open Akka.Actor
open MovieStreaming.FsClass.Messages
open MovieStreaming.FsClass.Actors

[<EntryPoint>]
let main argv =
    let movieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem")
    printfn "Actor system created."
    
    let playbackActorProps = Props.Create<PlaybackActor>()
    let playbackActorRef = movieStreamingActorSystem.ActorOf(playbackActorProps, "PlaybackActor")
    
    playbackActorRef.Tell({ MovieTitle="Akka.NET: The Movie"; UserId=43 })
    
    printfn "Press ENTER to continue..."
    Console.ReadLine() |> ignore
    
    movieStreamingActorSystem.Terminate() |> Async.AwaitTask |> ignore
    
    0 // return an integer exit code
