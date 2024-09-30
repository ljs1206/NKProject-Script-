using UnityEngine;

public interface IMovement
{
    public Vector3 Velocity { get; }
    public bool IsGround { get; }
    public bool CanMove { get; set; }
    public void Initialize(Agent agent);
    public void Move();
    public void StopImmediately();
    
    public void GetKnockback(Vector3 force);
}