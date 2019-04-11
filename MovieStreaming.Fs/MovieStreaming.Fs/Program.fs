// Learn more about F# at http://fsharp.org

open System
open Akka.FSharp
open MovieStreaming.Fs.Messages
open MovieStreaming.Fs.Actors

[<EntryPoint>]
let main argv =
    let actorSystem = System.create "MovieStreamingActorSystem" (Configuration.defaultConfig())
    printfn "Actor system, 'MovieStreamingActorSystem,' created."
        
    let actor = playbackActor actorSystem
    actor <! { MovieTitle="Akka.NET: The Movie"; UserId=42  }
    
    printfn "Press ENTER to continue..."
    Console.ReadLine() |> ignore
    
    actorSystem.Terminate() |> Async.AwaitTask |> ignore
    
    0 // return an integer exit code
