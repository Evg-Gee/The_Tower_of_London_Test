using UnityEngine;

namespace Scripts
{
    public class Ring : MonoBehaviour
    {
        public Color ringColor;
        private Pole currentPole;
        private bool isDragging = false;
        private Vector3 offset;

        public ParticleSystem placementEffect; // Эффект установки кольца

        private void OnMouseDown()
        {
            if (GameController.Instance.IsOutOfMoves()) return;

            Pole pole = GetCurrentPole();
            if (pole != null && pole.GetTopRing() == this)
            {
                isDragging = true;
                offset = transform.position - GetMouseWorldPosition();
            }
        }

        private void OnMouseDrag()
        {
            if (isDragging)
            {
                transform.position = GetMouseWorldPosition() + offset;
            }
        }

        private void OnMouseUp()
        {
            if (isDragging)
            {
                isDragging = false;

                Pole targetPole = Pole.GetHoveredPole();

                if (targetPole != null)
                {
                    if (GameController.Instance.moveManager.TryMove(this, targetPole))
                    {
                        return;
                    }
                }

                Pole originalPole = GetCurrentPole();
                if (originalPole != null)
                {
                    originalPole.SetRingPosition(this);
                    placementEffect.Play();
                }
            }            
        }

        private Vector3 GetMouseWorldPosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.forward, Vector3.up);
            float distance;
            plane.Raycast(ray, out distance);
            return ray.GetPoint(distance);
        }

        public void SetCurrentPole(Pole pole)
        {
            currentPole = pole;
        }

        public Pole GetCurrentPole()
        {
            return currentPole;
        }
    }
}
