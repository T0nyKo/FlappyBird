using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

//Written By: Tony Ko

namespace FlappyBird
{
    /// Interaction logic for MainWindow.xaml
    public partial class MainWindow : Window
    {
        private bool space = false;
        private bool firstSpaceBar = false;

        private void restartButton_Click(object sender, RoutedEventArgs e)
        {
            //hide scoreboard.
            collapseScoreboard();
            //method to erase pipes from screen.
            resetPipes();
            //reset bird to initial position.
            resetBird();
            //start moving the ground.
            startGroundScroll();
            //return to initial state. 
            firstSpaceBar = false;
            //reset the score.
            score = 0;
        }

        /// Event handler for when the user presses a key. 
        /// Spacebar moves the bird up.
        private void KeyDown_Event(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Space) && !birdCollision)
            {
                space = true;
                if (!firstSpaceBar)
                {
                    firstSpaceBar = true;
                }
            }
        }
    }
}
