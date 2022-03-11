using UnityEngine;
using EasyUI.Helpers;

[CreateAssetMenu(fileName = "Settings", menuName = "ScriptableObject/Easy UI/Progress UI/Settings")]
public class SettingsProgressUI : ScriptableObject {
    public Theme theme;

    [Range(0f,.4f)]
    public float fadeInDuration = .2f;

    [Range(0f,.2f)]
    public float fadeOutDuration = .1f;

}
