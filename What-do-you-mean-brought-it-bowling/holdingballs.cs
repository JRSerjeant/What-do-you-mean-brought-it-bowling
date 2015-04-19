using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace What_do_you_mean_brought_it_bowling
{
    public class holdingballs
    {
        Rectangle location;
        List<Vector2> holdingBallsPositions = new List<Vector2>();
        int totalBallsToDraw = 10;
        Texture2D texture;
        Rectangle screenBounds;


        public holdingballs(Texture2D texture, Rectangle screenBounds)
        {
            this.texture = texture;
            this.screenBounds = screenBounds;

            holdingBallsPositions.Add(new Vector2(168, 554));
            holdingBallsPositions.Add(new Vector2(202, 554));
            holdingBallsPositions.Add(new Vector2(374, 554));
            holdingBallsPositions.Add(new Vector2(403, 554));
            holdingBallsPositions.Add(new Vector2(580, 554));
            holdingBallsPositions.Add(new Vector2(610, 554));
            holdingBallsPositions.Add(new Vector2(778, 554));
            holdingBallsPositions.Add(new Vector2(810, 554));
            holdingBallsPositions.Add(new Vector2(403, 525));
            holdingBallsPositions.Add(new Vector2(607, 525));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < totalBallsToDraw; i++)
            {
                spriteBatch.Draw(texture, holdingBallsPositions[i], Color.White);
            }
        }

        public void reduceBallsToDraw()
        {
            totalBallsToDraw -= 1;
        }

        public void increaseBallsToDraw()
        {
            totalBallsToDraw += 1;
        }

    }
}
