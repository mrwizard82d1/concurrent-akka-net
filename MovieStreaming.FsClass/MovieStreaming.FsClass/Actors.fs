namespace MovieStreaming.FsClass

module Actors =
    open Akka.Actor
    open ColorConsole
    open Messages

    type PlaybackActor() =
        inherit ReceiveActor()
        
        do printfn "Creating a PlaybackActor."
        
        let handlePlayMovieMessage({ MovieTitle=movieTitle; UserId=userId }) =
            printfnYellow "Play movie, \"%s,\" for user %d" movieTitle userId
            
        do base.Receive<PlayMovieMessage>((fun m -> handlePlayMovieMessage(m)))
        
        override this.PreStart() = printfnGreen "PlaybackActor PreStart"
        override this.PostStop() = printfnGreen "PlaybackActor PostStop"

