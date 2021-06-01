using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotCards
{
    public class Card
    {
        public string Question;

        public string Answer;
    }

    public class CardSet
    {
        private FileInfo cardsetPath;

        private List<Card> cards;

        private int questionCount = 0;

        public CardSet(string cardsetPath)
        {
            this.cards = new List<Card>();
            this.cardsetPath = new FileInfo(cardsetPath);

            this.loadCards();
        }

        public List<Card> GetCardSet()
        {
            return this.cards;
        }

        public FileInfo GetFileInfo()
        {
            return this.cardsetPath;
        }

        public int GetQuestionCount()
        {
            return this.questionCount;
        }

        public bool GetCard(int idx, out Card card)
        {
            card = null;
            if (idx >= this.cards.Count || idx < 0)
            {
                return false;
            }

            card =  this.cards[idx];
            return true;
        }

        private void loadCards()
        {
            string[] lines = File.ReadAllLines(cardsetPath.FullName);

            foreach (string line in lines)
            {
                
                if (line.Contains("##") && !line.Contains("###"))
                {
                    Card card = new Card();
                    card.Question = line + Environment.NewLine;
                    this.cards.Add(card);
                    
                    this.questionCount++;
                    continue;
                }

                if (line.Contains("###"))
                {
                    this.cards.Last().Question += line.Replace("###", "") + Environment.NewLine;
                    continue;
                }


                if (this.cards.Count() > 0)
                {
                    this.cards.Last().Answer += line + Environment.NewLine;
                }
                
            }





        }
    }
}
