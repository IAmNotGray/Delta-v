﻿using Content.Server._RMC14.Emote; //RMC emote system
using Content.Server.Chat.Systems;
using Content.Shared.Chat;
using Robust.Shared.Prototypes;

namespace Content.Server.Speech;

public sealed partial class EmotesMenuSystem : EntitySystem
{
    [Dependency] private readonly IPrototypeManager _prototypeManager = default!;
    [Dependency] private readonly ChatSystem _chat = default!;
    [Dependency] private readonly RMCEmoteSystem _rmcEmote = default!; //RMC emote system

    public override void Initialize()
    {
        base.Initialize();

        SubscribeAllEvent<PlayEmoteMessage>(OnPlayEmote);
    }

    private void OnPlayEmote(PlayEmoteMessage msg, EntitySessionEventArgs args)
    {
        var player = args.SenderSession.AttachedEntity;
        if (!player.HasValue)
            return;

        if (!_prototypeManager.TryIndex(msg.ProtoId, out var proto) || proto.ChatTriggers.Count == 0)
            return;

        //RMC emote system
        if (!_rmcEmote.TryEmote(player.Value))
            return;

        _chat.TryEmoteWithChat(player.Value, msg.ProtoId);
    }
}
