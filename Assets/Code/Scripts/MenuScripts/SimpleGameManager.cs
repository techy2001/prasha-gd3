using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GD.Examples;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;
using AsyncOperation = UnityEngine.AsyncOperation;
using Object = UnityEngine.Object;

namespace Code.Scripts.MenuScripts
{
    /// <summary>
    /// Manages the game flow, scene loading, and persistent objects.
    /// </summary>
    public class SimpleGameManager : MonoBehaviour
    {
        /// <summary>
        /// Indicates whether the game manager has been loaded.
        /// </summary>
        [SerializeField]
        [ReadOnly]
        private bool isLoaded = false;

        [SerializeField]
        [ReadOnly]
        private bool isMenuVisible;

        private string activeLevel;

        #region UI & Menu

        [Header("Persistent UI")]
        [SerializeField]
        private GameObject mainMenu;

        [SerializeField]
        private GameObject ui;

        #endregion UI & Menu

        [Space]

        #region Levels

        [SerializeField]
        private string startLevel;

        [SerializeField]
        private Levels levels;

        #endregion Levels

        [Space]

        #region Events

        [FoldoutGroup("Events")]
        [SerializeField]
        private UnityEvent<float> OnLoadProgressUpdate;

        [FoldoutGroup("Events")]
        [SerializeField]
        private UnityEvent<string> OnLoadComplete;

        [FoldoutGroup("Events")]
        [SerializeField]
        private UnityEvent OnStart;

        [FoldoutGroup("Events")]
        [SerializeField]
        private UnityEvent OnPause;

        [FoldoutGroup("Events")]
        [SerializeField]
        private UnityEvent OnResume;

        [FoldoutGroup("Events")]
        [SerializeField]
        private UnityEvent OnExit;

        #endregion Events

        /// <summary>
        /// Start is called before the first frame update.
        /// </summary>
        private void Start()
        {
            // Don't start twice!
            if (isLoaded)
                return;

            // Load first level
            if(levels.Contents.Count > 0)
                StartCoroutine(LoadAllScenesAsync(startLevel));

            //show menu
            isMenuVisible = true;
        }

        /// <summary>
        /// Starts the game by loading the initial level.
        /// </summary>
        public void StartGame()
        {
            //TODO - replace with yield Wait
            //while (!isLoaded)
            //{
            //}

            Time.timeScale = 1;
            OnStart?.Invoke();
            isMenuVisible = false;
        }

        /// <summary>
        /// Pauses the game by setting the time scale to 0.
        /// </summary>
        public void PauseGame()
        {
            OnPause?.Invoke();
            Time.timeScale = 0;
        }

        /// <summary>
        /// Resumes the game by restoring the original time scale.
        /// </summary>
        public void ContinueGame()
        {
            Time.timeScale = 1; // or whatever it was originally
        }

        /// <summary>
        /// Exits the game.
        /// </summary>
        public void ExitGame()
        {
            OnExit?.Invoke();
            // Do other housekeeping here...
            Application.Quit();
        }

        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        private void Update()
        {
            ShowHideMenu();
        }

        /// <summary>
        /// F1 to show/hide menu
        /// </summary>
        private void ShowHideMenu()
        {
            // Handle input, e.g., escape key to show the menu
            if (Input.GetKeyUp(KeyCode.F1))
            {
                if (!mainMenu.activeSelf)
                {
                    mainMenu.SetActive(true);
                    PauseGame();
                }
                else
                {
                    mainMenu.SetActive(false);
                    ContinueGame();
                }
            }
        }

        /// <summary>
        /// Stop all running coroutines e.g. scene loader
        /// </summary>
        private void OnDestroy()
        {
            StopAllCoroutines();
        }

        /// <summary>
        /// Loads a list of scenes asynchronously in additive mode.
        /// </summary>
        /// <param name="scenes">The list of scenes to load.</param>
        /// <see cref="https://forum.unity.com/threads/issue-activating-multiple-scene-at-the-same-time-via-asyncoperation-allowsceneactivation.624604/"/>
        public IEnumerator LoadAllScenesAsync(string level)
        {
            level = level.Trim();
            if (!levels.Contents.ContainsKey(level))
                yield return null;

            var scenes = levels.Contents[level];

            if (scenes.Count == 0)
                yield return null;

            List<AsyncOperation> asyncOperations = new List<AsyncOperation>();

            foreach (Object scene in scenes)
            {
                if (!SceneManager.GetSceneByName(scene.name).isLoaded)
                {
                    AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene.name, LoadSceneMode.Additive);
                    asyncOperations.Add(asyncOperation);
                    asyncOperation.allowSceneActivation = false;
                }
            }

            if (asyncOperations.Count != 0)
            {
                while (!asyncOperations.Select(op => op.isDone).Aggregate((l, r) => l & r))
                {
                    var progresses = asyncOperations.Select(op => op.progress).ToList();
                    var lowestProgress = progresses.Aggregate((l, r) => l < r ? l : r);
                    var normalizedProgress = progresses.Aggregate((l, r) => l + r) / progresses.Count;
                    OnLoadProgressUpdate?.Invoke(normalizedProgress);

                    if (lowestProgress >= 0.9f)
                    {
                        foreach (var asyncOp in asyncOperations)
                        {
                            asyncOp.allowSceneActivation = true;
                        }
                        progresses = asyncOperations.Select(op => op.progress).ToList();
                        OnLoadProgressUpdate?.Invoke(progresses.Aggregate((l, r) => l + r) / progresses.Count);
                    }
                    yield return null;
                }

                isLoaded = true;
                activeLevel = level;
                OnLoadComplete?.Invoke(activeLevel);
            }
        }

        /// <summary>
        /// Unloads a list of scenes asynchronously.
        /// </summary>
        /// <param name="scenes">The list of scenes to unload.</param>
        public void UnLoadLevel(string level)
        {
            level = level.Trim();
            if (!levels.Contents.ContainsKey(level))
                return;

            var scenes = levels.Contents[level];

            if (scenes.Count == 0)
                return;

            foreach (Object scene in scenes)
            {
                if (SceneManager.GetSceneByName(scene.name).isLoaded)
                    SceneManager.UnloadSceneAsync(scene.name);
            }
        }

        /*private void OnGUI()
        {
            Rect rect = new Rect(10, 10, 100, 25);

            if (GUI.Button(rect, "Level 1"))
            {
                UnLoadLevel(activeLevel);
                StartCoroutine(LoadAllScenesAsync("level 1"));
            }

            rect = new Rect(110, 10, 100, 25);

            if (GUI.Button(rect, "Level 2"))
            {
                UnLoadLevel(activeLevel);
                StartCoroutine(LoadAllScenesAsync("level 2"));
            }
        }*/
    }
}