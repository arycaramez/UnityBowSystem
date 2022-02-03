using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private AnimationClip animShoot;
    [SerializeField] private AnimationClip animReload;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public bool BowEquiped
    {
        get { return anim.GetBool("bowEquiped"); }
        set { anim.SetBool("bowEquiped", value); }
    }

    public void PlayReload()
    {
        anim.Play(animReload.name);
    }

    public void PlayShoot()
    {
        anim.Play(animShoot.name);
    }

    public bool IsPlayReload() {
        return anim.GetCurrentAnimatorStateInfo(2).IsName(animReload.name);
    }

    public bool IsPlayShoot()
    {
        return anim.GetCurrentAnimatorStateInfo(2).IsName(animShoot.name);
    }
}
