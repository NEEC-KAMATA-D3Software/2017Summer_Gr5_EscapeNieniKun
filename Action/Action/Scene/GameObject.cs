using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MyLib.Device;

namespace Action.Scene
{
    /// <summary>
    /// 当たった時の方向の列挙型定義
    /// </summary>
    enum Direction
    {
        //上、下、左、右
        Top,Bottom,Left,Right,
        UP,DOWN,LEFT,RIGHT
        //キャラ向き
    };

    /// <summary>
    /// 抽象ゲームオブジェクトクラス
    /// </summary>
    abstract class GameObject : ICloneable //コピー機能を追加
    {
        protected string name;  //アセット名
        protected Vector2 position; //位置
        protected int height;   //高さ
        protected int width;    //幅
        protected bool isDead = false;  //死亡フラグ
        protected GameDevice gameDevice;    //ゲームデバイス

        protected GameObjectID id; //個別に見分ける

        public GameObjectID GetID()
        {
            return id;
        }

        public void SetID(GameObjectID id)
        {
            this.id = id;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">アセット名</param>
        /// <param name="position">位置</param>
        /// <param name="height">高さ</param>
        /// <param name="width">幅</param>
        /// <param name="gameDevice">ゲームデバイス</param>

        public GameObject(string name,Vector2 position, int height, int width, GameDevice gameDevice)
        {
            this.name = name;
            this.position = position;
            this.height = height;
            this.width=width;
            this.gameDevice = gameDevice;
        }

        /// <summary>
        /// 位置の設定
        /// </summary>        
        /// <param name="positon"></param>
        public void SetPositon(Vector2 position)
        {
            this.position = position;
        }

        /// <summary>
        /// 位置の取得
        /// </summary>        
        /// <returns></returns>
        public Vector2 GetPosition()
        {
            return position;
        }

        //抽象メソッド
        public abstract object Clone(); //ICloneable で必ず必要
        public abstract void Update(GameTime gameTime);
        public abstract void Hit(GameObject gameObject);

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="renderer">描画オブジェクト</param>
        public virtual void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position + gameDevice.GetDisplayModify());
        }

        /// <summary>
        /// 死亡しているか？
        /// </summary>
        /// <returns></returns>
        public bool IsDead()
        {
            return isDead;
        }

        /// <summary>
        /// 矩形情報の取得
        /// </summary>
        /// <returns></returns>
        public Rectangle GetRectangle()
        {
            //矩形情報の作成
            Rectangle area = new Rectangle();

            area.X = (int)position.X;
            area.Y = (int)position.Y;
            area.Height = height;
            area.Width = width;

            return area;
        }

        /// <summary>
        /// 衝突判定
        /// </summary>
        /// <param name="otherObj"></param>
        /// <returns></returns>
        public bool Collision(GameObject otherObj)
        {
            return this.GetRectangle().Intersects(otherObj.GetRectangle());
        }

        /// <summary>
        /// 当たっている方向の向き
        /// </summary>
        /// <param name="otherObj"></param>
        /// <returns></returns>
        public Direction CheckDirection(GameObject otherObj)
        {
            //中心位置の取得
            Point thisCenter = this.GetRectangle().Center;
            Point otherCenter = otherObj.GetRectangle().Center;
            Vector2 dir =
                new Vector2(thisCenter.X, thisCenter.Y) -
                new Vector2(otherCenter.X, otherCenter.Y);
       //     dir *= -1;
            //差分からどの方角で当たっているかを調べ、値を返す
            if (Math.Abs(dir.X) > Math.Abs(dir.Y))
            {
                if (dir.X > 0)
                {
                    return Direction.Right;
                }
                else {
                    return Direction.Left;
                }
            }

            if (dir.Y > 0)
            {
                return Direction.Bottom;
            }

            //プレイヤーがブロックに乗った
            return Direction.Top;
        }
    }
}
