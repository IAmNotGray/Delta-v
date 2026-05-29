using Content.Shared.Revenant.Components;
using Content.Shared.Popups;
using Content.Shared.StatusEffectNew; // Delta V - Migrate to New
using Content.Shared.Stunnable;
using Robust.Shared.Physics.Events;

namespace Content.Shared.Revenant.EntitySystems;

public abstract class SharedRevealRevenantOnCollideSystem : EntitySystem
{
    [Dependency] private readonly StatusEffectsSystem _status = default!;
    [Dependency] private readonly SharedPopupSystem _popup = default!;
    [Dependency] private readonly SharedStunSystem _stun = default!;
    // [Dependency] private readonly IGameTiming _gameTiming = default!; // Delta V - Never used

    private const string CorporealStatusId = "Corporeal";
    private const string StunStatusId = "Stun";

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<RevealRevenantOnCollideComponent, StartCollideEvent>(OnCollideStart);
    }

    public void OnCollideStart(EntityUid uid, RevealRevenantOnCollideComponent comp, StartCollideEvent args)
    {
        if (!HasComp<RevenantComponent>(args.OtherEntity))
            return;

        if (!string.IsNullOrEmpty(comp.PopupText) && !_status.HasStatusEffect(args.OtherEntity, CorporealStatusId))
            _popup.PopupClient(
                Loc.GetString(comp.PopupText, ("revealer", uid), ("revenant", args.OtherEntity)),
                args.OtherEntity,
                args.OtherEntity
            );

        _status.TryAddStatusEffectDuration(args.OtherEntity, CorporealStatusId, out _, comp.RevealTime);

        if (comp.StunTime != null && !_status.HasStatusEffect(args.OtherEntity, StunStatusId))
            _stun.TryUpdateStunDuration(args.OtherEntity, comp.StunTime.Value);
    }
}
