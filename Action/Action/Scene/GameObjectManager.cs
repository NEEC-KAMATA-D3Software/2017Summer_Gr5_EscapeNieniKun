using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using MyLib.Device;

namespace Action.Scene
{
    class GameObjectManager : IGameObjectMediator
    {
        private List<GameObject> gameObjectList; //プレイヤーグループ
        private List<GameObject> addGameObjects; //追加するキャラクター

        private GameDevice gameDevice;
        private Map map;
        private Haikei BackGraund;
        private BHaikei BigBackGraund;

        ///<summary>
        ///コンストラクタ
        ///</summary>
        ///<param name="gameDevice"></param>
        public GameObjectManager(GameDevice gameDevice)
        {
            this.gameDevice = gameDevice;
            Initialize();
        }

        ///<summary>
        ///初期化
        ///</summary>
        public void Initialize()
        {
            //各リストの生成とクリア
            if (gameObjectList != null)
            {
                gameObjectList.Clear();
            }
            else
            {
                gameObjectList = new List<GameObject>();
            }

            if (addGameObjects != null)
            {
                addGameObjects.Clear();
            }
            else
            {
                addGameObjects = new List<GameObject>();
            }
        }

        ///<summary>
        ///ゲームオブジェクトの追加
        ///</summary>
        ///<param name="gameObject"></param>
        public void Add(GameObject gameObject)
        {
            if (gameObject == null)
            {
                return;
            }
            addGameObjects.Add(gameObject);
        }


        ///<summary>
        ///マップの追加
        ///</summary>
        ///<param name="map"></param>
        public void Add(Map map)
        {
            if (map == null)
            {
                return;
            }
            this.map = map;
        }

        ///<summary>
        ///マップとの衝突
        ///</summary>
        private void HitToMap()
        {
            if (map == null)
            {
                return;
            }
            foreach (var c in gameObjectList)
            {
                map.Hit(c);
            }
        }

        ///<summary>
        ///マップの追加
        ///</summary>
        ///<param name="map"></param>
        public void Add(Haikei BackGraund)
        {
            if (BackGraund == null)
            {
                return;
            }
            this.BackGraund = BackGraund;
        }

        ///<summary>
        ///マップの追加
        ///</summary>
        ///<param name="map"></param>
        public void Add(BHaikei BigBackGraund)
        {
            if (BigBackGraund == null)
            {
                return;
            }
            this.BigBackGraund = BigBackGraund;
        }
        
        ///<summary>
        ///オブジェクト同士の衝突
        ///</summary>
        private void HitToGameObject()
        {
            foreach (var c1 in gameObjectList)
            {
                foreach (var c2 in gameObjectList)
                {
                    if (c1.Equals(c2) || c1.IsDead() || c2.IsDead())
                    {
                        //同キャラか、キャラが死んでいたら次へ
                        continue;
                    }
                    if (c1.Collision(c2))
                    {
                        //ヒット通知
                        c1.Hit(c2);
                        c2.Hit(c1);
                    }
                }
            }
        }

        ///<summary>
        ///死亡キャラを削除
        ///</summary>
        private void RemoveDeadCharacters()
        {
            gameObjectList.RemoveAll(c => c.IsDead());
        }

        ///<summary>
        ///更新
        ///</summary>
        ///<param name="gameTime">ゲーム時間</param>
        public void Update(GameTime gameTime)
        {
            //全キャラ更新
            foreach(var c in gameObjectList)
            {
                c.Update(gameTime);
            }

            //キャラクタの追加
            foreach (var c in addGameObjects)
            {
                gameObjectList.Add(c);
            }

            //追加終了後、追加リストはクリア
            addGameObjects.Clear();

            //当たり判定
            HitToMap();
            HitToGameObject();

            //死亡フラグが立っているキャラをすべて削除
            RemoveDeadCharacters();
        }

        ///<summary>
        ///描画
        ///</summary>
        ///<param name="renderer">描画オブジェクト</param>
        public void Draw(Renderer renderer)
        {
            //全キャラ描画
            foreach(var c in gameObjectList)
            {
                c.Draw(renderer);
            }
        }

        ///<summary>
        ///ゲームオブジェクトの追加
        ///</summary>
        ///<param name="gameObject"></param>
        public void AddGameObject(GameObject gameObject)
        {
            if (gameObject == null)
            {
                return;
            }
            addGameObjects.Add(gameObject);
        }

        ///<summary>
        ///プレイヤーが死亡しているか？
        ///</summary>
        ///<returns></returns>
        public bool IsPlayerDead()
        {
            //プレイヤーが見つからなければ死亡
            GameObject find = gameObjectList.Find(c => c is Player);
            return (find == null || find.IsDead());
        }

        ///<summary>
        ///プレイヤーの取得
        ///</summary>
        ///<returns></returns>
        public GameObject GetPlayer()
        {
            GameObject find = gameObjectList.Find(c => c is Player);
            if(find != null && !find.IsDead())
            {
                return find;
            }
            return null; //プレイヤーがいない
        }

        public GameObject GetGameObject(GameObjectID id)
        {
            GameObject find = gameObjectList.Find(c => c.GetID() == id);
            //発見したオブジェクトがnull出ないとき、かつ、死んでないとき
            if(find != null && !find.IsDead())
            {
                return find; //発見したオブジェクト
            }
            return null; //オブジェクトがいない
        }


        //複数のゲームオブジェクトの取得
        public List<GameObject> GetGameObjectList(GameObjectID id)
        {
            List<GameObject> list = gameObjectList.FindAll(c => c.GetID() == id);
            List<GameObject> aliveList = new List<GameObject>();
            foreach(var c in list)
            {
                if (!c.IsDead())
                {
                    aliveList.Add(c);
                }
            }
            return aliveList;
        }
    }
}
