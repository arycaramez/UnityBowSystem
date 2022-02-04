using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BowSystemLib
{
    public class BowCtrlBase : MonoBehaviour
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

        private void OnDisable()
        {
            DestroyImmediate(sceneBow);
        }

        private void FixedUpdate()
        {
            SceneBowManager();
        }

        virtual public void SceneBowManager() { }
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
    }
}