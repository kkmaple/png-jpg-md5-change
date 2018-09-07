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

namespace 图片变装器
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            textBox1.Text = folderBrowserDialog1.SelectedPath;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            folderBrowserDialog2.ShowDialog();
            textBox2.Text = folderBrowserDialog2.SelectedPath;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DoTheJob(textBox1.Text);
        }

        private void DoTheJob(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);

            FileInfo[] fil = dir.GetFiles();

            DirectoryInfo[] dii = dir.GetDirectories();

            foreach (FileInfo f in fil)

            {
                if(f.Extension == ".png" || f.Extension == ".jpg")
                {
                    byte[] b1 = null;
                    using (FileStream fs = f.OpenRead())
                    {
                        byte[] b2 = new byte[fs.Length];
                        ASCIIEncoding temp = new ASCIIEncoding();

                        while (fs.Read(b2, 0, b2.Length) > 0)
                        {
                            b1 = 合并Byte数组(b1, b2);
                        }
                    }

                    // Open the stream for writing.
                    using (FileStream fs = f.OpenWrite())
                    {
                        byte[] info = 合并Byte数组(b1, System.BitConverter.GetBytes(new Random().Next(9)));
                        // Add some information to the file.
                        fs.Write(info, 0, info.Length);
                    }

                }
            }

            //获取子文件夹内的文件列表，递归遍历

            foreach (DirectoryInfo d in dii)

            {
                DoTheJob(d.FullName);
            }
        }

        private byte[] 合并Byte数组(byte[] b1,byte[] b2)
        {
            if (b1 == null)
                return b2;
            byte[] ret = new byte[b1.Length + b2.Length];
            b1.CopyTo(ret, 0);
            b2.CopyTo(ret, b1.Length);
            return ret;
        }
    }
}
