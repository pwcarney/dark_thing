using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CityGenerator : MonoBehaviour
{
    int city_size = 20;

    bool placed_player = false;
    public GameObject player;

    List<GameObject> all_enemies = new List<GameObject>();
    public GameObject enemy;

    float ground_size = 40;
    public GameObject ground_prefab;

    float floor_offset = 4f;
    float floor_height = 8f;
    public GameObject floor_prefab;

    public NavMeshSurface nav_mesh;
    public List<Vector3> legal_random_locations = new List<Vector3>();

    void Start()
    {
        for (int i = -city_size; i < city_size; i++)
        {
            for (int j = -city_size; j < city_size; j++)
            {
                Vector3 location = new Vector3(i * ground_size, 0, j * ground_size);
                GameObject ground = Instantiate(ground_prefab, location, Quaternion.identity);

                if (Random.Range(0, 100) < 50)
                {
                    int building_height = Random.Range(1, 10);
                    for (int k = 0; k < building_height; k++)
                    {
                        Instantiate(floor_prefab, location + new Vector3(0, k * floor_height + floor_offset, 0), Quaternion.identity);
                    }
                }
                else if (!placed_player && (i > -5 && i < 5) && (j > -5 && j < 5))
                {
                    placed_player = true;
                    player.transform.position = location;
                }
                else if (Random.Range(0, 100) < 2)
                {
                    GameObject new_enemy = Instantiate(enemy, location, Quaternion.identity);
                    new_enemy.GetComponent<Enemy>().RegisterLegalLocations(legal_random_locations);
                    all_enemies.Add(new_enemy);
                }
                else
                {
                    legal_random_locations.Add(location);
                }
            }
        }

        nav_mesh.BuildNavMesh();
        foreach (GameObject new_enemy in all_enemies)
        {
            new_enemy.GetComponent<Enemy>().Activate();
        }
    }
}
