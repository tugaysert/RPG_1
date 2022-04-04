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
        Transform target;
        private void Update()
        {
            if (target == null) return; 
            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.position);

            }
            else

            {
               GetComponent<Mover>().Cancel();
            }
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        //Fighter
        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Transform>();
            print("Take that you short, squat peasant!");
        }

        //Fighter.cs
        public void Cancel()
        {
            target = null;
        }
    }


}