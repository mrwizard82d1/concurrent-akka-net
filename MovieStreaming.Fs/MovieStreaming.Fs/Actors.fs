namespace MovieStreaming.Fs

module Actors =
    open Akka.FSharp
    open Messages
    
    let playbackActor system =
        spawn system "playbackActor"
        <| fun mailbox ->
            let rec loop() = actor {
                let! { MovieTitle=movieTitle; UserId=userId } = mailbox.Receive()
                
                printfn "Received message to play movie, \"%s,\" for user, %d." movieTitle userId
                
                return! loop()
            }
            loop()

