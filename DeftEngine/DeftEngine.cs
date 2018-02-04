using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace DeftEngine
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class DeftEngine : Game
    {
        public GraphicsDeviceManager graphics;
        public Color bgColor = Color.DimGray;
        public SpriteBatch spriteBatch;
        public GameTime gameTime;

        private static Random _rand = new Random();

        public static int RandInt(int min, int max)
            => _rand.Next(min, max);

        public static byte RandByte(byte min, byte max)
            => (byte)_rand.Next(min, max);

        public static float RandFloat01()
        {
            return (float)_rand.NextDouble();
        }

        public static float RandFloat1Neg1()
        {
            var result = _rand.NextDouble();

            if (RandInt(0, 1) > 0.5f)
                result *= -1;

            return (float)result;
        }

        public DeftEngine()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            graphics.PreferredBackBufferWidth = 1366;
            graphics.PreferredBackBufferHeight = 768;

            // Restrict mouse to stay within the window.
            Input.SetMaxMouseX(graphics.PreferredBackBufferWidth);
            Input.SetMaxMouseY(graphics.PreferredBackBufferHeight);

            Assets.content = Content;
            Assets.LoadAssets();
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

            IsMouseVisible = true;
            Mouse.WindowHandle = Window.Handle;

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
            ECSCore.spriteBatch = spriteBatch;

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Input.UpdateStates();
            this.gameTime = gameTime;

            // TODO: Add your update logic here
            ECSCore.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(bgColor);

            spriteBatch.Begin();

            // TODO: Add your drawing code here
            ECSCore.RunDisplaySystems(spriteBatch);
            ECSCore.RunUIDisplaySystems(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
