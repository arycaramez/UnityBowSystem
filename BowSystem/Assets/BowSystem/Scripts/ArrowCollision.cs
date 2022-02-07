using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using BowSystemLib.Bow;

namespace BowSystemLib.Arrow {
    /// <summary>Custom arrow collision. controll all aspects of collision related of this projectile/arrow.</summary>
    [RequireComponent(typeof(ArrowInfo))]
    public class ArrowCollision : MonoBehaviour
    {
        public List<string> ignoreTags = new List<string>();
        public UnityEvent whenCollide;
        public Transform startPoint, endPoint;
        public float radius = 0.05f;
        public float waitTimeToDestroy = 3;
        public float waitTimeToDestroyAfterHit = 5;

        public Rigidbody GetRigidbody() { return GetComponent<Rigidbody>(); }

        public Vector3 lastPos { get; set; }

        void Update()
        {
            CollisionCtrl();
        }
        /// <summary>Detect the collision and ececute the relative next actions.</summary>
        virtual public void CollisionCtrl()
        {
            if (!GetRigidbody().isKinematic)
            {
                RaycastHit _hit1 = new RaycastHit();
                if (Physics.Linecast(lastPos, transform.position, out _hit1))
                {
                    if (CheckHit(_hit1, transform.rotation))
                    {
                        return;
                    }
                }
                lastPos = transform.position;

                Ray _ray = new Ray(endPoint.position, startPoint.position - endPoint.position);
                RaycastHit _hit = new RaycastHit();
                if (Physics.SphereCast(_ray, radius, out _hit, Vector3.Distance(endPoint.position, startPoint.position)))
                {
                    CheckHit(_hit, transform.rotation);
                }
            }
        }
        /// <summary>Check if the Arrow collide with a valid object in scene.</summary>
        /// <param name="hit"></param>
        /// <param name="lastRot"></param>
        /// <returns></returns>
        virtual public bool CheckHit(RaycastHit hit, Quaternion lastRot)
        {
            if (!GameObject.Equals(hit.collider.gameObject, gameObject) && !ignoreTags.Contains(hit.collider.gameObject.tag))
            {
                whenCollide.Invoke();
                HitTarget(hit);
                if (GetComponent<ArrowGrip>() is ArrowGrip arrowGrip && arrowGrip.enabled)
                {
                    arrowGrip.ApplyGrip(this, hit.point, lastRot, hit.collider.gameObject);
                    if (waitTimeToDestroy > 0) ExecDestroyAfterSeconds(waitTimeToDestroyAfterHit);
                }
                else
                {
                    Destroy(gameObject);
                }
                return true;
            }
            return false;
        }
        /// <summary>This function are executed after hit, return a "RaycastHit" and relative the collision.</summary>
        /// <param name="hit"></param>
        virtual public void HitTarget(RaycastHit hit) { }

        /// <summary>Disable Arrow collision.</summary>
        virtual public void DisableCollision()
        {
            Rigidbody rBody = GetRigidbody();
            rBody.velocity = Vector3.zero;
            rBody.isKinematic = true;
        }
        /// <summary>Enable arrow collision</summary>
        virtual public void EnableCollision()
        {
            GetRigidbody().isKinematic = false;
        }
        /// <summary>Instanciate the projectile object or Arrow.</summary>
        /// <param name="direction"></param>
        /// <param name="force"></param>
        virtual public void Shoot(Vector3 direction, BowInfo bowInfo)
        {
            EnableCollision();
            transform.rotation = Quaternion.LookRotation(direction);
            GetRigidbody().AddForce(direction * bowInfo.shootForce);
            if (waitTimeToDestroy > 0) ExecDestroyAfterSeconds(waitTimeToDestroy);
            lastPos = transform.position;
        }

        virtual public void ExecDestroyAfterSeconds(float waitTimeToDestroy)
        {
            if (cDestroyAfterSeconds != null) StopCoroutine(cDestroyAfterSeconds);
            cDestroyAfterSeconds = StartCoroutine("DestroyAfterSeconds", waitTimeToDestroy);
        }

        public Coroutine cDestroyAfterSeconds { get; set; }
        virtual public IEnumerator DestroyAfterSeconds(float waitTimeToDestroy)
        {
            yield return new WaitForSeconds(waitTimeToDestroy);
            Destroy(gameObject);
        }

        private void OnDrawGizmos()
        {
            if (startPoint && endPoint)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(startPoint.position, radius);
                Gizmos.DrawWireSphere(endPoint.position, radius);
                Gizmos.DrawLine(startPoint.position, endPoint.position);
            }
        }
    }
}
