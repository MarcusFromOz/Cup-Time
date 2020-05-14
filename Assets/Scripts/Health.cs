using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        //** Variables
        [SerializeField] float healthPoints = 100f;
        [SerializeField] float maxHealthPoints = 200f;

        bool isDead = false;
        //** Start and Update


        //** Public methods
        
        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            
            if (healthPoints == 0)
            {
                Die();
            }
        }

        public void HealDamage(float healAmount)
        {
            healthPoints = Mathf.Min(healthPoints + healAmount, maxHealthPoints);
        }


        //** Private methods
        private void Die()
        {
            if (isDead) {return;}

            isDead = true; 
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

    }
}
