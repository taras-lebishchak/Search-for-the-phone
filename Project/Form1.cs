using System;
using System.Diagnostics;
using System.Windows.Forms;
using xNet;
using Fizzler.Systems.HtmlAgilityPack;
using System.Linq;

namespace Project
{
    public partial class Form1 : Form
    {
        public Process p = new Process();
        public Form1()
        {
            InitializeComponent();
            {
            }
        }
        void Pars(string list, int Max_str)
        {
            string listing = "", NamePhone = "", prise = "", availability = "", characteristic = "";
            var danni = new HttpRequest();

            for (int j = 1; j < Max_str + 1; j++)
            {
                string response = danni.Get(list + j + "/").ToString();
                HtmlAgilityPack.HtmlDocument hap = new HtmlAgilityPack.HtmlDocument();
                hap.LoadHtml(response);
                string name = textBox2.Text.Trim();

              

                foreach (var item in hap.DocumentNode.QuerySelectorAll("div.categorypage__products__itemwrap"))
                {

                    try
                    {
                        
                        NamePhone = item.QuerySelector("div.itemcard__name").InnerText.Replace("<!---->", "");
                        if (NamePhone.Contains(name))
                        {
                            characteristic = item.QuerySelector("div.short-description").InnerText;
                            prise = item.QuerySelector("span.itemcard__price__new").InnerText;
                            availability = item.QuerySelector("div.itemcard__availability").InnerText;
                            listing = item.QuerySelector("div.itemcard__img.itemcard__img1>a").GetAttributeValue("href", null);
                            richTextBox1.AppendText(
                            "    " + NamePhone + "\n" + "\n" + "    Характеристика : " + characteristic + "\n"  + "\n" +
                            "    Ціна : " + prise + "\n"+ "    " + availability + "\n" + "\n" + "    Посилання : "+ "https://kvshop.com.ua" + listing + "\n" +
                            "_________________________________________________________________________________" + "\n" + "\n");
                        }
                    }
                    catch { }
                }
            }
            this.richTextBox1.LinkClicked += new LinkClickedEventHandler(this.richTextBox1_LinkClicked);
        }
        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            p = Process.Start("opera.exe", e.LinkText);
        }
        private void button1_Click(object sender, EventArgs e)
        {

            Pars(textBox1.Text, Convert.ToInt32(textBox3.Text));

        }
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                textBox2.Text = "";
        }

    }
}

