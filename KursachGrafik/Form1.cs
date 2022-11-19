using System;
using System.Drawing;
using System.Windows.Forms;

namespace KursachGrafik
{
    public partial class Form1 : Form
    {
        private int pxDist = 10; //Дистанция между двумя чёрточками в пикселях
        private double[] m = new double[5]; //коэф. 1
        private double[] n = new double[5]; //коэф. 2


        private readonly PointF OCentre;

        private void InitMN()
        {
            double.TryParse(this.textBox1.Text, out this.m[0]);
            double.TryParse(this.textBox2.Text, out this.n[0]);
            double.TryParse(this.textBox3.Text, out this.n[1]);
            double.TryParse(this.textBox4.Text, out this.m[1]);
            double.TryParse(this.textBox5.Text, out this.n[2]);
            double.TryParse(this.textBox6.Text, out this.m[2]);
            double.TryParse(this.textBox7.Text, out this.n[3]);
            double.TryParse(this.textBox8.Text, out this.m[3]);
            double.TryParse(this.textBox9.Text, out this.n[4]);
            double.TryParse(this.textBox10.Text, out this.m[4]);
        }

        public Form1()
        {
            InitializeComponent();

            this.InitMN();
            this.OCentre = new PointF(this.pictureBox1.Width / 2, this.pictureBox1.Height / 2);


        }

        private void button1_Click(object sender, EventArgs e)
        {
            return;
            Graphics graphics = this.pictureBox1.CreateGraphics();

            Pen pen = new Pen(Color.Black, 2.5f);

            PointF[] points = new PointF[1001];



            graphics.DrawLines(pen, points);

        }

        private void PrintBorderAndAxis(Graphics g)
        {
            Graphics Oxy = g;
            Oxy.Clear(this.pictureBox1.BackColor);
            
            //Рисование границ графической части
            Point borders1 = new Point(0, 0);
            Point borders2 = new Point(this.pictureBox1.Size.Width - 1, this.pictureBox1.Size.Height - 1);
            Size sizef = new Size(borders2);
            Rectangle border = new Rectangle(borders1, sizef);
            Oxy.DrawRectangle(Pens.Black, border);

            //Оси
            Point Ox1 = new Point(0, this.pictureBox1.Size.Height / 2);
            Point Ox2 = new Point(this.pictureBox1.Size.Width, this.pictureBox1.Size.Height / 2);
            Point Oy1 = new Point(this.pictureBox1.Size.Width / 2, 0);
            Point Oy2 = new Point(this.pictureBox1.Size.Width / 2, this.pictureBox1.Size.Height);
            Oxy.DrawLine(Pens.Black, Ox1, Ox2);
            Oxy.DrawLine(Pens.Black, Oy1, Oy2);
            
            //Названия осей
            Font font = new Font("Consolas", 8, FontStyle.Bold);
            PointF locationX = new PointF(this.pictureBox1.Size.Width - 10, this.pictureBox1.Height / 2);
            PointF locationY = new PointF(this.pictureBox1.Size.Width / 2, 0);
            SizeF sizeF = new SizeF(10, 10);
            Oxy.DrawString("X", font, Brushes.Black, new RectangleF(locationX, sizeF));
            Oxy.DrawString("Y", font, Brushes.Black, new RectangleF(locationY, sizeF));

            for (double i = 0; i < this.pictureBox1.Width / this.pxDist; i++)
            {
                PointF OyDiv1 = new PointF((float)i * this.pxDist, this.pictureBox1.Size.Height / 2 - 2),
                       OyDiv2 = new PointF((float)i * this.pxDist, this.pictureBox1.Size.Height / 2 + 2);
                Oxy.DrawLine(Pens.Black, OyDiv1, OyDiv2);

                PointF OxDiv1 = new PointF(this.pictureBox1.Size.Width / 2 - 2, (float)i * this.pxDist),
                       OxDiv2 = new PointF(this.pictureBox1.Size.Width / 2 + 2, (float)i * this.pxDist);
                Oxy.DrawLine(Pens.Black, OxDiv1, OxDiv2);
            }

            int fontSize = new int();
            if (this.pxDist == 5)
                fontSize = 6;
            else if (this.pxDist == 10)
                fontSize = 7;
            else if (this.pxDist == 20)
                fontSize = 8;

            Font numFont = new Font("Consolas", fontSize, FontStyle.Regular);
            for (int i = 1; i <= 6; i++)
            {
                Oxy.DrawString((i * 5).ToString(), numFont, Brushes.Black, new PointF(this.OCentre.X + (i * 5) * this.pxDist - 5, this.OCentre.Y + 2));
                Oxy.DrawString((-i * 5).ToString(), numFont, Brushes.Black, new PointF(this.OCentre.X - (i * 5) * this.pxDist - 5, this.OCentre.Y + 2));
                
                Oxy.DrawString((i * 5).ToString(), numFont, Brushes.Black, new PointF(this.OCentre.X + 2, this.OCentre.Y - (i * 5) * this.pxDist - 5));
                Oxy.DrawString((-i * 5).ToString(), numFont, Brushes.Black, new PointF(this.OCentre.X + 2, this.OCentre.Y + (i * 5) * this.pxDist - 5));
            }

        }
        
        private void CheckPoint(ref PointF Y)
        {
            if (Y.Y > this.pictureBox1.Height)
                Y.Y = this.pictureBox1.Height;
            if (Y.Y < 0)
                Y.Y = -1;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            this.PrintBorderAndAxis(e.Graphics);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int k = 100;
            switch (this.trackBar1.Value)
            {
                case 0:
                    k = 50;
                    this.pxDist = 5;
                    break;

                case 1:
                    k = 100;
                    this.pxDist = 10;
                    break;

                case 2:
                    k = 200;
                    this.pxDist = 20;
                    break;

                default:
                    break;
            }
            this.label3.Text = k.ToString() + "%";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.PrintBorderAndAxis(this.pictureBox1.CreateGraphics());
        }

        private void button3_Click(object sender, EventArgs e) //sin
        {
            this.textBox1.Text = this.m[0].ToString();
            this.textBox2.Text = this.n[0].ToString();

            Pen pen = new Pen(Color.Blue, (float)this.pxDist / 10);
            PointF[] points = new PointF[this.pictureBox1.Width];
            //i - 170
            for (int i = 0; i < points.Length; i++)
            {
                var a = (i - this.OCentre.X) * this.n[0] / -this.pxDist;
                var b = this.m[0] * pxDist;
                points[i] = new PointF(i, (float)Math.Sin(a) * (float)b + this.OCentre.Y);

                this.CheckPoint(ref points[(int)i]);
            }

            var g = this.pictureBox1.CreateGraphics();
            g.DrawLines(pen, points);
        }

        private void button4_Click(object sender, EventArgs e) //x^2
        {
            this.textBox4.Text = this.m[1].ToString();
            this.textBox3.Text = this.n[1].ToString();

            Pen pen = new Pen(Color.Purple, (float)this.pxDist / 10);
            PointF[] points = new PointF[this.pictureBox1.Width];

            for (int i = 0; i < points.Length; i++)
            {
                var a = (i - this.OCentre.X) / Math.PI;
                points[i] = new PointF(i, this.OCentre.Y - (float)(Math.Pow(a, 2) + this.m[1] * a + this.n[1] * this.pxDist));

                this.CheckPoint(ref points[i]);
            }

            var g = this.pictureBox1.CreateGraphics();
            g.DrawLines(pen, points);
        }

        private void button5_Click(object sender, EventArgs e) //ln
        {
            this.textBox6.Text = this.m[2].ToString();
            this.textBox5.Text = this.n[2].ToString();

            Pen pen = new Pen(Color.DarkCyan, (float)this.pxDist / 10);
            PointF[] points = new PointF[99 + 36];

            for (double i = 0; i < 100; i++) //(0;1)
            {
                var b = (float)(Math.Log(i / (10 * this.pxDist)) * -this.pxDist * this.m[2] * this.n[2]);
                points[(int)i] = new PointF((float)i / this.pxDist + this.OCentre.X, this.OCentre.Y + b);

                this.CheckPoint(ref points[(int)i]);
            }
            for (double i = 0; i < points.Length - 99; i++) //[1;+inf)
            {
                var l = (float)(Math.Log(i + 1) * this.pxDist * this.m[2] * this.n[2]);
                points[(int)i + 99] = new PointF((float)i * this.pxDist + this.OCentre.X + this.pxDist, this.OCentre.Y - l);
                
                this.CheckPoint(ref points[(int)i]);
            }

            var g = this.pictureBox1.CreateGraphics();
            g.DrawLines(pen, points);
        }

        private void button6_Click(object sender, EventArgs e) //e^x
        {
            this.textBox8.Text = this.m[3].ToString();
            this.textBox7.Text = this.n[3].ToString();

            Pen pen = new Pen(Color.Red, (float)this.pxDist / 10);
            PointF[] points = new PointF[this.pictureBox1.Width];

            for (int i = 0; i < points.Length; i++)
            {
                var x = (i - this.OCentre.X) / this.pxDist * this.n[3];
                points[i] = new PointF(i, this.OCentre.Y - (float)this.m[3] * this.pxDist - (float)Math.Pow(Math.E, x) * this.pxDist);
                
                this.CheckPoint(ref points[i]);
            }

            var g = this.pictureBox1.CreateGraphics();
            g.DrawLines(pen, points);
        }

        private void button7_Click(object sender, EventArgs e) ///\
        {
            this.textBox10.Text = this.m[4].ToString();
            this.textBox9.Text = this.n[4].ToString();

            Pen pen = new Pen(Color.Green, (float)this.pxDist / 10);
            PointF[] points = new PointF[(int)(this.m[4] * 2 * this.pxDist) + 1];

            var p = (int)(this.m[4] * this.pxDist);

            for (double i = 0 - p; i <= p; i++)
            {
                var a = this.m[4] * this.m[4] - i / this.pxDist * i / this.pxDist;
                var b = Math.Sqrt(a);
                points[(int)(i + p)] = new PointF(this.OCentre.X + (float)i + (float)this.n[4] * this.pxDist, this.OCentre.Y - (float)b * this.pxDist);

                this.CheckPoint(ref points[(int)(i + p)]);
            }

            var g = this.pictureBox1.CreateGraphics();
            g.DrawLines(pen, points);
        }

        /*
        ░░░░▄▄▄▄▀▀▀▀▀▀▀▀▄▄▄▄▄▄
        ░░░░█░░░░▒▒▒▒▒▒▒▒▒▒▒▒░░▀▀▄
        ░░░█░░░▒▒▒▒▒▒░░░░░░░░▒▒▒░░█
        ░░█░░░░░░▄██▀▄▄░░░░░▄▄▄░░░█
        ░▀▒▄▄▄▒░█▀▀▀▀▄▄█░░░██▄▄█░░░█
        █▒█▒▄░▀▄▄▄▀░░░░░░░░█░░░▒▒▒▒▒█
        █▒█░█▀▄▄░░░░░█▀░░░░▀▄░░▄▀▀▀▄▒█
        ░█▀▄░█▄░█▀▄▄░▀░▀▀░▄▄▀░░░░█░░█
        ░░█░░▀▄▀█▄▄░█▀▀▀▄▄▄▄▀▀█▀██░█
        ░░░█░░██░░▀█▄▄▄█▄▄█▄████░█
        ░░░░█░░░▀▀▄░█░░░█░███████░█
        ░░░░░▀▄░░░▀▀▄▄▄█▄█▄█▄█▄▀░░█
        ░░░░░░░▀▄▄░▒▒▒▒░░░░░░░░░░█
        ░░░░░░░░░░▀▀▄▄░▒▒▒▒▒▒▒▒▒▒░█
        ░░░░░░░░░░░░░░▀▄▄▄▄▄░░░░░█
         */
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            double.TryParse(this.textBox1.Text, out this.m[0]);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            double.TryParse(this.textBox2.Text, out this.n[0]);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            double.TryParse(this.textBox4.Text, out this.m[1]);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            double.TryParse(this.textBox3.Text, out this.n[1]);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            double.TryParse(this.textBox6.Text, out this.m[2]);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            double.TryParse(this.textBox5.Text, out this.n[2]);
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            double.TryParse(this.textBox8.Text, out this.m[3]);
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            double.TryParse(this.textBox7.Text, out this.n[3]);
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            double.TryParse(this.textBox10.Text, out this.m[4]);
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            double.TryParse(this.textBox9.Text, out this.n[4]);
        }
    }
}
