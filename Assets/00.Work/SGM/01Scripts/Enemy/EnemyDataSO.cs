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
        public int actionTurn; //�� �ϸ���
        public int moveDistance; //�󸶳� �����̳�
        public int damage;
        public int attakRange;
        public AnimationCurve ease;
    }
}
