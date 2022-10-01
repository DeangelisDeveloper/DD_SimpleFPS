using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject impactEffect = null;
    [SerializeField] ParticleSystem muzzleFlash = null;
    Animator animator;
    float nextShot;
    bool isReloading;
    [Header("Weapon Stats")]
    [SerializeField] int maxAmmo = 0;
    [SerializeField] float damage = 0f;
    [SerializeField] float shootRange = 0f;
    [SerializeField] float impactForce = 0f;
    [SerializeField] float fireRate = 0f;
    [SerializeField] float reloadTime = 0f;
    [SerializeField] float hitTime = 0f;
    [SerializeField] bool isAuto = false;
    int currentAmmo;
    [Header("Audio")]
    [SerializeField] AudioSource shootSource = null;
    [SerializeField] AudioSource reloadSource = null;
    [Header("GUI")]
    [SerializeField] GameObject sight = null;
    [SerializeField] Text ammoText = null;

    void Start()
    {
        currentAmmo = maxAmmo;
        nextShot = 0f;
        isReloading = false;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        SetAmmo();

        if (isReloading)
            return;

        CheckShootConditions();
        Reload();
    }

    void SetAmmo()
    {
        ammoText.text = "AMMO: " + currentAmmo.ToString();
    }

    void CheckShootConditions()
    {
        if (Time.time >= nextShot)
        {
            if ((isAuto && Input.GetKey(KeyCode.Mouse0)) || (!isAuto && Input.GetKeyDown(KeyCode.Mouse0)))
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    nextShot = Time.time + 1f / fireRate;
                    Shoot();
                }
            }
        }
    }

    void Shoot()
    {
        currentAmmo--;
        muzzleFlash.Play();
        shootSource.Play();
        RaycastHit hit;

        if (isReloading == false)
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, shootRange))
            {
                Enemy target = hit.transform.GetComponent<Enemy>();

                if (target != null)
                {
                    StartCoroutine(HitEnemy());
                    target.TakeDamage(damage);
                }

                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2f);
            }
        }
    }

    void Reload()
    {
        if (currentAmmo < 0 || (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo))
        {
            currentAmmo = 0;
            shootSource.Stop();
            reloadSource.Play();
            impactEffect.SetActive(false);
            StartCoroutine(Reloading());
            return;
        }
    }

    IEnumerator Reloading()
    {
        sight.SetActive(false);
        isReloading = true;
        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime - 0.25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.25f);
        currentAmmo = maxAmmo;
        impactEffect.SetActive(true);
        isReloading = false;
        sight.SetActive(true);
    }

    IEnumerator HitEnemy()
    {
        sight.GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(hitTime);
        sight.GetComponent<Image>().color = Color.white;
    }
}