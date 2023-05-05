using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public GameObject player;
    public GameObject sun;
    public GameObject moon;
    public GameObject skyDay;
    public GameObject skyNight;

    public GameObject musicPlayer;
    public AudioClip dayTheme;
    public AudioClip nightTheme;

    public float distance = 10f;

    public WorldData world;

    IEnumerator OnTick()
    {
        while (true)
        {
            float timeOfDay = world.gameTime / world.dayLength;
            float dayElapsed = timeOfDay - Mathf.Floor(world.gameTime / world.dayLength);

            float moonAngle = ((360f * dayElapsed) - 90f) * Mathf.Deg2Rad;
            moon.transform.position = new Vector3(player.transform.position.x + distance * Mathf.Cos(moonAngle), (player.transform.position.y - 10f) + distance * 1.5f * Mathf.Sin(moonAngle), moon.transform.position.z);

            float sunAngle = ((360f * dayElapsed) - 270f) * Mathf.Deg2Rad;
            sun.transform.position = new Vector3(player.transform.position.x + distance * Mathf.Cos(sunAngle), (player.transform.position.y - 10f) + distance * 1.5f * Mathf.Sin(sunAngle), sun.transform.position.z);
            
            if (dayElapsed == 0.25f)
            {
                skyDay.SetActive(false);
                musicPlayer.GetComponent<AudioSource>().clip = nightTheme;
                musicPlayer.GetComponent<AudioSource>().Play(0);
            }
            else
            {
                if (dayElapsed == 0.75f)
                {
                    skyDay.SetActive(true);
                    musicPlayer.GetComponent<AudioSource>().clip = dayTheme;
                    musicPlayer.GetComponent<AudioSource>().Play(0);
                }
            }

            yield return new WaitForSeconds(world.tickTime);
        }
    }

    void Start()
    {
        StartCoroutine(OnTick());
    }

    void Awake()
    {
        world = GameObject.Find("World").GetComponent<WorldData>();
    }
}
