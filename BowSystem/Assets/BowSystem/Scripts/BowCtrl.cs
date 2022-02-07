using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using BowSystemLib.Bow;
using BowSystemLib.Arrow;

namespace BowSystemLib
{
    public class BowCtrl : MonoBehaviour
    {
        [Header("References:")]
        public Transform rootPlayerObj;
        [Space(5)]
        public Transform keepBowAnchor;
        public Transform handBowAnchor;
        [Space(5)]
        public GameObject currentBow;
        [HideInInspector] public GameObject sceneBow;

        [Header("Settings:")]
        public bool inHand;
        public bool hide;

        [Space(10)]
        [SerializeField] private AudioSource audioSource;

        public ArrowCtrl GetArrowCtrlBase { get { return GetComponent<ArrowCtrl>(); } }

        private void OnDisable()
        {
            DestroyImmediate(sceneBow);
        }

        private void FixedUpdate()
        {
            SceneBowManager();
        }
        /// <summary></summary>
        virtual public void SceneBowManager()
        {
            Transform _anchor = GetBowAnchor();
            if (sceneBow == null && currentBow)
            {
                sceneBow = Instantiate(currentBow, _anchor.position, _anchor.rotation);
                BowInfo _sceneBowInfo = sceneBow.GetComponent<BowInfo>();
                _sceneBowInfo.originalPrefabName = currentBow.name;
                if (rootPlayerObj) sceneBow.transform.SetParent(rootPlayerObj);
            }
            else if (sceneBow && currentBow)
            {
                BowInfo _sceneBowInfo = sceneBow.GetComponent<BowInfo>();
                if (!_sceneBowInfo)
                {
                    DestroyImmediate(sceneBow);
                }

                if (_sceneBowInfo && _sceneBowInfo.originalPrefabName != currentBow.name)
                {
                    DestroyImmediate(sceneBow);
                    sceneBow = Instantiate(currentBow, _anchor.position, _anchor.rotation);
                    _sceneBowInfo = sceneBow.GetComponent<BowInfo>();
                    _sceneBowInfo.originalPrefabName = currentBow.name;
                    if (rootPlayerObj) sceneBow.transform.SetParent(rootPlayerObj);
                }
            }
            else if (currentBow == null && sceneBow)
            {
                DestroyImmediate(sceneBow);
            }
            // Checks if bow/crossbow it was supposed to be hiding or not 
            if (sceneBow)
            {
                BowInfo _sceneBowInfo = sceneBow.GetComponent<BowInfo>();
                _sceneBowInfo.ShowBow(!hide);
            }
            // Check if there is bow/crossbow, if true then apply "_anchor" position and rotation with it.
            if (sceneBow)
            {
                sceneBow.transform.position = _anchor.position;
                sceneBow.transform.rotation = _anchor.rotation;
            }
        }
        /// <summary>Return the current anchor to the Bow/Crossbow, it will use the position and rotation as a reference.</summary>
        /// <returns>Transform</returns>
        virtual public Transform GetBowAnchor()
        {
            return inHand ? handBowAnchor : keepBowAnchor;
        }
        /// <summary>Animation Trigger: Used to pick up the character's bow and place it in your hand.</summary>
        virtual public void TakeBow()
        {
            inHand = true;
        }
        /// <summary>Animation Trigger: Used to hold the character's bow.</summary>
        virtual public void KeepBow()
        {
            inHand = false;
        }
        /// <summary> Function responsible for shoot arrow. </summary>
        virtual public void ShootArrow(Vector3 targetPos)
        {
            if (audioSource && GetArrowCtrlBase.sceneArrow.GetComponent<ArrowSFX>() is ArrowSFX _arrowSFX)
            {
                audioSource.PlayOneShot(_arrowSFX.GetShotSound());
            }

            BowInfo _bowInfo = sceneBow.GetComponent<BowInfo>();

            GameObject _arrow = Instantiate(GetArrowCtrlBase.currentArrow, _bowInfo.arrowAnchor.position, _bowInfo.arrowAnchor.rotation);
            ArrowInfo _arrowInfo = _arrow.GetComponent<ArrowInfo>();
            _arrowInfo.ActiveShoot(targetPos - _bowInfo.arrowAnchor.position, _bowInfo);
        }
    }
}
