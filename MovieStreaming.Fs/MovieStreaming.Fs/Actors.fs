namespace MovieStreaming.Fs

module Actors =
    open Akka.FSharp
    open Messages
    
    let playbackActor (mailbox:Actor<_>) =
        // This section, before the recursive loop, acts like "PreStart"
        printfn "pre-start"
        
        // Register a function to be executed when the mailbox is destroyed. This acts like "PostStart"
        mailbox.Defer (fun () -> printfn "post-stop")
        
        let rec loop() = actor {
            let! { MovieTitle=movieTitle; UserId=userId } = mailbox.Receive()
            
            printfn "Received message to play movie, \"%s,\" for user, %d." movieTitle userId
            
            return! loop()
        }
        loop()

