using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Collider myCollider;

    private int damage;
    private float knockback;

    public int weaponLevel = 1;

    private List<Collider> alreadyColliderWith = new List<Collider>();

    private void OnEnable()
    {
        alreadyColliderWith.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other == myCollider) return;
        if (alreadyColliderWith.Contains(other)) return;

        alreadyColliderWith.Add(other);

        if (other.TryGetComponent(out Health health))
        {
            
            health.TakeDamage(damage);
        }

        if(other.TryGetComponent(out IDamagable damagable))
        {
            damagable.TakePhysicalDamage(damage);
        }

        if (other.TryGetComponent(out ForceReceiver forceReceiver))
        {
            Vector3 direction = (other.transform.position - myCollider.transform.position).normalized;
            forceReceiver.AddForce(direction * knockback);
        }
    }

    public void SetAttack(int damage, float knockback)
    {
        this.damage = damage * weaponLevel;
        this.knockback = knockback;
    }
}
