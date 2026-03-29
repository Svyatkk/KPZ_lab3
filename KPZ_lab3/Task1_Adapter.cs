namespace Task1_Adapter
{
    public interface ILogger
    {
        void Log(string message);
        void Error(string message);
        void Warn(string message);
    }

    public class ConsoleLogger : ILogger
    {
        public void Log(string message) { Print(message, ConsoleColor.Green); }
        public void Error(string message) { Print(message, ConsoleColor.Red); }
        public void Warn(string message) { Print(message, ConsoleColor.DarkYellow); }
        private void Print(string msg, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ResetColor();
        }
    }

    public class FileWriter
    {
        public void Write(string text) => Console.Write($"[File Write]: {text}");
        public void WriteLine(string text) => Console.WriteLine($"[File WriteLine]: {text}");
    }

    public class FileLoggerAdapter : ILogger
    {
        private FileWriter _fileWriter;
        public FileLoggerAdapter(FileWriter fileWriter) { _fileWriter = fileWriter; }
        public void Log(string message) => _fileWriter.WriteLine($"LOG: {message}");
        public void Error(string message) => _fileWriter.WriteLine($"ERROR: {message}");
        public void Warn(string message) => _fileWriter.WriteLine($"WARN: {message}");
    }
}