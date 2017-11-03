using System;
using System.Windows;
using System.Windows.Threading;

//Written By: Tony Ko

namespace FlappyBird
{
    /// Interaction logic for MainWindow.xaml
    public partial class MainWindow : Window
    {
        private int tickCount = 0; 

        public MainWindow()
        {
            InitializeComponent();
            collapseScoreboard();
            startTicker();
            startGroundScroll();
        }

        private void startTicker()
        {
            DispatcherTimer _timer = new DispatcherTimer(new TimeSpan(0, 0, 0, 0, 30), DispatcherPriority.Background, ticker, Dispatcher.CurrentDispatcher);
        }

        // This method executes every tick.
        private void ticker(object sender, EventArgs e)
        {
            if (firstSpaceBar)
            {
                if (initializePipes)
                {
                    createPipes();
                }
                else if (detectCollision())
                {
                    makeBirdFall();
                    displayScoreboard();
                }
                // Game is being played, happens here.
                else
                {
                    if (space)
                    {
                        moveBirdUp();
                    }
                    else
                    {
                        moveBirdDown();
                    }
                    scrollPipes();
                    tickCount++; 
                }
            }
            else
            {
                // Initial state of game: bird is on cruise control, pipes are invisible.
                cruiseControlFlight();
            }
            space = false;
        }
    }
}
