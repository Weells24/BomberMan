using System;
using Tao.Sdl;

namespace BomberMan
{
    class GameScreen : Screen
    {
        Image imgInfo, imgFloor;
        Audio audio;
        Font font40, font28;
        Level level;
        IntPtr fontLives, fontTime;
        Sdl.SDL_Color white;
        int lives;
        // initializing time
        int min = 3;
        int sec = 0;
        string time;



        public GameScreen(Hardware hardware) : base(hardware)
        {
            // preload text
            font28 = new Font("font/Joystix.ttf", 28);
            font40 = new Font("font/Joystix.ttf", 28);
            fontLives = SdlTtf.TTF_RenderText_Solid(font28.GetFontType(),
                                lives.ToString(), white);
            //fontTime = SdlTtf.TTF_RenderText_Solid(font40.GetFontType(),
                                //time, white);
            white = new Sdl.SDL_Color(255, 255, 255);
            // preload images
            imgInfo = new Image("imgs/InfoPanel.png", 800, 75);
            imgInfo.MoveTo(0, 0);
            imgFloor = new Image("imgs/Floor.png", 800, 700);
            imgFloor.MoveTo(0, 75);
            // preload level
            level = new Level("levels/level1.txt");
            
            //audio = new Audio(44100, 2, 4096);
            //audio.AddMusic("music/BombermanNES.mp3");

            //initializing lives
            lives = 3;
        }

        /*public string Time(ref int min, ref int sec)
        {
            sec--;
            if (sec < 0 && min != 0)
            {
                min = min--;
                sec = 59;
            }
            else if (min == 0 && sec < 0)
                sec = 0;

            return time = min + ":" + sec;
        }*/

        public override void Show()
        {
            bool enterPressed = false;
            bool escPressed = false;
            level = new Level("levels/level1.txt");
            
            //audio.PlayMusic(0, -1);

            do
            {
                // 1. Draw Map
                hardware.ClearScreen();
                hardware.DrawImage(imgInfo);
                hardware.DrawImage(imgFloor);
                //Time(ref min, ref sec);
                hardware.WriteText(fontTime, 480, 40);
                hardware.WriteText(fontLives, 158, 17);
                foreach (Brick brick in level.Bricks)
                    hardware.DrawSprite(Sprite.spritesheet, (short)(brick.X - level.XMap), (short)(brick.Y - level.YMap), brick.SpriteX, brick.SpriteY, Sprite.SPRITE_WIDTH, Sprite.SPRITE_HEIGHT);
                foreach (BrickDestroyable bdes in level.BricksDestroyable)
                    hardware.DrawSprite(Sprite.spritesheet, (short)(bdes.X - level.XMap), (short)(bdes.Y - level.YMap), bdes.SpriteX, bdes.SpriteY, Sprite.SPRITE_WIDTH, Sprite.SPRITE_HEIGHT);
                hardware.UpdateScreen();

                int keyPressed = hardware.KeyPressed();
                if (keyPressed == Hardware.KEY_ESC)
                {
                    escPressed = true;
                }
            }
            while (!escPressed && !enterPressed && Convert.ToInt32(time) == 0);
            
            //audio.StopMusic();
        }
    }
}
