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
    class Title : IScene
    {
        private InputState input;//入力処理用オブジェクト
        private bool endFlag; //終了フラグ
        private Vector2 title = new Vector2(170, -200);
        private Vector2 title2 = new Vector2(300, -200);
        private Vector2 nezumi = new Vector2(30,585);
        private Vector2 nezumi_velocity = new Vector2(10.0f, 0);
        private Sound sound;
        //private Timer timer;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="gameDevice"></param>
        public Title(GameDevice gameDevice)
        {
            input = gameDevice.GetInputState();
            endFlag = false;
            this.sound = gameDevice.GetSound();
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
            if (title2.Y > 216)
            {
                renderer.DrawTexture("waku", new Vector2(0, 19));
                renderer.DrawTexture("HIT_SPACE", new Vector2(190,230));
                renderer.DrawTexture("操作方法", new Vector2(190,400));
                if (nezumi_velocity.X > 0) renderer.DrawTexture("hari00", nezumi);
                if (nezumi_velocity.X < 0) renderer.DrawTexture("hari01", nezumi);
            }
            renderer.DrawTexture("dasshutu", title);
            renderer.DrawTexture("ninikun", title2);
            //描画終了
            renderer.End();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            title = new Vector2(170, -200);
            title2 = new Vector2(300, -200);
            endFlag = false;
            //timer = new Timer(20);
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
            return SceneType.GamePlay;
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
            if (title.Y < 220.0f)title.Y += 4.0f;
            if (title.Y > 190.0f && title2.Y < 220) title2.Y += 4.0f;
            if (nezumi.X < - 64 || nezumi.X > 800) nezumi_velocity.X *= -1;
            nezumi += nezumi_velocity;
            //スペースキーが押されたらシーン終了
            if ( input.GetKeyTrigger(Keys.Space))
            {
                endFlag = true;
            }
        }
    }
}
