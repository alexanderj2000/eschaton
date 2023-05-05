using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
	public WorldData world;

	public void StartNewGame()
	{
		SceneManager.LoadScene("Game");
		world.StartGame();
	}

	void Awake()
	{
		world = GameObject.Find("World").GetComponent<WorldData>();
	}
}
