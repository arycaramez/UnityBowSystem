using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BowSystemLib.Arrow
{
    [RequireComponent(typeof(ArrowCollision))]
    public class ArrowGrip : MonoBehaviour
    {
        [SerializeField] private Vector3 offset;
        private GameObject _grip;
        private bool active;

        private void Update()
        {
            if (active){
                transform.position = _grip.transform.TransformPoint(offset);
                transform.rotation = _grip.transform.rotation;
            }
        }

        public void ApplyGrip(ArrowCollision arrowCollision,Vector3 posArrow, Quaternion rotArrow, GameObject other)
        {
            if (!active)
            {
                arrowCollision.DisableCollision();

                _grip = new GameObject("ArrowAnchor");
                _grip.transform.position = posArrow;
                _grip.transform.rotation = rotArrow;
                _grip.transform.parent = other.transform;
                active = true;
            }
        }
        private void OnDestroy()
        {
            Destroy(_grip);
        }
    }
}
