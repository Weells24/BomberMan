using System;
using Tao.Sdl;
using System.Collections.Generic;

namespace BomberMan
{
    class SettingScreen : Screen
    {
        bool exit = false;
        Image imgSetting, imgChosenOption;
        int chosenOption = 1;
        Audio audio;
        Font font;
        IntPtr fontMap;
        string[] map = { "FireMap", "WaterMap", "TreeMap" };
        int count = 0;
        GameScreen game;
        Sdl.SDL_Color white;

        public SettingScreen(Hardware hardware) : base(hardware)
        {
            exit = false;
            audio = new Audio(44100, 2, 4096);
            audio.AddWAV("music/reset.wav");
            font = new Font("font/Joystix.ttf", 28);
            imgSetting =
                    new Image("imgs/SettingsScreen.png", 800, 700);
            imgChosenOption =
                    new Image("imgs/select.png", 40, 35);
            game = new GameScreen(hardware);
            imgSetting.MoveTo(0, 0);
            imgChosenOption.MoveTo(160, 230);
            white = new Sdl.SDL_Color(255, 255, 255);
        }


        public override void Show()
        {
            bool enterPressed = false;
            bool escPressed = false;

            do
            {
                hardware.ClearScreen();
                hardware.DrawImage(imgSetting);
                hardware.DrawImage(imgChosenOption);
                hardware.WriteText(fontMap, 360, 230);
                hardware.UpdateScreen();

                int keyPressed = hardware.KeyPressed();
                if (keyPressed == Hardware.KEY_UP && chosenOption > 1)
                {
                    audio.PlayWAV(0, 1, 0);
                    chosenOption--;
                    imgChosenOption.MoveTo(160, (short)(imgChosenOption.Y - 180));
                }
                else if (keyPressed == Hardware.KEY_DOWN && chosenOption < 2)
                {
                    audio.PlayWAV(0, 1, 0);
                    chosenOption++;
                    imgChosenOption.MoveTo(220, (short)(imgChosenOption.Y + 180));
                }
                /* // Option 1: ADD lives & Subtract lives
                 else if (chosenOption == 1 && keyPressed == Hardware.KEY_RIGHT)
                 {
                     if (lives < 3)
                     {
                         lives++;
                         fontLives = SdlTtf.TTF_RenderText_Solid(font.GetFontType(),
                                 lives.ToString(), white);
                         hardware.WriteText(fontLives, 325, 90);
                         hardware.UpdateScreen();
                     }
                 }
                 else if (chosenOption == 1 && keyPressed == Hardware.KEY_LEFT)
                 {
                     if (lives > 0)
                     {
                         lives--;
                         fontLives = SdlTtf.TTF_RenderText_Solid(font.GetFontType(),
                                 lives.ToString(), white);
                         hardware.WriteText(fontLives, 325, 90);
                         hardware.UpdateScreen();
                     }
                 }
                 // Option 2: Time 
                 else if (chosenOption == 2 && keyPressed == Hardware.KEY_RIGHT)
                 {
                     sec++;
                     if (sec > 60)
                     {
                         min++;
                         sec = 0;
                     }
                     else if (min == 3 && sec != 0)
                         sec = 0;
                     time = min + ":" + sec;
                     fontTime = SdlTtf.TTF_RenderText_Solid(font.GetFontType(),
                             time, white);
                     hardware.WriteText(fontTime, 325, 130);
                     hardware.UpdateScreen();
                 }
                 else if (chosenOption == 2 && keyPressed == Hardware.KEY_LEFT)
                 {
                     sec--;
                     if (sec < 0 && min != 0)
                     {
                         min--;
                         sec = 60;
                     }
                     else if (min == 0 && sec < 0)
                         sec = 0;

                     time = min + ":" + sec;
                     fontTime = SdlTtf.TTF_RenderText_Solid(font.GetFontType(),
                             time, white);
                     hardware.WriteText(fontTime, 325, 130);
                     hardware.UpdateScreen();
                 }*/
                // Option 3: change Map
                else if (chosenOption == 1 && keyPressed == Hardware.KEY_RIGHT)
                {
                    if (count < 2)
                    {
                        count++;
                        fontMap = SdlTtf.TTF_RenderText_Solid(font.GetFontType(),
                                map[count], white);
                        hardware.WriteText(fontMap, 360, 230);
                        hardware.UpdateScreen();
                    }
                }
                else if (chosenOption == 1 && keyPressed == Hardware.KEY_LEFT)
                {
                    if (count > 0)
                    {
                        count--;
                        fontMap = SdlTtf.TTF_RenderText_Solid(font.GetFontType(),
                                map[count], white);
                        hardware.WriteText(fontMap, 360, 230);
                        hardware.UpdateScreen();
                    }
                }
                // Option 4: Start Game
                else if (chosenOption == 2 && keyPressed == Hardware.KEY_ENTER)
                    game.Show();

                else if (keyPressed == Hardware.KEY_ESC)
                {
                    escPressed = true;
                    exit = false;
                }
                else if (keyPressed == Hardware.KEY_ENTER)
                {
                    enterPressed = true;
                    exit = false;
                }
            }
            while (!escPressed && !enterPressed);
        }
    }
}
