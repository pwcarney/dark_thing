using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    private bool light_on;
    private float light_time = 0;

    public bool LightOn
    {
        get
        {
            return light_on;
        }
        set
        {
            light_time = 0;
            GetComponent<Light>().enabled = value;
            light_on = value;
        }
    }

    private void Update()
    {
        light_time += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            if (LightOn)
            {
                LightOn = false;
            }
            else
            {
                LightOn = true;
            }
        }

        if (light_time > 10)
        {
            LightOn = false;
        }
    }
}
