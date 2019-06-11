using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    GameObject player;

    bool activated = false;
    Vector3 current_destination;
    List<Vector3> legal_random_locations;

    float talk_time;
    public AudioClip howl;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    public void RegisterLegalLocations(List<Vector3> legal_random_locations)
    {
        this.legal_random_locations = legal_random_locations;
    }

    public void Activate()
    {
        activated = true;

        SetTalkTime();

        ChooseLocation();
    }

    void ChooseLocation()
    {
        current_destination = legal_random_locations[Random.Range(0, legal_random_locations.Count)];
    }

    void SetTalkTime()
    {
        talk_time = Random.Range(Time.timeSinceLevelLoad, 30 + Time.timeSinceLevelLoad);
    }

    void Update()
    {
        if (activated)
        {
            GetComponent<NavMeshAgent>().SetDestination(current_destination);
            if (Vector3.Distance(transform.position, current_destination) < 0.1f)
            {
                ChooseLocation();
            }

            if (Time.timeSinceLevelLoad > talk_time)
            {
                SetTalkTime();
                GetComponent<AudioSource>().clip = howl;
                GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1.1f);
                GetComponent<AudioSource>().Play();
            }
        }
    }
}
