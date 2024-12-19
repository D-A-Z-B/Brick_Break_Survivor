using System;
using BBS.Entities;
using BBS.Players;
using UnityEngine;

namespace BBS.Combat
{
    public abstract class Health : MonoBehaviour, IEntityComponent, IDamageable
    {
        [SerializeField] protected int maxHealth;
        [SerializeField] protected int currentHealth;

        public event Action OnDead;
        public event Action OnHit;

        public virtual int CurrentHealth
        {
            get => currentHealth;
            set
            {
                currentHealth = value;

                if (currentHealth < 0)
                {
                    currentHealth = 0;
                }
                else if (currentHealth > maxHealth)
                {
                    currentHealth = maxHealth;
                }
            }
        }

        public virtual int MaxHealth {
            get => maxHealth;
            set => maxHealth = value;
        }

        private int dr = 0;
        public int Dr {
            get => dr;
            set {
                dr = value;
                if (dr < 0) dr = 0;
            }
        }

        public abstract void Initialize(Entity entity);

        private void Start()
        {
            currentHealth = maxHealth;
        }

        public virtual void ApplyDamage(ActionData data)
        {
            currentHealth -= (int)(data.damage - ((float)data.damage * (dr / 100)));

            OnHit?.Invoke();

            if (currentHealth <= 0)
            {
                OnDead?.Invoke();
            }
        }
    }
}
