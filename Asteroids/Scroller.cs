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
  
       
        class Background
        {
            public Texture2D texture;
            public Rectangle rectangle;

            public void Draw(List<Scrolling> scroll, SpriteBatch spritebatch)
            {
                foreach (Scrolling s in scroll)
                {
                    spritebatch.Draw(s.texture, s.rectangle, Color.White);
                }
            }
        
        }
        class Scrolling : Background
        {
            public Scrolling()
            { }
            public Scrolling(Texture2D newTexture, Rectangle newRectangle) {


                texture = newTexture;
                rectangle = newRectangle;
            
            
            
            }
            public void Update(int up, int down, int left, int right) {

                rectangle.X += right;
                rectangle.X += left;
                rectangle.Y += up;
                rectangle.Y += down;
            }
            public void Scroll(List<Scrolling> scroll, GraphicsDeviceManager graphics)
            {
                foreach (Scrolling s in scroll)
                {

                    if (s.rectangle.Y + s.texture.Height >= 2 * graphics.PreferredBackBufferHeight)
                    {

                        s.rectangle.Y = -(scroll.Count-1) * graphics.PreferredBackBufferHeight;
                    }



                    s.Update(0, 3, 0, 0);
                }
            
            }
        
        
        }
    
}
