using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace What_do_you_mean_brought_it_bowling
{
    public class score
    {
        int currentScore;
        Texture2D[] textures;
        Rectangle screenBounds;
        Vector2 position;

        public score(int score, Rectangle screenBounds, Texture2D[] textures)
        {
            this.currentScore = score;
            this.textures = textures;
            this.screenBounds = screenBounds;
        }

        public void Update(int currentScore)
        {
            this.currentScore = currentScore;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            char[] scoreArray = currentScore.ToString().ToCharArray().ToArray();

            for (int i = 0; i < scoreArray.Length; i++)
            {
                position.X = i * textures[0].Width;
                position.Y = screenBounds.Height - textures[0].Height;

                switch (scoreArray[i])
                {
                    case '0':
                        spriteBatch.Draw(textures[0], position, Color.White);
                        break;
                    case '1':
                        spriteBatch.Draw(textures[1], position, Color.White);
                        break;
                    case '2':
                        spriteBatch.Draw(textures[2], position, Color.White);
                        break;
                    case '3':
                        spriteBatch.Draw(textures[3], position, Color.White);
                        break;
                    case '4':
                        spriteBatch.Draw(textures[4], position, Color.White);
                        break;
                    case '5':
                        spriteBatch.Draw(textures[5], position, Color.White);
                        break;
                    case '6':
                        spriteBatch.Draw(textures[6], position, Color.White);
                        break;
                    case '7':
                        spriteBatch.Draw(textures[7], position, Color.White);
                        break;
                    case '8':
                        spriteBatch.Draw(textures[8], position, Color.White);
                        break;
                    case '9':
                        spriteBatch.Draw(textures[9], position, Color.White);
                        break;

                }
            }


        }

        

    }
}
