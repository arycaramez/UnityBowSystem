using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BowSystemLib.Bow
{
    public class BowInfo : MonoBehaviour
    {
        [HideInInspector] public string originalPrefabName;

        public float shootForce = 500;
        public Transform arrowAnchor;
        public List<Renderer> renderers = new List<Renderer>();

        virtual public void ShowBow(bool value) {
            foreach (Renderer e in renderers) {
                if(e.enabled != value) e.enabled = value;
            }
        }
    }
}