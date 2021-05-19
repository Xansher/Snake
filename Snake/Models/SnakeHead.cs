using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Snake.Models
{
    public class SnakeHead: SnakePart
    {
        public SnakeEye SnakeEye { get; set; }
        public SnakeHead(Canvas gameBoard, int PosX, int PosY) :base(gameBoard, PosX, PosY)
        {
            SnakeEye = new SnakeEye(gameBoard, PosX, PosY, Direction);
        }

        public override void Move()
        {
            base.Move();
            SnakeEye.Move();
        }

        public override void Update()
        {
            base.Update();
            SnakeEye.Update();
        }

        public void ChangeDirection(Direction direction)
        {
            
            Direction = direction;
            SnakeEye.Direction = direction;
            

        }

        public Boolean HitWall()
        {
            switch (Direction)
            {
                case Direction.South:
                    {
                        if (Y >= GameBoard.ActualHeight)
                        {
                            return true;
                        }
                        break;
                    }
                case Direction.East:
                    {
                        if (X >= GameBoard.ActualWidth)
                        {
                            return true;
                        }
                        break;
                    }
                case Direction.North:
                    {
                        if (Y < 0)
                        {
                            return true;
                        }
                        break;
                    }
                case Direction.West:
                    {
                        if (X < 0)
                        {
                            return true;
                        }
                        break;
                    }
            }
            return false;
        }

        public Boolean EatenFood(Food food)
        {
            if(X== food.X && Y== food.Y)
            return true;

            return false;
        }

    }
}
