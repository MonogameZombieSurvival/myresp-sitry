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
    class BloodeEffect:AnimatedGameObject
    {
        private SoundEffectInstance BulletHitSoundEffect;// mangler;
        public BloodeEffect(int size, Vector2 startPosition, ContentManager content) : base(16, 44, startPosition, content, "kisspngbloode")
        {
            //  explosionSound = content.Load<SoundEffect>("8bit_bomb_explosion").CreateInstance();
            // explosionSound.Play();
        }

        public override void Update(GameTime gameTime)
        {
            if(currentAnimationIndex == 15)
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
