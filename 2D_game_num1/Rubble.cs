using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2D_game_num1
{
    class Rubble
    {
        // it's static because we want it to continue on for each class
        // if it wasnt static, each class instance would use the same seed, meaning the same "random" numbers
        static Random rnd = new Random();
        Vector2 location = new Vector2();

        public Rubble()
        {
            // Makes it so each one starts at a random point
            location.X = rnd.Next(1, 700);
            // by being at different heights they will fall at different times (usually)
            location.Y = rnd.Next(-1000, -300);
            // This for some reason makes them be a little more spread apart. It's random but weirdly still close otherwise.
            // "Random" is what it actually is. - Ask about a better way to do this as a for statement seems a bit weird. But, it works. 
            for(int i = 0; i < 5; i++)
            {
                rnd.Next();
            }
        }

        public void RubbleFreeFall()
        {
            // Makes sure it doesnt keep falling forever, otherwise the game would eventually crash. 
            if (location.Y >= -1000)
            location.Y += 1;
            

        }

        public float GetLocationX()
        {
            return location.X;
        }
        public float GetLocationY()
        {
            return location.Y;
        }
        public Vector2 GetLocation()
        {
            return location;
        }

    }

}
