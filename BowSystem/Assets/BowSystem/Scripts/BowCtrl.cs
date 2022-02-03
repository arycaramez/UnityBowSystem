using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using BowSystemLib.Bow;

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
        [SerializeField] private bool inHand;
        [SerializeField] private bool hide;

        private void OnDisable()
        {
            DestroyImmediate(sceneBow);
        }

        private void FixedUpdate()
        {
            SceneBowInstanceCtrl();

            if (sceneBow) {
                BowInfo _sceneBowInfo = sceneBow.GetComponent<BowInfo>();
                _sceneBowInfo.ShowBow(!hide);
            }

            if (sceneBow)
            {
                Transform _anchor = GetBowAnchor();
                sceneBow.transform.position = _anchor.position;
                sceneBow.transform.rotation = _anchor.rotation;        
            }
        }

        private void SceneBowInstanceCtrl() {
            Transform _anchor = GetBowAnchor();
            if (sceneBow == null && currentBow)
            {
                sceneBow = Instantiate(currentBow, _anchor.position, _anchor.rotation);
                BowInfo _sceneBowInfo = sceneBow.GetComponent<BowInfo>();
                _sceneBowInfo.originalPrefabName = currentBow.name;
                if (rootPlayerObj) sceneBow.transform.SetParent(rootPlayerObj);
            } 
            else if(sceneBow && currentBow)
            {
                BowInfo _sceneBowInfo = sceneBow.GetComponent<BowInfo>();
                if (!_sceneBowInfo) {
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
            else if(currentBow == null && sceneBow)
            {
                DestroyImmediate(sceneBow);
            }
        }

        private Transform GetBowAnchor() {
            return inHand ? handBowAnchor : keepBowAnchor;
        }
        /// <summary>Animation Trigger: Used to pick up the character's bow and place it in your hand.</summary>
        public void TakeBow()
        {
            inHand = true;
        }
        /// <summary>Animation Trigger: Used to hold the character's bow.</summary>
        public void KeepBow()
        {
            inHand = false;
        }

        public bool GetBowInHand() { return inHand; }

        public void SetHideBow(bool value)
        {
            hide = value;
        }

        public bool GetHideBow() { return hide; }
    }
}
