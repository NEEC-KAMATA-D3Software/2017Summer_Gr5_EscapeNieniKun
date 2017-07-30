using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MyLib.Device;

namespace Action.Scene
{
    /// <summary>
    /// タイトルクラス
    /// </summary>
    class Ending : IScene
    {
        private Renderer renderer;
        private InputState input;//入力処理用オブジェクト
        private bool endFlag; //終了フラグ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="gameDevice"></param>
        public Ending(GameDevice gameDevice)
        {
            input = gameDevice.GetInputState();
            endFlag = false;
        }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="renderer"></param>
        public void Draw(Renderer renderer)
        {
            //描画開始
            renderer.Begin();

            //この間に描画処理を書く
            renderer.DrawTexture("harinezumi_title", Vector2.Zero);
            renderer.DrawTexture("HIT_SPACE", new Vector2(190,150));

            //描画終了
            renderer.End();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            endFlag = false;
        }

        /// <summary>
        /// タイトルシーン終了か？
        /// </summary>
        /// <returns></returns>
        public bool IsEnd()
        {
            return endFlag;
        }

        /// <summary>
        /// タイトルシーンの次のシーン名を取得
        /// </summary>
        /// <returns></returns>
        public SceneType Next()
        {
            return SceneType.Load;
        }

        /// <summary>
        /// シーン終了処理
        /// </summary>
        public void Shutdown()
        {
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            //スペースキーが押されたらシーン終了
            if (input.GetKeyTrigger(Keys.Space))
            {
                endFlag = true;
            }
        }
    }
}
