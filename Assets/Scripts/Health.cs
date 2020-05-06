using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100f;
        bool isDead = false;

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            //print(health);

            if (healthPoints == 0 && isDead == false)
            {
                GetComponent<Animator>().SetTrigger("die");
                isDead = true;
            }
        }
    }
}
