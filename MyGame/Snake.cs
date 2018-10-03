using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsGame
{
    public class Snake
    {
        //public int Width { get; set; }
        //public int Height { get; private set; }

        public Rectangle Shape;
        Pen blackPen = new Pen(Color.Black, 3);
        Pen whitePen = new Pen(Color.White, 3);
        public int Speed = 5;
        public List<int> posX = new List<int>();
        public List<int> posY = new List<int>();

        public Snake(int x, int y, int width, int height)
        {
            posX.Add(x);
            posY.Add(y);
            Shape = new Rectangle(x, y, width, height);
        }

        public void Move(int directionV, int directionH)
        {
            for (int i = posX.Count-1; i > 0; i--)
            {
                posX[i] = posX[i - 1];
                posY[i] = posY[i - 1];
            }
            Shape.X = posX[0] += directionH * Shape.Width;
            Shape.X -= Shape.X % Shape.Width;
            Shape.Y = posY[0] += directionV * Shape.Height;
            Shape.Y -= Shape.Y % Shape.Height;
        }

        internal void Paint(Graphics g)
        {
            for (int i = 0; i < posX.Count; i++)
            {
                g.DrawRectangle(blackPen, posX[i] - posX[i]% Shape.Width, posY[i]- posY[i] % Shape.Height, Shape.Width, Shape.Height);
            }
        }

        internal void eat(Food food)
        {
            posX.Add(posX.Last());
            posY.Add(posY.Last());
            Speed++;
        }

        internal void increaseSpeed(int dificulty)
        {
            Speed += dificulty;
        }
    }
}
