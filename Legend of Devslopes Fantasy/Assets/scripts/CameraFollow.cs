using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

//when the player moves around, the camera is following along with it
//reference to the target, the hero
//tell the camera to move

public class CameraFollow : MonoBehaviour
{

    [SerializeField]
    Transform target;
    [SerializeField]
    float smoothing = 5f;

    Vector3 offset;

    void Awake()
    {
        Assert.IsNotNull(target);
    }

    // Start is called before the first frame update
    void Start()
    {
        //position of the camera minus the position of the player
        offset = transform.position - target.position;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetCamePos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamePos, smoothing * Time.deltaTime);
        //linear interplation between two vectors
        //starting pos, end pos, and time between them

    }
}
