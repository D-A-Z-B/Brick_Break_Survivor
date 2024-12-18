using UnityEngine;

namespace BBS.Players {
    public class Bullet : MonoBehaviour, IPoolable {
		[SerializeField] private PoolTypeSO poolType;
        [SerializeField] private float speed = 5;
		[SerializeField] private float destroyTime; // 마지막으로 충돌한 시간부터 destroyTime이 지나면 삭제
	    private	Vector3	direction;
		private bool isCollision = false;

        public PoolTypeSO PoolType => poolType;

        public GameObject GameObject => gameObject;

		private float lastCollisionTime;

        public void Setup(Vector3 direction) {
    		this.direction	= direction;
	    }   

	    private void Update() {
    		transform.position += direction * speed * Time.deltaTime;   
			
	    }   

	    private void OnCollisionEnter(Collision collision) {
			isCollision = true;
			lastCollisionTime = Time.time;
    		direction = Vector3.Reflect(direction, collision.GetContact(0).normal);
	    }

        public void SetUpPool(Pool pool)
        {

        }

        public void ResetItem()
        {
			isCollision = false;
        }
    }
}
