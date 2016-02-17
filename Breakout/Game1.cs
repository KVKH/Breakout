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
        Vector2 ammoposition = Vector2.Zero;
        Vector2 ammospeed = new Vector2(150, 150);
        int screenWidth;
        int screenHeight;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = "Breakout HD";

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            device = graphics.GraphicsDevice;


            backgroundTexture = Content.Load<Texture2D>("background");
            screenWidth = device.PresentationParameters.BackBufferWidth;
            screenHeight = device.PresentationParameters.BackBufferHeight;
            ammosprite = Content.Load<Texture2D>("ammo");
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            ammoposition += ammospeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            int maxX = GraphicsDevice.Viewport.Width - ammosprite.Width;
            int maxY = GraphicsDevice.Viewport.Height - ammosprite.Height;

            if (ammoposition.X > maxX || ammoposition.X < 0)
                ammospeed.X *= -1;
            if (ammoposition.Y > maxY || ammoposition.Y < 0)
                ammospeed.Y *= -1;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            DrawBG();
            spriteBatch.Draw(ammosprite, ammoposition, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
        private void DrawBG()
        {
            Rectangle screenRectangle = new Rectangle(0, 0, screenWidth, screenHeight);
            spriteBatch.Draw(backgroundTexture, screenRectangle, Color.White);
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
*/
