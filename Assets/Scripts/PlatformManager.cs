using UnityEngine;

public enum RunningPlatformType {
    Desktop,
    Mobile
}

public class PlatformManager : MonoBehaviour {
    public static PlatformManager Instance;

    private RunningPlatformType runningPlatform;

    public RunningPlatformType RunningPlatform { get { return runningPlatform; } }

    private void Awake () {
        Instance = this;
        DetectPlatform ();
        DoPlatformModifications (runningPlatform);
    }

    public static RunningPlatformType GetRunningPlatform () {
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX // If we are on desktop
        return RunningPlatformType.Desktop;
#else
        return RunningPlatformType.Mobile;
#endif
    }

    private void DetectPlatform () {
        runningPlatform = GetRunningPlatform ();
    }

    /// <summary>
    /// Changes the game the interaction and environment according to the platform we are currently running on.
    /// </summary>
    /// <param name="platform"></param>
    private void DoPlatformModifications (RunningPlatformType platform) {
        Debug.Log (string.Format ("Running on {0} platform, performing modifications according to that platform.",
            runningPlatform.ToString ()));

        // Disable post processing as it it difficult for the processor
        var postProcess = GameObject.Find ("Post Process Volume");

        if (postProcess) {
            var postProcessPlatformModifications = postProcess.GetComponent<PostProcessPlatformModifications> ();
            postProcessPlatformModifications.DoPlatformModifications (platform);
        }

        // Show dust particles only on destop
        var dustParticles = GameObject.Find ("Dust Particles");

        if (dustParticles) {
            dustParticles.SetActive (runningPlatform == RunningPlatformType.Desktop);
        }
    }
}