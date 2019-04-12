namespace MovieStreaming.FsClass

module Messages =
    type PlayMovieMessage = { MovieTitle:string; UserId:int }
    type StopMovieMessage = StopMovieMessage of unit
     
