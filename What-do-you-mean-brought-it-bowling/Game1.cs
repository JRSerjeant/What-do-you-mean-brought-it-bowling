using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace What_do_you_mean_brought_it_bowling
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        //Set up Screen
        int screenWidth = 1024;
        int screenHeight = 768;
        Rectangle screenBounds;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Set up dude variables
        dude thedude;
        Texture2D dudeImage;

        //Set up ball variables
        public List<ball> balls = new List<ball>();
        Vector2 startL;
        public Texture2D ballImage;
        KeyboardState oldState;

        //Set up dog variables 
        public List<dog> dogs = new List<dog>();
        Texture2D dogImage;
        
        //Background
        Texture2D backgroungImage;

        //font
        SpriteFont font;

     



        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            screenBounds = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            
            this.Window.Title = "What do you mean \"brought it bowling\"?";
            startL.X = 100;
            startL.Y = 100;

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
            oldState = Keyboard.GetState();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ballImage = Content.Load<Texture2D>("ball.png");
            dudeImage = Content.Load<Texture2D>("dude.png");
            dogImage = Content.Load<Texture2D>("dog.png");
            backgroungImage = Content.Load<Texture2D>("background.png");
            //font = Content.Load<SpriteFont>("lebowski");
            thedude = new dude(dudeImage, ballImage, screenBounds);
            dogs.Add(new dog(dogImage, screenBounds, new Vector2(screenWidth / 1.5f, 0)));
            dogs.Add(new dog(dogImage, screenBounds, new Vector2(screenWidth / 2.5f, 0)));
            dogs.Add(new dog(dogImage, screenBounds, new Vector2(screenWidth / 2, -60)));
            dogs.Add(new dog(dogImage, screenBounds, new Vector2(screenWidth / 3.6f, -80)));
            dogs.Add(new dog(dogImage, screenBounds, new Vector2(screenWidth / 0.5f, -50)));

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            thedude.Update();

            foreach (ball ball in balls)
            {
                ball.Update();
                ball.checkCollision(dogs);
            }

            foreach (dog dog in dogs)
            {
                dog.Update();
                System.Diagnostics.Debug.WriteLine("Is dog alive: " + dog.isAlive.ToString());
            }

            
            //remove balls and dogs that are off screen
            balls.RemoveAll(item => item.isOffScreen() == true);
            dogs.RemoveAll(item => item.isOffScreen() == true || item.isAlive == false);
            
            KeyboardState newState = Keyboard.GetState();
            if (newState.IsKeyDown(Keys.Space))
            {
                if (!oldState.IsKeyDown(Keys.Space))
                {
                     addBall(thedude.position);
                    startL.X += ballImage.Width;
                }
            }
            oldState = newState;


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            //draw backfround Image
            spriteBatch.Draw(backgroungImage,new Vector2(0.0f, 0.0f) ,Color.White);
            
            //draw the dude.
            thedude.Draw(spriteBatch);

            //Draw the balls
            foreach (ball ball in balls)
            {
                ball.Draw(spriteBatch);
            }

            //Draw the dogs
            foreach (dog dog in dogs)
            {
                dog.Draw(spriteBatch);
            }

            spriteBatch.End();
            System.Diagnostics.Debug.WriteLine("Balls: " + balls.Count.ToString());
            System.Diagnostics.Debug.WriteLine("Dogs: " + dogs.Count.ToString());
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public void addBall(Vector2 dudePosition)
        {
            dudePosition.Y -= 20;
            dudePosition.X += dudeImage.Width;
            balls.Add(new ball(ballImage, screenBounds, dudePosition));
        }

        //public void drawText()
        //{
        //    spriteBatch.DrawString(font, "Balls: " + balls.Count.ToString(), new Vector2(10, 10), Color.White);
        //}

    }
}
