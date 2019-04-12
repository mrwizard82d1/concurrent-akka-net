namespace MovieStreaming.FsClass

module ColorConsole =
    // Based on code on GitHub: https://gist.github.com/devhawk/4719d1b369170b206cd88b9da16e1b8a
    open System

    // helper function to set the console collor and automatically set it back when disposed
    let consoleColor (fc : ConsoleColor) = 
        let current = Console.ForegroundColor
        Console.ForegroundColor <- fc
        { new IDisposable with
              member x.Dispose() = Console.ForegroundColor <- current }

    // printf statements that allow user to specify output color
    let cprintf color str = Printf.kprintf (fun s -> use c = consoleColor color in printf "%s" s) str
    let cprintfn color str = Printf.kprintf (fun s -> use c = consoleColor color in printfn "%s" s) str
    
    let printfnGreen str = cprintfn ConsoleColor.Green str
    let printfnYellow str = cprintfn ConsoleColor.Yellow str
    let printfnRed str = cprintfn ConsoleColor.Red str

