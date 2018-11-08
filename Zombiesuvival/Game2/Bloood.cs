using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Game2
{
    class Bloood :AnimatedGameObject
    {

        private SoundEffectInstance explosionSound;
        public Bloood(int size, Vector2 startPosition, ContentManager content) : base(1, 15, startPosition, content, "blood")
        {
            //  explosionSound = content.Load<SoundEffect>("8bit_bomb_explosion").CreateInstance();
            // explosionSound.Play();
        }

        public override void Update(GameTime gameTime)
        {
        //    if (explosionSound.State != SoundState.Playing)
        //    {
        //        // GameWorld.RemoveGameObject(this);
        //    }

            base.Update(gameTime);
        }



    }
}
