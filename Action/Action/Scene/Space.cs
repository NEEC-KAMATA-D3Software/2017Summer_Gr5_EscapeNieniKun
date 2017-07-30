using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using MyLib.Device;

namespace Action.Scene
{
    /// <summary>
    /// ゲームオブジェクト抽象クラスを継承した
    /// スペースクラス
    /// </summary>
    class Space : GameObject
    {
        ///<summary>
        ///コンストラクタ
        ///</summary>
        ///<param name="Position">位置</param>
        ///<param name="gameDevice">ゲームデバイス</param>
        public Space(Vector2 position, GameDevice gameDevice)
            : base("", position, 32, 32, gameDevice)
        {
        }

        ///<summary>
        ///コピーコンストラクタ
        ///</summary>
        ///<param name="other"></param>
        public Space(Space other)
            : this(other.position, other.gameDevice) //自分のコンストラクタ呼び出し
        { }

        ///<summary>
        ///複製
        ///</summary>
        ///<returns></returns>
        public override object Clone()
        {
            return new Space(this);
        }

        ///<summary>
        ///衝突
        ///</summary>
        ///<param name="gameObject">相手のゲームオブジェクト</param>
        public override void Hit(GameObject gameObject)
        {
        }

        ///<summary>
        ///更新
        ///</summary>
        ///<param name="gameObject">ゲーム時間</param>
        public override void Update(GameTime gameTime)
        {
        }

        ///<summary>
        ///描画
        ///</summary>
        ///<param name="gameObject">描画オブジェクト</param>
        public override void Draw(Renderer renderer)
        {
            //表示なし
        }

    }
}
