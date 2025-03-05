using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject background;
    public UI_ColorPicker backgroundChooser;

    public GameObject visualizerContainer;
    public GameObject barPrefab;
    public GameObject linePrefab;

    public Color greenScreen;
    public Color blueScreen;

    // Visualizer Size
    public float minHeight;
    public float maxHeight;
    public float heightMultiplier;
    public int visualizerSampleCount;

    public Color visualizerColor;

    void Awake() {
        Utils.uiManager = this;
        Utils.backgroundColorPicker = backgroundChooser;
        Utils.canvasTransform = gameObject.transform;

        Application.targetFrameRate = 60;
    }

    void Start() {
        SetBackgroundType("Green Screen");
        AddVisualizer("bar");

        SetBackgroundColor(Utils.backgroundColorPicker.GetColor());
    }

    void Update() {
        SetBackgroundColor(Utils.backgroundColorPicker.GetColor());
    }

    public void ClearVisualizers() {
        for (int i = visualizerContainer.transform.childCount - 1; i >= 0; i--) {
            Destroy(visualizerContainer.transform.GetChild(i).gameObject);
        }
    }

    public void MoveVisualizer(Slider _input) {
        visualizerContainer.transform.GetChild(0).localPosition = new Vector3(0, (_input.value - 0.5f) * Screen.height, 0);
    }

    public void CreateVisualizer() {
        ClearVisualizers();
        AddVisualizer("bar");
    }

    public void SetVisualizerCount(TMP_InputField _input) {
        visualizerSampleCount = int.Parse(_input.text);
    }

    public void SetVisualizerAttribute(Slider _input) {
        if (_input.gameObject.name.Equals("1")) {
            minHeight = _input.value * 5;
        } 
        else if (_input.gameObject.name.Equals("2")) {
            maxHeight = _input.value * 1000 + 100;
        }
        else if (_input.gameObject.name.Equals("3")) {
            heightMultiplier = _input.value * 20 + 1;
        }
    }

    public void HandleVisualizerType(TMP_Dropdown _input) {
        ClearVisualizers();

        if (_input.value == 0) {
            AddVisualizer("bar");
        }
        else if (_input.value == 1) {
            AddVisualizer("line");
        }
    }

    public void SetBackgroundType(TMP_Dropdown _input) {
        float h;
        float s;
        float v;

        if (_input.value == 0) {
            Color.RGBToHSV(greenScreen, out h, out s, out v);
            backgroundChooser.h = h;
        }
        else if (_input.value == 1) {
            Color.RGBToHSV(blueScreen, out h, out s, out v);
            backgroundChooser.h = h;
        }

        SetBackgroundColor(backgroundChooser.GetColor());
    }

    public void SetBackgroundType(string _input) {
        float h;
        float s;
        float v;

        if (_input.Equals("Green Screen")) {
            Color.RGBToHSV(greenScreen, out h, out s, out v);
            backgroundChooser.h = h;
        }
        else if (_input.Equals("Blue Screen")) {
            Color.RGBToHSV(blueScreen, out h, out s, out v);
            backgroundChooser.h = h;
        }

        SetBackgroundColor(backgroundChooser.GetColor());
    }

    public void SetBackgroundColor(Color _input) {
        background.GetComponent<Image>().color = _input;
    }

    public void AddVisualizer(string _type) {
        if (_type.Equals("bar")) {
            GameObject newObject = Instantiate(barPrefab, Vector3.zero, Quaternion.identity);

            newObject.transform.SetParent(visualizerContainer.transform);
            newObject.transform.localPosition = Vector3.zero;

            Visualizer_Bar comp = newObject.GetComponent<Visualizer_Bar>();

            comp.spectrumSource = Utils.audioManager.gameObject.GetComponent<AudioSource>();
            comp.Initialize(minHeight, maxHeight, heightMultiplier, visualizerColor, visualizerSampleCount);
        }
        else if (_type.Equals("line")) {
            
        }
    }
}
