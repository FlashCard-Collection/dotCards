using Markdig;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DotCards
{
    public partial class FrmMain : Form
    {
        private List<CardSet> cardSetCollection;

        private CardSet currCardSet = null;

        private string flashCardRepo = string.Empty;

        private int currCardIdx = 0;

        public FrmMain()
        {
            InitializeComponent();

            this.htmlView.Text = "";

            this.flashCardRepo = Path.Combine(Environment.CurrentDirectory, "FlashCards");
            this.lblQuestionCountNum.Text = string.Empty;
            this.lblPathStr.Text = string.Empty;
            

            this.loadCardSets();
            this.fillListView();
        }

        private void fillListView()
        {
            foreach (CardSet set in this.cardSetCollection)
            {
                ListViewItem itm = new ListViewItem(
                    set.GetFileInfo().FullName.Substring(
                        this.flashCardRepo.Length + 1,
                        set.GetFileInfo().FullName.Length - this.flashCardRepo.Length - 1)
                    );
                itm.Tag = (object)set;
                this.lstSets.Items.Add(itm);
            }
        }

        public void loadCardSets()
        {
            var cardSets = Directory.EnumerateFiles(
                this.flashCardRepo, 
                "*.md", 
                SearchOption.AllDirectories);
            cardSetCollection = new List<CardSet>();

            int totalQuestions = 0;
            foreach (string cardSetPath in cardSets)
            {
                CardSet set = new CardSet(cardSetPath);
                totalQuestions += set.GetQuestionCount();
                cardSetCollection.Add(set);
            }
            this.lblTotalQCountNum.Text   = totalQuestions.ToString();
            this.lblQuestionCountNum.Text = cardSetCollection.Count().ToString();
        }

        private void lstSets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lstSets.SelectedItems.Count <= 0)
            {
                return;
            }
                
            CardSet currSet = (CardSet)this.lstSets.SelectedItems[0].Tag;
            if (currSet != null)
            {
                this.lblQuestionCountNum.Text = currSet.GetQuestionCount().ToString();
                this.lblPathStr.Text = currSet.GetFileInfo().FullName;
            }

        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (this.lstSets.SelectedItems.Count <= 0)
            {
                return;
            }
            CardSet currSet = (CardSet)this.lstSets.SelectedItems[0].Tag;
            Directory.SetCurrentDirectory(currSet.GetFileInfo().Directory.FullName);
            this.currCardSet = currSet;
            this.tabControl1.SelectedTab = this.tbQuestions;
        }

        private void btnNextQ_Click(object sender, EventArgs e)
        {
            this.currCardIdx++;
            Card card;
            if(this.currCardSet.GetCard(this.currCardIdx, out card))
            {
                Console.WriteLine("Question:" + card.Question);
                this.htmlView.Text = Markdown.ToHtml(card.Question);
            }
        }

        private void btnShowAnswer_Click(object sender, EventArgs e)
        {
            Card card;
            if (this.currCardSet.GetCard(this.currCardIdx, out card))
            {
                this.htmlView.Text  = Markdown.ToHtml(card.Question);
                this.htmlView.Text += Markdown.ToHtml(card.Answer);
            }
        }

        private void btnPrevQ_Click(object sender, EventArgs e)
        {
            this.currCardIdx--;
            Card card;
            if (this.currCardSet.GetCard(this.currCardIdx, out card))
            {
                Console.WriteLine("Question:" + card.Question);
                this.htmlView.Text = Markdown.ToHtml(card.Question);
            }
        }
    }
}
