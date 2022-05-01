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
	private Slider envSlider;

	public AudioMixer musicMixer;
	public AudioMixer sfxMixer;
	public AudioMixer envMixer;

	private void OnEnable()
	{
		rve = GetComponent<UIDocument>().rootVisualElement;

		closeButton = rve.Q<Button>("close-button");
		creditsButton = rve.Q<Button>("credits-button");
		helpButton = rve.Q<Button>("help-button");
		sfxSlider = rve.Q<Slider>("vol-sfx");
		musicSlider = rve.Q<Slider>("vol-music");
		envSlider = rve.Q<Slider>("vol-env");
		
		closeButton.RegisterCallback<ClickEvent>(ev => { OnCloseButtonClick(); });
		creditsButton.RegisterCallback<ClickEvent>(ev => { OnCreditsButtonClick(); });
		helpButton.RegisterCallback<ClickEvent>(ev => { OnHelpButtonClick(); });
		sfxSlider.RegisterValueChangedCallback(vals => { OnSfxSliderChange(vals.previousValue, vals.newValue); }); //Why are these different lmao??
		musicSlider.RegisterValueChangedCallback(vals => { OnMusicSliderChange(vals.previousValue, vals.newValue); });
		envSlider.RegisterValueChangedCallback(vals => { OnEnvSliderChange(vals.previousValue, vals.newValue); });

		rve.visible = false;
	}

	public void ShowMenu()
	{
		float volSfx, volMusic, volEnv;
		sfxMixer.GetFloat("MasterVol", out volSfx);
		musicMixer.GetFloat("MasterVol", out volMusic);
		envMixer.GetFloat("MasterVol", out volEnv);
		sfxSlider.value = Mathf.Pow(10f, volSfx / 20f);
		musicSlider.value = Mathf.Pow(10f, volMusic / 20f);
		envSlider.value = Mathf.Pow(10f, volEnv / 20f);
		
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
		sfxMixer.SetFloat("MasterVol", Sl2Db(newVal));
	}

	private void OnMusicSliderChange(float oldVal, float newVal)
	{
		musicMixer.SetFloat("MasterVol", Sl2Db(newVal));
	}

	private void OnEnvSliderChange(float oldVal, float newVal)
	{
		envMixer.SetFloat("MasterVol", Sl2Db(newVal));
	}

}
