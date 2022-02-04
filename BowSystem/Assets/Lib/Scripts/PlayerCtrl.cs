﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BowSystemLib;
/// <summary>
/// This script it is responsible to implement a simple player controller, it's a example.
/// *Control of player rotation, using mouse position with reference.
/// *Control the use of bow/crossbow.
/// *Control Arrow shoot.
/// *Control Arrow reload.
/// </summary>
[RequireComponent(typeof(PlayerAnim))]
public class PlayerCtrl : MonoBehaviour
{
    private PlayerAnim playerAnim;
    private ArrowCtrlBase arrowCtrl;

    [SerializeField] private float cooldown;
    private float countCooldown;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<PlayerAnim>();
        arrowCtrl = GetComponent<ArrowCtrlBase>();
    }

    // Update is called once per frame
    void Update()
    {
        //Player Rotation
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit;

        if (Physics.Raycast(_ray, out _hit)) {
            Vector3 _euler = Quaternion.LookRotation(_hit.point - transform.position).eulerAngles;
            _euler.x = 0;
            _euler.z = 0;
            transform.eulerAngles = _euler;
        }
        // Use or Keep the bow/crossbow
        if (Input.GetKeyDown(KeyCode.Tab)) {
            playerAnim.BowEquiped = !playerAnim.BowEquiped;
        }
        // Shoot Arrow and Reload.
        if (Input.GetKey(KeyCode.Mouse0) && playerAnim.BowEquiped && !playerAnim.IsPlayReload() && !playerAnim.IsPlayShoot()) {
            if (!arrowCtrl.hide)
            {
                arrowCtrl.ShootArrow();
                playerAnim.PlayShoot();
                countCooldown = Time.time;

            }
            ArrowReload();
        } else {
            ArrowReload();
        }
        
    }
    /// <summary>Reload arrow if bow/crossbow was equiped, arrow is not hide and if player doesn't shooting.</summary>
    private void ArrowReload() {
        if (playerAnim.BowEquiped && !playerAnim.IsPlayReload())
        {
            if (arrowCtrl.hide && !playerAnim.IsPlayShoot()) playerAnim.PlayReload();
        }
    }
}
