using System;
using Tao.Sdl;
using System.Threading;

class GameScreen : Screen
{
    Player playerWhite;
    Image imgInfo, imgFloor;
    Font font36, font28;
    Level level;
    IntPtr fontLives, fontTime;
    Sdl.SDL_Color white;
    int lives;

    public GameScreen(Hardware hardware) : base(hardware)
    {
        // preload text
        font28 = new Font("font/Joystix.ttf", 28);
        font36 = new Font("font/Joystix.ttf", 36);

        white = new Sdl.SDL_Color(255, 255, 255);
        fontLives = SdlTtf.TTF_RenderText_Solid(font28.GetFontType(),
                            lives.ToString(), white);

        // preload images
        imgFloor = new Image("imgs/Floor.png", 840, 680);
        imgFloor.MoveTo(0, 0);
        imgInfo = new Image("imgs/InfoPanel.png", 840, 75);
        imgInfo.MoveTo(0, 680);
        
        // preload level
        level = new Level("levels/level1.txt");
        
        
        playerWhite = new PlayerWhite();
    }

    private void moveCharacter()
    {
        bool left = hardware.IsKeyPressed(Hardware.KEY_LEFT);
        bool right = hardware.IsKeyPressed(Hardware.KEY_RIGHT);
        bool up = hardware.IsKeyPressed(Hardware.KEY_UP);
        bool down = hardware.IsKeyPressed(Hardware.KEY_DOWN);

        if (up)
        {
            if (playerWhite.Y > 0)
            {
                playerWhite.Y -= Player.STEP_LENGTH;
                if (level.YMap > 0)
                    level.YMap -= Player.STEP_LENGTH;
            }
        }
        if (down)
        {
            if (playerWhite.Y < level.Height - Sprite.SPRITE_HEIGHT)
            {
                playerWhite.Y += Player.STEP_LENGTH;
                if (level.YMap < level.Height - GameController.SCREEN_HEIGHT)
                    level.YMap += Player.STEP_LENGTH;
            }
        }
        if (left)
        {
            if (playerWhite.X > 0)
            {
                playerWhite.X -= Player.STEP_LENGTH;
                if (level.XMap > 0)
                    level.XMap -= Player.STEP_LENGTH;
            }
        }
        if (right)
        {
            if (playerWhite.X < level.Width - Sprite.SPRITE_WIDTH)
            {
                playerWhite.X += Player.STEP_LENGTH;
                if (level.XMap < level.Width - GameController.SCREEN_WIDTH)
                    level.XMap += Player.STEP_LENGTH;
            }
        }

        if (left)
            playerWhite.Animate(MovableSprite.SpriteMovement.LEFT);
        else if (right)
            playerWhite.Animate(MovableSprite.SpriteMovement.RIGHT);
        else if (up)
            playerWhite.Animate(MovableSprite.SpriteMovement.UP);
        else if (down)
            playerWhite.Animate(MovableSprite.SpriteMovement.DOWN);
    }

    public void DecreaseTime(Object o)
    {
        int min = 3;
        int sec = 0;

        sec--;
        if (sec < 0 && min != 0)
        {
            min = min--;
            sec = 59;
        }
        else if (min == 0 && sec < 0)
            sec = 0;

        fontTime = SdlTtf.TTF_RenderText_Solid(font36.GetFontType(), 
                min + ":" + sec, white);
    }

    public override void Show()
    {
        short oldX, oldY, oldXMap, oldYMap;
        bool enterPressed = false;
        bool escPressed = false;
        level = new Level("levels/level1.txt");
        playerWhite.MoveTo(40, 40);
        var timer = new Timer(this.DecreaseTime, null, 1000, 1000);

        //audio.PlayMusic(0, -1);

        do
        {
            // 1. Draw Map
            hardware.ClearScreen();
            hardware.DrawImage(imgInfo);
            hardware.DrawImage(imgFloor);
            hardware.WriteText(fontTime, 360, 700);
            hardware.WriteText(fontLives, 158, 700);
            foreach (Brick brick in level.Bricks)
                hardware.DrawSprite(Sprite.spritesheet, (short)(brick.X - level.XMap), (short)(brick.Y - level.YMap), brick.SpriteX, brick.SpriteY, Sprite.SPRITE_WIDTH, Sprite.SPRITE_HEIGHT);
            foreach (BrickDestroyable bdes in level.BricksDestroyable)
                hardware.DrawSprite(Sprite.spritesheet, (short)(bdes.X - level.XMap), (short)(bdes.Y - level.YMap), bdes.SpriteX, bdes.SpriteY, Sprite.SPRITE_WIDTH, Sprite.SPRITE_HEIGHT);
            hardware.DrawSprite(Sprite.spritesheet, (short)(playerWhite.X - level.XMap), (short)(playerWhite.Y - level.YMap), playerWhite.SpriteX, playerWhite.SpriteY, Sprite.SPRITE_WIDTH, Sprite.SPRITE_HEIGHT);
            hardware.UpdateScreen();

            int keyPressed = hardware.KeyPressed();
            if (keyPressed == Hardware.KEY_ESC)
            {
                escPressed = true;
            }

            // 2.  Move character from keyboard input
            oldX = playerWhite.X;
            oldY = playerWhite.Y;
            oldXMap = level.XMap;
            oldYMap = level.YMap;
            moveCharacter();

            // 3.  Check collisions and update game state
            if (playerWhite.CollidesWith(level.Bricks) ||
                playerWhite.CollidesWith(level.BricksDestroyable))
            {
                playerWhite.X = oldX;
                playerWhite.Y = oldY;
                level.XMap = oldXMap;
                level.YMap = oldYMap;
            }

            //Pause Game
            Thread.Sleep(10);
        }
        while (!escPressed && !enterPressed);
        timer.Dispose();
        //audio.StopMusic();
    }
}
