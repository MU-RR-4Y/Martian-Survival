using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;

public class ThirdPersonCamController : MonoBehaviour
{
    public CinemachineVirtualCamera aimVirtualCam;
    public float normalSensitivity;
    public float aimSensitivity;
    public LayerMask aimColliderMask = new LayerMask();
    //public Transform debugTransform;
    public Transform projectilePrefab;
    public Transform spawnProjectilePos;

    private AudioSource playerAudio;
    public AudioClip shootSound;


    private StarterAssetsInputs starterAssetInputs;
    private ThirdPersonController thirdPersonController;
    TurretPlacement turretPlacement;
    public GameObject TurretPlacementObject;
    PlayerScript playerScript;
    //BuildTurret buildTurret;
    //public GameObject buildTurretObj;

    private bool playerAlive = true;


    private void Awake()
    {
        starterAssetInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        turretPlacement = TurretPlacementObject.GetComponent<TurretPlacement>();
        playerScript = GetComponent<PlayerScript>();
        playerAudio = GetComponent<AudioSource>();
        //buildTurret = buildTurretObj.GetComponent<BuildTurret>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAlive)
        {
            if (!PauseMenu.isPaused)
            {

                Vector3 mouseWorldPosition = Vector3.zero;
                Vector2 screenCenterPoint = new Vector2(Screen.width / 2.0f, Screen.height / 2.0f);

                Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
                if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderMask))
                {
                    mouseWorldPosition = raycastHit.point;
                    //debugTransform.position = raycastHit.point;
                }

                if (starterAssetInputs.aim)
                {
                    aimVirtualCam.gameObject.SetActive(true);
                    thirdPersonController.SetSensitivity(aimSensitivity);
                    thirdPersonController.Set_RotateOnMove(false);

                    //rotates player when in aim mode. turns to face crosshair/raycasthit point

                    Vector3 worldAimTarget = mouseWorldPosition;
                    worldAimTarget.y = transform.position.y;
                    Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

                    transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);

                }
                else
                {
                    aimVirtualCam.gameObject.SetActive(false);
                    thirdPersonController.SetSensitivity(normalSensitivity);
                    thirdPersonController.Set_RotateOnMove(true);
                    if (playerScript.canBuild)
                    {
                        turretPlacement.HandleNewObjectHotKey();
                    }
                    //buildTurret.HandleNewObjectHotKey();


                }


                if (starterAssetInputs.shoot && starterAssetInputs.aim)
                {
                    Vector3 aimDirection = (mouseWorldPosition - spawnProjectilePos.position).normalized;
                    Instantiate(projectilePrefab, spawnProjectilePos.position, Quaternion.LookRotation(aimDirection, Vector3.up));
                    playerAudio.PlayOneShot(shootSound);
                    starterAssetInputs.shoot = false;
                }

                ConstrainPlayerPosition();
            }
        }
    }

    void ConstrainPlayerPosition()
    {
        if (transform.position.x > 35)
        {
            transform.position = new Vector3(35, transform.position.y, transform.position.z);
        }

        if (transform.position.x < -35)
        {
            transform.position = new Vector3(-35, transform.position.y, transform.position.z);
        }


        if (transform.position.z > 35)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 35);
        }

        if (transform.position.z < -35)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -35);
        }


    }

    public void killPlayer()
    {
        playerAlive = false;
    }

    public void OnEnable()
    {
        PlayerScript.OnPlayerDeath += killPlayer;
    }

    public void OnDisable()
    {
        PlayerScript.OnPlayerDeath -= killPlayer;
    }
}
