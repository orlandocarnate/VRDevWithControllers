using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

namespace Zenva.VR
{
    public class AxisController : MonoBehaviour
    {

        static readonly Dictionary<string, InputFeatureUsage<float>> availableFeatures = new Dictionary<string, InputFeatureUsage<float>>
        {
            {"trigger", CommonUsages.trigger },
            {"grip", CommonUsages.grip },
            {"indexTouch", CommonUsages.indexTouch },
            {"thumbTouch", CommonUsages.thumbTouch },
            {"indexFinger", CommonUsages.indexFinger },
            {"middleFinger", CommonUsages.middleFinger },
            {"ringFinger", CommonUsages.ringFinger },
            {"pinkyFinger", CommonUsages.pinkyFinger },

        };

        private enum FeatureOptions
        {
            trigger,
            grip,
            indexTouch,
            thumbTouch,
            indexFinger,
            middleFinger,
            ringFinger,
            pinkyFinger
        }

        [Tooltip("Input device role (left/right hand)")]
        [SerializeField]
        private InputDeviceRole deviceRole;

        [Tooltip("Select an input feature")]
        [SerializeField]
        private FeatureOptions feature;

        [Tooltip("Sensitivity of Axis")]
        [SerializeField]
        [Range(0, 1)]
        private float threshold;

        [Tooltip("Event when the button starts being pressed")]
        [SerializeField]
        private UnityEvent OnPress;
                
        [Tooltip("Event when the button starts being pressed")]
        [SerializeField]
        private UnityEvent OnRelease;

        // keep track if button is pressed
        bool isPressed;

        // 2a) get label in Awake method
        // 2b) find object in dictionary in Awake method

        // ------ 3) use the object we found --------------------------
        // selected feature object
        InputFeatureUsage<float> selectedFeature;

        List<InputDevice> devices;

        float inputValue;



        void Awake()
        {
            // init devices list
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
                if (devices[i].TryGetFeatureValue(selectedFeature, out inputValue) && inputValue > threshold)
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
