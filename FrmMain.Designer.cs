
namespace DotCards
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lstSets = new System.Windows.Forms.ListView();
            this.colSetName = new System.Windows.Forms.ColumnHeader();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbSelect = new System.Windows.Forms.TabPage();
            this.grpInfo = new System.Windows.Forms.GroupBox();
            this.lblTotalQCountNum = new System.Windows.Forms.Label();
            this.lblTotalQCount = new System.Windows.Forms.Label();
            this.lblPathStr = new System.Windows.Forms.Label();
            this.lblQuestionCountNum = new System.Windows.Forms.Label();
            this.btnSelect = new System.Windows.Forms.Button();
            this.lblPath = new System.Windows.Forms.Label();
            this.lblQestionCount = new System.Windows.Forms.Label();
            this.tbQuestions = new System.Windows.Forms.TabPage();
            this.btnShowAnswer = new System.Windows.Forms.Button();
            this.htmlView = new TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
            this.btnPrevQ = new System.Windows.Forms.Button();
            this.btnNextQ = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tbSelect.SuspendLayout();
            this.grpInfo.SuspendLayout();
            this.tbQuestions.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstSets
            // 
            this.lstSets.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstSets.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colSetName});
            this.lstSets.FullRowSelect = true;
            this.lstSets.GridLines = true;
            this.lstSets.HideSelection = false;
            this.lstSets.Location = new System.Drawing.Point(3, 3);
            this.lstSets.Name = "lstSets";
            this.lstSets.Size = new System.Drawing.Size(515, 583);
            this.lstSets.TabIndex = 0;
            this.lstSets.UseCompatibleStateImageBehavior = false;
            this.lstSets.View = System.Windows.Forms.View.Details;
            this.lstSets.SelectedIndexChanged += new System.EventHandler(this.lstSets_SelectedIndexChanged);
            // 
            // colSetName
            // 
            this.colSetName.Text = "Set name";
            this.colSetName.Width = 300;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbSelect);
            this.tabControl1.Controls.Add(this.tbQuestions);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(946, 622);
            this.tabControl1.TabIndex = 1;
            // 
            // tbSelect
            // 
            this.tbSelect.Controls.Add(this.grpInfo);
            this.tbSelect.Controls.Add(this.lstSets);
            this.tbSelect.Location = new System.Drawing.Point(4, 24);
            this.tbSelect.Name = "tbSelect";
            this.tbSelect.Padding = new System.Windows.Forms.Padding(3);
            this.tbSelect.Size = new System.Drawing.Size(938, 594);
            this.tbSelect.TabIndex = 0;
            this.tbSelect.Text = "Select";
            this.tbSelect.UseVisualStyleBackColor = true;
            // 
            // grpInfo
            // 
            this.grpInfo.Controls.Add(this.lblTotalQCountNum);
            this.grpInfo.Controls.Add(this.lblTotalQCount);
            this.grpInfo.Controls.Add(this.lblPathStr);
            this.grpInfo.Controls.Add(this.lblQuestionCountNum);
            this.grpInfo.Controls.Add(this.btnSelect);
            this.grpInfo.Controls.Add(this.lblPath);
            this.grpInfo.Controls.Add(this.lblQestionCount);
            this.grpInfo.Location = new System.Drawing.Point(524, 3);
            this.grpInfo.Name = "grpInfo";
            this.grpInfo.Size = new System.Drawing.Size(406, 241);
            this.grpInfo.TabIndex = 1;
            this.grpInfo.TabStop = false;
            this.grpInfo.Text = "Info";
            // 
            // lblTotalQCountNum
            // 
            this.lblTotalQCountNum.AutoSize = true;
            this.lblTotalQCountNum.Location = new System.Drawing.Point(127, 59);
            this.lblTotalQCountNum.Name = "lblTotalQCountNum";
            this.lblTotalQCountNum.Size = new System.Drawing.Size(89, 15);
            this.lblTotalQCountNum.TabIndex = 6;
            this.lblTotalQCountNum.Text = "Question count";
            // 
            // lblTotalQCount
            // 
            this.lblTotalQCount.AutoSize = true;
            this.lblTotalQCount.Location = new System.Drawing.Point(6, 59);
            this.lblTotalQCount.Name = "lblTotalQCount";
            this.lblTotalQCount.Size = new System.Drawing.Size(118, 15);
            this.lblTotalQCount.TabIndex = 5;
            this.lblTotalQCount.Text = "Total question count:";
            // 
            // lblPathStr
            // 
            this.lblPathStr.AutoSize = true;
            this.lblPathStr.Location = new System.Drawing.Point(127, 83);
            this.lblPathStr.Name = "lblPathStr";
            this.lblPathStr.Size = new System.Drawing.Size(45, 15);
            this.lblPathStr.TabIndex = 4;
            this.lblPathStr.Text = "PathStr";
            // 
            // lblQuestionCountNum
            // 
            this.lblQuestionCountNum.AutoSize = true;
            this.lblQuestionCountNum.Location = new System.Drawing.Point(127, 34);
            this.lblQuestionCountNum.Name = "lblQuestionCountNum";
            this.lblQuestionCountNum.Size = new System.Drawing.Size(89, 15);
            this.lblQuestionCountNum.TabIndex = 3;
            this.lblQuestionCountNum.Text = "Question count";
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(276, 188);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(124, 47);
            this.btnSelect.TabIndex = 2;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Location = new System.Drawing.Point(90, 83);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(34, 15);
            this.lblPath.TabIndex = 1;
            this.lblPath.Text = "Path:";
            // 
            // lblQestionCount
            // 
            this.lblQestionCount.AutoSize = true;
            this.lblQestionCount.Location = new System.Drawing.Point(32, 34);
            this.lblQestionCount.Name = "lblQestionCount";
            this.lblQestionCount.Size = new System.Drawing.Size(92, 15);
            this.lblQestionCount.TabIndex = 0;
            this.lblQestionCount.Text = "Question count:";
            // 
            // tbQuestions
            // 
            this.tbQuestions.Controls.Add(this.btnShowAnswer);
            this.tbQuestions.Controls.Add(this.htmlView);
            this.tbQuestions.Controls.Add(this.btnPrevQ);
            this.tbQuestions.Controls.Add(this.btnNextQ);
            this.tbQuestions.Location = new System.Drawing.Point(4, 24);
            this.tbQuestions.Name = "tbQuestions";
            this.tbQuestions.Padding = new System.Windows.Forms.Padding(3);
            this.tbQuestions.Size = new System.Drawing.Size(938, 594);
            this.tbQuestions.TabIndex = 1;
            this.tbQuestions.Text = "Questions";
            this.tbQuestions.UseVisualStyleBackColor = true;
            // 
            // btnShowAnswer
            // 
            this.btnShowAnswer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowAnswer.Location = new System.Drawing.Point(421, 563);
            this.btnShowAnswer.Name = "btnShowAnswer";
            this.btnShowAnswer.Size = new System.Drawing.Size(75, 23);
            this.btnShowAnswer.TabIndex = 6;
            this.btnShowAnswer.Text = "Show";
            this.btnShowAnswer.UseVisualStyleBackColor = true;
            this.btnShowAnswer.Click += new System.EventHandler(this.btnShowAnswer_Click);
            // 
            // htmlView
            // 
            this.htmlView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.htmlView.AutoScroll = true;
            this.htmlView.AutoScrollMinSize = new System.Drawing.Size(922, 20);
            this.htmlView.BackColor = System.Drawing.Color.White;
            this.htmlView.BaseStylesheet = null;
            this.htmlView.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.htmlView.Location = new System.Drawing.Point(8, 3);
            this.htmlView.Name = "htmlView";
            this.htmlView.Size = new System.Drawing.Size(922, 554);
            this.htmlView.TabIndex = 5;
            this.htmlView.Text = "htmlPanel1";
            // 
            // btnPrevQ
            // 
            this.btnPrevQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrevQ.Location = new System.Drawing.Point(8, 563);
            this.btnPrevQ.Name = "btnPrevQ";
            this.btnPrevQ.Size = new System.Drawing.Size(75, 23);
            this.btnPrevQ.TabIndex = 4;
            this.btnPrevQ.Text = "Prev";
            this.btnPrevQ.UseVisualStyleBackColor = true;
            this.btnPrevQ.Click += new System.EventHandler(this.btnPrevQ_Click);
            // 
            // btnNextQ
            // 
            this.btnNextQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNextQ.Location = new System.Drawing.Point(855, 563);
            this.btnNextQ.Name = "btnNextQ";
            this.btnNextQ.Size = new System.Drawing.Size(75, 23);
            this.btnNextQ.TabIndex = 3;
            this.btnNextQ.Text = "Next";
            this.btnNextQ.UseVisualStyleBackColor = true;
            this.btnNextQ.Click += new System.EventHandler(this.btnNextQ_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 622);
            this.Controls.Add(this.tabControl1);
            this.Name = "FrmMain";
            this.Text = "DotCards";
            this.tabControl1.ResumeLayout(false);
            this.tbSelect.ResumeLayout(false);
            this.grpInfo.ResumeLayout(false);
            this.grpInfo.PerformLayout();
            this.tbQuestions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstSets;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbSelect;
        private System.Windows.Forms.TabPage tbQuestions;
        private System.Windows.Forms.GroupBox grpInfo;
        private System.Windows.Forms.Label lblQestionCount;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Label lblPathStr;
        private System.Windows.Forms.Label lblQuestionCountNum;
        private System.Windows.Forms.ColumnHeader colSetName;
        private System.Windows.Forms.Button btnPrevQ;
        private System.Windows.Forms.Button btnNextQ;
        private TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel htmlView;
        private System.Windows.Forms.Label lblTotalQCountNum;
        private System.Windows.Forms.Label lblTotalQCount;
        private System.Windows.Forms.Button btnShowAnswer;
    }
}

