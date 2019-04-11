// Learn more about F# at http://fsharp.org

open System
open Akka.FSharp

module Messages = 
    type GreeterMsg =
        | Hello of string
        | Goodbye of string
        
module Actors =
    open Messages 
    
    let playbackActor system =
        spawn system "playbackActor"
        <| fun mailbox ->
            let rec loop() = actor {
                let! msg = mailbox.Receive()
                
                match msg with
                | Hello name -> printfn "Hello, %s" name
                | Goodbye name -> printfn "Goodbye, %s" name
                
                return! loop()
            }
            loop()
    
[<EntryPoint>]
let main argv =
    let actorSystem = System.create "MovieStreamingActorSystem" (Configuration.defaultConfig())
    printfn "Actor system, 'MovieStreamingActorSystem,' created."
        
    let actor = Actors.playbackActor actorSystem
    actor <! Messages.Hello "Joe"
    actor <! Messages.Goodbye "Joe"
    
    printfn "Press ENTER to continue..."
    Console.ReadLine() |> ignore
    
    actorSystem.Terminate() |> Async.AwaitTask |> ignore
    
    0 // return an integer exit code
