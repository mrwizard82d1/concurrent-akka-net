// Learn more about F# at http://fsharp.org

open System
open Akka.Actor
open MovieStreaming.FsClass.Messages
open MovieStreaming.FsClass.Actors

let pause prompt =
    printfn "%s" (prompt)
    Console.ReadKey() |> ignore
    
let defaultPause () =
    pause (id "Press any key to continue...")
    
let playMovie movieTitle userId (userActorRef:IActorRef) =
    defaultPause ()
    printfn "Sending PlayMovieMessage for movie, \"%s.\"" movieTitle
    userActorRef.Tell({ MovieTitle=movieTitle; UserId=userId })
    userActorRef
    
let stopMovie (userActorRef:IActorRef) =
    defaultPause ()
    printfn "Sending StopMovieMessage for user, 42."
    userActorRef.Tell(StopMovieMessage ())
    userActorRef
    
[<EntryPoint>]
let main argv =
    let movieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem")
    printfn "Actor system created."
    
    printfn "Asynchronously create actor."
    let userActorProps = Props.Create<UserActor>()
    let userActorRef = movieStreamingActorSystem.ActorOf(userActorProps, "UserActor")
    
    userActorRef
    |> playMovie "Codenan the Destroyer" 42
    |> playMovie "Boolean Lies" 42
    |> stopMovie
    |> stopMovie
    |> ignore
    
    defaultPause ()
    
    movieStreamingActorSystem.Terminate() |> Async.AwaitTask |> ignore
    pause (id "Actor system shutdown.\nPress any key to continue...")
    
    0 // return an integer exit code
