using System;
using System.Collections.Generic;
using System.Text;

namespace dotCards.Models
{
    public class SetItem
    {

        public string Description { get; set; }

        public int NumQuenstions { get; set; }

        public string FilePath { get; set; }

        public bool IsChecked { get; set; }

        public List<SingleQuestion> QuestionCollection { get; set; }

    }
}
