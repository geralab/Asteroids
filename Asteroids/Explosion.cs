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
    class Explosion 
    {
        private Texture2D image;
        private Rectangle spriteRec;
        private Vector2 imageCenter;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont theFont;
        KeyboardState oldState;
        GamePadState oldPadStatePlayer;
        private float x, y, angle;
        private Vector2 position, velocity;
        private float velocityIncrement, velocityDecrement;
        private int frame;
        private float angleIncrement, angularVelocityIncrement, angularVelocityDecrement, angularVelocity;
        private const float fullRotation = ((float)(2 * Math.PI));
        private float speed, speedIncrement, speedDecrement;
        private Weapon weapon;
       
        private Random random;
        Player player;
        Fire fire;
       
        private List<Explosion> theExplosions = new List<Explosion>();

        public Explosion()
        {


        }


        public Explosion(Texture2D image, Weapon weapon, SpriteBatch spriteBatch, SpriteFont theFont)
        {

            speedIncrement = 0.003f;
            speedDecrement = 0.96f;
            angularVelocityIncrement = .0012f;
            AngularVelocityDecrement = 0.97f;
            velocityDecrement = 0.995f;
            angleIncrement = 0.1f;
            this.weapon = weapon;
            this.theFont = theFont;

            this.spriteBatch = spriteBatch;
            this.image = image;

            //timeSinceLastFrame = weapon.Timer.ElapsedGameTime;
            position = new Vector2(x, y);
            velocity = new Vector2(0f, 0f);
            angle = 0;
            angularVelocity = .000000123f;
        }
        public Explosion(Texture2D image, float x, float y, float xv, float yv)
        {
            this.x = x;
            this.y = y;
            speedIncrement = 0.003f;
            speedDecrement = 0.96f;
            angularVelocityIncrement = .0012f;
            AngularVelocityDecrement = 0.96f;
            velocityDecrement = 0.995f;
            angleIncrement = 0.1f;

            velocity = new Vector2(xv, yv);

            this.image = image;

            //timeSinceLastFrame = weapon.Timer.ElapsedGameTime;
            position = new Vector2(x, y);

            angle = 0;
            angularVelocity = .13f;
        }
        public Player AccessPlayer
        {
            set
            {
                player = value;

            }
            get
            {
                return player;

            }
        }
        public GraphicsDeviceManager Graphics
        {

            set { graphics = value; }
            get { return graphics; }

        }
        public float VelocityIncrement
        {
            set
            {
                velocityIncrement = value;

            }
            get
            {
                return velocityIncrement;

            }
        }
        public Vector2 ImageCenter
        {
            set
            {
                imageCenter = value;

            }
            get
            {
                return imageCenter;

            }

        }
        public float VelocityDecrement
        {
            set
            {
                velocityDecrement = value;

            }
            get
            {
                return velocityDecrement;

            }
        }
        public float SpeedDecrement
        {
            set
            {
                speedDecrement = value;

            }
            get
            {
                return speedDecrement;

            }
        }

        public float AngularVelocityDecrement
        {
            set
            {
                angularVelocityDecrement = value;

            }
            get
            {
                return angularVelocityDecrement;

            }
        }
        public Fire AccessFire
        {
            get
            { return fire; }

            set
            {
                fire = value;
            }
        }


        public float AngularVelocityIncrement
        {

            get
            { return angularVelocityIncrement; }

            set
            {
                angularVelocityIncrement = value;
            }
        }

        public float SpeedIncrement
        {

            get
            { return speedIncrement; }

            set
            {
                speedIncrement = value;
            }
        }
        public Weapon AccessWeapons
        {
            get
            { return weapon; }

            set
            {
                weapon = value;
            }

        }
        public List<Explosion> AccessExplosions
        {
            get
            { return theExplosions; }



        }
        public void LoadPlayer()
        {


        }

        public Rectangle SpriteRectangle
        {
            set
            {
                spriteRec = value;

            }
            get
            {
                return spriteRec;

            }

        }
        public void DrawAsteroids()
        {
            foreach (Explosion i in theExplosions)
                spriteBatch.Draw(i.Image, i.Position, null, Color.White, i.Angle, i.ImageCenter, 1f, SpriteEffects.None, 0);




        }
        public Vector2 Position
        {
            get
            {
                return position;
            }

            set { position = value; }
        }
        public float AngularVelocity
        {

            set
            {
                angularVelocity = value;
            }
            get
            {
                return angularVelocity;

            }
        }
        public void InitializePlayer()
        {
            oldState = Keyboard.GetState();
            oldPadStatePlayer = GamePad.GetState(PlayerIndex.One);

        }
        public float AngleIncrement
        {
            set
            {
                angleIncrement = value;

            }
            get
            {

                return angleIncrement;

            }


        }

        public void SetPosition(float x, float y)
        {
            position.X = x;
            position.Y = y;


        }
        public void SetPosition(Vector2 pos)
        {
            position = pos;


        }
        public void SetPositionX(float x)
        {
            position.X = x;



        }
        public void SetPositionY(float y)
        {
            position.Y = y;



        }


        public void UpdateExplosions()
        {
            foreach (Explosion i in theExplosions)
            {

                if (i.Position.X < (0 - i.image.Width))
                {
                    i.SetPositionX(AccessPlayer.Graphics.PreferredBackBufferWidth + Image.Width);
                    // thePosition.X = Window.ClientBounds.Width + image.Width;
                    //thePosition = new Vector2(X,Y);
                }
                if (i.Position.X > (AccessPlayer.Graphics.PreferredBackBufferWidth + i.Image.Width))
                {
                    i.SetPositionX(0 - i.Image.Width);

                }
                if (i.Position.Y < (0 - i.Image.Height))
                {
                    i.SetPositionY(AccessPlayer.Graphics.PreferredBackBufferHeight + i.Image.Height);

                }


                if (i.Position.Y > AccessPlayer.Graphics.PreferredBackBufferHeight + i.Image.Height)
                {
                    i.SetPositionY(0 - i.Image.Height);

                }

                i.SpriteRectangle = new Rectangle((int)i.Position.X, (int)i.Position.Y, i.Image.Width, i.Image.Height);


                i.SetPosition(i.Velocity + i.Position);
                i.ImageCenter = new Vector2(i.SpriteRectangle.Width / 2, i.SpriteRectangle.Height / 2);

                // TODO: Add your update logic here
               




                i.Angle += i.AngularVelocity;
                //  AngularVelocity*=AngularVelocityDecrement;
                if (i.Angle > fullRotation)
                    i.Angle -= fullRotation;
                if (Angle < 0)
                    Angle += fullRotation;
                // i.SetVelocityX(i.Velocity.X + (float)Math.Cos(Angle) * Speed);
                // i.SetVelocityY(Velocity.Y + (float)Math.Sin(Angle) * Speed);
                // SetVelocityX(Velocity.X * VelocityDecrement);
                // SetVelocityY(Velocity.Y * VelocityDecrement);

                // Speed *= SpeedDecrement;



            }

        }


        public float Angle
        {
            set
            {
                angle = value;
            }

            get
            {
                return angle;
            }

        }

        public float X
        {
            set
            {
                x = value;
            }

            get
            {
                return x;
            }
        }


        public float Y
        {
            set
            {
                y = value;
            }
            get
            {
                return y;

            }
        }
        public float Speed
        {
            set
            {
                speed = value;
            }
            get
            {
                return speed;

            }
        }

        public int Frame
        {

            set
            {
                frame = value;
            }
            get
            {
                return frame;
            }
        }
        public Texture2D Image
        {
            set
            {
                image = value;
            }

            get
            {
                return image;
            }

        }

        public Vector2 Velocity
        {
            get
            {
                return velocity;

            }



        }




        public float distance(float x1, float y1, float x2, float y2)
        {


            float dx = x2 - x1;
            float dy = y2 - y1;


            return (float)Math.Sqrt((dx * dx) + (dy * dy));
        }
        public bool Collided(Asteroid asteroid1, Asteroid weapon, Texture2D RadiusImage1)
        {
            if (distance(asteroid1.Position.X, asteroid1.Position.Y, weapon.Position.X, weapon.Position.Y) < FindRadius(RadiusImage1))
                return true;
            else
                return false;
        }
        public bool Collided(Asteroid asteroid1, Weapon weapon, Texture2D RadiusImage1)
        {
            if (distance(asteroid1.Position.X, asteroid1.Position.Y, weapon.Position.X, weapon.Position.Y) < FindRadius(RadiusImage1))
                return true;
            else
                return false;
        }
        public int FindRadius(Texture2D image)
        {
            return ((image.Width / 2) + (image.Height / 2) / 2);
        }
        public void SetVelocityY(float y)
        {
            velocity.Y = y;

        }
        public void CreateExplosions(float x, float y,float xv, float yv, int number)
        {
            random = new Random();
            for (int i = 0; i < number; i++)
            {
               
                theExplosions.Add(new Explosion(image, x, y,xv,yv));
                theExplosions[i].AngularVelocity = (xv + yv) / 40;

            }

        }

        public void SetVelocityX(float x)
        {
            velocity.X = x;

        }
    }
}
