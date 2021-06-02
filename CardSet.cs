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
        public Card()
        {
            this.Question = string.Empty;
            this.Answer = string.Empty;
        }

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

            // For each line in a card set, i for line index
            for (int i = 0; i < lines.Count(); i++)
            {
                // Get cardset name
                if (lines[i].Contains("#") && !lines[i].Contains("##") && !lines[i].Contains("###"))
                {
                    this.setName = lines[i].Replace("#", "").Trim();
                }

                // Handle one line question and multiple answer lines
                if (lines[i].Contains("##") && !lines[i].Contains("###"))
                {
                    Card card = this.parseCardSingleLineQuestion(lines, i);
                    this.cards.Add(card);
                }

                // Handle multiple question and answer lines
                if (lines[i].Contains("###"))
                {
                    Card card = this.parseCardMultilineQuestion(lines, i);
                    this.cards.Add(card);
                }
            }
        }

        private Card parseCardMultilineQuestion(string[] allLines, int questionIdx)
        {
            Card card = new Card();
            card.Question = allLines[questionIdx] + Environment.NewLine;

            int answerBeginIdx = -1;

            // read question until ---
            for (int j = questionIdx + 1; j < allLines.Count(); j++)
            {
                if (allLines[j].Contains("##") || allLines[j].Contains("###"))
                {
                    Console.WriteLine("Error expect --- before " + (allLines[j].Contains("###") ? "###" : "##"));
                    break;
                }

                // Read question until mk separator (---)
                if (allLines[j].Contains("---"))
                {
                    answerBeginIdx = j + 1;
                    break;
                }

                card.Question += allLines[j] + Environment.NewLine;
            }

            // No answer available
            if (answerBeginIdx == -1)
            {
                card.Answer = string.Empty;
                return card;
            }

            // read answer until ## or ###
            for (int j = answerBeginIdx; j < allLines.Count(); j++)
            {
                if (allLines[j].Contains("##") || allLines[j].Contains("###"))
                {
                    break;
                }
                card.Answer += allLines[j] + Environment.NewLine;
            }

            return card;
        }

        private Card parseCardSingleLineQuestion(string[] allLines, int questionIdx)
        {
            Card card = new Card();
            card.Question = allLines[questionIdx] + Environment.NewLine;
            
            // read answer until next ## or ###
            for (int i = questionIdx + 1; i < allLines.Count(); i++)
            {
                if (allLines[i].Contains("##") || allLines[i].Contains("###"))
                {
                    break;
                }
                card.Answer += allLines[i] + Environment.NewLine;
            }

            return card;
            
        }
    
    }
}