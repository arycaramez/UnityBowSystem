using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BowSystemLib.Arrow {
    [RequireComponent(typeof(ArrowInfo))]
    public class ArrowCollision : MonoBehaviour
    {
        public List<string> ignoreTags = new List<string>();
        [SerializeField] private UnityEvent whenCollide;
        [SerializeField] private Transform startPoint,endPoint;
        [SerializeField] private float radius = 0.05f;
        [SerializeField] private float waitTimeToDestroy = 3;
        [SerializeField] private float waitTimeToDestroyAfterHit = 5;

        private Vector3 lastPos;

        public Rigidbody GetRigidbody() { return GetComponent<Rigidbody>(); }

        private void Update()
        {
            if (!GetRigidbody().isKinematic) {
                RaycastHit _hit1 = new RaycastHit();
                if (Physics.Linecast(lastPos,transform.position,out _hit1)) {
                    if (CheckHit(_hit1.collider.gameObject, _hit1.point, transform.rotation)) {
                        return;
                    }
                }
                lastPos = transform.position;

                Ray _ray = new Ray(endPoint.position, startPoint.position - endPoint.position);
                RaycastHit _hit = new RaycastHit();
                if (Physics.SphereCast(_ray, radius, out _hit, Vector3.Distance(endPoint.position, startPoint.position)))
                {
                    CheckHit(_hit.collider.gameObject,_hit.point,transform.rotation);
                }
            }
        }

        private bool CheckHit(GameObject other,Vector3 hitPos,Quaternion lastRot) {
            if (!GameObject.Equals(other, gameObject) && !ignoreTags.Contains(other.tag)) 
            {
                whenCollide.Invoke();
                if (GetComponent<ArrowGrip>() is ArrowGrip arrowGrip && arrowGrip.enabled) {
                    arrowGrip.ApplyGrip(this,hitPos, lastRot, other);
                    if (waitTimeToDestroy > 0) ExecDestroyAfterSeconds(waitTimeToDestroyAfterHit);
                } else {
                    Destroy(gameObject);
                }
                return true;
            }
            return false;
        }

        public void DisableCollision() {
            Rigidbody rBody = GetRigidbody();
            rBody.velocity = Vector3.zero;
            rBody.isKinematic = true;
        }

        public void EnableCollision() {
            GetRigidbody().isKinematic = false;
        }

        public void Shoot(Vector3 direction, float force) {
            EnableCollision();
            transform.rotation = Quaternion.LookRotation(direction);
            GetRigidbody().AddForce(direction * force);
            if(waitTimeToDestroy > 0) ExecDestroyAfterSeconds(waitTimeToDestroy);
            lastPos = transform.position;
        }

        private void ExecDestroyAfterSeconds(float waitTimeToDestroy) {
            if (cDestroyAfterSeconds != null) StopCoroutine(cDestroyAfterSeconds);
            cDestroyAfterSeconds = StartCoroutine("DestroyAfterSeconds", waitTimeToDestroy);
        }

        private Coroutine cDestroyAfterSeconds;
        private IEnumerator DestroyAfterSeconds(float waitTimeToDestroy) {
            yield return new WaitForSeconds(waitTimeToDestroy);
            Destroy(gameObject);
        }

        private void OnDrawGizmos()
        {
            if (startPoint && endPoint) {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(startPoint.position, radius);
                Gizmos.DrawWireSphere(endPoint.position, radius);
                Gizmos.DrawLine(startPoint.position, endPoint.position);
            }
        }
    }
}
