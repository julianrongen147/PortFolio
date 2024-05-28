using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GunHandler : MonoBehaviour
{
    public static GunHandler instance;
    //machine gun
    [SerializeField] AudioClip machineGunShootClip;
    [SerializeField] AudioSource machineGunSchootEffect;
    [SerializeField] public float fireRate;
    private float lastShootTime = 0;
    [SerializeField] private GameObject machineGunBullet;
    [SerializeField] private GameObject[] machineGunSpawnPoints;
    [SerializeField] private KeyCode machineGunBind = KeyCode.F;
    [SerializeField] private MouseButton machineGunBindMouse = MouseButton.Right;

    //switch
    public GunHandler gunhandler;
    [SerializeField] private GameObject[] machineGunSpawnPoints2;
    [SerializeField] private KeyCode machineGunIsSwitched = KeyCode.Q;
    [SerializeField] private GameObject machineGunBullet2;
    public bool shootBack = false;
    private float shootState;


    private machineGunSide m_side = machineGunSide.Left;
    private machineGunSide m_side2 = machineGunSide.Right;

    //torpedo
    [SerializeField] AudioClip torpedoLaunch;
    [SerializeField] AudioSource TorpedoLaunchEffect;
    [SerializeField] private GameObject torpedo;
    [SerializeField] private GameObject[] torpedoSpawnpoints;
    [SerializeField] private KeyCode torpedoBind = KeyCode.E;
    [SerializeField] private MouseButton torpedoBindMouse = MouseButton.Left;

    //torpedo cooldown
    [SerializeField] public float coolDownTorpedo;
    public float lastShotTorpedo;

    public float currentCooldown;

    private torpedoSide t_side = torpedoSide.Left;
    private enum torpedoSide
    {
        Left, Right
    }

    private enum machineGunSide
    {
        Left, Right
    }

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        //switcher
        if (Input.GetKeyDown(machineGunIsSwitched))
        {
            shootBack = !shootBack;
        }

        //torpedo
        if (Input.GetKeyDown(torpedoBind) || Input.GetMouseButtonDown((int)torpedoBindMouse))
        {
            TorpedoShoot();
        }

        //machine gun

        if (Input.GetKey(machineGunBind) || Input.GetMouseButton((int)machineGunBindMouse))
        {
            if (!shootBack)
            {
                machineGunShoot();
            }
            else
            {
                machineGunShootBackWards();
            }
        }


    }


    private void TorpedoShoot()
    {
        currentCooldown = Time.time - lastShotTorpedo;
        if (Time.time - lastShotTorpedo < coolDownTorpedo)
        {
            return;
        }


        lastShotTorpedo = Time.time;
        TorpedoLaunchEffect.PlayOneShot(torpedoLaunch);
        if (t_side == torpedoSide.Left)
        {
            t_side = torpedoSide.Right;
        }
        else
        {
            t_side = torpedoSide.Left;
        }
        GameObject torp = Instantiate(torpedo, torpedoSpawnpoints[(int)t_side].transform);
        torp.transform.parent = null;
    }

    private void machineGunShoot()
    {
        if (Time.time > lastShootTime)
        {
            machineGunSchootEffect.PlayOneShot(machineGunShootClip);
            lastShootTime = Time.time + fireRate;
            GameObject gun = Instantiate(machineGunBullet, machineGunSpawnPoints[(int)m_side].transform.position, machineGunSpawnPoints[(int)m_side].transform.rotation);
            GameObject gun2 = Instantiate(machineGunBullet, machineGunSpawnPoints[(int)m_side2].transform.position, machineGunSpawnPoints[(int)m_side2].transform.rotation);
            gun.transform.parent = null;
            gun2.transform.parent = null;
        }
    }
    private void machineGunShootBackWards()
    {
        if (Time.time > lastShootTime)
        {
            machineGunSchootEffect.PlayOneShot(machineGunShootClip);
            lastShootTime = Time.time + fireRate;
            GameObject gun = Instantiate(machineGunBullet, machineGunSpawnPoints2[(int)m_side].transform.position, machineGunSpawnPoints[(int)m_side].transform.rotation * Quaternion.Euler(0, 180, 0));
            GameObject gun2 = Instantiate(machineGunBullet, machineGunSpawnPoints2[(int)m_side2].transform.position, machineGunSpawnPoints[(int)m_side2].transform.rotation * Quaternion.Euler(0, 180, 0));
            gun.transform.parent = null;
            gun2.transform.parent = null;
        }
    }
}
