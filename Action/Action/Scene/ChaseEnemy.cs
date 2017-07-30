using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MyLib.Device;
using MyLib.Utility;

namespace Action.Scene
{
    class ChaseEnemy : GameObject
    {
        private IGameObjectMediator mediator;
        private Vector2 velocity;
        private Motion motion;

        public ChaseEnemy(Vector2 position, GameDevice gameDevice, IGameObjectMediator mediator)
            :base("puddle", position, 32, 32, gameDevice)
        {
            this.mediator = mediator;
            velocity = Vector2.Zero;

            //アニメーションの設定
            motion = new Motion();
            for(int i= 0;i<6;i++) //アニメーション画像は六枚
            {
                motion.Add(i, new Rectangle(32 * i, 0, 32, 32));
            }
            Range range = new Range(0, 5); //6枚なので0～5
            Timer timer = new Timer(0.3f);
            motion.Initialize(range, timer);
        }

        public ChaseEnemy(ChaseEnemy other)
            :this(other.position, other.gameDevice, other.mediator)
        { }


        public override object Clone()
        {
            return new ChaseEnemy(this);
        }

        public override void Hit(GameObject gameObject)
        {
            Direction dir = this.CheckDirection(gameObject);
            if(gameObject is DeathBlock)
            {
                //落下による死亡
                isDead = true;
            }
            if (!(gameObject is Space))
            {
                if (dir == Direction.Top)
                {
                    position.Y = gameObject.GetRectangle().Top - this.height;
                    velocity.Y = 0.0f; //これ以上は落ちない
                }
                else if (dir == Direction.Right)
                {
                    position.X = gameObject.GetRectangle().Right;
                }
                else if (dir == Direction.Left)
                {
                    position.X = gameObject.GetRectangle().Left - this.width;
                }
                else if (dir == Direction.Bottom)
                {
                    position.X = gameObject.GetRectangle().Bottom;
                }
            }
        }

    public override void Update(GameTime gameTime)
    {
            //落下処理（プレイヤーよりゆっくり落ちる）
            velocity.Y = (velocity.Y > 8.0f) ? (8.0f) : (velocity.Y + 0.2f);

            //プレイヤーが死んでいないとき
            if (!mediator.IsPlayerDead())
            {
                //プレイヤー情報の取得
                Vector2 tempPlayerPosition = mediator.GetPlayer().GetPosition();

                velocity.X = 0.0f;

                //プレイヤーが一転範囲内(10ブロック分、320）に入ったら
                if (Vector2.Distance(position, tempPlayerPosition) <= 320)
                {
                    velocity.X = 1.0f; //右移動を予定
                    if (tempPlayerPosition.X < position.X)
                    {
                        //進行方向を変える
                        velocity.X = -1.0f;
                    }
                }
                //移動計算
                position = position + velocity;
            }

            motion.Update(gameTime);
    }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position + gameDevice.GetDisplayModify(), motion.DrawinRange());
        }

    }

}
