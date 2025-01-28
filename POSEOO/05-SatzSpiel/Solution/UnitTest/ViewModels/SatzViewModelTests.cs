using System;
using System.IO;

using WPFApp.ViewModels;

namespace UnitTest.ViewModels
{
    using System.Linq;

    using FluentAssertions;

    using Xunit;

    public class SatzViewModelTests
    {
        [Fact]
        public void LoadTest()
        {
            string filename = Path.GetTempFileName();
            var    game     = new Logic.Game();
            var    sentence = new Logic.Sentence();
            
            sentence.NameOfGame = "Test";
            sentence.Words      = new Logic.Word[] { new Logic.Word() { Name = "Hallo", From = DateTime.Today } };
            game.Save(sentence, filename);

            var mv = new GameViewModel();
            mv.FileName = filename;

            mv.Load();

            mv.Sentence.NameOfGame.Should().Be("Test");
            mv.Sentence.Words.Should().HaveCount(1);
            mv.Sentence.Words.First().Name.Should().Be("Hallo");
            mv.Sentence.Words.First().From.Should().Be(DateTime.Today);

            File.Delete(filename);
        }

        [Fact]
        public void SaveTest()
        {
            string filename = Path.GetTempFileName();

            var game = new Logic.Game();
            var sentence  = new Logic.Sentence();
            sentence.NameOfGame = "Test";
            sentence.Words      = new Logic.Word[] { new Logic.Word() { Name = "Hallo", From = DateTime.Today } };
            game.Save(sentence, filename);

            var mv = new GameViewModel();
            mv.FileName = filename;

            mv.Load();

            mv.Sentence.NewWord = "TestNeu";

            mv.Save();

            // assert => read File

            var newSentence = game.Load(filename);

            newSentence.NameOfGame.Should().Be("Test");
            newSentence.Words.Should().HaveCount(2);
            newSentence.Words.First().Name.Should().Be("Hallo");
            newSentence.Words.First().From.Should().Be(DateTime.Today);
            newSentence.Words.Skip(1).First().Name.Should().Be("TestNeu");

            File.Delete(filename);
        }

        [Fact]
        public void CanLoadTest()
        {
            string filename = Path.GetTempFileName();
            var    game     = new Logic.Game();
            var    sentence = new Logic.Sentence();

            sentence.NameOfGame = "Test";
            sentence.Words      = new Logic.Word[] { new Logic.Word() { Name = "Hallo", From = DateTime.Today } };

            File.Delete(filename);

            var mv = new GameViewModel();
            mv.FileName = filename;

            mv.LoadCommand.CanExecute(null).Should().BeFalse();

            game.Save(sentence, filename);

            mv.LoadCommand.CanExecute(null).Should().BeTrue();

            File.Delete(filename);
        }

        [Fact]
        public void CanSaveTest()
        {
            string filename = Path.GetTempFileName();
            var    game     = new Logic.Game();
            var    sentence = new Logic.Sentence();

            sentence.NameOfGame = "Test";
            sentence.Words      = new Logic.Word[] { new Logic.Word() { Name = "Hallo", From = DateTime.Today } };

            game.Save(sentence, filename);

            var mv = new GameViewModel();
            mv.FileName = filename;

            mv.SaveCommand.CanExecute(null).Should().BeFalse();

            mv.Sentence.NewWord = "NewWord";

            mv.SaveCommand.CanExecute(null).Should().BeTrue();

            File.Delete(filename);
        }

    }
}