using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private Text ammoText;

    public GameObject bulletPrefab;
    public GameObject muzzle;

    public float Ammo = 40;
    public float Mag = 30;
    private float magSize;
    public float fireRate = 0.2f;
    public float reloadTime = 2f;
    private AudioSource gunShot;

    private int enemyLayer = 8;
    private float time = 0;

    Backpack backpack;

    private void Start()
    {
        magSize = Mag;
        backpack = GameObject.FindGameObjectWithTag("Backpack").GetComponent<Backpack>();
        Mag = backpack.mag;
        Ammo = backpack.ammo;

        ammoText.text = $"{(float)Mag}" + "/" + $"{(float)magSize}" + " " + $"{(float)Ammo}";
        gunShot = muzzle.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (Input.GetButton("Fire1") && time > fireRate)
        {
            if (Mag > 0)
            {
                Fire();
                Mag--;
                ammoText.text = $"{(float)Mag}" + "/" + $"{(float)magSize}" + " " + $"{(float)Ammo}";
                time = 0f;
            }
            else
            {
                Invoke("Reload", reloadTime);
            }
        }
    }

    private void Fire()
    {
        var go = Instantiate(bulletPrefab);
        go.transform.position = muzzle.transform.position;
        var bullet = go.GetComponent<Bullet>();
        bullet.Fire(go.transform.position, muzzle.transform.eulerAngles, enemyLayer);
        gunShot.Play();
    }

    //When You get out of ammo and you still have ammo left, you will reload Your gun
    private void Reload()
    {
        if (Ammo != 0)
        {
            if (Ammo >= magSize)
            {
                Mag = magSize;
                Ammo -= magSize;
                ammoText.text = $"{(float)Mag}" + "/" + $"{(float)magSize}" + " " + $"{(float)Ammo}";
            }
            else
            {
                Mag = Ammo;
                Ammo = 0;
                ammoText.text = $"{(float)Mag}" + "/" + $"{(float)magSize}" + " " + $"{(float)Ammo}";
            }
        }
    }
}
