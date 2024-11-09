using Dalamud.Game.ClientState.JobGauge.Types;
using Dalamud.Game.ClientState.Statuses;
using XIVSlothComboX.Combos.PvE.Content;
using XIVSlothComboX.Core;
using XIVSlothComboX.CustomComboNS;
using XIVSlothComboX.Data;
using XIVSlothComboX.Extensions;

namespace XIVSlothComboX.Combos.PvE
{
    internal static class WAR
    {
        public const byte ClassID = 3;
        public const byte JobID = 21;

        public const uint HeavySwing = 31,
                          Maim = 37,
                          Berserk = 38,
                          Overpower = 41,
                          StormsPath = 42,
                          StormsEye = 45,
                          Tomahawk = 46,
                          原初之魂InnerBeast = 49,
                          钢铁旋风SteelCyclone = 51,
                          战壕Infuriate = 52,
                          裂石飞环FellCleave = 3549,
                          地毁人亡Decimate = 3550,
                          Upheaval = 7387,
                          原初的解放InnerRelease = 7389,
                          RawIntuition = 3551,
                          MythrilTempest = 16462,
                          ChaoticCyclone = 16463,
                          NascentFlash = 16464,
                          InnerChaos = 16465,
                          Orogeny = 25752,
                          蛮荒崩裂PrimalRend = 25753,
                          Onslaught = 7386,
                          原初激震 = 36924,
                          尽毁 = 36925,
                          留空 = 999999;

        public static class Buffs
        {
            public const ushort
                //fc
                原初的解放InnerRelease = 1177,
                SurgingTempest = 2677,
                //防击退
                原初的解放InnerRelease1 = 2663,
                NascentChaos = 1897,
                PrimalRendReady = 2624,
                尽毁Pre = 3834,
                原初激震Pre = 3901,
                Berserk = 86;
        }

        public static class Debuffs
        {
            public const ushort Placeholder = 1;
        }

        public static class Config
        {
            public const string WAR_InfuriateRange = "WarInfuriateRange",
                                WAR_SurgingRefreshRange = "WarSurgingRefreshRange",
                                WAR_KeepOnslaughtCharges = "WarKeepOnslaughtCharges",
                                WAR_VariantCure = "WAR_VariantCure",
                                WAR_FellCleaveGauge = "WAR_FellCleaveGauge",
                                WAR_DecimateGauge = "WAR_DecimateGauge",
                                WAR_InfuriateSTGauge = "WAR_InfuriateSTGauge",
                                WAR_InfuriateAoEGauge = "WAR_InfuriateAoEGauge";
        }


        internal class WAR_ST_Custom : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.WAR_Advanced_CustomMode;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is WAR.Maim)
                {
                    if (OnOpenerCustomActionAction(out var customActionActionId))
                    {
                        return customActionActionId;
                    }
                }


                return actionID;
            }
        }


        // Replace Storm's Path with Storm's Path combo and overcap feature on main combo to fellcleave
        // 
        internal class WAR_ST_StormsPath : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.WAR_ST_StormsPath;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (IsEnabled(CustomComboPreset.WAR_ST_StormsPath) && actionID == StormsPath)
                {
                    var gauge = GetJobGauge<WARGauge>().BeastGauge;
                    var surgingThreshold = PluginConfiguration.GetCustomIntValue(Config.WAR_SurgingRefreshRange);
                    var onslaughtChargesRemaining = PluginConfiguration.GetCustomIntValue(Config.WAR_KeepOnslaughtCharges);
                    var fellCleaveGaugeSpend = PluginConfiguration.GetCustomIntValue(Config.WAR_FellCleaveGauge);
                    var infuriateGauge = PluginConfiguration.GetCustomIntValue(Config.WAR_InfuriateSTGauge);

                    if (IsEnabled(CustomComboPreset.WAR_Variant_Cure)
                        && IsEnabled(Variant.VariantCure)
                        && PlayerHealthPercentageHp() <= GetOptionValue(Config.WAR_VariantCure))
                        return Variant.VariantCure;

                    if (IsEnabled(CustomComboPreset.WAR_ST_StormsPath_RangedUptime) && LevelChecked(Tomahawk) && !InMeleeRange() && HasBattleTarget())
                        return Tomahawk;

                    if (IsEnabled(CustomComboPreset.WAR_ST_StormsPath_Infuriate)
                        && InCombat()
                        && ActionReady(战壕Infuriate)
                        && !HasEffect(Buffs.NascentChaos)
                        && gauge <= infuriateGauge
                        && CanWeave(actionID))
                    {
                        if (GetCooldownRemainingTime(战壕Infuriate) < 6)
                        {
                            return 战壕Infuriate;
                        }

                       
                        if (!HasEffect(Buffs.原初的解放InnerRelease))
                        {
                            if (HasEffect(Buffs.原初的解放InnerRelease1))
                            {
                                return 战壕Infuriate;
                            }
                            
                            if (RaidBuff.爆发期())
                            {
                                return 战壕Infuriate;
                            }
                        }

                    }

                    //Sub Storm's Eye level check
                    if (IsEnabled(CustomComboPreset.WAR_ST_StormsPath_InnerRelease)
                        && CanWeave(actionID)
                        && IsOffCooldown(OriginalHook(Berserk))
                        && LevelChecked(Berserk)
                        && !LevelChecked(StormsEye)
                        && InCombat())
                        return OriginalHook(Berserk);

                    if (HasEffect(Buffs.SurgingTempest) && InCombat())
                    {
                        if (CanWeave(actionID))
                        {
                            Status? sustainedDamage = FindTargetEffect(Variant.Debuffs.SustainedDamage);
                            if (IsEnabled(CustomComboPreset.WAR_Variant_SpiritDart)
                                && IsEnabled(Variant.VariantSpiritDart)
                                && (sustainedDamage is null || sustainedDamage?.RemainingTime <= 3))
                                return Variant.VariantSpiritDart;

                            if (IsEnabled(CustomComboPreset.WAR_Variant_Ultimatum)
                                && IsEnabled(Variant.VariantUltimatum)
                                && IsOffCooldown(Variant.VariantUltimatum))
                                return Variant.VariantUltimatum;


                            if (IsEnabled(CustomComboPreset.WAR_ST_StormsPath_InnerRelease)
                                && CanWeave(actionID)
                                && IsOffCooldown(OriginalHook(Berserk))
                                && LevelChecked(Berserk))
                                return OriginalHook(Berserk);

                            if (IsEnabled(CustomComboPreset.WAR_ST_StormsPath_Upheaval) && IsOffCooldown(Upheaval) && LevelChecked(Upheaval))
                                return Upheaval;

                            if (IsEnabled(CustomComboPreset.WAR_ST_StormsPath_原初激震) && HasEffect(Buffs.原初激震Pre) && LevelChecked(原初激震))
                                return 原初激震;

                            if (IsEnabled(CustomComboPreset.WAR_ST_StormsPath_Onslaught)
                                && LevelChecked(Onslaught)
                                && GetRemainingCharges(Onslaught) > onslaughtChargesRemaining)
                            {
                                if (IsNotEnabled(CustomComboPreset.WAR_ST_StormsPath_Onslaught_MeleeSpender)
                                    || (IsEnabled(CustomComboPreset.WAR_ST_StormsPath_Onslaught_MeleeSpender)
                                        && !IsMoving
                                        && GetTargetDistance() <= 1
                                        && (GetCooldownRemainingTime(原初的解放InnerRelease) > 40 || !LevelChecked(原初的解放InnerRelease))))
                                    return Onslaught;
                            }
                        }


                        if (IsEnabled(CustomComboPreset.WAR_ST_StormsPath_FellCleave) && LevelChecked(原初之魂InnerBeast))
                        {
                            if (HasEffect(Buffs.原初的解放InnerRelease) || (HasEffect(Buffs.NascentChaos) && InnerChaos.LevelChecked()))
                                return OriginalHook(原初之魂InnerBeast);

                            if (gauge >= 50 && LevelChecked(原初的解放InnerRelease) && GetCooldownRemainingTime(原初的解放InnerRelease) >= 20 && RaidBuff.爆发期())
                            {
                                return OriginalHook(原初之魂InnerBeast);
                            }


                            if (HasEffect(Buffs.NascentChaos) && !InnerChaos.LevelChecked())
                                return OriginalHook(地毁人亡Decimate);
                        }

                        if (IsEnabled(CustomComboPreset.WAR_ST_StormsPath_PrimalRend) && HasEffect(Buffs.PrimalRendReady) && LevelChecked(蛮荒崩裂PrimalRend))
                        {
                            if (IsEnabled(CustomComboPreset.WAR_ST_StormsPath_PrimalRend_CloseRange)
                                && !IsMoving
                                && (GetTargetDistance() <= 1 || GetBuffRemainingTime(Buffs.PrimalRendReady) <= 10))
                                return 蛮荒崩裂PrimalRend;
                            if (IsNotEnabled(CustomComboPreset.WAR_ST_StormsPath_PrimalRend_CloseRange))
                                return 蛮荒崩裂PrimalRend;
                        }

                        if (IsEnabled(CustomComboPreset.WAR_ST_StormsPath_破坏斧) && 尽毁.LevelChecked() && HasEffect(Buffs.尽毁Pre))
                        {
                            return 尽毁;
                        }


                    }

                    if (comboTime > 0)
                    {
                        if (IsEnabled(CustomComboPreset.WAR_ST_StormsPath_FellCleave)
                            && LevelChecked(原初之魂InnerBeast)
                            && (!LevelChecked(StormsEye) || HasEffectAny(Buffs.SurgingTempest))
                            && gauge >= fellCleaveGaugeSpend)
                            return OriginalHook(原初之魂InnerBeast);

                        if (lastComboMove == HeavySwing && LevelChecked(Maim))
                        {
                            return Maim;
                        }

                        if (lastComboMove == Maim && LevelChecked(StormsPath))
                        {
                            if ((GetBuffRemainingTime(Buffs.SurgingTempest) <= surgingThreshold) && LevelChecked(StormsEye))
                                return StormsEye;

                            return StormsPath;
                        }
                    }

                    return HeavySwing;
                }

                return actionID;
            }
        }

        internal class War_ST_StormsEye : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.War_ST_StormsEye;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID == StormsEye)
                {
                    if (comboTime > 0)
                    {
                        if (lastComboMove == HeavySwing && LevelChecked(Maim))
                            return Maim;

                        if (lastComboMove == Maim && LevelChecked(StormsEye))
                            return StormsEye;
                    }

                    return HeavySwing;
                }

                return actionID;
            }
        }

        internal class WAR_AoE_Overpower : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.WAR_AoE_Overpower;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID == Overpower)
                {
                    var gauge = GetJobGauge<WARGauge>().BeastGauge;
                    var decimateGaugeSpend = PluginConfiguration.GetCustomIntValue(Config.WAR_DecimateGauge);
                    var infuriateGauge = PluginConfiguration.GetCustomIntValue(Config.WAR_InfuriateAoEGauge);

                    if (IsEnabled(CustomComboPreset.WAR_Variant_Cure)
                        && IsEnabled(Variant.VariantCure)
                        && PlayerHealthPercentageHp() <= GetOptionValue(Config.WAR_VariantCure))
                        return Variant.VariantCure;

                    if (IsEnabled(CustomComboPreset.WAR_AoE_Overpower_Infuriate)
                        && InCombat()
                        && ActionReady(战壕Infuriate)
                        && !HasEffect(Buffs.NascentChaos)
                        && gauge <= infuriateGauge
                        && CanWeave(actionID))
                        return 战壕Infuriate;

                    //Sub Mythril Tempest level check
                    if (IsEnabled(CustomComboPreset.WAR_AoE_Overpower_InnerRelease)
                        && CanWeave(actionID)
                        && IsOffCooldown(OriginalHook(Berserk))
                        && LevelChecked(Berserk)
                        && !LevelChecked(MythrilTempest)
                        && InCombat())
                        return OriginalHook(Berserk);

                    if (HasEffect(Buffs.SurgingTempest) && InCombat())
                    {
                        if (CanWeave(actionID))
                        {
                            Status? sustainedDamage = FindTargetEffect(Variant.Debuffs.SustainedDamage);
                            if (IsEnabled(CustomComboPreset.WAR_Variant_SpiritDart)
                                && IsEnabled(Variant.VariantSpiritDart)
                                && (sustainedDamage is null || sustainedDamage?.RemainingTime <= 3))
                                return Variant.VariantSpiritDart;

                            if (IsEnabled(CustomComboPreset.WAR_Variant_Ultimatum)
                                && IsEnabled(Variant.VariantUltimatum)
                                && IsOffCooldown(Variant.VariantUltimatum))
                                return Variant.VariantUltimatum;

                            if (IsEnabled(CustomComboPreset.WAR_AoE_Overpower_InnerRelease)
                                && CanWeave(actionID)
                                && IsOffCooldown(OriginalHook(Berserk))
                                && LevelChecked(Berserk))
                                return OriginalHook(Berserk);
                            if (IsEnabled(CustomComboPreset.WAR_AoE_Overpower_Orogeny)
                                && IsOffCooldown(Orogeny)
                                && LevelChecked(Orogeny)
                                && HasEffect(Buffs.SurgingTempest))
                                return Orogeny;
                        }

                        if (IsEnabled(CustomComboPreset.WAR_ST_StormsPath_PrimalRend) && HasEffect(Buffs.PrimalRendReady) && LevelChecked(蛮荒崩裂PrimalRend))
                        {
                            if (IsEnabled(CustomComboPreset.WAR_ST_StormsPath_PrimalRend_CloseRange)
                                && (GetTargetDistance() <= 3 || GetBuffRemainingTime(Buffs.PrimalRendReady) <= 10))
                                return 蛮荒崩裂PrimalRend;
                            if (IsNotEnabled(CustomComboPreset.WAR_ST_StormsPath_PrimalRend_CloseRange))
                                return 蛮荒崩裂PrimalRend;
                        }

                        if (IsEnabled(CustomComboPreset.WAR_AoE_Overpower_Decimate)
                            && LevelChecked(钢铁旋风SteelCyclone)
                            && (gauge >= decimateGaugeSpend || HasEffect(Buffs.原初的解放InnerRelease) || HasEffect(Buffs.NascentChaos)))
                            return OriginalHook(钢铁旋风SteelCyclone);
                    }

                    if (尽毁.LevelChecked() && HasEffect(Buffs.尽毁Pre))
                    {
                        return 尽毁;
                    }


                    if (IsEnabled(CustomComboPreset.WAR_ST_StormsPath_原初激震) && HasEffect(Buffs.原初激震Pre) && LevelChecked(原初激震))
                    {
                        return 原初激震;
                    }

                    if (comboTime > 0)
                    {
                        if (lastComboMove == Overpower && LevelChecked(MythrilTempest))
                        {
                            return MythrilTempest;
                        }
                    }

                    return Overpower;
                }

                return actionID;
            }
        }

        internal class WAR_NascentFlash : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.WAR_NascentFlash;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID == NascentFlash)
                {
                    if (LevelChecked(NascentFlash))
                        return NascentFlash;

                    return RawIntuition;
                }

                return actionID;
            }
        }


        internal class WAR_ST_StormsPath_PrimalRend : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.WAR_ST_StormsPath_PrimalRend;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID == 原初之魂InnerBeast || actionID == 钢铁旋风SteelCyclone)
                {
                    if (LevelChecked(蛮荒崩裂PrimalRend) && HasEffect(Buffs.PrimalRendReady))
                        return 蛮荒崩裂PrimalRend;

                    // Fell Cleave or Decimate
                    return OriginalHook(actionID);
                }

                return actionID;
            }
        }

        internal class WAR_InfuriateFellCleave : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.WAR_InfuriateFellCleave;

            protected override uint Invoke(uint actionID, uint lastComboActionID, float comboTime, byte level)
            {
                if (actionID is 原初之魂InnerBeast or 裂石飞环FellCleave or 钢铁旋风SteelCyclone or 地毁人亡Decimate)
                {
                    var rageGauge = GetJobGauge<WARGauge>();
                    var rageThreshold = PluginConfiguration.GetCustomIntValue(Config.WAR_InfuriateRange);
                    var hasNascent = HasEffect(Buffs.NascentChaos);
                    var hasInnerRelease = HasEffect(Buffs.原初的解放InnerRelease);

                    if (InCombat()
                        && rageGauge.BeastGauge <= rageThreshold
                        && ActionReady(战壕Infuriate)
                        && !hasNascent
                        && ((!hasInnerRelease) || IsNotEnabled(CustomComboPreset.WAR_InfuriateFellCleave_IRFirst)))
                        return OriginalHook(战壕Infuriate);
                }

                return actionID;
            }
        }

        internal class WAR_PrimalRend_InnerRelease : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.WAR_PrimalRend_InnerRelease;

            protected override uint Invoke(uint actionID, uint lastComboActionID, float comboTime, byte level)
            {
                if (actionID is Berserk or 原初的解放InnerRelease)
                {
                    if (LevelChecked(蛮荒崩裂PrimalRend) && HasEffect(Buffs.PrimalRendReady))
                    {
                        return 蛮荒崩裂PrimalRend;
                    }
                    if (尽毁.LevelChecked() && HasEffect(Buffs.尽毁Pre))
                    {
                        return 尽毁;
                    }
                }

                return actionID;
            }
        }
    }
}