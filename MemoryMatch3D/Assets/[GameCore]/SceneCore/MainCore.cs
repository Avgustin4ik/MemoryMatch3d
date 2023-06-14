using System;
using Entitas;
using MoreMountains.Tools;
using UnityEngine;

namespace Core
{
    public class MainCore : MMSingleton<MainCore>
    {
        [SerializeField] private SceneLoader sceneLoader;
        private DataContext _dataStorageContext;

        public delegate void OnAppFocus(bool hasFocus);
        public static event OnAppFocus AppFocus;
        
        public delegate void OnAppPause(bool pauseStatus);
        public static event OnAppPause AppPause;
        
        private Systems _coreSystems;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
            Application.targetFrameRate = 60;
            // sceneLoader.LoadScene(sceneLoader.MainScene);
            _coreSystems = new CoreSystems(Contexts.sharedInstance);
        }
        
        private void Start()
        {
            _coreSystems.Initialize();
        }
        
        public void LoadScene(string sceneName)
        {
            sceneLoader.LoadScene(sceneName);
        }
        [ContextMenu("LoadGameLevel")]
        public void LoadGameLevel() => sceneLoader.LoadScene(sceneLoader.GameScene);
        [ContextMenu("LoadMainMenu")]
        public void LoadMainMenu() => sceneLoader.LoadScene(sceneLoader.MainScene);
        
        

        #region Application Events

        private void OnApplicationFocus(bool hasFocus)
        {
            AppFocus?.Invoke(hasFocus);
        }
        private void OnApplicationPause(bool pauseStatus)
        {
            AppPause?.Invoke(pauseStatus);
        }

        #endregion

        private void Update()
        {
            _coreSystems.Execute();
            _coreSystems.Cleanup();
        }

        private void OnDestroy()
        {
            _coreSystems.TearDown();
        }

   }
}