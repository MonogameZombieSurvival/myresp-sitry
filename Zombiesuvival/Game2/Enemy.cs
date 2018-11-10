using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;

namespace Game2
{
    /// <summary>
    /// Class that represents a asteroid
    /// </summary>
    class Enemy : GameObject
    {

      
        
        float moveSpeed = 100;
         Random rand = new Random();
       
        /// <summary>
        /// Gets the Size of the Asteroid
        /// </summary>
        private int holdNumber;
   


        /// <summary>
        /// summens zzombies random out size mape set you own sobies on side and randow range;
        /// </summary>
        /// <param name="content">Content Manager for loading resources</param>
        /// 


        public Enemy(int HoldNumber, int SpawnDistansfromEachOther,  ContentManager content) : base(content, "zombiesmall")
        {

       
            if (HoldNumber <= 100)
            {

                position = new Vector2(rand.Next(-200, 0), rand.Next(GameWorld.ScreenSize.Height));// from left side
            }
            else if (HoldNumber >= 100 && HoldNumber <= 200)
            {
                position = new Vector2(rand.Next(GameWorld.ScreenSize.Width), rand.Next(-200, 0));//from top
            }
            else if (HoldNumber >= 200 && HoldNumber <= 300)
            {
                position = new Vector2(rand.Next(GameWorld.ScreenSize.Width), GameWorld.ScreenSize.Height + rand.Next(0, 200));//form button
            }
            else if (HoldNumber >= 300 && HoldNumber <= 400)
            {
                position = new Vector2(GameWorld.ScreenSize.Width + rand.Next(0, 200), rand.Next(GameWorld.ScreenSize.Height));//from right side
            }
        }


        /// <summary>
        /// summens zzombies random out size mape;
        /// </summary>
        /// <param name="content">Content Manager for loading resources</param>
        public Enemy(ContentManager content) : base(content, "zombiesmall")
        {                   
            holdNumber = rand.Next(0, 400);
            Thread.Sleep(100);
          

           
            if (holdNumber <= 100)
            {
                position = new Vector2(rand.Next(-200, 0), rand.Next(GameWorld.ScreenSize.Height));// from left side
            }
            else if (holdNumber >= 100 && holdNumber <= 200)
            {
                position = new Vector2(rand.Next(GameWorld.ScreenSize.Width), rand.Next(-200, 0));//randowmtop
            }
            else if (holdNumber >= 200 && holdNumber <= 300)
            {
                position = new Vector2(rand.Next(GameWorld.ScreenSize.Width), GameWorld.ScreenSize.Height + rand.Next(0, 200));//form
            }
            else if (holdNumber >= 300 && holdNumber <= 400)
            {
                position = new Vector2(GameWorld.ScreenSize.Width + rand.Next(0, 200), rand.Next(GameWorld.ScreenSize.Height));//from right side
            }
        }

        /// <summary>
        /// Sets the direction of the asteroid to a random direction
        /// </summary>
        private void SetRandomDirection()
        {

            Random rnd = new Random();
            Direction = new Vector2((rnd.Next(0, 2) * 2 - 1), (rnd.Next(0, 2) * 2 - 1)); //Set direction vector components to -1 or 1
            Direction.Normalize(); //Normalizes vector so that it is only a unit vector
        }


        private void SetDiraction()
        {


            Direction = realTimeplayerPosition - position;
            Direction.Normalize();

        }
      

      
        /// <summary>
        /// Update method that moves the asteroid in a specified direction. If asteroid is outside screen it sets a new random direction
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {

            rotation -= MathHelper.ToRadians(3);


            SetDiraction();

            position += Direction * (float)(moveSpeed * gameTime.ElapsedGameTime.TotalSeconds); //Added direction vector to current position


            rotation -= (float)(100 * gameTime.ElapsedGameTime.TotalSeconds);

            Direction = new Vector2((float)Math.Cos(rotation - MathHelper.Pi * 0.0f), (float)Math.Sin(rotation - MathHelper.Pi * 0.0f));

            position += Direction * (float)(gameTime.ElapsedGameTime.TotalSeconds);

            

            

            position += Direction * (float)(gameTime.ElapsedGameTime.TotalSeconds);
            //If Asteroid is outside screen set it to a new random direction
            if (!GameWorld.ScreenSize.Intersects(this.CollisionBox))
            {
                SetRandomDirection();
            }

        }

        /// <summary>
        /// Do the collide action for the Asteroid. If Player or a Bullet it explodes. If bullet it explodes into smaller asteroids
        /// </summary>
        /// <param name="otherObject">The object it collided with</param>
        public override void DoCollision(GameObject otherObject)
        {
            if (otherObject is Bullet)
            {
                Bloood blood = new Bloood( 1, Position, content);
                BloodeEffect bloodeEffect = new BloodeEffect(1, position, content);
                GameWorld.AddGameObject(blood);
                GameWorld.AddGameObject(bloodeEffect);

                GameWorld.RemoveGameObject(this);

                GameWorld.addKill();

            }
            /*
            if (otherObject is Bullet)
            { 
                GameWorld.RemoveGameObject(otherObject);
                for (int i = 0; i < Size; i++)
                {
                    GameWorld.AddGameObject(new Asteroid(position, size - 1, content));
                }
            }
            */
        }

    }
}
