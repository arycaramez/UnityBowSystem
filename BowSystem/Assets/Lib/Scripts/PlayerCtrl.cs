using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BowSystemLib;

[RequireComponent(typeof(PlayerAnim))]
public class PlayerCtrl : MonoBehaviour
{
    private PlayerAnim playerAnim;
    private BowCtrl bowCtrl;
    private ArrowCtrl arrowCtrl;

    [SerializeField] private float cooldown;
    private float countCooldown;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<PlayerAnim>();
        bowCtrl = GetComponent<BowCtrl>();
        arrowCtrl = GetComponent<ArrowCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit;

        if (Physics.Raycast(_ray, out _hit)) {
            Vector3 _euler = Quaternion.LookRotation(_hit.point - transform.position).eulerAngles;
            _euler.x = 0;
            _euler.z = 0;
            transform.eulerAngles = _euler;
        }

        if (Input.GetKeyDown(KeyCode.Tab)) {
            playerAnim.BowEquiped = !playerAnim.BowEquiped;
        }

        if (Input.GetKey(KeyCode.Mouse0) && playerAnim.BowEquiped && !playerAnim.IsPlayReload() && !playerAnim.IsPlayShoot()) {
            if (!arrowCtrl.Hide)
            {
                arrowCtrl.ShootArrow();
                playerAnim.PlayShoot();
                countCooldown = Time.time;
            }
            else
            {
                playerAnim.PlayReload();
            }
        }
    }
}
