using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyLib.Device
{
    public class GameDevice
    {
        private Renderer renderer;
        private InputState input;
        private Sound sound;
        private static Random rand;
        private Vector2 displayModify;//ディスプレイ位置修正

        public GameDevice(ContentManager contentManager, GraphicsDevice graphics)
        {
            renderer = new Renderer(contentManager, graphics);
            input = new InputState();
            sound = new Sound(contentManager);
            rand = new Random();
            displayModify = Vector2.Zero;
        }

        public void Initialize()
        {
        }

        public void Update(GameTime gameTime)
        {
            //デバイスで絶対に更新が必要なもの
            input.Update();
        }

        public void LoadContent()
        {
            //ロードシーンで必要なリソース読み込み
            renderer.LoadTexture("load", "./Texture/");
            renderer.LoadTexture("number", "./Texture/");
        }

        public void UnloadContent()
        {
            renderer.Unload();
        }

        public Renderer GetRenderer()
        {
            return renderer;
        }

        public InputState GetInputState()
        {
            return input;
        }

        public Sound GetSound()
        {
            return sound;
        }

        public Random GetRandom()
        {
            return rand;
        }

        public void SetDisplayModify(Vector2 position)
        {
            this.displayModify = position;
        }
        public Vector2 GetDisplayModify()
        {
            return displayModify;
        }
    }
}
