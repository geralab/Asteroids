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
    class Asteroid 
    {
        private List<Texture2D> images;
        private Texture2D image;
        private bool visible;
        private Rectangle spriteRec;
        private Vector2 imageCenter;
        GraphicsDeviceManager graphics;
        private int theType;
        private Asteroid hostAsteroid;
      
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
        private static float oldx, oldy;
        private const int VELOCITYE = 20;
        private int breakUpStage;
        private const int COLLISIONINTERVAL = 3500;
        private static int fireLoop = 0;
        private int loop;
        private int plusOrMinus;
        private Random random;
        public enum AsteroidType {
            ChunkSmall1,Small, ChunkMedium1,ChunkMedium2,ChunkMedium3,ChunkMedium4,ChunkMedium5,Medium, 
            ChunkLarge1,ChunkLarge2,ChunkLarge3,Large,ChunkExtra1,ChunkExtra2,ChunkExtra3,ChunkExtra4,ExtraLarge};
       private Player player;
       private Fire fire;
       private TimeSpan timeSinceLastFrame;
       private GameTime timer;
        private int time;
        private List<Asteroid> theAsteroids = new List<Asteroid>();

        public Asteroid()
        {


        }


        public Asteroid(List<Texture2D> images,  Weapon weapon, SpriteFont theFont)
        {
            this.images = images;
            speedIncrement = 0.003f;
            speedDecrement = 0.96f;
            angularVelocityIncrement = .0012f;
            AngularVelocityDecrement = 0.97f;
            velocityDecrement = 0.995f;
            angleIncrement = 0.1f;
            this.weapon = weapon;
            this.theFont = theFont;
            plusOrMinus = 0;
            visible = true;
            position = new Vector2(x, y);
            velocity = new Vector2(0f, 0f);
            time = COLLISIONINTERVAL;
            angle = 0;
            angularVelocity = .000000123f;
        }


        public Asteroid(Texture2D image, float x, float y, float xv, float yv)
        {
            this.x = x;
            this.y = y;
            speedIncrement = 0.003f;
            speedDecrement = 0.96f;
            time = COLLISIONINTERVAL;
            angularVelocityIncrement = .0012f;
            AngularVelocityDecrement = 0.96f;
            velocityDecrement = 0.995f;
            angleIncrement = 0.1f;
            plusOrMinus = 0;
            velocity = new Vector2(xv, yv);
            
            this.image = image;

            //timeSinceLastFrame = weapon.Timer.ElapsedGameTime;
            position = new Vector2(x, y);

            angle = 0;
            angularVelocity = .13f;
            visible = true;
        }

        public int GetAsteroidCount()

        { return theAsteroids.Count; }


        public Asteroid(float x, float y, float xv, float yv, Asteroid superAsteroid, int type)
        {
            this.x = x;
            this.y = y;
            speedIncrement = 0.003f;
            speedDecrement = 0.96f;
            time = 0;
            angularVelocityIncrement = .0012f;
            AngularVelocityDecrement = 0.96f;
            velocityDecrement = 0.995f;
            angleIncrement = 0.1f;
            plusOrMinus = 0;
            velocity = new Vector2(xv, yv);
            theType = type;

            image = superAsteroid.Images[type];

            //timeSinceLastFrame = weapon.Timer.ElapsedGameTime;
            position = new Vector2(x, y);
            visible = true;
            angle = 0;
            angularVelocity  = (xv + yv) / 40;
        }

        public int Loop
        {

            set
            {
                loop = value;

            }
            get
            {
                return loop;

            }
        
        
        
        
        
        
        }
        public bool Visible
        {

            set
            {
                visible= value;

            }
            get
            {
                return  visible;

            }






        }



        public int PlusOrMinus
        {
            set
            {
                plusOrMinus = value;

            }
            get
            {
                return plusOrMinus;

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

        public Asteroid HostAsteroid
        {
            get { return hostAsteroid; }
            set { hostAsteroid = value; }

        
        
        }
        public int TheType
        {

            set { theType = value; }
            get { return theType; }
        
        
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
        public void InitializeList()
        {
           theAsteroids = new List<Asteroid>();
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

        public int BreakUpStage
        {


            get {
               return breakUpStage;
            
            
            }
            set { breakUpStage = value; }
        
        
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
        public GameTime Timer
        {

            set { timer = value; }
            get { return timer; }
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
        public List<Asteroid> AccessAsteroid
        {
            get
            { return theAsteroids ; }

            set { theAsteroids = value; }

            

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
        public void DrawAsteroids(SpriteBatch spriteBatch)
        {

            foreach (Asteroid i in theAsteroids)
            {
                if(i.Visible)
                 spriteBatch.Draw(i.Image, i.Position, null, Color.White, i.Angle, i.ImageCenter, 1f, SpriteEffects.None, 0);
            }

           

                
            
            

           // spriteBatch.DrawString(theFont, "LOOP" + Loop, new Vector2(0, 50), Color.Green);

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
         public int Time
        {
            get {
                return time;
            }
            set { time = value; }
        
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


        public void UpdateAsteroids(Asteroid asteroidSuper, GameTime theTime, List<SoundEffect> sounds)
        {
            Timer = theTime;
            theAsteroids[0].Visible = false;
            for (int i = 0; i < theAsteroids.Count; i++)
            {

                if (theAsteroids[i].Position.X < (0 - theAsteroids[i].Image.Width))
                {
                    theAsteroids[i].SetPositionX(Graphics.PreferredBackBufferWidth + theAsteroids[i].Image.Width);
                    // thePosition.X = Window.ClientBounds.Width + image.Width;
                    //thePosition = new Vector2(X,Y);
                }
                if (theAsteroids[i].Position.X > (Graphics.PreferredBackBufferWidth + theAsteroids[i].Image.Width))
                {
                    theAsteroids[i].SetPositionX(0 - theAsteroids[i].Image.Width);

                }
                if (theAsteroids[i].Position.Y < (0 - theAsteroids[i].Image.Height))
                {
                    theAsteroids[i].SetPositionY(Graphics.PreferredBackBufferHeight + theAsteroids[i].Image.Height);

                }


                if (theAsteroids[i].Position.Y > Graphics.PreferredBackBufferHeight + theAsteroids[i].Image.Height)
                {
                    theAsteroids[i].SetPositionY(0 - theAsteroids[i].Image.Height);

                }

                theAsteroids[i].SpriteRectangle = new Rectangle((int)theAsteroids[i].Position.X, (int)theAsteroids[i].Position.Y, theAsteroids[i].Image.Width, theAsteroids[i].Image.Height);


                theAsteroids[i].SetPosition(theAsteroids[i].Velocity + theAsteroids[i].Position);
                theAsteroids[i].ImageCenter = new Vector2(theAsteroids[i].SpriteRectangle.Width / 2, theAsteroids[i].SpriteRectangle.Height / 2);

                //Colllision
                for (int w = 0; w < AccessWeapons.TheWeapons.Count; w++)
                {
                    Weapon p = AccessWeapons;
                    if(i !=0){
                        if (Collided(theAsteroids[i], p.TheWeapons[w], theAsteroids[i].Image))
                        {
                            sounds[(int)Game1.Sounds.Boom].Play();
                           
                                if (theAsteroids[i].TheType == (int)Asteroid.AsteroidType.Small)
                                {
                                    Vector2 vtemp = theAsteroids[i].Velocity;
                                    Vector2 ptemp = theAsteroids[i].Position;
                                   
                                    theAsteroids.RemoveAt(i);
                                    --i;
                                    
                                    asteroidSuper.CreateAsteroids(asteroidSuper, ptemp.X , ptemp.Y , 2, (int)Asteroid.AsteroidType.ChunkSmall1, -VELOCITYE, VELOCITYE);


                                    
                                    AccessWeapons.TheWeapons.RemoveAt(w);
                                    --w;
                                    AccessPlayer.Score += 100;

                                }
                                else if (theAsteroids[i].TheType == (int)Asteroid.AsteroidType.Medium)
                                {
                                    Vector2 ptemp = theAsteroids[i].Position;
                                    theAsteroids.RemoveAt(i);
                                    --i;
                                    asteroidSuper.CreateAsteroids(asteroidSuper, ptemp.X , ptemp.Y , 1, (int)Asteroid.AsteroidType.ChunkMedium1, -VELOCITYE, VELOCITYE);
                                   
                                    asteroidSuper.CreateAsteroids(asteroidSuper, ptemp.X , ptemp.Y , 1, (int)Asteroid.AsteroidType.ChunkMedium2, -VELOCITYE, VELOCITYE);
                                    asteroidSuper.CreateAsteroids(asteroidSuper, ptemp.X, ptemp.Y, 1, (int)Asteroid.AsteroidType.ChunkMedium3, -VELOCITYE, VELOCITYE);
                                    asteroidSuper.CreateAsteroids(asteroidSuper, ptemp.X, ptemp.Y, 1, (int)Asteroid.AsteroidType.ChunkMedium4, -VELOCITYE, VELOCITYE);
                                    asteroidSuper.CreateAsteroids(asteroidSuper, ptemp.X, ptemp.Y, 1, (int)Asteroid.AsteroidType.ChunkMedium5, -VELOCITYE, VELOCITYE);
                                    AccessWeapons.TheWeapons.RemoveAt(w);
                                    --w;

                                    AccessPlayer.Score += 200;

                                }
                                else if (theAsteroids[i].TheType == (int)Asteroid.AsteroidType.Large)
                                {
                                    Vector2 ptemp = theAsteroids[i].Position;
                                    theAsteroids.RemoveAt(i);
                                    --i;
                                    
                                    asteroidSuper.CreateAsteroids(asteroidSuper, ptemp.X , ptemp.Y , 1, (int)Asteroid.AsteroidType.ChunkLarge1, -VELOCITYE, VELOCITYE);
                                   
                                   
                                    asteroidSuper.CreateAsteroids(asteroidSuper, ptemp.X , ptemp.Y , 1, (int)Asteroid.AsteroidType.ChunkLarge2, -VELOCITYE, VELOCITYE);
                                    asteroidSuper.CreateAsteroids(asteroidSuper, ptemp.X, ptemp.Y, 1, (int)Asteroid.AsteroidType.ChunkLarge3, -VELOCITYE, VELOCITYE);
                                    AccessWeapons.TheWeapons.RemoveAt(w);
                                    --w;

                                    AccessPlayer.Score += 300;

                                }
                                else if (theAsteroids[i].TheType == (int)Asteroid.AsteroidType.ExtraLarge)
                                {
                                    Vector2 ptemp = theAsteroids[i].Position;
                                    theAsteroids.RemoveAt(i);
                                    --i;
                                   
                                    asteroidSuper.CreateAsteroids(asteroidSuper, ptemp.X , ptemp.Y , 1, (int)Asteroid.AsteroidType.ChunkExtra1, -VELOCITYE, VELOCITYE);
                                    asteroidSuper.CreateAsteroids(asteroidSuper, ptemp.X , ptemp.Y , 1, (int)Asteroid.AsteroidType.ChunkExtra2, -VELOCITYE, VELOCITYE);
                                    asteroidSuper.CreateAsteroids(asteroidSuper, ptemp.X , ptemp.Y , 1, (int)Asteroid.AsteroidType.ChunkExtra3, -VELOCITYE, VELOCITYE);
                                    asteroidSuper.CreateAsteroids(asteroidSuper, ptemp.X , ptemp.Y , 1, (int)Asteroid.AsteroidType.ChunkExtra4, -VELOCITYE, VELOCITYE);
                                    AccessWeapons.TheWeapons.RemoveAt(w);
                                    --w;
                                    AccessPlayer.Score += 400;

                                }




                                if (theAsteroids[i].TheType == (int)Asteroid.AsteroidType.ChunkSmall1)
                                {

                                    theAsteroids.RemoveAt(i);
                                    --i;
                                    AccessPlayer.Score += 50;

                                }
                                if (theAsteroids[i].TheType == (int)Asteroid.AsteroidType.ChunkMedium1)
                                {

                                    theAsteroids.RemoveAt(i);
                                    --i;
                                    AccessPlayer.Score += 100;


                                }
                                if (theAsteroids[i].TheType == (int)Asteroid.AsteroidType.ChunkMedium2)
                                {

                                    theAsteroids.RemoveAt(i);
                                    --i;
                                    AccessPlayer.Score += 100;


                                }
                                if (theAsteroids[i].TheType == (int)Asteroid.AsteroidType.ChunkMedium3)
                                {

                                    theAsteroids.RemoveAt(i);
                                    --i;
                                    AccessPlayer.Score += 100;


                                }
                                if (theAsteroids[i].TheType == (int)Asteroid.AsteroidType.ChunkMedium4)
                                {

                                    theAsteroids.RemoveAt(i);
                                    --i;
                                    AccessPlayer.Score += 100;


                                }
                                if (theAsteroids[i].TheType == (int)Asteroid.AsteroidType.ChunkMedium5)
                                {

                                    theAsteroids.RemoveAt(i);
                                    --i;
                                    AccessPlayer.Score += 100;


                                }
                                if (theAsteroids[i].TheType == (int)Asteroid.AsteroidType.ChunkLarge1)
                                {

                                    theAsteroids.RemoveAt(i);
                                    --i;
                                    AccessPlayer.Score += 150;


                                }
                                if (theAsteroids[i].TheType == (int)Asteroid.AsteroidType.ChunkLarge2)
                                {

                                    theAsteroids.RemoveAt(i);
                                    --i;
                                    AccessPlayer.Score += 150;


                                }
                                if (theAsteroids[i].TheType == (int)Asteroid.AsteroidType.ChunkLarge3)
                                {

                                    theAsteroids.RemoveAt(i);
                                    --i;

                                    AccessPlayer.Score += 150;
                                }
                                if (theAsteroids[i].TheType == (int)Asteroid.AsteroidType.ChunkExtra1)
                                {

                                    theAsteroids.RemoveAt(i);
                                    --i;
                                    AccessPlayer.Score +=200;


                                }
                                if (theAsteroids[i].TheType == (int)Asteroid.AsteroidType.ChunkExtra2)
                                {

                                    theAsteroids.RemoveAt(i);
                                    --i;
                                    AccessPlayer.Score +=200;


                                }
                                if (theAsteroids[i].TheType == (int)Asteroid.AsteroidType.ChunkExtra3)
                                {

                                    theAsteroids.RemoveAt(i);
                                    --i;
                                    AccessPlayer.Score +=200;

                                }
                                if (theAsteroids[i].TheType == (int)Asteroid.AsteroidType.ChunkExtra4)
                                {

                                    theAsteroids.RemoveAt(i);
                                    --i;
                                    AccessPlayer.Score +=200;



                                }


                                //if (i > 0)
                               // theAsteroids[i].Time = COLLISIONINTERVAL;
                                Loop = theAsteroids[i].TheType;
                                theAsteroids.TrimExcess();

                            }
                        }
                        




                    }


                //check asteroid collisions
                    for (int j = 0; j < theAsteroids.Count; j++)
                    {
                        if (Collided(theAsteroids[i], theAsteroids[j], theAsteroids[i].Image))
                        {
                           
                            theAsteroids[j].Time -= Timer.ElapsedGameTime.Milliseconds;

                            if (theAsteroids[j].Time < 0)
                            {
                               // sounds[(int)Game1.Sounds.AsteroidCollsion].Play();
                                
                                switch (theAsteroids[i].PlusOrMinus)
                                {
                                    case 0:
                                        theAsteroids[i].SetVelocityX((-theAsteroids[i].Velocity.X )- (float)random.NextDouble());
                                        theAsteroids[i].SetVelocityY((-theAsteroids[i].Velocity.Y ) - (float)random.NextDouble());
                                        theAsteroids[i].AngularVelocity = -theAsteroids[i].AngularVelocity;
                                        theAsteroids[j].SetVelocityX((-theAsteroids[j].Velocity.X ) - (float)random.NextDouble());
                                        theAsteroids[j].SetVelocityY((-theAsteroids[j].Velocity.Y ) - (float)random.NextDouble());
                                        theAsteroids[j].AngularVelocity = -theAsteroids[j].AngularVelocity;
                                        theAsteroids[i].PlusOrMinus = 1;
                                        break;
                                    case 1:
                                        theAsteroids[i].SetVelocityX((-theAsteroids[i].Velocity.X )+ (float)random.NextDouble());
                                        theAsteroids[i].SetVelocityY((-theAsteroids[i].Velocity.Y ) + (float)random.NextDouble());
                                        theAsteroids[i].AngularVelocity = -theAsteroids[i].AngularVelocity;
                                        theAsteroids[j].SetVelocityX((-theAsteroids[j].Velocity.X ) + (float)random.NextDouble());
                                        theAsteroids[j].SetVelocityY((-theAsteroids[j].Velocity.Y ) + (float)random.NextDouble());
                                        theAsteroids[j].AngularVelocity = -theAsteroids[j].AngularVelocity;
                                        theAsteroids[i].PlusOrMinus = 0;
                                        break;
                                 
                                }
                                theAsteroids[j].Time = COLLISIONINTERVAL;
                            }

                        }
                    }




                    theAsteroids[i].Angle += theAsteroids[i].AngularVelocity;
                    //  AngularVelocity*=AngularVelocityDecrement;
                    if (theAsteroids[i].Angle > fullRotation)
                        theAsteroids[i].Angle -= fullRotation;
                    if (theAsteroids[i].Angle < 0)
                        theAsteroids[i].Angle += fullRotation;
                    // i.SetVelocityX(i.Velocity.X + (float)Math.Cos(Angle) * Speed);
                    // i.SetVelocityY(Velocity.Y + (float)Math.Sin(Angle) * Speed);
                    // SetVelocityX(Velocity.X * VelocityDecrement);
                    // SetVelocityY(Velocity.Y * VelocityDecrement);

                    // Speed 


                
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
        public List<Texture2D> Images
        {
            set
            {
                images = value;
            }

            get
            {
                return images;
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
        public void CreateAsteroids(Asteroid a,int number, int type, int bound1, int bound2)
        {
            random = new Random();
            for (int i = 1; i <= number; i++)
            {
                int graphicsWidth = random.Next(Graphics.PreferredBackBufferWidth);
                int graphicsHeight = random.Next(Graphics.PreferredBackBufferHeight);
                float velocity1 = random.Next(bound1, bound2)+ (float)random.NextDouble();
                float velocity2 = random.Next(bound1, bound2)+ (float)random.NextDouble();
               theAsteroids.Add(new Asteroid(graphicsWidth, graphicsHeight, velocity1, velocity2, a, type));
               
                theAsteroids[i-1].HostAsteroid = a;
              

            }

        }

        public void CreateAsteroids( Asteroid a, float x, float y, int number, int type, int bound1, int bound2)
        {
            random = new Random();
            for (int i = 1; i <= number; i++)
            {
                
                float velocity1 = random.Next(bound1, bound2) + (float)random.NextDouble();
                float velocity2 = random.Next(bound1, bound2) + (float)random.NextDouble();
                theAsteroids.Add(new Asteroid(x, y, velocity1, velocity2, a, type));
                theAsteroids[i-1].HostAsteroid = a;
                
                 
                  

            }

        }

        public void SetVelocityX(float x)
        {
            velocity.X = x;

        }
      
    }
}

