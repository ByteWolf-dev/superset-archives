using System.Collections.ObjectModel;

namespace WPFApp.Models
{
    public class Sentence
    {
        public string NameOfGame { get; set; }

        public string NewWord { get; set; }

        public ObservableCollection<Word> Words { get; set; }
    }
}