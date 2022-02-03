using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BowSystemLib.Arrow;
using BowSystemLib.Bow;

namespace BowSystemLib
{
    [RequireComponent(typeof(BowCtrl))]
    public class ArrowCtrl : MonoBehaviour
    {
        private BowCtrl bowSystemCtrl { get { return GetComponent<BowCtrl>(); } }

        [SerializeField] private Transform handArrowAnchor;
        [Space(5)]
        [SerializeField] private GameObject currentArrow;
        [SerializeField] private GameObject sceneArrow;

        [Header("Settings:")]
        [SerializeField] private bool inHand;
        [SerializeField] private bool hide;
        
        private bool lastInHand;

        private void OnDisable()
        {
            DestroyImmediate(sceneArrow);
        }

        private void FixedUpdate()
        {
            if (bowSystemCtrl.sceneBow)
            {
                SceneInstanceCtrl();
            }
            else if (sceneArrow)
            {
                DestroyImmediate(sceneArrow);
            }

            if (sceneArrow)
            {
                ArrowInfo _sceneArrowInfo = sceneArrow.GetComponent<ArrowInfo>();

                Transform _anchor = GetAnchor();
                if (lastInHand != inHand) {
                    lastInHand = _anchor;
                    sceneArrow.transform.position = _anchor.position;
                    sceneArrow.transform.rotation = _anchor.rotation;
                    if(!inHand) sceneArrow.transform.SetParent(_anchor);
                }

                if (inHand) {
                    sceneArrow.transform.SetParent(bowSystemCtrl.rootPlayerObj);
                    sceneArrow.transform.position = _anchor.position;
                    sceneArrow.transform.rotation = _anchor.rotation;
                }

                _sceneArrowInfo.Show(ArrowIsHide());
            }
        }

        private bool ArrowIsHide() {
            return (bowSystemCtrl.GetBowInHand() && !bowSystemCtrl.GetHideBow()) && !hide;
        }

        private void SceneInstanceCtrl()
        {
            BowInfo _bowInfo = bowSystemCtrl.sceneBow.GetComponent<BowInfo>();
            Transform _anchor = GetAnchor();
            if (sceneArrow == null && currentArrow)
            {
                sceneArrow = Instantiate(currentArrow, _anchor.position, _anchor.rotation);
                ArrowInfo _sceneArrowInfo = sceneArrow.GetComponent<ArrowInfo>();
                _sceneArrowInfo.originalPrefabName = currentArrow.name;
                sceneArrow.transform.SetParent(_bowInfo.arrowAnchor);
            }
            else if (sceneArrow && currentArrow)
            {
                ArrowInfo _sceneArrowInfo = sceneArrow.GetComponent<ArrowInfo>();
                if (!_sceneArrowInfo)
                {
                    DestroyImmediate(sceneArrow);
                }

                if (_sceneArrowInfo && _sceneArrowInfo.originalPrefabName != currentArrow.name)
                {
                    DestroyImmediate(sceneArrow);
                    sceneArrow = Instantiate(currentArrow, _anchor.position, _anchor.rotation);
                    _sceneArrowInfo = sceneArrow.GetComponent<ArrowInfo>();
                    _sceneArrowInfo.originalPrefabName = currentArrow.name;
                    sceneArrow.transform.SetParent(_bowInfo.arrowAnchor);
                }
            }
            else if (currentArrow == null && sceneArrow)
            {
                DestroyImmediate(sceneArrow);
            }
        }

        private Transform GetAnchor()
        {
            BowInfo _bowInfo = bowSystemCtrl.sceneBow.GetComponent<BowInfo>();
            return inHand ? handArrowAnchor : _bowInfo.arrowAnchor;
        }

        public bool InHand {
            get { return inHand; }
            set { inHand = value; }
        }

        public void ArrowInHandTrue() {
            InHand = true;
        }
        public void ArrowInHandFalse()
        {
            InHand = false;
        }

        public bool Hide
        {
            get { return hide; }
            set { hide = value; }
        }

        public void ArrowHideTrue()
        {
            Hide = true;
        }
        public void ArrowHideFalse()
        {
            Hide = false;
        }

        public void ShootArrow() {
            BowInfo _bowInfo = bowSystemCtrl.sceneBow.GetComponent<BowInfo>();
            GameObject _arrow = Instantiate(currentArrow, _bowInfo.arrowAnchor.position, _bowInfo.arrowAnchor.rotation);
            ArrowInfo _arrowInfo = _arrow.GetComponent<ArrowInfo>();
            _arrowInfo.ActiveShoot(transform.forward);
        }
    }
}