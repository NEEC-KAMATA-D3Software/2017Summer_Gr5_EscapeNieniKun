using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MyLib.Device;

namespace Action.Scene
{
    class Pitfall : GameObject
    {
        public Pitfall(Vector2 position, GameDevice gameDevice)
            :base("black", position, 32, 32, gameDevice)
        {
        }

        public Pitfall(Pitfall other)
            :this(other.position, other.gameDevice)
        {}

        public override object Clone()
        {
            return new Pitfall(this);
        }

        public override void Hit(GameObject gameObject)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
