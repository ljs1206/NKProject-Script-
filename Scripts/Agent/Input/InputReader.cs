using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using Action = System.Action;

[CreateAssetMenu(menuName = "SO/InputReader")]
public class InputReader : ScriptableObject, Control.IMoveActions
{
    private Control _controls;
    public Vector2 Movement {get; private set;}
    public event Action OnJumpEvent;
    public event Action OnAttackEvent;

    private void OnEnable() {
        if(_controls == null){
            _controls = new Control();
        }

        _controls.Move.Enable();
        _controls.Move.SetCallbacks(this);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Movement = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnJumpEvent?.Invoke();
        }
    }
    
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnAttackEvent?.Invoke();
        }
    }
}

