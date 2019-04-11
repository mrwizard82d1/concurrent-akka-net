namespace MovieStreaming.Fs

module Messages = 
    type Playback =
        | MovieTitle of string
        | UserId of int
        
