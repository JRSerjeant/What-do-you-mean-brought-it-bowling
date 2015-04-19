using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace What_do_you_mean_brought_it_bowling
{
    public class dog
    {
        Vector2 position;
        Rectangle bounds;
        public bool isAlive;
        public bool isDogOffScreen;

        public Rectangle publicBounds
        {
            get
            {
                bounds.X = (int)position.X;
                bounds.Y = (int)position.Y;
                return bounds;
            }
        }

        float dogSpeed = 1f;

        Texture2D texture;
        Rectangle screenBounds;

        public dog(Texture2D texture, Rectangle screenBounds, Vector2 position)
        {
            bounds = new Rectangle(0, 0, texture.Width, texture.Height);
            this.texture = texture;
            this.screenBounds = screenBounds;
            this.position = position;
            this.isAlive = true;
        }

        public void Update()
        {
            position.Y += dogSpeed;
            isOffScreen();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public bool isOffScreen()
        {
            if (position.Y > screenBounds.Height)
            {
                isDogOffScreen = true;
                return true;
            }
            else
            {
                isDogOffScreen = false;
                return false;
            }

        }
    }
}
