using Content.Shared.StatusEffectNew;
using Robust.Shared.Prototypes;

namespace Content.Shared._DV.Addictions;

public abstract class SharedAddictionSystem : EntitySystem
{
    [Dependency] private readonly StatusEffectsSystem _statusEffects = default!;

    public const string StatusEffectKey = "Addicted";

    protected abstract void UpdateTime(EntityUid uid);

    public virtual void TryApplyAddiction(EntityUid uid, float addictionTime)
    {
        UpdateTime(uid);

        _statusEffects.TryAddStatusEffectDuration(uid, StatusEffectKey, out _, TimeSpan.FromSeconds(addictionTime));
    }

    public virtual void TrySuppressAddiction(EntityUid uid, float duration)
    {
        if (!TryComp<AddictedComponent>(uid, out var comp))
            return;

        var ent = new Entity<AddictedComponent>(uid, comp);
        UpdateAddictionSuppression(ent, duration);
    }

    protected virtual void UpdateAddictionSuppression(Entity<AddictedComponent> ent, float duration)
    {
    }
}
