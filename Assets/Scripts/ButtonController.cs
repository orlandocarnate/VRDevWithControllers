using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

namespace Zenva.VR
{
    public class ButtonController : MonoBehaviour
    {
        // ------ 1) select a label from the unity inspector --------------------------
        private enum FeatureOptions
        {
            triggerButton,
            gripButton,
            thumbrest,
            primary2DAxisClick,
            primary2DAxisTouch,
            menuButton,
            secondaryButton,
            secondaryTouch,
            primaryButton,
            primaryTouch
        }

        [Tooltip("Input device role (left/right hand)")]
        [SerializeField]
        private InputDeviceRole deviceRole;

        [Tooltip("Select an input feature")]
        [SerializeField]
        private FeatureOptions feature;
        
        [Tooltip("Event when the button starts being pressed")]
        [SerializeField]
        private UnityEvent OnPress;
                
        [Tooltip("Event when the button starts being pressed")]
        [SerializeField]
        private UnityEvent OnRelease;

        // keep track if button is pressed
        bool isPressed;

        // ------ 2) find the object that corresponds to that label --------------------------
        static readonly Dictionary<string, InputFeatureUsage<bool>> availableFeatures = new Dictionary<string, InputFeatureUsage<bool>>
        {
            {"triggerButton", CommonUsages.triggerButton },
            {"gripButton", CommonUsages.gripButton },
            {"thumbrest", CommonUsages.thumbrest },
            {"primary2DAxisClick", CommonUsages.primary2DAxisClick },
            {"primary2DAxisTouch", CommonUsages.primary2DAxisTouch },
            {"menuButton", CommonUsages.menuButton },
            {"secondaryButton", CommonUsages.secondaryButton },
            {"secondaryTouch", CommonUsages.secondaryTouch },
            {"primaryButton", CommonUsages.primaryButton },
            {"primaryTouch", CommonUsages.primaryTouch },
        };

        // 2a) get label in Awake method
        // 2b) find object in dictionary in Awake method

        // ------ 3) use the object we found --------------------------
        // selected feature object
        InputFeatureUsage<bool> selectedFeature;

        List<InputDevice> devices;

        bool inputValue;



        void Awake()
        {
            devices = new List<InputDevice>();

            // 2a get lable
            string featureLabel = Enum.GetName(typeof(FeatureOptions), feature);

            // 2b find dictionary
            availableFeatures.TryGetValue(featureLabel, out selectedFeature);
        }
        // Update is called once per frame
        void Update()
        {
            // get our device we want to check
            InputDevices.GetDevicesWithRole(deviceRole, devices); 

            // go through our devices
            for (int i = 0; i < devices.Count; i++)
            {
                // checj whether our button is being pressed
                // 1) check whether we can read the state of our button
                // - AND - 
                // 2) the button's value should be true
                if (devices[i].TryGetFeatureValue(selectedFeature, out inputValue) && inputValue)
                {
                    // check if we are already pressing
                    if(!isPressed)
                    {
                        // update flag
                        isPressed = true;

                        // trigger OnPress event
                        OnPress.Invoke();
                    }
                    
                }
                else if (isPressed)
                {
                    isPressed = false;

                    // trigger the OnRelease event
                    OnRelease.Invoke();
                }
            }

        }
    }

}
