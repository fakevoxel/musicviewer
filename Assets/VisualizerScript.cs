using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//OLD 
public class VisualizerScript : MonoBehaviour
{
    public VisualizerObject[] visualizerObjects;
    public Color visualizerColor;

    public AudioClip audioClip;
    public bool loop;
    public float volume;

    private AudioSource m_audioSource;

    public float minHeight = 15;
    public float maxHeight = 450;

    public int visualizerSamples = 64;

    void Awake()
    {
        visualizerObjects = GetComponentsInChildren<VisualizerObject>();

        if (audioClip)
        {
            m_audioSource = new GameObject("AudioSource").AddComponent<AudioSource>();
            m_audioSource.volume = volume;
            m_audioSource.loop = loop;
            m_audioSource.clip = audioClip;
            m_audioSource.Play();
        }
    }

    void Update()
    {
        float[] spectrumData = new float[visualizerSamples];
        m_audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.Rectangular);

        for (int i = 0; i < visualizerObjects.Length; i++)
        {
            Vector2 newSize = visualizerObjects[i].gameObject.GetComponent<RectTransform>().rect.size;

            newSize.y = Mathf.Clamp(minHeight + (spectrumData[i * i] * (maxHeight - minHeight) * 10), minHeight, maxHeight);
            visualizerObjects[i].gameObject.GetComponent<RectTransform>().sizeDelta = newSize;

            visualizerObjects[i].gameObject.GetComponent<Image>().color = visualizerColor;
        }
    }
}
