using Data;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class Pole : MonoBehaviour, IPole
    {
        [SerializeField] private Transform[] ringPositions; 

        public List<Ring> rings = new();
        public PoleTargetState targetState;                
        public int maxCapacity;                           

        public bool CanAccept(Ring ring)
        {
            if (rings.Count >= maxCapacity) return false;

            if (rings.Count == 0) return true;
            return true;
        }

        public void AddRing(Ring ring)
        {
            if (CanAccept(ring))
            {
                rings.Add(ring);
                ring.transform.position = GetRingPosition(rings.Count - 1);
                ring.SetCurrentPole(this);                    
            }
        }

        public void RemoveRing(Ring ring)
        {
            rings.Remove(ring);
        }

        private Vector3 GetRingPosition(int index)
        {
            if (index >= 0 && index < ringPositions.Length)
            {
                return ringPositions[index].position;              
            }
            Debug.LogError("Index out of range for ring positions.");
            return transform.position;
        }

        public bool IsInTargetState()
        {
            if (targetState == null || rings.Count != targetState.colors.Count)
            {
                return false;
            }

            for (int i = 0; i < rings.Count; i++)
            {
                if (rings[i].ringColor != targetState.colors[i])
                {
                    return false;
                }
            }

            return true;
        }

        public void SetRingPosition(Ring ring)
        {
            int index = rings.IndexOf(ring);
            if (index != -1)
            {
                ring.transform.position = GetRingPosition(index);
            }
        }

        public static Pole GetHoveredPole()
        {
            int poleLayer = LayerMask.NameToLayer("Pole");
            int layerMask = 1 << poleLayer;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                return hit.collider?.GetComponent<Pole>();
            }

            return null;
        }

        public Ring GetTopRing()
        {
            return rings.Count > 0 ? rings[^1] : null;
        }

        public void Clear()
        {
            foreach (var ring in rings)
            {
                if (ring != null)
                {
                    Destroy(ring.gameObject);
                }
            }
            rings.Clear();
        }

        public void SetTargetState(PoleTargetState targetState)
        {
            this.targetState = targetState;
        }
    }
}