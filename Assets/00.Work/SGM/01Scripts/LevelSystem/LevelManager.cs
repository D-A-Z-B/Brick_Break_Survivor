using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BBS
{
    public class LevelManager : MonoSingleton<LevelManager>
    {
        private List<Exp> expList;

        //test
        [SerializeField] private Transform playerTrm; 

        [SerializeField] private LevelPanelUI levelPanel;
        [SerializeField] private Transform expPrefab;
        [SerializeField] private float levelUpExpCalculation = 1;
        private int level = 0;
        private float curentExp = 0;
        private float needExp;

        protected override void Awake()
        {
            base.Awake();
            expList = new List<Exp>();
        }

        private void Start()
        {
            LevelUp();
        }

        private void Update()
        {
            //test
            if (Input.GetKeyDown(KeyCode.P))
            {
                GetExp(playerTrm);
            }
        }

        public void CreateExp(Vector3 pos)
        {
            Exp exp = Instantiate(expPrefab, transform).GetComponent<Exp>();
            exp.transform.position = pos;
            expList.Add(exp);
        }

        public void GetExp(Transform playerTrm)
        {
            expList.ForEach(exp =>
            {
                exp.MoveToPlayer(playerTrm);
            });
            expList.Clear();
        }

        public void AddExp(float exp)
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

            levelPanel.UpdateExpBarLevel(needExp, level);
        }
    }
}
