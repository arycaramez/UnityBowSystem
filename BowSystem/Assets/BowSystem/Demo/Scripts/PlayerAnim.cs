using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private AnimationClip animShoot;
    [SerializeField] private AnimationClip animReload;

    public Transform armR;

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

    public float BowTargetAngle {
        get{ return anim.GetFloat("bowTargetAngle"); }
        set { anim.SetFloat("bowTargetAngle",value); }
    }

    public void CtrlTargetArmR(Vector3 targetPos) {
        //armR.forward, targetPos - armR.position
        float _angle = Vector3.Angle(targetPos - armR.position, transform.forward);
        Quaternion _rot = Quaternion.LookRotation(targetPos - armR.position);

        if (_rot.eulerAngles.x < 180) {
            _angle = -_angle;
        }

        BowTargetAngle = _angle;
    }
}
