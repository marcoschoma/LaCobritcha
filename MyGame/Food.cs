using System.Drawing;

namespace WindowsFormsGame
{
    public class Food
    {
        public Food(int x, int y, int size)
        {
            Shape = new Rectangle(x, y, size, size);
            Pen = new Pen(Color.Red);
        }

        public int Size { get; set; }
        public Pen Pen { get; set; }
        public Rectangle Shape { get; internal set; }

        internal void paint(Graphics g)
        {
            //g.DrawEllipse(Pen, Shape);
            g.DrawRectangle(Pen, Shape);
            //g.DrawString("pizza", new Font("Arial", 5), Brushes.Black, Shape.X, Shape.Y);
        }
    }
}

