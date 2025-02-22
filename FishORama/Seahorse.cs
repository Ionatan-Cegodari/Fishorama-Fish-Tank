﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FishORamaEngineLibrary;

namespace FishORama
{
    /// CLASS: OrangeFish - this class is structured as a FishORama engine Token class
    /// It contains all the elements required to draw a token to screen and update it (for movement etc)
    class Seahorse : IDraw
    {
        // CLASS VARIABLES
        // Variables hold the information for the class
        // NOTE - these variables must be present for the class to act as a TOKEN for the FishORama engine
        private string textureID;               // Holds a string to identify asset used for this token
        private float xPosition;                // Holds the X coordinate for token position on screen
        private float yPosition;                // Holds the Y coordinate for token position on screen
        private int xDirection;                 // Holds the direction the token is currently moving - X value should be either -1 (left) or 1 (right)
        private int yDirection;                 // Holds the direction the token is currently moving - Y value should be either -1 (down) or 1 (up)
        private Screen screen;                  // Holds a reference to the screen dimansions (width and height)
        private ITokenManager tokenManager;     // Holds a reference to the TokenManager - for access to ChickenLeg variable

        // *** ADD YOUR CLASS VARIABLES HERE *** 

        int xSpeed = 5;
        int ySpeed = 5;

        float initialYpos;
       


        Random rand;
        /// CONSTRUCTOR: OrangeFish Constructor
        /// The elements in the brackets are PARAMETERS, which will be covered later in the course
        public Seahorse(string pTextureID, float pXpos, float pYpos, Screen pScreen, ITokenManager pTokenManager)
        {
            // State initialisation (setup) for the object
            textureID = pTextureID;
            xPosition = pXpos;
            yPosition = pYpos;
            xDirection = 1;
            yDirection = 1;
            screen = pScreen;
            tokenManager = pTokenManager;
            
            // *** ADD OTHER INITIALISATION (class setup) CODE HERE ***


            rand = new Random();

            initialYpos = pYpos;
            
            xSpeed = rand.Next(2, 6);
            ySpeed = xSpeed / 2;

        }

        /// METHOD: Update - will be called repeatedly by the Update loop in Simulation
        /// Write the movement control code here
        public void Update()
    {
            // *** ADD YOUR MOVEMENT/BEHAVIOUR CODE HERE ***
            int width = screen.width / 2;
           
            if (xPosition > width)
            {
            xDirection *= -1;
            }
                                                   // left and right of the screen
            if (xPosition < -width)
            {
            xDirection = 1;
            }

            // From line 66-92 will make sure that the piranha will be bouncing of the aquarium walls and not going off the screen


            int height = screen.height / 2;

            if (yPosition > height)
            {
                yDirection = -1;
            }
                                                   // top and bottom of the screen
            if (yPosition < -height)
            {
                yDirection = 1;
            }


            xPosition += xSpeed * xDirection;
            yPosition += ySpeed * yDirection;


            if ((initialYpos + 50) < yPosition) //this will allow the seahorses to zig zag as it will go up to 50 pixels and then down as far as 50 pixels allowing a zig zag
            {
                yDirection *= -1;
            }

            if ((initialYpos - 50) > yPosition)
            {
                yDirection *= -1;
            }




        }

    /// METHOD: Draw - Called repeatedly by FishORama engine to draw token on screen
    /// DO NOT ALTER, and ensure this Draw method is in each token (fish) class
    /// Comments explain the code - read and try and understand each lines purpose
    public void Draw(IGetAsset pAssetManager, SpriteBatch pSpriteBatch)
        {
            Asset currentAsset = pAssetManager.GetAssetByID(textureID); // Get this token's asset from the AssetManager

            SpriteEffects horizontalDirection; // Stores whether the texture should be flipped horizontally

            if (xDirection < 0)
            {
                // If the token's horizontal direction is negative, draw it reversed
                horizontalDirection = SpriteEffects.FlipHorizontally;
            }
            else
            {
                // If the token's horizontal direction is positive, draw it regularly
                horizontalDirection = SpriteEffects.None;
            }

            // Draw an image centered at the token's position, using the associated texture / position
            pSpriteBatch.Draw(currentAsset.Texture,                                             // Texture
                              new Vector2(xPosition, yPosition * -1),                                // Position
                              null,                                                             // Source rectangle (null)
                              Color.White,                                                      // Background colour
                              0f,                                                               // Rotation (radians)
                              new Vector2(currentAsset.Size.X / 2, currentAsset.Size.Y / 2),    // Origin (places token position at centre of sprite)
                              new Vector2(1, 1),                                                // scale (resizes sprite)
                              horizontalDirection,                                              // Sprite effect (used to reverse image - see above)
                              1);                                                               // Layer depth
        }
    }
}
