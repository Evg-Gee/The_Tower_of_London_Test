using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class MoveManager : MonoBehaviour
    {
        private int currentMoves = 0;
        private int maxMoves;

        public void Initialize(int maxMoves)
        {
            this.maxMoves = maxMoves;
            currentMoves = 0;
            UIManager.Instance.UpdateMoveCounter(currentMoves, this.maxMoves);
        }

        public bool TryMove(Ring ring, IPole targetPole)
        {
            if (currentMoves >= maxMoves) return false;

            IPole currentPole = ring.GetCurrentPole() as IPole;
            if (currentPole != null && targetPole.CanAccept(ring))
            {
                currentPole.RemoveRing(ring);
                targetPole.AddRing(ring);
                AnimationManager.Instance.PlayRingPlacementAnimation(ring.placementEffect);
                currentMoves++;
                UIManager.Instance.UpdateMoveCounter(currentMoves, maxMoves);
                AudioManager.Instance.PlayMoveSound();
                if (GameController.Instance.levelManager.CheckWinCondition())
                {
                    GameController.Instance.CompleteLevel();
                    currentMoves = maxMoves;
                }

                if (currentMoves >= maxMoves && !GameController.Instance.levelManager.CheckWinCondition())
                {
                    Debug.Log("ShowGameOverPanel()");
                    UIManager.Instance.ShowGameOverPanel();
                }

                return true;
            }
            return false;
        }
        public bool IsOutOfMoves()
        {
            return currentMoves >= maxMoves;
        }
    }
}