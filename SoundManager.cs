using System;
using UnityEngine;

namespace GPWS {
    [KSPAddon(KSPAddon.Startup.MainMenu, false)]
    class SoundManager: MonoBehaviour {
        public static AudioClip bankAngleWarning, dontSinkWarning, stallWarning, terrainPullUpWarning;
        public static AudioSource audioSource;
        public void Start() {
            bankAngleWarning = GameDatabase.Instance.GetAudioClip("PullUpWarning/Sounds/BankAngleWarning");
            dontSinkWarning = GameDatabase.Instance.GetAudioClip("PullUpWarning/Sounds/DontSinkWarning");
            stallWarning = GameDatabase.Instance.GetAudioClip("PullUpWarning/Sounds/StallWarning");
            terrainPullUpWarning = GameDatabase.Instance.GetAudioClip("PullUpWarning/Sounds/TerrainPullUpWarning");

            if (bankAngleWarning == null) {
                Debug.LogError("bankAngleWarning Sound not found.");
            } else if (dontSinkWarning == null) {
                Debug.LogError("dontSinkWarning Sound not found.");
            } else if (stallWarning == null) {
                Debug.LogError("stallWarning Sound not found.");
            } else if (terrainPullUpWarning == null) {
                Debug.LogError("terrainPullUpWarning Sound not found.");
            }

            audioSource = gameObject.GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
        }

        public void Update() {
        }


        public void PlaySound(AudioClip clip, float volume) {
            if (!audioSource.isPlaying) {
                audioSource.PlayOneShot(clip, volume);
            }
        }

        public void StopSound() {
            audioSource.Stop();
        }
    
    }
}
