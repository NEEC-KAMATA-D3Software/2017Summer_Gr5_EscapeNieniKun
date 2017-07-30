using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MyLib.Device;

namespace Action.Scene
{
    class SlidingBlock : GameObject
    {
        private Vector2 leftPosition; //左端
        private Vector2 rightPosition; //右端
        private Vector2 velocity; //移動量

        public SlidingBlock( Vector2 leftPosition, Vector2 rightPosition, GameDevice gameDevice)
            :base("block00", leftPosition, 32, 32, gameDevice)
        {
            //左端から移動開始
            this.leftPosition = leftPosition;
            this.rightPosition = rightPosition;
            velocity = new Vector2(1.0f, 0.0f); //最初は右移動
        }

        public SlidingBlock(SlidingBlock other)
            : this(other.leftPosition, other.rightPosition, other.gameDevice)
        { }

        public Vector2 GetVelocity()
        {
            return velocity;
        }

        public override object Clone()
        {
            return new SlidingBlock(this);
        }

        public override void Hit(GameObject gameObject)
        {
        }

        public override void Update(GameTime gameTime)
        {
            position = position + velocity; //移動
            //折り返し処理
            if (position.X > rightPosition.X)
            {
                position.X = rightPosition.X;
                velocity.X = -velocity.X; //向き反転
            }
            if (position.X < leftPosition.X)
            {
                position.X = leftPosition.X;
                velocity.X = -velocity.X; //向き反転
            }
        }
    }
}
