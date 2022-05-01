using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class OptionsMenuBehaviour : MonoBehaviour
{
	
	private const float CONT_APSPECT_RATIO = 1.25f;
	
	private VisualElement rve;
	private VisualElement container;
	private Button closeButton;
	private Button creditsButton;
	private Button eraseButton;
	private Button helpButton;
	private Slider sfxSlider;
	private Slider musicSlider;
	private Slider envSlider;
	private TextField saveFileName;

	public AudioMixer musicMixer;
	public AudioMixer sfxMixer;
	public AudioMixer envMixer;

	private SaveFileManager save;

	private void OnEnable()
	{
		rve = GetComponent<UIDocument>().rootVisualElement;
		
		container = rve.Q<VisualElement>("options-menu-container");

		closeButton = rve.Q<Button>("close-button");
		creditsButton = rve.Q<Button>("credits-button");
		eraseButton = rve.Q<Button>("erase-button");
		helpButton = rve.Q<Button>("help-button");
		sfxSlider = rve.Q<Slider>("vol-sfx");
		musicSlider = rve.Q<Slider>("vol-music");
		envSlider = rve.Q<Slider>("vol-env");
		
		rve.RegisterCallback<GeometryChangedEvent>(ev => { Rescale(); });
		
		closeButton.RegisterCallback<ClickEvent>(ev => { OnCloseButtonClick(); });
		creditsButton.RegisterCallback<ClickEvent>(ev => { OnCreditsButtonClick(); });
		eraseButton.RegisterCallback<ClickEvent>(ev => { OnEraseButtonClick(); });
		helpButton.RegisterCallback<ClickEvent>(ev => { OnHelpButtonClick(); });
		sfxSlider.RegisterValueChangedCallback(vals => { OnSfxSliderChange(vals.previousValue, vals.newValue); }); //Why are these different lmao??
		musicSlider.RegisterValueChangedCallback(vals => { OnMusicSliderChange(vals.previousValue, vals.newValue); });
		envSlider.RegisterValueChangedCallback(vals => { OnEnvSliderChange(vals.previousValue, vals.newValue); });
		saveFileName.RegisterValueChangedCallback(vals => { OnSaveFileNameChange(vals.previousValue, vals.newValue); });

		save = GameObject.FindObjectOfType<SaveFileManager>();
		
		save.LoadPreferences();
		sfxMixer.SetFloat("MasterVol", save.preferences.volSfx);
		musicMixer.SetFloat("MasterVol", save.preferences.volMusic);
		envMixer.SetFloat("MasterVol", save.preferences.volEnv);
		
		rve.visible = false;
	}

	public void ShowMenu()
	{
		save.LoadPreferences();
		sfxSlider.value = Mathf.Pow(10f, save.preferences.volSfx / 20f);
		musicSlider.value = Mathf.Pow(10f, save.preferences.volMusic / 20f);
		envSlider.value = Mathf.Pow(10f, save.preferences.volEnv / 20f);
		
		rve.visible = true;
	}

	private void OnCloseButtonClick()
	{
		save.SavePreferences();
		rve.visible = false;
	}

	private void OnCreditsButtonClick()
	{
		GameObject.FindObjectOfType<global_data>().creditsBackToMenu = true;
		SceneManager.LoadScene("Credits");
	}
	
	private void OnEraseButtonClick()
	{
		GameObject.FindObjectOfType<SaveFileDeleteUI>().ConfirmDelete();
	}

	private void OnHelpButtonClick()
	{ 
		Application.OpenURL("https://www.youtube.com/watch?v=dQw4w9WgXcQ"); //Rick roll ( ͡° ͜ʖ ͡°)
	}

	private float Sl2Db(float invol)
	{
		if (invol == 0)
		{
			return -80f;
		}
		else
		{
			return 20f * Mathf.Log10(invol);
		}
	}

	private void OnSfxSliderChange(float oldVal, float newVal)
	{
		save.preferences.volSfx = Sl2Db(newVal);
		sfxMixer.SetFloat("MasterVol", Sl2Db(newVal));
	}

	private void OnMusicSliderChange(float oldVal, float newVal)
	{
		save.preferences.volMusic = Sl2Db(newVal);
		musicMixer.SetFloat("MasterVol", Sl2Db(newVal));
	}

	private void OnEnvSliderChange(float oldVal, float newVal)
	{
		save.preferences.volEnv = Sl2Db(newVal);
		envMixer.SetFloat("MasterVol", Sl2Db(newVal));
	}

	private void OnSaveFileNameChange(string oldVal, string newVal)
	{
		save.preferences.saveFileName = newVal;
	}
	
	private void Rescale() {
		Debug.Log("Resizing options menu UI to new screen size");
        
		float contWidth;
		float contHeight;
        
		if (Screen.width <= Screen.height) {
			contWidth = Screen.width * 0.8f;
			contHeight = contWidth * (1f / CONT_APSPECT_RATIO);
		} else {
			contHeight = Screen.height * 0.66f;
			contWidth = contHeight * CONT_APSPECT_RATIO;
		}

#if PLATFORM_ANDROID
        contHeight /= 2;
        contWidth /= 2;
        if ((float) Screen.width / (float) Screen.height >= 2)
        {
            contHeight /= 2;
            contWidth /= 2;
        }
#endif
            
		container.style.height = contHeight;
		container.style.width = contWidth;
	}

}
