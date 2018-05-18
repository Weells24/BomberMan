using System;
using Tao.Sdl;

namespace BomberMan
{
    class GameScreen : Screen
    {
        Image imgInfo, imgFloor;
        Audio audio;
        Font font;
        Level level;
        IntPtr fontLives, fontTime;
        Sdl.SDL_Color white;

        int lives;


        public GameScreen(Hardware hardware) : base(hardware)
        {
            font = new Font("font/prince_valiant.ttf", 28);
            imgInfo = new Image("imgs/InfoPanel.png", 800, 75);
            imgFloor = new Image("imgs/Floor.png", 800, 700);
            level = new Level("levels/level1.txt");
            white = new Sdl.SDL_Color(255, 255, 255);
            //audio = new Audio(44100, 2, 4096);
            //audio.AddMusic("music/BombermanNES.mp3");
            imgInfo.MoveTo(0, 0);
            imgFloor.MoveTo(0, 75);
            lives = 3;
        }

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
                fontLives = SdlTtf.TTF_RenderText_Solid(font.GetFontType(),
                                lives.ToString(), white);
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
            while (!escPressed && !enterPressed);
            
            //audio.StopMusic();
        }
    }
}
