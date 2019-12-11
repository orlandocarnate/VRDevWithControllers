using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zenva.VR
{
    public class Spawner : MonoBehaviour
    {
        void Spawn(Color color)
        {
            // create a sphere
            GameObject sphere = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere));

            // adjust transform
            sphere.transform.localScale *= 0.05f;
            sphere.transform.position = transform.position + transform.forward * 0.3f;
            sphere.GetComponent<Renderer>().material.color = color;
        }

        public void SpawnRed()
        {
            Spawn(Color.red);
        }
        
        public void SpawnGreed()
        {
            Spawn(Color.green);
        }
        
        public void SpawnBlue()
        {
            Spawn(Color.blue);
        }
        
        public void SpawnYellow()
        {
            Spawn(Color.yellow);
        }

    }
}

