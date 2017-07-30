using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MyLib.Device;

namespace Action.Scene
{
    class SlidingBlock_UD : GameObject
    {
        private Vector2 downPosition; //左端
        private Vector2 upPosition; //右端
        private Vector2 velocity; //移動量

        public SlidingBlock_UD(Vector2 downPosition, Vector2 upPosition, GameDevice gameDevice)
            : base("block00", downPosition, 32, 32, gameDevice)
        {
            //左端から移動開始
            this.downPosition = downPosition;
            this.upPosition = upPosition;
            velocity = new Vector2(0.0f, 1.0f); //最初は右移動
        }

        public SlidingBlock_UD(SlidingBlock_UD other)
            : this(other.downPosition, other.upPosition, other.gameDevice)
        { }

        public Vector2 GetVelocity()
        {
            return velocity;
        }

        public override object Clone()
        {
            return new SlidingBlock_UD(this);
        }

        public override void Hit(GameObject gameObject)
        {
        }

        public override void Update(GameTime gameTime)
        {
            position = position + velocity; //移動
            //折り返し処理
            if (position.Y > upPosition.Y)
            {
                position.Y = upPosition.Y;
                velocity.Y = -velocity.Y; //向き反転
            }
            if (position.Y < downPosition.Y)
            {
                position.Y = downPosition.Y;
                velocity.Y = -velocity.Y; //向き反転
            }
        }
    }
}
