using BBS.Core;
using BBS.Enemies;
using UnityEngine;

namespace BBS.Bullets {
    public abstract class Bullet : MonoBehaviour, IPoolable {
		[field: SerializeField] public BulletDataSO dataSO {get; private set;}
		[field: SerializeField] public int maxLevel {get; private set;}
	    protected Vector3 direction;

        public PoolTypeSO PoolType {get; set;}

        public GameObject GameObject => gameObject;

		protected Pool myPool;

		protected bool isCollision = false;
        protected float lastCollisionTime;
        protected float startTime;

		protected virtual void Awake() {
			
		}

        public virtual void Setup(Vector3 position, Vector3 direction) {
    		this.direction	= direction;
			transform.position = position;
			startTime = Time.time;
	    }

		private void OnEnable() {
			GameManager.Instance.AddBullet(this);
		}

		private void OnDisable() {
			GameManager.Instance.RemoveBullet(this);
		}

	    protected virtual void Update() {
    		transform.position += direction * dataSO.currentSpeed * Time.deltaTime;
	    }   

	    protected virtual void OnCollisionEnter(Collision collision) {
			GameManager.Instance.IncreaseHitCount();
			if (TryGetComponent<Enemy>(out Enemy enemy)) {
				enemy.GetCompo<EnemyHealth>().ApplyDamage(new Combat.ActionData((int)dataSO.currentDamage));
			}
			isCollision = true;
			lastCollisionTime = Time.time;
    		direction = Vector3.Reflect(direction, collision.GetContact(0).normal);
	    }

        public virtual void SetUpPool(Pool pool)
        {
			myPool = pool;
        }

        public virtual void ResetItem()
        {
			dataSO.currentSpeed = dataSO.defaultSpeed;
			isCollision = false;
			transform.localScale = new Vector3(dataSO.currentScale, dataSO.currentScale, dataSO.currentScale);
        }
    }
}
