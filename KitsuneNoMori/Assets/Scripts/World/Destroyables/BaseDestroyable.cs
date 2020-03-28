using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.World.Destroyables
{
    public class BaseDestroyable : IDestroyable
    {
        private int healthMax = 1;
        private int currentHealth = 1;

        public void SetMaxHealth(int maxHealth)
        {
            healthMax = maxHealth;
        }

        public void SetCurrentHealth(int newCurrentHealth)
        {
            this.currentHealth = newCurrentHealth;
        }

        public virtual bool DropLoot()
        {
            if(currentHealth <= 0)
            {
                return true;
            }
            return false;
        }

        // for the future, implement damage system, so not every hit is just 1 dmg but the incoming dmg will be giving as argument
        public virtual void SendHit()
        {
            currentHealth--;
        }
    }
}
