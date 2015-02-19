using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Ts
{
    class GameplayState : IGameState
    {
        private Game1 game;
        private GameStateManager gameStateManager;
        private List<IDrawable> drawables;
        private Field field;
        //reference to a class that manages the game logic of the gameplay
        private const int WIDTH = 10;
        private const int HEIGHT = 5;
        private const float FIELD_X = 10;
        private const float FIELD_Y = 10;
        private const string BACKGROUND_TEXTURE = "border";
        private const string BLOCK_TEXTURE = "blue";

        private FieldBackgroundBuilder staticGameObjectBuilder;
        private FieldBlocksBuilder dynamicGameObjectBuilder;

        public GameplayState(Game1 game, GameStateManager gameStateManager)
        {
            this.game = game;
            this.gameStateManager = gameStateManager;
            
            Texture2D texture = game.Content.Load<Texture2D>(BACKGROUND_TEXTURE);
            Texture2D blockTexture = game.Content.Load<Texture2D>(BLOCK_TEXTURE);
            TextureAtlas atlas = new TextureAtlas(texture);
            TextureAtlas blockAtlas = new TextureAtlas(blockTexture);
            int width = 48;
            int height = 48;
            atlas.Width = width + 2; atlas.Height = height + 2;
            blockAtlas.Width = width; blockAtlas.Height = height;

            int size = 50;
            Vector2 fieldPosition = new Vector2(FIELD_X, FIELD_Y);
            field = new Field(WIDTH, HEIGHT, fieldPosition, size);

            dynamicGameObjectBuilder = new FieldBlocksBuilder(field, blockAtlas);

            staticGameObjectBuilder = new FieldBackgroundBuilder(field, atlas);
            List<GameObject> staticFieldGameObjects = staticGameObjectBuilder.CreateGameObjects();

            drawables = new List<IDrawable>();
            foreach (GameObject gameObject in staticFieldGameObjects)
            {
                IDrawable drawable = gameObject.DrawableComponent.CreateDrawable();
                drawables.Add(drawable);
            }
        }

        public void Update(GameTime gameTime)
        {
            // call update of the game logic class 
            if (InputManager.KeyPressed(Keys.Left))
                field.MoveLeft();
        }

        public void Draw(GameTime gameTime)
        {
            // get a new list of drawables if the state of the game changed
            // get a reference to render manager from game class and draw all darawables
            // draw all ui elements, like highscore and next tile

            List<IDrawable> dynamicDrawables = new List<IDrawable>();
            List<GameObject> dynamicGameObjects = dynamicGameObjectBuilder.CreateGameObjects();

            foreach (var gameObject in dynamicGameObjects)
            {
                IDrawable drawable = gameObject.DrawableComponent.CreateDrawable();
                dynamicDrawables.Add(drawable);
            }
            game.RenderManager.Draw(dynamicDrawables);

            game.RenderManager.Draw(drawables);
        }
    }
}
