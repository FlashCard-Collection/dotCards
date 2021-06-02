using Markdig;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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

        private MarkdownPipeline pipeline;

        public FrmMain()
        {
            InitializeComponent();

            this.htmlView.Text = "";

            this.flashCardRepo = Path.Combine(Environment.CurrentDirectory, "FlashCards");
            this.lblQuestionCountNum.Text = string.Empty;
            this.lblPathStr.Text = string.Empty;

            this.btnNextQ.Click += btnNextQ_Click;
            this.btnPrevQ.Click += btnPrevQ_Click;
            this.btnShowAnswer.Click += btnShowAnswer_Click;

            treeView1.PathSeparator = @"\";

            this.loadCardSets();
            this.fillListView();

            this.fillTreeView();

            this.pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();

           
        }

        private void fillTreeView()
        {
            char pathSeparator = '\\';
            TreeNode lastNode = null;
            string subPathAgg;
            foreach (CardSet set in this.cardSetCollection)
            {

                string path = set.GetFileInfo().FullName.Substring(
                        this.flashCardRepo.Length + 1,
                        set.GetFileInfo().FullName.Length - this.flashCardRepo.Length - 1);

                subPathAgg = string.Empty;
                foreach (string subPath in path.Split(pathSeparator))
                {
                    subPathAgg += subPath + pathSeparator;
                    TreeNode[] nodes = this.treeView1.Nodes.Find(subPathAgg, true);
                    if (nodes.Length == 0)
                        if (lastNode == null)
                            lastNode = this.treeView1.Nodes.Add(subPathAgg, subPath);
                        else
                            lastNode = lastNode.Nodes.Add(subPathAgg, subPath);
                    else
                        lastNode = nodes[0];
                }
            }
        }

        private void fillListView()
        {
            this.lstSets.Items.Clear();
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

            // Sort card set
            this.cardSetCollection.OrderBy(x =>
            {
                return x.GetFileInfo().FullName.Substring(
                        this.flashCardRepo.Length + 1,
                        x.GetFileInfo().FullName.Length - this.flashCardRepo.Length - 1);
            });
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

            this.currCardIdx = 0;
            CardSet currSet = (CardSet)this.lstSets.SelectedItems[0].Tag;
            Directory.SetCurrentDirectory(currSet.GetFileInfo().Directory.FullName);
            this.currCardSet = currSet;
            this.tabControl1.SelectedTab = this.tbQuestions;

            Card card;
            if (this.currCardSet.GetCard(this.currCardIdx, out card))
            {
                Console.WriteLine("Question:" + card.Question);
                this.htmlView.Text = Markdown.ToHtml(card.Question, this.pipeline);
            }
        }

        private void btnNextQ_Click(object sender, EventArgs e)
        {
            if(this.currCardIdx <= this.currCardSet.GetQuestionCount() - 1)
            {
                this.currCardIdx++;
            }

            Card card;
            if(this.currCardSet.GetCard(this.currCardIdx, out card))
            {
                Console.WriteLine("Question:" + card.Question);
                this.htmlView.Text = Markdown.ToHtml(card.Question, this.pipeline);
            }
        }

        private void btnPrevQ_Click(object sender, EventArgs e)
        {
            if (this.currCardIdx > 0)
            {
                this.currCardIdx--;
            }

            Card card;
            if (this.currCardSet.GetCard(this.currCardIdx, out card))
            {
                this.htmlView.Text = Markdown.ToHtml(card.Question, this.pipeline);
            }
        }

        private void btnShowAnswer_Click(object sender, EventArgs e)
        {
            if(this.currCardSet == null)
            {
                return;
            }

            Card card;
            if (this.currCardSet.GetCard(this.currCardIdx, out card))
            {
                this.htmlView.Text  = Markdown.ToHtml(card.Question, this.pipeline);
                this.htmlView.Text += Markdown.ToHtml(card.Answer, this.pipeline);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string batchFilePath = Path.Combine(Directory.GetParent(System.Reflection.Assembly.GetEntryAssembly().Location).FullName, "GetFlashCards.bat");
           
            ProcessStartInfo info = new ProcessStartInfo(batchFilePath);
            info.WorkingDirectory = Directory.GetParent(System.Reflection.Assembly.GetEntryAssembly().Location).FullName;
            
            Process.Start(info);
            this.loadCardSets();
            this.fillListView();
        }

        
    }
}
