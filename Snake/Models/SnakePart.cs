using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Snake.Models
{
    public class SnakePart: GameObject
    {
        public Direction Direction { get; set; }
        public SnakePart(Canvas gameBoard, int PosX, int PosY):base(gameBoard)
        {
            X = PosX;
            Y = PosY;
            Direction = Direction.East;
            Draw();
        }

        public virtual void Move()
        {
            switch (Direction)
            {
                case Direction.South:
                    {
                        Y += 30;
                        break;
                    }
                case Direction.East:
                    {
                        X += 30;
                        break;
                    }
                case Direction.North:
                    {
                        Y -= 30;
                        break;
                    }
                case Direction.West:
                    {
                        X -= 30;
                        break;
                    }
            }
        }
    }
}
