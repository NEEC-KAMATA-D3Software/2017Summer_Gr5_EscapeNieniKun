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
    class BigWood : GameObject
    {
        ///<summary>
        ///コンストラクタ
        ///</summary>
        ///<param name="position">位置</param>
        ///<param name="gameDevice">ゲームデバイス</param>
        public BigWood(Vector2 position, GameDevice gameDevice)
            : base("big_wood", position, 640, 640, gameDevice)
        {
        }

        ///<summary>
        ///コピーコンストラクタ
        ///</summary>
        ///<param name="other"></param>
        public BigWood(BigWood other)
            : this(other.position, other.gameDevice)
        { }

        ///<summary>
        ///複製
        ///</summary>
        ///<returns></returns>
        public override object Clone()
        {
            return new BigWood(this); //Blockは必須
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
