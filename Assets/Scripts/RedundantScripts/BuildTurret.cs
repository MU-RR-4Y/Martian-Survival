using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTurret : MonoBehaviour
{
    public GameObject dummyTurret;
    public KeyCode newObjectHotkey = KeyCode.B;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (dummyTurret != null)
        //{
        //    Vector3 mousePos = Input.mousePosition;
        //    mousePos = new Vector3(mousePos.x, 0, mousePos.z);
        //    Vector3 turretPosition = Camera.main.ScreenToWorldPoint(mousePos);
        //    dummyTurret.transform.position = new Vector3(turretPosition.x, turretPosition.y, turretPosition.z);
        //}

    }

    public void spawnDummyTurret()
    {
        Instantiate(dummyTurret);
    }

    public void HandleNewObjectHotKey()
    {
        if (Input.GetKeyDown(newObjectHotkey))
        {
            if (dummyTurret == null)
            {
                Instantiate(dummyTurret);
                Vector3 mousePos = Input.mousePosition;
                mousePos = new Vector3(mousePos.x, 0, mousePos.z);
                Vector3 turretPosition = Camera.main.ScreenToWorldPoint(mousePos);
                dummyTurret.transform.position = new Vector3(turretPosition.x, turretPosition.y, turretPosition.z);
            }
        }
    }
}
