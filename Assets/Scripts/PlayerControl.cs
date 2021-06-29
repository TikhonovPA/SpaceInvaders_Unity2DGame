using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public float speed = 5f, enemySpeed; //Скорость движения игрока и астероидов-противников
    public float offset; //Переменная, чтобы убрать смещение курсора при вращении игрока
    private Rigidbody2D _rb;
    public GameObject ammo, death;
    public GameObject[] enemies; //С помощью этого будем создавать случайного противника, чтобы был элемент рандома
    private GameObject enemy_inst_down; //Снаряд, выпускаемый игроком, враг и спавн врага
    public Transform shotDir; //Переменная для направления стрельбы
    private AudioSource shoot; //Для звука стрельбы

    public static int count = 0; //Счетчик очков
    public Text Score, Lose;
    public Button Restart;

    private float timeShot; //Переменная для времени между выстрелами
    public float startTime; //Разница во времени между выстрелами

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
            float moveHorizontal = Input.GetAxis("Horizontal"); //Устанавливаем в переменную изменения от -1 до 1 по оси X (нажатие a/d, левая/правая стрелки)
            float moveVertical = Input.GetAxis("Vertical"); //Устанавливаем в переменную изменения от -1 до 1 по оси Y (нажание w/s, стрелки вверх/вниз)

            Vector2 movement = new Vector2(moveHorizontal, moveVertical); //Объявляем новый вектор, который будет изменяться в зависимости от двух предыдущих переменных

            _rb.AddForce(movement * speed * Time.deltaTime); //Добавляем силу к нашему игроку, чтобы он двигался по вектору movement с объявленной скоростью speed

            Vector2 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; //Вычисляем разницу в координатах между курсором и игроком
            float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; //Здесь хранится градус между курсором и положением игрока
            transform.rotation = Quaternion.Euler(0, 0, rotateZ + offset); // поворот игрока со смещением offset

            if (timeShot <= 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    shoot.Play();
                    Instantiate(ammo, shotDir.position, Quaternion.Euler(0, 0, rotateZ + offset + 51)); // выстрел с корректировкой угла снаряда
                    timeShot = startTime;
                }
            }
            else
            {
                timeShot -= Time.deltaTime;
            }

            if (!Ammo.enemyKilled)
            { //Условие, при котором проверяется, уничтожен астероид или нет
                return;
            }
            else
            {
                DetectClicks.EnemySpawn(enemy_inst_down, enemies);
                Ammo.enemyKilled = false;
            }

        }
    }
    void OnCollisionEnter2D(Collision2D other) //Поражение игрока
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
