using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Threading.Tasks;
using TMPro;

// TEST: C:\Users\maxim\Desktop\Music Files/Context Sensitive - Weekly Loops Season 3 (Special Edition) - 68 104 - Advance (lopable).wav
 
public class AudioManager : MonoBehaviour {
    private string trackDirectory = "file:///c:/Folder/funny.wav";
    public AudioClip audioTrack;

    void Awake() {
        Utils.audioManager = this;
    }

    void Update() {
        if (Input.GetKeyDown("p")) {
            GetComponent<AudioSource>().Play();
        }
    }

    public void SetDirectory(TMP_InputField _input) {
        trackDirectory = _input.text;
    }

    // Get an audio file from disk and put it in the audio source
    public async void SetupAudio() {
        if (!string.IsNullOrEmpty(trackDirectory)) {
            audioTrack = await GetAudioClip(trackDirectory, AudioType.WAV);

            // Attach the audioclip to the audio source
            GetComponent<AudioSource>().clip = audioTrack;
        }
    }

    public async Task<AudioClip> GetAudioClip(string filePath, AudioType fileType)
    {
 
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(filePath, fileType))
        {
            var result = www.SendWebRequest();
 
            while (!result.isDone) { await Task.Delay(100); }
 
            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
                return null;
            }
            else
            {
                return DownloadHandlerAudioClip.GetContent(www);
            }
        }
    }
}