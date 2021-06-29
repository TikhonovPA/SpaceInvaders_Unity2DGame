using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Destroy : MonoBehaviour
{
    public GameObject explosion, enemy3, player;
    private void Start()
    {
        
    }
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2 (Random.Range(-13f,13f),10f), 2f * Time.deltaTime);
        if (transform.position.y > 6.2f)
            Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ammo"))
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, Quaternion.identity);
            Instantiate(enemy3, new Vector2(transform.position.x + Random.Range(0f, 2f), transform.position.y + Random.Range(0f, 2f)), Quaternion.identity);
            Instantiate(enemy3, new Vector2(transform.position.x + Random.Range(0f, -2f), transform.position.y + Random.Range(0f, -2f)), Quaternion.identity);
        }

    }
}
