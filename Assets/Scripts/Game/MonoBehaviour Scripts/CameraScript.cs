using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    public GameObject lowerBarrier;
    public GameObject upperBarrier;

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, Mathf.Clamp(player.transform.position.y, 8f, 120f), transform.position.z);
        lowerBarrier.transform.position = new Vector3(player.transform.position.x, -0.5f, upperBarrier.transform.position.z);
        upperBarrier.transform.position = new Vector3(player.transform.position.x, 127.5f, upperBarrier.transform.position.z);
    }
}
