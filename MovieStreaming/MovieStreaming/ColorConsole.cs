using System;

namespace MovieStreaming
{
    public static class ColorConsole
    {
        public static void WriteLineGreen(string message)
        {
            WriteLineInColor(message, ConsoleColor.Green);
        }

        public static void WriteLineYellow(string message)
        {
            WriteLineInColor(message, ConsoleColor.Yellow);
        }

        private static void WriteLineInColor(string message, ConsoleColor foregroundColor)
        {
            var beforeColor = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(message);
            Console.ForegroundColor = beforeColor;
        }
        
    }
}