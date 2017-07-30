using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using MyLib.Device;

namespace Action.Scene
{
    /// <summary>
    /// ブロック
    /// </summary>
    class SmallWood : GameObject
    {
        ///<summary>
        ///コンストラクタ
        ///</summary>
        ///<param name="position">位置</param>
        ///<param name="gameDevice">ゲームデバイス</param>
        public SmallWood(Vector2 position, GameDevice gameDevice)
            : base("small_wood", position, 128, 128, gameDevice)
        {
        }

        ///<summary>
        ///コピーコンストラクタ
        ///</summary>
        ///<param name="other"></param>
        public SmallWood(SmallWood other)
            : this(other.position, other.gameDevice)
        { }

        ///<summary>
        ///複製
        ///</summary>
        ///<returns></returns>
        public override object Clone()
        {
            return new SmallWood(this); //Blockは必須
        }

        ///<summary>
        ///衝突
        ///</summary>
        ///<param name="gameObject"></param>
        public override void Hit(GameObject gameObject)
        {
        }


        ///<summary>
        ///更新
        ///</summary>
        ///<param name="gameTime">ゲーム時間</param>
        public override void Update(GameTime gameTime)
        {
        }
    }
}
