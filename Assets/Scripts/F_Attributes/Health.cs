using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using System;
using GameDevTV.Utils;
using UnityEngine.Events;
using RPG.SceneManagement;
using System.Collections;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float regenerationPercentage = 70;
        [SerializeField] UnityEvent onDie;
        [SerializeField] UnityEvent onWin;

        public TakeDamageEvent takeDamage;
        
        //ToDo think about where this should be
        private int numberOfTrophies = 0;


        [System.Serializable]
        public class TakeDamageEvent : UnityEvent<float>
        {
        }

        LazyValue<float> healthPoints;

        bool isDead = false;

        private void Awake()
        {
            healthPoints = new LazyValue<float>(GetInitialHealth);
        }

        private float GetInitialHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        private void Start()
        {
            healthPoints.ForceInit();
        }

        private void OnEnable()
        {
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
        }

        private void OnDisable()
        {
            GetComponent<BaseStats>().onLevelUp -= RegenerateHealth;
        }
        
        private void RegenerateHealth()
        {
            float newHealthPoints = GetComponent<BaseStats>().GetStat(Stat.Health) * (regenerationPercentage/100);
            if (newHealthPoints > healthPoints.value)
                {
                    healthPoints.value = newHealthPoints;
                }
        }
                
        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            //print(gameObject.name + " took damage: " + damage);
            
            healthPoints.value = Mathf.Max(healthPoints.value - damage, 0);

            takeDamage.Invoke(damage);

            if (healthPoints.value == 0)
            {
            
                onDie.Invoke();
                Die();

                //only load End Screen if the player dies

                if (this.tag == "Player")
                {
                    StartCoroutine(LoadEndScreen(2));
                }
                else
                {
                    //Get XP from Basestats
                    AwardExperience(instigator);
                }
            }
        }

        public void IncrementTrophyCount()
        {
            //ToDo get this working and/or move it to somewhere more sensible
            numberOfTrophies++;

            //ToDo #Trophies and Scene number hardcoded for now 
            if (numberOfTrophies == 1)
            {
                GetComponent<Animator>().SetTrigger("win");
                onWin.Invoke();
                StartCoroutine(LoadEndScreen(3)); 
            }
        }

        public float GetHealthPoints()
        {
            return healthPoints.value; 
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
            return 100 * GetFraction();
        }

        public float GetFraction()
        {
            return healthPoints.value / GetComponent<BaseStats>().GetStat(Stat.Health);
        }


        public void HealDamage(float healAmount)
        {
            healthPoints.value = Mathf.Min(healthPoints.value + healAmount, GetMaxHealthPoints());
        }
        
        private void Die()
        {
            if (isDead) { return; }
            
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
            
        }

        IEnumerator LoadEndScreen(int delay)
        {
            yield return new WaitForSeconds(delay);
            GetComponent<Portal>().LoadClosingScene();
        }

        public object CaptureState()
        {
            return healthPoints.value;
        }

        public void RestoreState(object state)
        {
            float storedHealth = (float)state;
            healthPoints.value = storedHealth;

            if (healthPoints.value == 0)
            {
                Die();
            }
        }

    }
}
