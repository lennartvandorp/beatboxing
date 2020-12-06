using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    public float screenDistance;
    Vector3 velocity;
    public float speedMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = player.transform.position - new Vector3(0, 0, screenDistance);
    }

    // Update is called once per frame
    void Update()
    {
        velocity = Distance(transform.position, player.transform.position) * (player.transform.position - transform.position);
        velocity = Vector3.Scale(velocity, new Vector3(1, 1, 0)) * speedMultiplier;
        transform.position += velocity * Time.deltaTime;
    }
    private float Distance(Vector3 a, Vector3 b)
    {
        return (float)math.sqrt(math.pow(a.x - b.x, 2) + math.pow(a.y - b.y, 2));
    }

}