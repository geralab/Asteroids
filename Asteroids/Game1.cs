
/* Gerald Blake
 * Asteroids Remake
 * 
 * 
 * 
 * 
 * 
 * 
 *
 * */







using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Asteroids
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        
        
        SoundEffect weaponSound;
        SoundEffect fireSound;
        SoundEffect playerHitEW;
        SoundEffect boom,asteroidCollision;
        SoundEffect asteroidHit,enemyHit,enemyWeponHit;
        List<SoundEffect> sounds = new List<SoundEffect>();
        public enum Sounds
        { 
            Weapon,Boom,Thrust,AsteroidHit,EnemyHit,EnemyWeaponHit,PlayerHitEnemyWeapon,AsteroidCollsion
        
        };
        Song song1;
        List<Scrolling> scroll = new List<Scrolling>();
        GraphicsDeviceManager graphics;
        Texture2D back1, back2, back3, back4, back5, back6, back7, back8, back9, back10, back11, back12, back13, back14, back15, back16
            , back17, back18, back19, back20, back21;
        SpriteBatch spriteBatch;
        Texture2D enemyImage;
        EnemyWeapon enemyWeapon;
        SpriteFont theFont;
        Texture2D enemyWeaponImage;
        Texture2D ship;
        Player player1;
        Texture2D [] weapons = new Texture2D[5];
        List<Texture2D> asteroidImage = new List<Texture2D>();
        Enemy enemy;
        SpriteFont nextLevelFont;
        Texture2D nextLevel;
        KeyboardState oldState;
        GamePadState oldPadStatePlayer;
        Weapon weapon;
        private static int level = 0;
        Texture2D fireImage;
        Rectangle levelRec;
        Fire fire;
        Scrolling scroller = new Scrolling();
        static bool drawI = false;
        Asteroid asteroid;
        Rectangle source1, dest1;
        Stat theStat;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";
            Window.Title = "XNA Asteroids - Gerald Blake";
            //set to proper widths
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            oldState = Keyboard.GetState();
            oldPadStatePlayer = GamePad.GetState(PlayerIndex.One);
           
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            theFont = Content.Load<SpriteFont>(@"Arial");
            nextLevelFont = Content.Load<SpriteFont>(@"NextLevel");
            ship = Content.Load<Texture2D>("Images/Spaceship4");
            weapons[0] = Content.Load<Texture2D>("Images/Weapon1");
       
            fireImage = Content.Load<Texture2D>("Images/Fire2");
            asteroidImage.Add( Content.Load<Texture2D>("Images/SmallChunk1")); //0
            asteroidImage.Add(Content.Load<Texture2D>("Images/Small"));//1
            asteroidImage.Add(Content.Load<Texture2D>("Images/MediumChunk1"));//2
            asteroidImage.Add(Content.Load<Texture2D>("Images/MediumChunk2"));//3

            asteroidImage.Add(Content.Load<Texture2D>("Images/MediumChunk3"));//4
            asteroidImage.Add(Content.Load<Texture2D>("Images/MediumChunk4"));//5
            asteroidImage.Add(Content.Load<Texture2D>("Images/MediumChunk5"));//6

            asteroidImage.Add(Content.Load<Texture2D>("Images/Medium"));//4
            asteroidImage.Add(Content.Load<Texture2D>("Images/LargeChunk1"));//5
            asteroidImage.Add(Content.Load<Texture2D>("Images/LargeChunk2"));//6
            asteroidImage.Add(Content.Load<Texture2D>("Images/LargeChunk3"));//7
            asteroidImage.Add(Content.Load<Texture2D>("Images/Large"));//8
            asteroidImage.Add(Content.Load<Texture2D>("Images/ExtraLargeChunk1"));//9
            asteroidImage.Add(Content.Load<Texture2D>("Images/ExtraLargeChunk2"));//10
            asteroidImage.Add(Content.Load<Texture2D>("Images/ExtraLargeChunk3"));//11
            asteroidImage.Add(Content.Load<Texture2D>("Images/ExtraLargeChunk4"));//12
            asteroidImage.Add(Content.Load<Texture2D>("Images/ExtraLarge"));//13
            enemyWeaponImage = Content.Load<Texture2D>("Images/Enemy Weapon");
            weaponSound = Content.Load<SoundEffect>("Sound/WeaponSound1");
            boom = Content.Load<SoundEffect>("Sound/Boom");
            fireSound = Content.Load<SoundEffect>("Sound/FireSound");
            asteroidHit = Content.Load<SoundEffect>("Sound/CLUNKIT2");
            enemyHit = Content.Load<SoundEffect>("Sound/FlaskGone");
            enemyWeponHit = Content.Load<SoundEffect>("Sound/ELECTRIC2");
            playerHitEW = Content.Load<SoundEffect>("Sound/EWHS");
            asteroidCollision = Content.Load<SoundEffect>("Sound/AsteroidCollision");
            sounds.Add(weaponSound);//0
            sounds.Add(boom);//1
            sounds.Add(fireSound);//2
            sounds.Add(asteroidHit);//3
            sounds.Add(enemyHit);//4
            sounds.Add(enemyWeponHit);//5
            sounds.Add(playerHitEW);
            sounds.Add(asteroidCollision);

            nextLevel = Content.Load<Texture2D>("Images/LevelComplete");
            levelRec = new Rectangle(0, 0, nextLevel.Width, nextLevel.Height);
            back1  = Content.Load<Texture2D>("Images/back1");
            back2 = Content.Load<Texture2D>("Images/back2");
            back3 = Content.Load<Texture2D>("Images/back3");
            back4 = Content.Load<Texture2D>("Images/back4");
            back5 = Content.Load<Texture2D>("Images/back5");
            back6 = Content.Load<Texture2D>("Images/back6");
            back7 = Content.Load<Texture2D>("Images/back7");
            back8 = Content.Load<Texture2D>("Images/back8");
            back9 = Content.Load<Texture2D>("Images/back9");
            back10 = Content.Load<Texture2D>("Images/back10");
            back11= Content.Load<Texture2D>("Images/back11");
            back12 = Content.Load<Texture2D>("Images/back12");
            back13 = Content.Load<Texture2D>("Images/back13");
            back14 = Content.Load<Texture2D>("Images/back14");
            back15 = Content.Load<Texture2D>("Images/back15");
            back16 = Content.Load<Texture2D>("Images/back16");
            back17 = Content.Load<Texture2D>("Images/back17");
            back18 = Content.Load<Texture2D>("Images/back18");
            back19 = Content.Load<Texture2D>("Images/back19");
            back20 = Content.Load<Texture2D>("Images/back20");
            back21 = Content.Load<Texture2D>("Images/back21");
            source1 = new Rectangle(0, 0, 2560, 1600);
            scroll.Add(new Scrolling(back1, new Rectangle(0, 0, back1.Width, back1.Height)));
            scroll.Add(new Scrolling(back2, new Rectangle(0, -back2.Height, back2.Width, back2.Height)));
            scroll.Add(new Scrolling(back3, new Rectangle(0, -2*back3.Height, back3.Width, back3.Height)));
            scroll.Add(new Scrolling(back4, new Rectangle(0, -3 * back4.Height, back4.Width, back4.Height)));
            scroll.Add(new Scrolling(back5, new Rectangle(0, -4 * back5.Height, back5.Width, back5.Height)));
            scroll.Add(new Scrolling(back6, new Rectangle(0, -5 * back6.Height, back6.Width, back6.Height)));
            scroll.Add(new Scrolling(back7, new Rectangle(0, -6 * back7.Height, back7.Width, back7.Height)));
            scroll.Add(new Scrolling(back8, new Rectangle(0, -7 * back8.Height, back8.Width, back8.Height)));
            scroll.Add(new Scrolling(back9, new Rectangle(0, -8 * back9.Height, back9.Width, back9.Height)));
            scroll.Add(new Scrolling(back10, new Rectangle(0, -9 * back10.Height, back10.Width, back10.Height)));
            scroll.Add(new Scrolling(back11, new Rectangle(0, -10 * back11.Height, back11.Width, back11.Height)));
            scroll.Add(new Scrolling(back12, new Rectangle(0, -11 * back12.Height, back12.Width, back12.Height)));
            scroll.Add(new Scrolling(back13, new Rectangle(0, -12 * back13.Height, back13.Width, back13.Height)));
            scroll.Add(new Scrolling(back14, new Rectangle(0, -13 * back14.Height, back13.Width, back13.Height)));
            scroll.Add(new Scrolling(back15, new Rectangle(0, -14 * back15.Height, back13.Width, back13.Height)));
            scroll.Add(new Scrolling(back16, new Rectangle(0, -15 * back16.Height, back13.Width, back13.Height)));
            scroll.Add(new Scrolling(back17, new Rectangle(0, -16 * back17.Height, back13.Width, back13.Height)));
            scroll.Add(new Scrolling(back18, new Rectangle(0, -17 * back18.Height, back13.Width, back13.Height)));
            scroll.Add(new Scrolling(back19, new Rectangle(0, -18 * back19.Height, back13.Width, back13.Height)));
            scroll.Add(new Scrolling(back20, new Rectangle(0, -19 * back20.Height, back13.Width, back13.Height)));
            scroll.Add(new Scrolling(back21, new Rectangle(0, -20 * back21.Height, back13.Width, back13.Height)));

            dest1 = new Rectangle(graphics.PreferredBackBufferWidth/2, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            song1 = Content.Load<Song>("Sound/SB");
            fire = new Fire(fireImage);
            weapon = new Weapon(weapons, 0f, 0f, 0f, 0f, 0,0f);
            weapon.InitializeList();
            

           enemyImage =  Content.Load<Texture2D>("Images/Enemy1");
           enemy = new Enemy(enemyImage, graphics.PreferredBackBufferWidth / 2, enemyImage.Height + 3, enemyWeapon,Color.White);
           enemyWeapon = new EnemyWeapon(enemyWeaponImage, 0f, 0f, 0f, 0f, 0, 0f, enemy,weapon,player1);
           enemy.InitializeEnemies();
            
            enemyWeapon.InitializeList();
            asteroid = new Asteroid(asteroidImage, weapon, theFont);
            asteroid.InitializeList();
            asteroid.HostAsteroid = asteroid;
            asteroid.Graphics = graphics;
            asteroid.CreateAsteroids(asteroid,5, (int)Asteroid.AsteroidType.Small, -5, 5);
            asteroid.CreateAsteroids(asteroid,2, (int)Asteroid.AsteroidType.Medium, -4, 4);
            asteroid.CreateAsteroids(asteroid,2, (int)Asteroid.AsteroidType.Large, -3, 3);
            asteroid.CreateAsteroids(asteroid,1, (int)Asteroid.AsteroidType.ExtraLarge, -2, 2);
            
            player1 = new Player(ship,
                      graphics.PreferredBackBufferWidth/ 2,
                      graphics.PreferredBackBufferHeight / 2, 0, weapon, 
                      fire, oldState, oldPadStatePlayer, theFont,asteroid);
            //Class Communication
            weapon.AccessPlayer = player1;
            enemyWeapon.AccessEnemy = enemy;
            fire.AccessPlayer = player1;
            player1.AccessPlayer = player1;
            player1.Graphics = graphics;
            enemy.Graphics = graphics;
            asteroid.AccessPlayer = player1;
            asteroid.AccessWeapons = weapon;
            player1.AccesEnemyWeapon = enemyWeapon;
            player1.AccessAsteroids = asteroid.AccessAsteroid;
            enemy.AccessWeapons = enemyWeapon;
            enemy.CreateEnemies(enemy,graphics.PreferredBackBufferWidth/2, enemyImage.Height + 3,1,1,-2,2);
            enemyWeapon.AccessEnemy = enemy;
            enemyWeapon.AccessPlayer = player1;
            enemyWeapon.AcessWeapon = weapon;
            player1.AccessEnemy = enemy;
            theStat = new Stat();
            MediaPlayer.Play(song1);
          
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            switch (level)
            {
                case 0:
                    theStat.Count = asteroid.GetAsteroidCount();

                    if (asteroid.GetAsteroidCount() > 1)
                    {
                        scroller.Scroll(scroll, graphics);
                        enemy.AccessWeapons = enemyWeapon;
                        asteroid.AccessWeapons = weapon;
                        asteroid.Timer = gameTime;
                        weapon.UpdateWeapons(gameTime);
                        asteroid.AccessWeapons = weapon;
                        enemyWeapon.UpdateWeapons(gameTime, sounds);
                        asteroid.UpdateAsteroids(asteroid, gameTime, sounds);
                        player1.AccessAsteroids = asteroid.AccessAsteroid;
                        player1.AccesEnemyWeapon = enemyWeapon;
                        player1.UpdatePlayer(gameTime, sounds);
                        enemyWeapon.AccessPlayer = player1;
                        enemyWeapon.AcessWeapon = weapon;

                        fire = player1.AccessFire;
                        fire.UpdateFire();
                        weapon.Timer = gameTime;
                        asteroid.AccessWeapons = weapon;
                        enemy.AccessWeapons = enemyWeapon;
                        enemy.UpdateEnemy(gameTime);
                        enemyWeapon.AccessEnemy = enemy;
                        player1.AccessEnemy = enemy;
                    }
                    else
                    {
                        player1.GetStates();

                    }
                    if (asteroid.GetAsteroidCount() < 2 && (GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed)
                            || (player1.KeyState.IsKeyDown(Keys.X) && player1.OldKeyState.IsKeyUp(Keys.X)))
                    {

                        asteroid.CreateAsteroids(asteroid, 1, (int)Asteroid.AsteroidType.Small, -5, 5);
                        asteroid.CreateAsteroids(asteroid, 3, (int)Asteroid.AsteroidType.Medium, -4, 4);
                        asteroid.CreateAsteroids(asteroid, 3, (int)Asteroid.AsteroidType.Large, -3, 3);
                        asteroid.CreateAsteroids(asteroid, 2, (int)Asteroid.AsteroidType.ExtraLarge, -2, 2);
                        enemy.CreateEnemies(enemy, graphics.PreferredBackBufferWidth / 2, enemyImage.Height + 3, 1, 1, -2, 2);
                        player1.HealthPoints = 200;
                        enemyWeapon.TheWeapons.Clear();
                        weapon.TheWeapons.Clear();
                        drawI = true;



                        drawI = false;
                        level = 1;
                    }
                    break;
                case 1:
                    theStat.Count = asteroid.GetAsteroidCount();

                    if (asteroid.GetAsteroidCount() > 1)
                    {
                        scroller.Scroll(scroll, graphics);
                        enemy.AccessWeapons = enemyWeapon;
                        asteroid.AccessWeapons = weapon;
                        asteroid.Timer = gameTime;
                        weapon.UpdateWeapons(gameTime);
                        asteroid.AccessWeapons = weapon;
                        enemyWeapon.UpdateWeapons(gameTime, sounds);
                        asteroid.UpdateAsteroids(asteroid, gameTime, sounds);
                        player1.AccessAsteroids = asteroid.AccessAsteroid;
                        player1.AccesEnemyWeapon = enemyWeapon;
                        player1.UpdatePlayer(gameTime, sounds);
                        enemyWeapon.AccessPlayer = player1;
                        enemyWeapon.AcessWeapon = weapon;
                        fire = player1.AccessFire;
                        fire.UpdateFire();
                        weapon.Timer = gameTime;
                        asteroid.AccessWeapons = weapon;
                        enemy.AccessWeapons = enemyWeapon;
                        enemy.UpdateEnemy(gameTime);
                        enemyWeapon.AccessEnemy = enemy;
                        player1.AccessEnemy = enemy;
                    }
                    else
                    {
                        player1.GetStates();

                    }
                    if (asteroid.GetAsteroidCount() < 2 && (GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed)
                            || (player1.KeyState.IsKeyDown(Keys.X) && player1.OldKeyState.IsKeyUp(Keys.X)))
                    {

                        asteroid.CreateAsteroids(asteroid, 6, (int)Asteroid.AsteroidType.Small, -5, 5);
                        asteroid.CreateAsteroids(asteroid, 2, (int)Asteroid.AsteroidType.Medium, -4, 4);
                        asteroid.CreateAsteroids(asteroid, 4, (int)Asteroid.AsteroidType.Large, -3, 3);
                        asteroid.CreateAsteroids(asteroid, 14, (int)Asteroid.AsteroidType.ExtraLarge, -2, 2);
                        enemy.CreateEnemies(enemy, graphics.PreferredBackBufferWidth / 2, enemyImage.Height + 3, 1, 1, -2, 2);
                        player1.HealthPoints = 200;
                        enemyWeapon.TheWeapons.Clear();
                        weapon.TheWeapons.Clear();

                        level = 2;


                    }
                    break;

                case 2:
                    theStat.Count = asteroid.GetAsteroidCount();

                    if (asteroid.GetAsteroidCount() > 1)
                    {
                        scroller.Scroll(scroll, graphics);
                        enemy.AccessWeapons = enemyWeapon;
                        asteroid.AccessWeapons = weapon;
                        asteroid.Timer = gameTime;
                        weapon.UpdateWeapons(gameTime);
                        asteroid.AccessWeapons = weapon;
                        enemyWeapon.UpdateWeapons(gameTime, sounds);
                        asteroid.UpdateAsteroids(asteroid, gameTime, sounds);
                        player1.AccessAsteroids = asteroid.AccessAsteroid;
                        player1.AccesEnemyWeapon = enemyWeapon;
                        player1.UpdatePlayer(gameTime, sounds);
                        enemyWeapon.AccessPlayer = player1;
                        enemyWeapon.AcessWeapon = weapon;
                        fire = player1.AccessFire;
                        fire.UpdateFire();
                        weapon.Timer = gameTime;
                        asteroid.AccessWeapons = weapon;
                        enemy.AccessWeapons = enemyWeapon;
                        enemy.UpdateEnemy(gameTime);
                        enemyWeapon.AccessEnemy = enemy;
                        player1.AccessEnemy = enemy;
                    }
                    else
                    {
                        player1.GetStates();

                    }
                    if (asteroid.GetAsteroidCount() < 2 && (GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed)
                            || (player1.KeyState.IsKeyDown(Keys.X) && player1.OldKeyState.IsKeyUp(Keys.X)))
                    {

                        asteroid.CreateAsteroids(asteroid, 4, (int)Asteroid.AsteroidType.Small, -5, 5);
                        asteroid.CreateAsteroids(asteroid, 8, (int)Asteroid.AsteroidType.Medium, -4, 4);
                        asteroid.CreateAsteroids(asteroid, 15, (int)Asteroid.AsteroidType.Large, -3, 3);
                        asteroid.CreateAsteroids(asteroid, 7, (int)Asteroid.AsteroidType.ExtraLarge, -20, 20);
                        enemy.CreateEnemies(enemy, graphics.PreferredBackBufferWidth / 2, enemyImage.Height + 3, 1, 1, -2, 2);
                        player1.HealthPoints = 200;
                        enemyWeapon.TheWeapons.Clear();
                        weapon.TheWeapons.Clear();

                        level = 3;
                    }
                    break;
                case 3:
                    theStat.Count = asteroid.GetAsteroidCount();

                    if (asteroid.GetAsteroidCount() > 1)
                    {
                        scroller.Scroll(scroll, graphics);
                        enemy.AccessWeapons = enemyWeapon;
                        asteroid.AccessWeapons = weapon;
                        asteroid.Timer = gameTime;
                        weapon.UpdateWeapons(gameTime);
                        asteroid.AccessWeapons = weapon;
                        enemyWeapon.UpdateWeapons(gameTime, sounds);
                        asteroid.UpdateAsteroids(asteroid, gameTime, sounds);
                        player1.AccessAsteroids = asteroid.AccessAsteroid;
                        player1.AccesEnemyWeapon = enemyWeapon;
                        player1.UpdatePlayer(gameTime, sounds);
                        enemyWeapon.AccessPlayer = player1;
                        enemyWeapon.AcessWeapon = weapon;
                        fire = player1.AccessFire;
                        fire.UpdateFire();
                        weapon.Timer = gameTime;
                        asteroid.AccessWeapons = weapon;
                        enemy.AccessWeapons = enemyWeapon;
                        enemy.UpdateEnemy(gameTime);
                        enemyWeapon.AccessEnemy = enemy;
                        player1.AccessEnemy = enemy;
                    }
                    else
                    {
                        player1.GetStates();

                    }
                    if (asteroid.GetAsteroidCount() < 2 && (GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed)
                            || (player1.KeyState.IsKeyDown(Keys.X) && player1.OldKeyState.IsKeyUp(Keys.X)))
                    {

                        asteroid.CreateAsteroids(asteroid, 20, (int)Asteroid.AsteroidType.Small, -5, 5);
                        asteroid.CreateAsteroids(asteroid, 2, (int)Asteroid.AsteroidType.Medium, -4, 4);
                        asteroid.CreateAsteroids(asteroid, 4, (int)Asteroid.AsteroidType.Large, -3, 3);
                        asteroid.CreateAsteroids(asteroid, 4, (int)Asteroid.AsteroidType.ExtraLarge, -30, 30);
                        enemy.CreateEnemies(enemy, graphics.PreferredBackBufferWidth / 2, enemyImage.Height + 3, 1, 1, -2, 2);
                        player1.HealthPoints = 200;
                        enemyWeapon.TheWeapons.Clear();
                        weapon.TheWeapons.Clear();
                      


                        level = 4;


                    }
                    break;
                case 4:
                    theStat.Count = asteroid.GetAsteroidCount();

                    if (asteroid.GetAsteroidCount() > 1)
                    {
                        scroller.Scroll(scroll, graphics);
                        enemy.AccessWeapons = enemyWeapon;
                        asteroid.AccessWeapons = weapon;
                        asteroid.Timer = gameTime;
                        weapon.UpdateWeapons(gameTime);
                        asteroid.AccessWeapons = weapon;
                        enemyWeapon.UpdateWeapons(gameTime, sounds);
                        asteroid.UpdateAsteroids(asteroid, gameTime, sounds);
                        player1.AccessAsteroids = asteroid.AccessAsteroid;
                        player1.AccesEnemyWeapon = enemyWeapon;
                        player1.UpdatePlayer(gameTime, sounds);
                        enemyWeapon.AccessPlayer = player1;
                        enemyWeapon.AcessWeapon = weapon;
                        fire = player1.AccessFire;
                        fire.UpdateFire();
                        weapon.Timer = gameTime;
                        asteroid.AccessWeapons = weapon;
                        enemy.AccessWeapons = enemyWeapon;
                        enemy.UpdateEnemy(gameTime);
                        enemyWeapon.AccessEnemy = enemy;
                        player1.AccessEnemy = enemy;
                    }
                    else
                    {
                        player1.GetStates();

                    }
                    if (asteroid.GetAsteroidCount() < 2 && (GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed)
                            || (player1.KeyState.IsKeyDown(Keys.X) && player1.OldKeyState.IsKeyUp(Keys.X)))
                    {

                        asteroid.CreateAsteroids(asteroid, 2, (int)Asteroid.AsteroidType.Small, -5, 5);
                        asteroid.CreateAsteroids(asteroid, 20, (int)Asteroid.AsteroidType.Medium, -4, 4);
                        asteroid.CreateAsteroids(asteroid, 4, (int)Asteroid.AsteroidType.Large, -3, 3);
                        asteroid.CreateAsteroids(asteroid, 8, (int)Asteroid.AsteroidType.ExtraLarge, -30, 30);
                        enemy.CreateEnemies(enemy, graphics.PreferredBackBufferWidth / 2, enemyImage.Height + 3, 1, 1, -2, 2);
                        player1.HealthPoints = 200;
                        enemyWeapon.TheWeapons.Clear();
                        weapon.TheWeapons.Clear();
                     


                        level = 5;
                    }
                    break;
                case 5:
                     theStat.Count = asteroid.GetAsteroidCount();
                   
                       if(asteroid.GetAsteroidCount() >1){
                           scroller.Scroll(scroll, graphics);
                    enemy.AccessWeapons = enemyWeapon;
                    asteroid.AccessWeapons = weapon;
                    asteroid.Timer = gameTime;
                    weapon.UpdateWeapons(gameTime);
                    asteroid.AccessWeapons = weapon;
                    enemyWeapon.UpdateWeapons(gameTime,sounds);
                    asteroid.UpdateAsteroids(asteroid, gameTime,sounds);
                    player1.AccessAsteroids = asteroid.AccessAsteroid;
                    player1.AccesEnemyWeapon = enemyWeapon;
                    player1.UpdatePlayer(gameTime,sounds);
                    enemyWeapon.AccessPlayer = player1;
                    enemyWeapon.AcessWeapon = weapon;
                    fire = player1.AccessFire;
                    fire.UpdateFire();
                    weapon.Timer = gameTime;
                    asteroid.AccessWeapons = weapon;
                    enemy.AccessWeapons = enemyWeapon;
                    enemy.UpdateEnemy(gameTime);
                    enemyWeapon.AccessEnemy = enemy;
                    player1.AccessEnemy = enemy;
                       }
                       else
                       {
                           player1.GetStates();

                       }
                       if (asteroid.GetAsteroidCount() < 2 && (GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed)
                               || (player1.KeyState.IsKeyDown(Keys.X) && player1.OldKeyState.IsKeyUp(Keys.X)))
                       {

                           asteroid.CreateAsteroids(asteroid, 2, (int)Asteroid.AsteroidType.Small, -5, 5);
                           asteroid.CreateAsteroids(asteroid, 20, (int)Asteroid.AsteroidType.Medium, -4, 4);
                           asteroid.CreateAsteroids(asteroid, 4, (int)Asteroid.AsteroidType.Large, -3, 3);
                           asteroid.CreateAsteroids(asteroid, 8, (int)Asteroid.AsteroidType.ExtraLarge, -30, 30);
                           enemy.CreateEnemies(enemy, graphics.PreferredBackBufferWidth / 2, enemyImage.Height + 3, 1, 1, -2, 2);
                           player1.HealthPoints = 200;
                           enemyWeapon.TheWeapons.Clear();
                           weapon.TheWeapons.Clear();
                           


                           level = 6;
                       }
                    
                    break;
                case 6:
                     theStat.Count = asteroid.GetAsteroidCount();
                   
                       if(asteroid.GetAsteroidCount() >1){
                           scroller.Scroll(scroll, graphics);
                    enemy.AccessWeapons = enemyWeapon;
                    asteroid.AccessWeapons = weapon;
                    asteroid.Timer = gameTime;
                    weapon.UpdateWeapons(gameTime);
                    asteroid.AccessWeapons = weapon;
                    enemyWeapon.UpdateWeapons(gameTime,sounds);
                    asteroid.UpdateAsteroids(asteroid, gameTime,sounds);
                    player1.AccessAsteroids = asteroid.AccessAsteroid;
                    player1.AccesEnemyWeapon = enemyWeapon;
                    player1.UpdatePlayer(gameTime,sounds);
                    enemyWeapon.AccessPlayer = player1;
                    enemyWeapon.AcessWeapon = weapon;
                    fire = player1.AccessFire;
                    fire.UpdateFire();
                    weapon.Timer = gameTime;
                    asteroid.AccessWeapons = weapon;
                    enemy.AccessWeapons = enemyWeapon;
                    enemy.UpdateEnemy(gameTime);
                    enemyWeapon.AccessEnemy = enemy;
                    player1.AccessEnemy = enemy;
                       }
                       else
                       {
                           player1.GetStates();

                       }
                       if (asteroid.GetAsteroidCount() < 2 && (GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed)
                               || (player1.KeyState.IsKeyDown(Keys.X) && player1.OldKeyState.IsKeyUp(Keys.X)))
                       {

                           asteroid.CreateAsteroids(asteroid, 20, (int)Asteroid.AsteroidType.Small, -5, 5);
                           asteroid.CreateAsteroids(asteroid, 20, (int)Asteroid.AsteroidType.Medium, -4, 4);
                           asteroid.CreateAsteroids(asteroid, 20, (int)Asteroid.AsteroidType.Large, -3, 3);
                           asteroid.CreateAsteroids(asteroid, 8, (int)Asteroid.AsteroidType.ExtraLarge, -30, 30);
                           enemy.CreateEnemies(enemy, graphics.PreferredBackBufferWidth / 2, enemyImage.Height + 3, 1, 1, -2, 2);
                           player1.HealthPoints = 200;
                           enemyWeapon.TheWeapons.Clear();
                           weapon.TheWeapons.Clear();
                         


                           level = 7;
                       }
                    
                    break;
                case 7:
                     theStat.Count = asteroid.GetAsteroidCount();
                   
                       if(asteroid.GetAsteroidCount() >1){
                           scroller.Scroll(scroll, graphics);
                    enemy.AccessWeapons = enemyWeapon;
                    asteroid.AccessWeapons = weapon;
                    asteroid.Timer = gameTime;
                    weapon.UpdateWeapons(gameTime);
                    asteroid.AccessWeapons = weapon;
                    enemyWeapon.UpdateWeapons(gameTime,sounds);
                    asteroid.UpdateAsteroids(asteroid, gameTime,sounds);
                    player1.AccessAsteroids = asteroid.AccessAsteroid;
                    player1.AccesEnemyWeapon = enemyWeapon;
                    player1.UpdatePlayer(gameTime,sounds);
                    enemyWeapon.AccessPlayer = player1;
                    enemyWeapon.AcessWeapon = weapon;
                    fire = player1.AccessFire;
                    fire.UpdateFire();
                    weapon.Timer = gameTime;
                    asteroid.AccessWeapons = weapon;
                    enemy.AccessWeapons = enemyWeapon;
                    enemy.UpdateEnemy(gameTime);
                    enemyWeapon.AccessEnemy = enemy;
                    player1.AccessEnemy = enemy;
                       }
                       else
                       {
                           player1.GetStates();

                       }
                       if (asteroid.GetAsteroidCount() < 2 && (GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed)
                               || (player1.KeyState.IsKeyDown(Keys.X) && player1.OldKeyState.IsKeyUp(Keys.X)))
                       {

                           asteroid.CreateAsteroids(asteroid, 2, (int)Asteroid.AsteroidType.Small, -5, 5);
                           asteroid.CreateAsteroids(asteroid, 12, (int)Asteroid.AsteroidType.Medium, -4, 4);
                           asteroid.CreateAsteroids(asteroid, 12, (int)Asteroid.AsteroidType.Large, -3, 3);
                           asteroid.CreateAsteroids(asteroid, 12, (int)Asteroid.AsteroidType.ExtraLarge, -30, 30);
                           enemy.CreateEnemies(enemy, graphics.PreferredBackBufferWidth / 2, enemyImage.Height + 3, 1, 1, -2, 2);
                           player1.HealthPoints = 200;
                           enemyWeapon.TheWeapons.Clear();
                           weapon.TheWeapons.Clear();
                           


                           level = 8;
                       }
                    break;
                case 8:
                     theStat.Count = asteroid.GetAsteroidCount();
                   
                       if(asteroid.GetAsteroidCount() >1){
                           scroller.Scroll(scroll, graphics);
                    enemy.AccessWeapons = enemyWeapon;
                    asteroid.AccessWeapons = weapon;
                    asteroid.Timer = gameTime;
                    weapon.UpdateWeapons(gameTime);
                    asteroid.AccessWeapons = weapon;
                    enemyWeapon.UpdateWeapons(gameTime,sounds);
                    asteroid.UpdateAsteroids(asteroid, gameTime,sounds);
                    player1.AccessAsteroids = asteroid.AccessAsteroid;
                    player1.AccesEnemyWeapon = enemyWeapon;
                    player1.UpdatePlayer(gameTime,sounds);
                    enemyWeapon.AccessPlayer = player1;
                    enemyWeapon.AcessWeapon = weapon;
                    fire = player1.AccessFire;
                    fire.UpdateFire();
                    weapon.Timer = gameTime;
                    asteroid.AccessWeapons = weapon;
                    enemy.AccessWeapons = enemyWeapon;
                    enemy.UpdateEnemy(gameTime);
                    enemyWeapon.AccessEnemy = enemy;
                    player1.AccessEnemy = enemy;
                       }
                       else
                       {
                           player1.GetStates();

                       }
                       if (asteroid.GetAsteroidCount() < 2 && (GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed)
                               || (player1.KeyState.IsKeyDown(Keys.X) && player1.OldKeyState.IsKeyUp(Keys.X)))
                       {

                           asteroid.CreateAsteroids(asteroid, 1, (int)Asteroid.AsteroidType.Small, -5, 5);
                           asteroid.CreateAsteroids(asteroid,1 , (int)Asteroid.AsteroidType.Medium, -4, 4);
                           asteroid.CreateAsteroids(asteroid, 1, (int)Asteroid.AsteroidType.Large, -3, 3);
                           asteroid.CreateAsteroids(asteroid, 40, (int)Asteroid.AsteroidType.ExtraLarge, -30, 30);
                           enemy.CreateEnemies(enemy, graphics.PreferredBackBufferWidth / 2, enemyImage.Height + 3, 1, 1, -2, 2);
                           player1.HealthPoints = 200;
                           enemyWeapon.TheWeapons.Clear();
                           weapon.TheWeapons.Clear();
                         


                           level = 9;
                       }
                    break;
                case 9:
                     theStat.Count = asteroid.GetAsteroidCount();
                   
                       if(asteroid.GetAsteroidCount() >1){
                           scroller.Scroll(scroll, graphics);
                    enemy.AccessWeapons = enemyWeapon;
                    asteroid.AccessWeapons = weapon;
                    asteroid.Timer = gameTime;
                    weapon.UpdateWeapons(gameTime);
                    asteroid.AccessWeapons = weapon;
                    enemyWeapon.UpdateWeapons(gameTime,sounds);
                    asteroid.UpdateAsteroids(asteroid, gameTime,sounds);
                    player1.AccessAsteroids = asteroid.AccessAsteroid;
                    player1.AccesEnemyWeapon = enemyWeapon;
                    player1.UpdatePlayer(gameTime,sounds);
                    enemyWeapon.AccessPlayer = player1;
                    enemyWeapon.AcessWeapon = weapon;
                    fire = player1.AccessFire;
                    fire.UpdateFire();
                    weapon.Timer = gameTime;
                    asteroid.AccessWeapons = weapon;
                    enemy.AccessWeapons = enemyWeapon;
                    enemy.UpdateEnemy(gameTime);
                    enemyWeapon.AccessEnemy = enemy;
                    player1.AccessEnemy = enemy;
                       }
                       else
                       {
                           player1.GetStates();

                       }
                       if (asteroid.GetAsteroidCount() < 2 && (GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed)
                               || (player1.KeyState.IsKeyDown(Keys.X) && player1.OldKeyState.IsKeyUp(Keys.X)))
                       {

                           asteroid.CreateAsteroids(asteroid, 2, (int)Asteroid.AsteroidType.Small, -5, 5);
                           asteroid.CreateAsteroids(asteroid, 20, (int)Asteroid.AsteroidType.Medium, -4, 4);
                           asteroid.CreateAsteroids(asteroid, 50, (int)Asteroid.AsteroidType.Large, -3, 3);
                           asteroid.CreateAsteroids(asteroid, 5, (int)Asteroid.AsteroidType.ExtraLarge, -30, 30);
                           enemy.CreateEnemies(enemy, graphics.PreferredBackBufferWidth / 2, enemyImage.Height + 3, 1, 1, -2, 2);
                           player1.HealthPoints = 200;
                           enemyWeapon.TheWeapons.Clear();
                           weapon.TheWeapons.Clear();
                         


                           level = 10;
                       }
                    break;
                case 10:
                     theStat.Count = asteroid.GetAsteroidCount();
                   
                       if(asteroid.GetAsteroidCount() >1){
                           scroller.Scroll(scroll, graphics);
                    enemy.AccessWeapons = enemyWeapon;
                    asteroid.AccessWeapons = weapon;
                    asteroid.Timer = gameTime;
                    weapon.UpdateWeapons(gameTime);
                    asteroid.AccessWeapons = weapon;
                    enemyWeapon.UpdateWeapons(gameTime,sounds);
                    asteroid.UpdateAsteroids(asteroid, gameTime,sounds);
                    player1.AccessAsteroids = asteroid.AccessAsteroid;
                    player1.AccesEnemyWeapon = enemyWeapon;
                    player1.UpdatePlayer(gameTime,sounds);
                    enemyWeapon.AccessPlayer = player1;
                    enemyWeapon.AcessWeapon = weapon;
                    fire = player1.AccessFire;
                    fire.UpdateFire();
                    weapon.Timer = gameTime;
                    asteroid.AccessWeapons = weapon;
                    enemy.AccessWeapons = enemyWeapon;
                    enemy.UpdateEnemy(gameTime);
                    enemyWeapon.AccessEnemy = enemy;
                    player1.AccessEnemy = enemy;
                       }
                       else
                       {
                           player1.GetStates();

                       }
                       if (asteroid.GetAsteroidCount() < 2 && (GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed)
                               || (player1.KeyState.IsKeyDown(Keys.X) && player1.OldKeyState.IsKeyUp(Keys.X)))
                       {

                           asteroid.CreateAsteroids(asteroid, 20, (int)Asteroid.AsteroidType.Small, -5, 5);
                           asteroid.CreateAsteroids(asteroid, 20, (int)Asteroid.AsteroidType.Medium, -4, 4);
                           asteroid.CreateAsteroids(asteroid, 4, (int)Asteroid.AsteroidType.Large, -3, 3);
                           asteroid.CreateAsteroids(asteroid, 15, (int)Asteroid.AsteroidType.ExtraLarge, -30, 30);
                        
                           player1.HealthPoints = 200;
                           enemyWeapon.TheWeapons.Clear();
                           weapon.TheWeapons.Clear();
                           enemy.AccessEnemyList.Clear();
                           enemy.CreateEnemies(enemy, graphics.PreferredBackBufferWidth / 2, enemyImage.Height + 3, 1, 1, -2, 2);

                           level = 0;
                       }
                    break;

            }
            
                //currentFrame = (currentFrame + 1) % numOfFrames;
            
           
            base.Update(gameTime);
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            switch(level)
            {
                case 0:
                    scroller.Draw(scroll, spriteBatch);
                    
                    asteroid.HostAsteroid = asteroid;
                    if(asteroid .GetAsteroidCount() >1)
                        asteroid.DrawAsteroids(spriteBatch);
                    fire.DrawFire(spriteBatch);
                    enemyWeapon.AccessEnemy = enemy;
                    weapon.DrawWeapons(spriteBatch);
                    if (asteroid.GetAsteroidCount() > 1)
                        enemyWeapon.DrawWeapons(spriteBatch);
                    player1.DrawPlayer(spriteBatch);
                    weapon.AccessPlayer = player1;
                    if (asteroid.GetAsteroidCount() > 1)
                        enemy.DrawEnemy(spriteBatch);



                    theStat.DrawLevelScreen(spriteBatch, nextLevel, levelRec, level, nextLevelFont, nextLevel.Width / 2, nextLevel.Height / 2, Color.White);
                    break;
                case 1:
                    scroller.Draw(scroll, spriteBatch);
                     asteroid.HostAsteroid = asteroid;
                     if (asteroid.GetAsteroidCount() > 1)
                         asteroid.DrawAsteroids(spriteBatch);
                     fire.DrawFire(spriteBatch);
                    enemyWeapon.AccessEnemy = enemy;
                    weapon.DrawWeapons(spriteBatch);
                    if (asteroid.GetAsteroidCount() > 1)
                        enemyWeapon.DrawWeapons(spriteBatch);
                    player1.DrawPlayer(spriteBatch);
                    weapon.AccessPlayer = player1;

                    enemy.DrawEnemy(spriteBatch);
                    theStat.DrawLevelScreen(spriteBatch, nextLevel, levelRec, level, nextLevelFont, nextLevel.Width / 2, nextLevel.Height / 2, Color.White);
                    break;
                case 2:
                    scroller.Draw(scroll, spriteBatch);
                      asteroid.HostAsteroid = asteroid;
                     if (asteroid.GetAsteroidCount() > 1)
                         asteroid.DrawAsteroids(spriteBatch);
                     fire.DrawFire(spriteBatch);
                    enemyWeapon.AccessEnemy = enemy;
                    weapon.DrawWeapons(spriteBatch);
                    if (asteroid.GetAsteroidCount() > 1)
                        enemyWeapon.DrawWeapons(spriteBatch);
                    player1.DrawPlayer(spriteBatch);
                    weapon.AccessPlayer = player1;

                    enemy.DrawEnemy(spriteBatch);
                    theStat.DrawLevelScreen(spriteBatch, nextLevel, levelRec, level, nextLevelFont, nextLevel.Width / 2, nextLevel.Height / 2, Color.White);
                    break;
                case 3:
                    scroller.Draw(scroll, spriteBatch);
                     asteroid.HostAsteroid = asteroid;
                     if (asteroid.GetAsteroidCount() > 1)
                         asteroid.DrawAsteroids(spriteBatch);
                     fire.DrawFire(spriteBatch);
                    enemyWeapon.AccessEnemy = enemy;
                    weapon.DrawWeapons(spriteBatch);
                    if (asteroid.GetAsteroidCount() > 1)
                        enemyWeapon.DrawWeapons(spriteBatch);
                    player1.DrawPlayer(spriteBatch);
                    weapon.AccessPlayer = player1;

                    enemy.DrawEnemy(spriteBatch);
                    theStat.DrawLevelScreen(spriteBatch, nextLevel, levelRec, level, nextLevelFont, nextLevel.Width / 2, nextLevel.Height / 2, Color.White);
                    break;
                case 4:
                    scroller.Draw(scroll, spriteBatch);
                     asteroid.HostAsteroid = asteroid;
                     if (asteroid.GetAsteroidCount() > 1)
                         asteroid.DrawAsteroids(spriteBatch);
                     fire.DrawFire(spriteBatch);
                    enemyWeapon.AccessEnemy = enemy;
                    weapon.DrawWeapons(spriteBatch);
                    if (asteroid.GetAsteroidCount() > 1)
                        enemyWeapon.DrawWeapons(spriteBatch);
                    player1.DrawPlayer(spriteBatch);
                    weapon.AccessPlayer = player1;

                    enemy.DrawEnemy(spriteBatch);
                    theStat.DrawLevelScreen(spriteBatch, nextLevel, levelRec, level, nextLevelFont, nextLevel.Width / 2, nextLevel.Height / 2, Color.White);
                    break;
                case 5:
                    scroller.Draw(scroll, spriteBatch);
                    asteroid.HostAsteroid = asteroid;
                    if (asteroid.GetAsteroidCount() > 1)
                        asteroid.DrawAsteroids(spriteBatch);
                    fire.DrawFire(spriteBatch);
                    enemyWeapon.AccessEnemy = enemy;
                    weapon.DrawWeapons(spriteBatch);
                    if (asteroid.GetAsteroidCount() > 1)
                        enemyWeapon.DrawWeapons(spriteBatch);
                    player1.DrawPlayer(spriteBatch);
                    weapon.AccessPlayer = player1;

                    enemy.DrawEnemy(spriteBatch);
                    theStat.DrawLevelScreen(spriteBatch, nextLevel, levelRec, level, nextLevelFont, nextLevel.Width / 2, nextLevel.Height / 2, Color.White);
                    break;
                case 6:
                    scroller.Draw(scroll, spriteBatch);
                    asteroid.HostAsteroid = asteroid;
                    if (asteroid.GetAsteroidCount() > 1)
                        asteroid.DrawAsteroids(spriteBatch);
                    fire.DrawFire(spriteBatch);
                    enemyWeapon.AccessEnemy = enemy;
                    weapon.DrawWeapons(spriteBatch);
                    if (asteroid.GetAsteroidCount() > 1)
                        enemyWeapon.DrawWeapons(spriteBatch);
                    player1.DrawPlayer(spriteBatch);
                    weapon.AccessPlayer = player1;

                    enemy.DrawEnemy(spriteBatch);
                    theStat.DrawLevelScreen(spriteBatch, nextLevel, levelRec, level, nextLevelFont, nextLevel.Width / 2, nextLevel.Height / 2, Color.White);
                    break;
                case 7:
                    scroller.Draw(scroll, spriteBatch);
                    asteroid.HostAsteroid = asteroid;
                    if (asteroid.GetAsteroidCount() > 1)
                        asteroid.DrawAsteroids(spriteBatch);
                    fire.DrawFire(spriteBatch);
                    enemyWeapon.AccessEnemy = enemy;
                    weapon.DrawWeapons(spriteBatch);
                    if (asteroid.GetAsteroidCount() > 1)
                        enemyWeapon.DrawWeapons(spriteBatch);
                    player1.DrawPlayer(spriteBatch);
                    weapon.AccessPlayer = player1;

                    enemy.DrawEnemy(spriteBatch);
                    theStat.DrawLevelScreen(spriteBatch, nextLevel, levelRec, level, nextLevelFont, nextLevel.Width / 2, nextLevel.Height / 2, Color.White);
                    break;
                case 8:
                    scroller.Draw(scroll, spriteBatch);
                    asteroid.HostAsteroid = asteroid;
                    if (asteroid.GetAsteroidCount() > 1)
                        asteroid.DrawAsteroids(spriteBatch);
                    fire.DrawFire(spriteBatch);
                    enemyWeapon.AccessEnemy = enemy;
                    weapon.DrawWeapons(spriteBatch);
                    if (asteroid.GetAsteroidCount() > 1)
                        enemyWeapon.DrawWeapons(spriteBatch);
                    player1.DrawPlayer(spriteBatch);
                    weapon.AccessPlayer = player1;

                    enemy.DrawEnemy(spriteBatch);
                    theStat.DrawLevelScreen(spriteBatch, nextLevel, levelRec, level, nextLevelFont, nextLevel.Width / 2, nextLevel.Height / 2, Color.White);
                    break;
                case 9:
                    scroller.Draw(scroll, spriteBatch);
                    asteroid.HostAsteroid = asteroid;
                    if (asteroid.GetAsteroidCount() > 1)
                        asteroid.DrawAsteroids(spriteBatch);
                    fire.DrawFire(spriteBatch);
                    enemyWeapon.AccessEnemy = enemy;
                    weapon.DrawWeapons(spriteBatch);
                    if (asteroid.GetAsteroidCount() > 1)
                        enemyWeapon.DrawWeapons(spriteBatch);
                    player1.DrawPlayer(spriteBatch);
                    weapon.AccessPlayer = player1;

                    enemy.DrawEnemy(spriteBatch);
                    theStat.DrawLevelScreen(spriteBatch, nextLevel, levelRec, level, nextLevelFont, nextLevel.Width / 2, nextLevel.Height / 2, Color.White);
                    break;
                case 10:
                    scroller.Draw(scroll, spriteBatch);
                    asteroid.HostAsteroid = asteroid;
                    if (asteroid.GetAsteroidCount() > 1)
                        asteroid.DrawAsteroids(spriteBatch);
                    fire.DrawFire(spriteBatch);
                    enemyWeapon.AccessEnemy = enemy;
                    weapon.DrawWeapons(spriteBatch);
                    if (asteroid.GetAsteroidCount() > 1)
                        enemyWeapon.DrawWeapons(spriteBatch);
                    player1.DrawPlayer(spriteBatch);
                    weapon.AccessPlayer = player1;

                    enemy.DrawEnemy(spriteBatch);
                    theStat.DrawLevelScreen(spriteBatch, nextLevel, levelRec, level, nextLevelFont, nextLevel.Width / 2, nextLevel.Height / 2, Color.White);
                    break;
            }
          
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }


       
    }
}
