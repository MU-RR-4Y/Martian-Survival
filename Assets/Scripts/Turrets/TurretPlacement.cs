using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class TurretPlacement : MonoBehaviour
{
    public GameObject placeablePrefab;
    public KeyCode newObjectHotkey = KeyCode.E;
    private GameObject currentPlaceableObject;

    public GameObject dummyTurret;


    private GameObject player;
    PlayerScript playerScript;
    //private StarterAssetsInputs starterAssetInputs;

    private void Awake()
    {
    //    starterAssetInputs = GetComponent<StarterAssetsInputs>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerScript>();
    }

   

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

            //HandleNewObjectHotKey();

            if(currentPlaceableObject != null)
            {
            MoveCurrentPlaceableObjectToMouse();
            ReleaseIfClicked();
            }

    }

    private void ReleaseIfClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {

            currentPlaceableObject = null;
            playerScript.payForTower();
        }

    }

    private void MoveCurrentPlaceableObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo))
        {
            currentPlaceableObject.transform.position = hitInfo.point;
        }
    }

        
    public void HandleNewObjectHotKey()
    {
       if (Input.GetKeyDown(newObjectHotkey))
            {
                if(currentPlaceableObject == null)
                {
                    currentPlaceableObject = Instantiate(dummyTurret);
                }
                else
                {
                    Destroy(currentPlaceableObject);
                }
            }
    }


    public void spawnDummyTurret()
    {
        Instantiate(dummyTurret);
    }
}
