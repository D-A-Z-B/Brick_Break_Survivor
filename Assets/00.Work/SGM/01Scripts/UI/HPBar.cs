using BBS.Combat;
using UnityEngine;
using UnityEngine.UI;

namespace BBS
{
    public class HPBar : MonoBehaviour
    {
        private Slider hpBar;
        private PlayerHealth health;

        private void Awake()
        {
            hpBar = GetComponent<Slider>();
            health = transform.root.GetComponent<PlayerHealth>();
        }

        private void Update()
        {
            hpBar.maxValue = health.MaxHealth;
            hpBar.value = health.CurrentHealth;
        }
    }
}
