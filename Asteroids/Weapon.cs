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
    class Weapon 
    {
        Texture2D image;
        Vector2 imageCenter;
        const int MAXBULLETS = 28;
        Rectangle spriteRec;
        private float x, y,angle,posAngle;
        private int time;
        private Vector2 position,velocity;
        private float angularVelocity;
        private int frame;
        private float angleIncrement = 0.1f;
        private const float fullRotation = ((float)(2*Math.PI));
        private float  speed;
        private const int WEAPONINTERVAL = 1000;
        public enum WeaponType {
            BULLET,ORB, LASER,SHOTGUN,SPRED
        };
        private List<Weapon> theWeapons;
        Texture2D[] weapons;
        private int theWeapon;
        Player player;
        SpriteBatch spriteBatch;
        private float bulletSpeed;
        GameTime timer;
       
       
        public Weapon(Texture2D[] weapons, float x, float y, float vx, float vy, int theWeapon, float angle)
        {
            
            this.theWeapon = theWeapon;
            this.weapons = weapons;
            this.angle = angle;
            bulletSpeed = 50;
            position = new Vector2(x, y);
            velocity = new Vector2(vx, vy);
            //angle = (float)((3*Math.PI)/2);
            angularVelocity = 0f;

            time = WEAPONINTERVAL;

        }

        public void InitializeList()

        {
            theWeapons = new List<Weapon>();
        }
        public Vector2 ImageCenter
        {
            set { imageCenter = value; }
            get { return imageCenter; }
        
        }

        public Rectangle SpriteRectangle
        {
            set { spriteRec= value; }
            get { return spriteRec; }

        }
        public int Time
        {
            get {
                return time;
            }
            set { time = value; }
        
        }

        public GameTime Timer
        {

            set {timer=value;}
            get { return timer;}
        }
        
        public List<Weapon> TheWeapons {

            get
            {

                return theWeapons;
            }
            set { theWeapons = value; }
        }
        public float BulletSpeed
        {
            get {

                return bulletSpeed;
            }
            set { bulletSpeed = value; }
        
        }
        public Player AccessPlayer
        {
            set {
                player = value;
            
            }
            get {
                return player;
            
            }
        }

        public int TheWeapon
        {
            set {
                theWeapon = value;
            }
            get {

                return theWeapon;
            }
        }

        

        public Vector2 Position
        {
            get
            { 
                return position;
            }

           // set { position= value; }
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

        public void FireWeapon()
        {
            if (TheWeapon == (int)WeaponType.BULLET)
            {
                if (theWeapons.Count < MAXBULLETS) 
                theWeapons.Add(new Weapon(Image,player.Position.X,player.Position.Y,player.Velocity.X + (float)(BulletSpeed* Math.Cos(player.Angle))
                , player.Velocity.Y + (float)(BulletSpeed * Math.Sin(player.Angle)), (int)WeaponType.BULLET,(player.Angle -(float)(3*Math.PI/2))));
	            
	            
	            
	            
            }
        
        }

        public void UpdateWeapons(GameTime theTime)
        {
            Timer = theTime;
            for(int i = 0; i < theWeapons.Count; i++)
            {
                if (theWeapons[i].TheWeapon == (int)WeaponType.BULLET)
                {
                    theWeapons[i].SpriteRectangle = new Rectangle((int)theWeapons[i].Position.X, (int)theWeapons[i].Position.Y, theWeapons[i].Image[0].Width, theWeapons[i].Image[0].Height);
                    
                  
                    theWeapons[i].SetPosition(theWeapons[i].Velocity + theWeapons[i].Position);
                    theWeapons[i].ImageCenter = new Vector2(theWeapons[i].SpriteRectangle.Width / 2, theWeapons[i].SpriteRectangle.Height / 2);
                }
                if (theWeapons[i].Position.X < (0 - theWeapons[i].Image[0].Width))
                {
                    theWeapons[i].SetPositionX(player.Graphics.PreferredBackBufferWidth + theWeapons[i].Image[0].Width);
                    // thePosition.X = Window.ClientBounds.Width + weapon1.Width;
                    //thePosition = new Vector2(X,Y);
                }
                else if (theWeapons[i].Position.X > (player.Graphics.PreferredBackBufferWidth + theWeapons[i].Image[0].Width))
                {
                    theWeapons[i].SetPositionX(0 - theWeapons[i].Image[0].Width);

                }
                else if (theWeapons[i].Position.Y < (0 - theWeapons[i].Image[0].Height))
                {
                    theWeapons[i].SetPositionY(player.Graphics.PreferredBackBufferHeight + theWeapons[i].Image[0].Height);

                }


                else if (theWeapons[i].Position.Y > player.Graphics.PreferredBackBufferHeight + theWeapons[i].Image[0].Height)
                {
                    theWeapons[i].SetPositionY(0 - theWeapons[i].Image[0].Height);

                }

                

               theWeapons[i].Time -= Timer.ElapsedGameTime.Milliseconds;

               if (theWeapons[i].Time < 0)
               {
                   theWeapons[i].Time = WEAPONINTERVAL;
                   theWeapons.RemoveAt(i);


                   if (i >=1) i--;
                   
                   break;
               }

                
            }

           
        
        }

      
        public void DrawWeapons(SpriteBatch spriteBatch)
        {
            foreach (Weapon i in theWeapons)
            {
                if (i.TheWeapon == (int)WeaponType.BULLET)
                {
                    spriteBatch.Draw(i.Image[0], i.Position, null, Color.White, i.Angle, i.ImageCenter, 1f, SpriteEffects.None, 0);
                }
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
        public Texture2D [] Image
        {
            set {
                weapons = value;
            }

            get {
                return weapons;
            }
        
        }

        public Vector2 Velocity
        {
            get {
                return velocity;
            
            }
           
        
        
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
