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
        public int moveTurn; //�� �ϸ���
        public int moveDistance; //�󸶳� �����̳�
        public float damage;
        public int attakRange;
    }
}
