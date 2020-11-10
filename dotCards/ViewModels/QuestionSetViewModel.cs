using dotCards.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive;
using System.Text;
using System.Windows;

namespace dotCards.ViewModels
{
    public class QuestionSetViewModel : ViewModelBase
    {

        private SetItem currSetItem = null;

        private int questionIdx = 0;

        private string currQuestion = string.Empty;

        private string currAnswer = string.Empty;

        private bool showAnswer = false; 

        public QuestionSetViewModel(SetItem itm)
        {

            this.SpaceCommand = ReactiveCommand.Create(() =>
            {
                this.ToggleAnswerVisibility();
            });

            this.NextCommand = ReactiveCommand.Create(() =>
            {
                this.NextQuestion();
            });

            this.PrevCommand = ReactiveCommand.Create(() =>
            {
                this.PrevQuestion();
            });


            this.currSetItem = itm;

            this.CurrQuestion = this.currSetItem.QuestionCollection[questionIdx].Question;
            this.CurrAnswer = this.currSetItem.QuestionCollection[questionIdx].Answer;

        }


        public void PrevQuestion()
        {

            if (questionIdx - 1 > 0)
            {
                questionIdx--;
            }

            this.CurrQuestion = this.currSetItem.QuestionCollection[questionIdx].Question;
            this.CurrAnswer = this.currSetItem.QuestionCollection[questionIdx].Answer;

        }

        public void NextQuestion()
        {

            if(questionIdx < this.currSetItem.QuestionCollection.Count - 1)
            {
                questionIdx++;
            }

            this.CurrQuestion = this.currSetItem.QuestionCollection[questionIdx].Question;
            this.CurrAnswer = this.currSetItem.QuestionCollection[questionIdx].Answer;

        }

        public void ToggleAnswerVisibility()
        {
            this.ShowAnswer = !this.ShowAnswer;
        }

        public ReactiveCommand<Unit, Unit> SpaceCommand { get; set; }

        public ReactiveCommand<Unit, Unit> NextCommand { get; set; }

        public ReactiveCommand<Unit, Unit> PrevCommand { get; set; }

        public string CurrQuestion { 
            get => this.currQuestion;
            set => this.RaiseAndSetIfChanged(ref this.currQuestion, value);
        }

        public string CurrAnswer
        {
            get => this.currAnswer;
            set => this.RaiseAndSetIfChanged(ref this.currAnswer, value);

        }

        public bool ShowAnswer
        {
            get => this.showAnswer;
            set => this.RaiseAndSetIfChanged(ref this.showAnswer, value);

        }

        




    }
}
