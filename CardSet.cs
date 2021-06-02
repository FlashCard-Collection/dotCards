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

        private string setName = string.Empty;

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
            return this.cards.Count();
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

        public string GetSetName()
        {
            return this.setName;
        }

        private void loadCards()
        {
            if(cardsetPath.FullName.Contains("README"))
            {
                return;
            }

            string[] lines = File.ReadAllLines(cardsetPath.FullName);

            for (int i = 0; i < lines.Count(); i++)// string line in lines)
            {
                // Get cardset name
                if (lines[i].Contains("#") && !lines[i].Contains("##") && !lines[i].Contains("###"))
                {
                    this.setName = lines[i].Replace("#", "").Trim();
                }

                // Handle one line question and multiple answer lines
                if (lines[i].Contains("##") && !lines[i].Contains("###"))
                {
                    Card card = new Card();
                    card.Question = lines[i] + Environment.NewLine;
                    
                    // read answer until next ## or ###
                    for (int j = i + 1; j < lines.Count(); j++)
                    {
                        if (lines[j].Contains("##") || lines[j].Contains("###"))
                        {
                            // Set new index to continue with next question, one back not to skip the next question
                            i = j - 1; 
                            break;
                        }
                        card.Answer += lines[j] + Environment.NewLine;
                    }

                    this.cards.Add(card);
                }

                // Handle multiple question and answer lines
                if (lines[i].Contains("###"))
                {
                    Card card = new Card();
                    card.Question = lines[i] + Environment.NewLine;
                    
                    // read question until ---
                    for (int j = i + 1; j < lines.Count(); j++)
                    {
                        if (lines[j].Contains("##") || lines[j].Contains("###"))
                        {
                            i = j;
                            Console.WriteLine("error: expected --- before new question.");
                            break;
                        }
                        card.Question += lines[j] + Environment.NewLine;

                        if (lines[j].Contains("---"))
                        {
                            i = j;
                            break;
                        }
                    }

                    // read answer until ## or ###
                    for (int j = i + 1; j < lines.Count(); j++)
                    {
                        if (lines[j].Contains("##") || lines[j].Contains("###"))
                        {
                            i = j - 1; // Set new index to continue with next question
                            break;
                        }
                        card.Answer += lines[j] + Environment.NewLine;
                    }

                    this.cards.Add(card);
                }

            }


        }
    }
}
