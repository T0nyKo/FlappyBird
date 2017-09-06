using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

/// <Title>
/// FlappyBird using WPF C#. By Tony Ko
/// </Title>

namespace FlappyBird
{
    /// <Class>
    /// Interaction logic for MainWindow.xaml
    /// </Class>
    public partial class MainWindow : Window
    {
        /**stays here**/
        DispatcherTimer _timer; 
        DateTime start;

        /**User.cs class?**/
        bool space = false;
        bool alt = false;
        bool firstIteration = true;

        /**move to Pipes.cs class**/
        private const int SPACEBETWEENPIPES = 150;
        private const int PIPELEFTSPEED = 5; 

        public MainWindow()
        {
            InitializeComponent();

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 0, 0, 30), DispatcherPriority.Background, ticker, Dispatcher.CurrentDispatcher);
            _timer.IsEnabled = true;
            start = DateTime.Now;
        }

        /// <method>
        /// Event handler that executes for every tick that passes by on the _timer.
        /// </method>
        private void ticker(object sender, EventArgs e)
        {
            if (this.firstIteration || !detectCollision())
            {
                //Increments the display timer on MainWindow.
                var diff = DateTime.Now - start;
                timer.Text = $"{diff.Minutes:00}:{diff.Seconds:00}";

                //Simulates gravity on the bird.
                if (!this.space && !this.alt)
                {
                    long birdPos = Convert.ToInt64(bird.GetValue(Canvas.TopProperty));
                    Canvas.SetTop(bird, birdPos + 5);
                }
                this.space = false;
                this.alt = false;

                //Move pipes to the left, simulate scrolling landscape.
                generatePipes();
            }
        }

        /**MOVE THIS TO BIRD.CS Class, have to pass in bird coordinates? or can i grab bird 
         * coordinates from bird.cs class?**/
        /// <method>
        /// Detects whether the bird has collided with any of the pipes. 
        /// Will return true when collision is detected.
        /// </method>
        private bool detectCollision()
        {
            //get top right & bottom right corner of bird. RightProperty was not working properly.
            long birdLeftPos = Convert.ToInt64(bird.GetValue(Canvas.LeftProperty));
            long birdRightPos = birdLeftPos + (long)bird.Width;

            long pipeLeftPos = Convert.ToInt64(pipe.GetValue(Canvas.LeftProperty));
            long pipeRightPos = pipeLeftPos + (long)pipe.Width;

            long pipe2LeftPos = Convert.ToInt64(pipe2.GetValue(Canvas.LeftProperty));
            long pipe2RightPos = pipe2LeftPos + (long)pipe2.Width;

            //Checks if the bird is passing through pipes 1 and 3
            if ((pipeLeftPos <= birdRightPos && birdRightPos <= pipeRightPos) || (pipeLeftPos <= birdLeftPos && birdLeftPos <= pipeRightPos))
            {
                long birdTopPos = Convert.ToInt64(bird.GetValue(Canvas.TopProperty));
                long birdBotPos = birdTopPos + (long)bird.Height;

                long pipe1TopPos = Convert.ToInt64(pipe.GetValue(Canvas.TopProperty));
                long pipe3BotPos = pipe1TopPos - SPACEBETWEENPIPES;

                if (pipe3BotPos <= birdTopPos && pipe1TopPos >= birdBotPos)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            //Checks if the bird is passing through pipes 2 and 4. 
            else if ((pipe2LeftPos <= birdRightPos && birdRightPos <= pipe2RightPos) || (pipe2LeftPos <= birdLeftPos && birdLeftPos <= pipe2RightPos))
            {
                long birdTopPos = Convert.ToInt64(bird.GetValue(Canvas.TopProperty));
                long birdBotPos = birdTopPos + (long)bird.Height;

                long pipe2TopPos = Convert.ToInt64(pipe2.GetValue(Canvas.TopProperty));
                long pipe4BotPos = pipe2TopPos - SPACEBETWEENPIPES;

                if (pipe4BotPos <= birdTopPos && pipe2TopPos >= birdBotPos)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        /***************MOVE TO PIPES Class*****************/
        /// <summary>
        /// Interaction logic for MainWindow.xaml
        /// </summary>
        private void generatePipes()
        {
            //Random pipe length generator.
            Random random = new Random();
            int canvasHeight = (int)Convert.ToInt64(pipe.GetValue(Window.HeightProperty));

            //Initialize all 4 pipes.
            if (this.firstIteration)
            {
                int pipeHeight = random.Next(150, 500);
                Canvas.SetTop(pipe, pipeHeight);

                int pipeHeight2 = random.Next(150, 500);
                Canvas.SetTop(pipe2, pipeHeight2);

                int pipeHeight3 = canvasHeight - pipeHeight + SPACEBETWEENPIPES;
                Canvas.SetBottom(pipe3, pipeHeight3);

                int pipeHeight4 = canvasHeight - pipeHeight2 + SPACEBETWEENPIPES;
                Canvas.SetBottom(pipe4, pipeHeight4);

                this.firstIteration = false;
            }

            long pipePos = Convert.ToInt64(pipe.GetValue(Canvas.LeftProperty));
            long pipePos3 = Convert.ToInt64(pipe3.GetValue(Canvas.LeftProperty));

            //Moves pipes 1 & 3.
            if (pipePos > -84)
            {
                Canvas.SetLeft(pipe, pipePos - PIPELEFTSPEED);
                Canvas.SetLeft(pipe3, pipePos3 - PIPELEFTSPEED);
            }
            else
            {
                Canvas.SetLeft(pipe, 791);
                Canvas.SetLeft(pipe3, 791);

                int pipeHeight = random.Next(150, 500);
                Canvas.SetTop(pipe, pipeHeight);

                int pipeHeight3 = canvasHeight - pipeHeight + SPACEBETWEENPIPES;
                Canvas.SetBottom(pipe3, pipeHeight3);
            }

            //Moves pipes 2 & 4
            long pipePos2 = Convert.ToInt64(pipe2.GetValue(Canvas.LeftProperty));
            long pipePos4 = Convert.ToInt64(pipe4.GetValue(Canvas.LeftProperty));

            if (pipePos2 > -84)
            {
                Canvas.SetLeft(pipe2, pipePos2 - PIPELEFTSPEED);
                Canvas.SetLeft(pipe4, pipePos4 - PIPELEFTSPEED);
            }
            else
            {
                Canvas.SetLeft(pipe2, 791);
                Canvas.SetLeft(pipe4, 791);

                int pipeHeight2 = random.Next(150, 500);
                Canvas.SetTop(pipe2, pipeHeight2);

                int pipeHeight4 = canvasHeight - pipeHeight2 + SPACEBETWEENPIPES;
                Canvas.SetBottom(pipe4, pipeHeight4);
            }
        }

        /// <summary>
        /// Event handler for when the user presses a key. 
        /// Spacebar moves the bird up, shift will move the bird down.
        /// </summary>
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Space))
            {
                long birdPos = Convert.ToInt64(bird.GetValue(Canvas.TopProperty));
                Canvas.SetTop(bird, birdPos - 40);
                this.space = true;
            }
            else if (e.Key.Equals(Key.LeftShift) || e.Key.Equals(Key.RightShift))
            {
                long birdPos = Convert.ToInt64(bird.GetValue(Canvas.TopProperty));
                Canvas.SetTop(bird, birdPos + 30);
                this.alt = true;
            }
        }
    }
}
