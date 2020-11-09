using dotCards.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace dotCards.ViewModels
{
    public class QuestionSetViewModel : ViewModelBase
    {
        private SetItem currSetItem = null;

        private int questionIdx = 0;

        private string currQuestion = string.Empty;
        private string currAnswer = string.Empty;


        public QuestionSetViewModel(SetItem itm)
        {
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
            if (questionIdx < this.currSetItem.QuestionCollection.Count - 1)
            {
                questionIdx++;
            }

            this.CurrQuestion = this.currSetItem.QuestionCollection[questionIdx].Question;
            this.CurrAnswer = this.currSetItem.QuestionCollection[questionIdx].Answer;
        }

        public string CurrQuestion { 
            get => this.currQuestion;
            set => this.RaiseAndSetIfChanged(ref this.currQuestion, value);
        }

        public string CurrAnswer
        {
            get => this.currAnswer;
            set => this.RaiseAndSetIfChanged(ref this.currAnswer, value);

        }




    }
}
