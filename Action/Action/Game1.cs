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
        private GameDevice gameDevice; //�Q�[���f�o�C�X�I�u�W�F�N�g
        private Renderer renderer; //�`��I�u�W�F�N�g
        private SceneManager sceneManager;
        
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public Game1()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //��ʃT�C�Y�̐ݒ�
            graphicsDeviceManager.PreferredBackBufferWidth = Screen.Width;
            graphicsDeviceManager.PreferredBackBufferHeight = Screen.Height;
        }

        /// <summary>
        /// ������
        /// </summary>
        protected override void Initialize()
        {
            //�Q�[���f�o�C�X�̎��̐���
            gameDevice = new GameDevice(Content, GraphicsDevice);
            //�`��I�u�W�F�N�g�̎擾
            renderer = gameDevice.GetRenderer();

            sceneManager = new SceneManager();
            sceneManager.Add(SceneType.Load, new Load(gameDevice));
            sceneManager.Add(SceneType.Title, new Title(gameDevice));
            sceneManager.Add(SceneType.GamePlay, new GamePlay(gameDevice));
            sceneManager.Add(SceneType.Ending, new Ending(gameDevice));
            sceneManager.Change(SceneType.Load);

            ////// �������ɏ������������L�q //////
            base.Initialize(); //��΂ɏ�����
        }

        /// <summary>
        /// ���[�\�[�X�̓ǂݍ���
        /// </summary>
        protected override void LoadContent()
        {
            gameDevice.LoadContent();
            renderer.LoadTexture("load", "./Texture/");

          //  gameDevice.LoadContent();
        }

        /// <summary>
        /// ���\�[�X�̉������
        /// </summary>
        protected override void UnloadContent()
        {
            gameDevice.UnloadContent();
        }

        /// <summary>
        /// �X�V����
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            // �I������
            //�Q�[���p�b�h�̃o�b�N�{�^���܂��̓L�[�{�[�h��ESC�L�[�ŏI��
            if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) ||
                (Keyboard.GetState().IsKeyDown(Keys.Escape)))
            {
                this.Exit();
            }

            //�����艺�ɍX�V�������L�q

            // �Q�[���f�o�C�X�̍X�V
            gameDevice.Update(gameTime); //���̃v���W�F�N�g�ł��̍X�V������1��̂�

            sceneManager.Update(gameTime);



            ////// �������ɍX�V�������L�q //////
            base.Update(gameTime); //��΂ɏ�����
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // �����艺�ɕ`�揈�����L�q

            sceneManager.Draw(renderer);

            ////// �������ɕ`�揈�����L�q //////
            base.Draw(gameTime);//��΂ɏ�����
        }
    }
}
