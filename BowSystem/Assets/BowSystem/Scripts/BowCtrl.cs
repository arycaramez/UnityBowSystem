using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using BowSystemLib.Bow;

namespace BowSystemLib
{
    public class BowCtrl : BowCtrlBase
    {
        /// <summary>
        /// Control the instances of bow/crossbow and, including their references.
        /// </summary>
        override public void SceneBowManager() {
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
    }
}
