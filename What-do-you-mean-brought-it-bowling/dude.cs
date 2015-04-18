using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace What_do_you_mean_brought_it_bowling
{
    class dude
    {
        public Game1 game;

        Vector2 position;
        Vector2 motion;
        float dudeSpeed = 8.0f;

        KeyboardState keyboardState;

        Texture2D texture;
        Texture2D ballTexture;
        Rectangle screenBounds;

        public dude(Texture2D texture, Texture2D ballTexture, Rectangle screenBounds)
        {
            this.texture = texture;
            this.screenBounds = screenBounds;
            this.ballTexture = ballTexture;
            SetInStartPosition();
            game = new Game1();
        }


        public void Update()
        {
            motion = Vector2.Zero;

            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Q))
            {
                motion.X = -1;
            }
            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.E))
            {
                motion.X = 1;
            }
            if (keyboardState.IsKeyDown(Keys.Space) || keyboardState.IsKeyDown(Keys.Enter))
            {
                //game.addBall(position);
                //game.balls.Add(new ball(ballTexture, screenBounds, position));

            }

            motion.X *= dudeSpeed;
            position += motion;
            keepDudeOnScreen();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public void SetInStartPosition()
        {
            position.X = (screenBounds.Width - texture.Width) / 2;
            position.Y = (screenBounds.Height - texture.Height) - 5;
        }

        private void keepDudeOnScreen()
        {
            if (position.X < 0)
            {
                position.X = 0;
            }
            if (position.X + texture.Width > screenBounds.Width)
            {
                position.X = screenBounds.Width - texture.Width;
            }
        }
    }
}
