using SFML.Graphics;
using SFML.System;

namespace App
{
    class Mushroom
    {
        public float posX;
        public float posY;
        public Texture texture;
        public Sprite mushroomSprite = new Sprite();
        
        public Mushroom(float posX, float posY, Texture texture)
        {
            this.posX = posX;
            this.posY = posY;

            this.texture = texture;
            mushroomSprite.Texture = this.texture;
            mushroomSprite.Position = new Vector2f(posX, posY);
            //playerSprite.Scale = new Vector2f(500,500);
        }
    }
}