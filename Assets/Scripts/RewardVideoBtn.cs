using System.Collections;
using System.Collections.Generic;
using MagicOwl;
using UnityEngine;
using UnityEngine.UI;

public class RewardVideoBtn : CommonUIBehaviour
{
    private Button Button => GetCachedComponent<Button>();
    
    private void Update()
    {
        Button.interactable = AdManager.Instance.IsRewardAdReady;
    }
}
