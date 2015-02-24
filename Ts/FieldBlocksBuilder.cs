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
        private Dictionary<int, Position> colorMap;

        public FieldBlocksBuilder(Field field, TextureAtlas texture)
        {
            this.field = field;
            this.texture = texture;

            colorMap = new Dictionary<int, Position>();
            colorMap[1] = new Position(0, 0);
            colorMap[2] = new Position(0, 1);
            colorMap[3] = new Position(1, 0);
            colorMap[4] = new Position(1, 1);
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

                        TextureAtlas blockTexture = new TextureAtlas(texture.Texture, 2, 2);
                        blockTexture.Height = texture.Height;
                        blockTexture.Width = texture.Width;
                        int blockColor = field.Grid[row][column].Value;
                        blockTexture.CurrentFrame = colorMap[blockColor];
                        gameObject.DrawableComponent = new DrawableComponent(blockTexture, positionComponent);

                        gameObjects.Add(gameObject);
                    }
                }
            }

            return gameObjects;
        }
    }
}
