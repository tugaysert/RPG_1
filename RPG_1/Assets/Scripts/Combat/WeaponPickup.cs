using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {

        [SerializeField] Weapon weapon;
      
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {     
                    other.gameObject.GetComponent<Fighter>().EquipWeapon(weapon);
                    Destroy(this.gameObject);
            }
        }
    }
}
