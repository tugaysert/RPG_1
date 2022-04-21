using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    [SerializeField] float arrowSpeed = 8.0f;

    Health target;
    float damage = 0;

    void Update()
    {
        if(target == null) return;  
        transform.LookAt(GetAimLocation());
        transform.Translate(Vector3.forward*Time.deltaTime* arrowSpeed);
    }

    public void SetTarget(Health target, float damage)
    {
        this.target = target;  
        this.damage = damage;
    }

    private Vector3 GetAimLocation()
    {
        CapsuleCollider capsuleCollider = target.GetComponent<CapsuleCollider>();
        if(capsuleCollider == null) return target.transform.position;
        return target.transform.position + Vector3.up * capsuleCollider.height / 2;
    }

    //projectile.cs
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Health>() != target) return;
        target.TakeDamage(damage);
        Destroy(gameObject);
      
    }

}
