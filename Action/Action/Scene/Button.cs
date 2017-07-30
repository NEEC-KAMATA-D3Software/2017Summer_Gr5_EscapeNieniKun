using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using MyLib.Device;

namespace Action.Scene
{
    class Button : GameObject
    {
        private bool isTouch;
        private bool isHit;
        private GameObjectID linkedGameObjectID;
        private IGameObjectMediator mediator;

        public Button(string name, Vector2 position, GameDevice gameDevice, IGameObjectMediator mediator)
            : base(name, position, 32, 32, gameDevice)
        {
            this.mediator = mediator;
            isTouch = false;
            isHit = false;
        }

        public Button(Button other)
            :this(other.name, other.position, other.gameDevice, other.mediator)
        { }

        public void SetLinkedGameObjectID(GameObjectID id)
        {
            linkedGameObjectID = id;
        }

        public GameObjectID GetLinkedGameObjectID()
        {
            return linkedGameObjectID;
        }

        public override object Clone()
        {
            return linkedGameObjectID;
        }

        public override void Hit(GameObject gameObject)
        {
            if (isHit == false && gameObject is Player)
            {
                if (isTouch == false)
                {//連打されない対策
                    //Door door = (Door)mediator.GetGameObject(GetLinkedGameObjectID());
                    //door.Flip();

                    //複数のドアに対応
                    List<GameObject> doorList = mediator.GetGameObjectList(GetLinkedGameObjectID());
                    foreach(var d in doorList)
                    {
                        ((Door)d).Flip();
                    }
                }
                isHit = true;
            }
        }

        public override void Update(GameTime gameTime)
        {
            //連打されない対策
            isTouch = (isHit) ? (true) : (false);

            isHit = false;
        }
    }
}
