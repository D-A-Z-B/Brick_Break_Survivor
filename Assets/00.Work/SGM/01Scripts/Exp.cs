using DG.Tweening;
using System;
using UnityEngine;

namespace BBS
{
    public class Exp : MonoBehaviour
    {
        [SerializeField] private AnimationCurve easeCurve;
        [SerializeField] private LayerMask whatIsPlayer;
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float exp = 1f;
        private float initalY;
        private Vector3 dir;
        private bool isMove = false;

        private void Start()
        {
            initalY = transform.position.y;
            Floating();
        }

        private void Floating()
        {
            if (isMove) return;

            transform.DOMoveY(initalY + 1f, 2f).SetEase(easeCurve)
            .OnComplete(() => transform.DOMoveY(initalY, 2f).SetEase(easeCurve)
            .OnComplete(() => Floating()));
        }

        private void Update()
        {
            if(isMove)
                transform.position += dir * moveSpeed * Time.deltaTime;
        }

        public void MoveToPlayer(Transform playerTrm)
        {
            transform.DOKill();

            dir = (playerTrm.position - transform.position).normalized;
            isMove = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                LevelManager.Instance.AddExp(exp);
                Destroy(gameObject);
            }
        }
    }
}
