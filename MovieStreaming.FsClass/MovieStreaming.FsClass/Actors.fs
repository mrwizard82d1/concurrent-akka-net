namespace MovieStreaming.FsClass

module Actors =
    open System
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

        override this.PreRestart(reason, message) =
            printfnGreen "PlaybackActor PreRestart because %A" reason
            base.PreRestart(reason, message)
            
        override this.PostRestart(reason) =
            printfnGreen "PlaybackActor PostRestart because %A" reason
            base.PostRestart(reason)
            
    type UserActor () =
        inherit ReceiveActor ()
        
        let mutable currentlyWatching = String.Empty
        
        do printfn "Creating a UserActor."
        
        let startPlayingMovie movieTitle =
             currentlyWatching <- movieTitle
             printfnYellow "User is currently watching, \"%s.\"" currentlyWatching
        let handlePlayMovieMessage ({ MovieTitle=movieTitle; UserId=userId }) =
            match currentlyWatching with
            | "" ->
                startPlayingMovie movieTitle
            | _ ->
                 printfnRed "Error: Cannot start playing a movie before stopping current movie."
        do base.Receive<PlayMovieMessage> ((fun m -> handlePlayMovieMessage (m)))
        
        let stopPlayingCurrentMovie () =
             printfnYellow "Stop playing movie, \"%s.\"" currentlyWatching
             currentlyWatching <- String.Empty
        let handleStopMovieMessage () =
            match currentlyWatching with
            | "" ->
                 printfnRed "Error: Cannot stop playing a movie if currently watching no movie."
            | _ ->
                 stopPlayingCurrentMovie ()
        do base.Receive<StopMovieMessage> ((fun _ -> handleStopMovieMessage ()))
        
        override this.PreStart() = printfnGreen "UserActor PreStart"
        override this.PostStop() = printfnGreen "UserActor PostStop"

        override this.PreRestart(reason, message) =
            printfnGreen "UserActor PreRestart because %A" reason
            base.PreRestart(reason, message)
            
        override this.PostRestart(reason) =
            printfnGreen "UserActor PostRestart because %A" reason
            base.PostRestart(reason)
