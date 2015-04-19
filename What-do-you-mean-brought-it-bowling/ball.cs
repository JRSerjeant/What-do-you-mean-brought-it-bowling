using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace What_do_you_mean_brought_it_bowling
{
    public class ball
    {
        Vector2 position;
        Rectangle location;
        float ballSpeed = 3f;
        
        Texture2D texture;
        Rectangle screenBounds;

        public ball(Texture2D texture, Rectangle screenBounds, Vector2 position)
        {
            this.texture = texture;
            this.screenBounds = screenBounds;
            this.position = position;
        }

        public void Update()
        {
            position.Y -= ballSpeed;
            location = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);

        }

        public bool isOffScreen()
        {
            if (position.Y < 0)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        public void checkCollision(List<dog> dogs)
        {
            foreach (dog dog in dogs)
            {
                if(dog.publicBounds.Intersects(location))
                {
                    dog.isAlive = false;
                }
            }
        }
        
    }
}
