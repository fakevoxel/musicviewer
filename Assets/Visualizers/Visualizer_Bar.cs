using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Visualizer_Bar : MonoBehaviour
{
    private List<GameObject> components;
    public GameObject barPrefab;
    public AudioSource spectrumSource;
    public int sampleCount;
    public int componentCount;

    private float screenWidth = Screen.width;

    public float maxHeight;
    public float minHeight;
    public float heightMultiplier;

    public Color componentColor;

    public void Initialize(float _min, float _max, float _mult, Color _col, int _count) {
        minHeight = _min;
        maxHeight = _max;
        heightMultiplier = _mult;
        componentColor = _col;

        componentCount = _count;
        sampleCount = 4096;

        components = new List<GameObject>();

        // Create the bars used for the visualization
        for (int i = 0; i < componentCount; i++) {
            GameObject newObject = Instantiate(barPrefab, Vector3.zero, Quaternion.identity);

            newObject.transform.SetParent(transform);
            newObject.transform.localPosition = new Vector3(-screenWidth / 2 + screenWidth / componentCount * i, 0, 0);
            newObject.GetComponent<Image>().color = componentColor;

            // Add object to components list
            components.Add(newObject);
        }
    }

    void Update()
    {
        if (spectrumSource != null && components != null) {
            float[] spectrumData = new float[sampleCount];
            spectrumSource.GetSpectrumData(spectrumData, 0, FFTWindow.Blackman);

            for (int i = 0; i < components.Count; i++)
            {
                Vector2 newSize = components[i].gameObject.GetComponent<RectTransform>().rect.size;

                // Min 15 max 800
                newSize.y = Mathf.Clamp(minHeight + (spectrumData[Mathf.RoundToInt(Mathf.Pow((float)i / componentCount, 4) * sampleCount)] * (maxHeight - minHeight) * heightMultiplier), minHeight, maxHeight);
                components[i].gameObject.GetComponent<RectTransform>().sizeDelta = newSize;

                // No color stuff for now
                //components[i].gameObject.GetComponent<Image>().color = visualizerColor;
            }
        }
    }
}
