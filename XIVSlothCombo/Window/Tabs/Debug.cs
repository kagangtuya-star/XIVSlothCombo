﻿using System.Linq;
using System.Numerics;
using Dalamud.Game.ClientState.JobGauge.Types;
using Dalamud.Game.ClientState.Objects.SubKinds;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Game.ClientState.Statuses;
using ImGuiNET;
using XIVSlothCombo.Combos;
using XIVSlothCombo.Combos.PvE;
using XIVSlothCombo.CustomComboNS;
using XIVSlothCombo.CustomComboNS.Functions;
using XIVSlothCombo.Data;
using XIVSlothCombo.Extensions;
using XIVSlothCombo.Services;

#if DEBUG
namespace XIVSlothCombo.Window.Tabs
{

    internal class Debug : ConfigWindow
    {

        internal class DebugCombo : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; }

            protected override uint Invoke(uint actionID, uint lastComboActionID, float comboTime, byte level) => actionID;
        }

        internal static new  void Draw()
        {
            PlayerCharacter? LocalPlayer = Service.ClientState.LocalPlayer;
            // DebugCombo? comboClass = new();

            if (LocalPlayer != null)
            {
                unsafe
                {
                    if (Service.ClientState.LocalPlayer.TargetObject is BattleChara chara)
                    {
                        foreach (Status? status in chara.StatusList)
                        {
                            ImGui.TextUnformatted($"TARGET STATUS CHECK: {chara.Name} -> {ActionWatching.GetStatusName(status.StatusId)}: {status.StatusId}");
                        }
                    }

                    foreach (Status? status in (Service.ClientState.LocalPlayer as BattleChara).StatusList)
                    {
                        ImGui.TextUnformatted($"SELF STATUS CHECK: {Service.ClientState.LocalPlayer.Name} -> {ActionWatching.GetStatusName(status.StatusId)}: {status.StatusId}");
                    }

                    ImGui.TextUnformatted($"TARGET OBJECT KIND: {Service.ClientState.LocalPlayer.TargetObject?.ObjectKind}");
                    ImGui.TextUnformatted($"TARGET IS BATTLE CHARA: {Service.ClientState.LocalPlayer.TargetObject is BattleChara}");
                    ImGui.TextUnformatted($"PLAYER IS BATTLE CHARA: {LocalPlayer is BattleChara}");
                    ImGui.TextUnformatted($"IN COMBAT: {CustomComboFunctions.InCombat()}");
                    ImGui.TextUnformatted($"ActionWatching.CombatActions.Count: {ActionWatching.CombatActions.Count}");
                    ImGui.TextUnformatted($"IN MELEE RANGE: {CustomComboFunctions.InMeleeRange()}");
                    ImGui.TextUnformatted($"DISTANCE FROM TARGET: {CustomComboFunctions.GetTargetDistance()}");
                    ImGui.TextUnformatted($"TARGET HP VALUE: {CustomComboFunctions.EnemyHealthCurrentHp()}");
                    ImGui.TextUnformatted($"LAST ACTION: {ActionWatching.GetActionName(ActionWatching.LastAction)} (ID:{ActionWatching.LastAction})");
                    ImGui.TextUnformatted($"LAST ACTION COST: {CustomComboFunctions.GetResourceCost(ActionWatching.LastAction)}");
                    ImGui.TextUnformatted($"LAST ACTION TYPE: {ActionWatching.GetAttackType(ActionWatching.LastAction)}");
                    ImGui.TextUnformatted($"LAST WEAPONSKILL: {ActionWatching.GetActionName(ActionWatching.LastWeaponskill)}");
                    ImGui.TextUnformatted($"LAST SPELL: {ActionWatching.GetActionName(ActionWatching.LastSpell)}");
                    ImGui.TextUnformatted($"LAST ABILITY: {ActionWatching.GetActionName(ActionWatching.LastAbility)}");
                    ImGui.TextUnformatted($"ZONE: {Service.ClientState.TerritoryType}");
                
                    ImGui.TextUnformatted($"buff : {CustomComboFunctions.GetBuffRemainingTime(PLD.Buffs.DivineMight)}");
                
                    float comboTime = *(float*)Service.Address.ComboTimer;
                    
                    uint LastComboMove = *(uint*)Service.Address.LastComboMove;
                
                    ImGui.TextUnformatted($"王权层数 : {CustomComboFunctions.GetBuffStacks(PLD.Buffs.忠义之剑SwordOath)}");
                    ImGui.TextUnformatted($"comboTime : {comboTime}");
                    ImGui.TextUnformatted($"LastComboMove : {LastComboMove}");
                    
                    
                    ImGui.TextUnformatted($"四色技巧舞步结束TechnicalFinish4 : {CustomComboFunctions.WasLastAction(DNC.四色技巧舞步结束TechnicalFinish4)} 进攻之探戈Devilment {CustomComboFunctions.WasLastAction(DNC.进攻之探戈Devilment)}}} 综合{!CustomComboFunctions.WasLastAction(DNC.四色技巧舞步结束TechnicalFinish4) && !CustomComboFunctions.WasLastAbility(DNC.进攻之探戈Devilment) && CustomComboFunctions.IsOnCooldown(DNC.进攻之探戈Devilment)} ");
                    ImGui.TextUnformatted($"四色技巧舞步结束TechnicalFinish4 : {CustomComboFunctions.WasLastAction(DNC.四色技巧舞步结束TechnicalFinish4)} 进攻之探戈Devilment {CustomComboFunctions.WasLastAction(DNC.进攻之探戈Devilment)}}} 综合{!CustomComboFunctions.WasLastAction(DNC.四色技巧舞步结束TechnicalFinish4) && !CustomComboFunctions.WasLastAbility(DNC.进攻之探戈Devilment) } ");
                
                
                    ImGui.TextUnformatted($"战逃倒计时 : {CustomComboFunctions.GetCooldownRemainingTime(PLD.战逃反应FightOrFlight)}");
                
                    ImGui.TextUnformatted($"战逃个数 : {ActionWatching.CombatActions.FindAll(actionId => actionId ==PLD. 战逃反应FightOrFlight).Count}");
                
                    ImGui.TextUnformatted($"DK血乱 : {CustomComboFunctions.GetBuffStacks(DRK.Buffs.BloodWeapon)}");
                    // var gauge = GetJobGauge<DRKGauge>();
                    ImGui.TextUnformatted($"DK2 : {CustomComboFunctions.GetJobGauge<DRKGauge>().Blood}");
                    ImGui.TextUnformatted($"DK3 : 血乱CD{CustomComboFunctions.GetCooldownRemainingTime(DRK.血乱)},弗雷CD{CustomComboFunctions.GetCooldownRemainingTime(DRK.LivingShadow)}");
                    // ImGui.TextUnformatted($"DK4 : {CustomComboFunctions.FindTargetEffect(RaidBuff.连环计) is not null}");
                    // ImGui.TextUnformatted($"DK5 : {CustomComboFunctions.FindTargetEffectAny(RaidBuff.连环计) is not null}");    
                
                
                    // ImGui.TextUnformatted($"gun1 : {CustomComboFunctions.ActionReady(GNB.倍攻DoubleDown)}");
                    // ImGui.TextUnformatted($"gun2 : {CustomComboFunctions.GetCooldownRemainingTime(GNB.倍攻DoubleDown)}");
                    ImGui.TextUnformatted($"烈牙GnashingFang : {CustomComboFunctions.GetCooldownRemainingTime(GNB.烈牙GnashingFang)}");
                    ImGui.TextUnformatted($"利刃斩KeenEdge : {CustomComboFunctions.GetCooldownRemainingTime(GNB.利刃斩KeenEdge)}");
                    ImGui.TextUnformatted($"倍攻DoubleDown : {CustomComboFunctions.GetCooldownRemainingTime(GNB.倍攻DoubleDown)}");
                
                
                
                
                
                    // ImGui.TextUnformatted($"摆烂循环 : {PLD.ActionLoop}");
                    // ImGui.TextUnformatted($"摆烂循环 : {CustomComboFunctions.GetOptionValue(PLD.Config.PLD_FOF_GCD)}");
                
                    // ImGui.TextUnformatted($"WasLast2ActionsAbilities : {ActionWatching.WasLast2ActionsAbilities()}");
                
                
                    // ImGui.TextUnformatted($"Service.ComboCache.GetMaxCharges : {Service.ComboCache.GetMaxCharges(DRK.BloodWeapon)}");
                
                    // ImGui.TextUnformatted($"LocalPlayer.TotalCastTime : {LocalPlayer.TotalCastTime}");
                    //倒计时
                    // ImGui.TextUnformatted($"CooldownRemaining1 : {Service.ComboCache.GetCooldown(DRK.血溅).CooldownRemaining}");
                    // ImGui.TextUnformatted($"CooldownRemaining2 : {CustomComboFunctions.GetCooldownRemainingTime(DRK.血溅)}");
                    // ImGui.TextUnformatted($"CooldownElapsed : {Service.ComboCache.GetCooldown(DRK.血溅).CooldownElapsed}");
                    ImGui.TextUnformatted($"CooldownElapsed : {CustomComboFunctions.GetBuffRemainingTime(DRK.Buffs.Delirium)}");
                
                
                    // if (ActionWatching.CombatActions.Count > 2)
                    // {
                    //     ImGui.TextUnformatted($"摆烂循环1 : {ActionWatching.CombatActions.Last()}");
                    //     ImGui.TextUnformatted($"摆烂循环2 : {ActionWatching.CombatActions[ActionWatching.CombatActions.Count - 2]}");
                    //     ImGui.TextUnformatted($"摆烂循环3 : {ActionWatching.GetAttackType(ActionWatching.CombatActions.Last())}");
                    // }


                    // ImGui.TextUnformatted($"战逃个数 : {ActionWatching.CombatActions.Count}");
                
                    ImGui.BeginChild("BLUSPELLS", new Vector2(250, 100), false);
                    ImGui.TextUnformatted($"SELECTED BLU SPELLS:\n{string.Join("\n", Service.Configuration.ActiveBLUSpells.Select(x => ActionWatching.GetActionName(x)).OrderBy(x => x))}");
                    ImGui.EndChild();
                }
            }

            else
            {
                ImGui.TextUnformatted("Please log in to use this tab.");
            }
        }
    }
}
#endif