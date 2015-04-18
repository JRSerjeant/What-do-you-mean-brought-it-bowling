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
        float ballSpeed = 8.0f;

        Texture2D texture;
        Rectangle screenBounds;


        public ball(Texture2D texture, Rectangle screenBounds, Vector2 position)
        {
            this.texture = texture;
            this.screenBounds = screenBounds;
            this.position = position;
            //SetInStartPosition();
        }

        public void Update()
        {
 
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public void SetInStartPosition()
        {
            
        }
    }
}
