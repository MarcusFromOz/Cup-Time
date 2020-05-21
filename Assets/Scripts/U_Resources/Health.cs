﻿using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using System;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        //** Variables
        [SerializeField] float healthPoints = 100f;
        [SerializeField] float maxHealthPoints = 200f;

        bool isDead = false;

        //** Start and Update

        private void Start()
        {
            healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
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
