using System;
using System.Windows;
using System.Windows.Media.Animation;

/// <Title>
/// FlappyBird using WPF C#. By Tony Ko
/// </Title>

namespace FlappyBird
{
    /// Interaction logic for MainWindow.xaml
    public partial class MainWindow : Window
    {
        private int GroundHeight
        {
            get {
                return (int)ground.ActualHeight; 
            }
        }

        private void startGroundScroll()
        {
            Storyboard storyboard = (Storyboard)FindResource("ground");
            storyboard.Begin();
        }

        private bool stopGroundScroll()
        {
            Storyboard storyboard = (Storyboard)FindResource("ground");
            storyboard.Stop();
            return true;
        }
    }
}
