using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace Asteroids
{
    
    class Stat
    {
        private SpriteBatch spriteBatch;
        private SpriteFont theFont;
        private int count;
        public Stat()
        {
            count = 0;
        
        }
        public Stat(SpriteBatch spriteBatch, SpriteFont theFont)
        {

            this.spriteBatch = spriteBatch;
            this.theFont = theFont;
        
        }
        public int Count
        {

            get { return count; }
            set { count = value; }
    
    }
        public void DrawHud()

        {

            spriteBatch.DrawString(theFont, "X: ", new Vector2(0f, 0f), Color.Green);
            

        }

        public int LevelWinLose(Asteroid asteroid, Player player)
        {


            return 0;
        
        }

        public void DrawLevelScreen(SpriteBatch spriteBatch, Texture2D image, Rectangle rec, int level, SpriteFont font, float x, float y, Color theColor)
        {
            if (Count < 2)
            {
                spriteBatch.Draw(image, rec, Color.White);
                spriteBatch.DrawString(font, " " + (level + 2), new Vector2(x, y), theColor);
            }

        }

    }
}
