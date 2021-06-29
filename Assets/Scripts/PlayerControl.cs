using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public float speed = 5f, enemySpeed; //�������� �������� ������ � ����������-�����������
    public float offset; //����������, ����� ������ �������� ������� ��� �������� ������
    private Rigidbody2D _rb;
    public GameObject ammo, death;
    public GameObject[] enemies; //� ������� ����� ����� ��������� ���������� ����������, ����� ��� ������� �������
    private GameObject enemy_inst_down; //������, ����������� �������, ���� � ����� �����
    public Transform shotDir; //���������� ��� ����������� ��������
    private AudioSource shoot; //��� ����� ��������

    public static int count = 0; //������� �����
    public Text Score, Lose;
    public Button Restart;

    private float timeShot; //���������� ��� ������� ����� ����������
    public float startTime; //������� �� ������� ����� ����������

    private void Start()
    {
            shoot = GetComponent<AudioSource>();
            _rb = GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        if (DetectClicks.clicked)
        {
            if (transform.position.x > 10.5f || transform.position.x < -11f || transform.position.y > 5.5f || transform.position.y < -5.5f)
            {
                Destroy(gameObject);
                Instantiate(death, transform.position, Quaternion.identity);
                Lose.gameObject.SetActive(true);
                Restart.gameObject.SetActive(true);
            }

            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }

            Score.text = "SCORE: " + count;
            float moveHorizontal = Input.GetAxis("Horizontal"); //������������� � ���������� ��������� �� -1 �� 1 �� ��� X (������� a/d, �����/������ �������)
            float moveVertical = Input.GetAxis("Vertical"); //������������� � ���������� ��������� �� -1 �� 1 �� ��� Y (������� w/s, ������� �����/����)

            Vector2 movement = new Vector2(moveHorizontal, moveVertical); //��������� ����� ������, ������� ����� ���������� � ����������� �� ���� ���������� ����������

            _rb.AddForce(movement * speed * Time.deltaTime); //��������� ���� � ������ ������, ����� �� �������� �� ������� movement � ����������� ��������� speed

            Vector2 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; //��������� ������� � ����������� ����� �������� � �������
            float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; //����� �������� ������ ����� �������� � ���������� ������
            transform.rotation = Quaternion.Euler(0, 0, rotateZ + offset); // ������� ������ �� ��������� offset

            if (timeShot <= 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    shoot.Play();
                    Instantiate(ammo, shotDir.position, Quaternion.Euler(0, 0, rotateZ + offset + 51)); // ������� � �������������� ���� �������
                    timeShot = startTime;
                }
            }
            else
            {
                timeShot -= Time.deltaTime;
            }

            if (!Ammo.enemyKilled)
            { //�������, ��� ������� �����������, ��������� �������� ��� ���
                return;
            }
            else
            {
                DetectClicks.EnemySpawn(enemy_inst_down, enemies);
                Ammo.enemyKilled = false;
            }

        }
    }
    void OnCollisionEnter2D(Collision2D other) //��������� ������
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Instantiate(death, transform.position, Quaternion.identity);
            Lose.gameObject.SetActive(true);
            Restart.gameObject.SetActive(true);
        }
    }
}
