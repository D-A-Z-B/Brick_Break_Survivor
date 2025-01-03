using BBS.Combat;
using BBS.Players;
using UnityEngine;

namespace BBS
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private int damage = 5;
        [SerializeField] private float moveSpeed = 10f;

        private void Start()
        {
            Destroy(gameObject, 5f);
        }

        private void Update()
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                ActionData actionData = new ActionData(damage, transform);
                PlayerManager.Instance.Player.GetCompo<PlayerHealth>().ApplyDamage(actionData);
            }
            Destroy(gameObject);
        }
    }
}
