// Learn more about F# at http://fsharp.org

open System
open Akka.Actor
open MovieStreaming.FsClass.Messages
open MovieStreaming.FsClass.Actors

let pause prompt =
    printfn "%s" (prompt)
    Console.ReadKey() |> ignore
    
[<EntryPoint>]
let main argv =
    let movieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem")
    printfn "Actor system created."
    
    let playbackActorProps = Props.Create<PlaybackActor>()
    let playbackActorRef = movieStreamingActorSystem.ActorOf(playbackActorProps, "PlaybackActor")
    
    playbackActorRef.Tell({ MovieTitle="Akka.NET: The Movie"; UserId=42 })
    playbackActorRef.Tell({ MovieTitle="Partial Recall"; UserId=99 })
    playbackActorRef.Tell({ MovieTitle="Boolean Lies"; UserId=77 })
    playbackActorRef.Tell({ MovieTitle="Codenan the Destroyer"; UserId=1 })
    
    pause (id "Press any key to continue...")
    
    movieStreamingActorSystem.Terminate() |> Async.AwaitTask |> ignore
    pause (id "Actor system shutdown.\nPress any key to continue...")
    
    0 // return an integer exit code
