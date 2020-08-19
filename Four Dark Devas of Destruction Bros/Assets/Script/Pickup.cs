using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup: MonoBehaviour
{
    public AudioClip collectSound;
    public int pointValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
            GameManager.instance.playerPoint += 100;
            GameManager.instance.Victory();
        }
    }
}
