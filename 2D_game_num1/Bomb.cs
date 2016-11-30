using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2D_game_num1
{
    class Bomb
    {
        Vector2 bombLocation = new Vector2(-100, -100);
        Enemy enemy;
        Player player;
        bool active = false;

        public Bomb(Enemy enemy)
        {
            this.enemy = enemy;
        }
        public Bomb(Player player)
        {
            this.player = player;
        }

        // Sets the spawn points
        public void SetBombLocation_withEnemyLocation()
        {
            bombLocation.X = enemy.GetLocationX();
            bombLocation.Y = enemy.GetLocationY();
        }
        public void SetBombLocation_withPlayerLocation()
        {
            bombLocation.X = player.GetLocationX();
            bombLocation.Y = player.GetLocationY();
        }

        // Sets the bombs to just drop down in place
        public void BombFreeFall()
        {
            if (enemy.GetHealth() >= 1)
            {
                bombLocation.Y += 2.5f;
            }
            else
            {
                bombLocation.X = 2000;
                bombLocation.Y = 2000;
            }
        }
        // Sets the missile to fire up in place
        public void MissileFireUp()
        {
            bombLocation.Y -= 2.5f;
        }
        // Sets it so the bomb is alive and active
        public void ActivateBomb()
        {
            active = true;
        }
        // Kills the bomb to get rid of it
        public void DeactivateBomb()
        {
            active = false;
            bombLocation.Y = -300;
        }
        // just in case we want to know the status 
        public bool GetState()
        {
            return active;
        }

        // Location getters
        public float GetLocationX()
        {
            return bombLocation.X;
        }
        public float GetLocationY()
        {
            return bombLocation.Y;
        }
        public Vector2 GetLocation()
        {
            return bombLocation;
        }



    }
}
