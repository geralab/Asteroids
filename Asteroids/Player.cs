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
    class Player 
    {
        
        Texture2D image;
        private double healthPoints;
        Rectangle spriteRec;
        Vector2 imageCenter;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont theFont;
        private int score;
        private KeyboardState oldState;
        private GamePadState oldPadStatePlayer;
        private KeyboardState keyState;
        private GamePadState player1State;
        private Color chosenColor;
        private int time;
        private const int FIREINTERVAL = 1001;
        int fireTime;
        private float currentAngularVelocity;
        private const int HEALTHPOINTDEDUCTION = 15;
        private int timeSinceLastCollision, timeSinceLastFlash, timeSinceLastCollision2;
        private const int MAXCOLLISIONINTERVAL = 900;
        private GameTime timer;
        private const int FLASH = 1500;
        private const int INTENSITY = 250;
        private float x, y,angle;
        private Vector2 position,velocity;
        private bool hit;
        private float velocityIncrement, velocityDecrement;
        private int frame;
        private double degree = 0;
        private float angleIncrement, angularVelocityIncrement,angularVelocityDecrement, angularVelocity;
        private const float fullRotation = ((float)(2*Math.PI));
        private float  speed,speedIncrement,speedDecrement;
        private const int FIREWEAPONINTERVAL = 100;
        private Weapon weapon;
        private static float oldx, oldy;
        private List<Asteroid> theAsteroids;
        private EnemyWeapon enemyWeapon;
        private Asteroid asteroid;
        Color theColor;
        Player player;
        Fire fire;
        private Enemy enemy;
       
        public Player()
        {
        
        
        }
        public Player(Texture2D image,float x, float y, int frame,Weapon weapon,Fire fire, KeyboardState oldState,
        GamePadState oldPadStatePlayer,SpriteFont theFont, Asteroid asteroid)
        {
            this.fire = fire;
            speedIncrement = 0.004f;
            speedDecrement = 0.96f;
            angularVelocityIncrement = .001111111f;
            AngularVelocityDecrement = 0.96f;
            velocityDecrement = 0.995f;
            angleIncrement = 0.1f;
            this.asteroid = asteroid;
            this.weapon = weapon;
            this.theFont = theFont;
            this.oldPadStatePlayer = oldPadStatePlayer;
            this.oldState = oldState;
            theColor = Color.White;
            this.image = image;
            this.x = x;
            this.y = y;
            this.frame = frame;
            healthPoints = 150;
            currentAngularVelocity = 0;

            timeSinceLastCollision = 0;
            timeSinceLastCollision2 = 0;
            timeSinceLastFlash = 0;
            chosenColor = Color.White;
            fireTime = 0;
            hit = false;
             //timeSinceLastFrame = weapon.Timer.ElapsedGameTime;
            position = new Vector2(x, y);
            velocity = new Vector2(0f, 0f);
            angle = (float)((3*Math.PI)/2);
            angularVelocity = 0;
            time = 0;
        }

        public double HealthPoints
        {
            set { healthPoints = value; }
            get { return healthPoints; }
        
        
        }
        public GraphicsDeviceManager Graphics {

            set { graphics = value; }
            get { return graphics; }
        
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
        public Color ChosenColor
        {
            set
            {
                chosenColor = value;

            }
            get
            {
                return chosenColor;

            }
        }
        public KeyboardState OldKeyState
        {
            get {return oldState ;}
            set { oldState=value;}
        
        }
        public GamePadState OldPadState
        {
            get { return oldPadStatePlayer; }
            set { oldPadStatePlayer = value; }

        }
        public GamePadState PlayerState
        {
            get { return player1State; }
            set {player1State = value; }

        }

        public KeyboardState KeyState
        {
            get { return keyState; }
            set { keyState = value; }

        }

        public float VelocityIncrement
        {
            set
            {
                 velocityIncrement = value;

            }
            get
            {
                return velocityIncrement ;

            }
        }
        public int Score
        {
            set
            {
                score = value;

            }
            get
            {
                return score;

            }
        
        }
        public float CurrentAngularVelocity
        {
            set
            {
                currentAngularVelocity = value;

            }
            get
            {
                return currentAngularVelocity;

            }

        }

        public bool Hit
        {

            set
            {
                hit = value;

            }
            get
            {
                return hit;

            }
        
        }

        public double Degree
        {

            get
            {
                return Angle * (180/Math.PI);
            
            
            }
            set {

                degree = value;
            
            }
        
        
        }
        public Color TheColor
        {
            set
            {
                theColor= value;

            }
            get
            {
                return theColor;

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
                return velocityDecrement ;

            }
        }


        public void InitializeList()
        {
            theAsteroids = new List<Asteroid>();
        
        }
        public float SpeedDecrement
        {
            set
            {
                speedDecrement = value;

            }
            get
            {
                return speedDecrement ;

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
                return angularVelocityDecrement ;

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
        public EnemyWeapon AccesEnemyWeapon
        {
            get
            { return enemyWeapon ; }

            set
            {
                enemyWeapon = value;
            }

        }
        public void LoadPlayer()
        {

            
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
        public void DrawPlayer(SpriteBatch spriteBatch)
        {
           // if(HealthPoints >0)
                 spriteBatch.Draw(image, Position, null, TheColor, Angle, imageCenter, 1f, SpriteEffects.None, 0);


            ////Debug
          //  spriteBatch.DrawString(theFont, "X: " + Position.X, new Vector2(0f, 0f), Color.Green);
           // spriteBatch.DrawString(theFont, "Y: " + Position.Y, new Vector2(0f, 20f), Color.Green);
           // spriteBatch.DrawString(theFont, "Angle: " + (Angle*180/Math.PI), new Vector2(0f, 60f), Color.White);
            //spriteBatch.DrawString(theFont, "FireCount: " + AccessFire.FireLoop, new Vector2(0f, 60f), Color.Green);
            //spriteBatch.DrawString(theFont, "TheWeapons: " + AccessWeapons.TheWeapons.Capacity, new Vector2(0f, 80f), Color.Green);
            //spriteBatch.DrawString(theFont, "PVX: " + Velocity.X, new Vector2(0f, 100f), Color.Green);
           // spriteBatch.DrawString(theFont, "PVY: " + Velocity.Y, new Vector2(0f, 120f), Color.Green);
           // spriteBatch.DrawString(theFont, "Hit " + Hit, new Vector2(0f, 140f), Color.Green);
            //spriteBatch.DrawString(theFont, "ANV: " + VelocityDecrement, new Vector2(0f, 160f), Color.Green);
            //spriteBatch.DrawString(theFont, "W: " + Graphics.PreferredBackBufferWidth, new Vector2(0f, 180f), Color.Green);
            //spriteBatch.DrawString(theFont, "H: " + Graphics.PreferredBackBufferHeight, new Vector2(0f, 200f), Color.Green);
            spriteBatch.DrawString(theFont, "ASTEROID COUNT " + (asteroid.AccessAsteroid.Count - 1), new Vector2(0f, 25f), Color.White);
            spriteBatch.DrawString(theFont, "SCORE " + Score, new Vector2(Graphics.PreferredBackBufferWidth - 300, 0), Color.Gold);
            spriteBatch.DrawString(theFont, "HEALTH POINTS " + HealthPoints, new Vector2(0f, 0f), Color.White);


        
        }
        public Vector2 Position
        {
            get
            { 
                return position;
            }

            set { position= value; }
        }
        public float AngularVelocity
        {
        
            set{
                angularVelocity = value;
            }
            get{
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
            get {

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


        public void GetStates()
        {
            keyState = Keyboard.GetState();
            player1State = GamePad.GetState(PlayerIndex.One);
        }
        public void UpdatePlayer(GameTime theTime, List<SoundEffect> sounds)
        {
           
            Timer = theTime;
            GetStates();
            if (Position.X < (0 - Image.Width))
            {
                SetPositionX(Graphics.PreferredBackBufferWidth + Image.Width);
                // thePosition.X = Graphics.PreferredBackBufferWidth + image.Width;
                //thePosition = new Vector2(X,Y);
            }
            if (Position.X > (Graphics.PreferredBackBufferWidth + Image.Width))
            {
                SetPositionX(0 - Image.Width);

            }
            if (Position.Y < (0 - Image.Height))
            {
                SetPositionY(Graphics.PreferredBackBufferHeight + Image.Height);

            }


            if (Position.Y > Graphics.PreferredBackBufferHeight + Image.Height)
            {
                SetPositionY(0 - Image.Height);

            }

            spriteRec = new Rectangle((int)Position.X, (int)Position.Y, Image.Width, Image.Height);


            SetPosition(Velocity + Position);
            imageCenter = new Vector2(spriteRec.Width / 2, spriteRec.Height / 2);

            // TODO: Add your update logic here
            if (keyState.IsKeyDown(Keys.Right) 
                || player1State.ThumbSticks.Left.X >0)
            {
                // X += 2;
                Angle += AngleIncrement;
                AngularVelocity += AngularVelocityIncrement;

            }
            if (keyState.IsKeyDown(Keys.Left) 
               || player1State.ThumbSticks.Left.X < 0 )
            {
                //X -= 2;
                Angle -= AngleIncrement;
                AngularVelocity -= AngularVelocityIncrement;
            }
            if (keyState.IsKeyDown(Keys.Up) 
                || player1State.Triggers.Left > 0)
            {
                Speed += SpeedIncrement;


              // if(Degree >= 255 && Degree <=285)
                    fire.CreateFire(Position.X, Position.Y+40 , INTENSITY,keyState,player1State);
                
                fireTime -= Timer.ElapsedGameTime.Milliseconds;
                if (fireTime < 0)
                {
                    sounds[(int)Game1.Sounds.Thrust].Play();
                    fireTime = FIREINTERVAL;
                }
                oldx = Position.X;
                oldy = Position.Y;

            }
            if (keyState.IsKeyDown(Keys.T) && oldState.IsKeyUp(Keys.T)||player1State.Buttons.X >0
                )
            {
                SetPosition(new Vector2(Graphics.PreferredBackBufferWidth/2,graphics.PreferredBackBufferHeight/2));
                Velocity = new Vector2(0f, 0f);
            }
                
            else if ( float.IsNaN(Position.X)
                || float.IsNaN(Position.Y)  || float.IsNaN(Velocity.X) || float.IsNaN(Velocity.Y))
            {

                SetPosition(new Vector2(Graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2));
                Velocity = new Vector2(0f, 0f);
            }
            if (keyState.IsKeyDown(Keys.Down)
                || player1State.IsButtonDown(Buttons.DPadDown) && oldPadStatePlayer.IsButtonUp(Buttons.DPadDown))
            {
                Speed -= SpeedIncrement;
                //if (!(Position.X == oldx && Position.Y == oldy))
                //{
                    fire.CreateFire(Position.X, Position.Y , INTENSITY, keyState,player1State);
                //}
                //oldx = Position.X;
                //oldy = Position.Y;

            }
            if (keyState.IsKeyDown(Keys.Space)  || player1State.Triggers.Right > 0)

            {
                 
         time -= Timer.ElapsedGameTime.Milliseconds;

         if (time < 0)
         {

             weapon.FireWeapon();
             time = FIREWEAPONINTERVAL;
             sounds[(int)Game1.Sounds.Weapon].Play();

            
         }
            }

            oldState = keyState;
            
		    Angle+=AngularVelocity;
            AngularVelocity*=AngularVelocityDecrement;
		    if( Angle>fullRotation)
                angle-=fullRotation;
            if (Angle < 0)
                Angle += fullRotation;
            SetVelocityX(Velocity.X + (float)Math.Cos(Angle) * Speed);
            SetVelocityY(Velocity.Y + (float)Math.Sin(Angle) * Speed);
            SetVelocityX(Velocity.X * VelocityDecrement);
            SetVelocityY(Velocity.Y * VelocityDecrement);
            SetPositionX(Position.X + Velocity.X);
            SetPositionY(Position.Y + Velocity.Y);
            Speed *= SpeedDecrement;

            
            //player vs asteroid collision
            foreach (Asteroid i in AccessAsteroids)
            {
                timeSinceLastCollision2 -= Timer.ElapsedGameTime.Milliseconds;
                if (Collided(Position.X,Position.Y, i, i.Image))
                {
                    ChosenColor = Color.Red;
                    timeSinceLastCollision -= Timer.ElapsedGameTime.Milliseconds;

                    if (timeSinceLastCollision < 0)
                    {
                        sounds[(int)Game1.Sounds.AsteroidHit].Play();
                        SetVelocityX((i.Velocity.X / i.Velocity.X) * (i.Velocity.X - Velocity.X) / 2);
                        SetVelocityY((i.Velocity.Y / i.Velocity.Y) * (i.Velocity.Y - Velocity.X) / 2);
                        float currentAngularVelocity = AngularVelocity;
                        AngularVelocity += ((i.AngularVelocity / 3.5f) - currentAngularVelocity);
                        HealthPoints -= HEALTHPOINTDEDUCTION;
                        timeSinceLastCollision = MAXCOLLISIONINTERVAL;
                       
                      
                        
                    }
                    Hit = true;
                    timeSinceLastCollision2 = MAXCOLLISIONINTERVAL;
                    
                }
                if (timeSinceLastCollision2 < 0)
                {
                    Hit = false;
                }
                if (Hit)
                {
                    timeSinceLastFlash -= Timer.ElapsedGameTime.Milliseconds;
                    if (timeSinceLastFlash < 0 && TheColor != ChosenColor)
                    {
                        TheColor = ChosenColor;
                        timeSinceLastFlash = FLASH;
                    }
                    else if (timeSinceLastFlash < 0 && TheColor !=Color.White)
                    {
                        TheColor = Color.White;
                        timeSinceLastFlash = FLASH;
                    }
                   
                    
                }
                else
                {
                    TheColor = Color.White;
                }
             
            
            }
            //Enemy Weapon vs player


            foreach (EnemyWeapon e in AccesEnemyWeapon.AccessWeapons)
            {
                timeSinceLastCollision2 -= Timer.ElapsedGameTime.Milliseconds;
                if (Collided(Position.X, Position.Y, e, Image))
                {
                    ChosenColor = Color.Blue;
                    timeSinceLastCollision -= Timer.ElapsedGameTime.Milliseconds;
                    if (timeSinceLastCollision < 0)
                    {
                        sounds[(int)Game1.Sounds.EnemyWeaponHit].Play();
                        SetVelocityX((e.Velocity.X / e.Velocity.X) * (e.Velocity.X - Velocity.X) / 2);
                        SetVelocityY((e.Velocity.Y / e.Velocity.Y) * (e.Velocity.Y - Velocity.X) / 2);
                        float currentAngularVelocity = AngularVelocity;
                        AngularVelocity += ((e.AngularVelocity / 3.5f) - currentAngularVelocity);
                        HealthPoints -= HEALTHPOINTDEDUCTION;
                        timeSinceLastCollision = MAXCOLLISIONINTERVAL;


                       
                    }
                    Hit = true;
                    timeSinceLastCollision2 = MAXCOLLISIONINTERVAL;
                    


                }

                if (timeSinceLastCollision2 < 0)
                {
                    Hit = false;
                }
                if (Hit)
                {
                    timeSinceLastFlash -= Timer.ElapsedGameTime.Milliseconds;
                    if (timeSinceLastFlash < 0 && TheColor != ChosenColor)
                    {
                        TheColor = ChosenColor;
                        timeSinceLastFlash = FLASH;
                    }
                    else if (timeSinceLastFlash < 0 && TheColor!=Color.White)
                    {
                        TheColor = Color.White;
                        timeSinceLastFlash = FLASH;
                    }
                }
                else
                {
                    TheColor = Color.White;
                }

            }

            // Enemy Vs Player Collision

            foreach (Enemy e in AccessEnemy.AccessEnemyList)
            {
                timeSinceLastCollision2 -= Timer.ElapsedGameTime.Milliseconds;
                if (Collided(Position.X, Position.Y, e, Image))
                {
                    ChosenColor = Color.Purple;
                    timeSinceLastCollision -= Timer.ElapsedGameTime.Milliseconds;
                    if (timeSinceLastCollision < 0)
                    {
                        sounds[(int)Game1.Sounds.EnemyHit].Play();
                        SetVelocityX((e.Velocity.X / e.Velocity.X) * (e.Velocity.X - Velocity.X) / 2);
                        SetVelocityY((e.Velocity.Y / e.Velocity.Y) * (e.Velocity.Y - Velocity.X) / 2);
                        CurrentAngularVelocity = AngularVelocity;
                        AngularVelocity += ((e.AngularVelocity / 2f) - currentAngularVelocity);
                        HealthPoints -= HEALTHPOINTDEDUCTION;
                        timeSinceLastCollision = MAXCOLLISIONINTERVAL;
                       
                     
                    }

                    Hit = true;
                    timeSinceLastCollision2 = MAXCOLLISIONINTERVAL;
                }

                if (timeSinceLastCollision2 < 0)
                {
                    Hit = false;
                }
                if (Hit)
                {
                    timeSinceLastFlash -= Timer.ElapsedGameTime.Milliseconds;
                    if (timeSinceLastFlash < 0 && TheColor !=ChosenColor)
                    {
                        TheColor = ChosenColor;
                        timeSinceLastFlash = FLASH;
                    }
                    else if (timeSinceLastFlash < 0 && TheColor !=Color.White)
                    {
                        TheColor = Color.White;
                        timeSinceLastFlash = FLASH;
                    }
                }
                else
                {
                    TheColor = Color.White;
                }


            }
        
        
        }
    
        public Enemy AccessEnemy{


            get
            {
                return enemy;

            }
            set { enemy = value; }
        
        
        }

        public List<Asteroid> AccessAsteroids
        {
            get {
                return theAsteroids;
            
            }
            set {theAsteroids = value; }
        
        
        
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
        public bool Collided(float x, float y, EnemyWeapon weapon, Texture2D RadiusImage1)
        {
            if (distance(x, y, weapon.Position.X, weapon.Position.Y) < FindRadius(RadiusImage1))
                return true;
            else
                return false;
        }
        public bool Collided(float x, float y, Enemy e, Texture2D RadiusImage1)
        {
            if (distance(x, y, e.Position.X, e.Position.Y) < FindRadius(RadiusImage1))
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
            set {
                image = value;
            }

            get {
                return image;
            }
        
        }

        public Vector2 Velocity
        {
            get {
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
