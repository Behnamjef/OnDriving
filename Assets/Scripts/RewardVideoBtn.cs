using MagicOwl;
using UnityEngine;
using UnityEngine.UI;

public class RewardVideoBtn : CommonUIBehaviour
{
    private Button Button => GetCachedComponent<Button>();
    [SerializeField] private RectTransform Text;
    [SerializeField] private GameObject noAdAvailableObj;
    public int EnableTextGap = 145;

    private void Start()
    {
        Button.onClick.AddListener(() => { SoundManager.Instance.PlaySound(SoundType.BlowCashButton); });
    }

    private void Update()
    {
        var isReady = AdManager.Instance.IsRewardAdReady;
        Button.interactable = isReady;
        var pos = Text.localPosition;
        pos.x = isReady ? EnableTextGap : 0;
        Text.localPosition = pos;
        noAdAvailableObj.SetActive(!isReady);
    }
}