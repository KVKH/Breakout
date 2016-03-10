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

namespace Breakout
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GraphicsDevice device;
        Texture2D backgroundTexture;
        Texture2D ammosprite;
        Texture2D paddleSprite;
        Vector2 paddlePosition;
        Vector2 ammoposition = Vector2.Zero;
        Vector2 ammospeed = new Vector2(250, 250);
        int screenWidth;
        int screenHeight;

        //Brickstuff

        Texture2D brickSprite;
        Rectangle brickLocation;
        Color brickTint;
        bool alive;

        int bricksWide = 10;
        int bricksHigh = 5;
        Texture2D brickImage;
        //Brick[,] bricks;

        Rectangle screenrekt;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            screenrekt = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = "Breakout HD";
            IsMouseVisible = false;

            base.Initialize();

            paddlePosition = new Vector2(
                graphics.GraphicsDevice.Viewport.Width / 2 - paddleSprite.Width / 2,
                graphics.GraphicsDevice.Viewport.Height - paddleSprite.Height);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            device = graphics.GraphicsDevice;


            backgroundTexture = Content.Load<Texture2D>("background");
            screenWidth = device.PresentationParameters.BackBufferWidth;
            screenHeight = device.PresentationParameters.BackBufferHeight;
            ammosprite = Content.Load<Texture2D>("ammo");
            paddleSprite = Content.Load<Texture2D>("pad");
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.F))
            {
                graphics.IsFullScreen = !graphics.IsFullScreen;
                graphics.ApplyChanges();
            }


            if(paddlePosition.X < 0)
            {
                paddlePosition.X = 0;
            }
            if(paddlePosition.X + paddleSprite.Width > screenrekt.Width)
            {
                paddlePosition.X = screenrekt.Width - paddleSprite.Width;
            }

            ammoposition += ammospeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            int maxX = GraphicsDevice.Viewport.Width - ammosprite.Width;
            int maxY = GraphicsDevice.Viewport.Height - ammosprite.Height;

            if (ammoposition.X > maxX || ammoposition.X < 0)
                ammospeed.X *= -1;
            if (ammoposition.Y > maxY || ammoposition.Y < 0)
                ammospeed.Y *= -1;

            if (ammoposition.X > maxX || ammoposition.X < 0)
                ammospeed.X *= -1;
            if (ammoposition.Y < 0)
                ammospeed.Y *= -1;
            else if (ammoposition.Y > maxY)
            {
                ammoposition.Y = 0;
                ammospeed.X = 250;
                ammospeed.Y = 250;
            }

            Rectangle ammorect =
                new Rectangle((int)ammoposition.X, (int)ammoposition.Y,
            ammosprite.Width, ammosprite.Height);

            Rectangle padrect =
                new Rectangle((int)paddlePosition.X, (int)paddlePosition.Y,
                    paddleSprite.Width, paddleSprite.Height);

            if (ammorect.Intersects(padrect) && ammospeed.Y > 0)
            {
                ammospeed.Y += 50;
                if (ammospeed.X < 0)
                    ammospeed.X -= 50;
                else
                    ammospeed.X += 50;

                ammospeed.Y *= -1;
            }

            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Right))
                paddlePosition.X += 5;
            else if (keyState.IsKeyDown(Keys.Left))
                paddlePosition.X -= 5;

            /* TÄÄ PITÄIS LISÄTÄ / ESTÄÄ PADIA MENEMÄSTÄ YLI
             * 
            KeyboardState newKeyState = Keyboard.GetState();
            if (newKeyState.IsKeyDown(Keys.Right) && X + paddleSprite.Width
                + moveDistance <= GraphicsDevice.Viewport.Width)
            {
                X += moveDistance;
            }
            else if (newKeyState.IsKeyDown(Keys.Left) && X - moveDistance >= 0)
            {
                X -= moveDistance;
            }
            */

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();
            DrawBG();
            spriteBatch.Draw(ammosprite, ammoposition, Color.White);
            spriteBatch.Draw(paddleSprite, paddlePosition, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
        private void DrawBG()
        {
            Rectangle screenRectangle = new Rectangle(0, 0, screenWidth, screenHeight);
            spriteBatch.Draw(backgroundTexture, screenRectangle, Color.White);

        }

        //Brick

        public Rectangle Location
        {
            get { return brickLocation; }
        }
        /*public Brick(Texture2D brickSprite, Rectangle brickLocation, Color brickTint)
        {
            this.brickSprite = brickSprite;
            this.brickLocation = brickLocation;
            this.brickTint = brickTint;
            this.alive = true;
        }
        public void CheckCollision(Ball ball KORVAA AMMO)
        {
        }*/
        public void Draw(SpriteBatch spriteBatch)
        {
            if (alive)
            spriteBatch.Draw(brickSprite, brickLocation, brickTint);
        }


    }
}

/* Tutorials:
 * http://www.riemers.net/eng/Tutorials/XNA/Csharp/Series2D/Drawing_fullscreen_images.php
 * http://www.harding.edu/fmccown/xna/pong/
 * http://xnagpa.net/xna4beginner.php
 * 
 * Remeksen Drive:
 * https://drive.google.com/folderview?id=0BzImvAoTbOrdOGJlNzFSSTktWk0&usp=sharing
 * 
 * TEE GRAFFOIHIN OHJEET:
 * ESC = EXIT
 * LIIKUTTAMINEN
 * F = FULSCREEN
 * 
 * VAIHDA GRAFFAT FULLSCREENIKSI
*/
