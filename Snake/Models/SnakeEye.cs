using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Snake.Models
{
    public class SnakeEye : SnakePart
    {
        public SnakeEye(Canvas gameBoard, int PosX, int PosY, Direction direction) : base(gameBoard, PosX, PosY)
        {
            Direction = direction;
            X += 12;
            Y += 12;
        }

        public override void Draw()
        {
            Ellipse rect = new Ellipse()
            {
                Width = 6,
                Height = 6,
                Fill = Brushes.Yellow
            };
            Index = GameBoard.Children.Add(rect);
            Canvas.SetLeft(rect, X);
            Canvas.SetTop(rect, Y);
        }

    }
}
