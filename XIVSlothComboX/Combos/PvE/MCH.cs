using Dalamud.Game.ClientState.JobGauge.Types;
using ECommons.DalamudServices;
using XIVSlothComboX.Combos.JobHelpers;
using XIVSlothComboX.Combos.PvE.Content;
using XIVSlothComboX.Core;
using XIVSlothComboX.CustomComboNS;
using XIVSlothComboX.CustomComboNS.Functions;
using XIVSlothComboX.Data;
using XIVSlothComboX.Extensions;

namespace XIVSlothComboX.Combos.PvE
{
    internal class MCH
    {
        public const byte JobID = 31;
        public const byte 钻头倒计时偏移 = 40;


        public const uint
            CleanShot = 2873,
            HeatedCleanShot = 7413,
            SplitShot = 2866,
            HeatedSplitShot = 7411,
            SlugShot = 2868,
            HeatedSlugShot = 7412,
            虹吸弹GaussRound = 2874,
            弹射Ricochet = 2890,
            整备Reassemble = 2876,
            钻头Drill = 16498,
            HotShot = 2872,
            空气锚AirAnchor = 16500,
            超荷Hypercharge = 17209,
            热冲击HeatBlast = 7410,
            SpreadShot = 2870,
            Scattergun = 25786,
            AutoCrossbow = 16497,
            车式浮空炮塔RookAutoturret = 2864,
            超档车式炮塔RookOverdrive = 7415,
            后式自走人偶AutomatonQueen = 16501,
            QueenOverdrive = 16502,
            Tactician = 16889,
            回转飞锯ChainSaw = 25788,
            BioBlaster = 16499,
            枪管加热BarrelStabilizer = 7414,
            野火Wildfire = 2878,
            Dismantle = 2887,
            Flamethrower = 7418,
            BlazingShot = 36978,
            DoubleCheck = 36979,
            CheckMate = 36980,
            Excavator = 36981,
            FullMetalField = 36982;

        public static class Buffs
        {
            public const ushort
                Reassembled = 851,
                Tactician = 1951,
                Wildfire = 1946,
                Overheated = 2688,
                Flamethrower = 1205,
                Hypercharged = 3864,
                ExcavatorReady = 3865,
                FullMetalMachinist = 3866;
        }

        public static class Debuffs
        {
            public const ushort
                Dismantled = 2887,
                Bioblaster = 1866;
        }


        public static class Traits
        {
            public const ushort
                EnhancedMultiWeapon = 605;
        }

        public static class Config
        {
            public static UserInt
                MCH_ST_SecondWindThreshold = new("MCH_ST_SecondWindThreshold", 25),
                MCH_AoE_SecondWindThreshold = new("MCH_AoE_SecondWindThreshold", 25),
                MCH_VariantCure = new("MCH_VariantCure"),
                MCH_ST_TurretUsage = new("MCH_ST_Adv_TurretGauge"),
                MCH_AoE_TurretUsage = new("MCH_AoE_TurretUsage"),
                MCH_ST_ReassemblePool = new("MCH_ST_ReassemblePool", 0),
                MCH_AoE_ReassemblePool = new("MCH_AoE_ReassemblePool", 0),
                MCH_ST_WildfireHP = new("MCH_ST_WildfireHP", 0),
                MCH_ST_HyperchargeHP = new("MCH_ST_HyperchargeHP", 0),
                MCH_ST_QueenOverDrive = new("MCH_ST_QueenOverDrive", 100);

            public static UserBoolArray
                MCH_ST_Reassembled = new("MCH_ST_Reassembled"),
                MCH_AoE_Reassembled = new("MCH_AoE_Reassembled");

            public static UserBool
                MCH_AoE_Hypercharge = new("MCH_AoE_Hypercharge");
        }

        internal class MCH_ST_Custom : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.MCH_ST_CustomMode;


            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is SlugShot or HeatedSlugShot)
                {
                    if (CustomTimelineIsEnable())
                    {
                        double? seconds = -9999d;

                        if (InCombat())
                        {
                            seconds = CombatEngageDuration().TotalSeconds;
                        }
                        else
                        {
                            var timeRemaining = Countdown.TimeRemaining();
                            if (timeRemaining != null)
                            {
                                seconds = -timeRemaining;
                            }
                        }

                        foreach (var customAction in 药品轴)
                        {
                            if (customAction.UseTimeStart < seconds && seconds < customAction.UseTimeEnd)
                            {
                                Useitem(customAction.ActionId);
                            }
                        }


                        foreach (var customAction in 时间轴)
                        {
                            if (customAction.ActionId.ActionReady() && customAction.UseTimeStart < seconds && seconds < customAction.UseTimeEnd)
                            {
                                return customAction.ActionId;
                            }
                        }


                        int index = ActionWatching.CustomList.Count;
                        if (index < 序列轴.Count)
                        {
                            var newActionId = 序列轴[index].ActionId;
                            return newActionId;
                        }
                    }
                }


                return actionID;
            }
        }


        internal class MCH_ST_AdvancedMode : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.MCH_ST_AdvancedMode;
            internal static MCHOpenerLogic MCHOpener = new();

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                MCHGauge? gauge = GetJobGauge<MCHGauge>();

                bool interruptReady = ActionReady(All.HeadGraze) && CanInterruptEnemy() && CanDelayedWeave(actionID);
                float heatblastRC = GetCooldown(热冲击HeatBlast).CooldownTotal;
                bool drillCD = !LevelChecked(钻头Drill)
                               || (!TraitLevelChecked(Traits.EnhancedMultiWeapon) && GetCooldownRemainingTime(钻头Drill) > heatblastRC * 6)
                               || (TraitLevelChecked(Traits.EnhancedMultiWeapon) && GetRemainingCharges(钻头Drill) < GetMaxCharges(钻头Drill) && GetCooldownRemainingTime(钻头Drill) > heatblastRC * 6);

                bool anchorCD = !LevelChecked(空气锚AirAnchor) || (LevelChecked(空气锚AirAnchor) && GetCooldownRemainingTime(空气锚AirAnchor) > heatblastRC * 6);
                bool sawCD = !LevelChecked(回转飞锯ChainSaw) || (LevelChecked(回转飞锯ChainSaw) && GetCooldownRemainingTime(回转飞锯ChainSaw) > heatblastRC * 6);
                float GCD = GetCooldown(OriginalHook(SplitShot)).CooldownTotal;
                float wildfireCDTime = GetCooldownRemainingTime(野火Wildfire);


                if (actionID is SplitShot or HeatedSplitShot)
                {

                    // Opener for MCH
                    if (IsEnabled(CustomComboPreset.MCH_ST_Adv_Opener))
                    {
                        if (MCHOpener.DoFullOpener(ref actionID))
                            return actionID;
                    }


                    if (IsEnabled(CustomComboPreset.MCH_Variant_Cure) && IsEnabled(Variant.VariantCure) && PlayerHealthPercentageHp() <= Config.MCH_VariantCure)
                        return Variant.VariantCure;

                    if (IsEnabled(CustomComboPreset.MCH_Variant_Rampart) && IsEnabled(Variant.VariantRampart) && IsOffCooldown(Variant.VariantRampart) && CanWeave(actionID))
                        return Variant.VariantRampart;


                    // Interrupt
                    if (IsEnabled(CustomComboPreset.MCH_ST_Adv_Interrupt) && interruptReady)
                        return All.HeadGraze;

                    if (IsEnabled(CustomComboPreset.MCH_ST_Adv_QueenOverdrive) && gauge.IsRobotActive && GetTargetHP() <= Config.MCH_ST_QueenOverDrive * 10000 && CanWeave(actionID) && ActionReady(OriginalHook(超档车式炮塔RookOverdrive)))
                    {
                        return OriginalHook(超档车式炮塔RookOverdrive);
                    }

                    // Wildfire
                    if (IsEnabled(CustomComboPreset.MCH_ST_Adv_WildFire) && WasLastAbility(超荷Hypercharge) && CanWeave(actionID) && ActionReady(野火Wildfire) && GetTargetHP() >= Config.MCH_ST_WildfireHP * 10000)
                    {
                        return 野火Wildfire;
                    }

                    // BarrelStabilizer
                    if (IsEnabled(CustomComboPreset.MCH_ST_Adv_Stabilizer) && !gauge.IsOverheated && CanWeave(actionID) && ActionReady(枪管加热BarrelStabilizer))
                    {
                        return 枪管加热BarrelStabilizer;
                    }

                    // if (IsEnabled(CustomComboPreset.MCH_ST_Adv_Hypercharge) && CanWeave(actionID) && (gauge.Heat >= 50 || HasEffect(Buffs.Hypercharged)) && LevelChecked(超荷Hypercharge) && !gauge.IsOverheated && GetTargetHP() >= Config.MCH_ST_HyperchargeHP * 10000)
                    if (IsEnabled(CustomComboPreset.MCH_ST_Adv_Hypercharge) && CanWeave(actionID) && (gauge.Heat >= 50 || HasEffect(Buffs.Hypercharged)) && LevelChecked(超荷Hypercharge) && !gauge.IsOverheated)
                    {
                        //Protection & ensures Hyper charged is double weaved with WF during reopener
                        if ((LevelChecked(FullMetalField) && WasLastWeaponskill(FullMetalField) && (GetCooldownRemainingTime(野火Wildfire) < GCD || ActionReady(野火Wildfire))) || ((!LevelChecked(FullMetalField)) && ActionReady(野火Wildfire)) || !LevelChecked(野火Wildfire))
                        {
                            return 超荷Hypercharge;
                        }

                        if (drillCD && anchorCD && sawCD && (GetCooldownRemainingTime(野火Wildfire) > 40 && LevelChecked(野火Wildfire) || !LevelChecked(野火Wildfire)))
                        {
                            return 超荷Hypercharge;
                        }

                        //超荷判断
                        if (IsEnabled(CustomComboPreset.MCH_ST_Adv_Hypercharge)
                            && CanSpellWeavePlus(actionID)
                            && gauge.Heat >= 50
                            && 超荷Hypercharge.ActionReady()
                            && GetCooldownRemainingTime(虹吸弹GaussRound) > 50
                            && GetCooldownRemainingTime(弹射Ricochet) > 50
                            && !gauge.IsOverheated)
                        {
                            if (LevelChecked(钻头Drill) && GetCooldownRemainingTime(钻头Drill) > 7.5)
                            {
                                if (LevelChecked(空气锚AirAnchor) && GetCooldownRemainingTime(空气锚AirAnchor) > 7.5)
                                {
                                    if (LevelChecked(回转飞锯ChainSaw) && GetCooldownRemainingTime(回转飞锯ChainSaw) > 7.5)
                                    {
                                        if (UseHyperchargeDelayedTools(gauge, wildfireCDTime))
                                            return 超荷Hypercharge;
                                    }

                                    else if (!LevelChecked(回转飞锯ChainSaw))
                                    {
                                        if (UseHyperchargeDelayedTools(gauge, wildfireCDTime))
                                            return 超荷Hypercharge;
                                    }
                                }

                                else if (!LevelChecked(空气锚AirAnchor))
                                {
                                    if (UseHyperchargeDelayedTools(gauge, wildfireCDTime))
                                        return 超荷Hypercharge;
                                }
                            }
                            else if (!LevelChecked(钻头Drill))
                            {
                                if (UseHyperchargeDelayedTools(gauge, wildfireCDTime))
                                    return 超荷Hypercharge;
                            }
                        }
                    }

                    //Full Metal Field
                    if (IsEnabled(CustomComboPreset.MCH_ST_Adv_Stabilizer_FullMetalField) && HasEffect(Buffs.FullMetalMachinist) && (GetCooldownRemainingTime(野火Wildfire) <= GCD || ActionReady(野火Wildfire) || GetBuffRemainingTime(Buffs.FullMetalMachinist) <= 6) && LevelChecked(FullMetalField))
                    {
                        return OriginalHook(枪管加热BarrelStabilizer);
                    }

                    //queen 优雅的使用机器人
                    if (IsEnabled(CustomComboPreset.MCH_Adv_TurretQueen)
                        && Config.MCH_ST_TurretUsage == 1
                        && !WasLastAction(野火Wildfire)
                        && CanSpellWeavePlus(actionID)
                        // && !gauge.IsOverheated
                        && OriginalHook(车式浮空炮塔RookAutoturret).ActionReady()
                        && !gauge.IsRobotActive)
                    {
                        if (gauge.Battery >= 80)
                        {
                            var 空气锚快好了 = GetCooldownRemainingTime(OriginalHook(空气锚AirAnchor)) <= 3 || ActionReady(OriginalHook(空气锚AirAnchor));

                            if (空气锚快好了)
                                return OriginalHook(车式浮空炮塔RookAutoturret);

                            var 回转飞锯ChainSaw快好了 = GetCooldownRemainingTime(回转飞锯ChainSaw) <= 3 || ActionReady(OriginalHook(回转飞锯ChainSaw));

                            if (回转飞锯ChainSaw快好了)
                                return OriginalHook(车式浮空炮塔RookAutoturret);

                        }

                        if (gauge.Battery >= 90)
                        {
                            if (lastComboMove is SlugShot or HeatedSlugShot)
                            {
                                return OriginalHook(车式浮空炮塔RookAutoturret);
                            }
                        }

                        if (gauge.Battery >= 90)
                        {
                            return OriginalHook(车式浮空炮塔RookAutoturret);
                        }
                    }

                    //机器人好了就用
                    if (IsEnabled(CustomComboPreset.MCH_Adv_TurretQueen)
                        && Config.MCH_ST_TurretUsage == 0
                        && !WasLastAction(野火Wildfire)
                        && !gauge.IsRobotActive
                        && CanSpellWeavePlus(actionID)
                        && OriginalHook(车式浮空炮塔RookAutoturret).ActionReady()
                        && gauge.Battery >= 50)
                    {
                        if (HasEffect(Buffs.Wildfire) && gauge.Heat < 50)
                        {
                            return OriginalHook(车式浮空炮塔RookAutoturret);
                        }

                        if (!HasEffect(Buffs.Wildfire) && gauge.Battery >= 50)
                        {
                            return OriginalHook(车式浮空炮塔RookAutoturret);
                        }

                    }


                    //Heatblast, Gauss, Rico
                    if (IsEnabled(CustomComboPreset.MCH_ST_Adv_GaussRicochet) && LevelChecked(热冲击HeatBlast.OriginalHook()))
                    {
                        if (CanSpellWeavePlus(actionID))
                        {
                            //超荷状态下
                            if (gauge.IsOverheated)
                            {
                                if (WasLastAction(热冲击HeatBlast.OriginalHook())
                                    && GetCooldownRemainingTime(虹吸弹GaussRound) >= GetCooldownRemainingTime(弹射Ricochet)
                                    && 弹射Ricochet.ActionReady())
                                {
                                    return 弹射Ricochet.OriginalHook();
                                }

                                if (WasLastAction(热冲击HeatBlast.OriginalHook())
                                    && GetCooldownRemainingTime(弹射Ricochet) >= GetCooldownRemainingTime(虹吸弹GaussRound)
                                    && 虹吸弹GaussRound.ActionReady())
                                {
                                    return 虹吸弹GaussRound.OriginalHook();
                                }
                            }

                            if (!gauge.IsOverheated)
                            {
                                if (WasLastAction(热冲击HeatBlast.OriginalHook())
                                    && GetCooldownRemainingTime(弹射Ricochet) >= GetCooldownRemainingTime(虹吸弹GaussRound)
                                    && 虹吸弹GaussRound.ActionReady())
                                {
                                    return 虹吸弹GaussRound.OriginalHook();
                                }

                                if (WasLastAction(热冲击HeatBlast.OriginalHook())
                                    && GetCooldownRemainingTime(虹吸弹GaussRound) >= GetCooldownRemainingTime(弹射Ricochet)
                                    && 弹射Ricochet.ActionReady())
                                {
                                    return 弹射Ricochet.OriginalHook();
                                }


                                if (!HasEffect(Buffs.Wildfire) && !WasLastAction(野火Wildfire))
                                {
                                    if (!WasLastAction(虹吸弹GaussRound.OriginalHook())
                                        && 虹吸弹GaussRound.ActionReady()
                                        && GetCooldownRemainingTime(虹吸弹GaussRound) >= 0
                                        && GetCooldownRemainingTime(虹吸弹GaussRound) <= 59)
                                    {
                                        return 虹吸弹GaussRound.OriginalHook();
                                    }

                                    if (!WasLastAction(弹射Ricochet.OriginalHook())
                                        && 弹射Ricochet.ActionReady()
                                        && GetCooldownRemainingTime(弹射Ricochet) >= 0
                                        && GetCooldownRemainingTime(弹射Ricochet) <= 59)
                                    {
                                        return 弹射Ricochet.OriginalHook();
                                    }


                                    if (wildfireCDTime >= 40 && RaidBuff.爆发期())
                                    {
                                        if (!WasLastAction(虹吸弹GaussRound.OriginalHook()) && 虹吸弹GaussRound.ActionReady())
                                        {
                                            return 虹吸弹GaussRound.OriginalHook();
                                        }

                                        if (!WasLastAction(弹射Ricochet.OriginalHook()) && 弹射Ricochet.ActionReady())
                                        {
                                            return 弹射Ricochet.OriginalHook();
                                        }
                                    }
                                }

                            }

                        }

                    }

                    if (IsEnabled(CustomComboPreset.MCH_ST_Adv_Heatblast) && gauge.IsOverheated && LevelChecked(OriginalHook(热冲击HeatBlast)))
                        return OriginalHook(热冲击HeatBlast);


                    if (ReassembledTools(ref actionID, gauge) && !gauge.IsOverheated)
                        return actionID;

                    // healing
                    if (IsEnabled(CustomComboPreset.MCH_ST_Adv_SecondWind) && CanWeave(actionID) && PlayerHealthPercentageHp() <= Config.MCH_ST_SecondWindThreshold && ActionReady(All.SecondWind))
                        return All.SecondWind;

                    //1-2-3 Combo
                    if (comboTime > 0)
                    {
                        if (lastComboMove is SplitShot && LevelChecked(OriginalHook(SlugShot)))
                            return OriginalHook(SlugShot);

                        if (lastComboMove is SlugShot && LevelChecked(OriginalHook(CleanShot)))
                            return OriginalHook(CleanShot);
                    }
                    return OriginalHook(SplitShot);
                }
                return actionID;
            }

            private static bool ReassembledTools(ref uint actionID, MCHGauge gauge)
            {
                bool battery = Svc.Gauges.Get<MCHGauge>().Battery >= 100;


                bool reassembledExcavator = (IsEnabled(CustomComboPreset.MCH_ST_Adv_Reassemble) && Config.MCH_ST_Reassembled[0] && (HasEffect(Buffs.Reassembled) || !HasEffect(Buffs.Reassembled))) || (IsEnabled(CustomComboPreset.MCH_ST_Adv_Reassemble) && !Config.MCH_ST_Reassembled[0] && !HasEffect(Buffs.Reassembled)) || (!HasEffect(Buffs.Reassembled) && GetRemainingCharges(整备Reassemble) <= Config.MCH_ST_ReassemblePool) || (!IsEnabled(CustomComboPreset.MCH_ST_Adv_Reassemble));
                bool reassembledChainsaw = (IsEnabled(CustomComboPreset.MCH_ST_Adv_Reassemble) && Config.MCH_ST_Reassembled[1] && (HasEffect(Buffs.Reassembled) || !HasEffect(Buffs.Reassembled))) || (IsEnabled(CustomComboPreset.MCH_ST_Adv_Reassemble) && !Config.MCH_ST_Reassembled[1] && !HasEffect(Buffs.Reassembled)) || (!HasEffect(Buffs.Reassembled) && GetRemainingCharges(整备Reassemble) <= Config.MCH_ST_ReassemblePool) || (!IsEnabled(CustomComboPreset.MCH_ST_Adv_Reassemble));
                bool reassembledAnchor = (IsEnabled(CustomComboPreset.MCH_ST_Adv_Reassemble) && Config.MCH_ST_Reassembled[2] && (HasEffect(Buffs.Reassembled) || !HasEffect(Buffs.Reassembled))) || (IsEnabled(CustomComboPreset.MCH_ST_Adv_Reassemble) && !Config.MCH_ST_Reassembled[2] && !HasEffect(Buffs.Reassembled)) || (!HasEffect(Buffs.Reassembled) && GetRemainingCharges(整备Reassemble) <= Config.MCH_ST_ReassemblePool) || (!IsEnabled(CustomComboPreset.MCH_ST_Adv_Reassemble));
                bool reassembledDrill = (IsEnabled(CustomComboPreset.MCH_ST_Adv_Reassemble) && Config.MCH_ST_Reassembled[3] && (HasEffect(Buffs.Reassembled) || !HasEffect(Buffs.Reassembled))) || (IsEnabled(CustomComboPreset.MCH_ST_Adv_Reassemble) && !Config.MCH_ST_Reassembled[3] && !HasEffect(Buffs.Reassembled)) || (!HasEffect(Buffs.Reassembled) && GetRemainingCharges(整备Reassemble) <= Config.MCH_ST_ReassemblePool) || (!IsEnabled(CustomComboPreset.MCH_ST_Adv_Reassemble));


                // TOOLS!! Chainsaw Drill Air Anchor Excavator
                if (IsEnabled(CustomComboPreset.MCH_ST_Adv_Reassemble)
                    && !gauge.IsOverheated
                    && !HasEffect(Buffs.Reassembled)
                    && ActionReady(整备Reassemble)
                    && (CanWeave(actionID) || !InCombat())
                    && GetRemainingCharges(整备Reassemble) > Config.MCH_ST_ReassemblePool
                    && ((Config.MCH_ST_Reassembled[0] && LevelChecked(Excavator) && HasEffect(Buffs.ExcavatorReady) && !battery)
                        || (Config.MCH_ST_Reassembled[1] && LevelChecked(回转飞锯ChainSaw) && (!LevelChecked(Excavator) || !Config.MCH_ST_Reassembled[0]) && ((GetCooldownRemainingTime(回转飞锯ChainSaw) <= GetCooldownRemainingTime(OriginalHook(SplitShot)) + 0.25) || ActionReady(回转飞锯ChainSaw) || 回转飞锯ChainSaw.GCDActionReady()) && !battery)
                        || (Config.MCH_ST_Reassembled[2] && LevelChecked(空气锚AirAnchor) && ((GetCooldownRemainingTime(空气锚AirAnchor) <= GetCooldownRemainingTime(OriginalHook(SplitShot)) + 0.55) || ActionReady(空气锚AirAnchor) || 空气锚AirAnchor.GCDActionReady() ) && !battery)
                        || (Config.MCH_ST_Reassembled[3] && LevelChecked(钻头Drill) && (!LevelChecked(空气锚AirAnchor) || !Config.MCH_ST_Reassembled[2]) && (GetCooldownRemainingTime(钻头Drill) <= GetCooldownRemainingTime(OriginalHook(SplitShot)) + 0.25) || ActionReady(钻头Drill)|| 钻头Drill.GCDActionReady()))
                   )
                {
                    actionID = 整备Reassemble;
                    return true;
                }

                if (IsEnabled(CustomComboPreset.MCH_ST_Adv_Excavator) && reassembledExcavator && LevelChecked(OriginalHook(回转飞锯ChainSaw)) && !battery && HasEffect(Buffs.ExcavatorReady))
                {
                    actionID = OriginalHook(回转飞锯ChainSaw);
                    return true;
                }

                if (IsEnabled(CustomComboPreset.MCH_ST_Adv_Chainsaw) && reassembledChainsaw && LevelChecked(回转飞锯ChainSaw) && !battery && 回转飞锯ChainSaw.GCDActionReady(CleanShot))
                {
                    actionID = 回转飞锯ChainSaw;
                    return true;
                }

                if (IsEnabled(CustomComboPreset.MCH_ST_Adv_AirAnchor) && reassembledAnchor && LevelChecked(OriginalHook(空气锚AirAnchor)) && !battery && 空气锚AirAnchor.GCDActionReady(CleanShot))
                {
                    actionID = OriginalHook(空气锚AirAnchor);
                    return true;
                }


                if (IsEnabled(CustomComboPreset.MCH_ST_Adv_Drill) && LevelChecked(钻头Drill))
                {
                    if (GetCooldownRemainingTime(野火Wildfire) is > 40)
                    {

                        if (GetCooldownRemainingTime(钻头Drill) < 20)
                        {
                            if (IsEnabled(CustomComboPreset.MCH_ST_Adv_Reassemble)
                                && !gauge.IsOverheated
                                && CanWeave(actionID)
                                && Config.MCH_ST_Reassembled[3]
                                && LevelChecked(钻头Drill)
                                && GetRemainingCharges(整备Reassemble) > Config.MCH_ST_ReassemblePool
                                && !HasEffect(Buffs.Reassembled)
                                && ActionReady(整备Reassemble))
                            {
                                actionID = 整备Reassemble;
                                return true;
                            }

                            actionID = 钻头Drill;
                            return true;
                        }
                    }
                    else
                    {
                        if (钻头Drill.GCDActionReady())
                        {
                            actionID = 钻头Drill;
                            return true;
                        }
                    }

                }

                return false;
            }

            private bool UseHyperchargeDelayedTools(MCHGauge gauge, float wildfireCDTime)
            {
                if (!LevelChecked(超荷Hypercharge))
                {
                    return false;
                }

                if (野火Wildfire.ActionReady())
                {
                    return false;
                }


                if (gauge.Heat >= 50)
                {

                    if (wildfireCDTime is >= 0.5f and <= 33)
                    {
                        if (GetCooldownRemainingTime(枪管加热BarrelStabilizer) < wildfireCDTime)
                        {
                            return true;
                        }

                        return false;
                    }


                    if (wildfireCDTime >= 60)
                    {
                        return true;
                    }

                    if (gauge.Heat >= 50)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        internal class MCH_AoE_AdvancedMode : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.MCH_AoE_AdvancedMode;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is SpreadShot or Scattergun)
                {
                    MCHGauge? gauge = GetJobGauge<MCHGauge>();
                    float GCD = GetCooldown(OriginalHook(SpreadShot)).CooldownTotal;
                    bool reassembledScattergun = IsEnabled(CustomComboPreset.MCH_AoE_Adv_Reassemble) && Config.MCH_AoE_Reassembled[0] && HasEffect(Buffs.Reassembled);
                    bool reassembledCrossbow = (IsEnabled(CustomComboPreset.MCH_AoE_Adv_Reassemble) && Config.MCH_AoE_Reassembled[1] && HasEffect(Buffs.Reassembled)) || (IsEnabled(CustomComboPreset.MCH_AoE_Adv_Reassemble) && !Config.MCH_AoE_Reassembled[1] && !HasEffect(Buffs.Reassembled)) || (!IsEnabled(CustomComboPreset.MCH_AoE_Adv_Reassemble));
                    bool reassembledChainsaw = (IsEnabled(CustomComboPreset.MCH_AoE_Adv_Reassemble) && Config.MCH_AoE_Reassembled[2] && HasEffect(Buffs.Reassembled)) || (IsEnabled(CustomComboPreset.MCH_AoE_Adv_Reassemble) && !Config.MCH_AoE_Reassembled[2] && !HasEffect(Buffs.Reassembled)) || (!HasEffect(Buffs.Reassembled) && GetRemainingCharges(整备Reassemble) <= Config.MCH_AoE_ReassemblePool) || (!IsEnabled(CustomComboPreset.MCH_AoE_Adv_Reassemble));
                    bool reassembledExcavator = (IsEnabled(CustomComboPreset.MCH_AoE_Adv_Reassemble) && Config.MCH_AoE_Reassembled[3] && HasEffect(Buffs.Reassembled)) || (IsEnabled(CustomComboPreset.MCH_AoE_Adv_Reassemble) && !Config.MCH_AoE_Reassembled[3] && !HasEffect(Buffs.Reassembled)) || (!HasEffect(Buffs.Reassembled) && GetRemainingCharges(整备Reassemble) <= Config.MCH_AoE_ReassemblePool) || (!IsEnabled(CustomComboPreset.MCH_AoE_Adv_Reassemble));

                    if (IsEnabled(CustomComboPreset.MCH_Variant_Cure) && IsEnabled(Variant.VariantCure) && PlayerHealthPercentageHp() <= GetOptionValue(Config.MCH_VariantCure))
                        return Variant.VariantCure;

                    if (HasEffect(Buffs.Flamethrower) || JustUsed(Flamethrower))
                        return OriginalHook(11);

                    if (IsEnabled(CustomComboPreset.MCH_Variant_Rampart) && IsEnabled(Variant.VariantRampart) && IsOffCooldown(Variant.VariantRampart) && CanWeave(actionID))
                        return Variant.VariantRampart;

                    //Full Metal Field
                    if (IsEnabled(CustomComboPreset.MCH_AoE_Adv_Stabilizer_FullMetalField) && HasEffect(Buffs.FullMetalMachinist) && LevelChecked(FullMetalField))
                        return OriginalHook(枪管加热BarrelStabilizer);

                    // BarrelStabilizer
                    if (IsEnabled(CustomComboPreset.MCH_AoE_Adv_Stabilizer) && !gauge.IsOverheated && CanWeave(actionID) && ActionReady(枪管加热BarrelStabilizer))
                        return 枪管加热BarrelStabilizer;

                    if (IsEnabled(CustomComboPreset.MCH_AoE_Adv_Bioblaster) && ActionReady(BioBlaster) && !TargetHasEffect(Debuffs.Bioblaster))
                        return OriginalHook(BioBlaster);

                    if (IsEnabled(CustomComboPreset.MCH_AoE_Adv_FlameThrower) && ActionReady(Flamethrower) && !IsMoving)
                        return OriginalHook(Flamethrower);

                    if (IsEnabled(CustomComboPreset.MCH_AoE_Adv_Queen) && !gauge.IsOverheated)
                    {
                        if (gauge.Battery >= Config.MCH_AoE_TurretUsage)
                            return OriginalHook(车式浮空炮塔RookAutoturret);
                    }

                    // Hypercharge        
                    if (IsEnabled(CustomComboPreset.MCH_AoE_Adv_Hypercharge) && (gauge.Heat >= 50 || HasEffect(Buffs.Hypercharged)) && LevelChecked(超荷Hypercharge) && LevelChecked(AutoCrossbow) && !gauge.IsOverheated && ((BioBlaster.LevelChecked() && GetCooldownRemainingTime(BioBlaster) > 10) || !BioBlaster.LevelChecked() || IsNotEnabled(CustomComboPreset.MCH_AoE_Adv_Bioblaster)) && ((Flamethrower.LevelChecked() && GetCooldownRemainingTime(Flamethrower) > 10) || !Flamethrower.LevelChecked() || IsNotEnabled(CustomComboPreset.MCH_AoE_Adv_FlameThrower)))
                        return 超荷Hypercharge;

                    //AutoCrossbow, Gauss, Rico
                    if (IsEnabled(CustomComboPreset.MCH_AoE_Adv_GaussRicochet) && !Config.MCH_AoE_Hypercharge && CanWeave(actionID) && WasLastWeaponskill(OriginalHook(AutoCrossbow)) && (ActionWatching.GetAttackType(ActionWatching.LastAction) != ActionWatching.ActionAttackType.Ability))
                    {
                        if (ActionReady(OriginalHook(虹吸弹GaussRound)) && GetRemainingCharges(OriginalHook(虹吸弹GaussRound)) >= GetRemainingCharges(OriginalHook(弹射Ricochet)))
                            return OriginalHook(虹吸弹GaussRound);

                        if (ActionReady(OriginalHook(弹射Ricochet)) && GetRemainingCharges(OriginalHook(弹射Ricochet)) > GetRemainingCharges(OriginalHook(虹吸弹GaussRound)))
                            return OriginalHook(弹射Ricochet);
                    }

                    if (gauge.IsOverheated && AutoCrossbow.LevelChecked())
                        return OriginalHook(AutoCrossbow);

                    //gauss and ricochet outside HC
                    if (IsEnabled(CustomComboPreset.MCH_AoE_Adv_GaussRicochet) && Config.MCH_AoE_Hypercharge && CanWeave(actionID) && !gauge.IsOverheated)
                    {
                        if (ActionReady(OriginalHook(虹吸弹GaussRound)) && !WasLastAbility(OriginalHook(虹吸弹GaussRound)))
                            return OriginalHook(虹吸弹GaussRound);

                        if (ActionReady(OriginalHook(弹射Ricochet)) && !WasLastAbility(OriginalHook(弹射Ricochet)))
                            return OriginalHook(弹射Ricochet);
                    }

                    if (IsEnabled(CustomComboPreset.MCH_AoE_Adv_Reassemble) && !HasEffect(Buffs.Wildfire) && !HasEffect(Buffs.Reassembled) && HasCharges(整备Reassemble) && GetRemainingCharges(整备Reassemble) > Config.MCH_AoE_ReassemblePool && ((Config.MCH_AoE_Reassembled[0] && Scattergun.LevelChecked()) || (gauge.IsOverheated && Config.MCH_AoE_Reassembled[1] && AutoCrossbow.LevelChecked()) || (GetCooldownRemainingTime(回转飞锯ChainSaw) < 1 && Config.MCH_AoE_Reassembled[2] && 回转飞锯ChainSaw.LevelChecked()) || (GetCooldownRemainingTime(OriginalHook(回转飞锯ChainSaw)) < 1 && Config.MCH_AoE_Reassembled[3] && Excavator.LevelChecked())))
                        return 整备Reassemble;

                    if (IsEnabled(CustomComboPreset.MCH_AoE_Adv_Excavator) && reassembledExcavator && LevelChecked(Excavator) && HasEffect(Buffs.ExcavatorReady))
                        return OriginalHook(回转飞锯ChainSaw);

                    if (IsEnabled(CustomComboPreset.MCH_AoE_Adv_Chainsaw) && reassembledChainsaw && LevelChecked(回转飞锯ChainSaw) && ((GetCooldownRemainingTime(回转飞锯ChainSaw) <= GCD + 0.25) || ActionReady(回转飞锯ChainSaw)))
                        return 回转飞锯ChainSaw;

                    if (reassembledScattergun)
                        return OriginalHook(Scattergun);

                    if (reassembledCrossbow && LevelChecked(AutoCrossbow) && gauge.IsOverheated)
                        return AutoCrossbow;

                    if (IsEnabled(CustomComboPreset.MCH_AoE_Adv_SecondWind))
                    {
                        if (PlayerHealthPercentageHp() <= Config.MCH_AoE_SecondWindThreshold && ActionReady(All.SecondWind))
                            return All.SecondWind;
                    }
                }
                return actionID;
            }
        }

        internal class MCH_HeatblastGaussRicochet : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.MCH_Heatblast;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                MCHGauge? gauge = GetJobGauge<MCHGauge>();

                if (actionID is 热冲击HeatBlast or BlazingShot)
                {
                    if (IsEnabled(CustomComboPreset.MCH_Heatblast_AutoBarrel) && ActionReady(枪管加热BarrelStabilizer) && !gauge.IsOverheated)
                        return 枪管加热BarrelStabilizer;

                    if (IsEnabled(CustomComboPreset.MCH_Heatblast_Wildfire) && ActionReady(野火Wildfire) && WasLastAbility(超荷Hypercharge))
                        return 野火Wildfire;

                    if (!gauge.IsOverheated && LevelChecked(超荷Hypercharge) && (gauge.Heat >= 50 || HasEffect(Buffs.Hypercharged)))
                        return 超荷Hypercharge;

                    //Heatblast, Gauss, Rico
                    if (IsEnabled(CustomComboPreset.MCH_Heatblast_GaussRound) && CanWeave(actionID) && WasLastWeaponskill(OriginalHook(热冲击HeatBlast)) && (ActionWatching.GetAttackType(ActionWatching.LastAction) != ActionWatching.ActionAttackType.Ability))
                    {
                        if (ActionReady(OriginalHook(虹吸弹GaussRound)) && GetRemainingCharges(OriginalHook(虹吸弹GaussRound)) >= GetRemainingCharges(OriginalHook(弹射Ricochet)))
                            return OriginalHook(虹吸弹GaussRound);

                        if (ActionReady(OriginalHook(弹射Ricochet)) && GetRemainingCharges(OriginalHook(弹射Ricochet)) > GetRemainingCharges(OriginalHook(虹吸弹GaussRound)))
                            return OriginalHook(弹射Ricochet);
                    }

                    if (gauge.IsOverheated && LevelChecked(OriginalHook(热冲击HeatBlast)))
                        return OriginalHook(热冲击HeatBlast);
                }
                return actionID;
            }
        }

        internal class MCH_AutoCrossbowGaussRicochet : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.MCH_AutoCrossbow;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                MCHGauge? gauge = GetJobGauge<MCHGauge>();

                if (actionID is AutoCrossbow)
                {
                    if (IsEnabled(CustomComboPreset.MCH_AutoCrossbow_AutoBarrel) && ActionReady(枪管加热BarrelStabilizer) && !gauge.IsOverheated)
                        return 枪管加热BarrelStabilizer;

                    if (!gauge.IsOverheated && LevelChecked(超荷Hypercharge) && (gauge.Heat >= 50 || HasEffect(Buffs.Hypercharged)))
                        return 超荷Hypercharge;

                    //Autocrossbow, Gauss, Rico
                    if (IsEnabled(CustomComboPreset.MCH_AutoCrossbow_GaussRound) && CanWeave(actionID) && WasLastWeaponskill(OriginalHook(AutoCrossbow)) && (ActionWatching.GetAttackType(ActionWatching.LastAction) != ActionWatching.ActionAttackType.Ability))
                    {
                        if (ActionReady(OriginalHook(虹吸弹GaussRound)) && GetRemainingCharges(OriginalHook(虹吸弹GaussRound)) >= GetRemainingCharges(OriginalHook(弹射Ricochet)))
                            return OriginalHook(虹吸弹GaussRound);

                        if (ActionReady(OriginalHook(弹射Ricochet)) && GetRemainingCharges(OriginalHook(弹射Ricochet)) > GetRemainingCharges(OriginalHook(虹吸弹GaussRound)))
                            return OriginalHook(弹射Ricochet);
                    }

                    if (gauge.IsOverheated && LevelChecked(OriginalHook(AutoCrossbow)))
                        return OriginalHook(AutoCrossbow);
                }
                return actionID;
            }
        }

        internal class MCH_GaussRoundRicochet : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.MCH_GaussRoundRicochet;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {

                if (actionID is 虹吸弹GaussRound or 弹射Ricochet or CheckMate or DoubleCheck)
                {
                    {
                        if (ActionReady(OriginalHook(虹吸弹GaussRound)) && GetRemainingCharges(OriginalHook(虹吸弹GaussRound)) >= GetRemainingCharges(OriginalHook(弹射Ricochet)))
                            return OriginalHook(虹吸弹GaussRound);

                        if (ActionReady(OriginalHook(弹射Ricochet)) && GetRemainingCharges(OriginalHook(弹射Ricochet)) > GetRemainingCharges(OriginalHook(虹吸弹GaussRound)))
                            return OriginalHook(弹射Ricochet);
                    }
                }

                return actionID;
            }
        }

        internal class MCH_Overdrive : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.MCH_Overdrive;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                MCHGauge? gauge = GetJobGauge<MCHGauge>();

                if (actionID is 车式浮空炮塔RookAutoturret or 后式自走人偶AutomatonQueen && gauge.IsRobotActive)
                    return OriginalHook(QueenOverdrive);

                return actionID;
            }
        }

        internal class MCH_HotShotDrillChainsawExcavator : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.MCH_HotShotDrillChainsawExcavator;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is 钻头Drill or HotShot or 空气锚AirAnchor or 回转飞锯ChainSaw)
                {
                    if (LevelChecked(Excavator) && HasEffect(Buffs.ExcavatorReady))
                        return CalcBestAction(actionID, Excavator, 回转飞锯ChainSaw, 空气锚AirAnchor, 钻头Drill);

                    if (LevelChecked(回转飞锯ChainSaw))
                        return CalcBestAction(actionID, 回转飞锯ChainSaw, 空气锚AirAnchor, 钻头Drill);

                    if (LevelChecked(空气锚AirAnchor))
                        return CalcBestAction(actionID, 空气锚AirAnchor, 钻头Drill);

                    if (LevelChecked(钻头Drill))
                        return CalcBestAction(actionID, 钻头Drill, HotShot);

                    return HotShot;
                }
                return actionID;
            }
        }

        internal class MCH_DismantleTactician : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.MCH_DismantleTactician;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is Dismantle
                    && (IsOnCooldown(Dismantle) || !LevelChecked(Dismantle))
                    && ActionReady(Tactician)
                    && !HasEffect(Buffs.Tactician))
                    return Tactician;

                return actionID;
            }
        }

        internal class All_PRanged_Dismantle : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.All_PRanged_Dismantle;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is Dismantle && TargetHasEffectAny(Debuffs.Dismantled) && IsOffCooldown(Dismantle))
                    return OriginalHook(11);

                return actionID;
            }
        }
    }
}