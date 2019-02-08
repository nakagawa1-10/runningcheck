using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox
{
    public class KeySenderTest : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Return))
            {
                Debug.Log("ENTER");
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                
            }
        }
    }
}