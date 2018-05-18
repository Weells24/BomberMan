using System;
using Tao.Sdl;

namespace BomberMan
{
    class WelcomeScreen : Screen
    {
        bool exit;
        Image imgWelcome, imgChosenOption;
        //add
        int chosenOption = 1;
        Audio audio;

        public WelcomeScreen(Hardware hardware) : base(hardware)
        {
            exit = false;
            audio = new Audio(44100, 2, 4096);
            audio.AddWAV("music/reset.wav");
            imgWelcome =
                    new Image("imgs/MenuPrincipal.png", 800, 700);
            imgChosenOption =
                    new Image("imgs/BombMenu1.png", 50, 50);
            imgWelcome.MoveTo(0, 0);
            imgChosenOption.MoveTo(240, 390);
        }

        public override void Show()
        {
            //add
            bool enterPressed = false;
            bool escPressed = false;

            do
            {
                hardware.ClearScreen();
                hardware.DrawImage(imgWelcome);
                hardware.DrawImage(imgChosenOption);
                hardware.UpdateScreen();

                int keyPressed = hardware.KeyPressed();
                if (keyPressed == Hardware.KEY_UP && chosenOption > 1)
                {
                    audio.PlayWAV(0, 1, 0);
                    chosenOption--;
                    imgChosenOption.MoveTo(240, (short)(imgChosenOption.Y - 45));
                }
                else if (keyPressed == Hardware.KEY_DOWN && chosenOption < 5)
                {
                    audio.PlayWAV(0, 1, 0);
                    chosenOption++;
                    imgChosenOption.MoveTo(240, (short)(imgChosenOption.Y + 45));
                }
                else if (keyPressed == Hardware.KEY_ESC)
                {
                    escPressed = true;
                    exit = true;
                }
                else if (keyPressed == Hardware.KEY_ENTER)
                {
                    enterPressed = true;
                    exit = false;
                }
            }
            while (!escPressed && !enterPressed);
        }

        public bool GetExit()
        {
            return exit;
        }

        public int GetChosenOption()
        {
            return chosenOption;
        }


    }
}
