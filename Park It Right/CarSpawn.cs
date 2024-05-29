using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CarSpawn : MonoBehaviour
{
    CarControl carControl;
    LevelTimer levelTimer;

    private CinemachineVirtualCamera myCinemachine;
    private Transform originalFollowTarget;

    private float respawnTimer = 2f;

    private float freezeTimer = 0f;

    public bool frozenState = false;

    [SerializeField] private GameObject terrain;

    public bool isDrowning;

    public bool carCanGetParked = false;
    private bool carCanGetParkedLock = false;
    private float timerPark = 0;

    public bool canLevelFail = false;

    private void Start()
    {
        carControl = FindObjectOfType<CarControl>();
        myCinemachine = FindObjectOfType<CinemachineVirtualCamera>();
        levelTimer = FindObjectOfType<LevelTimer>();
    }

    private void Update()
    {
        CarSafetyTimer();
    }

    private void CarSafetyTimer()
    {
        if (carControl.verticalInput == 0 && !carCanGetParkedLock)
        {
            carCanGetParked = false;
        }
        else
        {
            timerPark += Time.deltaTime;
            if (timerPark > 0.2f)
            {
                carCanGetParked = true;
            }
            if (carCanGetParked)
            {
                timerPark = 0;
            }
            carCanGetParkedLock = true;
        }
    }


    public void RespawnCar()
    {
        if (levelTimer.normalTimer > 0)
        {
            canLevelFail = false;
        }
        else
        {
            canLevelFail = true;
        }
        carCanGetParkedLock = false;
        carCanGetParked = false;
        carControl.transform.position = this.gameObject.transform.position;
        carControl.transform.rotation = this.gameObject.transform.rotation;
        carControl.rb.velocity = Vector3.zero;
        frozenState = false;
        freezeTimer = 0;
        terrain.layer = 6;
    }
    public void RespawnCarParking()
    {
        carCanGetParkedLock = false;
        carCanGetParked = false;
        carControl.transform.position = this.gameObject.transform.position;
        carControl.transform.rotation = this.gameObject.transform.rotation;
        frozenState = false;
        freezeTimer = 0;
        terrain.layer = 6;
    }

    public bool CanRespawnCar()
    {
        terrain.layer = 9;
        originalFollowTarget = myCinemachine.Follow;
        myCinemachine.LookAt = null;
        myCinemachine.Follow = null;
        StartCoroutine(PlayerRespawn());
        return true;
    }

    private IEnumerator PlayerRespawn()
    {
        yield return new WaitForSeconds(respawnTimer);
        RespawnCar();
        myCinemachine.Follow = originalFollowTarget;
        myCinemachine.LookAt = originalFollowTarget;
    }

    public bool FreezePlayerInput()
    {
        terrain.layer = 9;
        freezeTimer += Time.deltaTime;
        frozenState = true;
        carControl.rb.velocity = Vector3.zero;

        if (freezeTimer >= respawnTimer)
        {
            canLevelFail = true;
            RespawnCar();
        }

        return true;
    }
    public bool FreezePlayerInputParking()
    {
        terrain.layer = 9;
        freezeTimer += Time.deltaTime;
        frozenState = true;
        carControl.rb.velocity = Vector3.zero;
        if (freezeTimer >= respawnTimer)
        {
            RespawnCarParking();
        }

        return true;
    }

    public bool RemoveCarSound()
    {
        terrain.layer = 9;
        carControl.engineSound.volume = 0f;
        StartCoroutine(PlayerRemoveCarSound());
        return true;
    }

    private IEnumerator PlayerRemoveCarSound()
    {
        yield return new WaitForSeconds(respawnTimer);
        terrain.layer = 6;
        carControl.engineSound.volume = 1f;
    }

    public void DrowningCar()
    {
        isDrowning = true;
        StartCoroutine(PlayerDrowningCar());
    }

    private IEnumerator PlayerDrowningCar()
    {
        yield return new WaitForSeconds(respawnTimer);
        isDrowning = false;
    }
}
