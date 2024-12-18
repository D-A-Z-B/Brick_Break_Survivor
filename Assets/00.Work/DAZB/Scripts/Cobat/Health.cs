using System;
using BBS.Entities;
using BBS.Players;
using UnityEngine;

namespace BBS.Combat {
    public abstract class Health : MonoBehaviour, IEntityComponent, IDamageable {
        [SerializeField] protected int maxHealth;
        [SerializeField] protected int currentHealth;

        public event Action OnDead;
        public event Action OnHit;

        protected Player player;

        public virtual int CurrentHealth {
            get=> currentHealth;
            set {
                currentHealth = value;

                if (currentHealth < 0) {
                    currentHealth = 0;
                }
                else if (currentHealth > maxHealth) {
                    currentHealth = maxHealth;
                }
            }
        }

        public void Initialize(Entity entity)
        {
            player = entity as Player;
        }

        private void Start() {
            currentHealth = maxHealth;
        }

        public virtual void ApplyDamage(ActionData data) { 
            currentHealth -= data.damage;

            OnHit?.Invoke();

            if (currentHealth <= 0 && player.IsDead == false) {
                Debug.Log("주금");
                OnDead?.Invoke();
            }
        }
    }
}
