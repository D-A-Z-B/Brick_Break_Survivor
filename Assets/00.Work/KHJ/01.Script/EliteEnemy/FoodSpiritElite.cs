using BBS.Combat;
using BBS.Players;
using KHJ.Core;
using UnityEngine;

namespace BBS.Enemies
{
    public class FoodSpiritElite : Enemy
    {
        [HideInInspector] public Player eatPlayer;
      public float forceDuration;
       public AnimationCurve forceEase;

        protected override void AfterInitialize()
        {
            base.AfterInitialize();
            GetComponent<EnemyHealth>().OnDead += () => ResultPanel.Instance.OnResultPanel(true);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Player player))
            {
                print("����");
                eatPlayer = player;
                SoundManager.Instance.PlaySFX("Boss_Eat");  
                mapManager.DestroyEntity(new Coord(eatPlayer.transform.position), player);
            }

            if (collision.gameObject.TryGetComponent(out Enemy enemy))
            {
                SoundManager.Instance.PlaySFX("Boss_Eat");
                mapManager.DestroyEntity(new Coord(enemy.transform.position), enemy, true);

                EnemyHealth health = GetCompo<Health>(true) as EnemyHealth;
                health.CurrentHealth += health.MaxHealth / 10;
            }
        }
    }
}
