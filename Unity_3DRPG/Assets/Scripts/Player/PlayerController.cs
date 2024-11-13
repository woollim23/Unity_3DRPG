using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerInputs playerInputs { get; private set; }
    public PlayerInputs.PlayerActions playerActions { get; private set; }

    private void Awake()
    {
        playerInputs = new PlayerInputs();
        playerActions = playerInputs.Player; // ���� �÷��̾���ǲ�� ������
    }

    private void OnEnable()
    {
        playerInputs.Enable();
    }

    private void OnDisable()
    {
        playerInputs.Disable();
    }
}
