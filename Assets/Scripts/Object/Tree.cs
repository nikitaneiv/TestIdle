using UnityEngine;
using DG.Tweening;

public class Tree : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private CapsuleCollider capsuleCollider;
    [SerializeField] private GameObject log;

    private Animator _animator;

    private readonly static Vector3 punch = new Vector3(0, 20, 20);
    private readonly static Vector3 height = new Vector3(1f, 1f, 1f);
    private readonly static Vector3 heightStart = new Vector3(0.5f, 0.5f, 0.5f);
    private readonly static Vector3 rotateStart = new Vector3(0f, 0f, 0f);

    private float durationOfHeight = 10f;
    private float duration = 0.5f;
    private int vibrato = 2;
    private float elasticity = 1F;

    private Sequence Seq;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        HeightOfTree();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Axe>())
        {
            transform.DOPunchRotation(punch, duration, vibrato, elasticity).OnComplete(() => Animation());
        }
    }

    private void Animation()
    {
        _animator.enabled = true;
        TriggerIsDeactive();
        Invoke("InstanceLog", 1.5f);
        Invoke("RemoveTree", 1.5f);
    }

    private void HeightOfTree()
    {
        transform.DOScale(height, durationOfHeight).OnComplete(() => TriggerIsActive());
    }

    private void TriggerIsActive()
    {
        boxCollider.isTrigger = true;
        capsuleCollider.isTrigger = true;
        boxCollider.enabled = true;
        capsuleCollider.enabled = true;
    }

    private void TriggerIsDeactive()
    {
        boxCollider.isTrigger = false;
        capsuleCollider.isTrigger = false;
        boxCollider.enabled = false;
        capsuleCollider.enabled = false;
    }

    private void RemoveTree()
    {
        gameObject.SetActive(false);
        _animator.enabled = false;

        Seq = DOTween.Sequence();
        Seq.Append(transform.DOScale(heightStart, duration)).SetEase(Ease.Linear);
        Seq.Append(transform.DORotate(rotateStart, duration, RotateMode.Fast)
            .OnComplete(() => gameObject.SetActive(true)));
        Seq.Append(transform.DOScale(height, durationOfHeight).SetEase(Ease.Linear)
            .OnComplete(() => TriggerIsActive()));
        Seq.SetEase(Ease.Linear);
    }

    private void InstanceLog()
    {
        GameObject Log = Instantiate(log, transform.position, Quaternion.identity);
        // Log.transform.DOJump(gameObject.transform.position , 2f, 1, duration,
        //     snapping); Доработать!!
        
    }
}