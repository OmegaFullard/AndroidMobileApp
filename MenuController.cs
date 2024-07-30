//MenuController



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MinoBehaviour
{
	[Header("Volume Setting")]
	[SerializedField] private Text volumeTextValue = null;
	//Text Mesh Pro [SerializedField] private TMP_Text volumeTextValue = null;
	[SerializedField] Slider volumeSlider = null;
	[SerializedField] private float defaultVolume = 1.0f;
	
	[SerializedField] private GameObject confirmationPrompt = null;
	
	[Header("Levels To Load")]
	public string _newGameLevel;
	private string levelToLoad;
	[SerializedField]  private GameObject noSavedGameDialog = null;
	
	
}

public void NewGameDialogYes()
{
	SceneManager.LoadScene(_newGameLevel);
	
}

public void LoadGameDialogYes()
{
	if (PlayerPrefs.HasKey("SavedLevel"))
	{
		levelToLoad = PlayerPrefs.GetString("SavedLevel");
		SceneManager.LoadScene(levelToLoad);
	}
	else
	{
		noSavedGameDialog.SetActive(true);
	}
}

public void ExitButton()
{
	Application.Quit();
}

public void SetVolume(float volume)
{
	AudioListener.volume = volume;
	volumeTextValue.text = volume.ToString("0.0");
	
}

public void VolumeApply()
{
	PlayerPrefs.SetFloat("mastervolume", AudioListener.volume);
StartCoroutine(ConfirmationBox());	
}

public IEnumerator ConfirmationBox()
{
	confirmationPrompt.SetActive(true);
	yield return new WaitForSectonds(2);
	confirmationPrompt.SetActive(false);
}

