using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{

    private float length, startpos;
    public GameObject cam;
    public float parallaxEffect;
    // Start is called before the first frame update
    void Start()
    {
        //The startpos is set the the X position of the object that the script is attached to.
        startpos = transform.position.x;
        //It gets the length of the sprite on the sprite renderer.
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        // How far the object has moved relative tot he camera assigned on the object.
        float temp = cam.transform.position.x * (1 - parallaxEffect);
        //This will take in account of how far the object is moved in world space.
        float dist = (cam.transform.position.x * parallaxEffect);
        //
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);
        //When the camera goes past the length of the sprite it will add more on the other side of movement.
        if (temp > startpos + length)
        {
            startpos += length;
        }
        //When the camera goes past the length of the sprite it will add more on the other side of movement.
        else if ( temp < startpos - length)
        {
            startpos -= length;
        }
    }
}
