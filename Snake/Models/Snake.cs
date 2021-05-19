using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Snake.Models
{
    public delegate void HitWall();
    public delegate void EatenFood();
    public delegate void HitBody();

    public class Snake
    {
        public Canvas GameBoard { get; set; }
        public SnakeHead SnakeHead { get; set; }
        public List<SnakeBodyPart> SnakeBody {get; set;}
        private Boolean IsUpdating = false;
        public Snake(Canvas gameBoard)
        {
            GameBoard = gameBoard;
            SnakeHead = new SnakeHead(GameBoard, 300, 300);
            SnakeBody = new List<SnakeBodyPart>();
            AddSnakeBody();
            AddSnakeBody();
        }
        public void Update(Food food)
        {
            
            // Snake head move
            SnakeHead.Move();
            SnakeHead.Update();
            //Snake bodypart move
            Direction previousDirection;
            Direction nextDirection = SnakeHead.Direction;
            foreach (var part in SnakeBody)
            {
                part.Move();
                part.Update();
                previousDirection = part.Direction;
                part.Direction = nextDirection;
                nextDirection = previousDirection;
            }
            //check if snake head hit wall

            if (SnakeHead.HitWall())
            {
                OnHitWall?.Invoke();
            }

            //check if snake eat food

            if (SnakeHead.EatenFood(food))
            {
                
                OnFoodEaten?.Invoke();
                AddSnakeBody();
            }

            //check if snakehead hit body

            foreach(var part in SnakeBody)
            {
                if (SnakeHead.X == part.X && SnakeHead.Y == part.Y)
                    OnHitBody?.Invoke();
            }
            IsUpdating = false;
            
        }

        public void AddSnakeBody()
        {
            if (SnakeBody.Count==0)
            {

                SnakeBodyPart snakePart = new SnakeBodyPart(GameBoard, SnakeHead.X - 30, SnakeHead.Y, SnakeHead.Direction);
                
                SnakeBody.Add(snakePart);
                return;
            }

            SnakeBodyPart last = SnakeBody[SnakeBody.Count-1];
            var part= new SnakeBodyPart(GameBoard,last.X, last.Y, last.Direction);
            switch (last.Direction)
            {
                case Direction.East:
                    {
                        part.X -= 30;
                        break;
                    }
                case Direction.West:
                    {
                        part.X += 30;
                        break;
                    }
                case Direction.South:
                    {
                        part.Y -= 30;
                        break;
                    }
                case Direction.North:
                    {
                        part.Y += 30;
                        break;
                    }
            }
            SnakeBody.Add(part);

            

        }
        public void ChangeDirection(Direction dir)
        {

            if (!IsUpdating)
            {
                if (SnakeHead.Direction == dir)
                {
                    return;
                }
                if ((SnakeHead.Direction == Direction.East && dir == Direction.West) || (SnakeHead.Direction == Direction.West && dir == Direction.East))
                {
                    return;
                }
                if ((SnakeHead.Direction == Direction.South && dir == Direction.North) || (SnakeHead.Direction == Direction.North && dir == Direction.South))
                {
                    return;
                }
                IsUpdating = true;
                SnakeHead.ChangeDirection(dir);
            }
            

        }

        public event HitWall OnHitWall;
        public event EatenFood OnFoodEaten;
        public event HitBody OnHitBody;
    }
}
