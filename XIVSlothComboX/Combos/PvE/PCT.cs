using System;
using Dalamud.Game.ClientState.JobGauge.Types;
using FFXIVClientStructs.FFXIV.Client.Game.Gauge;
using XIVSlothComboX.CustomComboNS;
using XIVSlothComboX.CustomComboNS.Functions;
using XIVSlothComboX.Data;
using XIVSlothComboX.Extensions;
using XIVSlothComboX.Services;

namespace XIVSlothComboX.Combos.PvE;

/// <summary>
/// fragile
/// https://github.com/zfxsquare 
/// </summary>
internal class PCT
{
    public const byte JobID = 42;

    public const uint
        短1 = 34650,
        短2 = 34651,
        短3 = 34652,
        长1 = 34653,
        长2 = 34654,
        长3 = 34655,
        AOE短1 = 34656,
        AOE短2 = 34657,
        AOE短3 = 34658,
        AOE长1 = 34659,
        AOE长2 = 34660,
        AOE长3 = 34661,
        白豆 = 34662,
        黑豆 = 34663,
        反转 = 34683,
        生物画1 = 34664,
        生物画2 = 34665,
        生物画3 = 34666,
        生物画4 = 34667,
        生物1具现 = 34670,
        生物2具现 = 34671,
        生物3具现 = 34672,
        生物4具现 = 34673,
        莫古 = 34676,
        蔬菜 = 34677,
        武器画 = 34668,
        武器具现 = 34674,
        锤1 = 34678,
        锤2 = 34679,
        锤3 = 34680,
        大招 = 34681,
        彩虹 = 34688,
        风景画 = 34669,
        黑魔纹 = 34675,
        
        BlizzardinCyan = 34653,
        BlizzardIIinCyan = 34659,
        ClawMotif = 34666,
        ClawedMuse = 34672,
        CometinBlack = 34663,
        CreatureMotif = 34689,
        FireInRed = 34650,
        FireIIinRed = 34656,
        HammerStamp = 34678,
        HolyInWhite = 34662,
        LandscapeMotif = 34691,
        LivingMuse = 35347,
        MawMotif = 34667,
        MogoftheAges = 34676,
        PomMotif = 34664,
        PomMuse = 34670,
        RainbowDrip = 34688,
        RetributionoftheMadeen = 34677,
        ScenicMuse = 35349,
        Smudge = 34684,
        StarPrism = 34681,
        SteelMuse = 35348,
        SubtractivePalette = 34683,
        ThunderIIinMagenta = 34661,
        ThunderinMagenta = 34655,
        WaterinBlue = 34652,
        WeaponMotif = 34690,
        WingMotif = 34665;

    public static class Buffs
    {
        public const ushort
            长Buff = 3674,
            连击2buff = 3675,
            连击3buff = 3676,
            黑豆buff = 3691,
            锤子预备 = 3680,
            彩虹预备 = 3679,
            免费反转 = 3690,
            大招预备 = 3681,
            加速 = 3688,
            团辅 = 3685,
            
            SubtractivePalette = 3674,
            RainbowBright = 3679,
            HammerTime = 3680,
            MonochromeTones = 3691,
            StarryMuse = 3685,
            Hyperphantasia = 3688,
            Inspiration = 3689,
            SubtractiveSpectrum = 3690,
            Starstruck = 3681;
        
    }

    public static class Debuffs
    {
    }

    public static class Config
    {
        public static UserInt
            CombinedAetherhueChoices = new("CombinedAetherhueChoices");

        public static UserBool
            CombinedMotifsMog = new("CombinedMotifsMog"),
            CombinedMotifsMadeen = new("CombinedMotifsMadeen"),
            CombinedMotifsWeapon = new("CombinedMotifsWeapon");
    }

    
    internal class PCT_ST_Custom : CustomCombo
    {
        protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.PCT_Advanced_CustomMode;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID is All.Sleep)
            {
                if (CustomTimelineIsEnable())
                {
                    var seconds = CombatEngageDuration().TotalSeconds;

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

    
    internal class PCT_ST_EasyMode : CustomCombo
    {
        protected internal override CustomComboPreset Preset => CustomComboPreset.PCT_ST_EasyMode;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            var gauge = new TmpPCTGauge();
            
            if (actionID is 短1)
            {
                if (gauge.风景画)
                {
                    if (CanUse(黑魔纹))
                    {
                        return 黑魔纹;
                    }
                }
                if (!gauge.风景画)
                {
                    if (!HasEffect(Buffs.团辅))
                    {
                        return 风景画;
                    }
                    
                }
                if (HasEffect(Buffs.大招预备))
                {
                    return 大招;
                }

                if (HasEffect(Buffs.彩虹预备))
                {
                    return 彩虹;
                }

                if (HasEffect(Buffs.加速))
                {
                    if ((gauge.能量>=50||HasEffect(Buffs.免费反转))&&LevelChecked(反转)&&!HasEffect(Buffs.长Buff))
                    {
                        return 反转;
                    }
                    if (HasEffect(Buffs.长Buff))
                    {
                        if (HasEffect(Buffs.连击3buff))
                        {
                            return 长3;
                        }
                        if (HasEffect(Buffs.连击2buff))
                        {
                            return 长2;
                        }
                        return 长1;
                    }

                    return 短1;
                }
                if (gauge.蔬菜准备&& CanUse(蔬菜))
                {
                    return 蔬菜;
                }
                if (gauge.莫古准备&& CanUse(莫古))
                {
                    return 莫古;
                }

                if (HasEffect(Buffs.锤子预备))
                {
                    return 锤1.OriginalHook();
                }

                if (gauge.武器画)
                {
                    if (CanUse(武器具现))
                    {
                        return 武器具现;
                    }
                    
                }

                if (!gauge.武器画)
                {
                    if (CanUse(武器画))
                    {
                        return 武器画;
                    }
                }
                if (gauge.生物画)
                {
                    if (CanUse(生物4具现))
                    {
                        return 生物4具现;
                    }
                    if (CanUse(生物3具现))
                    {
                        return 生物3具现;
                    }
                    if (CanUse(生物2具现))
                    {
                        return 生物2具现;
                    }
                    if (CanUse(生物1具现))
                    {
                        return 生物1具现;
                    }
                }
                if (!gauge.生物画)
                {
                    if (CanUse(生物画4))
                    {
                        return 生物画4;
                    }
                    if (CanUse(生物画3))
                    {
                        return 生物画3;
                    }
                    if (CanUse(生物画2))
                    {
                        return 生物画2;
                    }
                    if (CanUse(生物画1))
                    {
                        return 生物画1;
                    }
                }

                
                if (HasEffect(Buffs.黑豆buff))
                {
                    return 黑豆;
                }
                if ((gauge.能量>=50||HasEffect(Buffs.免费反转))&&LevelChecked(反转)&&!HasEffect(Buffs.长Buff))
                {
                    return 反转;
                }

                if (HasEffect(Buffs.长Buff))
                {
                    if (HasEffect(Buffs.连击3buff))
                    {
                        return 长3;
                    }
                    if (HasEffect(Buffs.连击2buff))
                    {
                        return 长2;
                    }
                    return 长1;
                }
            }
            return actionID;
        }
    }
    
    internal class PCT_AOE_EasyMode : CustomCombo
    {
        protected internal override CustomComboPreset Preset => CustomComboPreset.PCT_AOE_EasyMode;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            var gauge = new TmpPCTGauge();
            
            if (actionID is AOE短1)
            {
                if (gauge.风景画)
                {
                    if (CanUse(黑魔纹))
                    {
                        return 黑魔纹;
                    }
                }
                if (!gauge.风景画)
                {
                    if (!HasEffect(Buffs.加速))
                    {
                        return 风景画;
                    }
                    
                }
                if (HasEffect(Buffs.大招预备))
                {
                    return 大招;
                }

                if (HasEffect(Buffs.彩虹预备))
                {
                    return 彩虹;
                }
                if (HasEffect(Buffs.加速))
                {
                    if ((gauge.能量>=50||HasEffect(Buffs.免费反转))&&LevelChecked(反转)&&!HasEffect(Buffs.长Buff))
                    {
                        return 反转;
                    }
                    if (HasEffect(Buffs.长Buff))
                    {
                        if (HasEffect(Buffs.连击3buff))
                        {
                            return AOE长3;
                        }
                        if (HasEffect(Buffs.连击2buff))
                        {
                            return AOE长2;
                        }
                        return AOE长1;
                    }

                    return AOE短1;
                }
                if (gauge.蔬菜准备&& CanUse(蔬菜))
                {
                    return 蔬菜;
                }
                if (gauge.莫古准备&& CanUse(莫古))
                {
                    return 莫古;
                }

                if (HasEffect(Buffs.锤子预备))
                {
                    return 锤1.OriginalHook();
                }

                if (gauge.武器画)
                {
                    if (CanUse(武器具现))
                    {
                        return 武器具现;
                    }
                    
                }

                if (!gauge.武器画)
                {
                    if (CanUse(武器画))
                    {
                        return 武器画;
                    }
                }
                if (gauge.生物画)
                {
                    if (CanUse(生物4具现))
                    {
                        return 生物4具现;
                    }
                    if (CanUse(生物3具现))
                    {
                        return 生物3具现;
                    }
                    if (CanUse(生物2具现))
                    {
                        return 生物2具现;
                    }
                    if (CanUse(生物1具现))
                    {
                        return 生物1具现;
                    }
                }
                if (!gauge.生物画)
                {
                    if (CanUse(生物画4))
                    {
                        return 生物画4;
                    }
                    if (CanUse(生物画3))
                    {
                        return 生物画3;
                    }

                    // if(生物画2.ActionReady())
                    
                    if (CanUse(生物画2))
                    {
                        return 生物画2;
                    }
                    if (CanUse(生物画1))
                    {
                        return 生物画1;
                    }
                }

                
                if (HasEffect(Buffs.黑豆buff))
                {
                    return 黑豆;
                }
                if ((gauge.能量>=50||HasEffect(Buffs.免费反转))&&LevelChecked(反转)&&!HasEffect(Buffs.长Buff))
                {
                    return 反转;
                }

                if (HasEffect(Buffs.长Buff))
                {
                    if (HasEffect(Buffs.连击3buff))
                    {
                        return AOE长3;
                    }
                    if (HasEffect(Buffs.连击2buff))
                    {
                        return AOE长2;
                    }
                    return AOE长1;
                }
            }
            return actionID;
        }
    }
    
    internal class PCT_ST_SimpleMode : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.PCT_ST_SimpleMode;

            protected override uint Invoke(uint actionID, uint lastComboActionID, float comboTime, byte level)
            {
                if (actionID is FireInRed)
                {
                    var gauge = GetJobGauge<PCTGauge>();
                    bool canWeave = HasEffect(Buffs.SubtractivePalette) ? CanSpellWeave(OriginalHook(BlizzardinCyan)) : CanSpellWeave(OriginalHook(FireInRed));

                    if (HasEffect(Buffs.Starstruck))
                        return OriginalHook(StarPrism);

                    if (HasEffect(Buffs.RainbowBright))
                        return OriginalHook(RainbowDrip);

                    if (IsMoving)
                    {
                        if (gauge.Paint > 0)
                        {
                            if (HasEffect(Buffs.MonochromeTones))
                                return OriginalHook(CometinBlack);

                            return OriginalHook(HolyInWhite);
                        }
                    }


                    if (HasEffect(Buffs.StarryMuse))
                    {
                        if (HasEffect(Buffs.SubtractiveSpectrum) && !HasEffect(Buffs.SubtractivePalette) && canWeave)
                            return OriginalHook(SubtractivePalette);

                        if (MogoftheAges.LevelChecked() && (gauge.MooglePortraitReady || gauge.MadeenPortraitReady) && IsOffCooldown(OriginalHook(MogoftheAges)))
                            return OriginalHook(MogoftheAges);

                        if (!HasEffect(Buffs.HammerTime) && gauge.WeaponMotifDrawn && HasCharges(OriginalHook(SteelMuse)) && GetBuffRemainingTime(Buffs.StarryMuse) >= 15f)
                            return OriginalHook(SteelMuse);

                        if (gauge.CreatureMotifDrawn && HasCharges(OriginalHook(LivingMuse)) && canWeave)
                            return OriginalHook(LivingMuse);

                        if (HasEffect(Buffs.HammerTime))
                            return OriginalHook(HammerStamp);

                        if (HasEffect(Buffs.SubtractivePalette))
                            return OriginalHook(BlizzardinCyan);

                        return actionID;
                    }

                    if (gauge.PalleteGauge >= 50 && !HasEffect(Buffs.SubtractivePalette) && canWeave)
                        return OriginalHook(SubtractivePalette);

                    if (HasEffect(Buffs.HammerTime) && !canWeave)
                        return OriginalHook(HammerStamp);

                    if (InCombat())
                    {
                        if (gauge.LandscapeMotifDrawn && gauge.WeaponMotifDrawn && (gauge.MooglePortraitReady || gauge.MadeenPortraitReady) && IsOffCooldown(MogoftheAges) && IsOffCooldown(ScenicMuse) && canWeave)
                            return OriginalHook(ScenicMuse);

                        if (MogoftheAges.LevelChecked() && (gauge.MooglePortraitReady || gauge.MadeenPortraitReady) && IsOffCooldown(OriginalHook(MogoftheAges)) && (GetCooldown(LivingMuse).CooldownRemaining < GetCooldown(ScenicMuse).CooldownRemaining || !ScenicMuse.LevelChecked()) && canWeave)
                            return OriginalHook(MogoftheAges);

                        if (!HasEffect(Buffs.HammerTime) && gauge.WeaponMotifDrawn && HasCharges(OriginalHook(SteelMuse)) && (GetCooldown(SteelMuse).CooldownRemaining < GetCooldown(ScenicMuse).CooldownRemaining || GetRemainingCharges(SteelMuse) == GetMaxCharges(SteelMuse) || !ScenicMuse.LevelChecked()) && canWeave)
                            return OriginalHook(SteelMuse);

                        if (gauge.CreatureMotifDrawn && (!(gauge.MooglePortraitReady || gauge.MadeenPortraitReady) || GetCooldown(LivingMuse).CooldownRemaining > GetCooldown(ScenicMuse).CooldownRemaining || GetRemainingCharges(LivingMuse) == GetMaxCharges(LivingMuse) || !ScenicMuse.LevelChecked()) && HasCharges(OriginalHook(LivingMuse)) && canWeave)
                            return OriginalHook(LivingMuse);

                        if (LandscapeMotif.LevelChecked() && !gauge.LandscapeMotifDrawn && GetCooldownRemainingTime(ScenicMuse) <= GetActionCastTime(OriginalHook(LandscapeMotif)))
                            return OriginalHook(LandscapeMotif);

                        if (CreatureMotif.LevelChecked() && !gauge.CreatureMotifDrawn && (HasCharges(LivingMuse) || GetCooldownChargeRemainingTime(LivingMuse) <= GetActionCastTime(OriginalHook(CreatureMotif))))
                            return OriginalHook(CreatureMotif);

                        if (WeaponMotif.LevelChecked() && !HasEffect(Buffs.HammerTime) && !gauge.WeaponMotifDrawn && (HasCharges(SteelMuse) || GetCooldownChargeRemainingTime(SteelMuse) <= GetActionCastTime(OriginalHook(WeaponMotif))))
                            return OriginalHook(WeaponMotif);

                    }
                    if (gauge.Paint > 0 && HasEffect(Buffs.MonochromeTones))
                        return OriginalHook(CometinBlack);

                    if (HasEffect(Buffs.SubtractivePalette))
                        return OriginalHook(BlizzardinCyan);

                }
                return actionID;
            }
        }

        internal class PCT_AoE_SimpleMode : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.PCT_AoE_SimpleMode;

            protected override uint Invoke(uint actionID, uint lastComboActionID, float comboTime, byte level)
            {
                if (actionID is FireIIinRed)
                {
                    var gauge = GetJobGauge<PCTGauge>();
                    bool canWeave = HasEffect(Buffs.SubtractivePalette) ? CanSpellWeave(OriginalHook(BlizzardinCyan)) : CanSpellWeave(OriginalHook(FireInRed));

                    if (HasEffect(Buffs.Starstruck))
                        return OriginalHook(StarPrism);

                    if (HasEffect(Buffs.RainbowBright))
                        return OriginalHook(RainbowDrip);

                    if (IsMoving)
                    {
                        if (gauge.Paint > 0)
                        {
                            if (HasEffect(Buffs.MonochromeTones))
                                return OriginalHook(CometinBlack);

                            return OriginalHook(HolyInWhite);
                        }
                    }

                    if (HasEffect(Buffs.StarryMuse))
                    {
                        if (HasEffect(Buffs.SubtractiveSpectrum) && !HasEffect(Buffs.SubtractivePalette) && canWeave)
                            return OriginalHook(SubtractivePalette);

                        if (MogoftheAges.LevelChecked() && (gauge.MooglePortraitReady || gauge.MadeenPortraitReady) && IsOffCooldown(OriginalHook(MogoftheAges)))
                            return OriginalHook(MogoftheAges);

                        if (!HasEffect(Buffs.HammerTime) && gauge.WeaponMotifDrawn && HasCharges(OriginalHook(SteelMuse)) && GetBuffRemainingTime(Buffs.StarryMuse) >= 15f)
                            return OriginalHook(SteelMuse);

                        if (gauge.CreatureMotifDrawn && HasCharges(OriginalHook(LivingMuse)) && canWeave)
                            return OriginalHook(LivingMuse);

                        if (HasEffect(Buffs.HammerTime))
                            return OriginalHook(HammerStamp);

                        if (HasEffect(Buffs.SubtractivePalette))
                            return OriginalHook(BlizzardIIinCyan);

                        return actionID;
                    }

                    if (gauge.PalleteGauge >= 50 && !HasEffect(Buffs.SubtractivePalette) && canWeave)
                        return OriginalHook(SubtractivePalette);

                    if (HasEffect(Buffs.HammerTime) && !canWeave)
                        return OriginalHook(HammerStamp);

                    if (InCombat())
                    {
                        if (gauge.LandscapeMotifDrawn && gauge.WeaponMotifDrawn && (gauge.MooglePortraitReady || gauge.MadeenPortraitReady) && IsOffCooldown(MogoftheAges) && IsOffCooldown(ScenicMuse) && canWeave)
                            return OriginalHook(ScenicMuse);

                        if (MogoftheAges.LevelChecked() && (gauge.MooglePortraitReady || gauge.MadeenPortraitReady) && IsOffCooldown(OriginalHook(MogoftheAges)) && (GetCooldown(MogoftheAges).CooldownRemaining < GetCooldown(ScenicMuse).CooldownRemaining || !ScenicMuse.LevelChecked()) && canWeave)
                            return OriginalHook(MogoftheAges);

                        if (!HasEffect(Buffs.HammerTime) && gauge.WeaponMotifDrawn && HasCharges(OriginalHook(SteelMuse)) && (GetCooldown(SteelMuse).CooldownRemaining < GetCooldown(ScenicMuse).CooldownRemaining || GetRemainingCharges(SteelMuse) == GetMaxCharges(SteelMuse) || !ScenicMuse.LevelChecked()) && canWeave)
                            return OriginalHook(SteelMuse);

                        if (gauge.CreatureMotifDrawn && (!(gauge.MooglePortraitReady || gauge.MadeenPortraitReady) || GetCooldown(LivingMuse).CooldownRemaining > GetCooldown(ScenicMuse).CooldownRemaining || GetRemainingCharges(LivingMuse) == GetMaxCharges(LivingMuse) || !ScenicMuse.LevelChecked()) && HasCharges(OriginalHook(LivingMuse)) && canWeave)
                            return OriginalHook(LivingMuse);

                        if (LandscapeMotif.LevelChecked() && !gauge.LandscapeMotifDrawn && GetCooldownRemainingTime(ScenicMuse) <= GetActionCastTime(OriginalHook(LandscapeMotif)))
                            return OriginalHook(LandscapeMotif);

                        if (CreatureMotif.LevelChecked() && !gauge.CreatureMotifDrawn && (HasCharges(LivingMuse) || GetCooldownChargeRemainingTime(LivingMuse) <= GetActionCastTime(OriginalHook(CreatureMotif))))
                            return OriginalHook(CreatureMotif);

                        if (WeaponMotif.LevelChecked() && !HasEffect(Buffs.HammerTime) && !gauge.WeaponMotifDrawn && (HasCharges(SteelMuse) || GetCooldownChargeRemainingTime(SteelMuse) <= GetActionCastTime(OriginalHook(WeaponMotif))))
                            return OriginalHook(WeaponMotif);
                    }

                    if (gauge.Paint > 0 && HasEffect(Buffs.MonochromeTones))
                        return OriginalHook(CometinBlack);

                    if (gauge.Paint > 0)
                        return OriginalHook(HolyInWhite);

                    if (HasEffect(Buffs.SubtractivePalette))
                        return OriginalHook(BlizzardIIinCyan);

                }
                return actionID;
            }
        }

        internal class CombinedAetherhues : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.CombinedAetherhues;

            protected override uint Invoke(uint actionID, uint lastComboActionID, float comboTime, byte level)
            {
                int choice = Config.CombinedAetherhueChoices;

                if (actionID == FireInRed && choice is 0 or 1)
                {
                    if (HasEffect(Buffs.SubtractivePalette))
                        return OriginalHook(BlizzardinCyan);
                }

                if (actionID == FireIIinRed && choice is 0 or 2)
                {
                    if (HasEffect(Buffs.SubtractivePalette))
                        return OriginalHook(BlizzardIIinCyan);
                }

                return actionID;
            }
        }

        internal class CombinedMotifs : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.CombinedMotifs;

            protected override uint Invoke(uint actionID, uint lastComboActionID, float comboTime, byte level)
            {
                var gauge = GetJobGauge<PCTGauge>();

                if (actionID == CreatureMotif)
                {
                    if ((Config.CombinedMotifsMog && gauge.MooglePortraitReady) || (Config.CombinedMotifsMadeen && gauge.MadeenPortraitReady) && IsOffCooldown(OriginalHook(MogoftheAges)))
                        return OriginalHook(MogoftheAges);

                    if (gauge.CreatureMotifDrawn)
                        return OriginalHook(LivingMuse);
                }

                if (actionID == WeaponMotif)
                {
                    if (Config.CombinedMotifsWeapon && HasEffect(Buffs.HammerTime))
                        return OriginalHook(HammerStamp);

                    if (gauge.WeaponMotifDrawn)
                        return OriginalHook(SteelMuse);
                }

                if (actionID == LandscapeMotif)
                {
                    if (gauge.LandscapeMotifDrawn)
                        return OriginalHook(ScenicMuse);
                }

                return actionID;
            }
        }

        internal class CombinedPaint : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.CombinedPaint;

            protected override uint Invoke(uint actionID, uint lastComboActionID, float comboTime, byte level)
            {
                if (actionID == HolyInWhite)
                {
                    if (HasEffect(Buffs.MonochromeTones))
                        return CometinBlack;
                }

                return actionID;
            }
        }
}