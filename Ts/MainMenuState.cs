using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Ts
{
    class MainMenuState : IGameState
    {
        Menu menu;
        Game1 game;
        DrawableComponent drawable;
        IDrawable sprite;

        Graphics g;
        Random r = new Random();

        public MainMenuState(Game1 game)
        {
            this.game = game;
            //string[] menuItems = { "Start", "Options", "Exit" };
            SpriteFont font = game.Content.Load<SpriteFont>("MenuItem");
            menu = new Menu(font);
            menu.MenuItems.Add(new MenuItem("Start"));
            menu.MenuItems.Add(new MenuItem("Options"));
            MenuItem exitMenuItem = new MenuItem("Exit");
            exitMenuItem.Click += exitMenuItem_Click;
            menu.MenuItems.Add(exitMenuItem);
            menu.Position = new Vector2(50, 50);

            g = new Graphics(game.GraphicsDevice);
            Texture2D texture = game.Content.Load<Texture2D>("sprite sheet");
            TextureAtlas atlas = new TextureAtlas(texture, 4, 3, new Position(0, 1));
            drawable = new DrawableComponent(atlas, new PositionComponent(new Vector2(150, 150)));
            sprite = drawable.CreateDrawable();
        }

        void exitMenuItem_Click(object sender, EventArgs e)
        {
            game.Exit();
        }

        public void Update(GameTime gameTime)
        {
            menu.Update();

            //if (InputManager.KeyPressed(Keys.Enter))
            //{
            //    sprite.Texture.CurrentFrame = new Position(r.Next() % 4, r.Next() % 3);
            //}
        }

        public void Draw(GameTime gameTime)
        {
            game.GraphicsDevice.Clear(Color.Black);

            menu.Draw(game.spriteBatch);

            sprite.Draw(g);
        }
    }
}
