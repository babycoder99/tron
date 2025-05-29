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
using Newtonsoft.Json;


namespace TronUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeAsync();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private async void InitializeAsync()
        {
            try
            {
                await Editor.EnsureCoreWebView2Async(null);
                Editor.CoreWebView2.Navigate(new Uri($"file:///{Directory.GetCurrentDirectory()}/MonacoCzk/index.html").ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing WebView2: {ex.Message}", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void guna2Button5_Click(object sender, EventArgs e)
        {
         
        }


        private async void guna2Button7_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Lua Files (*.lua)|*.lua|Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                    DefaultExt = "lua",
                    Title = "Save Lua or Text File"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Run the JavaScript function `GetText()` from Monaco Editor
                    string editorContent = await Editor.ExecuteScriptAsync("GetText();");

                    // Monaco/WebView2 returns a JSON-escaped string, so we unescape it
                    string cleanedText = JsonConvert.DeserializeObject<string>(editorContent);

                    File.WriteAllText(saveFileDialog.FileName, cleanedText);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async void guna2Button8_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Txt Files (*.txt)|*.txt|Lua Files (*.lua)|*.lua|All Files (*.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string script = File.ReadAllText(dialog.FileName);

                // Escape backslashes and backticks for safe JS string
                string escapedScript = script
                    .Replace("\\", "\\\\")
                    .Replace("`", "\\`");

                await Editor.CoreWebView2.ExecuteScriptAsync($"editor.setValue(`{escapedScript}`);");
            }
        }

        private async void guna2Button6_Click(object sender, EventArgs e)
        {
            await Editor.ExecuteScriptAsync("SetText(``);");
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
          Application.Exit();   
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
