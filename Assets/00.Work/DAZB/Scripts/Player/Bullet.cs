using UnityEngine;

namespace BBS.Players {
    public class Bullet : MonoBehaviour, IPoolable {
        [SerializeField] private float speed = 5;
		[SerializeField] private float destroyTime; // 마지막으로 충돌한 시간부터 destroyTime이 지나면 삭제
	    private	Vector3	direction;
		private bool isCollision = false;

        public PoolTypeSO PoolType {get; set;}

        public GameObject GameObject => gameObject;

		private float lastCollisionTime;

		private Pool myPool;

        public void Setup(Vector3 position, Vector3 direction) {
    		this.direction	= direction;
			transform.position = position;
	    }   

	    private void Update() {
    		transform.position += direction * speed * Time.deltaTime;   
			
			if (isCollision == true && lastCollisionTime + destroyTime < Time.time) {
				myPool.Push(this);
			}
	    }   

	    private void OnCollisionEnter(Collision collision) {
			isCollision = true;
			lastCollisionTime = Time.time;
    		direction = Vector3.Reflect(direction, collision.GetContact(0).normal);
	    }

        public void SetUpPool(Pool pool)
        {
			myPool = pool;
        }

        public void ResetItem()
        {
			isCollision = false;
        }
    }
}
