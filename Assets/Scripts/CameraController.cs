using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    public GameObject PlayerGO;
    Vector3 posOffset = new Vector3(0, 1.0f, -4.0f);
    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, PlayerGO.transform.position + posOffset, 0.1f);
    }
}
