using Snake.ModelViews;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Snake.Models
{
    public enum GameState
    {
        ShowingMenu, Playing, ShowingRestartMenu
    }
    public class Game: INotifyPropertyChanged
    {
        private readonly GameWindow window;
        private int StartingInterval = 175;
        private int Level=0;
        DispatcherTimer gameTimer = new DispatcherTimer();
        private Random rnd = new Random();
        private List<String> Photos = new List<String>();

        public event PropertyChangedEventHandler PropertyChanged;

        public GameState State { get; set; }
        public Snake Snake { get; set; }
        public Food Food { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public int FieldWidth { get; set; }
        public int FieldHeight { get; set; }
        private int _Score;
        public int Score
        {
            get
            {
                return _Score;
            }
            set
            {
                _Score = value;
                NotifyPropertyChanged("Score");
            }
        }


        public Game(GameWindow window)
        {
            Photos.Add("https://cdn.pixabay.com/photo/2014/10/25/07/52/kingsnake-502263_960_720.jpg");
            Photos.Add("https://cdn.pixabay.com/photo/2013/07/12/17/14/dragon-151851_960_720.png");
            Photos.Add("https://cdn.pixabay.com/photo/2013/10/15/10/04/snake-195917_960_720.jpg");
            Photos.Add("https://cdn.pixabay.com/photo/2015/02/28/15/25/rattlesnake-653646_960_720.jpg");
            Photos.Add("https://cdn.pixabay.com/photo/2013/08/11/19/47/snake-171655_960_720.jpg");
            this.State = GameState.ShowingMenu;
            this.window = window;
            SetBoard();

            gameTimer.Tick += GameEventTimer;
            gameTimer.Interval = TimeSpan.FromMilliseconds(StartingInterval-Level);
        }

        

        public void Start()
        {
            State = GameState.Playing;
            //reset stats
            Score = 0;
            Level = 0;
            window.Image.Source = null;
            gameTimer.Interval = TimeSpan.FromMilliseconds(StartingInterval);
            
            window.GameBoard.Children.Clear();
            SetBoard();
            //spawn snake
            Snake = new Snake(window.GameBoard);
            
            //spawn food
            Food = new Food(window.GameBoard);
            while (!Food.SpawnNew(Snake));
            Food.Draw();


            Snake.OnHitWall += new HitWall(HitWallEventHandler);
            Snake.OnFoodEaten += new EatenFood(EatenFoodEventHandler);
            Snake.OnHitBody += new HitBody(HitBodyEventHandler);
            
            

            gameTimer.Start();
            
        }
        public void Stop()
        {
            State = GameState.ShowingRestartMenu;
            gameTimer.Stop();
            window.RestartMessage.Visibility = System.Windows.Visibility.Visible;
        }

        private void GameEventTimer(object sender, EventArgs e)
        {
            Snake.Update(Food);
            
            
        }
        public BitmapImage ToImage(byte[] array)
        {
            var ms = new MemoryStream(array);
            var image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.StreamSource = ms;
            image.EndInit();
            return image;
            
        }

        public async Task DownloadImage()
        {
            
            var httpClient = new HttpClient();
            
            var array=await httpClient.GetByteArrayAsync(Photos[rnd.Next(0,5)]);

            window.Image.Source = ToImage(array);
            
        }

        private void SetBoard()
        {
            this.Height = 600;
            this.Width = 600;
            FieldWidth = 30;
            FieldHeight = 30;

            int currentY = 0, currentX = 0;
            Boolean isDrawingEnd = false;

            Boolean isNextDiff = false;

            while (!isDrawingEnd)
            {
                Rectangle rect = new Rectangle
                {
                    Width = FieldWidth,
                    Height = FieldHeight,  
                    Fill = isNextDiff?Brushes.LightBlue:Brushes.SkyBlue
                };
                
                window.GameBoard.Children.Add(rect);
                Canvas.SetTop(rect, currentY);
                Canvas.SetLeft(rect, currentX);

                isNextDiff = !isNextDiff;

                currentX += FieldWidth;
                if (currentX >= Width)
                {
                    currentX = 0;
                    currentY += FieldHeight;
                    isNextDiff = !isNextDiff;
                }
                    
                if (currentY >= Height)
                    isDrawingEnd = true;
            }
                
            

        }



        private void HitWallEventHandler()
        {
            Stop();        
        }

        private async void EatenFoodEventHandler()
        {
            while(!Food.SpawnNew(Snake));
            Food.Update();
            Level+=2;
            gameTimer.Interval = TimeSpan.FromMilliseconds(StartingInterval-Level);
            Score++;
            await DownloadImage();
        }

        private void HitBodyEventHandler()
        {
            Stop();
        }

        public void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }

}
