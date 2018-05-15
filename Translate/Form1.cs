using System;
using System.Windows.Forms;

namespace Translate
{
    public partial class Form1 : Form
    {
        string inputText = string.Empty;
        string transText = string.Empty;
        string clipboardText = string.Empty;

        public Form1()
        {
            InitializeComponent();
            webBrowser1.Url = new Uri("https://fanyi.youdao.com/");
            toolStripStatusLabel1.Text = "就绪";
        }

        private void 开始ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartRealTranslate();
        }

        private void StartRealTranslate()
        {
            Clipboard.Clear();
            inputText = string.Empty;
            transText = string.Empty;
            clipboardText = string.Empty;
            clipboardTimer.Start();
            HtmlDocument document = webBrowser1.Document;
            toolStripStatusLabel1.Text = "实时翻译已开启";
            HtmlElement inputElement = document.GetElementById("inputOriginal");
            inputElement.Focus();
        }

        private void clipboardTimer_Tick(object sender, EventArgs e)
        {
            clipboardText = Clipboard.GetText();
            HtmlDocument document = webBrowser1.Document;
            if (clipboardText != inputText && clipboardText != transText)
            {//剪切板文本与输入文本不同
                HtmlElement inputElement = document.GetElementById("inputOriginal");

                inputElement.InnerText = clipboardText;
                inputText = clipboardText;
                toolStripStatusLabel1.Text = "获取输入";
            }
            else if(transText != clipboardText)
            {
                toolStripStatusLabel1.Text = "获取翻译";
                HtmlElement transTargetElement = document.GetElementById("transTarget");
                //transText = transTargetElement.InnerText;
                if(transTargetElement.InnerText != null&&transText!= transTargetElement.InnerText)
                {
                    transText = transTargetElement.InnerText;
                    Clipboard.SetText(transText);
                    clipboardText = transText;
                    richTextBox1.Text = transText;
                    toolStripStatusLabel1.Text = "翻译成功";
                }
            }
        }

        private void 停止ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clipboardTimer.Stop();
            toolStripStatusLabel1.Text = "实时翻译已停止";
        }

        private void 置顶ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TopMost = !this.TopMost;
            toolStripStatusLabel1.Text = "窗口置顶:" + this.TopMost.ToString();
            
        }

        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            开始ToolStripMenuItem.Enabled = true;
            StartRealTranslate();
        }
    }
}
