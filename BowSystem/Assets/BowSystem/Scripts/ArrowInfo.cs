using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BowSystemLib.Bow;

namespace BowSystemLib.Arrow
{
    [RequireComponent(typeof(ArrowCollision))]
    public class ArrowInfo : MonoBehaviour
    {
        private ArrowCollision arrowCollision;
        public string originalPrefabName { get; set; }
        /// <summary>Used to control renderer components.</summary>
        [SerializeField] private List<Renderer> renderers = new List<Renderer>();

        private void Awake()
        {
            arrowCollision = GetComponent<ArrowCollision>();
        }
        /// <summary>Show and Hide the renderer components of Arrow.</summary>
        /// <param name="value"></param>
        virtual public void SetRenderers(bool value) {
            foreach (Renderer e in renderers)
            {
                if (e.enabled != value) e.enabled = value;
            }
        }
        /// <summary>
        /// Instantiates a new arrow by enabling its physics including collision detection, then 
        /// applies a firing force towards the target, using the parameters given to this function.
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="bowInfo"></param>
        virtual public void ActiveShoot(Vector3 direction,BowInfo bowInfo) {
            arrowCollision.Shoot(direction, bowInfo);
        }
    }
}