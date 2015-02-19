using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ts
{
    public class FieldBlocksBuilder : IGameObjectBuilder
    {
        private Field field;
        private TextureAtlas texture;

        public FieldBlocksBuilder(Field field, TextureAtlas texture)
        {
            this.field = field;
            this.texture = texture;
        }

        public List<GameObject> CreateGameObjects()
        {
            List<GameObject> gameObjects = new List<GameObject>();

            for (int row = 0; row < field.Grid.Count; row++)
            {
                for (int column = 0; column < field.Grid[row].Count; column++)
                {
                    if (field.Grid[row][column].Value != 0)
                    {
                        GameObject gameObject = new GameObject();

                        float X = field.Grid[row][column].X;
                        float Y = field.Grid[row][column].Y;

                        PositionComponent positionComponent = new PositionComponent(X, Y);

                        gameObject.PositionComponent = positionComponent;
                        gameObject.DrawableComponent = new DrawableComponent(texture, positionComponent);

                        gameObjects.Add(gameObject);
                    }
                }
            }

            return gameObjects;
        }
    }
}
