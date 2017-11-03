using System;
using System.Windows;
using System.Windows.Controls;

// Written By: Tony Ko

namespace FlappyBird
{
    // Interaction logic for MainWindow.xaml
    public partial class MainWindow : Window
    {
        private bool initializePipes = true;
        private const int spaceBetweenPipes = 200;
        private const int pipeLeftSpeed = 5;
        private const int leftSideOfScreen = -84;
        private const int rightSideOfScreen = 791;
        private const int pipeTipOffset = 9;
        private const int pipeBotTopLeft_startPosition = 922;
        private const int pipeBotTopRight_startPosition = 1308;
        private const int pipeTipBotTopLeft_startPosition = 913; 
        private const int pipeTipBotTopRight_startPosition = 1299;
        private const int lo = 265;
        private const int hi = 425;

        private int CanvasHeight
        {
            get {
                return (int)canvas.ActualHeight;
            }
        }

        private int PipeWidth
        {
            get{
                return (int)pipe1.ActualWidth;
            }
        }

        // Diagram of pipes on MainWindow.
        //           []         []
        //          pipe3      pipe4
        //  bird
        //
        //          pipe1      pipe2
        //           []         []
        //
        // This are properties for pipe1.
        private int PipeBotLeft_TopCoordinate
        {
            get {
                return (int)Convert.ToInt64(pipe1.GetValue(Canvas.TopProperty));
            }
            set {
                Canvas.SetTop(pipe1, value);
            }
        }
        private int PipeBotLeft_LeftCoordinate
        {
            get {
                return (int)Convert.ToInt64(pipe1.GetValue(Canvas.LeftProperty));
            }
            set {
                Canvas.SetLeft(pipe1, value);
            }
        }
        private int PipeBotLeft_RightCoordinate
        {
            get {
                return PipeBotLeft_LeftCoordinate + PipeWidth; 
            }
        }
        // Pipe tip for pipe1. 
        private int PipeTipBotLeft_TopCoordinate
        {
            get {
                return (int)Convert.ToInt64(pipeTip1.GetValue(Canvas.TopProperty));
            }
            set {
                Canvas.SetTop(pipeTip1, value);
            }
        }
        private int PipeTipBotLeft_LeftCoordinate
        {
            get {
                return (int)Convert.ToInt64(pipeTip1.GetValue(Canvas.LeftProperty));
            }
            set {
                Canvas.SetLeft(pipeTip1, value);
            }
        }

        // These are properties for pipe2.
        private int PipeBotRight_TopCoordinate
        {
            get{
                return (int)Convert.ToInt64(pipe2.GetValue(Canvas.TopProperty));
            }
            set{
                Canvas.SetTop(pipe2, value);
            }
        }
        private int PipeBotRight_LeftCoordinate
        {
            get{
                return (int)Convert.ToInt64(pipe2.GetValue(Canvas.LeftProperty));
            }
            set{
                Canvas.SetLeft(pipe2, value);
            }
        }
        private int PipeBotRight_RightCoordinate
        {
            get{
                return PipeBotRight_LeftCoordinate + PipeWidth;
            }
        }
        // Pipe tip for pipe2. 
        private int PipeTipBotRight_TopCoordinate
        {
            get {
                return (int)Convert.ToInt64(pipeTip2.GetValue(Canvas.TopProperty));
            }
            set {
                Canvas.SetTop(pipeTip2, value);
            }
        }
        private int PipeTipBotRight_LeftCoordinate
        {
            get {
                return (int)Convert.ToInt64(pipeTip2.GetValue(Canvas.LeftProperty));
            }
            set {
                Canvas.SetLeft(pipeTip2, value);
            }
        }

        // These are properties for pipe3.
        private int PipeTopLeft_BotCoordinate
        {
            get {
                return PipeBotLeft_TopCoordinate - spaceBetweenPipes; 
            }
            set {
                Canvas.SetBottom(pipe3, value);
            }
        }
        private int PipeTopLeft_LeftCoordinate
        {
            get {
                return (int)Convert.ToInt64(pipe3.GetValue(Canvas.LeftProperty));
            }
            set {
                Canvas.SetLeft(pipe3, value);
            }
        }
        private int PipeTopLeft_RightCoordinate
        {
            get {
                return PipeTopLeft_LeftCoordinate + PipeWidth;
            }
        }
        private int PipeTipTopLeft_BotCoordinate
        {
            get
            {
                return PipeTopLeft_BotCoordinate; 
            }
            set
            {
                Canvas.SetBottom(pipeTip3, value);
            }
        }
        private int PipeTipTopLeft_LeftCoordinate
        {
            get {
                return (int)Convert.ToInt64(pipeTip3.GetValue(Canvas.LeftProperty));
            }
            set {
                Canvas.SetLeft(pipeTip3, value);
            }
        }

        // These are properties for pipe4.
        private int PipeTopRight_BotCoordinate
        {
            get {
                return PipeBotRight_TopCoordinate - spaceBetweenPipes; 
            }
            set {
                Canvas.SetBottom(pipe4, value);
            }
        }
        private int PipeTopRight_LeftCoordinate
        {
            get {
                return (int)Convert.ToInt64(pipe4.GetValue(Canvas.LeftProperty));
            }
            set {
                Canvas.SetLeft(pipe4, value);
            }
        }
        private int PipeTopRight_RightCoordinate
        {
            get {
                return PipeTopRight_LeftCoordinate + PipeWidth;
            }
        }
        // Pipe tip for pipe2. 
        private int PipeTipTopRight_BotCoordinate
        {
            get
            {
                return PipeTopRight_BotCoordinate; 
            }
            set
            {
                Canvas.SetBottom(pipeTip4, value);
            }
        }
        private int PipeTipTopRight_LeftCoordinate
        {
            get
            {
                return (int)Convert.ToInt64(pipeTip4.GetValue(Canvas.LeftProperty));
            }
            set
            {
                Canvas.SetLeft(pipeTip4, value);
            }
        }

        // Used to generate random length pipes.
        private int generatePipeHeight()
        {
            Random random = new Random();
            return random.Next( lo, hi);
        }

        private void createPipes()
        {
            // Change the state of the flag variable. 
            initializePipes = false;

            // Place all 4 pipes in starting positions.
            PipeBotLeft_TopCoordinate = generatePipeHeight();
            PipeTipBotLeft_TopCoordinate = PipeBotLeft_TopCoordinate;

            PipeBotRight_TopCoordinate = generatePipeHeight();
            PipeTipBotRight_TopCoordinate = PipeBotRight_TopCoordinate;

            int pipeTopLeft_Height = CanvasHeight - PipeBotLeft_TopCoordinate + spaceBetweenPipes;
            PipeTopLeft_BotCoordinate = pipeTopLeft_Height;
            PipeTipTopLeft_BotCoordinate = pipeTopLeft_Height;

            int pipeTopRight_Height = CanvasHeight - PipeBotRight_TopCoordinate + spaceBetweenPipes;
            PipeTopRight_BotCoordinate = pipeTopRight_Height;
            PipeTipTopRight_BotCoordinate = pipeTopRight_Height; 
        }

        private void resetPipes()
        {
            PipeBotLeft_LeftCoordinate = pipeBotTopLeft_startPosition;
            PipeTipBotLeft_LeftCoordinate = pipeTipBotTopLeft_startPosition;

            PipeBotRight_LeftCoordinate = pipeBotTopRight_startPosition;
            PipeTipBotRight_LeftCoordinate = pipeTipBotTopRight_startPosition;

            PipeTopLeft_LeftCoordinate = pipeBotTopLeft_startPosition;
            PipeTipTopLeft_LeftCoordinate = pipeTipBotTopLeft_startPosition;

            PipeTopRight_LeftCoordinate = pipeBotTopRight_startPosition;
            PipeTipTopRight_LeftCoordinate = pipeTipBotTopRight_startPosition;  
        }

        // Moves the pipes leftward across the screen as well as resetting them after they fall off the page. 
        private void scrollPipes()
        {
            // Don't scroll pipes if bird has collided already. 
            if (birdCollision)
            {
                return; 
            }

            // Scroll pipes 1 & 3.
            if (PipeBotLeft_LeftCoordinate > leftSideOfScreen)
            {
                PipeBotLeft_LeftCoordinate = PipeBotLeft_LeftCoordinate - pipeLeftSpeed;
                PipeTipBotLeft_LeftCoordinate = PipeTipBotLeft_LeftCoordinate - pipeLeftSpeed;

                PipeTopLeft_LeftCoordinate = PipeTopLeft_LeftCoordinate - pipeLeftSpeed;
                PipeTipTopLeft_LeftCoordinate = PipeTipTopLeft_LeftCoordinate - pipeLeftSpeed; 
            }
            // Reset pipes 1 & 3
            else
            {
                PipeBotLeft_LeftCoordinate = rightSideOfScreen;
                PipeTipBotLeft_LeftCoordinate = rightSideOfScreen - pipeTipOffset;
                int pipeBotLeft_Height = generatePipeHeight();
                PipeBotLeft_TopCoordinate = pipeBotLeft_Height;
                PipeTipBotLeft_TopCoordinate = pipeBotLeft_Height; 

                PipeTopLeft_LeftCoordinate = rightSideOfScreen;
                PipeTipTopLeft_LeftCoordinate = rightSideOfScreen - pipeTipOffset; 
                int pipeTopLeft_Height = CanvasHeight - pipeBotLeft_Height + spaceBetweenPipes;
                PipeTopLeft_BotCoordinate = pipeTopLeft_Height;
                PipeTipTopLeft_BotCoordinate = pipeTopLeft_Height; 

                score++;
            }

            // Scroll pipes 2 & 4.
            if (PipeBotRight_LeftCoordinate > leftSideOfScreen)
            {
                PipeBotRight_LeftCoordinate = PipeBotRight_LeftCoordinate - pipeLeftSpeed;
                PipeTipBotRight_LeftCoordinate = PipeTipBotRight_LeftCoordinate - pipeLeftSpeed;
                PipeTopRight_LeftCoordinate = PipeTopRight_LeftCoordinate - pipeLeftSpeed;
                PipeTipTopRight_LeftCoordinate = PipeTipTopRight_LeftCoordinate - pipeLeftSpeed;
            }
            // Reset pipes 2 & 4.
            else
            {
                PipeBotRight_LeftCoordinate = rightSideOfScreen;
                PipeTipBotRight_LeftCoordinate = rightSideOfScreen - pipeTipOffset;
                int pipeBotRight_Height = generatePipeHeight();
                PipeBotRight_TopCoordinate = pipeBotRight_Height;
                PipeTipBotRight_TopCoordinate = pipeBotRight_Height;

                PipeTopRight_LeftCoordinate = rightSideOfScreen;
                PipeTipTopRight_LeftCoordinate = rightSideOfScreen - pipeTipOffset;
                int pipeTopRight_Height = CanvasHeight - pipeBotRight_Height + spaceBetweenPipes;
                PipeTopRight_BotCoordinate = pipeTopRight_Height;
                PipeTipTopRight_BotCoordinate = pipeTopRight_Height; 

                score++; 
            }
        }
    }
}
