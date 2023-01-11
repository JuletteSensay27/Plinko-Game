using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Drawing;
using System.Windows.Media.Effects;
using System.Media;
using System.Linq.Expressions;
using System.Windows.Media.Animation;

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
        private Ellipse gameBall = new Ellipse();
        private Regex pattern = new Regex(@"^[0-9]+$");
        private string[] errorMessage = new string[3];
        private Image img = new Image();
        private AmazonDBDataContext amazonDB = new AmazonDBDataContext(Properties.Settings.Default.Group_2___CasinoConnectionString);
        private int nextcustomerID = 0;
        private int playerWager = 0;
        private decimal userBalance = 0;
        private int machineID = 2;
        private int gameID = 2;
        private decimal currentPlayerWinnings = 0;
        private Dictionary<string , List<Object>> logsCont = new Dictionary<string, List<Object>>();
        private int logCounter = 0;
        private int totalWager = 0;
        private decimal newMachineBal = 0;

        public MainWindow()
        {
             InitializeComponent();
             initGameBoard();
             if (userNameLbl.Content.ToString().Split(':')[1] == " ")
             {
                 confirmWagerBtn.IsEnabled = false;
                 addValueBtn.IsEnabled = false;
                 subValueBtn.IsEnabled = false;
                 gameButton.IsEnabled = false;
                userWagerTbx.IsEnabled = false;

             }
            gameButton.Visibility = Visibility.Hidden;
            
 
            infoHolRect.Height = gameBoardCont.Height + 150;
            txtloginUsername.FontFamily = new FontFamily("Lexend Deca");
            txtloginUsername.Foreground = new SolidColorBrush(Colors.White);
            txtloginPassword.Foreground = new SolidColorBrush(Colors.White);
            infoHolRect.Width = 500;
            gameNavRect.Width = Width + Width;
            titleLbl.FontFamily = new FontFamily("ROYALE");
            loginGrid.Margin = new Thickness(gameNavRect.Width - 1500, (gameNavRect.Height / 2) - 40, 0, 0);
            closeBtn.Margin = new Thickness(Width+427, 0, 0, 0); ;
            closeBtn.Height = gameNavRect.Height;
            closeBtn.FontFamily = new FontFamily("ROYALE");
            closeBtn.FontSize = 30;
            uNLbl.FontFamily = new FontFamily("ROYALE");
            uPassLbl.FontFamily = new FontFamily("ROYALE");
            playInfoGrid.Margin = new Thickness(35,(infoHolRect.Height/2)-150,0,0);
        }

        private decimal getMachineBal() 
        {
            var bal = (from a in amazonDB.table_Machines where a.Machine_ID == machineID select a).FirstOrDefault();

            decimal MachineBalance = 0;

            MachineBalance = bal.Machine_CurrentBalance;

            return MachineBalance;
            
        }

        private void initGameBoard() 
        {


            int v = 5;
            int h = 0;

            gameBoardCont = new Grid();
            gameBoardCont.VerticalAlignment = VerticalAlignment.Top;
            gameBoardCont.HorizontalAlignment = HorizontalAlignment.Left;
            gameBoardCont.Width = 1010;
            gameBoardCont.Height = 715;
            gameBoardCont.Margin = new Thickness(480,((int)mainWindow.Height/2)-125,0,0);
            mainGrid.Children.Add(gameBoardCont);

            for (int i = 0; i < gameBoard.Length; i++)
                gameBoard[i] = new Label[i + 1];

            h = (int)gameBoardCont.Width / 2;
            v = 0;

            h = 0;
            v = 70;

            for (int x = 0; x < gameBoard.Length; x++) 
            {
                switch (x) 
                {
                    case 0:
                        h = (int)gameBoardCont.Width / 2;
                        break;
                    case 1:
                        h = ((int)gameBoardCont.Width / 2) - 45;
                        break;
                    case 2:
                        h = ((int)gameBoardCont.Width / 2) - 90;
                        break;
                    case 3:
                        h = ((int)gameBoardCont.Width / 2) - 135;
                        break;
                    case 4:
                        h = ((int)gameBoardCont.Width / 2) - 180;
                        break;
                    case 5:
                        h = ((int)gameBoardCont.Width / 2) - 225;
                        break;
                    case 6:
                        h = ((int)gameBoardCont.Width / 2) - 270;
                        break;
                    case 7:
                        h = ((int)gameBoardCont.Width / 2) - 315;
                        break;
                    case 8:
                        h = ((int)gameBoardCont.Width / 2) - 360;
                        break;
                }

                for (int y = 0; y < gameBoard[x].Length; y++) 
                {               
                    Label grid = new Label();
                    DropShadowEffect ds = new DropShadowEffect();
                    ds.Color = Colors.Yellow;
                    ds.BlurRadius = 20;
                    ds.Direction = 250;
                    ds.ShadowDepth = 0;
                    grid.VerticalAlignment = VerticalAlignment.Top;
                    grid.HorizontalAlignment = HorizontalAlignment.Left;
                    grid.Width = 60;
                    grid.Height = 60;
                    grid.Background = new SolidColorBrush(Colors.DarkGoldenrod);

                    grid.Margin = new Thickness(h, v, 0, 0);
                    grid.BorderBrush = new SolidColorBrush(Colors.Yellow);
                    grid.BorderThickness = new Thickness(2, 2, 2, 2);
                    grid.Effect = ds;
                    grid.FontFamily = new FontFamily("ROYALE");
                   
                    grid.FontSize = 20;
                    
                    if (x == 8) 
                    {
                        grid.Foreground = new SolidColorBrush(Colors.White);
                        switch (y) 
                        {
                            case 0:
                                grid.Content = "x10";
                                break;
                            case 1:
                                grid.Content = "x3";
                                break;
                            case 2:
                                grid.Content = "x1/2";
                                break;
                            case 3:
                                grid.Content = "x1/5";
                                break;
                            case 4:
                                grid.Content = "x2";
                                break;
                            case 5:
                                grid.Content = "x1/5";
                                break;
                            case 6:
                                grid.Content = "x1/2";
                                break;
                            case 7:
                                grid.Content = "x3";
                                break;
                            case 8:
                                grid.Content = "x10";
                                break;
                        }
                    }

                    grid.VerticalContentAlignment = VerticalAlignment.Center;
                    grid.HorizontalContentAlignment = HorizontalAlignment.Center;
                    gameBoard[x][y] = grid;
                    gameBoardCont.Children.Add(gameBoard[x][y]);

                    h += 30 + (int)grid.Width;
                }

                v += 70;
                h = 0;
            }

        }

        private async void dropBall() 
        {
            Label[] prizeRow = new Label[9];

            gameButton.IsEnabled = false;

            for (int x = 0; x < gameBoard.Length; x++)
                for (int y = 0; y < gameBoard[x].Length; y++)
                    gameBoard[x][y].Content = string.Empty;

            for (int y = 0; y < gameBoard[8].Length; y++)
            {
                switch (y)
                {
                    case 0:
                        gameBoard[8][y].Content = "x10";
                        break;
                    case 1:
                        gameBoard[8][y].Content = "x3";
                        break;
                    case 2:
                        gameBoard[8][y].Content = "x1/2";
                        break;
                    case 3:
                        gameBoard[8][y].Content = "x1/5";
                        break;
                    case 4:
                        gameBoard[8][y].Content = "x2";
                        break;
                    case 5:
                        gameBoard[8][y].Content = "x1/5";
                        break;
                    case 6:
                        gameBoard[8][y].Content = "x1/2";
                        break;
                    case 7:
                        gameBoard[8][y].Content = "x3";
                        break;
                    case 8:
                        gameBoard[8][y].Content = "x10";
                        break;
                }
            }

            Random rnd = new Random();

            gameBall = new Ellipse();
            gameBall.Width = 30;
            gameBall.Height = 30;
            gameBall.Fill = new SolidColorBrush(Colors.Gold);
            gameBall.Stroke = new SolidColorBrush(Colors.LightGoldenrodYellow);
            gameBall.StrokeThickness = 3;

            int newPos = 0;
            int counter = 0;
            int prevPos = 0;
            ballPosition[0] = 0;

            while (counter <= gameBoard.Length)
            {
                await Task.Delay(75);

                if (counter < 1)
                {
                    prevPos = ballPosition[0];
                    gameBoard[counter][ballPosition[0]].Content = gameBall;
                    newPos = rnd.Next(0, 1000000);
                    newPos %= 100000;
                    newPos %= 10000;
                    newPos %= 2;
                    ballPosition[1] = newPos;
                }
                else if (counter == 1)
                {
                    gameBoard[counter - 1][prevPos].Content = string.Empty;
                    prevPos = ballPosition[1];
                    newPos = prevPos;
                    gameBoard[counter][ballPosition[1]].Content = gameBall;


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

                    gameBoard[counter - 1][prevPos].Content = string.Empty;
                    prevPos = ballPosition[2];
                    newPos = prevPos;
                    gameBoard[counter][ballPosition[2]].Content = gameBall;

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

                    gameBoard[counter - 1][prevPos].Content = string.Empty;
                    prevPos = ballPosition[3];
                    newPos = prevPos;
                    gameBoard[counter][ballPosition[3]].Content = gameBall;

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

                    gameBoard[counter - 1][prevPos].Content = string.Empty;
                    prevPos = ballPosition[4];
                    newPos = prevPos;
                    gameBoard[counter][ballPosition[4]].Content = gameBall;

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

                    gameBoard[counter - 1][prevPos].Content = string.Empty;
                    prevPos = ballPosition[5];
                    newPos = prevPos;
                    gameBoard[counter][ballPosition[5]].Content = gameBall;

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

                    gameBoard[counter - 1][prevPos].Content = string.Empty;
                    prevPos = ballPosition[6];
                    newPos = prevPos;
                    gameBoard[counter][ballPosition[6]].Content = gameBall;

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

                    gameBoard[counter - 1][prevPos].Content = string.Empty;
                    prevPos = ballPosition[7];
                    newPos = prevPos;
                    gameBoard[counter][ballPosition[7]].Content = gameBall;

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

                    gameBoard[counter - 1][prevPos].Content = string.Empty;
                    prevPos = ballPosition[8];
                    newPos = prevPos;
                    gameBoard[counter][ballPosition[8]].Content = gameBall;
                }

                counter++;
            }

            for (int i = 0; i < gameBoard[8].Length; i++)
            {
                prizeRow[i] = gameBoard[8][i];
            }
            showPrize(prizeRow, playerWager, userBalance);
            gameButton.IsEnabled = false;
            confirmWagerBtn.IsEnabled = true;
        }

        private string formatErrMessage(string[] errMess) 
        {
            string errMessToShow = string.Empty;

            errMessToShow = "Error Code: " + errMess[0] + "\n" +
                "Error Name: " + errMess[1] + "\n" +
                "Error Description: " + errMess[2];

            return errMessToShow;
        }

        private void addValueBtn_Click(object sender, RoutedEventArgs e)
        {
            string currentWagerVal = userWagerTbx.Text;
            int newWagerVal = 0;

            if (!pattern.IsMatch(currentWagerVal))
            {
                for (int i = 0; i < errorMessage.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            errorMessage[i] = "E1";
                            break;
                        case 1:
                            errorMessage[i] = "INPUT ERROR!";
                            break;
                        case 2:
                            errorMessage[i] = "INVALID AMOUNT! PLEASE TRY AGAIN !";
                            break;
                    }
                }

                MessageBox.Show(formatErrMessage(errorMessage));
            }
            else
            {
                newWagerVal = int.Parse(currentWagerVal);
                newWagerVal++;
                userWagerTbx.Text = newWagerVal.ToString();

            }
            
        }

        private void subValueBtn_Click(object sender, RoutedEventArgs e)
        {
            string currentWagerVal = userWagerTbx.Text;
            int newWagerVal = 0;

            if (!pattern.IsMatch(currentWagerVal))
            {
                for (int i = 0; i < errorMessage.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            errorMessage[i] = "E1";
                            break;
                        case 1:
                            errorMessage[i] = "INPUT ERROR!";
                            break;
                        case 2:
                            errorMessage[i] = "INVALID AMOUNT! PLEASE TRY AGAIN !";
                            break;
                    }
                }

                MessageBox.Show(formatErrMessage(errorMessage));
            }
            else
            {
                newWagerVal = int.Parse(currentWagerVal);

                if (newWagerVal <= 0) 
                {
                    newWagerVal = 0;
                }
                else
                {
                    newWagerVal--;              
                }

                userWagerTbx.Text = newWagerVal.ToString();
            }
        }

        private void confirmWagerBtn_Click(object sender, RoutedEventArgs e)
        {
            this.playerWager = 0;
            string userWagerVal = userWagerTbx.Text;
            decimal playerBalance = 0;
            int playerWager = 0;
            string message = "Do you wish to proceed with this operation?";
            string caption = "PLACING WAGER";
            var confirm = MessageBox.Show(message, caption, MessageBoxButton.YesNo);
            DateTime timeLogin = DateTime.Now;
            var customerIn = (from a in amazonDB.table_Customers
                              where a.Customer_Username == userNameHolLbl.Content.ToString()
                              select a).FirstOrDefault();
            int customerID = customerIn.Customer_ID;
            string logComment = string.Empty;

            if (confirm == MessageBoxResult.Yes) 
            {
                if (!pattern.IsMatch(userWagerVal))
                {
                    for (int i = 0; i < errorMessage.Length; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                errorMessage[i] += "E1";
                                break;
                            case 1:
                                errorMessage[i] = "INPUT ERROR!";
                                break;
                            case 2:
                                errorMessage[i] = "INVALID AMOUNT! PLEASE TRY AGAIN !";
                                break;
                        }
                    }

                    MessageBox.Show(formatErrMessage(errorMessage));
                }
                else
                {
                    playerBalance = decimal.Parse(userBalanceLbl.Content.ToString().Split(':')[1]);
                    playerWager = int.Parse(userWagerVal);

                    if (playerBalance < playerWager)
                    {
                        for (int i = 0; i < errorMessage.Length; i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    errorMessage[i] = "E2";
                                    break;
                                case 1:
                                    errorMessage[i] = "WAGER ERROR!";
                                    break;
                                case 2:
                                    errorMessage[i] = "INSUFFICIENT BALANCE!";
                                    break;
                            }
                        }
                        logComment = "Insufficient Balance";
                        createLog(timeLogin, customerID, machineID, gameID, 2, logComment , currentPlayerWinnings , getMachineBal());    
                        MessageBox.Show(formatErrMessage(errorMessage));
                    }
                    else if (playerWager < 1)
                    {
                        for (int i = 0; i < errorMessage.Length; i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    errorMessage[i] = "E3";
                                    break;
                                case 1:
                                    errorMessage[i] = "WAGER ERROR!";
                                    break;
                                case 2:
                                    errorMessage[i] = "CANNOT WAGER A VALUE OF ZERO!";
                                    break;
                            }
                        }

                        MessageBox.Show(formatErrMessage(errorMessage));
                    }
                    else
                    {
                      
                        userBalanceLbl.Content = $"User Balance: {playerBalance - playerWager}";
                        userWagerTbx.Text = "0";
                        totalWager += playerWager;
                        logComment = $"Customer Wagered {playerWager}";                    
                        createLog(timeLogin, customerID, machineID, gameID, 1, logComment, currentPlayerWinnings, getMachineBal());
                        this.playerWager = playerWager;
                        userBalance = decimal.Parse(userBalanceLbl.Content.ToString().Split(':')[1]);
                        confirmWagerBtn.IsEnabled = false;
                        gameButton.IsEnabled = true;
                        dropBall();

                    }
                }
            }
        }

        private void login()
        {

            lblloginstatus.Content = "Login Status : ";
            uPassStatusLbl.Content = "Password Status : ";
            uNameStatusLbl.Content = "Username Status: ";
            totalWager = 0;

            var allCustomers = (from b in amazonDB.table_Customers
                                where b.Customer_Username == txtloginUsername.Text
                                select b).ToList();

            var customerLogin = (from a in amazonDB.table_Customers
                                 where a.Customer_Username == txtloginUsername.Text
                                 select a).FirstOrDefault();

            string[] customers = new string[nextcustomerID + 1];
            string password = "";

            int y = 0;

            foreach (var customer in allCustomers)
            {
                customers[y] = customer.Customer_Username.ToString();
                y++;
            }

            if (customers.Contains(txtloginUsername.Text))
            {
                uNameStatusLbl.Content += "Username Found";
                password = customerLogin.Customer_Password.ToString();
                
                if (txtloginPassword.Password == password)
                {
                    //lblloginstatus.Content = "Login Success";
                    /*
                     * Game Logs Components
                     * 
                     * DateTime
                     * CustomerID
                     * MachineID
                     * gameID
                     * errorID
                     * gamelogComments
                     * customerWinnings
                     * machineCurrentBalance
                     */

                    DateTime timeLogin = DateTime.Now;
                    userNameHolLbl.Content = txtloginUsername.Text;
                    var customerIn = (from a in amazonDB.table_Customers
                                      where a.Customer_Username == userNameHolLbl.Content.ToString()
                                      select a).FirstOrDefault();

                    int customerID = customerIn.Customer_ID;
                  
                    checkCustomerIfLoggedIn(customerID);
                  
                }
                else
                {
                    uPassStatusLbl.Content += "Password not match!";
                    lblloginstatus.Content += "Login Failed";
                }
            }
            else
            {
                DateTime timeLogin = DateTime.Now;
               amazonDB.uspCreateGameLog(timeLogin, 1, machineID, gameID, 4, $"Attempt log in with non existent username {txtloginUsername.Text}", 0, 0);
                uNameStatusLbl.Content += "Username Not Found";
                lblloginstatus.Content += "Login Failed";
            }
        }

        private void checkCustomerIfLoggedIn(int customerID)
        {
            var current = amazonDB.vwFunqAllMachines().ToList();

            var customerIn = (from a in amazonDB.table_Customers
                              where a.Customer_Username == userNameHolLbl.Content.ToString()
                              select a).FirstOrDefault();

           List<int> idsLoggedIn = new List<int>();
           

            foreach (var id in current)
            {
                idsLoggedIn.Add(id.Customer_ID);
            }

            if (idsLoggedIn.Contains(customerID))
            {
                userNameHolLbl.Content = string.Empty;
                MessageBox.Show("User is logged in already in different machine");
                DateTime timeLogin = DateTime.Now;
                amazonDB.uspCreateGameLog(timeLogin, customerID, machineID, gameID, 5, $"Attempted login while customer is already active on customer {customerID}", 0, getMachineBal());
                this.Close();
            }
            else
            {
                if (customerIn.Customer_CurrentBalance <= 0)
                {
                   
                    MessageBox.Show("This account has 0 balance left");
                    DateTime timeLogin = DateTime.Now;
                    createLog(timeLogin, customerID, machineID, gameID, 2, $"Customer {customerID} balance zero balance", 0, getMachineBal());             
                }
                else
                {
                    var customerLogin = (from a in amazonDB.table_Customers
                                         where a.Customer_Username == txtloginUsername.Text
                                         select a).FirstOrDefault();
                    DateTime timeLogin = DateTime.Now;
                    createLog(timeLogin, customerID, machineID, gameID, 1, $"Customer {customerID} has logged in", 0, getMachineBal());
                    amazonDB.uspUpdateMachineCustomer(machineID, customerID);
                    uPassStatusLbl.Content += "Password match";
                    lblloginstatus.Content += "Login Success";
                    userNameLbl.Content += customerLogin.Customer_FirstName;
                    userBalanceLbl.Content += customerLogin.Customer_CurrentBalance.ToString();
                    confirmWagerBtn.IsEnabled = true;
                    addValueBtn.IsEnabled = true;
                    subValueBtn.IsEnabled = true;
                    userWagerTbx.IsEnabled = true;
                    loginBtn.Content = "Log Out";
                    txtloginPassword.Password = string.Empty;
                    txtloginUsername.Text = string.Empty;
                    txtloginPassword.IsEnabled = false;
                    txtloginUsername.IsEnabled = false;

                }
            }
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (loginBtn.Content.ToString() == "Log In")
                login();
            else 
            {
                string message = "Do you wish to proceed with this operation?";
                string caption = "LOGGING OUT";
                var confirm = MessageBox.Show(message, caption, MessageBoxButton.YesNo);

                if (confirm == MessageBoxResult.Yes) 
                {
                    logout();
                   
                }
            }

        }

        private void logout() 
        {
            DateTime timeLogin = DateTime.Now;
            var customerIn = (from a in amazonDB.table_Customers
                              where a.Customer_Username == userNameHolLbl.Content.ToString()
                              select a).FirstOrDefault();

            int customerID = customerIn.Customer_ID;

            newMachineBal = getMachineBal() + totalWager;

            userBalanceLbl.Content = "User Balance : ";
            userNameLbl.Content = "Username : ";
            confirmWagerBtn.IsEnabled = false;
            addValueBtn.IsEnabled = false;
            subValueBtn.IsEnabled = false;
            gameButton.IsEnabled = false;
            loginBtn.Content = "Log In";
            lblloginstatus.Content = "Login Status : ";
            uPassStatusLbl.Content = "Password Status : ";
            uNameStatusLbl.Content = "Username Status: ";
            userWinningsLbl.Content = "User Winnings : ";
            userNameHolLbl.Content = string.Empty;
            amazonDB.uspUpdateMachineCurrentWinnings(machineID, currentPlayerWinnings);
            amazonDB.uspUpdateMachineBalance(machineID, newMachineBal);
            amazonDB.uspUpdateCustomerCurrentBalance(customerID, totalWager);
            createLog(timeLogin, 1, machineID, gameID, 1, $"Machine Balance has increased by {totalWager}", currentPlayerWinnings, getMachineBal());
            createLog(timeLogin, customerID, machineID, gameID, 1, $"Customer {customerID} has logged out", currentPlayerWinnings, getMachineBal());
            pushLogToDB();
            amazonDB.uspUpdateMachineCustomer(machineID, 1);
            MainWindow mw = new MainWindow();
            this.Close();
            mw.Show();

        }

        private void showPrize(Label[] prizeRow, int playerWager, decimal userBalance) 
        {

            decimal playerPrize = 0;
            DateTime timeLogin = DateTime.Now;
            var customerIn = (from a in amazonDB.table_Customers
                              where a.Customer_Username == userNameHolLbl.Content.ToString()
                              select a).FirstOrDefault();

            int customerID = customerIn.Customer_ID;
            string logComment = string.Empty;

            for (int i = 0; i < prizeRow.Length; i++) 
            {
                if (prizeRow[i].Content == gameBall) 
                {
                    switch (i) 
                    {
                        case 0:
                            playerPrize = playerWager * 10;                        
                            currentPlayerWinnings += playerPrize;
                            logComment = $"Customer {customerIn.Customer_FirstName} won ten times their wager ";
                            userBalanceLbl.Content = $"User Balance: {userBalance}";
                            break;
                        case 1:
                            playerPrize = playerWager * 3;                       
                            currentPlayerWinnings += playerPrize; 
                            logComment = $"Customer {customerIn.Customer_FirstName} won three times their wager ";
                            userBalanceLbl.Content = $"User Balance: {userBalance}";
                            break;
                        case 2:
                            playerPrize = playerWager * 1/2;                          
                            currentPlayerWinnings += playerPrize;
                            logComment = $"Customer {customerIn.Customer_FirstName} Lost half of their wager ";
                            userBalanceLbl.Content = $"User Balance: {userBalance}";
                            break;
                        case 3:
                            playerPrize = playerWager * 1/5;                           
                            currentPlayerWinnings += playerPrize;
                            logComment = $"Customer {customerIn.Customer_FirstName} lost a fifth of their wager ";
                            userBalanceLbl.Content = $"User Balance: {userBalance}";
                            break;
                        case 4:
                            playerPrize = playerWager * 2;                          
                            currentPlayerWinnings += playerPrize;
                            logComment = $"Customer {customerIn.Customer_FirstName}  won twice their wager ";
                            userBalanceLbl.Content = $"User Balance: {userBalance}";
                            break;
                        case 5:
                            playerPrize = playerWager * 1/5;                         
                            currentPlayerWinnings += playerPrize;
                            logComment = $"Customer {customerIn.Customer_FirstName} lost a fifth of their wager ";
                            userBalanceLbl.Content = $"User Balance: {userBalance}";
                            break;
                        case 6:
                            playerPrize = playerWager * 1/2;                           
                            currentPlayerWinnings += playerPrize;
                            logComment = $"Customer {customerIn.Customer_FirstName}  Lost half of their wager ";
                            userBalanceLbl.Content = $"User Balance: {userBalance}";
                            break;
                        case 7:
                            playerPrize = playerWager * 3;                           
                            currentPlayerWinnings += playerPrize;
                            logComment = $"Customer {customerIn.Customer_FirstName} won three times their wager ";
                            userBalanceLbl.Content = $"User Balance: {userBalance}";
                            break;
                        case 8:
                            playerPrize = playerWager * 10;                           
                            currentPlayerWinnings += playerPrize;
                            logComment = $"Customer {customerIn.Customer_FirstName}  won ten times their wager ";
                            userBalanceLbl.Content = $"User Balance: {userBalance}";
                            break;
                    }
                }
            }

            userWinningsLbl.Content = $"User Winnings : {currentPlayerWinnings.ToString()}" ;

            createLog(timeLogin, customerID, machineID, gameID, 1, logComment, currentPlayerWinnings, getMachineBal());
        }

        private void mainWindow_Closed(object sender, EventArgs e)
        {

            if (userNameHolLbl.Content.ToString() != string.Empty) 
            {
                DateTime timeLogin = DateTime.Now;
                var customerIn = (from a in amazonDB.table_Customers
                                  where a.Customer_Username == userNameHolLbl.Content.ToString()
                                  select a).FirstOrDefault();

                int customerID = customerIn.Customer_ID;
                newMachineBal = getMachineBal() + totalWager;
                amazonDB.uspUpdateMachineCurrentWinnings(machineID, currentPlayerWinnings);
                amazonDB.uspUpdateMachineBalance(machineID, newMachineBal);
                amazonDB.uspUpdateCustomerCurrentBalance(customerID, totalWager);
                amazonDB.uspUpdateMachineCustomer(machineID, 1);
                createLog(timeLogin, 1, machineID, gameID, 1, $"Machine Balance has increased by {totalWager}", currentPlayerWinnings, getMachineBal());
                createLog(timeLogin, customerID, machineID, gameID, 1, $"Customer {customerID} has quit the application unexpectedly", currentPlayerWinnings, getMachineBal());
                pushLogToDB();
            }
        }

        private void mainWindow_Initialized(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            string logComment = "Machine Running Low on funds";
            amazonDB.uspUpdateMachineGame(machineID,gameID);

            if (getMachineBal() <= 10000)
            {
                MessageBox.Show("MACHINE FUNDS RUNNING LOW! MAINTENANCE NEEDED!");
                createLog(date, 1 , machineID, gameID, 3, logComment, 0, getMachineBal());
                pushLogToDB();
                this.Close();
            }
        }

        private void createLog(DateTime date, int CID, int MID, int GID, int EID, string GLC, decimal CW, decimal MCB) 
        {
            /*
             * Game Logs Components
             * 
             * DateTime
             * CustomerID
             * MachineID
             * gameID
             * errorID
             * gamelogComments
             * customerWinnings
             * machineCurrentBalance
             */
            
            List<Object> logsHol = new List<Object>();

            logsHol.Add(date);
            logsHol.Add(CID);
            logsHol.Add(MID);
            logsHol.Add(GID);
            logsHol.Add(EID);
            logsHol.Add(GLC);
            logsHol.Add(CW);
            logsHol.Add(MCB);

            logsCont.Add($"log{logCounter+1}",logsHol);
            logCounter++;

        }

        private void pushLogToDB() 
        {
            for (int i = 0; i < logsCont.Count; i++) 
            {
                int x = 0;

                    amazonDB.uspCreateGameLog(
                            DateTime.Parse(logsCont.Values.ElementAt(i).ElementAt(x).ToString()),
                            int.Parse(logsCont.Values.ElementAt(i).ElementAt(x+1).ToString()),
                            int.Parse(logsCont.Values.ElementAt(i).ElementAt(x+2).ToString()),
                            int.Parse(logsCont.Values.ElementAt(i).ElementAt(x+3).ToString()),
                            int.Parse(logsCont.Values.ElementAt(i).ElementAt(x+4).ToString()),
                            logsCont.Values.ElementAt(i).ElementAt(x+5).ToString(),
                            decimal.Parse(logsCont.Values.ElementAt(i).ElementAt(x+6).ToString()),
                            decimal.Parse(logsCont.Values.ElementAt(i).ElementAt(x+7).ToString())
                        );
            }
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            var confirm = MessageBox.Show(messageBoxText: "Do you want to proceed with this operation?", caption: "Closing Application", MessageBoxButton.YesNo);

            if (confirm == MessageBoxResult.Yes) 
            {
                this.Close();
            }
        }
    }
}
