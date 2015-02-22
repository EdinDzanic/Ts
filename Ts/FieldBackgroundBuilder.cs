using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ts
{
    public class FieldBackgroundBuilder : IGameObjectBuilder
    {
        private Field field;
        private TextureAtlas texture;

        public FieldBackgroundBuilder(Field field, TextureAtlas texture)
        {
            this.field = field;
            this.texture = texture;
        }
        
        public List<GameObject> CreateGameObjects()
        {
            List<GameObject> gameObjects = new List<GameObject>();

            for (int i = 0; i < field.Width; i++)
            {
                for (int j = 0; j < field.Height; j++)
                {
                    GameObject gameObject = new GameObject();

                    PositionComponent positionComponent = new PositionComponent(field.Position.X + (field.CellSize + 1) * i,
                        field.Position.Y + (field.CellSize + 1) * j);

                    gameObject.PositionComponent = positionComponent;
                    gameObject.DrawableComponent = new DrawableComponent(texture, positionComponent);

                    gameObjects.Add(gameObject);
                }
            }

            for (int k = 0; k < field.Width - 1; k++)
            {
                GameObject gameObject = new GameObject();

                PositionComponent positionComponent = new PositionComponent(
                    field.Position.X + (field.CellSize + 1) * k + (field.CellSize/2),
                    field.Position.Y + (field.CellSize + 1) * (field.Height - 1) + field.CellSize);

                TextureAtlas halfTexture = new TextureAtlas(texture.Texture, 2, 1, new Position(1, 0));
                halfTexture.Height = texture.Height / 2;

                gameObject.PositionComponent = positionComponent;
                gameObject.DrawableComponent = new DrawableComponent(halfTexture, positionComponent);

                gameObjects.Add(gameObject);
            }

            return gameObjects;
        }
    }
}
