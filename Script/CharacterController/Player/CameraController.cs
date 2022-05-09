using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    Vector3 offset = new Vector3( 0, 3, -12);

    // void Start(){
    //     transform.rotation = Quaternion.Euler( 10, 0, 0);
    //     offset = transform.position - player.transform.position;
    // }

    void LateUpdate(){
        transform.position = player.transform.position + offset;
    }
}
