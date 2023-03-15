using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;


public class Player : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private DynamicJoystick _stick;
    [SerializeField] private float speed;
    [SerializeField] private Transform Back;
    [SerializeField] private TextMeshProUGUI _textDynamic;

    private Animator animator;

    private readonly static Vector3 LogPosition = new Vector3(0.4f, 0.08f, 0.1f);
    private readonly static Vector3 HomePosition = new Vector3(15f, 1f, 1.5f);

    private const string IDLE = "Idle";
    private const string RUN = "Run";
    private const string ATTACK = "Attack";

    private bool _isSale = false;
    private bool _isAttack = false;

    private float logHeight;
    private int logCount;

    private Stack<GameObject> logStack = new Stack<GameObject>();

    private void Start()
    {
        animator = GetComponent<Animator>();
        _textDynamic.text = logCount.ToString();
    }

    private void Update()
    {
        Move();

        if (_isSale)
        {
            LogSale();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Tree>())
        {
            _isAttack = true;
        }

        if (other.gameObject.GetComponent<Log>())
        {
            if (logCount <= 9)
            {
                logHeight += 0.3f;
                logCount++;
                _textDynamic.text = logCount.ToString();
                var position = LogPosition + new Vector3(0, 0, logHeight);
                other.isTrigger = false;
                other.transform.SetParent(Back);
                other.transform.SetLocalPositionAndRotation(position, Quaternion.Euler(0, 0, 90));

                logStack.Push(other.gameObject);
            }
        }

        if (other.gameObject.GetComponent<Home>() && logStack.Count > 0)
        {
            _isSale = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Tree>())
        {
            _isAttack = false;
        }
    }

    private void Move()
    {
        var direction = new Vector3(_stick.Direction.x, 0, _stick.Direction.y) * speed;
        if (direction != Vector3.zero)
        {
            animator.Play(RUN);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction),
                speed * Time.deltaTime);
            characterController.SimpleMove(direction);
        }
        else if (_isAttack)
        {
            animator.Play(ATTACK);
        }
        else
        {
            animator.Play(IDLE);
        }
    }

    private void LogSale()
    {
        var currentItem = logStack.Pop();
        currentItem.transform.DOMove(HomePosition, 1.5f, false);
        currentItem.transform.SetParent(null);
        if (logStack.Count == 0)
        {
            _isSale = false;
            logHeight = 0;
            logCount = 0;
            _textDynamic.text = logCount.ToString();
        }
    }
}