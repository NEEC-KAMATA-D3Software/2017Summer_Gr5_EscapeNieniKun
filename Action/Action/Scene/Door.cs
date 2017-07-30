using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using MyLib.Device;

namespace Action.Scene
{
    class Door : GameObject
    {
        private bool status; //開閉状態、false : 閉じてる

        public Door(string name, Vector2 position,GameDevice gameDevice)
            : base(name, position, 32, 32, gameDevice)
        {
            status = false; //開いていない
        }

        public Door(Door other)
            : this(other.name, other.position, other.gameDevice)
        { }

        public void Operation(bool status)
        {
            this.status = status;
        }

        public bool GetStatus()
        {
            return status;
        }

        public void Flip()
        {
            status = !status; //状態の入れ替え
        }


        public override void Draw(Renderer renderer)
        {
            //開いているときは表示しない
            if (status)
            {
                return;
            }
            //標準の描画を利用
            base.Draw(renderer);
        }

        public override object Clone()
        {
            return new Door(this);
        }

        public override void Hit(GameObject gameObject)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
