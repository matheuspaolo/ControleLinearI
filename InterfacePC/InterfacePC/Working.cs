using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InterfacePC
{
    public partial class Working : Form
    {
        public Working()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void Working_Load(object sender, EventArgs e)
        {
            await Task.Delay(2000);
            CarregarGIF();
            
        }

        private async void CarregarGIF()
        {
            for (int i = 0; i < Screen.PrimaryScreen.Bounds.Height; i++)
            {
                this.Location = new Point(20, i);
                await Task.Delay(3);
                Console.WriteLine(this.Location);
            }

            pictureBox1.Image = Properties.Resources.gif2;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Size = new Size(148, 148);
            pictureBox1.Size = new Size(150, 150);

            for (int i = 0; i < Screen.PrimaryScreen.Bounds.Width; i++)
            {
                this.Location = new Point(i, 10);
                await Task.Delay(3);
                Console.WriteLine(this.Location);
            }

            pictureBox1.Image = Properties.Resources.gif5;
            this.Size = new Size(160, 147);
            pictureBox1.Size = new Size(160, 150);

            for (int i = 0; i < Screen.PrimaryScreen.Bounds.Height; i++)
            {
                this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - 200, i);
                await Task.Delay(3);
                Console.WriteLine(this.Location);
            }

            pictureBox1.Image = Properties.Resources.gif3;
            this.Size = new Size(247, 178);
            pictureBox1.Size = new Size(250, 181);

            for (int i = Screen.PrimaryScreen.Bounds.Width; i > 0; i--)
            {
                this.Location = new Point(i,Screen.PrimaryScreen.Bounds.Height - 200);
                await Task.Delay(3);
                Console.WriteLine(this.Location);
            }

        }
    }
}
