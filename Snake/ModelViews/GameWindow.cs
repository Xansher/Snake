using Snake.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace Snake.ModelViews
{
    public class GameWindow
    {
        
        public GameWindow(Canvas gameBoard, StackPanel welcomeMessage, Image image, StackPanel restartMessage)
        {
            GameBoard = gameBoard;
            WelcomeMessage = welcomeMessage;
            RestartMessage = restartMessage;
            Image = image;
            Game = new Game(this);
            

        }
        public StackPanel WelcomeMessage { get; set; }
        public StackPanel RestartMessage { get; set; }
        public Canvas GameBoard { get; set; }
        public Image Image { get; set; }
        public Game Game { get; set; }

        public void HandleKey(Key key)
        {
            //Showing menu key
            if (Game.State == GameState.ShowingMenu)
            {
                if (key == Key.Enter)
                {
                    WelcomeMessage.Visibility = System.Windows.Visibility.Collapsed;
                    
                    Game.Start();
                }
            }
            if (Game.State == GameState.ShowingRestartMenu)
            {
                if (key == Key.Enter)
                {
                    RestartMessage.Visibility = System.Windows.Visibility.Collapsed;
                    Game.Start();
                }
            }

            //Playing keys
            if (Game.State == GameState.Playing)
            {
                switch (key)
                {
                    case Key.Up:
                        {
                            Game.Snake.ChangeDirection(Direction.North);
                            break;
                        }
                    case Key.Down:
                        {
                            Game.Snake.ChangeDirection(Direction.South);
                            break;
                        }
                    case Key.Left:
                        {
                            Game.Snake.ChangeDirection(Direction.West);
                            break;
                        }
                    case Key.Right:
                        {
                            Game.Snake.ChangeDirection(Direction.East);
                            break;
                        }
                }
            }

            
           
            
        }

    }
}
