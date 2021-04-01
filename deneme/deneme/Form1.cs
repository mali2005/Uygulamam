using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using MediaToolkit.Model;
using MediaToolkit;
using VideoLibrary;

namespace deneme
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int farkx, farky;
        private void Form1_Load(object sender, EventArgs e)
        {
            this.TransparencyKey = this.BackColor;
            comboBox1.Items.Add(".mp3");
            comboBox1.Items.Add(".mp4");
            comboBox1.Items.Add(".wav");
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Location = new Point(Cursor.Position.X - farkx, Cursor.Position.Y - farky);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            farkx = Cursor.Position.X - this.Location.X;
            farky = Cursor.Position.Y - this.Location.Y;
            timer1.Start();
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            timer1.Stop();
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            farkx = Cursor.Position.X - this.Location.X;
            farky = Cursor.Position.Y - this.Location.Y;
            timer1.Start();
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            timer1.Stop();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog folderDlg = new FolderBrowserDialog();
                folderDlg.ShowNewFolderButton = true;
                // Show the FolderBrowserDialog.  
                DialogResult result = folderDlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    WebClient web = new WebClient();
                    string url = web.DownloadString("https://www.google.com/search?q=" + textBox1.Text.Trim().Replace(' ', '+'));
                    int a = url.IndexOf("watch?v=");
                    string kod = "https://www.youtube.com/watch?v=" + url.Substring(a + 8, 11);

                    YouTube y = new YouTube();
                    Video vid = y.GetVideo(kod);

                    if (comboBox1.Text == ".mp4")
                    {
                        System.IO.File.WriteAllBytes(folderDlg.SelectedPath + "//" + vid.FullName, vid.GetBytes());
                    }
                    else
                    {
                        System.IO.File.WriteAllBytes(folderDlg.SelectedPath + "//" + vid.FullName, vid.GetBytes());
                        var inputFile = new MediaFile { Filename = folderDlg.SelectedPath + "//" + vid.FullName };
                        var outputFile = new MediaFile { Filename = $"{folderDlg.SelectedPath + "//" + vid.FullName + comboBox1.Text}" };

                        using (var engine = new Engine())
                        {
                            engine.GetMetadata(inputFile);
                            engine.Convert(inputFile, outputFile);
                        }
                        System.IO.File.Delete(folderDlg.SelectedPath + "//" + vid.FullName);
                    }
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Girdiğinizin bir şarkı olduğundan emin olun.","HATA",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {

        }
    }
}
