using UnityEngine;

public class Actor : MonoBehaviour
{
    public string actorName { get; protected set; }
    public ActorContext actorContext { get; private set; } = new ActorContext();
    public bool initialized { get; protected set; }

    public virtual void Initialize()
    {
        actorContext.transform = transform;
        actorContext.cc = GetComponent<CharacterController>();
        actorContext.animator = GetComponent<Animator>();

        initialized = true;
    }

    public virtual Vector3 GetPosition()
    {
        return actorContext.transform.position;
    }

    public virtual Quaternion GetRotation()
    {
        return actorContext.transform.rotation;
    }

    public virtual Vector3 GetVelocity()
    {
        return default;
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
