using Content.Shared.Movement.Components;
using Robust.Shared.GameStates;

namespace Content.Shared._DV.Movement.Components;

[RegisterComponent, NetworkedComponent]
public sealed partial class MovementBodyPartComponent : Component
{
    [DataField("walkSpeed")]
    public float WalkSpeed = MovementSpeedModifierComponent.DefaultBaseWalkSpeed;

    [DataField("sprintSpeed")]
    public float SprintSpeed = MovementSpeedModifierComponent.DefaultBaseSprintSpeed;

    [DataField("acceleration")]
    public float Acceleration = MovementSpeedModifierComponent.DefaultAcceleration;
}
