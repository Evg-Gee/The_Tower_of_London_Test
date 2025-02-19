using DG.Tweening;
using UnityEngine;

namespace Scripts
{
    public class RingMoveAnimation : BaseAnimation
    {
        private Transform ringTransform;
        private Vector3 targetPosition;
        private float duration;

        public RingMoveAnimation(Transform ringTransform, Vector3 targetPosition, float duration)
        {
            this.ringTransform = ringTransform;
            this.targetPosition = targetPosition;
            this.duration = duration;
        }

        public override void Play()
        {
            ringTransform.DOMove(targetPosition, duration).SetEase(Ease.Linear);
        }

        public override void Stop()
        {
            ringTransform.DOKill();
        }
    }
}