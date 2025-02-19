using UnityEngine;

namespace Scripts
{
    public class SuccessAnimation : BaseAnimation
    {
         private GameObject successEffect;

        public SuccessAnimation(GameObject successEffect)
        {
            this.successEffect = successEffect;
        }

        public override void Play()
        {
            if (successEffect != null)
            {
                successEffect.SetActive(true);
                successEffect.GetComponent<ParticleSystem>().Play();
            }
        }

        public override void Stop()
        {
            if (successEffect != null)
            {
                successEffect.SetActive(false);
            }
        }
    }
}