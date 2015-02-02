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
    class Fire
    {
         Texture2D image;
        Rectangle spriteRec;
        Vector2 imageCenter;
        Player player;
        SpriteBatch spriteBatch;
        KeyboardState oldState;
        GamePadState oldPadStatePlayer;
        private float x, y,angle;
        private Vector2 position,velocity;
        private float angularVelocity;
        private int frame;
        private  int fireLoop = 0,fireNumber;
        private float angleIncrement = 0.1f;
        private const float fullRotation = ((float)(2*Math.PI));
        private const int FIRERANGE = 30;
        private float  speed;
        private bool used;
        private Weapon weapon;
        bool draw;
        Random random;
        const int particleNumber = 8000;
        Texture2D fireImage;
        private List<Fire> theFire = new List<Fire>();
        public Fire()
        {
        
        
        }
        public Fire(Texture2D fireImage)
        {
            draw = true;
           
           // this.spriteBatch = spriteBatch;
            this.fireImage = fireImage;
            
            angle = 0;
         
           used = false;
            position = new Vector2(0, 0);
            velocity = new Vector2(0f, 0f);
            imageCenter = new Vector2(fireImage.Width / 2, fireImage.Height / 2);
            angularVelocity = 0f;
            random = new Random();
            for (int i = 0; i < particleNumber; i++)
            {
                theFire.Add(new Fire(fireImage, 0, 0,0f,0f,angle));
                theFire[i].DrawPlayertoScreen = true;



            }
        }
        public Fire(Texture2D fireImage, float x, float y, float xv, float yv, float angle)
        {


            // this.spriteBatch = spriteBatch;
            this.fireImage = fireImage;
            this.x = x;
            this.y = y;
            this.angle = angle;
           // this.spriteBatch = spriteBatch;

            position = new Vector2(x, y);
            velocity = new Vector2(xv, yv);
            imageCenter = new Vector2(fireImage.Width / 2, fireImage.Height / 2);
            angularVelocity = 0f;
            random = new Random();
        }
        public int FireLoop
        {
            get { return fireLoop; }
            set { fireLoop = value; }
        }
        public int FireNumber
        {
            get { return fireNumber; }
            set {  fireNumber= value; }
        }
        public bool DrawPlayertoScreen
        {
            get { return draw; }
            set { draw = value; }
        }
        public bool Used
        {
            get { return used; }
            set { used = value; }
        }
        public List<Fire> TheFire
        {
            get
            { return theFire; }

            set
            {
                theFire = value;
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
     
        public void DrawPlayer()
        {
            spriteBatch.Draw(image, Position, null, Color.White, Angle, imageCenter, 1f, SpriteEffects.None, 0);
            
        
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
                fireImage = value;
            }

            get {
                return fireImage;
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
        public Player AccessPlayer
        {
            set {
                player = value;
            
            }
            get {
                return player;
            
            }
        }

        public void CreateFire(float x, float y, int intensity, KeyboardState key, GamePadState pad)
        {

            random = new Random();

            float number = (random.Next(intensity) + intensity) + (float)random.NextDouble();
            AngleIncrement = (float)(fullRotation / number);
            Angle = (float)(random.Next((int)AngleIncrement)) + (float)random.NextDouble();
            FireNumber = (int)number;

            if (key.IsKeyDown(Keys.Up) || key.IsKeyDown(Keys.Down)
                || pad.Triggers.Left > 0 || pad.IsButtonDown(Buttons.DPadUp) || pad.IsButtonDown(Buttons.DPadDown))
            {
                for (int f = 0; f < number; f++)
                {

                   
                    if (theFire[f].Used == false)
                    {
                         theFire[f].DrawPlayertoScreen = true;
                        theFire[f].SetPositionX(x);
                        theFire[f].SetPositionY(y);
                        theFire[f].Used = true;

                        theFire[f].SetVelocityX((float)(Math.Cos(theFire[f].Angle) * random.Next(-FIRERANGE, FIRERANGE) + random.NextDouble()));

                        theFire[f].SetVelocityY((float)(Math.Sin(theFire[f].Angle) * random.Next(-FIRERANGE, FIRERANGE) + random.NextDouble()));


                        theFire[f].Angle += angleIncrement;
                    }

                }
            }
        }
               
        
      

        public void UpdateFire()
        { 
            for(int i = 0; i < theFire.Count;i++){
                if (float.IsNaN(theFire[i].Position.X)
                || float.IsNaN(theFire[i].Position.Y) || float.IsNaN(theFire[i].Velocity.X) || float.IsNaN(theFire[i].Velocity.Y))
                {

                    theFire[i].SetPosition(new Vector2(AccessPlayer.Graphics.PreferredBackBufferWidth / 2, AccessPlayer.Graphics.PreferredBackBufferHeight / 2));
                    theFire[i].Velocity = new Vector2(0f, 0f);
                }
                if (theFire[i].DrawPlayertoScreen == true)
                {

                    theFire[i].Position=(theFire[i].Position + theFire[i].Velocity);
                    theFire[i].Velocity=(theFire[i].Velocity + (-4*AccessPlayer.Velocity));
                   
                    

                }

                if (theFire[i].Position.X < 0 - theFire[i].Image.Width || theFire[i].Position.Y < 0 - theFire[i].Image.Height||
                    theFire[i].Position.X > AccessPlayer.Graphics.PreferredBackBufferWidth + theFire[i].Image.Width 
                    ||
                    theFire[i].Position.Y > AccessPlayer.Graphics.PreferredBackBufferHeight + theFire[i].Image.Height)
                {
                    theFire[i].DrawPlayertoScreen = false;
                    theFire[i].Position = new Vector2(AccessPlayer.Graphics.PreferredBackBufferWidth / 2, AccessPlayer.Graphics.PreferredBackBufferHeight / 2);
                    theFire[i].Used = false;
                }
			    
            }

	
		   // Color f\r,f\g,f\b
		    //Rect f\x-1,f\y-1,3,3
            }

        public void DrawFire( SpriteBatch spriteBatch)
        {
            Color theColor = Color.White;
            foreach (Fire i in theFire)
            {
                int colorPick = random.Next(0, 3);

                switch (colorPick)
                {
                    case 0:
                        theColor = Color.OrangeRed;
                        break;
                        
                    case 1:
                        theColor = Color.Red;
                        break;
                    case 2:
                        theColor = Color.Orange;
                        break;
                }
                Rectangle source = new Rectangle(16, 16, 16, 16);
                // Rectangle dest = new Rectangle(random.Next(343,random.Next(343), 3, 3);
                // spriteBatch.Draw(fireImage, dest, source, Color.White);
                //if(theFire.Count >0)
                //spriteBatch.Draw(fireImage,Position,source, Color.White, Angle, imageCenter,0, SpriteEffects.None, 0);e
                if (i.DrawPlayertoScreen == true)
                     spriteBatch.Draw(fireImage, i.Position, theColor);
            }
        }
        }
    }

        
