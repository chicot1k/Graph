using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace polynom
{
    public partial class Form1 : Form
    {
        float polynom(float x, float[] k)
        {
            float result=0;
            for(int i = 0; i < k.Length; i++)
            {
                result += k[i] * (float)Math.Pow(x, k.Length - 1 - i);
            }
            return result;
        }

        
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                return;
            float[] k;
            float a, b;
            k = textBox1.Text.Split(' ').Select(float.Parse).ToArray();
            float max_cord_x=10, min_cord_x=-10, max_cord_y=10, min_cord_y=-10, step_cord_x=2, step_cord_y=2;
            
            b = float.Parse(textBox3.Text);
            a = float.Parse(textBox2.Text);
            if (a > b)
            {
                float temp = a;
                a = b;
                b = temp;
            }
               
            while (a < min_cord_x || b > max_cord_x)
            {
                max_cord_x *= 2;
                min_cord_x *= 2;
                step_cord_x *= 2;
               
            }
            while(a>min_cord_x+4*step_cord_x && b < max_cord_x - 4*step_cord_x)
            {
                max_cord_x /= 2;
                min_cord_x /= 2;
                step_cord_x /= 2;

            }
            Bitmap picture = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics de = Graphics.FromImage(picture);
            pictureBox1.Image = picture;
            Pen ruchka = new Pen(Color.Black, 1f);
            de.DrawLine(ruchka, 0, pictureBox1.Height / 2, pictureBox1.Width, pictureBox1.Height / 2);
            de.DrawLine(ruchka, pictureBox1.Width / 2, 0, pictureBox1.Width / 2, pictureBox1.Height);
            var shrift = new Font(Font.FontFamily, 8);
            for (int i=0; i<11; i++)
            {
                if (i != 5)
                {
                    de.DrawLine(ruchka, pictureBox1.Width / 10 * i, pictureBox1.Height / 2 - 3, pictureBox1.Width / 10 * i, pictureBox1.Height / 2 + 3);
                    float x = min_cord_x + i * step_cord_x;
                    de.DrawString(x.ToString(), shrift, Brushes.Black, pictureBox1.Width / 10 * i, pictureBox1.Height / 2 + 10);
                }
            }
            de.DrawString(max_cord_x.ToString(), shrift, Brushes.Black, pictureBox1.Width-20, pictureBox1.Height / 2 +10);
            float y_min = 10000000000000, y_max = -9999999999999999 ;
            float step_x = (max_cord_x - min_cord_x) / pictureBox1.Width;
            for (int i=0; i < pictureBox1.Width; i++)
            {
                if (polynom(min_cord_x+i * step_x, k) > y_max)
                {
                    y_max = polynom(min_cord_x + i * step_x, k);
                }
                if (polynom(min_cord_x+i * step_x, k) < y_min)
                {
                    y_min = polynom(min_cord_x + i * step_x, k);
                }

            }
            while (y_min < min_cord_y || y_max > max_cord_y)
            {
                max_cord_y *= 2;
                min_cord_y *= 2;
                step_cord_y *= 2;

            }
            while (y_min > min_cord_y + 4 * step_cord_y && y_max < max_cord_y - 4 * step_cord_y)
            {
                max_cord_y /= 2;
                min_cord_y /= 2;
                step_cord_y /= 2;

            }
            
            for (int i = 0; i < 11; i++)
            {
                if (i != 5)
                {
                    de.DrawLine(ruchka, pictureBox1.Width / 2 - 3, pictureBox1.Height / 10*i, pictureBox1.Width / 2 + 3, pictureBox1.Height / 10*i);
                    float y = (min_cord_y + i * step_cord_y)*-1;
                    de.DrawString(y.ToString(), shrift, Brushes.Black, pictureBox1.Width/2 - 30,i*pictureBox1.Height/10) ;
                    
                }
                de.DrawString((-1*max_cord_y).ToString(), shrift, Brushes.Black, pictureBox1.Width/2 - 30,pictureBox1.Height-15) ;
            }
            float cur_x=min_cord_x, cur_y=polynom(cur_x,k), next_x, next_y;
            
            float ky =pictureBox1.Height / (max_cord_y - min_cord_y);
            for (int i = 0; i < pictureBox1.Width - 1; i++)
            {
                next_x = cur_x + step_x;
                next_y = polynom(next_x, k);
                ruchka.Color = Color.Red;
                de.DrawLine(ruchka, i, pictureBox1.Height / 2 - ky * cur_y, i+1, pictureBox1.Height / 2 - ky * next_y);
                cur_x = next_x;
                cur_y = next_y;

            }
        }

        
    }
}
