using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BowSystemLib.Arrow
{
    public class ArrowSFX : MonoBehaviour
    {
        [SerializeField] private AudioClip shotSound;
        [SerializeField] private AudioClip impactSound;
        [SerializeField] private AudioSource audioSource;
        /// <summary>Return arrow shot sound.</summary>
        virtual public AudioClip GetShotSound()
        {
            return shotSound;
        }
        /// <summary>Return arrow impact sound.</summary>
        virtual public AudioClip GetImpactSound()
        {
            return impactSound;
        }
        /// <summary>play the sound of arrow shooting.</summary>
        virtual public void PlayImpactSound()
        {
            if (audioSource) audioSource.PlayOneShot(GetImpactSound());
        }
    }
}
