using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ammo : MonoBehaviour
{
    public float speed; //�������� ������ �������
    public float destroyTime; //����� ��� ����������� �������
    public static bool enemyKilled = false;
    

    private void Start()
    {
        Invoke("DestroyAmmo", destroyTime); // ����� �������� ����� ���������� ������
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            enemyKilled = true;
            PlayerControl.count++;
        }
    }

    void DestroyAmmo() 
    {
        Destroy(gameObject);
    }
}
