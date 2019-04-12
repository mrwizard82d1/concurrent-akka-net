// Learn more about F# at http://fsharp.org

open System
open Akka.Actor

module Messages =
    type PlayMovieMessage = { MovieTitle:string; UserId:int }
    
module Actors =
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
            

[<EntryPoint>]
let main argv =
    let movieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem")
    printfn "Actor system created."
    
    let playbackActorProps = Props.Create<Actors.PlaybackActor>()
    let playbackActorRef = movieStreamingActorSystem.ActorOf(playbackActorProps, "PlaybackActor")
    
    playbackActorRef.Tell({ Messages.MovieTitle="Akka.NET: The Movie"; Messages.UserId=42 })
    
    printfn "Press ENTER to continue..."
    Console.ReadLine() |> ignore
    
    movieStreamingActorSystem.Terminate() |> Async.AwaitTask |> ignore
    
    0 // return an integer exit code
