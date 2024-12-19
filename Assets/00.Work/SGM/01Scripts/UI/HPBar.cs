using BBS.Combat;
using UnityEngine;
using UnityEngine.UI;

namespace BBS
{
    public class HPBar : MonoBehaviour
    {
        private PlayerHealth health;

        private void Awake()
        {
            health = GetComponentInParent<PlayerHealth>();
        }

        private void Update()
        {
            float curHealth = (float)health.CurrentHealth / (float)health.MaxHealth;
            transform.localScale = new Vector3(Mathf.Clamp(curHealth, 0, 1), 1, 1);
        }
    }
}
