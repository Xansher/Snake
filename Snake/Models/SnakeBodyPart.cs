using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Snake.Models
{
    public class SnakeBodyPart: SnakePart
    {
        public SnakeBodyPart(Canvas gameBoard, int PosX, int PosY, Direction direction):base(gameBoard,PosX, PosY)
        {
            Direction = direction;
        }

    }
}
