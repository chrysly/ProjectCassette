using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(UnityEngine.UI.Image))]
public class Transition : MonoBehaviour {

    [SerializeField] private float defaultTransitionTime;
    private CanvasGroup cg;

    public enum Level {
        MainMenu = 0,
        Game = 1,
    } public event System.Action<Level> OnLevelTransition;

    private static Transition instance;
    public static Transition Instance;

    void Awake() {
        cg = GetComponent<CanvasGroup>();
        cg.alpha = 0;

        /// Initialize Singleton;
        if (instance != this) {
            instance = this;
            DontDestroyOnLoad(this);
        } else Destroy(gameObject);
    }

    public void TransitionToLevel(Level level) => TransitionToLevel((int) level);

    public void TransitionToLevel(int levelIndex) {
        OnLevelTransition?.Invoke((Level) levelIndex);
        SceneManager.LoadScene(levelIndex);
    }

    /// <summary>
    /// Fade in the transition canvas;
    /// </summary>
    /// <param name="density"> Alpha of the canvas image; </param>
    /// <param name="duration"> Duration of the transition; </param>
    public void Fade(float density, float duration = -1) => StartCoroutine(_Fade(density, duration));

    private IEnumerator _Fade(float density, float duration = -1) {
        StopAllCoroutines();
        duration = ValidateDuration(duration);
        while (cg.alpha != density) {
            Mathf.MoveTowards(cg.alpha, density, Time.unscaledDeltaTime * duration);
            yield return null;
        } cg.blocksRaycasts = true;
        cg.interactable = true;
    }

    /// <summary>
    /// Fade out the transition canvas;
    /// </summary>
    /// <param name="duration"> Duration of the transition; </param>
    public void Clear(float duration = -1) => StartCoroutine(_Clear(duration));

    private IEnumerator _Clear(float duration = -1) {
        StopAllCoroutines();
        duration = ValidateDuration(duration);
        while (cg.alpha != 0) {
            Mathf.MoveTowards(cg.alpha, 0, Time.unscaledDeltaTime * duration);
            yield return null;
        } cg.blocksRaycasts = false;
        cg.interactable = false;
    }

    private float ValidateDuration(float duration) => duration > 0 ? duration : 1;
}
