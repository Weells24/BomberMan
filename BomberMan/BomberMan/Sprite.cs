
namespace BomberMan
{
    class Sprite
    {
        public const short SPRITE_WIDTH = 40;
        public const short SPRITE_HEIGHT = 40;

        public static Image spritesheet =
            new Image("imgs/spritesheetbom.png",310,262);
            
        public short X { get; set; }
        public short Y { get; set; }
        public short SpriteX { get; set; }
        public short SpriteY { get; set; }

        public void MoveTo(short x, short y)
        {
            X = x;
            Y = y;
        }
    }
}
