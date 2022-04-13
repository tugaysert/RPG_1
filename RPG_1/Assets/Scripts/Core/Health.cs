using RPG.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable
    {
        bool isDead = false;
        [SerializeField] float healthPoint = 100f;

     
        public bool IsDead()
        {
            return isDead;  
        }

 

        public void TakeDamage(float damage)
        {
            healthPoint = Mathf.Max(healthPoint - damage, 0);
            print(healthPoint);

            if (healthPoint == 0)
            {
               
                Die();

            }
        }


        
        private void Die()
        {
            if (isDead) return;
            isDead = true;
            gameObject.GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
            
        }

        //ISaveable override for Health.cs
        public object CaptureState()
        {
            return healthPoint;
        }
        public void RestoreState(object state)
        {
            healthPoint = (float)state;

            if (healthPoint <= 0)
            {
                Die();
            }

        }
    }
}
