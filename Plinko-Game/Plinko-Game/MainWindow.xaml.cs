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

namespace Plinko_Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Label[][] gameBoard = new Label[9][];
        private Grid gameBoardCont = new Grid();
        Border gameContBorder = new Border();
 
        public MainWindow()
        {
            InitializeComponent();
            initGameBoard();
        }

        private void initGameBoard() 
        {
            int v = 5;
            int h = 0;

            gameBoardCont = new Grid();
            gameBoardCont.VerticalAlignment = VerticalAlignment.Top;
            gameBoardCont.HorizontalAlignment = HorizontalAlignment.Left;
            gameBoardCont.Width = 1200;
            gameBoardCont.Height = 600;

            mainGrid.Children.Add(gameBoardCont);

            for (int i = 0; i < gameBoard.Length; i++)
                gameBoard[i] = new Label[i + 1];

            
            for (int x = 0; x < gameBoard.Length; x++) 
            {
                switch (x) 
                {
                    case 0:
                        h = (int)gameBoardCont.Width / 2;
                        break;
                    case 1:
                        h = ((int)gameBoardCont.Width / 2) - 25;
                        break;
                    case 2:
                        h = ((int)gameBoardCont.Width / 2) - 50;
                        break;
                    case 3:
                        h = ((int)gameBoardCont.Width / 2) - 75;
                        break;
                    case 4:
                        h = ((int)gameBoardCont.Width / 2) - 100;
                        break;
                    case 5:
                        h = ((int)gameBoardCont.Width / 2) - 125;
                        break;
                    case 6:
                        h = ((int)gameBoardCont.Width / 2) - 150;
                        break;
                    case 7:
                        h = ((int)gameBoardCont.Width / 2) - 175;
                        break;
                    case 8:
                        h = ((int)gameBoardCont.Width / 2) - 200;
                        break;
                }

                for (int y = 0; y < gameBoard[x].Length; y++) 
                {               
                    Label grid = new Label();
                    grid.VerticalAlignment = VerticalAlignment.Top;
                    grid.HorizontalAlignment = HorizontalAlignment.Left;
                    grid.Width = 40;
                    grid.Height = 40;
                    grid.Margin = new Thickness(h, v, 0, 0);
                    grid.BorderBrush = new SolidColorBrush(Colors.Black);
                    grid.BorderThickness = new Thickness(2, 2, 2, 2);
                    gameBoard[x][y] = grid;
                    gameBoardCont.Children.Add(gameBoard[x][y]);

                    h += 10 + (int)grid.Width;
                }

                v += 60;
                h = 0;
            }
        }
    }
}
