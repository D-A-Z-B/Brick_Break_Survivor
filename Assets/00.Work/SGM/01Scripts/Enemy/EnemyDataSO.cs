using UnityEngine;

namespace BBS
{
    public enum EnemyType
    {
        Normal,
        Range,
        assassin
    }

    [CreateAssetMenu(fileName = "EnemyDataSO", menuName = "SO/Enemy/EnemyData")]
    public class EnemyDataSO : ScriptableObject
    {
        public EnemyType type;
        public int maxHealth;
        public int actionTurn; //몇 턴마다
        public int moveDistance; //얼마나 움직이냐
        public int damage;
        public int attakRange;
        public AnimationCurve ease;
    }
}
