namespace MovieStreaming.Fs

module Actors =
    open Akka.FSharp
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

