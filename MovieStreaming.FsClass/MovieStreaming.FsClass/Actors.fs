namespace MovieStreaming.FsClass

module Actors =
    open Akka.Actor
    open Messages

    type PlaybackActor() =
        inherit UntypedActor()
        
        do printfn "Creating a PlaybackActor."
        
        override this.OnReceive(message:obj) =
            match message with
            | :? PlayMovieMessage as playMovieMessage ->
                let { MovieTitle=movieTitle; UserId=userId } = playMovieMessage
                printfn "Received movie title, \"%s.\"" movieTitle
                printfn "Received user id: %d." userId
            | _ -> this.Unhandled(message)
            


