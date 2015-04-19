using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace What_do_you_mean_brought_it_bowling
{
    public class life
    {
        int numberLives;
        Texture2D texture;
        Rectangle screenBounds;
        Vector2 position;
        public bool gameover;

        public life(Texture2D texture, Rectangle screenBounds)
        {
            this.texture = texture;
            this.screenBounds = screenBounds;
            this.numberLives = 5;
            this.gameover = false;
        }

        public void Update()
        {
            if (numberLives < 0)
            {
                gameover = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < numberLives; i++)
            {
                position.X = (screenBounds.Width - texture.Width) - (i * texture.Width);
                position.Y = screenBounds.Height - texture.Height;
                spriteBatch.Draw(texture, position, Color.White);
            }
            System.Diagnostics.Debug.WriteLine(numberLives);
        }

        public void removeLife()
        {
            numberLives -= 1;
        }

        public void setNumberLives()
        {
            numberLives = 5;
        }
        public void setGameOverFalse()
        {
            gameover = false;
        }
        
    }
}
