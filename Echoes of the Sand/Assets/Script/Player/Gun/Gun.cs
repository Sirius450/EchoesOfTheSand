using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawnPoint;

    [SerializeField] Transform holster;
    [SerializeField] Transform aimPos;

    [SerializeField] float shootingRate = 2f;
    [SerializeField] float shootingCooldown = 0f;

    //Energy Bar
    [SerializeField] GameObject energyBar;
    [SerializeField] float energyUsedPerShot = 0.5f;

    Aim aim;

    private Ray ray;


    public bool isAiming, isShooting /*shotOnce*/ ;

    [SerializeField] private string audioClipNamePew = "pewpew";


    private void Awake()
    {
        aim = GetComponentInParent<Aim>();
    }


    // Update is called once per frame
    void Update()
    {
        //isAiming = InputManager.isAimingInput;
        //isShooting = InputManager.isShooting;
        //shotOnce = false;

        isAiming = aim.isAming;

        if (shootingCooldown > 0)
        {
            shootingCooldown -= Time.deltaTime;
        }

        if (isAiming)
        {
            if (isShooting && shootingCooldown <= 0)
            {
                //Shoot();
                shootingCooldown = shootingRate;
            }

            transform.position = aimPos.position;
            transform.rotation = aimPos.rotation;
        }
        else
        {
            transform.position = holster.position;
            transform.rotation = holster.rotation;
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AudioManager.Singleton.PlayAudio(audioClipNamePew);


            if (energyBar.GetComponent<Health_Bar>().isEmpty(energyUsedPerShot) || !isAiming)
            {
                return;
            }
  
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            Quaternion rotation = Quaternion.identity;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 direction = hit.point - bulletSpawnPoint.position;
                rotation = Quaternion.LookRotation(direction);
            }
            else
            {
                rotation = Quaternion.LookRotation(ray.direction);
            }


            Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, rotation);

            energyBar.GetComponent<Health_Bar>().useEnergy(energyUsedPerShot);
        }

        // shotOnce = true;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * 1000);
    }
}

