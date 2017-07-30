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
    class GamePlay : IScene
    {
        private Renderer renderer;
        private InputState input;//入力処理用オブジェクト
        private bool endFlag; //終了フラグ
        private Map map;
        private Haikei BackGraund;
        private BHaikei BigBackGraund;
        private Player player;
        private GameObjectManager gameObjectManager;
        private Random rnd = new Random();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="gameDevice"></param>
        public GamePlay(GameDevice gameDevice)
        {
            input = gameDevice.GetInputState();
            endFlag = false;

            map = new Map(gameDevice);
            BackGraund = new Haikei(gameDevice);
            BigBackGraund = new BHaikei(gameDevice);

            //map.Load("mori_MAP"); //読み込むファイルは適宜変更
            //map.Load("mori_MAP2"); //読み込むファイルは適宜変更
            //BackGraund.Load("SmallBackWood"); //読み込むファイルは適宜変更
            //BigBackGraund.Load("BigBackWood"); //読み込むファイルは適宜変更

            gameObjectManager = new GameObjectManager(gameDevice);
            gameObjectManager.Add(map);
            gameObjectManager.Add(BackGraund);
            gameObjectManager.Add(BigBackGraund);

            player = new Player(new Vector2(64 * 10, 64 * 17), gameDevice, gameObjectManager);
            gameObjectManager.Add(player);
            player.SetID(GameObjectID.Player);
            gameObjectManager.Add(player);

            //ゲームオブジェクトの追加
            GameObject tempGameObj;

            //初期位置は適宜変更

            //横スライド
            for (int i = 0; i < 20; i++)
            {
                tempGameObj = new SlidingBlock(new Vector2(32 * 57, 32 * (43- i)), new Vector2(32 * 58, 32 * (43 - i)), gameDevice);
                tempGameObj.SetID(GameObjectID.SlidingBlock_A);
                gameObjectManager.Add(tempGameObj);
            }
            for (int i = 0; i < 5; i++)
            {
                tempGameObj = new SlidingBlock(new Vector2(32 * (163 + i), 32 * 40), new Vector2(32 * (170 + i), 32 * 40), gameDevice);
                tempGameObj.SetID(GameObjectID.SlidingBlock_A);
                gameObjectManager.Add(tempGameObj);
            }
            for (int i = 0; i < 5; i++)
            {
                tempGameObj = new SlidingBlock(new Vector2(32 * (163 + i), 32 * 30), new Vector2(32 * (170 + i), 32 * 30), gameDevice);
                tempGameObj.SetID(GameObjectID.SlidingBlock_A);
                gameObjectManager.Add(tempGameObj);
            }
            for (int i = 0; i < 5; i++)
            {
                tempGameObj = new SlidingBlock(new Vector2(32 * (183 + i), 32 * 43), new Vector2(32 * (192 + i), 32 * 43), gameDevice);
                tempGameObj.SetID(GameObjectID.SlidingBlock_A);
                gameObjectManager.Add(tempGameObj);
            }
            for (int i = 0; i < 3; i++)
            {
                tempGameObj = new SlidingBlock(new Vector2(32 * (200 + i), 32 * 43), new Vector2(32 * (203 + i), 32 * 43), gameDevice);
                tempGameObj.SetID(GameObjectID.SlidingBlock_A);
                gameObjectManager.Add(tempGameObj);
            }

            //縦スライド
            for (int i = 0; i < 5; i++)
            {
                tempGameObj = new SlidingBlock_UD(new Vector2(32 * (128+i), 32 * 15), new Vector2(32 * (130+ i), 32 * 25), gameDevice);
                tempGameObj.SetID(GameObjectID.SlidingBlock_B);
                gameObjectManager.Add(tempGameObj);
            }
            for (int i = 0; i < 5; i++)
            {
                tempGameObj = new SlidingBlock_UD(new Vector2(32 * (141 + i), 32 * 30), new Vector2(32 * (141 + i), 32 * 40), gameDevice);
                tempGameObj.SetID(GameObjectID.SlidingBlock_C);
                gameObjectManager.Add(tempGameObj);
            }
            for (int i = 0; i < 5; i++)
            {
                tempGameObj = new SlidingBlock_UD(new Vector2(32 * (155 + i), 32 * 31), new Vector2(32 * (155 + i), 32 * 41), gameDevice);
                tempGameObj.SetID(GameObjectID.SlidingBlock_C);
                gameObjectManager.Add(tempGameObj);
            }
            for (int i = 0; i < 4; i++)
            {
                tempGameObj = new SlidingBlock_UD(new Vector2(32 * (216 + i), 32 * 8), new Vector2(32 * (216 + i), 32 * 43), gameDevice);
                tempGameObj.SetID(GameObjectID.SlidingBlock_C);
                gameObjectManager.Add(tempGameObj);
            }


            //for(int i = 0; i < 6; i++)
            //{
            //tempGameObj = new SlidingBlock(new Vector2(32 * (10-i), 32 * 15), new Vector2(32 * (15- i), 32 * 15), gameDevice);
            //tempGameObj.SetID(GameObjectID.SlidingBlock_D);
            //gameObjectManager.Add(tempGameObj);
            //}

            //敵オブジェクトの追加
            //for (int i = 0; i < rnd.Next(15, 30); i++)
            //{
            //    tempGameObj = new ChaseEnemy(new Vector2(32 * rnd.Next(5,40), 32 * rnd.Next(5,15)), gameDevice, gameObjectManager);
            //    tempGameObj.SetID(GameObjectID.Enemy01);
            //    gameObjectManager.Add(tempGameObj);
            //}

            //for (int i = 0; i < rnd.Next(15, 30); i++)
            //{
            //    tempGameObj = new JumpEnemy(new Vector2(32 * rnd.Next(5, 40), 32 * rnd.Next(5, 15)), gameDevice, gameObjectManager);
            //    tempGameObj.SetID(GameObjectID.Enemy02);
            //    gameObjectManager.Add(tempGameObj);
            //}

            //ヤマネコ
            tempGameObj = new Yamaneko(new Vector2(32 * 20, 32 * 15), gameDevice, gameObjectManager,player);
            tempGameObj.SetID(GameObjectID.Yamaneko);
            gameObjectManager.Add(tempGameObj);

            tempGameObj = new Yamaneko(new Vector2(32 * 68, 32 * 35), gameDevice, gameObjectManager, player);
            tempGameObj.SetID(GameObjectID.Yamaneko);
            gameObjectManager.Add(tempGameObj);

            tempGameObj = new Yamaneko(new Vector2(32 * 233, 32 * 8), gameDevice, gameObjectManager, player);
            tempGameObj.SetID(GameObjectID.Yamaneko);
            gameObjectManager.Add(tempGameObj);

            tempGameObj = new Yamaneko(new Vector2(32 * 250, 32 * 8), gameDevice, gameObjectManager, player);
            tempGameObj.SetID(GameObjectID.Yamaneko);
            gameObjectManager.Add(tempGameObj);

            //テン
            tempGameObj = new Ten(new Vector2(32 * 78, 32* 25), gameDevice, gameObjectManager);
            tempGameObj.SetID(GameObjectID.Ten);
            gameObjectManager.Add(tempGameObj);

            tempGameObj = new Ten(new Vector2(32 * 186, 32 * 33), gameDevice, gameObjectManager);
            tempGameObj.SetID(GameObjectID.Ten);
            gameObjectManager.Add(tempGameObj);

            tempGameObj = new Ten(new Vector2(32 * 192, 32 * 35), gameDevice, gameObjectManager);
            tempGameObj.SetID(GameObjectID.Ten);
            gameObjectManager.Add(tempGameObj);

            for(int i = 0; i < 3; i++)
            {
                tempGameObj = new Ten(new Vector2(32 * 208, 32 * (35-i*10)), gameDevice, gameObjectManager);
                tempGameObj.SetID(GameObjectID.Ten);
                gameObjectManager.Add(tempGameObj);
            }
            //ドアとボタン
            //for (int i = 0; i < 10; i++)
            //{
            //    tempGameObj = new Door("renga", new Vector2(32 * 50, 32 * 25 + 32 * i), gameDevice);
            //    tempGameObj.SetID(GameObjectID.Door_A);
            //    gameObjectManager.Add(tempGameObj);
            //}

            //tempGameObj = new Door("button1", new Vector2(32 * 13, 32 * 35), gameDevice);
            //tempGameObj.SetID(GameObjectID.Button_A);
            //gameObjectManager.Add(tempGameObj);
            //リンクするドアのIDを登録
            //((Button)tempGameObj).SetLinkedGameObjectID(GameObjectID.Door_A);
            //gameObjectManager.Add(tempGameObj);

            //for (int i = 0; i < 10; i++)
            //{
            //    tempGameObj = new Door("door2", new Vector2(32 * 35, 32 * (17 - i)), gameDevice);
            //    tempGameObj.SetID(GameObjectID.Door_B);
            //    gameObjectManager.Add(tempGameObj);
            //}

            //tempGameObj = new Door("button2", new Vector2(32 * 3, 32 * 7), gameDevice);
            //tempGameObj.SetID(GameObjectID.Button_B);
            ////リンクするドアのIDを登録
            //((Button)tempGameObj).SetLinkedGameObjectID(GameObjectID.Door_B);
            //gameObjectManager.Add(tempGameObj);

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
            renderer.DrawTexture("mori01", Vector2.Zero);
            BigBackGraund.Draw(renderer);
            BackGraund.Draw(renderer);
            map.Draw(renderer);
            gameObjectManager.Draw(renderer);

            if (player.DeadJudge())
            {
                renderer.DrawTexture("HIT_1", new Vector2(200,250));
            }
            //player.Draw(renderer);
            //map.Draw(renderer);
            //描画終了
            renderer.End();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            endFlag = false;
            //map.Load("mori_MAP");
            map.Load("mori_MAP2"); //読み込むファイルは適宜変更
            BackGraund.Load("SmallBackWood"); //読み込むファイルは適宜変更
            BigBackGraund.Load("BigBackWood"); //読み込むファイルは適宜変更
            gameObjectManager.Add(player);
            player.Initialize();
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
            return SceneType.Ending;
        }

        /// <summary>
        /// シーン終了処理
        /// </summary>
        public void Shutdown()
        {
            map.Unload();
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            //スペースキーが押されたらシーン終了
            if (input.GetKeyTrigger(Keys.D1) || player.DeadJudge())
            {
                endFlag = true;
            }
            
            gameObjectManager.Update(gameTime);
            map.Update(gameTime);
            //player.Update(gameTime);
            //map.Hit(player);
        }
    }
}
