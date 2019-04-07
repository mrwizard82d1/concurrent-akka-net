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

        public static void WriteLineRed(string message)
        {
            WriteLineInColor(message, ConsoleColor.Red);
        }

        public static void WriteLineCyan(string message)
        {
            WriteLineInColor(message, ConsoleColor.Cyan);
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