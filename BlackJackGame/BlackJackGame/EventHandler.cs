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
    class EventHandler
    {
        KeyboardState oldkbstate = new KeyboardState();

        public void CheckState(Game1 game, Card[] player, ref int playerCards, ref bool turn, ref int playerScore)
        {
            bool includesAce = false; //keeps track of whether or not the hand contains 1 or more ace for scoring
            for (int i = 0; i < playerCards; i++)
            {
                playerScore = playerScore + player[i].Score();
                if (player[i].Score() == 1)
                {
                    includesAce = true; //check if the hand includes any aces
                }
            }

            if (playerScore >= 21)
            {
                turn = true; //if max score or bust change to deal turn
            }
            else
            {
                KeyboardState curkbstate = Keyboard.GetState();
                if (curkbstate.IsKeyDown(Keys.H) && !oldkbstate.IsKeyDown(Keys.H)) //press h for "hit me" and a new card is dealt if possible
                {
                    if (playerCards < 5)
                    {
                        Random random = new Random();
                        player[playerCards] = new Card(game, random.Next(0, 13), random.Next(0, 4));
                        if (player[playerCards].Score() == 1)
                        {
                            includesAce = true; //check for aces
                        }
                        playerCards++;
                    }
                }

                if (curkbstate.IsKeyDown(Keys.S) && !oldkbstate.IsKeyDown(Keys.S)) //press S to stick on current hand
                {
                    turn = true;
                    if (includesAce == true && playerScore < 12)
                    {
                        playerScore = playerScore + 10; //choose ace as high to maximise the players score if possible
                    }
                }

                oldkbstate = curkbstate; //update keypress state
            }
        }
    }
}
