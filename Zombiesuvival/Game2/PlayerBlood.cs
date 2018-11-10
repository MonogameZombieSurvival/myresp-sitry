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
    class PlayerBlood : AnimatedGameObject
    {
        private SoundEffectInstance BulletHitSoundEffect;// mangler;
        public PlayerBlood(int size, Vector2 startPosition, ContentManager content) : base(20, 60, startPosition, content, "PlayerBlood")
        {
            //  explosionSound = content.Load<SoundEffect>("8bit_bomb_explosion").CreateInstance();
            // explosionSound.Play();
        }

        public override void Update(GameTime gameTime)
        {
            if (currentAnimationIndex == 19)
            {
                GameWorld.RemoveGameObject(this);

            }

            //if (BulletHitSoundEffect.State != SoundState.Playing)
            //{
            //    // GameWorld.RemoveGameObject(this);
            //}

            base.Update(gameTime);
        }
    }
}
