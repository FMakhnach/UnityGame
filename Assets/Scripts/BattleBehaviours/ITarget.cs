using UnityEngine;

/// <summary>
/// Represents objects that one can aim.
/// </summary>
public interface ITarget
{
    Transform TargetPoint { get; }
    PlayerManager Owner { get; }
}