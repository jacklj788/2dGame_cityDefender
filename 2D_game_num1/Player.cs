using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2D_game_num1
{
    class Player
    {
        int health;
        int score;
        int fuel;
        string name;
        Vector2 player_location = new Vector2(50, 400);
        Vector2 player_speed = new Vector2(3, 2);

        public Player(string name)
        {
            this.health = 100;
            this.score = 0;
            this.fuel = 100;
            this.name = name;
        }

        // We just use player_speed as an easier way to manage how fast it is going.
        // Otherwise we would need to do player_location.Y += 2; and then we would have to change it 
        // in each lines it's used. It's just easier this way. 
        public void MovePlayerLeft()
        {
            player_location.X -= player_speed.X;
        }
        public void MovePlayerRight()
        {
            player_location.X += player_speed.X;
        }
        public float GetLocationX()
        {
            return player_location.X;
        }
        public float GetLocationY()
        {
            return player_location.Y;
        }
        public void SetPlayerX(float x)
        {
            player_location.X = x;
        }
        public void SetPlayerY(float y)
        {
            player_location.Y = y;
        }
        public Vector2 GetLocation()
        {
            return player_location;
        }
    }
}
