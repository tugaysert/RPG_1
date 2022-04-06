using RPG.Core;
using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 20.0f;

       
        Health target;

        public float timeSinceLastAttack = 0;
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null || target.IsDead()) return; 
         
            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position);
                
            }
            else

            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }
        //fighter.cs
        private void AttackBehaviour()
        {
            
            transform.LookAt(target.transform);
            
            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                TriggerAttack();
                timeSinceLastAttack = 0;

               

            }

        }
        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            //This will trigger the "Hit()" event.
            GetComponent<Animator>().SetTrigger("attack");
        }

        //ANIMATION EVENT
        void Hit()
        {
            if(target== null) return;   
            //target Health objesi field'?d?r.
            target.TakeDamage(weaponDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public bool CanAttack(CombatTarget combatTarget)
        {
            if (combatTarget == null) return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
            
        }

        public void Attack(CombatTarget combatTarget)
        {
            
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
            
        }

        
        public void Cancel()
        {
            StopAttack();

            target = null;

        }

        private void StopAttack()
        {
            gameObject.GetComponent<Animator>().ResetTrigger("attack");
            gameObject.GetComponent<Animator>().SetTrigger("stopAttack");
        }

    }


}