using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BowSystemLib.Arrow
{
    public class ArrowInfo : MonoBehaviour
    {
        [HideInInspector] public string originalPrefabName;

        [SerializeField] float force = 10;
        [SerializeField] int secondsToDestroy = 10;
        [SerializeField] GameObject ignoreObj;
        [SerializeField] Rigidbody rBody;

        [SerializeField] List<Renderer> renderers = new List<Renderer>();

        public void Show(bool value)
        {
            foreach (Renderer e in renderers)
            {
                if (e.enabled != value) e.enabled = value;
            }
        }
        public void ActiveShoot(Vector3 direction) {
            rBody.isKinematic = false;
            if(secondsToDestroy > 0) Destroy(gameObject, secondsToDestroy);
            transform.rotation = Quaternion.LookRotation(direction);
            rBody.AddForce(direction * force);
        }

        private void OnTriggerEnter(Collider other)
        {
            DestroyImmediate(gameObject);
        }
    }

}