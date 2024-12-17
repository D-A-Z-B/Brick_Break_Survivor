using UnityEngine;

namespace BBS.Players {
    public class Bullet : MonoBehaviour {
        [SerializeField] private float speed = 5;
        [SerializeField] private LayerMask obstacleLayer;
	    private	Vector3	direction;


	    public void Setup(Vector3 direction) {
    		this.direction	= direction;
	    }   

	    private void Update() {
    		transform.position += direction * speed * Time.deltaTime;   

	    }   

	    private void OnCollisionEnter(Collision collision) {
    		direction = Vector3.Reflect(direction, collision.GetContact(0).normal);
	    }
    }
}
