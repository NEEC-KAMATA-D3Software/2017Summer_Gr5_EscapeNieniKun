using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input; //キー入力
using MyLib.Device;
using Action.Def;
using MyLib.Utility;

namespace Action.Scene
{
    class Player : GameObject
    {
        private InputState input;

        private Vector2 velocity; //移動量（Yだけ使用)
        private bool isJump; //ジャンプの状態管理
        private Motion motion;  //アニメーション管理
        private IGameObjectMediator mediator;
        private Vector2 gravity;
        private Timer stamina;

        private Direction dir;
        private Vector2 slideModify; //移動ブロック同調用変数
        private static Vector2 center;

        float dash = 10.0f;

        ///<summary>
        ///コンストラクタ
        ///</summary>
        ///<param name="position">位置</param>
        ///<param name="gameDevice">ゲームデバイス</param>
        public Player(Vector2 position, GameDevice gameDevice, IGameObjectMediator mediator)
            : base("ハリネズミ　アニメーション　２", position, 32, 32, gameDevice)
        {
            this.mediator = mediator;
            this.input = gameDevice.GetInputState();
            velocity = Vector2.Zero;
            isJump = true; //ジャンプ中
            gravity = new Vector2(0.0f, 0.5f);
            slideModify = Vector2.Zero; //初期化
            stamina = new Timer(5.0f);
        }

        ///<summary>
        ///コピーコンストラクタ
        ///</summary>
        ///<param name="other">コピー元player</param>
        public Player(Player other)
            : this(other.position, other.gameDevice, other.mediator)
        { }

        ///<summary>
        ///複製
        ///</summary>
        ///<returns>複製したplayer</returns>
        public override object Clone()
        {
            return true;
        }

        ///<summary>
        ///初期化
        ///</summary>
        public void Initialize()
        {
            isDead = false;
            position = new Vector2(64 * 10, 64 * 17);

            //モーションの実体生成と描画範囲を追加
            motion = new Motion();
            //全向き
            for (int i = 0; i < 16; i++) motion.Add(i, new Rectangle(32 * (i % 4), 32 * (i / 4), 32, 32));
            motion.Initialize(new Range(0, 15), new Timer(0.2f));

            //最初は下向き
            dir = Direction.DOWN;
        }

        ///<summary>
        ///衝突
        ///</summary>
        ///<param name="gameObject">確認したいオブジェクト</param>
        public override void Hit(GameObject gameObject)
        {
            //どの向きで当たっているか取得
            dir = this.CheckDirection(gameObject);

            if (gameObject is DeathBlock)
            {
                isDead = true;
            }

            if (gameObject is Pitfall)
            {
                isDead = true;
            }

            else if (gameObject is Door && ((Door)gameObject).GetStatus())
            {
            }

            //ブロックと当たっているとき
            else if (!(gameObject is Space))
            {
                if (dir == Direction.Top) //上
                {
                    //プレイヤーがブロックに乗った
                    if (position.Y > 0.0f) //降下中の時、ジャンプ状態終了
                    {
                        position.Y = gameObject.GetRectangle().Top - this.height;
                        velocity.Y = 0.0f;
                        isJump = false;
                    }
                }
                else if (dir == Direction.Right) //右
                {
                    position.X = gameObject.GetRectangle().Right;
                    isJump = false;
                }
                else if (dir == Direction.Left) //左
                {
                    position.X = gameObject.GetRectangle().Left - this.width;
                    isJump = false;
                }
                else if (dir == Direction.Bottom) //下
                {
                    position.Y = gameObject.GetRectangle().Bottom;
                    isJump = false;
                }

                if (position.X <= 0) position.X = 0;
                if (position.Y < 0) position.Y = 1;

                //プレイヤーの位置を画面の中心に設定
                SetDisplayModify();
            }


            //移動ブロック関連処理
            //移動ブロックの上に乗ったとき
            if (gameObject is SlidingBlock && dir == Direction.Top)
            {
                //一緒に行動する
                slideModify = ((SlidingBlock)gameObject).GetVelocity();
            }

            else if (!(gameObject is SlidingBlock)) //移動ブロック以外に接触したら
            {
                //移動しない
                slideModify = Vector2.Zero;
            }


        }

        ///<summary>
        ///衝突
        ///</summary>
        ///<param name="gameTime">ゲーム時間</param>
        public override void Update(GameTime gameTime)
        {
            //ジャンプ処理(上下の移動量の計算)
            //ジャンプしていないときに、Bボタンまたはスペースキーが押されたらジャンプ開始
            if ((isJump == false) && ((input.GetKeyTrigger(Buttons.B) || input.GetKeyTrigger(Keys.Space))))
            {
                velocity.Y = -15.0f; //移動量を上に
                isJump = true; //ジャンプ中に
            }
            else //空中にいるとき
            {
                //ジャンプ中だけ落下
                velocity.Y += gravity.Y;//ちょっとずつ下へ
                //落下速度制限(画像の大きさの半分を超えて落下させない)
                velocity.Y = (velocity.Y > 20.0f) ? (20.0f) : (velocity.Y);
                //isJump = true;
            }


            //左右移動処理
            float speed = 4.0f; //通常時の早さ
            if ((input.GetKeyState(Buttons.A) || input.GetKeyState(Keys.Z)) && (!stamina.IsTime()))
            {   //ダッシュ時の早さ
                speed += dash;
                if (dash >= 0.0f) dash -= 0.2f;
                stamina.Update();
            }
            else
            {
                if (dash <= 10.0f) dash += 0.1f;
            }

            if (!(input.GetKeyState(Keys.Z)))
            {
                stamina.Initialize();
            }


            //左右の移動量計算
            velocity.X = input.Velocity().X * speed; //左右だけ

            //位置の計算
            //position.X += velocity.X;
            //position.Y += velocity.Y;
            position = position + velocity + slideModify; //移動ブロックに同調させる

            //プレイヤーの位置を画面の中心に設定
            var vel = input.Velocity();
            UpdateMotion(vel);
            SetDisplayModify();
            motion.Update(gameTime);
            center = new Vector2(position.X + 16, position.Y + 16);
        }

        private void SetDisplayModify()
        {
            gameDevice.SetDisplayModify(new Vector2(-position.X + (Screen.Width / 2 - width / 2), -position.Y + (Screen.Height / 2 - width / 2)));
        }

        ///<summary>
        ///モーションの更新
        ///</summary>
        ///<param name="velocity"></param>
        private void UpdateMotion(Vector2 velocity)
        {
            //キー入力がなければ終了
            if (velocity.Length() <= 0.0f)
            {
                return;
            }
            Timer timer = new Timer(0.2f);
            if ((input.GetKeyState(Buttons.A) || input.GetKeyState(Keys.Z)) && (!stamina.IsTime()))
            {
                dir = Direction.DOWN;
                motion.Initialize(new Range(12, 15), timer);
            }
            else if ((velocity.X > 0.0f) && (dir != Direction.RIGHT))
            {
                dir = Direction.RIGHT;
                motion.Initialize(new Range(4, 7), timer);
            }
            else if ((velocity.X < 0.0f) && (dir != Direction.LEFT))
            {
                dir = Direction.LEFT;
                motion.Initialize(new Range(8, 11), timer);
            }
            else if((velocity.X == 0.0f) && (dir != Direction.RIGHT) && (dir != Direction.LEFT))
            {
                dir = Direction.DOWN;
                motion.Initialize(new Range(0, 3), timer);
            }

        }

        ///<summary>
        ///CharacterクラスのDrawメソッドに代わって描画
        ///</summary>
        ///<param name="renderer"></param>
        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position + gameDevice.GetDisplayModify(), motion.DrawinRange());
        }

        public bool DeadJudge()
        {
            return isDead;
        }

        public Vector2 GetCenter()
        {
            return center;
        }

        public Timer GetStaminaTime()
        {
            return stamina;
        }
    }
}
