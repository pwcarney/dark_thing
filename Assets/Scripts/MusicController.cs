using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    bool chased = false;
    bool ending_chase = false;

    float chase_refresh = 0;
    float chase_end_time = 5f;

    public AudioSource chase_source;

    public void Chasing()
    {
        chase_refresh = Time.timeSinceLevelLoad;

        if (!chased)
        {
            chased = true;
            ending_chase = false;

            chase_source.volume = 1;
            chase_source.Play();
        }
    }

    void FixedUpdate()
    {
        if (ending_chase)
        {
            chase_source.volume -= 0.02f;
        }
        else if (Time.timeSinceLevelLoad > chase_refresh + chase_end_time)
        {
            ending_chase = true;
            chased = false;
        }
    }
}
