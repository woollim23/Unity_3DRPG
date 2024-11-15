using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [field: Header("Reference")]
    [field: SerializeField] public EnemySO Data { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    [field: Header("DropItems")]
    [field: SerializeField] public ItemData[] dropOnDeath;

    public Rigidbody Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public CharacterController Controller { get; private set; }
    public ForceReceiver ForceReceiver { get; private set; }

    public EnemyStateMachine stateMachine;

    public Health enemyHealth { get; private set; }

    [field: SerializeField] public Weapon Weapon { get; private set; }

    private void Awake()
    {
        AnimationData.Initialize();

        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponentInChildren<Animator>();
        Controller = GetComponent<CharacterController>();
        ForceReceiver = GetComponent<ForceReceiver>();

        stateMachine = new EnemyStateMachine(this);
        enemyHealth = GetComponent<Health>();
    }

    private void Start()
    {
        stateMachine.ChangeState(stateMachine.IdleState);

        enemyHealth.OnDie += OnDie;
    }

    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }

    void OnDie()
    {
        GameManager.Instance.DeathEnemy();
        GiveItem();
        Animator.SetTrigger("Die");
      

        Invoke("Release", 5);
    }

    void Release()
    {
        this.gameObject.SetActive(false);
        enemyHealth.health = enemyHealth.maxHealth;
        enemyHealth.IsDie = false;
        stateMachine.ChangeState(stateMachine.IdleState);
        ObjectPoolManager.Instance.pool.Release(this.gameObject);
        
    }

    void GiveItem()
    {
        CharacterManager.Instance.Player.itemData = dropOnDeath[0];
        CharacterManager.Instance.Player.addItem?.Invoke();
        CharacterManager.Instance.Player.itemData = dropOnDeath[1];
        CharacterManager.Instance.Player.addItem?.Invoke();
    }
}
