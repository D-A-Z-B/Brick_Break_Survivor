using UnityEngine;

namespace BBS.Bullets {
    public abstract class Bullet : MonoBehaviour, IPoolable {
        [SerializeField] protected float speed = 5;
	    protected Vector3 direction;

        public PoolTypeSO PoolType {get; set;}

        public GameObject GameObject => gameObject;

		protected Pool myPool;

        public virtual void Setup(Vector3 position, Vector3 direction) {
    		this.direction	= direction;
			transform.position = position;
	    }   

	    protected virtual void Update() {
    		transform.position += direction * speed * Time.deltaTime;   
			
	    }   

	    protected virtual void OnCollisionEnter(Collision collision) {
    		direction = Vector3.Reflect(direction, collision.GetContact(0).normal);
	    }

        public virtual void SetUpPool(Pool pool)
        {
			myPool = pool;
        }

        public virtual void ResetItem()
        {
			
        }
    }
}
