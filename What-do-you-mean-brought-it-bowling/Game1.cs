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
        float dogSpeed = 1.0f;

        //Set up ball variables
        public List<ball> balls = new List<ball>();
        Vector2 startL;
        public Texture2D ballImage;
        KeyboardState oldState;
        holdingballs holdingBalls;
        

        //Set up dog variables 
        public List<dog> dogs = new List<dog>();
        Texture2D dogImage;
        
        //Background
        Texture2D backgroungImage;

        //numbers
        Texture2D[] scoresTextures; 
        score scoreBoard;
        int score;

        //life
        life life;
        Texture2D lifeImage;

        //messages
        Texture2D gameOverImage;
        Texture2D welcomeImage;
        Texture2D spake1Image;

        //walter
        Texture2D walterImage;

        bool newgame = true;

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
            score = 0;
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


            //load numbers
            scoresTextures = new Texture2D[] {Content.Load<Texture2D>("0.png"), 
                Content.Load<Texture2D>("1.png"),
                Content.Load<Texture2D>("2.png"),
                Content.Load<Texture2D>("3.png"),
                Content.Load<Texture2D>("4.png"),
                Content.Load<Texture2D>("5.png"),
                Content.Load<Texture2D>("6.png"),
                Content.Load<Texture2D>("7.png"),
                Content.Load<Texture2D>("8.png"),
                Content.Load<Texture2D>("9.png")};
            
            // load ball, dude, dog, life, gameover and background textures
            gameOverImage = Content.Load<Texture2D>("gameover.png");
            ballImage = Content.Load<Texture2D>("ball.png");
            dudeImage = Content.Load<Texture2D>("dude.png");
            dogImage = Content.Load<Texture2D>("dog.png");
            backgroungImage = Content.Load<Texture2D>("background.png");
            lifeImage = Content.Load<Texture2D>("life.png");
            welcomeImage = Content.Load <Texture2D>("welcome.png");
            spake1Image = Content.Load<Texture2D>("speak1.png");
            walterImage = Content.Load<Texture2D>("walter.png");
            //Load first set of dogs. 
            StartGame();
            //create class instances
            life = new life(lifeImage, screenBounds);
            scoreBoard = new score(score, screenBounds, scoresTextures);
            thedude = new dude(dudeImage, ballImage, screenBounds);
            holdingBalls = new holdingballs(ballImage, screenBounds);


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
                if (ball.isOffScreen() == true)
                {
                    holdingBalls.increaseBallsToDraw();
                }
            }

            foreach (dog dog in dogs)
            {
                dog.Update();
                if (dog.isAlive == false)
                {
                    score += 1;
                }
                if (dog.isDogOffScreen == true && newgame == false)
                {
                    life.removeLife();
                }
            }
            
            //remove balls and dogs that are off screen
            balls.RemoveAll(item => item.isOffScreen() == true);
            dogs.RemoveAll(item => item.isOffScreen() == true || item.isAlive == false);
            
            KeyboardState newState = Keyboard.GetState();
            if (newState.IsKeyDown(Keys.X) && newgame == true)
            {
                newgame = false;
                RestartGame();
            }
            if (newState.IsKeyDown(Keys.Space) && newgame == false)
            {
                if (!oldState.IsKeyDown(Keys.Space))
                {
                    if (balls.Count <= 10 && life.gameover == false)
                    {
                        holdingBalls.reduceBallsToDraw();
                        addBall(thedude.position);
                        //startL.X += ballImage.Width;
                    }
                }
            }
            oldState = newState;

            if (newState.IsKeyDown(Keys.X) && life.gameover == true)
            {
                RestartGame();
            }
            if (dogs.Count < 8)
            {
                Random random = new Random();
                int x = random.Next((0 + dudeImage.Width), (screenWidth - dogImage.Width));
                int y = random.Next(-555, -55);
                dogSpeed += 0.04f;
                if (dogSpeed > 3.1f)
                {
                    dogSpeed = 3.0f;
                }
                dogs.Add(new dog(dogImage, screenBounds, new Vector2(x , y),dogSpeed));
            }
            scoreBoard.Update(score);
            life.Update();
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
            spriteBatch.Draw(walterImage, new Vector2(10, screenHeight - walterImage.Height - 150));
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

            //Draw the Score
            scoreBoard.Draw(spriteBatch);

            //Draw the life icons.
            life.Draw(spriteBatch);
            holdingBalls.Draw(spriteBatch);

            //spriteBatch.Draw(spake1Image, new Vector2(180, 643), Color.White);

            //message windows !!must go last!!
            if (life.gameover == true)
            {
                spriteBatch.Draw(gameOverImage, new Vector2((screenWidth / 2) -(gameOverImage.Width / 2), (screenHeight /2) - (gameOverImage.Height /2)), Color.White);
            }

            if (newgame == true)
            {
                spriteBatch.Draw(welcomeImage, new Vector2((screenWidth / 2) - (welcomeImage.Width / 2), (screenHeight / 2) - (welcomeImage.Height / 2)), Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void addBall(Vector2 dudePosition)
        {
            dudePosition.Y -= 20;
            dudePosition.X += dudeImage.Width - 20;
            balls.Add(new ball(ballImage, screenBounds, dudePosition));
        }

        public void addScore()
        {
            score += 1;
        }

        public void StartGame()
        {
            dogs.Add(new dog(dogImage, screenBounds, new Vector2(screenWidth / 1.5f, 0),dogSpeed));
            dogs.Add(new dog(dogImage, screenBounds, new Vector2(screenWidth / 2.5f, 0),dogSpeed));
            dogs.Add(new dog(dogImage, screenBounds, new Vector2(screenWidth / 2, -160),dogSpeed));
            dogs.Add(new dog(dogImage, screenBounds, new Vector2(screenWidth / 3.6f, -80),dogSpeed));
            dogs.Add(new dog(dogImage, screenBounds, new Vector2(screenWidth / 0.5f, -250),dogSpeed));
        }

        public void RestartGame()
        {
            life.setNumberLives();
            life.setGameOverFalse();
            dogs.Clear();
            balls.Clear();
            score = 0;
            dogSpeed = 1.0f;
            StartGame();
        }
    }
}
