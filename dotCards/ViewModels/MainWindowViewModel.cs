using Avalonia.Controls;
using dotCards.Models;
using dotCards.Views;

using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace dotCards.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        ViewModelBase content;


        private List<SetItem> setCollection = new List<SetItem>();




        public MainWindowViewModel()
        {

            string dir = @".\FlashCards";

            if (Directory.Exists(dir))
            {

                string[] files = this.collectionFiles(dir);

                foreach (string file in files)
                {
                    SetItem itm = new SetItem();

                    var info = new FileInfo(file);
                    itm.Description = info.Name;
                    itm.FilePath = file;
                    itm.QuestionCollection = this.collectionQuestionsFromFile(file);
                    itm.Description = info.Name + "(" + itm.QuestionCollection.Count.ToString() + ")";

                    this.setCollection.Add(itm);
                }


                this.Content = this.SetList = new SetListViewModel(this.setCollection);
                

            }

        }



        public ViewModelBase Content
        {
            get => content;

            private set {
                this.RaiseAndSetIfChanged(ref content, value);
            }
        }

        public SetListViewModel SetList { get; set; }

        public QuestionSetViewModel QuestionSet
        {
            get;
            set;
        }


        public SingleQuestion CurrQuestion { get; set; }

        public void SetSelection(string file)
        {

            SetItem itm = this.setCollection.Where((setitem) => { return setitem.FilePath == file; }).First();

            this.QuestionSet = new QuestionSetViewModel(itm);

            Content = this.QuestionSet;
        }

        public void ShowSets()
        {

            this.Content = this.SetList;
        }

        #region Private Methods

        private string[] collectionFiles(string dirPath)
        {
            return Directory.GetFiles(dirPath, "*.md", SearchOption.AllDirectories);
        }

        private List<SingleQuestion> collectionQuestionsFromFile(string file)
        {

            List <SingleQuestion> listOfQuestions = new List<SingleQuestion>();

            string[] lines = File.ReadAllLines(file);

            // Crappy markdown parser

            

            for (int i = 0; i < lines.Count(); i++)
            {
                SingleQuestion qst = new SingleQuestion();

                string trmLine = lines[i];


                if (trmLine.StartsWith("## ")) // Head
                {
                    qst.Question = trmLine.Replace("## ", "");

                    #region Parse answer

                    bool hasAnswer = false;
                    
                    for (int j = i + 1; j < lines.Count(); j++)
                    { 

                        if (!lines[j].StartsWith("## "))
                        {

                            qst.Answer += lines[j] + Environment.NewLine;
                            hasAnswer = true;

                        } else  {
                            i = j - 1;
                            break;
                        }

                    }

                    if(hasAnswer)
                    {
                        listOfQuestions.Add(qst);
                    }

                    #endregion


                }
                else if (trmLine.StartsWith("# ")) // Head 2
                {

                }
                else if (trmLine.StartsWith("![")) // Link
                {

                }


            }

            return listOfQuestions;

        }




        #endregion

    }

}
