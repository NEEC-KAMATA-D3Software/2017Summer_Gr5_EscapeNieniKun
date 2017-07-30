using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Scene
{
    interface IGameObjectMediator
    {
        void AddGameObject(GameObject gameObject);
        bool IsPlayerDead();
        GameObject GetPlayer();
        //特定のオブジェクトを取得する
        GameObject GetGameObject(GameObjectID id);

        //複数のゲームオブジェクトの取得
        List<GameObject> GetGameObjectList(GameObjectID id);
    }
}
