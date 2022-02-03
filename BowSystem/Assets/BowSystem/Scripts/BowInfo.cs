using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BowSystemLib.Bow
{
    public class BowInfo : MonoBehaviour
    {
        [HideInInspector] public string originalPrefabName;
        public Transform arrowAnchor;

        [SerializeField] List<Renderer> renderers = new List<Renderer>();

        public void ShowBow(bool value) {
            foreach (Renderer e in renderers) {
                if(e.enabled != value) e.enabled = value;
            }
        }
    }
}