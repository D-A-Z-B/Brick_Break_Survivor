using BBS.Combat;
using BBS.Players;
using KHJ.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BBS.Enemies
{
    public class FoodSpiritElite : Enemy
    {
        [HideInInspector] public Player eatPlayer;
      public float forceDuration;
       public AnimationCurve forceEase;

       private bool isExe;

        protected override void AfterInitialize()
        {
            base.AfterInitialize();
            //GetCompo<Health>(true).OnDead += () => ResultPanel.Instance.OnResultPanel(true);

            isExe = false;
        }

        protected override void Update() {
            base.Update();

            if (isExe == false && GetCompo<Health>(true).CurrentHealth <= 0) {
                Time.timeScale = 0;
                ResultPanel.Instance.OnResultPanel(true);
                isExe = true;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Player player))
            {
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

                ChangeState("EAT");
            }
        }
    }
}
