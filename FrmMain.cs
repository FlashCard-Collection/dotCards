using Markdig;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DotCards
{
    public partial class FrmMain : Form
    {

        #region Private vars

        private List<CardSet> cardSetCollection;

        private CardSet currCardSet = null;

        private string flashCardRepo = string.Empty;

        private int currCardIdx = 0;

        private MarkdownPipeline pipeline;

        private static FrmMain StaticInstance = null;

        private const int WH_KEYBOARD_LL = 13;

        private const int WM_KEYDOWN = 0x0100;

        private static LowLevelKeyboardProc _proc = HookCallback;

        private static IntPtr _hookID = IntPtr.Zero;


        #endregion

        #region Static func

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        #endregion

        #region delegates

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        #endregion

        #region Konstruktor

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


            this.loadCardSets();
            this.fillTreeView();


            this.pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();

            _hookID = SetHook(_proc);
            FrmMain.StaticInstance = this;
        }

        ~FrmMain()
        {
            UnhookWindowsHookEx(_hookID);
            FrmMain.StaticInstance = null;
        }

        #endregion

        #region Private methods

        private void fillTreeView()
        {
            string repoParentDir = Directory.GetParent(this.flashCardRepo).FullName;
            string[] cardSetLst =
                this.cardSetCollection
                .Select(x => x.GetFileInfo()
                .FullName
                .Substring(
                        repoParentDir.Length + 1,
                        x.GetFileInfo().FullName.Length - repoParentDir.Length - 1))
                .OrderBy(x => x)
                .Where(x =>
                this.txtSearchText.Text == string.Empty ||
                x.ToLower().Contains(this.txtSearchText.Text.ToLower()))
                .ToArray();
            
            this.trvCardSet.Nodes.Clear();
            this.populateTreeView(this.trvCardSet, cardSetLst, '\\');
            this.trvCardSet.ExpandAll();
        }

        private void populateTreeView(TreeView treeView, IEnumerable<string> paths, char pathSeparator)
        {
            TreeNode lastNode = null;
            string subPathAgg;
            foreach (string path in paths)
            {
                subPathAgg = string.Empty;
                foreach (string subPath in path.Split(pathSeparator))
                {

                    subPathAgg += subPath + pathSeparator;
                    TreeNode[] nodes = treeView.Nodes.Find(subPathAgg, true);
                    if (nodes.Length == 0)
                        if (lastNode == null)
                            lastNode = treeView.Nodes.Add(subPathAgg, subPath);
                        else
                            lastNode = lastNode.Nodes.Add(subPathAgg, subPath);
                    else
                        lastNode = nodes[0];
                }
                lastNode.Tag =
                    (object)this.cardSetCollection
                    .Where(x => x.GetFileInfo()
                    .FullName.Contains(path))
                    .FirstOrDefault();
            }
        }

        private void loadCardSets()
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

        private void nextQuestion()
        {
            if (this.currCardSet == null)
            {
                return;
            }

            if (this.currCardIdx <= this.currCardSet.GetQuestionCount() - 1)
            {
                this.currCardIdx++;
            }

            Card card;
            if (this.currCardSet.GetCard(this.currCardIdx, out card))
            {
                Console.WriteLine("Question:" + card.Question);
                this.htmlView.Text = Markdown.ToHtml(card.Question, this.pipeline);
            }
        }

        private void prevQuestion()
        {
            if (this.currCardSet == null)
            {
                return;
            }

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

        private void showAnswer()
        {
            if (this.currCardSet == null)
            {
                return;
            }

            Card card;
            if (this.currCardSet.GetCard(this.currCardIdx, out card))
            {
                this.htmlView.Text = Markdown.ToHtml(card.Question, this.pipeline);
                this.htmlView.Text += Markdown.ToHtml(card.Answer, this.pipeline);
            }
        }

        #endregion

        #region Private static methods

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);

                var currKey = (Keys)vkCode;
                if (currKey == Keys.Right)
                {
                    FrmMain.StaticInstance.nextQuestion();
                } 
                else if (currKey == Keys.Left)
                {
                    FrmMain.StaticInstance.prevQuestion();
                }
                else if (currKey == Keys.Down)
                {
                    FrmMain.StaticInstance.showAnswer();
                }
                else if (currKey == Keys.Up)
                {
                    FrmMain.StaticInstance.nextQuestion();
                    FrmMain.StaticInstance.prevQuestion();
                }
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        #endregion

        #region Events

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (this.trvCardSet.SelectedNode == null)
            {
                return;
            }

            this.currCardIdx = 0;
            CardSet currSet = (CardSet)this.trvCardSet.SelectedNode.Tag;
            if(currSet == null)
            {
                return;
            }

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
            this.nextQuestion();
        }

        private void btnPrevQ_Click(object sender, EventArgs e)
        {
            this.prevQuestion();
        }

        private void btnShowAnswer_Click(object sender, EventArgs e)
        {
            this.showAnswer();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string batchFilePath = Path.Combine(Directory.GetParent(System.Reflection.Assembly.GetEntryAssembly().Location).FullName, "GetFlashCards.bat");
           
            ProcessStartInfo info = new ProcessStartInfo(batchFilePath);
            info.WorkingDirectory = Directory.GetParent(System.Reflection.Assembly.GetEntryAssembly().Location).FullName;
            
            Process.Start(info);
            this.loadCardSets();

            this.fillTreeView();

            this.trvCardSet.CollapseAll();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.trvCardSet.SelectedNode == null)
            {
                return;
            }

            CardSet currSet = (CardSet)this.trvCardSet.SelectedNode.Tag;

            if (currSet != null)
            {
                this.lblQuestionCountNum.Text = currSet.GetQuestionCount().ToString();
                this.lblPathStr.Text = currSet.GetFileInfo().FullName;
            }
        }

        private void txtSearchText_TextChanged(object sender, EventArgs e)
        {
            this.fillTreeView();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtSearchText.Clear();
        }

        #endregion

    }
}
