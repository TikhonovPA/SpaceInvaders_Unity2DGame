using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectClicks : MonoBehaviour
{
    public Text Title, startGame;
    public static bool clicked = false;
    public GameObject[] enemies;
    private GameObject enemy_inst_down;

    private void OnMouseDown()
    {
        if (!clicked)
        {
            clicked = true;
            Title.gameObject.SetActive(false);
            startGame.gameObject.SetActive(false);
            EnemySpawn(enemy_inst_down, enemies);
        }
    }

    public static void EnemySpawn(GameObject enemy_inst_down, GameObject[] enemies) //Функция для появления противников снизу
    {
        enemy_inst_down = Instantiate(enemies[Random.Range(0, enemies.Length)], new Vector3(Random.Range(-16, 16), -6.3f, 0), Quaternion.identity); //Создание противника
    }
}
