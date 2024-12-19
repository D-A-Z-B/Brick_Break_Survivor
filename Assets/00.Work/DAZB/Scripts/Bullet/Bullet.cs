using BBS.Combat;
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

		private bool isForcePush;

		protected virtual void Awake() {
			
		}

        public virtual void Setup(Vector3 position, Vector3 direction) {
    		this.direction	= direction;
			transform.position = position;
			startTime = Time.time;
	    }

		protected virtual void OnEnable() {
			isForcePush = false;
			GameManager.Instance.AddBullet(this);
		}

		protected virtual void OnDisable() {
			if (isForcePush) return;
			GameManager.Instance.RemoveBullet(this);
		}

	    protected virtual void Update() {
			float speed = dataSO.currentSpeed * (GameManager.Instance.IsFever ? 2 : 1);
    		transform.position += direction * speed * Time.deltaTime;
	    }   

	    protected virtual void OnCollisionEnter(Collision collision) {
			SoundManager.Instance.PlaySFX("BounceSound");
			if (((1 << collision.gameObject.layer) & LayerMask.GetMask("Enemy")) != 0) {
				Enemy enemy = collision.gameObject.GetComponent<Enemy>();

				enemy.GetCompo<Health>(true).ApplyDamage(new Combat.ActionData((int)dataSO.currentDamage));

			    GameManager.Instance.IncreaseHitCount();
			}
			isCollision = true;
			lastCollisionTime = Time.time;
    		direction = Vector3.Reflect(direction, collision.GetContact(0).normal);
	    }

        public virtual void SetUpPool(Pool pool)
        {
			myPool = pool;
        }

		public void ForcePush() {
			isForcePush = true;
			myPool.Push(this);
		}

        public virtual void ResetItem()
        {
			dataSO.currentSpeed = dataSO.defaultSpeed;
			isCollision = false;
			transform.localScale = new Vector3(dataSO.currentScale, dataSO.currentScale, dataSO.currentScale);
        }
    }
}
