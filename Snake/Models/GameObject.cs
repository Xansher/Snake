using System;
using System.Collections.Generic;
using System.Windows.Shapes;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace Snake.Models
{
    public class GameObject
    {
        public GameObject(Canvas gameBoard)
        {
            this.GameBoard = gameBoard;
            Width = 30;
            Height = 30;
        }
        public virtual void Update()
        {
            Canvas.SetLeft(GameBoard.Children[Index],X);
            Canvas.SetTop(GameBoard.Children[Index], Y);
        }
        public virtual void Draw()
        { 

            Rectangle rect = new Rectangle
            {
                Width = Width,
                Height = Height,
                Fill = Brushes.Green
            };
            Index = GameBoard.Children.Add(rect);
            Canvas.SetLeft(rect, X);
            Canvas.SetTop(rect, Y);
        }

        protected Canvas GameBoard;
        public int Index { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
