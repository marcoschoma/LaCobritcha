using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsGame
{
    public partial class Form1 : Form
    {
        public bool IsRunning = true;
        public int DirectionV = 1;
        public int DirectionH = 0;
        public Snake Snake;
        public Food Food;
        private int columns = 30;
        private int rows = 20;
        Random random = new Random();
        private int dificulty = 0;

        public bool IsGameOver { get; private set; }

        public Form1()
        {
            this.BackColor = System.Drawing.Color.White;
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer,
                true);

            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            StartGame();
            var t = new Thread(() => run(this));
            t.Start();
        }

        private Snake CreateSnake()
        {
            int w = Width / columns;
            int h = this.Height / rows;
            return new Snake(Width / 2 - w / 2, Height - h * 2, h, h);
        }

        private void run(Form form)
        {
            while (IsRunning)
            {
                if (Snake.Shape.X < 0 || Snake.Shape.Y < 0 || Snake.Shape.X > form.Size.Width || Snake.Shape.Y > form.Size.Height)
                {
                    IsGameOver = true;
                }

                if (IsGameOver)
                {
                    lblGameOver.Visible = true;
                }
                else
                    Snake.Move(DirectionV, DirectionH);

                if (Snake.Shape.IntersectsWith(Food.Shape))
                {
                    Snake.eat(Food);
                    Snake.increaseSpeed(dificulty++);
                    lblPontuacao.Text = dificulty.ToString();
                    Food = CreateFood();
                }

                form.Refresh();
                Thread.Sleep(100);
            }
        }

        private Food CreateFood()
        {
            int i_x = random.Next(0, (int)(Width * .8));
            int i_y = random.Next(0, (int)(Height * .8));
            int fac = this.Height / rows;
            return new Food((i_x - i_x % fac), (i_y - i_y % fac), (this.Height / rows));
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Food.paint(e.Graphics);
            Snake.Paint(e.Graphics);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!IsGameOver)
            {
                if (DirectionV == 0)
                {
                    if (e.KeyCode == Keys.Up)
                    {
                        DirectionV = -1;
                        DirectionH = 0;
                    }
                    else if (e.KeyCode == Keys.Down)
                    {
                        DirectionV = 1;
                        DirectionH = 0;
                    }
                }
                if (DirectionH == 0)
                {
                    if (e.KeyCode == Keys.Right)
                    {
                        DirectionV = 0;
                        DirectionH = 1;
                    }
                    else if (e.KeyCode == Keys.Left)
                    {
                        DirectionV = 0;
                        DirectionH = -1;
                    }
                }
            }
            else
            {
                StartGame();
            }
        }

        private void StartGame()
        {
            Snake = CreateSnake();
            Food = CreateFood();
            IsRunning = true;
            IsGameOver = false;
            lblGameOver.Visible = false;
            DirectionH = 0;
            DirectionV = -1;
            dificulty = 0;
            lblPontuacao.Text = "0";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsRunning = false;
        }
    }
}