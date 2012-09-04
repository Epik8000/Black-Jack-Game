using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace BlackJackGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {

        EventHandler eventHandler;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public Card[] player;
        public Card[] dealer; //define arrays to store max hands
        public int playerCards;
        public int dealerCards;
        Texture2D background;
        int width;
        bool turn;
        int playerScore;
        int dealerScore;
        Random random;
        bool gameover;
        int winner;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here 
            playerCards = 2;
            dealerCards = 2; 
            player = new Card[5];
            dealer = new Card[5];
            random = new Random();
            for (int i = 0; i < playerCards; i++)
            {
                
                player[i] = new Card(this, random.Next(0, 13), random.Next(0, 4)); //create initial hands, x value is suit, y value is card type
                dealer[i] = new Card(this, random.Next(0, 13), random.Next(0, 4));
            }
            this.IsMouseVisible = true;

            eventHandler = new EventHandler();
            base.Initialize();
            turn = false; //set first turn to player turn
            playerScore = 0;
            gameover = false; //if true the round is over
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("chess.board.fabric"); 

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        /// 

        protected int dealerTurn()
        {
            dealerScore = dealer[0].Score() + dealer[1].Score();
            Console.WriteLine(dealerScore);
            while (dealerScore < 16 && dealerCards < 5)
            {
                dealer[dealerCards] = new Card(this, random.Next(0, 13), random.Next(0, 4));
                dealerScore = dealerScore + dealer[dealerCards].Score();
                dealerCards++;
                Console.WriteLine(dealerScore);

            }
            if ((playerScore > dealerScore && playerScore < 22) || (playerScore < 22 && dealerScore > 21))
            {
                return 0;
            }
            else if (dealerScore < 22)
            {
                return 1;
            }
            else
            {
                return 2;
            }

        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (!gameover)
            {
                if (!turn)
                {
                    playerScore = 0;
                    eventHandler.CheckState(this, player, ref playerCards, ref turn, ref playerScore);
                }

                // TODO: Add your update logic here
                else
                {
                    winner = dealerTurn();
                    gameover = true;
                }
            }
            else
            {
                if (winner == 0)
                {
                    Console.WriteLine("Player Wins on" + playerScore);
                }
                else if (winner == 1)
                {
                    Console.WriteLine("Dealer Wins on" + dealerScore);
                }
                else if (winner == 2)
                {
                    Console.WriteLine("Draw");
                }
                System.Threading.Thread.Sleep(5000); //dealer waits 5 seconds then deals again
                Initialize();
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue); 
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);
            
            width = Math.Min(this.Window.ClientBounds.Width, this.Window.ClientBounds.Height);
            Rectangle pos = new Rectangle(0, 0, width, width);
            spriteBatch.Draw(background, pos, Color.Green); //draw background

            // TODO: Add your drawing code here

            for (int i = 0; i < playerCards; i++)
            {
                player[i].DrawPiece(spriteBatch, width, i, 5); //player 1 is referred to as 5 to set its position on the screen
            }

            for (int i = 0; i < dealerCards; i++)
            {
                dealer[i].DrawPiece(spriteBatch, width, i, 1); //dealer 1 is referred to as 1 to set its position on the screen
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
