using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace BlackJackGame
{
    public class Card
    {

        Texture2D imagefile;
        protected Game1 game;
        int x, y;
        //public int id { get { return x; } set { x = value + 1; } }
        Rectangle position;

        public Card(Game1 game, int x, int y)
        {
            this.game = game;
            this.x = x;
            this.y = y;
            LoadImage();
        }

        public int Score()
        {
            if (x + 1 > 10)
            {
                return 10; //calculate the score of the card using the x value selected from the cards image
            }
            else
            {
                return x + 1;
            }
        }

        protected void LoadImage()
        {
            imagefile = game.Content.Load<Texture2D>("playing-card-deck");
        }

        public void DrawPiece(SpriteBatch spriteBatch, int width, int i, int player) //selects the relavant card from the image and displays it 
        {
            position = new Rectangle(2 * i * width / 10, width / 10 * player, 2 * width / 10, width * 3 / 10);
            spriteBatch.Draw(imagefile, position, new Rectangle(x * imagefile.Width / 13, y * imagefile.Height / 4, imagefile.Width / 13, imagefile.Height / 4), Color.White);
        }
    }
}
