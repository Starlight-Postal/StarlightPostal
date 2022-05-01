using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class OptionsMenuBehaviour : MonoBehaviour
{
	
	private VisualElement rve;
	private Button closeButton;
	private Button creditsButton;
	private Button helpButton;
	private Slider sfxSlider;
	private Slider musicSlider;

	public AudioMixer musicMixer;
	public AudioMixer sfxMixer;

	private void OnEnable()
	{
		rve = GetComponent<UIDocument>().rootVisualElement;

		closeButton = rve.Q<Button>("close-button");
		creditsButton = rve.Q<Button>("credits-button");
		helpButton = rve.Q<Button>("help-button");
		sfxSlider = rve.Q<Slider>("vol-sfx");
		musicSlider = rve.Q<Slider>("vol-music");
		
		closeButton.RegisterCallback<ClickEvent>(ev => { OnCloseButtonClick(); });
		creditsButton.RegisterCallback<ClickEvent>(ev => { OnCreditsButtonClick(); });
		helpButton.RegisterCallback<ClickEvent>(ev => { OnHelpButtonClick(); });
		sfxSlider.RegisterValueChangedCallback(vals => { OnSfxSliderChange(vals.previousValue, vals.newValue); }); //Why are these different lmao??
		musicSlider.RegisterValueChangedCallback(vals => { OnMusicSliderChange(vals.previousValue, vals.newValue); });

		rve.visible = false;
	}

	public void ShowMenu()
	{
		float volSfx, volMusic;
		sfxMixer.GetFloat("MasterVol", out volSfx);
		musicMixer.GetFloat("MasterVol", out volMusic);
		sfxSlider.value = Mathf.Pow(10f, volSfx) / 20f;
		musicSlider.value = Mathf.Pow(10f, volMusic) / 20f;
		rve.visible = true;
	}

	private void OnCloseButtonClick()
	{
		rve.visible = false;
	}

	private void OnCreditsButtonClick()
	{
		
	}

	private void OnHelpButtonClick()
	{
		
	}

	private float SliderToDecibels(float invol)
	{
		float vol;
		if (invol == 0)
		{
			vol = -80f;
		}
		else
		{
			vol = 60f * Mathf.Log10(invol) - 100f;
		}
		return vol;
	}

	private void OnSfxSliderChange(float oldVal, float newVal)
	{
		sfxMixer.SetFloat("MasterVol", SliderToDecibels(newVal));
	}

	private void OnMusicSliderChange(float oldVal, float newVal)
	{
		musicMixer.SetFloat("MasterVol", SliderToDecibels(newVal));
	}

}
