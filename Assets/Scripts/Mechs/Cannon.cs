using Assets.Scripts.MISC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cannon : MonoBehaviour
{
    [SerializeField]
    private Transform shootTransform;
    [SerializeField]
    private float shootPower, upPowerFactor, shootInterval, randomRange, spinSpeed;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private bool randomPower, spin;

    private float timer;

    private void FixedUpdate()
    {
        timer -= Time.deltaTime;
        if (spin)
        {
            transform.Rotate(Vector3.up * spinSpeed);
        }
        if (timer < 0)
        {
            timer += shootInterval;
            Shoot();
        }
    }

    private void Shoot()
    {
        var bulletObj = Instantiate(bulletPrefab, shootTransform.position, Quaternion.Euler(shootTransform.eulerAngles));
        if (bulletObj.GetComponent<IShootableObject>() != null)
            bulletObj.GetComponent<IShootableObject>().OnShot();
        if (randomPower)
        {
            bulletObj.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, upPowerFactor * shootPower * (Random.Range(1 - randomRange, 1 + randomRange)), shootPower * (Random.Range(1 - randomRange, 1 + randomRange))), ForceMode.Impulse);
        }
        else
        {
            bulletObj.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, shootPower, shootPower), ForceMode.Impulse);
        }
    }
}
