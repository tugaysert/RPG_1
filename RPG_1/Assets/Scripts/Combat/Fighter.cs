using RPG.Core;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        
        [SerializeField] float timeBetweenAttacks = 1f;

        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] Weapon defaultWeapon = null;  
       
        Health target;
        public float timeSinceLastAttack = Mathf.Infinity;
        Weapon currentWeapon = null; 


        private void Start()
        {
            EquipWeapon(defaultWeapon);
        }

    

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null || target.IsDead()) return; 
         
            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position, 1f);
                
            }
            else

            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }


        public void EquipWeapon(Weapon weapon)
        {

            currentWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }



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

        //fighter.cs
        //ANIMATION EVENT
        void Hit()
        {
            if(target== null) return;   

            if(currentWeapon.HasProjectile())
            {
                currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target);
            }
            else
            {
                target.TakeDamage(currentWeapon.GetWeaponDamage());
            }         
        }
        void Shoot()
        {
            Hit();
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.GetWeaponRange();
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
            
        }

        public void Attack(GameObject combatTarget)
        {
            
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
            
        }

        
        public void Cancel()
        {
            StopAttack();

            target = null;
            //cinematic control remover cancel verdigi zaman eger combat icin yuruyorsa alttaki kod olmazsa durmaz.
            GetComponent<Mover>().Cancel();
        }

        private void StopAttack()
        {
            gameObject.GetComponent<Animator>().ResetTrigger("attack");
            gameObject.GetComponent<Animator>().SetTrigger("stopAttack");
        }

    }


}