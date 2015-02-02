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
    class Enemy 
    {

        Texture2D image;
        Rectangle spriteRec;
        Vector2 imageCenter;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont theFont;
       private Color theColor;
        KeyboardState oldState;
        GamePadState oldPadStatePlayer;
        private int time;
        private GameTime timer;
        private float x, y, angle;
        private Vector2 position, velocity;
        private float velocityIncrement, velocityDecrement;
        private int frame;
        private float angleIncrement, angularVelocityIncrement, angularVelocityDecrement, angularVelocity;
        private const float fullRotation = ((float)(2 * Math.PI));
        private float speed, speedIncrement, speedDecrement;
        private const int CHANGEDIRECTIONTIME = 1020;
        private EnemyWeapon weapon;
        private static float oldx, oldy;
        private Asteroid asteroid;
        private List<Enemy> theEnimies;
        Player player;
        private Enemy enemy;
        Fire fire;
        private Random random;

        public Enemy()
        {


        }
       
        public Enemy(Texture2D image, float x, float y, EnemyWeapon weapon, Color theColor)
        {

            speedIncrement = 0.004f;
            speedDecrement = 0.96f;
            angularVelocityIncrement = .001111111f;
            AngularVelocityDecrement = 0.97f;
            velocityDecrement = 0.995f;
            angleIncrement = 0.1f;
            random = new Random();
            this.theColor = theColor;
            this.weapon = weapon;

            this.image = image;
            this.x = x;
            this.y = y;

            //timeSinceLastFrame = weapon.Timer.ElapsedGameTime;
            position = new Vector2(x, y);
            velocity = new Vector2(0f, 0f);
            //angle = (float)((3 * Math.PI) / 2);
            angle = 0;
            angularVelocity = 0;
            time = CHANGEDIRECTIONTIME;
        }

        public GraphicsDeviceManager Graphics
        {

            set { graphics = value; }
            get { return graphics; }

        }

        public Color TheColor
        {
            set
            {
                theColor = value;

            }
            get
            {
                return theColor;

            }
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

        public  Enemy AccessEnemy
        {
            set
            {
                enemy = value;

            }
            get
            {
                return enemy;
            }

        }
        public List<Enemy> AccessEnemyList
        { 
            set
            {
                theEnimies = value;

            }
            get
            {
                return theEnimies;
            }

        
        }
        public void  InitializeEnemies()
        {
            theEnimies = new List<Enemy>();
        
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
        public int Time
        {
            set
            {
                time = value;

            }
            get
            {
                return time;

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
        public EnemyWeapon AccessWeapons
        {
            get
            { return weapon; }

            set
            {
                weapon = value;
            }

        }
        
        public void LoadPlayer()
        {


        }

        public void CreateEnemies(Enemy a, float x, float y, int number, int type, int bound1, int bound2)
        {
           
            for (int i = 1; i <= number; i++)
            {

                float velocity1 = random.Next(bound1, bound2) + (float)random.NextDouble();
                float velocity2 = random.Next(bound1, bound2) + (float)random.NextDouble();
                int colorPick = random.Next(-1, 4);

                switch (colorPick)
                { 
                    case 0:
                        TheColor = Color.Blue;
                        break;

                     case 1:
                        TheColor = Color.Orange;
                        break;
                    case 3:
                        TheColor = Color.Red;
                        break;
                    default:
                        TheColor = Color.White;
                        break;

                
                
                }
                theEnimies.Add(new Enemy(a.Image,x,y,a.weapon,TheColor));
             ;




            }

        }
        public Rectangle BoundingRectangle
        {
            get
            {
                int left = (int)Math.Round(Position.X - imageCenter.X) + spriteRec.X;
                int top = (int)Math.Round(Position.Y - imageCenter.Y) + spriteRec.Y;

                return new Rectangle(left, top, spriteRec.Width, spriteRec.Height);
            }
        }
        public void DrawEnemy(SpriteBatch spriteBatch)
        {
            foreach(Enemy e in theEnimies)
             spriteBatch.Draw(e.Image, e.Position, null, e.TheColor, e.Angle, e.ImageCenter, 1f, SpriteEffects.None, 0);
            



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

        public GameTime Timer
        {

            set { timer = value; }
            get { return timer; }
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
        public void UpdateEnemy(GameTime theTime)
        {
            
            foreach (Enemy e in theEnimies)
            {
                e.Timer = theTime;
                random = new Random();
                if (e.Position.X < (0 - e.Image.Width))
                {
                    e.SetPositionX(Graphics.PreferredBackBufferWidth + e.Image.Width);
                    // thePosition.X = Graphics.PreferredBackBufferWidth + image.Width;
                    //thePosition = new Vector2(X,Y);
                }
                if (e.Position.X > (Graphics.PreferredBackBufferWidth + e.Image.Width))
                {
                    e.SetPositionX(0 - e.Image.Width);

                }
                if (e.Position.Y < (0 - e.Image.Height))
                {
                    e.SetPositionY(Graphics.PreferredBackBufferHeight + e.Image.Height);

                }


                if (e.Position.Y > Graphics.PreferredBackBufferHeight + e.Image.Height)
                {
                    e.SetPositionY(0 - e.Image.Height);

                }

                e.SpriteRectangle = new Rectangle((int)e.Position.X, (int)e.Position.Y, e.Image.Width, e.Image.Height);


                e.SetPosition(e.Velocity + e.Position);
                e.ImageCenter = new Vector2(e.SpriteRectangle.Width / 2, e.SpriteRectangle.Height / 2);



                e.Time -= e.Timer.ElapsedGameTime.Milliseconds;

                if (e.Time < 0)
                {
                    e.Velocity = new Vector2(random.Next(-3, 3), random.Next(-3, 3));
                    e.AngularVelocity += (float)random.NextDouble() * (float)random.Next(-1, 1) * 2;
                    if (Math.Abs(e.AngularVelocity) > .01f)
                     
                        e.AccessWeapons.FireWeapon(e);


                    e.Time = CHANGEDIRECTIONTIME;

                }




                e.Angle += e.AngularVelocity;
                e.AngularVelocity *= e.AngularVelocityDecrement;
                if (e.Angle > fullRotation)
                    angle -= fullRotation;
                if (e.Angle < 0)
                    e.Angle += fullRotation;
                e.SetVelocityX(e.Velocity.X + (float)Math.Cos(e.Angle) * e.Speed);
                e.SetVelocityY(e.Velocity.Y + (float)Math.Sin(e.Angle) * e.Speed);
                e.SetVelocityX(e.Velocity.X * e.VelocityDecrement);
                e.SetVelocityY(e.Velocity.Y * e.VelocityDecrement);
                e.SetPositionX(e.Position.X + e.Velocity.X);
                e.SetPositionY(e.Position.Y + e.Velocity.Y);
                e.Speed *= e.SpeedDecrement;
            }


        }
        public float distance(float x1, float y1, float x2, float y2)
        {


            float dx = x2 - x1;
            float dy = y2 - y1;


            return (float)Math.Sqrt((dx * dx) + (dy * dy));
        }
        public bool Collided(float x, float y, Asteroid asteroid, Texture2D RadiusImage1)
        {
            if (distance(x, y, asteroid.Position.X, asteroid.Position.Y) < FindRadius(RadiusImage1))
                return true;
            else
                return false;
        }
        public int FindRadius(Texture2D image)
        {
            return ((image.Width / 2) + (image.Height / 2) / 2);
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

            set { velocity = value; }

        }





        public void SetVelocityY(float y)
        {
            velocity.Y = y;

        }

        public void SetVelocityX(float x)
        {
            velocity.X = x;

        }


    }
}
