using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    GameObject player;
    int player_seek_range = 100;
    public LayerMask wall_mask;

    MusicController music;

    bool activated = false;
    Vector3 current_destination;
    List<Vector3> legal_random_locations;

    float talk_time;
    public AudioClip howl;
    public AudioClip[] chase;

    void Start()
    {
        player = GameObject.Find("Player");
        music = FindObjectOfType<MusicController>();
    }

    public void RegisterLegalLocations(List<Vector3> legal_random_locations)
    {
        this.legal_random_locations = legal_random_locations;
    }

    public void Activate()
    {
        activated = true;

        SetTalkTime(30);

        ChooseLocation();
    }

    void ChooseLocation()
    {
        current_destination = legal_random_locations[Random.Range(0, legal_random_locations.Count)];
    }

    void SetTalkTime(int time_set)
    {
        talk_time = Random.Range(Time.timeSinceLevelLoad, time_set + Time.timeSinceLevelLoad);
    }

    void Update()
    {
        if (activated)
        {
            bool chased = false;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit, player_seek_range))
            {
                if (hit.collider.name == "Trigger")
                {
                    chased = true;
                    Chase();
                }
            }

            if (!chased)
            {
                Seek();
            }
        }
    }

    void Seek()
    {
        GetComponent<NavMeshAgent>().SetDestination(current_destination);
        if (Vector3.Distance(transform.position, current_destination) < 0.1f)
        {
            ChooseLocation();
        }

        if (Time.timeSinceLevelLoad > talk_time)
        {
            SetTalkTime(30);
            GetComponent<AudioSource>().clip = howl;
            GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1.1f);
            GetComponent<AudioSource>().Play();
        }
    }

    void Chase()
    {
        // To chase player, mark where player is when you lose sight of him, if you move there and don't see him, stop chasing?
        GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
        if (Vector3.Distance(transform.position, player.transform.position) < 1.5f)
        {
            SceneManager.LoadScene("Main");
        }

        if (Time.timeSinceLevelLoad > talk_time)
        {
            SetTalkTime(10);
            GetComponent<AudioSource>().clip = chase[Random.Range(0, chase.Length)];
            GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1.1f);
            GetComponent<AudioSource>().Play();
        }

        music.Chasing();
    }
}
