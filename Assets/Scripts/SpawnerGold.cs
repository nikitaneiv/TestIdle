using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class SpawnerGold : MonoBehaviour
{
    [SerializeField] private Image gold;
    [SerializeField] private Box box;
    [SerializeField] private Transform parent;
    [SerializeField] private TextMeshProUGUI _textDynamic;
    [SerializeField] private Transform endGold;

    private readonly static Vector3 StartGoldPosition = new Vector3(75, 0f, 0f);
    private readonly static Vector3 EndGoldPosition = new Vector3(68f, 253f, 0f);
    
    private int goldScore;

    private void Start()
    {
        _textDynamic.text = goldScore.ToString();
    }

    private void OnEnable()
    {
        box.InstantiateGold += Instance;
    }

    private void OnDisable()
    {
        box.InstantiateGold -= Instance;
    }

    private void Instance()
    {
        Image Gold = Instantiate(gold, gameObject.transform.position + StartGoldPosition, Quaternion.identity, parent)  ;
        Gold.transform.DOLocalMove(endGold.transform.localPosition, 1f, false).OnComplete(()=>AddScore());
        Destroy(Gold.gameObject, 5f);
    }

    private void AddScore()
    {
        goldScore++;
        _textDynamic.text = goldScore.ToString();
    }

}

