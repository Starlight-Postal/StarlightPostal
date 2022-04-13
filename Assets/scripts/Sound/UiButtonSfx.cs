using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Audio;

public class UiButtonSfx : MonoBehaviour {

    public AudioMixerGroup mixer;

    private AudioSource source;
    private AudioClip[] clicks;
    private AudioClip[] hovers;

    private void OnEnable() {
        var rve = GetComponent<UIDocument>().rootVisualElement;

        var buttons = rve.Query<Button>();

        buttons.ForEach(button => {
            button.RegisterCallback<ClickEvent>(ev => { OnUiButtonClick(); });
            button.RegisterCallback<MouseEnterEvent>(ev => { OnUiButtonHover(); });
        });
    }

    private void Start() {
        source = gameObject.AddComponent<AudioSource>();
        source.outputAudioMixerGroup = mixer;
        clicks = Resources.LoadAll<AudioClip>("audio/SFX/menu/click");
        hovers = Resources.LoadAll<AudioClip>("audio/SFX/menu/hover");
    }

    public void OnUiButtonClick() {
        source.clip = clicks[Random.Range(0,clicks.Length - 1)];
        source.Play();
    }

    public void OnUiButtonHover() {
        source.clip = hovers[Random.Range(0,hovers.Length - 1)];
        source.Play();
    }

}
