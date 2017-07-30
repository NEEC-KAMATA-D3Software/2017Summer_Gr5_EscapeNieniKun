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
    class Yamaneko : GameObject
    {
        private Player player;
        private IGameObjectMediator mediator;
        private InputState inputState;
        private Motion motion;

        private Vector2 playerPosition;
        private Vector2 playerCenter;
        private Vector2 velocity;
        private Vector2 mySpace;
        private Vector2 center;
        private Vector2 velo2;

        private float x, max_Y, speed = 3, rightRadius, leftRadius,
                    radius = 130, checkPosition, size = 64 ;
        private int count = 0;
        private bool nearMove, back, end = true, stop, jamp1, jamp2, run1, run2;

        //private enum Direction
        //{ Left, Right };

        private Direction direction;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="input"></param>
        public Yamaneko(Vector2 position, GameDevice gameDevice, IGameObjectMediator mediator, Player player)
            : base("ヤマネコ　アニメーション",position,64,64,gameDevice)
        {
            inputState = gameDevice.GetInputState();
            x = position.X;
            Initialize();
            this.mediator = mediator;
            this.player = player;
            
        }

        /// <summary>
        /// コピーコンストラクタ
        /// </summary>
        /// <param name="other"></param>
        public Yamaneko(Yamaneko other)
            :this(other.position,other.gameDevice, other.mediator,other.player)
        { }

        public override object Clone()
        {
            return new Yamaneko(this);
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            max_Y = 32 * 37;
           position = new Vector2(x, max_Y);
            motion = new Motion();
            

            //モーション追加
            for (int i = 0, n = 0, g = 0, count = 0; i <= 15; i++, n++, count++)
            {
                if (n >= 4) n = 0;
                if (count >= 4)
                {
                    g++;
                    count = 0;
                }
                motion.Add(i, new Rectangle(64 * n, 64 * g, 64, 64));
            }

            Timer timer = new Timer(0.2f);
            //最初は左向き
            direction = Direction.Left;
            motion.Initialize(new Range(0, 3), timer);
        }

        //更新
        public override void Update(GameTime gameTime)
        {
            motion.Update(gameTime);
            MotionUpdate(velocity);

            if (!mediator.IsPlayerDead())
            {
                velocity.Y += 5;

                //velocity.Y = (velocity.Y > 12.0f) ? (12.0f) : (velocity.Y + 0.5f);
                //if (position.Y >= max_Y)
                //{ position.Y = max_Y; }

                //プレイヤーの位置取得
                playerPosition = mediator.GetPlayer().GetPosition();

                
                if (Vector2.Distance(position, playerPosition) <= 400)
                {
                    

                    //それぞれの値を取得
                    playerCenter =  player.GetCenter();
                    center = new Vector2(position.X + size / 2, position.Y + size / 2);
                    mySpace = playerCenter - center;
                    rightRadius = playerCenter.X + radius;
                    leftRadius = playerCenter.X - radius - 64;

                    RadiusCheck();

                    NearMove(ref back);
                    Back(ref stop);
                    Stop(ref jamp1);
                    Jamp(ref run1);
                    Run();

                }
            }
        }

        public override void Hit(GameObject gameObject)
        {
            Direction dir = this.CheckDirection(gameObject);
            if (gameObject is DeathBlock)
            {
                //落下による死亡
                isDead = true;
            }
            
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
            
        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position + gameDevice.GetDisplayModify(), motion.DrawinRange());
        }

        /// <summary>
        /// 元々してほしい動き
        /// </summary>
        /// <param name="velo"></param>
        public void Move(Vector2 velo)
        {
            velocity = velo;
            if (velocity != Vector2.Zero)
            { velocity.Normalize(); }
            position = position + velocity * speed;
        }

        /// <summary>
        /// 近づく動き
        /// </summary>
        public void NearMove(ref bool check)
        {
            if (nearMove && end)
            {
                speed = 3;
                velocity.X = mySpace.X;
                Move(velocity);

            }
            if (!nearMove && end)
            {
                end = false;
                check = true;
            }
        }

        /// <summary>
        /// 数歩下がる
        /// </summary>
        public void Back(ref bool check)
        {
            if (back && !end && position.X > playerPosition.X)
            {
                count++;
                velocity = new Vector2(7, 0);
                Move(velocity);
                if (count >= 10)
                {
                    count = 0;
                    check = true;
                    back = false;
                }
            }
            if (back && !end && position.X < playerPosition.X)
            {
                count++;
                velocity = new Vector2(-7, 0);
                Move(velocity);
                if (count >= 10)
                {
                    count = 0;
                    check = true;
                    back = false;
                }
            }
        }

        /// <summary>
        /// その場で止まる
        /// </summary>
        public void Stop(ref bool check)
        {
            if (stop && !end)
            {
                count++;
                velocity = Vector2.Zero;
                Move(velocity);
                if (count >= 10)
                {
                    count = 0;
                    stop = false;
                    check = true;
                }
            }
        }
        /// <summary>
        /// ジャンプ
        /// </summary>
        public void Jamp(ref bool check)
        {
            //positionが左側の時
            if (jamp1 && !end && position.X > playerPosition.X)
            {
                velo2 = new Vector2(-2, -3);
                Move(velo2);
                speed = 10;
                jamp1 = false;
                jamp2 = true;
            }
            //positionが右側の時
            if (jamp1 && !end && position.X < playerPosition.X)
            {
                velo2 = new Vector2(2, -3);
                Move(velo2);
                speed = 10;
                jamp1 = false;
                jamp2 = true;
            }
            if (jamp2 && !end)
            {
                count++;
                if (count >= 4)
                {
                    count = 0;
                    velo2.Y = velo2.Y + 1f;
                }
                Move(velo2);
                if (position.Y >= max_Y)
                {
                    position.Y = max_Y;
                    count = 0;
                    jamp2 = false;
                    check = true;
                    //speed = 3;

                }
            }
        }

        public void Run()
        {
            if (run1)
            {
                checkPosition = position.X;
                run1 = false;
                run2 = true;
            }

            if (run2 && !end)
            {
                if (checkPosition < playerPosition.X)
                {
                    count++;
                    velocity = new Vector2(-7, 0);
                    Move(velocity);
                    if (count >= 20)
                    {
                        count = 0;
                        run2 = false;
                        end = true;
                    }
                }
                if (checkPosition > playerPosition.X)
                {
                    count++;
                    velocity = new Vector2(7, 0);
                    Move(velocity);
                    if (count >= 20)
                    {
                        count = 0;
                        run2 = false;
                        end = true;
                    }
                }
            }
        }

        /// <summary>
        /// 半径チェック
        /// </summary>
        //position.XがplayerPositionより大きいとき"かつ"、position.Xがplayerの半径に入った時と、
        //position.XがplayerPositionより小さいとき"かつ"、position.Xがplayerの半径に入った時にfalse
        //それ以外はtrue
        public void RadiusCheck()
        {
            if ((position.X > playerPosition.X && position.X <= rightRadius)
                || (position.X < playerPosition.X && position.X >= leftRadius))
            { nearMove = false; }
            else
            { nearMove = true; }
        }

        /// <summary>
        /// モーションの方向を認識する
        /// </summary>
        /// <param name="velocity"></param>
        public void MotionUpdate(Vector2 velocity)
        {
            Timer timer = new Timer(0.2f);
            //左
            if (((position.X > playerPosition.X) && (velocity.X < 0.0f) && (direction != Direction.Left))
                || (back && velocity.X > 0.0f))
            {
                direction = Direction.Left;
                motion.Initialize(new Range(0, 3), timer);
            }
            //右
            else if (((velocity.X > 0.0f) && (direction != Direction.Right))
                || (back && velocity.X < 0.0f))
            {
                direction = Direction.Right;
                motion.Initialize(new Range(4, 7), timer);
            }
        }

        

       
    }
}
