using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

            int question_count = 0;
            int answer_count = 0;
            // For each line in a card set, i for line index
            for (int i = 0; i < lines.Count(); i++)
            {
                string line = lines[i];

                // Get cardset name
                if (line.Contains("#") && !line.Contains("## Q") && !line.Contains("### A"))
                {
                    question_count++;
                    this.setName = line.Replace("#", "").Trim();
                }

                if (line.Contains("### A"))
                {
                    answer_count++;
                }

                    // Handle one line question and multiple answer lines
                    if (line.Contains("## Q"))
                {
                    Card card = this.parseCardMultilineQuestion(lines, i);
                    this.cards.Add(card);
                }

            }

            if(question_count != answer_count)
            {
                Console.WriteLine("Question count is not equal to answer count in file: " + cardsetPath.FullName);
            }
        }

        private Card parseCardMultilineQuestion(string[] allLines, int questionIdx)
        {
            Card card = new Card();

            int answerBeginIdx = -1;

            // read question until or ### A
            for (int j = questionIdx + 1; j < allLines.Count(); j++)
            {

                if (allLines[j].Contains("## Q"))
                {
                    break;
                }

                // Read question until mk separator (---)
                if (allLines[j].Contains("### A") || allLines[j].Contains("## Q"))
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

            // read answer until ## Q or ### A
            for (int j = answerBeginIdx; j < allLines.Count(); j++)
            {
                if (allLines[j].Contains("## Q") || allLines[j].Contains("### A"))
                {
                    break;
                }
                card.Answer += allLines[j] + Environment.NewLine;
            }

            return card;
        }

    }
}