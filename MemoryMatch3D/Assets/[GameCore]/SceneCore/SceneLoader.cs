using System;
using System.Collections;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Core
{
    public class SceneLoader : MMSingleton<SceneLoader>
    {
        [field: SerializeField] public string MainScene { get; private set; }
        [field: SerializeField] public string GameScene { get; private set; }
        [FormerlySerializedAs("mmfSceneLoader")] [SerializeField] private MMF_Player mmfPlayer;
        private MMF_LoadScene _mmSceneLoadingManager;
        public string CurrentSceneName { get; private set; }

        private void Awake()
        {
            _mmSceneLoadingManager = mmfPlayer.GetFeedbackOfType<MMF_LoadScene>();
            DontDestroyOnLoad(gameObject);
        }
        
        [Obsolete("Obsolete")]
        public bool LoadScene(string sceneName)
        {
            if (Application.CanStreamedLevelBeLoaded(sceneName))
            {
                CurrentSceneName = sceneName;
                // Application.LoadLevel(sceneName);
                StartCoroutine(Load(sceneName));
                return true;
            }
            return false;
        }

        
        public void MMLoadGameLevel(string name)
        {
            _mmSceneLoadingManager.DestinationSceneName = name;
            mmfPlayer.PlayFeedbacks();
        }
        
        public void LoadLevel(int index)
        {
            StartCoroutine(Load(index));
        }
    
        public void RestartLevel()
        {
            StartCoroutine(Load(SceneManager.GetActiveScene().buildIndex));
        }


        private IEnumerator Load(int index)
        {
            Debug.Log("Scene Load by Async Loader");
            // PlayerPrefs.SetInt("level", index);
            yield return  new WaitForEndOfFrame();
            AsyncOperation operation = SceneManager.LoadSceneAsync(index);
            while (!operation.isDone)
            {
                float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
                yield return null;
            }
        }
        private IEnumerator Load(string name)
        {
            yield return  new WaitForEndOfFrame();
            AsyncOperation operation = SceneManager.LoadSceneAsync(name);
            while (!operation.isDone)
            {
                float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
                yield return null;
            }
        }
    }
}