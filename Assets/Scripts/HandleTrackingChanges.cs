using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandleTrackingChanges : MonoBehaviour
{
    List<InputDevice> devices;

    [Tooltip("Input device role (left/right hand)")]
    [SerializeField]
    private InputDeviceRole deviceRole;

    [Tooltip("Game object representing controller")]
    [SerializeField]
    private GameObject gameObj;

    // Start is called before the first frame update
    void Awake()
    {
        // init devices list
        devices = new List<InputDevice>();

        gameObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Check();
    }

    void Check()
    {
        // get the tracked devices for the specified role
        InputDevices.GetDevicesWithRole(deviceRole, devices);

        // if the controller was being shown, and it's not found...
        if (gameObj.activeInHierarchy && devices.Count == 0)
        {
            // disable unused controller
            gameObj.SetActive(false);
        }


        // if the controller was hidden, and now it's found
        else if (!gameObj.activeInHierarchy && devices.Count > 0)
        {
            // enable controller
            gameObj.SetActive(true);
        }

    }
}
