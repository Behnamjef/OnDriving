using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Sound Listener", menuName = "ScriptableObjects/Create sound listener", order = 1)]
public class SoundListener : ScriptableObject
{
    public Action OnClick;

    public void ButtonClick()
    {
        OnClick?.Invoke();
        SoundManager.Instance.PlaySound(SoundType.Click);
    }
}