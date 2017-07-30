using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MyLib.Device;

namespace Action.Scene
{
    class Load : IScene
    {
        private Renderer renderer;
        private Sound sound;
        private bool endFlag;
        private TextureLoader textureLoader;
        private BGMLoader bgmLoader;
        private SELoader seLoader;
        private int totalResouceNum;
        private Map map;

        private string[,] TextureList()
        {
            string path = "./Texture/";
            string path2 = "./Photo/";
            string[,] list = new string[,]
                { 
                    {"block", path},
                    {"block2", path},
                    {"black", path},
                    {"player", path},
                    {"puddle", path},
                    {"door1", path},
                    {"door2", path},
                    {"button1", path},
                    {"button2", path},

                    {"block00", path2},
                    {"block01", path2},
                    {"block02", path2},
                    {"block03", path2},
                    {"field00", path2},
                    {"field01", path2},
                    {"mori01", path2},
                    {"saku00", path2},
                    {"saku01", path2},
                    {"saku02", path2},
                    {"saku03", path2},
                    {"saku04", path2},
                    {"saku05", path2},
                    {"saku06", path2},
                    {"harinezumi_title", path2},
                    {"dasshutu", path2},
                    {"ninikun", path2},
                    {"waku", path2},
                    {"hari", path2},
                    {"hari00", path2},
                    {"hari01", path2},
                    {"hari02", path2},
                    {"small_wood", path2},
                    {"big_wood", path2},
                    {"water", path2},
                    {"renga", path2},
                    {"HIT_SPACE", path2},
                    {"HIT_1", path2},
                    {"操作方法", path2},
                    {"テン", path2},
                    {"ハリネズミ　アニメーション　２", path2},
                    {"ヤマネコ　アニメーション", path2},
                    {"鷹　アニメーション　２", path2},
                };
            return list;
        }

        //BGMList
        //private string[,] BGMList()
        //{
        //    string path = "./bgm/";
        //    string[,] list = new string[,]
        //    {
        //            {"BGM", path},
        //            {"BGM001", path},
        //            {"GAMEOVER001", path},
        //            {"sound00", path},
        //    };
        //    return list;
        //}

        //SEList
        //private string[,] SEList()
        //{
        //    string path = "./bgm/";
        //    string[,] list = new string[,]
        //    {
        //            {"JUMP00", path},
        //            {"SE001", path},
        //            {"SE002", path},
        //            {"SE003", path},
        //            {"SE004", path},
        //    };
        //    return list;
        //}


        public Load( GameDevice gameDevice)
        {
            //描画オブジェクトの取得
            renderer = gameDevice.GetRenderer();

            sound = gameDevice.GetSound();

            //画像読み込みオブジェクトの実体生成
            textureLoader = new TextureLoader(renderer, TextureList());
            renderer.LoadTexture("load", "./Texture/");
            renderer.LoadTexture("harinezumi_title", "./Photo/");

            //bgmLoader = new BGMLoader(sound, BGMList());

            //seLoader = new SELoader(sound, SEList());
        }



        public void Draw(Renderer renderer)
        {
            renderer.Begin();

            renderer.DrawTexture("harinezumi_title", Vector2.Zero);
            renderer.DrawTexture("load", new Vector2(160, 280));

            //現在読み込んでいる数を取得
            int currentCount =
                textureLoader.CurrentCount();
                //+
                //bgmLoader.CurrentCount() +
                //seLoader.CurrentCount();

            if (totalResouceNum != 0)
            {
                renderer.DrawNumber(
                    "number",
                    new Vector2(530, 285),
                    (int)(currentCount / (float)totalResouceNum * 100));
            }

            //終了判定
            if (textureLoader.IsEnd())
                //&&
                //bgmLoader.IsEnd() &&
                //seLoader.IsEnd())
            {
                endFlag = true;
            }

            renderer.End();
        }

        public void Initialize()
        {
            endFlag = false;
            //画像読み込みオブジェクトを初期化
            textureLoader.Initialize();
            //bgmLoader.Initialize();
            //seLoader.Initialize();

            //全リソース数を計算
            totalResouceNum =
                textureLoader.Count();
                //+
                //bgmLoader.Count() +
                //seLoader.Count();
        }

        public bool IsEnd()
        {
            return endFlag;
        }

        public SceneType Next()
        {
            return SceneType.Title;
        }

        public void Shutdown()
        {
        }

        public void Update(GameTime gameTime)
        {
            //画像読み込みが終了してないか？
            if (!textureLoader.IsEnd())
            {
                textureLoader.Update();
            }
            //else if (!bgmLoader.IsEnd())
            //{
            //    bgmLoader.Update();
            //}
            //else if (!seLoader.IsEnd())
            //{
            //    seLoader.Update();
            //}
        }
    }
}
