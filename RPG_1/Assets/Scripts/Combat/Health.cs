using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
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
            
        }
    }
}
