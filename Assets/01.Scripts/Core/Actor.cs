using UnityEngine;

public class Actor : MonoBehaviour
{
    public string actorName { get; protected set; }
    public ActorType actorType { get; protected set; }
    public ActorContext actorContext { get; protected set; } = new ActorContext();
    public bool initialized { get; protected set; }

    protected virtual void Awake() { Initialize(); }
    
    public virtual void Initialize()
    {
        actorContext.transform = transform;
        actorContext.cc = GetComponent<CharacterController>();
        actorContext.animator = GetComponent<Animator>();

        actorContext.movementContext = new MovementContext();

        initialized = true;
    }

    public virtual void OverridePosition(Vector3 position) { }
    
    public virtual void OverrideRotation(Quaternion rotation) { }
    
    public virtual void OverrideRotationLookAtXZ(Vector3 lookAt) { }
    
    public virtual void OverrideTransform(Transform targetTransform) { }

    public virtual void Spawn() { }

    public enum ActorType
    {
        Player
    }
}
