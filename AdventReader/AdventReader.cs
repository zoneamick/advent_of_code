using System.Net.Http.Headers;
using System.Runtime.Serialization;

namespace AdventReader
{
    public class Reader
    {
        private const string _url = "https://adventofcode.com/";
        private readonly string _file;
        private string _text;
        public Reader()
        {
            _file = GetFile();
            if (String.IsNullOrEmpty(_file))
            {
                throw new ReaderException("No .txt file available.");
            }
            ReadText();
        }

        private void ReadText()
        {
            using (var sr = new StreamReader(_file))
            {
                _text = sr.ReadToEnd();
            }
        }
        public string Read() => _text;
        public char[] ReadChars() => _text.ToCharArray();
        public List<string> ReadLines() => _text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).ToList();
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