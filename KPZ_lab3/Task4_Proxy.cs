using System.Text.RegularExpressions;

namespace Task4_Proxy
{
    public interface ITextReader
    {
        char[][] Read(string filePath);
    }

    public class SmartTextReader : ITextReader
    {
        public char[][] Read(string filePath)
        {
            // Для прикладу імітуємо читання
            string[] lines = { "Line 1", "Line 2 text" };
            char[][] result = new char[lines.Length][];
            for (int i = 0; i < lines.Length; i++) result[i] = lines[i].ToCharArray();
            return result;
        }
    }

    public class SmartTextChecker : ITextReader
    {
        private SmartTextReader _reader = new SmartTextReader();
        public char[][] Read(string filePath)
        {
            Console.WriteLine($"[Log]: Opening file {filePath}");
            var result = _reader.Read(filePath);
            Console.WriteLine($"[Log]: Successfully read file. Lines: {result.Length}, Total chars: {result.Sum(l => l.Length)}");
            Console.WriteLine($"[Log]: Closing file {filePath}");
            return result;
        }
    }

    public class SmartTextReaderLocker : ITextReader
    {
        private ITextReader _reader;
        private Regex _regex;
        public SmartTextReaderLocker(ITextReader reader, string regexPattern)
        {
            _reader = reader;
            _regex = new Regex(regexPattern);
        }
        public char[][] Read(string filePath)
        {
            if (_regex.IsMatch(filePath))
            {
                Console.WriteLine("Access denied!");
                return new char[0][];
            }
            return _reader.Read(filePath);
        }
    }
}