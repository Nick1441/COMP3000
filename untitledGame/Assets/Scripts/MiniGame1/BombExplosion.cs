using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosion : MonoBehaviour
{

    public GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Trigger", 3.0f);
    }

    // Update is called once per frame
    void Trigger()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity); //1

        GetComponent<MeshRenderer>().enabled = false;
        Destroy(gameObject, .3f); //4

    }
}
