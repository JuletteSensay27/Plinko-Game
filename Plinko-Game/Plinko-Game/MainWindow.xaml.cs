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
        Button gameButton = new Button();
        private int[] ballPosition = new int[9];
 
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
            gameBoardCont.Width = 800;
            gameBoardCont.Height = 590;
            gameBoardCont.Margin = new Thickness(300,((int)mainWindow.Height/2)-190,0,0);

            mainGrid.Children.Add(gameBoardCont);

            for (int i = 0; i < gameBoard.Length; i++)
                gameBoard[i] = new Label[i + 1];

            h = (int)gameBoardCont.Width / 2;
            v = 0;

            gameButton = new Button();
            gameButton.VerticalAlignment = VerticalAlignment.Top;
            gameButton.HorizontalAlignment = HorizontalAlignment.Left;
            gameButton.Margin = new Thickness(h-15, v, 0, 0);
            gameButton.Width = 70;
            gameButton.Height = 20;
            gameButton.Content = "Drop Ball";
            gameButton.Click += gameButton_Click;
            gameBoardCont.Children.Add(gameButton);

            h = 0;
            v = 30;

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
                    grid.VerticalContentAlignment = VerticalAlignment.Center;
                    grid.HorizontalContentAlignment = HorizontalAlignment.Center;
                    gameBoard[x][y] = grid;
                    gameBoardCont.Children.Add(gameBoard[x][y]);

                    h += 10 + (int)grid.Width;
                }

                v += 60;
                h = 0;
            }

        }

        private async void gameButton_Click(object sender, EventArgs e) 
        {
            for(int x = 0; x < gameBoard.Length; x++)
                for(int y = 0;y  < gameBoard[x].Length; y++)
                    gameBoard[x][y].Content = string.Empty;

            Random rnd = new Random();
            int newPos = 0;
            int counter = 0;
            int prevPos = 0;
            ballPosition[0] = 0;

            while (counter <= gameBoard.Length) 
            {
                await Task.Delay(100) ;

                if (counter < 1)
                {
                    prevPos = ballPosition[0];
                    gameBoard[counter][ballPosition[0]].Content = "*";
                    newPos = rnd.Next(0, 1000000);
                    newPos %= 100000;
                    newPos %= 10000;
                    newPos %= 2;
                    ballPosition[1] = newPos;
                }
                else if (counter == 1)
                {
                    gameBoard[counter - 1][prevPos].Content = "";
                    prevPos = ballPosition[1];
                    newPos = prevPos;
                    gameBoard[counter][ballPosition[1]].Content = "*";


                    if (newPos == 0)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 0;
                        }
                        else
                        {
                            newPos = 1;
                        }
                    }
                    else if (newPos == 1)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 1;
                        }
                        else
                        {
                            newPos = 2;
                        }
                    }

                    ballPosition[2] = newPos;

                }
                else if (counter == 2) 
                {

                    gameBoard[counter - 1][prevPos].Content = "";
                    prevPos = ballPosition[2];
                    newPos = prevPos;
                    gameBoard[counter][ballPosition[2]].Content = "*";

                    if (newPos == 0)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 0;
                        }
                        else
                        {
                            newPos = 1;
                        }
                    }
                    else if (newPos == 1)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 1;
                        }
                        else
                        {
                            newPos = 2;
                        }
                    }
                    else if (newPos == 2)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 2;
                        }
                        else
                        {
                            newPos = 3;
                        }
                    }

                    ballPosition[3] = newPos;
                }
                else if (counter == 3)
                {

                    gameBoard[counter - 1][prevPos].Content = "";
                    prevPos = ballPosition[3];
                    newPos = prevPos;
                    gameBoard[counter][ballPosition[3]].Content = "*";

                    if (newPos == 0)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 0;
                        }
                        else
                        {
                            newPos = 1;
                        }
                    }
                    else if (newPos == 1)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 1;
                        }
                        else
                        {
                            newPos = 2;
                        }
                    }
                    else if (newPos == 2)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 2;
                        }
                        else
                        {
                            newPos = 3;
                        }
                    }
                    else if (newPos == 3)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 3;
                        }
                        else
                        {
                            newPos = 4;
                        }
                    }

                    ballPosition[4] = newPos;
                }
                else if (counter == 4)
                {

                    gameBoard[counter - 1][prevPos].Content = "";
                    prevPos = ballPosition[4];
                    newPos = prevPos;
                    gameBoard[counter][ballPosition[4]].Content = "*";

                    if (newPos == 0)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 0;
                        }
                        else
                        {
                            newPos = 1;
                        }
                    }
                    else if (newPos == 1)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 1;
                        }
                        else
                        {
                            newPos = 2;
                        }
                    }
                    else if (newPos == 2)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 2;
                        }
                        else
                        {
                            newPos = 3;
                        }
                    }
                    else if (newPos == 3)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 3;
                        }
                        else
                        {
                            newPos = 4;
                        }
                    }
                    else if (newPos == 4)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 4;
                        }
                        else
                        {
                            newPos = 5;
                        }
                    }

                    ballPosition[5] = newPos;
                }
                else if (counter == 5)
                {

                    gameBoard[counter - 1][prevPos].Content = "";
                    prevPos = ballPosition[5];
                    newPos = prevPos;
                    gameBoard[counter][ballPosition[5]].Content = "*";

                    if (newPos == 0)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 0;
                        }
                        else
                        {
                            newPos = 1;
                        }
                    }
                    else if (newPos == 1)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 1;
                        }
                        else
                        {
                            newPos = 2;
                        }
                    }
                    else if (newPos == 2)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 2;
                        }
                        else
                        {
                            newPos = 3;
                        }
                    }
                    else if (newPos == 3)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 3;
                        }
                        else
                        {
                            newPos = 4;
                        }
                    }
                    else if (newPos == 4)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 4;
                        }
                        else
                        {
                            newPos = 5;
                        }
                    }
                    else if (newPos == 5)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 5;
                        }
                        else
                        {
                            newPos = 6;
                        }
                    }

                    ballPosition[6] = newPos;
                }
                else if (counter == 6)
                {

                    gameBoard[counter - 1][prevPos].Content = "";
                    prevPos = ballPosition[6];
                    newPos = prevPos;
                    gameBoard[counter][ballPosition[6]].Content = "*";

                    if (newPos == 0)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 0;
                        }
                        else
                        {
                            newPos = 1;
                        }
                    }
                    else if (newPos == 1)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 1;
                        }
                        else
                        {
                            newPos = 2;
                        }
                    }
                    else if (newPos == 2)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 2;
                        }
                        else
                        {
                            newPos = 3;
                        }
                    }
                    else if (newPos == 3)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 3;
                        }
                        else
                        {
                            newPos = 4;
                        }
                    }
                    else if (newPos == 4)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 4;
                        }
                        else
                        {
                            newPos = 5;
                        }
                    }
                    else if (newPos == 5)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 5;
                        }
                        else
                        {
                            newPos = 6;
                        }
                    }
                    else if (newPos == 6)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 6;
                        }
                        else
                        {
                            newPos = 7;
                        }
                    }

                    ballPosition[7] = newPos;
                }
                else if (counter == 7)
                {

                    gameBoard[counter - 1][prevPos].Content = "";
                    prevPos = ballPosition[7];
                    newPos = prevPos;
                    gameBoard[counter][ballPosition[7]].Content = "*";

                    if (newPos == 0)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 0;
                        }
                        else
                        {
                            newPos = 1;
                        }
                    }
                    else if (newPos == 1)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 1;
                        }
                        else
                        {
                            newPos = 2;
                        }
                    }
                    else if (newPos == 2)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 2;
                        }
                        else
                        {
                            newPos = 3;
                        }
                    }
                    else if (newPos == 3)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 3;
                        }
                        else
                        {
                            newPos = 4;
                        }
                    }
                    else if (newPos == 4)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 4;
                        }
                        else
                        {
                            newPos = 5;
                        }
                    }
                    else if (newPos == 5)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 5;
                        }
                        else
                        {
                            newPos = 6;
                        }
                    }
                    else if (newPos == 6)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 6;
                        }
                        else
                        {
                            newPos = 7;
                        }
                    }
                    else if (newPos == 7)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 7;
                        }
                        else
                        {
                            newPos = 8;
                        }
                    }

                    ballPosition[8] = newPos;
                }
                else if (counter == 8)
                {

                    gameBoard[counter - 1][prevPos].Content = "";
                    prevPos = ballPosition[8];
                    newPos = prevPos;
                    gameBoard[counter][ballPosition[8]].Content = "*";

                    /*if (newPos == 0)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 0;
                        }
                        else
                        {
                            newPos = 1;
                        }
                    }
                    else if (newPos == 1)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 1;
                        }
                        else
                        {
                            newPos = 2;
                        }
                    }
                    else if (newPos == 2)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 2;
                        }
                        else
                        {
                            newPos = 3;
                        }
                    }
                    else if (newPos == 3)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 4;
                        }
                        else
                        {
                            newPos = 5;
                        }
                    }
                    else if (newPos == 4)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 5;
                        }
                        else
                        {
                            newPos = 6;
                        }
                    }
                    else if (newPos == 5)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 6;
                        }
                        else
                        {
                            newPos = 7;
                        }
                    }
                    else if (newPos == 6)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 7;
                        }
                        else
                        {
                            newPos = 8;
                        }
                    }
                    else if (newPos == 7)
                    {
                        newPos = rnd.Next(0, 1000000);
                        newPos %= 100000;
                        newPos %= 10000;
                        newPos %= 2;

                        if (newPos == 0)
                        {
                            newPos = 8;
                        }
                        else
                        {
                            newPos = 9;
                        }
                    }

                    ballPosition[8] = newPos;*/
                }




                counter++;
            }

        }
       
    }
}
