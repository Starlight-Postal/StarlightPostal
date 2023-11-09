using System;
using UnityEditor.SceneManagement;
using UnityEngine;

public class CreditInsertSFXHandler : MonoBehaviour
{
    [SerializeField]
    private AudioClip clip;
    
    private AudioSource audioSource;
    
    private void Start()
    {
        FindObjectOfType<CreditAccumulator>().onCreditInserted += OnCreditInserted;
        audioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
    }

    public void OnCreditInserted()
    {
        audioSource.PlayOneShot(clip);
    }
}
