using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MyLib.Device;
using Action.Def;
using MyLib.Utility;
using Action.Scene;

namespace Action.Scene
{
    class Ten : GameObject
    {
        private Vector2 position2;
        private IGameObjectMediator mediator;
        private Vector2 playerPosition;

        private Vector2 velocity;
        private float x, y = Screen.Height;
        private float rotation;

        float angle = 0; //回転角度

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="x">position.Xの値</param>
        public Ten(Vector2 position, GameDevice gameDevice, IGameObjectMediator mediator)
            : base("テン", position,64,64, gameDevice)
        {
            this.mediator = mediator;
            //this.position = position;
            x = position.X;
            y = position.Y;
            Initialize();
            
        }

        /// <summary>
        /// コピーコンストラクタ
        /// </summary>
        /// <param name="other"></param>
        public Ten(Ten other)
            :this(other.position,other.gameDevice,other.mediator)
        { }

        public override object Clone()
        {
            return new Ten(this);
        }

        public void Initialize()
        { 
            position = new Vector2(x,y); 
            
            rotation = 270.0f;
        }

        public override void Hit(GameObject gameObject)
        {
            Direction dir = this.CheckDirection(gameObject);
            if (gameObject is DeathBlock)
            {
                //落下による死亡
                //isDead = true;
            }
            //if (dir == Direction.Top)
            //{
            //    position.Y = gameObject.GetRectangle().Top - this.height;
            //    velocity.Y = 0.0f; // これ以上は落ちない
            //}
            //else if (dir == Direction.Right)
            //{
            //    position.X = gameObject.GetRectangle().Right;
            //}
            //else if (dir == Direction.Left)
            //{
            //    position.X = gameObject.GetRectangle().Left - this.width;
            //}
            //else if (dir == Direction.Bottom)
            //{
            //    position.X = gameObject.GetRectangle().Bottom;
            //}
        }

        //更新
        public override void Update(GameTime gameTime)
        {
            //落下処理（プレイヤーよりゆっくり落ちる）
            //velocity.Y = (velocity.Y > 8.0f) ? (8.0f) : (velocity.Y + 0.2f);

            //プレイヤーが死んでいないとき
            if (!mediator.IsPlayerDead())
            {
                //プレイヤーの情報を取得
                //playerPosition = mediator.GetPlayer().GetPosition();

                //velocity.X = 0.0f;

                //プレイヤーが一定範囲内（10ブロック分、320）に入ったら
                //if (Vector2.Distance(position,playerPosition) <= 500)
                //{
                    //キャラの円運動の回転角度(angleが360以下なら+3、超えたら-357)
                    angle = (angle < 360.0f) ? (angle + 4.0f) : (angle - 356.0f);
                    //画像の回転角度(rotationが0以上なら-3、超えたら+357)
                    rotation = (rotation > 0) ? (rotation - 4.0f) : (rotation + 356.0f);

                    //三項演算子で角度更新
                    velocity = new Vector2(
                        (float)Math.Sin(MathHelper.ToRadians(angle)) * 16, (float)Math.Cos(MathHelper.ToRadians(angle)) * 30);

                    //基本の動き
                    position += velocity;
                //}
            }
        }

        //描画
        public override void Draw(Renderer renderer)
        {
            //if (Vector2.Distance(position,playerPosition) <= 320)
            //{
                //円運動しながら画像が回転
                renderer.DrawTexture(name, position + gameDevice.GetDisplayModify(), new Rectangle(0, 0, 64, 64),
                    MathHelper.ToRadians(rotation),new Vector2(32,32));
            //}
        }
    }
}
