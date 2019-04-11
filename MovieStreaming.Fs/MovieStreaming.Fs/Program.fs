// Learn more about F# at http://fsharp.org

open System
open System.Threading
open Akka.FSharp
open MovieStreaming.Fs.Messages
open MovieStreaming.Fs.Actors

let prompt promptFunc =
    promptFunc()
    Console.ReadKey()
    
let defaultPrompt () =
    prompt (fun () -> printfn "Press any key to continue...")
    |> ignore

[<EntryPoint>]
let main argv =
    let actorSystem = System.create "MovieStreamingActorSystem" (Configuration.defaultConfig())
    printfn "Actor system, 'MovieStreamingActorSystem,' created."
        
    let actorRef = spawn actorSystem "PlaybackActor" (playbackActor)
    
    actorRef <! { MovieTitle="Akka.NET: The Movie"; UserId=42  }
    actorRef <! { MovieTitle="Partial Recall"; UserId=99  }
    actorRef <! { MovieTitle="Boolean Lies"; UserId=77  }
    actorRef <! { MovieTitle="Codenan the Destroyer"; UserId=1  }
    
    defaultPrompt()
    
    Thread.Sleep(500)
    
    actorSystem.Terminate() |> Async.AwaitTask |> ignore
    
    0 // return an integer exit code
