using System;
using System.Collections.Generic;
using Tao.Sdl;

namespace BomberMan
{
    class CreditsScreen : Screen
    {
        bool exit;
        Audio audio;
        Font font18, font24;
        IntPtr fontTexts;
        Sdl.SDL_Color green;
        Sdl.SDL_Color blue;

        protected short yText = 40;
        protected short startY = 600;
        protected bool nextName = false;
        

        public CreditsScreen(Hardware hardware) : base(hardware)
        {
            exit = false;
            font18 = new Font("font/prince_valiant.ttf", 18);
            font24 = new Font("font/prince_valiant.ttf", 24);
            green = new Sdl.SDL_Color(0, 255, 0);
            blue = new Sdl.SDL_Color(0, 0, 255);
        }
        protected string[] credits = {"Credits",
            "Original Game: Brandom Blasco",
            "Version Game: V.04",
            "Year Game: 2018",
            "Records: 100 points / Brandom Blasco",
            "  ", "remakers: ",
            "Raul Gogna",
            "  ", "Proyect Boss: Nacho Cabanes"};
        public override void Show()
        {
            bool escPressed = false;
            do
            {
                hardware.ClearScreen();
                fontTexts = SdlTtf.TTF_RenderText_Solid(font24.GetFontType(),
                                credits[0], green);
                hardware.WriteText(fontTexts, 512, 10);
                yText = 40;
                for (int i = 1; i < credits.Length; i++)
                {
                    fontTexts = SdlTtf.TTF_RenderText_Solid(font18.GetFontType(),
                        credits[i], blue);
                    hardware.WriteText(fontTexts, 500, (short)(startY + yText));
                    yText += 22;
                }
                hardware.UpdateScreen();

                int keyPressed = hardware.KeyPressed();
                hardware.Pause(20);
                if (keyPressed == Hardware.KEY_ESC)
                {
                    escPressed = true;
                }
                if (startY < -800)
                    exit = true;
                startY -= 2;
            } while (!escPressed || !exit);
        }
    }
}
