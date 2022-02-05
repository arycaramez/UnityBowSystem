using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BowSystemLib.Arrow
{
    [RequireComponent(typeof(ArrowCollision))]
    public class ArrowInfo : MonoBehaviour
    {
        private ArrowCollision arrowCollision;
        
        [HideInInspector] public string originalPrefabName;

        public List<Renderer> renderers = new List<Renderer>();

        private void Awake()
        {
            arrowCollision = GetComponent<ArrowCollision>();
        }

        virtual public void Show(bool value)
        {
            foreach (Renderer e in renderers)
            {
                if (e.enabled != value) e.enabled = value;
            }
        }

        virtual public void ActiveShoot(Vector3 direction,float force) {
            arrowCollision.Shoot(direction, force);
        }
    }
}