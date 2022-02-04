using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BowSystemLib.Arrow
{
    public class ArrowInfo : MonoBehaviour
    {
        [HideInInspector] public string originalPrefabName;

        public float force = 10;
        public int secondsToDestroy = 10;
        public GameObject ignoreObj;
        public Rigidbody rBody;

        public List<Renderer> renderers = new List<Renderer>();

        virtual public void Show(bool value)
        {
            foreach (Renderer e in renderers)
            {
                if (e.enabled != value) e.enabled = value;
            }
        }

        virtual public void ActiveShoot(Vector3 direction) {
            rBody.isKinematic = false;
            if(secondsToDestroy > 0) Destroy(gameObject, secondsToDestroy);
            transform.rotation = Quaternion.LookRotation(direction);
            rBody.AddForce(direction * force);
        }
    }
}