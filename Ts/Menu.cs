using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace Ts
{
    class Menu
    {
        #region Field and Property Region

        //string[] menuItems; //Replace later with List of MenuItems objects
        List<MenuItem> menuItems;
        int selectedIndex;
        SpriteFont spriteFont;

        public Vector2 Position { get; set; }
        public Color NormalColor { get; set; }
        public Color SelectedColor { get; set; }
        public List<MenuItem> MenuItems { get { return menuItems; } }
        public int SelectedIndex { get { return selectedIndex; } }

        #endregion

        #region Constructor Region

        public Menu(SpriteFont spriteFont)
        {
            this.spriteFont = spriteFont;
            menuItems = new List<MenuItem>();
            selectedIndex = 0;
            NormalColor = Color.Red;
            SelectedColor = Color.Blue;
        }

        #endregion

        #region Methods
        
        //public void SetMenuItems(string[] items)
        //{
        //    menuItems = (string[])items.Clone();
        //}

        public void Update() 
        {
            if(InputManager.KeyReleased(Keys.Down))
            {
                selectedIndex++;

                if (selectedIndex >= MenuItems.Count) 
                {
                    selectedIndex = 0;
                }
            }

            if (InputManager.KeyReleased(Keys.Up))
            {
                selectedIndex--;
                if (selectedIndex < 0)
                {
                    selectedIndex = MenuItems.Count - 1;
                }
            }

            if (InputManager.KeyReleased(Keys.Enter))
            {
                MenuItems[selectedIndex].PerformClick();
            }
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            Vector2 menuPosition = Position;
            for (int i = 0; i < MenuItems.Count; i++)
            {
                if (i == selectedIndex)
                    spriteBatch.DrawString(spriteFont, MenuItems[i].Text, menuPosition, SelectedColor);
                else
                    spriteBatch.DrawString(spriteFont, MenuItems[i].Text, menuPosition, NormalColor);
                menuPosition.Y += spriteFont.LineSpacing;
            }
        }

        #endregion
    }
}
