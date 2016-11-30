using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2D_game_num1
{
    class Enemy
    {
        int health;
        static int enemyCount;
        Vector2 enemy_location = new Vector2(50, 50);
        Vector2 enemy_speed = new Vector2(1, 2);
        float rightPoint, leftPoint;
        Player player = new Player("dummy");
        bool right = true;


        // put the float right & left within the paramaets.
        // That way I can set it up so every enemy has a different to and from path

        public Enemy(float leftPoint, float rightPoint, float enemy_location_X, float enemy_location_Y)
        {
            enemyCount = enemyCount + 1;
            health = 100;
            this.rightPoint = rightPoint;
            this.leftPoint = leftPoint;
            enemy_location.X = enemy_location_X;
            enemy_location.Y = enemy_location_Y;
        }

        public void UpdateLocation()
        {
            //Vector2 player_pos = player.GetLocation();
            //if (player_pos.X < 200)
            // Using the players location to figure out where the enemy should move
            if (health >= 1)
            {
                if (right)
                {
                    if (enemy_location.X == rightPoint)
                        right = false;
                    enemy_location.X += enemy_speed.X;
                }
                else
                {
                    if (enemy_location.X == leftPoint)
                        right = true;
                    enemy_location.X -= enemy_speed.X;
                }
            }
            else
            {
                enemy_location.X = 2000;
                enemy_location.Y = 2000;
            }
        }

        public void Kill()
        {
            health = 0;
            enemyCount = enemyCount - 1;
        }
        public float GetHealth()
        {
            return health;
        }

        static public float GetCount()
        {
            return enemyCount;
        }

        public float GetLocationX()
        {
            return enemy_location.X;
        }
        public float GetLocationY()
        {
            return enemy_location.Y;
        }

        public Vector2 GetLocation()
        {
            return enemy_location;
        }


    }
}
