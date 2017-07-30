using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MyLib.Device;

namespace Action.Scene
{
    class DeathBlock : GameObject
    {
        public DeathBlock(Vector2 position, GameDevice gameDevice)
            :base("water", position, 32, 32, gameDevice)
        {
        }

        public DeathBlock(DeathBlock other)
            :this(other.position, other.gameDevice)
        { }

        public override object Clone()
        {
            return new DeathBlock(this);
        }

        public override void Hit(GameObject gameObject)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }

        //public override void Draw(Renderer renderer)
        //{
        //    //表示なし
        //}
    }
}
