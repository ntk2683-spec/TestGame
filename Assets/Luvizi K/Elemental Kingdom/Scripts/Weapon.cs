using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Weapon : MonoBehaviour
{
    public Joystick joystick;
    private float rotateOffset = 0f;
    [SerializeField] private Transform firePos;
    [SerializeField] private GameObject bulletPrefabs;
    [SerializeField] private float shootDelay = 1f;
    private float nextShoot;
    private bool isShooting = false;
    private Coroutine shootingCoroutine;
    void Start()
    {
        isShooting = false;
    }
    void Update()
    {
        RotateGun();
    }
    void RotateGun()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        if (horizontal == 0 && vertical == 0)
        {
            return;
        }

        float angle = Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + rotateOffset);
        Vector3 currentPosition = transform.localPosition;
        if (angle < -90 || angle > 90)
        {
            transform.localScale = new Vector3(0.6f, -0.6f, 1);
            currentPosition.x = Mathf.Abs(currentPosition.x) * -1;
        }
        else
        {
            transform.localScale = new Vector3(0.6f, 0.6f, 1);
            currentPosition.x = Mathf.Abs(currentPosition.x);
        }
        transform.localPosition = currentPosition;
    }
    public void Shoot()
    {
        nextShoot = Time.time + shootDelay;
        Instantiate(bulletPrefabs, firePos.position, firePos.rotation);
    }
    public void StartShooting()
    {
        if (!isShooting)
        {
            isShooting = true;
            shootingCoroutine = StartCoroutine(ShootContinuously());
        }
    }
    public void StopShooting()
    {
        isShooting = false;
        if (shootingCoroutine != null)
        {
            StopCoroutine(shootingCoroutine);
        }
    }
    private IEnumerator ShootContinuously()
    {
        while (isShooting)
        {
            Shoot();
            yield return new WaitForSeconds(shootDelay);
        }
    }
}