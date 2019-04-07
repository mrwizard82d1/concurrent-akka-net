namespace MovieStreaming.Messages
{
    public class PlaybackMoveMessage
    {
        public PlaybackMoveMessage(string movieTitle, int userId)
        {
            MovieTitle = movieTitle;
            UserId = userId;
        }
        
        public string MovieTitle { get; }
        public int UserId { get;  }
    }
}