using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class PlayerWhite : Player
{
    public PlayerWhite(): base()
        {
            SpriteXCoordinates[(int)MovableSprite.SpriteMovement.DOWN] = new int[] { 320, 360, 400 };
            SpriteYCoordinates[(int)MovableSprite.SpriteMovement.DOWN] = new int[] { 0, 0, 0 };

            SpriteXCoordinates[(int)MovableSprite.SpriteMovement.LEFT] = new int[] {200 , 240, 280 };
            SpriteYCoordinates[(int)MovableSprite.SpriteMovement.LEFT] = new int[] { 0, 0, 0 };

            SpriteXCoordinates[(int)MovableSprite.SpriteMovement.UP] = new int[] { 80, 120, 160 };
            SpriteYCoordinates[(int)MovableSprite.SpriteMovement.UP] = new int[] { 0, 0, 0 };
        
            SpriteXCoordinates[(int)MovableSprite.SpriteMovement.RIGHT] = new int[] { 440, 0, 40 };
            SpriteYCoordinates[(int)MovableSprite.SpriteMovement.RIGHT] = new int[] { 0, 40, 40 };

            UpdateSpriteCoordinates();
        }
}
