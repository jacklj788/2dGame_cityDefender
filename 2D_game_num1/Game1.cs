using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2D_game_num1
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D player_texture, enemy_texture, backDrop, rubble_texture, rubbleAnimation, bombTexture, missileTexture, Victory;
        Player player;
        Enemy enemy1, enemy2;
        Bomb bomb1, bomb2, missile1;
        Rubble rubble1, rubble2;
        //Enemy[] enemies = new Enemy[10];

        Vector2 backdropLocation = new Vector2(0, 0);

        // These rectangles are for collision 
        Rectangle playerZone, enemyZone, enemyZone2, bombZone, bombZone2, missileZone;
        // Seperating these rectangles as they're for animation 
        Rectangle sourceRubble, sourceBomb, sourceBomb2, sourceMissile;
        // elapsed time, does as it says. Stores the elaspsed time (in miliseconds currently). 
        float[] elapsedTime = new float[4];
        float[] counter = new float[2];
        // once we've used all of our frames (hit the final block of our image) it will loop back. 
        int rubbleFrames, bombFrames, bomb2Frames, missileFrames;

        //bool bombActive = false;



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            // Sets the width and height of our window. Funny enough these were the defaults for Monogame anyway. 
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            // Can either do it here or at the top when I create the class to begin with. Not sure on the difference but lets just do it this way. 
            player = new Player("Jack");

            // Left point, right points, X and Y starting points. 
            enemy1 = new Enemy(50, 421, 50, 50);
            enemy2 = new Enemy(200, 627, 250, 100);

            rubble1 = new Rubble();
            rubble2 = new Rubble();

            bomb1 = new Bomb(enemy1);
            bomb2 = new Bomb(enemy2);
            missile1 = new Bomb(player);
            //enemies[1] = new Enemy();
            counter[0] = 0;
            counter[1] = 0;
            // 
            //destinationRubble = new Rectangle((int)rubble2.GetLocationX(), (int)rubble2.GetLocationY(), 320, 150);
            base.Initialize();
        }


        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player_texture = Content.Load<Texture2D>("tank");
            enemy_texture = Content.Load<Texture2D>("Enemy_new");
            backDrop = Content.Load<Texture2D>("Background_NEW");
            rubble_texture = Content.Load<Texture2D>("Rubble");
            rubbleAnimation = Content.Load<Texture2D>("Rubble_animation_NEW");
            bombTexture = Content.Load<Texture2D>("bomb");
            missileTexture = Content.Load<Texture2D>("missile3");
            Victory = Content.Load<Texture2D>("YouWon");

            // TODO: use this.Content to load your game content here

            //destinationRubble = new Rectangle((int)rubble2.GetLocationX(), (int)rubble2.GetLocationY(), 80, 150);
        }


        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // The first rectangle is the destination rectangle. 
            // Second rectangle is the SOURCE which defines where within our Texture we're actually drawing
            // Source rectangle explained: 0, 0, 80, 150 = The first two 0's are the starting points on our images (0, 0) being the top left.
            // The 80 is how far along it then needs to draw, and 150 is how far down. 

            // Once the game is other it stops running all of this code, I stuck this here quickly and assume it works. Testing hasnt really proved or disproved it yet.
            if (Enemy.GetCount() > 0)
            {
                // I for some reason need multiple of these, not yet looked into just using the same one other and other
                // not even sure if possible, since they all manage different times and animations. 
                elapsedTime[0] += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                elapsedTime[1] += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                elapsedTime[2] += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                elapsedTime[3] += (float)gameTime.TotalGameTime.TotalMilliseconds;

                // If it hasn't already ran the animation frame within 120ms, do so
                if (elapsedTime[0] >= 120)
                {
                    // 4 frames = 0, 1, 2, 3
                    // If we have more or less frames, update this. 
                    // Should probably move this code to the class to make it neat. 
                    if (rubbleFrames >= 7)
                    {
                        rubbleFrames = 0;
                    }
                    else
                    {
                        rubbleFrames++;
                    }
                    elapsedTime[0] = 0;
                }

                // 80 * frames makes it so every frame it shifts 80 along the X acis (80 * 0 = 0, so it starts at 0, then 80 * 1 = 80, so it starts at 80)
                sourceRubble = new Rectangle(80 * rubbleFrames, 0, 80, 150);

                if (elapsedTime[1] >= 230)
                {
                    // we only want it to go to 2 for now because number 3 is the explosions. 
                    if (bombFrames >= 2)
                    {
                        bombFrames = 0;
                        bomb2Frames = 0;
                    }
                    else
                    {
                        bombFrames++;
                        bomb2Frames++;
                    }
                    elapsedTime[1] = 0;
                }
                // need 1 unique source and frames varable for each bomb, otherwise 1 bomb exploding will make all the bombs look like they're exploding. 
                sourceBomb = new Rectangle(60 * bombFrames, 0, 60, 60);
                sourceBomb2 = new Rectangle(60 * bomb2Frames, 0, 60, 60);
                // if the specific bomb hits the player, set that bomb and that bomb only to frame 4 (60 * 3) which is the explosion.  
                if (playerZone.Intersects(bombZone))
                {
                    sourceBomb = new Rectangle(60 * 3, 0, 60, 60);
                }
                if (playerZone.Intersects(bombZone2))
                {
                    sourceBomb2 = new Rectangle(60 * 3, 0, 60, 60);
                }

                // need 1 unique source and frames varable for each bomb, otherwise 1 bomb exploding will make all the bombs look like they're exploding. 


                if (elapsedTime[2] >= 200)
                {
                    // we only want it to go to 2 for now because number 3 is the explosions. 
                    if (missileFrames >= 1)
                    {
                        missileFrames = 0;
                    }
                    else
                    {
                        missileFrames++;
                    }
                    elapsedTime[2] = 0;
                }
                sourceMissile = new Rectangle(60 * missileFrames, 0, 60, 60);
                if (enemyZone.Intersects(missileZone))// | enemyZone2.Intersects(missileZone))
                {
                    // sets it so the Missile displays the explosion frame on collision
                    sourceMissile = new Rectangle(60 * 2, 0, 60, 60);
                    counter[0]++;
                    // this needs a little tinkering to work correctly. 
                    if (counter[0] >= 20)
                    {
                        missile1.DeactivateBomb();
                        enemy1.Kill();
                        counter[0] = 0;
                    }

                }
                if (enemyZone2.Intersects(missileZone))
                {
                    // sets it so the Missile displays the explosion frame on collision
                    sourceMissile = new Rectangle(60 * 2, 0, 60, 60);
                    counter[1]++;
                    // this needs a little tinkering to work correctly. 
                    if (counter[1] >= 20)
                    {
                        missile1.DeactivateBomb();
                        enemy2.Kill();
                        counter[1] = 0;
                    }

                }


                // TODO: Add your update logic here
                KeyboardState kb = Keyboard.GetState();

                if (kb.IsKeyDown(Keys.A))
                {
                    player.MovePlayerLeft();
                }
                if (kb.IsKeyDown(Keys.D))
                {
                    player.MovePlayerRight();
                }

                // These if statements define the play area
                // We don't need a Y set because its a tank (it cant go up and down).
                if (player.GetLocationX() >= 790)
                {
                    player.SetPlayerX(790);
                }
                else if (player.GetLocationX() <= 0)
                {
                    player.SetPlayerX(0);
                }

                // Moves the ENEMY - enemies dont need player input so a lot simpler (at least within this class, the real code is in the enemy class)
                enemy1.UpdateLocation();
                enemy2.UpdateLocation();

                // Detects if the bomb is currently active. 
                // If this code isn't within an if, then it will constantly place the bomb ontop of the enemy helicopter 
                if (bomb1.GetState() == false)
                {
                    bomb1.SetBombLocation_withEnemyLocation();
                    bomb1.ActivateBomb();
                }
                bomb1.BombFreeFall();
                if (bomb2.GetState() == false)
                {
                    bomb2.SetBombLocation_withEnemyLocation();
                    bomb2.ActivateBomb();
                }
                bomb2.BombFreeFall();

                missile1.MissileFireUp();

                if (kb.IsKeyDown(Keys.Up))
                {
                    missile1.ActivateBomb();
                    missile1.SetBombLocation_withPlayerLocation();
                }


                // If the bomb hits the floor, respawn
                if (bomb1.GetLocationY() > 500)
                {
                    bomb1.SetBombLocation_withEnemyLocation();
                }
                if (bomb2.GetLocationY() > 500)
                {
                    bomb2.SetBombLocation_withEnemyLocation();
                }

                rubble1.RubbleFreeFall();
                rubble2.RubbleFreeFall();

                // Defines the rectangles which are used for collision detections. They are invisible bounding boxes
                // Sets the location on where to place the rectangle, then how big they should be
                playerZone = new Rectangle((int)player.GetLocationX(), (int)player.GetLocationY(), 60, 60);
                enemyZone = new Rectangle((int)enemy1.GetLocationX(), (int)enemy1.GetLocationY(), 35, 35);
                enemyZone2 = new Rectangle((int)enemy2.GetLocationX(), (int)enemy2.GetLocationY(), 35, 35);
                bombZone = new Rectangle((int)bomb1.GetLocationX(), (int)bomb1.GetLocationY(), 30, 20);
                bombZone2 = new Rectangle((int)bomb2.GetLocationX(), (int)bomb2.GetLocationY(), 30, 20);
                missileZone = new Rectangle((int)missile1.GetLocationX(), (int)missile1.GetLocationY(), 25, 60);

            }

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(backDrop, backdropLocation, Color.White);
            spriteBatch.Draw(player_texture, player.GetLocation(), Color.White);
            spriteBatch.Draw(enemy_texture, enemy1.GetLocation(), Color.White);
            spriteBatch.Draw(enemy_texture, enemy2.GetLocation(), Color.White);
            // This is all for animation, which uses a source rectangle not just a vector2 location. 
            spriteBatch.Draw(rubbleAnimation, rubble1.GetLocation(), sourceRubble, Color.White);
            spriteBatch.Draw(rubbleAnimation, rubble2.GetLocation(), sourceRubble, Color.White);
            spriteBatch.Draw(bombTexture, bomb1.GetLocation(), sourceBomb, Color.White);
            spriteBatch.Draw(bombTexture, bomb2.GetLocation(), sourceBomb2, Color.White);
            if (missile1.GetState() == true)
            spriteBatch.Draw(missileTexture, missile1.GetLocation(), sourceMissile, Color.White);
            // You win OR you lose here depending on how many enemies are left.
            // getCount is a static float, so it doesn't need a specific enemy. (It's like Console.WriteLine();)
            if (Enemy.GetCount() == 0)
            {
                spriteBatch.Draw(Victory, backdropLocation, Color.White);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
