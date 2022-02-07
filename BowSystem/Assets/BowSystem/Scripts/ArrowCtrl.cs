using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BowSystemLib.Arrow;
using BowSystemLib.Bow;

namespace BowSystemLib
{
    public class ArrowCtrl : MonoBehaviour
    {
        public BowCtrl bowCtrl;

        [Header("References:")]
        public Transform handArrowAnchor;
        [Space(5)]
        public GameObject currentArrow;
        [HideInInspector] public GameObject sceneArrow;

        [Header("Settings:")]
        public bool inHand;
        public bool hide;

        public bool lastInHand { get; set; }

        private void Start()
        {
            bowCtrl = GetComponent<BowCtrl>();
        }

        private void OnDisable()
        {
            DestroyImmediate(sceneArrow);
        }

        private void FixedUpdate()
        {
            SceneArrowManager();
        }

        /// <summary> Run the main code of arrow controllers. </summary>
        virtual public void SceneArrowManager()
        {
            if (bowCtrl.sceneBow)
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
                if (lastInHand != inHand)
                {
                    lastInHand = _anchor;
                    sceneArrow.transform.position = _anchor.position;
                    sceneArrow.transform.rotation = _anchor.rotation;
                    if (!inHand) sceneArrow.transform.SetParent(_anchor);
                }

                if (inHand)
                {
                    sceneArrow.transform.SetParent(bowCtrl.rootPlayerObj);
                    sceneArrow.transform.position = _anchor.position;
                    sceneArrow.transform.rotation = _anchor.rotation;
                }

                _sceneArrowInfo.SetRenderers(ArrowIsHide());
            }
        }
        /// <summary>Return when arrow cam be hide.</summary>
        /// <returns></returns>
        virtual public bool ArrowIsHide()
        {
            return (bowCtrl.inHand && !bowCtrl.hide) && !hide;
        }
        /// <summary>  </summary>
        virtual public void SceneInstanceCtrl()
        {
            BowInfo _bowInfo = bowCtrl.sceneBow.GetComponent<BowInfo>();
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
        /// <summary> Return the current anchor reference of bow/crossbow. </summary>
        /// <returns></returns>
        virtual public Transform GetAnchor()
        {
            BowInfo _bowInfo = bowCtrl.sceneBow.GetComponent<BowInfo>();
            return inHand ? handArrowAnchor : _bowInfo.arrowAnchor;
        }
        /// <summary> Trigger for animation: Enable hand anchor reference like current.</summary>
        virtual public void ArrowInHandTrue()
        {
            inHand = true;
        }
        /// <summary> Trigger for animation: Disable hand anchor reference like current.</summary>
        virtual public void ArrowInHandFalse()
        {
            inHand = false;
        }
        /// <summary> Trigger for animation: Enable renderer arrow components.</summary>
        virtual public void ArrowHideTrue()
        {
            hide = true;
        }
        /// <summary> Trigger for animation: Disable renderer arrow components.</summary>
        virtual public void ArrowHideFalse()
        {
            hide = false;
        }
    }
}