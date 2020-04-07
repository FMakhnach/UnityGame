using UnityEngine;

public interface ITarget
{
    Transform TargetPoint { get; }
    Alignment Alignment { get; }
}
