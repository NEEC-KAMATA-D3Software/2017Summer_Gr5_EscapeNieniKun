using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MyLib.Device;
using MyLib.Utility;

namespace MyLib.Device
{
    public class Motion
    {
        private Range range;//範囲
        private Timer timer;//モーション時間
        private int motionNumber;//モーション番号

        //表示位置を番号で管理
        //Dictionaryを使えば登録順番を気にしなくてもよい
        private Dictionary<int, Rectangle> rectangles = new Dictionary<int, Rectangle>();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Motion()
        {
            Initialize(new Range(0, 0), new Timer());
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="range"></param>
        /// <param name="timer"></param>
        public Motion( Range range, Timer timer)
        {
            Initialize(range, timer);
        }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="range"></param>
        /// <param name="timer"></param>
        public void Initialize(Range range, Timer timer)
        {
            this.range = range;
            this.timer = timer;
            motionNumber = range.First();
        }

        /// <summary>
        /// モーション矩形情報を追加
        /// </summary>
        /// <param name="index"></param>
        /// <param name="rect"></param>
        public void Add(int index, Rectangle rect)
        {
            rectangles.Add(index, rect);
        }

        /// <summary>
        /// モーションの更新
        /// </summary>
        private void MotionUpdate()
        {
            motionNumber += 1;
            if( range.IsOutOfRange(motionNumber))
            {
                motionNumber = range.First();
            }
        }

        public void Update( GameTime gameTime)
        {
            if(range.IsOutOfRange())
            {
                return;
            }

            timer.Update();
            if ( timer.IsTime())
            {
                timer.Initialize();
                MotionUpdate();
            }
        }

        public Rectangle DrawinRange()
        {
            return rectangles[motionNumber];
        }

    }
}
