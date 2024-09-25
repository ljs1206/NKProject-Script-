using UnityEngine;

public class PlayerAnimationTrigger : MonoBehaviour
{
    [SerializeField] private Player _player;
    private void AnimationEnd()
    {
        _player.StateMachine.CurrentState.AnimationFinishTrigger();
    }

    private void AttackMove(){
        Vector3 dir = new Vector3(transform.localScale.x  * _player.frontMoves[0], 0, 0);
        _player.PlayerMovement.Rg2d.AddForce(dir, ForceMode2D.Impulse);
    }

    private void PlayVFX()
    {
        _player.PlayBladeVFX();
    }

    private void DamageCast()
    {
        _player.Attack();
    }
}
