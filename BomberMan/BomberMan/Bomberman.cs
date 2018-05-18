using System;
using Tao.Sdl;

namespace BomberMan
{
    class Bomberman
    {
        static void Main(string[] args)
        {
            GameController controller = new GameController();
            controller.Start();
        }
    }
}
