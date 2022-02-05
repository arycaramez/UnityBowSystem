using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BowSystemLib.Arrow;
using BowSystemLib.Bow;

namespace BowSystemLib
{
    public class ArrowCtrlBase : MonoBehaviour
    {
        public BowCtrlBase bowSystemCtrl;

        [Header("References:")]
        public Transform handArrowAnchor;
        [Space(5)]
        public GameObject currentArrow;
        public GameObject sceneArrow;

        [Header("Settings:")]
        public bool inHand;
        public bool hide;

        private void Start()
        {
            bowSystemCtrl = GetComponent<BowCtrlBase>();
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
        virtual public void SceneArrowManager() { }
        /// <summary> Function responsible for shoot arrow. </summary>
        virtual public void ShootArrow(Vector3 targetPos) { }
        /// <summary>Return when arrow cam be hide.</summary>
        /// <returns></returns>
        virtual public bool ArrowIsHide()
        {
            return (bowSystemCtrl.inHand && !bowSystemCtrl.hide) && !hide;
        }
        /// <summary>  </summary>
        virtual public void SceneInstanceCtrl() { }
        /// <summary> Return the current anchor reference of bow/crossbow. </summary>
        /// <returns></returns>
        virtual public Transform GetAnchor()
        {
            BowInfo _bowInfo = bowSystemCtrl.sceneBow.GetComponent<BowInfo>();
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