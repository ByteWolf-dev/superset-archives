namespace Logic
{
    using System.Collections;
    using System.Collections.Generic;

    public class Sentence
    {
        public string NameOfGame { get; set; }

        public IEnumerable<Word> Words { get; set; }
    }
}