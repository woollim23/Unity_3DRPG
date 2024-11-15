using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public int maxHealth;
    [SerializeField] public int health;
    public event Action OnDie;

    public bool IsDie = false;

    public bool IsDead => health == 0;

    private void Start()
    {
        health = maxHealth;
        IsDie = false;
    }

    public void TakeDamage(int damage)
    {
        if (health == 0) return;
        health = Mathf.Max(health - damage, 0);

        if (health == 0)
        {
            IsDie = true;
            OnDie?.Invoke();
        }
    }
}
