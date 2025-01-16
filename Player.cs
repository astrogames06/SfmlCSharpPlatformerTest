using SFML.Graphics;
using SFML.System;

namespace App
{
    class Player
    {
        public float posX;
        public float posY;
        public Texture texture;
        public Sprite playerSprite = new Sprite();
        
        public Player(float posX, float posY, Texture texture)
        {
            this.posX = posX;
            this.posY = posY;

            this.texture = texture;
            playerSprite.Texture = this.texture;
            playerSprite.Position = new Vector2f(posX, posY);
            //playerSprite.Scale = new Vector2f(500,500);
        }
    }
}