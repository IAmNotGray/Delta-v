using System.Numerics;
using Content.Shared.Damage;
using Content.Shared.StatusEffectNew.Components;
using Content.Shared.Whitelist;
using Robust.Shared.Audio;
using Robust.Shared.GameStates;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom;
using Robust.Shared.Utility;

namespace Content.Shared.Weather;

/// <summary>
/// Used only in conjure with <see cref="StatusEffectComponent"/> for status effects applied to map entities.
/// Contains basic information about all types of weather effects.
/// </summary>
[RegisterComponent, NetworkedComponent, Access(typeof(SharedWeatherSystem))]
public sealed partial class WeatherStatusEffectComponent : Component
{
    /// <summary>
    /// A texture that will tile and render as a weather effect across the entire map.
    /// </summary>
    [DataField(required: true)]
    public SpriteSpecifier Sprite = default!;

    /// <summary>
    /// Tint that will be applied to the weather texture.
    /// </summary>
    [DataField]
    public Color? Color;

    /// <summary>
    /// Weather scrolling speed.
    /// </summary>
    [DataField]
    public Vector2? Scrolling;

    /// <summary>
    /// Sound to play on the affected areas.
    /// </summary>
    [DataField]
    public SoundSpecifier? Sound;

    /// <summary>
    /// Client audio stream.
    /// Not used on the server.
    /// </summary>
    [ViewVariables]
    public EntityUid? Stream;


    /// <summary>
    /// DeltaV: How long to wait between updating weather effects.
    /// </summary>
    [DataField]
    public TimeSpan UpdateDelay = TimeSpan.FromSeconds(1);

    /// <summary>
    /// DeltaV: When to next update weather effects (damage).
    /// </summary>
    [DataField(customTypeSerializer: typeof(TimeOffsetSerializer))]
    public TimeSpan NextUpdate = TimeSpan.Zero;

    /// <summary>
    /// DeltaV: Damage you can take from being in this weather.
    /// Only applies when weather has fully set in.
    /// </summary>
    [DataField]
    public DamageSpecifier? Damage;

    /// <summary>
    /// DeltaV: Don't damage entities that match this blacklist.
    /// </summary>
    [DataField]
    public EntityWhitelist? DamageBlacklist;
}
