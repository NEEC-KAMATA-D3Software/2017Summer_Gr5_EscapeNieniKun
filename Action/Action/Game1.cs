using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using MyLib.Device;
using Action.Def;
using Action.Scene;

namespace Action
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphicsDeviceManager;
        private GameDevice gameDevice; //ゲームデバイスオブジェクト
        private Renderer renderer; //描画オブジェクト
        private SceneManager sceneManager;
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Game1()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //画面サイズの設定
            graphicsDeviceManager.PreferredBackBufferWidth = Screen.Width;
            graphicsDeviceManager.PreferredBackBufferHeight = Screen.Height;
        }

        /// <summary>
        /// 初期化
        /// </summary>
        protected override void Initialize()
        {
            //ゲームデバイスの実体生成
            gameDevice = new GameDevice(Content, GraphicsDevice);
            //描画オブジェクトの取得
            renderer = gameDevice.GetRenderer();

            sceneManager = new SceneManager();
            sceneManager.Add(SceneType.Load, new Load(gameDevice));
            sceneManager.Add(SceneType.Title, new Title(gameDevice));
            sceneManager.Add(SceneType.GamePlay, new GamePlay(gameDevice));
            sceneManager.Add(SceneType.Ending, new Ending(gameDevice));
            sceneManager.Change(SceneType.Load);

            ////// これより上に初期化処理を記述 //////
            base.Initialize(); //絶対に消すな
        }

        /// <summary>
        /// リーソースの読み込み
        /// </summary>
        protected override void LoadContent()
        {
            gameDevice.LoadContent();
            renderer.LoadTexture("load", "./Texture/");

          //  gameDevice.LoadContent();
        }

        /// <summary>
        /// リソースの解放処理
        /// </summary>
        protected override void UnloadContent()
        {
            gameDevice.UnloadContent();
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            // 終了処理
            //ゲームパッドのバックボタンまたはキーボードのESCキーで終了
            if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) ||
                (Keyboard.GetState().IsKeyDown(Keys.Escape)))
            {
                this.Exit();
            }

            //これより下に更新処理を記述

            // ゲームデバイスの更新
            gameDevice.Update(gameTime); //このプロジェクトでこの更新処理は1回のみ

            sceneManager.Update(gameTime);



            ////// これより上に更新処理を記述 //////
            base.Update(gameTime); //絶対に消すな
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // これより下に描画処理を記述

            sceneManager.Draw(renderer);

            ////// これより上に描画処理を記述 //////
            base.Draw(gameTime);//絶対に消すな
        }
    }
}
