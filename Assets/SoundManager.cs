using System;
using MagicOwl;
using UnityEngine;

public class SoundManager : SingletonBehaviour<SoundManager>
{
    private AudioSource AudioSource => GetCachedComponent<AudioSource>();
    
    public AudioClip BlowCashButton;
    public AudioClip Click;
    public AudioClip CarHit;
    public AudioClip UIShowUp;
    public AudioClip FinishReached;
    public AudioClip Gear_D;
    public AudioClip Gear_R;
    public AudioClip LevelStart;
    public AudioClip Win;

    public void PlaySound(SoundType type)
    {
        switch (type)
        {
            case SoundType.BlowCashButton:
                AudioSource.PlayOneShot(BlowCashButton);
                break;
            case SoundType.Click:
                AudioSource.PlayOneShot(Click);
                break;
            case SoundType.CarHit:
                AudioSource.PlayOneShot(CarHit);
                break;
            case SoundType.UIShowUp:
                AudioSource.PlayOneShot(UIShowUp);
                break;
            case SoundType.FinishReached:
                AudioSource.PlayOneShot(FinishReached);
                break;
            case SoundType.Gear_D:
                AudioSource.PlayOneShot(Gear_D);
                break;
            case SoundType.Gear_R:
                AudioSource.PlayOneShot(Gear_R);
                break;
            case SoundType.LevelStart:
                AudioSource.PlayOneShot(LevelStart);
                break;
            case SoundType.Win:
                AudioSource.PlayOneShot(Win);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}

public enum SoundType
{
    BlowCashButton,
    Click,
    CarHit,
    UIShowUp,
    FinishReached,
    Gear_D,
    Gear_R,
    LevelStart,
    Win
}