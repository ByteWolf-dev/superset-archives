namespace Logic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class Game
    {
        public Sentence Load(string filename)
        {
            var lines = File.ReadAllLines(filename);

            return new Sentence()
            {
                NameOfGame = lines.FirstOrDefault(),
                Words = lines.Skip(1).Select(line =>
                    {
                        var content = line.Split(';');
                        return new Word()
                        {
                            Name = content[0],
                            From = DateTime.Parse(content[1])
                        };
                    }
                ).ToArray()
            };
        }

        public void Save(Sentence sentence, string filename)
        {
            var lines = new List<string>();
            lines.Add(sentence.NameOfGame);
            lines.AddRange(sentence.Words.Select(w => $"{w.Name};{w.From}"));

            File.WriteAllLines(filename, lines);
        }
    }
}