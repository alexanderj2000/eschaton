using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
	// Screens
	public GameObject currentScreen;

	public GameObject StartMenu;
	public GameObject PlayMenu;
	public GameObject NewGameMenu;
	public GameObject LoadGameMenu;
	public GameObject OptionsMenu;

	// Button Functions
	void GoTo(GameObject target)
	{
		currentScreen.SetActive(false);
		currentScreen = target;
		currentScreen.SetActive(true);
	}
	
	public void GoToStartMenu()
	{
		GoTo(StartMenu);
	}

	public void GoToPlayMenu()
	{
		GoTo(PlayMenu);
	}

	public void GoToNewGameMenu()
	{
		GoTo(NewGameMenu);
	}

	public void GoToLoadGameMenu()
	{
		GoTo(LoadGameMenu);
	}

	public void GoToOptionsMenu()
	{
		GoTo(OptionsMenu);
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}
