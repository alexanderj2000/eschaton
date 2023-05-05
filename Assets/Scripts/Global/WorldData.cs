using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldData : MonoBehaviour
{
    public static WorldData World;

    public delegate void Change();
    public static event Change SceneChanged;

    public string worldName;
    public string worldFilepath;
    public DifficultyMode difficulty;
    public DisasterType disaster;
    public int prepTime;
    public float dayLength = 1440f;
    public float gameTime;
    public float seed = 0.123f;
    public float tickTime = 1f / 4f;

    private string scene;

    IEnumerator GameTimeTick()
    {
        while (true)
        {
            gameTime += tickTime;
            yield return new WaitForSeconds(tickTime);
        }
    }

    public void StartGame()
    {
        StartCoroutine(GameTimeTick());
    }

    public void StopGame()
    {
        StopCoroutine(GameTimeTick());
    }

    private void Awake()
    {
        if (World == null)
        {
            World = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}