using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace Zenva.VR
{
    public class ButtonController : MonoBehaviour
    {
        List<InputDevice> devices;

        bool inputValue;

        void Awake()
        {
            devices = new List<InputDevice>();
        }
        // Update is called once per frame
        void Update()
        {
            // get our device we want to check
            InputDevices.GetDevicesWithRole(InputDeviceRole.LeftHanded, devices); 

            // go through our devices
            for (int i = 0; i < devices.Count; i++)
            {
                // checj whether our button is being pressed
                // 1) check whether we can read the state of our button
                // - AND - 
                // 2) the button's value should be true
                if (devices[i].TryGetFeatureValue(CommonUsages.triggerButton, out inputValue) && inputValue)
                {
                    // say hello in the console
                    Debug.Log("Hello");
                }
            }

        }
    }

}
