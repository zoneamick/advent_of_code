using System.Runtime.Serialization;

namespace AdventReader
{
    public class Reader
    {
        private readonly string _file;
        public Reader()
        {
            _file = GetFile();
            if (String.IsNullOrEmpty(_file)) throw new ReaderException("No .txt file available.");
        }

        public string Read()
        {
            using (var sr = new StreamReader(_file))
            {
                return sr.ReadToEnd();
            }
        }
        public List<string> ReadLines()
        {
            using (var sr = new StreamReader(_file))
            {
                List<string> lines = new List<string>();
                string? line;
                while ((line = sr.ReadLine()) != null)
                {
                    lines.Add(line);
                }
                return lines;
            }
        }
        private string GetFile() => Directory.GetFiles(Environment.CurrentDirectory, "*.txt").FirstOrDefault();

    }
    [Serializable]
    public class ReaderException : Exception
    {
        public ReaderException()
        {
        }

        public ReaderException(string? message) : base(message)
        {
        }

        public ReaderException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ReaderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

}