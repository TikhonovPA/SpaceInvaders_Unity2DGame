using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Destroy : MonoBehaviour
{
    public GameObject explosion, player;
    private void Start()
    {
        
    }
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(Random.Range(-13f, 13f), 10f), 2f * Time.deltaTime);
        if (transform.position.y > 6.2f)
            Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ammo"))
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, Quaternion.identity);
        }

    }
}
