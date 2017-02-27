using System;
using System.Text;
using JetBrains.Annotations;

namespace RepetitionDetection.Commons
{
    public class TextSubstring : IEquatable<TextSubstring>
    {
        public TextSubstring([NotNull] StringBuilder text, int startIndex, int length)
        {
            this.text = text;
            this.startIndex = startIndex;
            this.length = length;
        }

        public static implicit operator TextSubstring([NotNull] string word)
        {
            return new TextSubstring(new StringBuilder(word), 0, word.Length);
        }

        public char this[int index]
        {
            get
            {
                if (index >= length)
                    throw new IndexOutOfRangeException("In text substring");
                return text[startIndex + index];
            }
        }

        public bool Equals(TextSubstring other)
        {
            if (other == null)
                return false;
            return text.ToString(startIndex, length).Equals(other.text.ToString(other.startIndex, other.length));
        }

        public int Length {get { return length; }}

        private readonly StringBuilder text;

        private readonly int startIndex;

        private readonly int length;
    }
}
