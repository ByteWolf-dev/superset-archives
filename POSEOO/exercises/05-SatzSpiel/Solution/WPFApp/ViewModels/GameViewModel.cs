using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows.Input;

using WPFApp.Helpers;

namespace WPFApp.ViewModels
{
    using System.Linq;
    using System.Reflection;

    using WPFApp.Models;

    public class GameViewModel : INotifyPropertyChanged
    {
        #region INPC

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChanged<TProperty>(Expression<Func<TProperty>> projection)
        {
            var memberExpression = (MemberExpression)projection.Body;
            OnPropertyChanged(memberExpression.Member.Name);
        }

        #endregion

        #region Properties

        private static string BaseDirectory => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private        string _filename = $"{BaseDirectory}\\game.txt";

        public string FileName
        {
            get => _filename;
            set
            {
                _filename = value;
                OnPropertyChanged();
            }
        }

        private Models.Sentence _sentence = new Models.Sentence() { Words = new System.Collections.ObjectModel.ObservableCollection<Models.Word>() };

        public Models.Sentence Sentence
        {
            get => _sentence;
            set
            {
                _sentence = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Operations

        public void Load()
        {
            var sentence = new Logic.Game().Load(FileName);
            Sentence.NameOfGame = sentence.NameOfGame;
            Sentence.Words.Clear();

            foreach (var w in sentence.Words)
            {
                Sentence.Words.Add(new Models.Word() { From = w.From, Name = w.Name });
            }

            OnPropertyChanged(() => Sentence);
        }

        bool CanLoad()
        {
            return File.Exists(FileName);
        }

        public void Save()
        {
            Sentence.Words.Add(new Word() { From = DateTime.Now, Name = Sentence.NewWord });

            var sentence = new Logic.Sentence()
            {
                NameOfGame = Sentence.NameOfGame,
                Words = Sentence.Words.Select(w =>
                    new Logic.Word()
                    {
                        From = w.From,
                        Name = w.Name
                    })
            };

            new Logic.Game().Save(sentence, FileName);

/*
            Sentence.Words.Clear();
            Sentence.NewWord = "";
            Sentence.NameOfGame = "";
*/
            OnPropertyChanged(() => Sentence);
        }

        bool CanSave()
        {
            return !string.IsNullOrEmpty(Sentence.NewWord);
        }

        #endregion

        #region Commands

        public ICommand LoadCommand => new DelegateCommand(Load, CanLoad);
        public ICommand SaveCommand => new DelegateCommand(Save, CanSave);

        #endregion
    }
}