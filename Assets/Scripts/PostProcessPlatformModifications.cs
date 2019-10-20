using UnityEngine;

public class PostProcessPlatformModifications : MonoBehaviour {
    public void DoPlatformModifications (RunningPlatformType platform) {
        gameObject.SetActive (GameSettings.Instance.enablePostProcessing);
    }
}