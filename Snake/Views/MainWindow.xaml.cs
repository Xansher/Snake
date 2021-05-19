using Snake.Models;
using Snake.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Snake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public GameWindow GameWindow { get; set; }
        public MainWindow()
        {
            
            InitializeComponent();
            GameWindow = new GameWindow(GameBoard, WelcomeMessage, Image, RestartMessage);
            this.DataContext = GameWindow;

        }


        private void OnKeyPressed(object sender, KeyEventArgs e)
        {
            GameWindow.HandleKey(e.Key);
            
        }
    }
}
