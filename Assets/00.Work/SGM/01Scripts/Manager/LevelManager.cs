using BBS.UI.Skills;
using KHJ.Core;
using System.Collections.Generic;
using UnityEngine;

namespace BBS
{
    public class LevelManager : MonoSingleton<LevelManager>
    {
        private List<Exp> expList;

        [SerializeField] private LevelPanelUI levelPanel;
        [SerializeField] private SkillSelectionUI skillSelectionUI;
        [SerializeField] private Transform expPrefab;
        [SerializeField] private float levelUpExpCalculation = 1;
        private int level = 0;
        private float curentExp = 0;
        private float needExp;
        private int killCount = 0;

        private void Awake()
        {
            expList = new List<Exp>();
        }

        private void Start()
        {
            LevelUp();
        }

        public void CreateExp(Vector3 pos)
        {
            killCount++;
            Exp exp = Instantiate(expPrefab, transform).GetComponent<Exp>();
            exp.transform.position = pos;
            expList.Add(exp);
        }

        public void GetExp()
        {
            expList.ForEach(exp =>
            {
                if(exp != null)
                    exp.MoveToPlayer();
            });
            expList.Clear();
        }

        public void PlusExp(float exp)
        {
            curentExp += exp;

            if (curentExp >= needExp)
                LevelUp();
            else
                levelPanel.UpdateExp(curentExp);
        }

        private void LevelUp()
        {
            curentExp = 0;
            level++;
            needExp = Mathf.Pow(level * 50 / 49, 2.5f) * levelUpExpCalculation;
            skillSelectionUI.Open();
            levelPanel.UpdateExpBarLevel(needExp, level);
        }

        public int GetKillCount() => killCount;
    }
}
