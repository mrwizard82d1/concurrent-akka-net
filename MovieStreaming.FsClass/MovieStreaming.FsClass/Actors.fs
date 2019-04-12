namespace MovieStreaming.FsClass

module Actors =
    open Akka.Actor
    open Messages

    type PlaybackActor() =
        inherit ReceiveActor()
        
        do printfn "Creating a PlaybackActor."
        
        let handlePlayMovieMessage({ MovieTitle=movieTitle; UserId=userId }) =
            printfn "Play movie, \"%s,\" for user %d" movieTitle userId
            
        do base.Receive<PlayMovieMessage>((fun m -> handlePlayMovieMessage(m)))

