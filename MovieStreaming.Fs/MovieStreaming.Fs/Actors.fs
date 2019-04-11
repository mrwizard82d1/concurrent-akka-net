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
                | MovieTitle name -> printfn "Playing movie, \"%s.\"" name
                | UserId userId -> printfn "User with id, %d" userId
                
                return! loop()
            }
            loop()

