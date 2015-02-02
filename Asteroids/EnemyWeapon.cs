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
    class  EnemyWeapon
    {
        AudioEngine audioEngine;
        WaveBank waveBank;
        SoundBank soundBank;
        Cue trackCue;
        Texture2D image;
        Vector2 imageCenter;
        const int MAXBULLETS = 10;
        Rectangle spriteRec;
        private int hitPoints;
        private const int MAXHITS = 3;
        private float x, y, angle, posAngle;
        private int time;
        private Vector2 position, velocity;
        private float angularVelocity;
        private int frame;
        private int playerTime = 0;
        private const int MAXPLAYINTERVAL = 1500;
        private float angleIncrement = 0.1f;
        private const float fullRotation = ((float)(2 * Math.PI));
        private float speed;
        private const int WEAPONINTERVAL = 2500;
        private enum WeaponType { BULLET, LASER, SHOTGUN, SPRED };
        private Random random;
        private List<EnemyWeapon> theWeapons;
        Texture2D[] weapons;
        private int theWeapon;
        Player player;
        Color theColor;
        Enemy enemy;
        private Weapon weaponClass;
        SpriteBatch spriteBatch;
        private float bulletSpeed;
        GameTime timer;
       

        public EnemyWeapon (Texture2D weapon, float x, float y, float vx, float vy, 
            int theWeapon, float angle, Enemy enemy,Weapon weaponClass, Player player)
        {
            this.player = player;
           
            this.theWeapon = theWeapon;
            image = weapon;
            this.angle = angle;
            bulletSpeed = 5;
            theColor = Color.Blue;
            this.weaponClass = weaponClass;
            position = new Vector2(x, y);
            this.enemy = enemy;
            velocity = new Vector2(vx, vy);
            //angle = (float)((3*Math.PI)/2);
            angularVelocity = 0f;
            hitPoints = 0;

            time = WEAPONINTERVAL;

        }
        public EnemyWeapon(Texture2D weapon, float x, float y, float vx, float vy, int theWeapon, float angle)
        {

            this.theWeapon = theWeapon;
            image = weapon;
            this.angle = angle;
            bulletSpeed = 5;
            theColor = Color.Blue;
            position = new Vector2(x, y);
            velocity = new Vector2(vx, vy);
            //angle = (float)((3*Math.PI)/2);
            angularVelocity = 0f;
            playerTime = 0;
             hitPoints = 0;
            time = WEAPONINTERVAL;

        }

        public void InitializeList()
        {
            theWeapons = new List<EnemyWeapon>();
        }
        public Vector2 ImageCenter
        {
            set { imageCenter = value; }
            get { return imageCenter; }

        }

        public Player AccessPlayer
        {
            set { player = value; }
            get { return player; }

        }
         public int HitPoints
        {
            set { hitPoints = value; }
            get { return hitPoints; }

        }

        public Color TheColor
        {
            set { theColor = value; }
            get { return theColor; }

        }

        public Weapon AcessWeapon
        {

            set { weaponClass = value; }
            get { return weaponClass; }
        }
        public Rectangle SpriteRectangle
        {
            set { spriteRec = value; }
            get { return spriteRec; }

        }
        public int Time
        {
            get
            {
                return time;
            }
            set { time = value; }

        }
        public int PlayerTime
        {
            get
            {
                return playerTime;
            }
            set { playerTime = value; }

        }

        public GameTime Timer
        {

            set { timer = value; }
            get { return timer; }
        }

        public List<EnemyWeapon> TheWeapons
        {

            get
            {

                return theWeapons;
            }
            set { theWeapons = value; }
        }
        public float BulletSpeed
        {
            get
            {

                return bulletSpeed;
            }
            set { bulletSpeed = value; }

        }
        public List<EnemyWeapon> AccessWeapons
        {
            get
            { return theWeapons; }

            set
            {
                theWeapons = value;
            }

        }
        public Enemy AccessEnemy
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

        public int TheWeapon
        {
            set
            {
                theWeapon = value;
            }
            get
            {

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

            set
            {
                angularVelocity = value;
            }
            get
            {
                return angularVelocity;

            }
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

        public void FireWeapon(Enemy enemy)
        {
            if (TheWeapon == (int)WeaponType.BULLET)
            {
                if (theWeapons.Count < MAXBULLETS)
                    theWeapons.Add(new EnemyWeapon(Image, enemy.Position.X, enemy.Position.Y, enemy.Velocity.X + (float)(BulletSpeed * Math.Cos(enemy.Angle))
                    ,enemy.Velocity.Y + (float)(BulletSpeed * Math.Sin(enemy.Angle)), (int)WeaponType.BULLET, (enemy.Angle)));




            }

        }

        public void UpdateWeapons(GameTime theTime, List<SoundEffect> sounds)
        {
            random = new Random();
            Timer = theTime;
            for (int i = 0; i < theWeapons.Count; i++)
            {
                if (theWeapons[i].TheWeapon == (int)WeaponType.BULLET)
                {
                    theWeapons[i].SpriteRectangle = new Rectangle((int)theWeapons[i].Position.X, (int)theWeapons[i].Position.Y, theWeapons[i].Image.Width, theWeapons[i].Image.Height);


                    theWeapons[i].SetPosition(theWeapons[i].Velocity + theWeapons[i].Position);
                    theWeapons[i].ImageCenter = new Vector2(theWeapons[i].SpriteRectangle.Width / 2, theWeapons[i].SpriteRectangle.Height / 2);
                }
                if (theWeapons[i].Position.X < (0 - theWeapons[i].Image.Width))
                {
                    theWeapons[i].SetPositionX(enemy.Graphics.PreferredBackBufferWidth + theWeapons[i].Image.Width);
                    // thePosition.X = Window.ClientBounds.Width + weapon1.Width;
                    //thePosition = new Vector2(X,Y);
                }
                else if (theWeapons[i].Position.X > (enemy.Graphics.PreferredBackBufferWidth + theWeapons[i].Image.Width))
                {
                    theWeapons[i].SetPositionX(0 - theWeapons[i].Image.Width);

                }
                else if (theWeapons[i].Position.Y < (0 - theWeapons[i].Image.Height))
                {
                    theWeapons[i].SetPositionY(enemy.Graphics.PreferredBackBufferHeight + theWeapons[i].Image.Height);

                }


                else if (theWeapons[i].Position.Y > enemy.Graphics.PreferredBackBufferHeight + theWeapons[i].Image.Height)
                {
                    theWeapons[i].SetPositionY(0 - theWeapons[i].Image.Height);

                }



                theWeapons[i].Time -= Timer.ElapsedGameTime.Milliseconds;

                if (theWeapons[i].Time < 0)
                {
                   
                    theWeapons.RemoveAt(i);


                    if (i >= 1) i--;
                    theWeapons[i].Time = WEAPONINTERVAL;


                }
                //player weapon vs enemy weapon

                for (int j = 0; j < AcessWeapon.TheWeapons.Count; j++)
                {
                    
                    if (Collided(AcessWeapon.TheWeapons[j], theWeapons[i], AcessWeapon.TheWeapons[j].Image[0]))
                    {
                        theWeapons[i].PlayerTime -= Timer.ElapsedGameTime.Milliseconds;
                        if (theWeapons[i].PlayerTime < 0)
                        {
                            sounds[(int)Game1.Sounds.PlayerHitEnemyWeapon].Play();
                            theWeapons[i].PlayerTime = MAXPLAYINTERVAL;
                        }
                       
                        if(i > 0){
                            if(theWeapons[i].HitPoints >= MAXHITS)
                            {
                                theWeapons.RemoveAt(i);
                                i--;
                                AccessPlayer.Score+=700;
                            }else{
                                AccessPlayer.Score+=10;
                                if (theWeapons[i].HitPoints > 0 && theWeapons[i].HitPoints < 2)
                                    theWeapons[i].TheColor = Color.Red;
                                else if (theWeapons[i].HitPoints >= 2)
                                    theWeapons[i].TheColor = Color.DarkRed;

                                theWeapons[i].HitPoints++;
                            
                            
                            }
                            theWeapons[i].SetVelocityX((AcessWeapon.TheWeapons[j].Velocity.X / AcessWeapon.TheWeapons[j].Velocity.X) * (AcessWeapon.TheWeapons[j].Velocity.X - theWeapons[i].Velocity.X) / 2);
                            theWeapons[i].SetVelocityY((AcessWeapon.TheWeapons[j].Velocity.Y / AcessWeapon.TheWeapons[j].Velocity.Y) * (AcessWeapon.TheWeapons[j].Velocity.Y - theWeapons[i].Velocity.Y) / 2);
                            
                    }
                    }

                }

            }
        }



        

        public float distance(float x1, float y1, float x2, float y2)
        {


            float dx = x2 - x1;
            float dy = y2 - y1;


            return (float)Math.Sqrt((dx * dx) + (dy * dy));
        }
        public bool Collided(Weapon w, EnemyWeapon ew, Texture2D RadiusImage1)
        {
            if (distance(w.Position.X, w.Position.Y, ew.Position.X, ew.Position.Y) < FindRadius(RadiusImage1))
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

        public void DrawWeapons(SpriteBatch spriteBatch)
        {
            foreach (EnemyWeapon i in theWeapons)
            {
                
                    spriteBatch.Draw(i.Image, i.Position, null, i.TheColor, i.Angle, i.ImageCenter, 1f, SpriteEffects.None, 0);
                
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
       

        public void SetVelocityX(float x)
        {
            velocity.X = x;

        }

    }
}
