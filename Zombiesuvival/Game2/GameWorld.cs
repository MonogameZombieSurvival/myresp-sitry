using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
namespace Game2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameWorld : Game
    {

        private SpriteBatch spriteBatch;

        public List<GameObject> gameObjects = new List<GameObject>();
        private static List<GameObject> toBeAdded = new List<GameObject>();
        private static List<GameObject> toBeRemoved = new List<GameObject>();


        private Player player;
        private SpriteFont font;
        private SpriteFont WaveTimer;
        private SpriteFont KillCount;
        private Texture2D collisionTexture;
        //private Song backgroundMusic;
        private Texture2D backgroundImg;
        Random rand = new Random();

        GameTimer gametimer;
        GameTimer Spawnspeed;
        
      
        private int WaveTimeOutPut;
        private int level =1;     
        static private int kills;
        public int Kills
        {
            get {
                return kills;

            }
        }
        private double spawtimeBetwenneEnemys; 

        private static GraphicsDeviceManager graphics;
       
        public static Rectangle ScreenSize
        {
            get
            {
                return graphics.GraphicsDevice.Viewport.Bounds;
            }
        }
        public  static void addKill()
        {
            kills += 1;
            

        }
    

        






        /// <summary>
        /// Timer 
        /// </summary>


        /*
        private static ContentManager _content;
        public static ContentManager ContentManager
        {
            get
            {
                return _content;
            }
        }
       */

        public GameWorld()
        {
            graphics = new GraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
          //  graphics.IsFullScreen = true;
            // _content = Content;
        }


        public static void AddGameObject(GameObject go)
        {
            toBeAdded.Add(go);
        }

        public static void RemoveGameObject(GameObject go)
        {
            toBeRemoved.Add(go);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
          
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            //backgroundMusic = Content.Load<Song>("BlindShift");
            //  MediaPlayer.Play(backgroundMusic);
            //MediaPlayer.IsRepeating = true;

          
            //Background Img
            backgroundImg = Content.Load<Texture2D>("bg-grass");

            // Create a timer with a two second interval.
         
            // Hook up the Elapsed event for the timer. 
                     
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("ExampleFont");
            WaveTimer = Content.Load<SpriteFont>("WaveTimer");
            KillCount = Content.Load<SpriteFont>("KillCount");

            collisionTexture = Content.Load<Texture2D>("CollisionTexture");


            Spawnspeed = new GameTimer();
            gametimer = new GameTimer();
           
            player = new Player(Content);
            gameObjects.Add(player);


            //Adds randomized asteroids to game
           
            //for (int i = 0; i < 10; i++)
            //{
            //    gameObjects.Add(new Enemy(Content));
            //    Thread.Sleep(100);
            //}

          


        }

        //go = new AnimatedGameObject(4,20,Content,"HeroStrip");

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }



        /// <summary>
        /// SpawnAynemeas
        /// </summary>
        public void SpawnAnymens(double spawnCircle)
        {          
            if(spawnCircle <=0.0001)
            {
              
                     
                    gameObjects.Add(new Enemy(rand.Next(0, 400), rand.Next(0, 400), Content));                                       
            }          
        }

        public void Setlevel(int WavetimeOutput)
        {
            if(WaveTimeOutPut == 0)
            {
                level += 1;
            }

        }
       
        //}
       
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            if (level == 1)
            {

                spawtimeBetwenneEnemys = Spawnspeed.gameTimerMIlilesecs(gameTime, 0.5, 0.01);
                SpawnAnymens(spawtimeBetwenneEnemys);
            }
            if (level == 2)
            {

                spawtimeBetwenneEnemys = Spawnspeed.gameTimerMIlilesecs(gameTime, 0.2, 0.01);

                SpawnAnymens(spawtimeBetwenneEnemys);
            }
            WaveTimeOutPut = gametimer.gameTimerSec(gameTime, 30);// level clock// spawn clock

            Setlevel(WaveTimeOutPut);
            
                
            foreach (GameObject go in gameObjects)
            {
                go.Update(gameTime);

                go.GetPlayerPosition(player.playerPosition);
                go.GetPlayerRot(player.playerRot);
                foreach (GameObject other in gameObjects)
                {
                    if (go != other && go.IsColliding(other))
                    {
                        go.DoCollision(other);
                    }
                }
            }

            foreach (GameObject go in toBeRemoved)
            {
                gameObjects.Remove(go);
            }
            toBeRemoved.Clear();

            gameObjects.AddRange(toBeAdded);
            toBeAdded.Clear();

            

            }



        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundImg, new Rectangle(0, 0, 1280, 720), Color.White);
            
            foreach (GameObject go in gameObjects)
            {
                go.Draw(spriteBatch);
#if DEBUG
                DrawCollisionBox(go);
#endif
            }

            spriteBatch.DrawString(WaveTimer, $"Next wave in:{WaveTimeOutPut} level:{level}", new Vector2(600, 5), Color.White);   
            spriteBatch.DrawString(font, $"Health:{player.Health}", new Vector2(5,5), Color.White);
            spriteBatch.DrawString(KillCount, $"KilleCount:{Kills}", new Vector2(1160, 5), Color.Red);
            
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void DrawCollisionBox(GameObject go)
        {
            Rectangle collisionBox = go.CollisionBox;
            Rectangle topLine = new Rectangle(collisionBox.X, collisionBox.Y, collisionBox.Width, 1);
            Rectangle bottomLine = new Rectangle(collisionBox.X, collisionBox.Y + collisionBox.Height, collisionBox.Width, 1);
            Rectangle rightLine = new Rectangle(collisionBox.X + collisionBox.Width, collisionBox.Y, 1, collisionBox.Height);
            Rectangle leftLine = new Rectangle(collisionBox.X, collisionBox.Y, 1, collisionBox.Height);

            spriteBatch.Draw(collisionTexture, topLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, bottomLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, rightLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, leftLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
        }
    }
}
