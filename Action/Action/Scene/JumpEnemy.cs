using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MyLib.Device;
using MyLib.Utility;

namespace Action.Scene
{
    class JumpEnemy : GameObject
    {
        private IGameObjectMediator mediator;
        private Vector2 velocity;
        private bool isJump;
        private Motion motion;

        public JumpEnemy(Vector2 position, GameDevice gameDevice, IGameObjectMediator mediator)
            :base("puddle", position, 32, 32, gameDevice)
        {
            this.mediator = mediator;
            velocity.X = -1.0f;
            isJump = true;

            motion = new Motion(new Range(0, 5), new Timer(0.3f));
            for(int i = 0; i < 6; i++)
            {
                motion.Add(i, new Rectangle(32 * i, 0, 32, 32));
            }
        }

        public JumpEnemy(JumpEnemy other)
            :this(other.position, other.gameDevice, other.mediator)
        { }

        public override object Clone()
        {
            return new JumpEnemy(this);
        }

        public override void Hit(GameObject gameObject)
        {
            Direction dir = this.CheckDirection(gameObject);

            if(gameObject is DeathBlock)
            {
                isDead = true;
            }
            if (!(gameObject is Space))
            {
                if (dir == Direction.Top)
                {
                    position.Y = gameObject.GetRectangle().Top - this.height;
                    velocity.Y = 0.0f;
                    isJump = false;
                }
                else if (dir == Direction.Right)
                {
                    position.X = gameObject.GetRectangle().Right;
                    velocity.X = -velocity.X;
                }
                else if (dir == Direction.Left)
                {
                    position.X = gameObject.GetRectangle().Left - this.width;
                    velocity.X = -velocity.X;
                }
                else if (dir == Direction.Bottom)
                {
                    position.Y = gameObject.GetRectangle().Bottom;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {

            //落下処理
            velocity.Y = (velocity.Y > 4.0f) ? (4.0f) : (velocity.Y + 0.1f);

            //着地したらジャンプする
            if (isJump == false)
            {
                velocity.Y = -4.0f;
                isJump = true;
            }

            //移動計算
            position = position + velocity;

            motion.Update(gameTime);
        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position + gameDevice.GetDisplayModify(), motion.DrawinRange());
        }
    }
}
