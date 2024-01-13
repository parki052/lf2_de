using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToPlayerController : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        var pos = player.transform.position;
        pos.x += 0.25f;
        pos.y -= 1.1f;
        transform.position = pos;
    }
}
