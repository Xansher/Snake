using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Snake.Models
{
    public class Food : GameObject
    {
        private Random rnd = new Random();
        public Food(Canvas gameBoard):base(gameBoard)
        {
            
        }

        public override void Draw()
        {

            Ellipse rect = new Ellipse()
            {
                Width = 30,
                Height = 30,
                Fill = Brushes.Red
            };
            Index = GameBoard.Children.Add(rect);
            Canvas.SetLeft(rect, X);
            Canvas.SetTop(rect, Y);
        }
        public Boolean SpawnNew(Snake snake)
        {
            int x = rnd.Next(0, 19) * 30;
            int y = rnd.Next(0, 19) * 30;
            if (x == snake.SnakeHead.X && y == snake.SnakeHead.Y)
                return false;
            foreach(var SnakeBodyPart in snake.SnakeBody)
            {
                if (x == SnakeBodyPart.X && y == SnakeBodyPart.Y)
                    return false;
            }
            X = x;
            Y = y;
            return true;
        }
        
    }
}
