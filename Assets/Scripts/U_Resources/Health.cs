using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using System;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        //** Variables
        float healthPoints = -1f;
        [SerializeField] float maxHealthPoints = 200f;
        [SerializeField] float regenerationPercentage = 70;
        bool isDead = false;

        //** Start and Update

        private void Start()
        {
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth;

            if (healthPoints < 0)
            {
                healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
            }
        }

        private void RegenerateHealth()
        {
            float newHealthPoints = GetComponent<BaseStats>().GetStat(Stat.Health) * (regenerationPercentage/100);
            if (newHealthPoints > healthPoints)
                {
                    healthPoints = newHealthPoints;
                }
        }

        //** Public methods

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            
            if (healthPoints == 0)
            {
                Die();
                //Get XP from Basestats
                AwardExperience(instigator);
            }
        }

        public float GetHealthPoints()
        {
            return healthPoints; 
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }


        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;

            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }

        public float GetPercentage()
        {
            return 100 * (healthPoints / GetComponent<BaseStats>().GetStat(Stat.Health));
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

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            float storedHealth = (float)state;
            healthPoints = storedHealth;

            if (healthPoints == 0)
            {
                Die();
            }
        }

    }
}
