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

            return gameObjects;
        }
    }
}
