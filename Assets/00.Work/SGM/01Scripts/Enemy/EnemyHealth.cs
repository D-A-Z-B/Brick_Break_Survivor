using BBS.Combat;
using BBS.Entities;
using DG.Tweening;
using System.Drawing;
using TMPro;
using UnityEngine;
using Color = UnityEngine.Color;

namespace BBS.Enemies
{
    public class EnemyHealth : Health
    {
        Enemy enemy;

        [SerializeField] private TextMeshPro hpTxt;
        [SerializeField] protected MeshRenderer meshRender;

        public override void Initialize(Entity entity)
        {
            enemy = entity as Enemy;
        }

        private void Awake()
        {
            maxHealth = enemy.data.maxHealth;
        }

        private void Update()
        {
            hpTxt.text = currentHealth.ToString();
            RotateToCam();
            ColorHealth();
        }

        private void RotateToCam()
        {
            hpTxt.transform.rotation = Quaternion.Euler(90, 0, 0);
        }

        private void ColorHealth()
        {
            float hp = (float)currentHealth / maxHealth;
            Color color = meshRender.material.color;
            print(hp);
            color.a = ConvertIntToRange(hp);
            meshRender.material.color = color;
        }

        float ConvertIntToRange(float input)
        {
            float min = 0.3922f;
            float max = 1f;

            return Mathf.Lerp(min, max, input);
        }
    }
}
