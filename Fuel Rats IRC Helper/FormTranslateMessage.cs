using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fuel_Rats_IRC_Helper
{
    public partial class FormTranslateMessage : Form
    {
        public static FormTranslateMessage formTranslateMessage { get; private set; }

        public FormTranslateMessage()
        {
            InitializeComponent();
        }

        internal static void TranslateStatic(string sender, string text)
        {
            if (formTranslateMessage == null)
                formTranslateMessage = new FormTranslateMessage();

            try
            {
                formTranslateMessage.Translate(sender, text);
                formTranslateMessage.Show();
            }
            catch
            {
                formTranslateMessage.Dispose();
                formTranslateMessage = null;
                TranslateStatic(sender, text);
            }
        }

        private void Translate(string sender, string text)
        {
            List<string> d = new List<string>();
            d.Add(sender);
            d.Add(text);

            if (text == null || string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text)) return;

            var toLanguage = "en";//English
            var fromLanguage = "auto";//Deutsch
            var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={fromLanguage}&tl={toLanguage}&dt=t&q={System.Web.HttpUtility.UrlEncode(text)}";
            using (var httpClient = new HttpClient())
            {
                var result = httpClient.GetStringAsync(url).Result;
                try
                {
                    string fix = result.Substring(4, result.IndexOf("\"", 4, StringComparison.Ordinal) - 4);
                    d.Add(fix);
                }
                catch
                {
                    d.Add("error");
                }
            }

            dataGridViewTranslatedMessage.Rows.Add(d.ToArray());
        }
    }
}
