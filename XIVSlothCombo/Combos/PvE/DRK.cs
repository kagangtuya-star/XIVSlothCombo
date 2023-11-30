using Dalamud.Game.ClientState.JobGauge.Types;
using Dalamud.Game.ClientState.Statuses;
using XIVSlothCombo.Combos.PvE.Content;
using XIVSlothCombo.Combos.PvP;
using XIVSlothCombo.Core;
using XIVSlothCombo.CustomComboNS;

namespace XIVSlothCombo.Combos.PvE
{
    internal static class DRK
    {
        public const byte JobID = 32;

        public const uint HardSlash = 3617,
            Unleash = 3621,
            //吸收斩
            SyphonStrike = 3623,
            Souleater = 3632,
            SaltedEarth = 3639,
            AbyssalDrain = 3641,
            CarveAndSpit = 3643,
            //血乱
            血乱 = 7390,
            Quietus = 7391,
            //Bloodspiller
            血溅 = 7392,
            //暗黑波动
            FloodOfDarkness = 16466,
            //暗黑锋
            EdgeOfDarkness = 16467,
            StalwartSoul = 16468,
            FloodOfShadow = 16469,
            EdgeOfShadow = 16470,
            //弗雷
            LivingShadow = 16472,
            SaltAndDarkness = 25755,
            Oblation = 25754,
            Shadowbringer = 25757,
            Plunge = 3640,
            //BloodWeapon
            嗜血 = 3625,
            Unmend = 3624;

        public static class Buffs
        {
            public const ushort
                //嗜血
                BloodWeapon = 742,
                Darkside = 751,
                BlackestNight = 1178,
                //血乱
                Delirium = 1972,
                SaltedEarth = 749;
        }

        public static class Debuffs
        {
            public const ushort Placeholder = 1;
        }

        public static class Config
        {
            public const string DRK_KeepPlungeCharges = "DrkKeepPlungeCharges",
                DRK_MPManagement = "DrkMPManagement",
                DRK_VariantCure = "DRKVariantCure";
        }


        internal class DRK_SouleaterCombo : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.DRK_SouleaterCombo;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID == Souleater)
                {
                    var gauge = GetJobGauge<DRKGauge>();
                    var plungeChargesRemaining = PluginConfiguration.GetCustomIntValue(Config.DRK_KeepPlungeCharges);
                    var mpRemaining = PluginConfiguration.GetCustomIntValue(Config.DRK_MPManagement);

                    if (IsEnabled(CustomComboPreset.DRK_Variant_Cure) && IsEnabled(Variant.VariantCure) && PlayerHealthPercentageHp() <= GetOptionValue(Config.DRK_VariantCure))
                        return Variant.VariantCure;

                    if (IsEnabled(CustomComboPreset.DRK_RangedUptime) && LevelChecked(Unmend) && !InMeleeRange() && HasBattleTarget())
                        return Unmend;

                    if (InCombat())
                    {
                        // oGCDs
                        if (CanWeave(actionID))
                        {
                            Status? sustainedDamage = FindTargetEffect(Variant.Debuffs.SustainedDamage);
                            if (IsEnabled(CustomComboPreset.DRK_Variant_SpiritDart) && IsEnabled(Variant.VariantSpiritDart) && (sustainedDamage is null || sustainedDamage?.RemainingTime <= 3))
                                return Variant.VariantSpiritDart;

                            if (IsEnabled(CustomComboPreset.DRK_Variant_Ultimatum) && IsEnabled(Variant.VariantUltimatum) && IsOffCooldown(Variant.VariantUltimatum))
                                return Variant.VariantUltimatum;

                            //Mana Features
                            if (IsEnabled(CustomComboPreset.DRK_ManaOvercap))
                            {
                                if ((CombatEngageDuration().TotalSeconds < 7 && gauge.DarksideTimeRemaining == 0) || CombatEngageDuration().TotalSeconds >= 6)
                                {
                                    if (IsEnabled(CustomComboPreset.DRK_EoSPooling) && GetCooldownRemainingTime(血乱) >= 40 && (gauge.HasDarkArts || LocalPlayer.CurrentMp > (mpRemaining + 3000)) && LevelChecked(EdgeOfDarkness) && CanDelayedWeave(actionID))
                                        return OriginalHook(EdgeOfDarkness);


                                    if (gauge.DarksideTimeRemaining < 10 * 1000 && LocalPlayer.CurrentMp >= 3000)
                                    {
                                        if (LevelChecked(EdgeOfDarkness))
                                            return OriginalHook(EdgeOfDarkness);
                                        if (LevelChecked(FloodOfDarkness) && !LevelChecked(EdgeOfDarkness))
                                            return FloodOfDarkness;
                                    }


                                    if (LocalPlayer.CurrentMp >= 9800)
                                    {
                                        if (LevelChecked(EdgeOfDarkness))
                                            return OriginalHook(EdgeOfDarkness);
                                        if (LevelChecked(FloodOfDarkness) && !LevelChecked(EdgeOfDarkness))
                                            return FloodOfDarkness;
                                    }

                                    if (comboTime > 0 && lastComboMove == Unleash && LocalPlayer.CurrentMp >= 9200)
                                    {
                                        if (LevelChecked(EdgeOfDarkness))
                                            return OriginalHook(EdgeOfDarkness);
                                        if (LevelChecked(FloodOfDarkness) && !LevelChecked(EdgeOfDarkness))
                                            return FloodOfDarkness;
                                    }

                                    if (comboTime > 0 && lastComboMove == Unleash && GetBuffStacks(Buffs.BloodWeapon) > 0 && LocalPlayer.CurrentMp > 8600)
                                    {
                                        if (LevelChecked(EdgeOfDarkness))
                                            return OriginalHook(EdgeOfDarkness);
                                        if (LevelChecked(FloodOfDarkness) && !LevelChecked(EdgeOfDarkness))
                                            return FloodOfDarkness;
                                    }

                                    if (GetBuffStacks(Buffs.BloodWeapon) > 0 && LocalPlayer.CurrentMp >= 9200)
                                    {
                                        if (LevelChecked(EdgeOfDarkness))
                                            return OriginalHook(EdgeOfDarkness);
                                        if (LevelChecked(FloodOfDarkness) && !LevelChecked(EdgeOfDarkness))
                                            return FloodOfDarkness;
                                    }

                                }
                            }

                            //oGCD Features
                            if (gauge.DarksideTimeRemaining > 1)
                            {
                                if (IsEnabled(CustomComboPreset.DRK_MainComboCDs_Group) && IsEnabled(CustomComboPreset.DRK_LivingShadow) && gauge.Blood >= 50 && IsOffCooldown(LivingShadow) && LevelChecked(LivingShadow))
                                    return LivingShadow;

                                if (IsEnabled(CustomComboPreset.DRK_MainComboBuffs_Group))
                                {
                                    if (IsEnabled(CustomComboPreset.DRK_BloodWeapon) && IsOffCooldown(嗜血) && LevelChecked(嗜血))
                                        return 嗜血;

                                    if (IsEnabled(CustomComboPreset.DRK_Delirium) && IsOffCooldown(血乱) && LevelChecked(血乱) && (gauge.Blood <= 70 || GetBuffStacks(Buffs.BloodWeapon) < 0))
                                        return 血乱;
                                }

                                if (IsEnabled(CustomComboPreset.DRK_MainComboCDs_Group))
                                {

                                    if (IsEnabled(CustomComboPreset.DRK_SaltedEarth) && LevelChecked(SaltedEarth))
                                    {
                                        if ((IsOffCooldown(SaltedEarth) && !HasEffect(Buffs.SaltedEarth))
                                            || //Salted Earth
                                            (HasEffect(Buffs.SaltedEarth) && IsOffCooldown(SaltAndDarkness) && IsOnCooldown(SaltedEarth) && LevelChecked(SaltAndDarkness)) && GetBuffRemainingTime(Buffs.SaltedEarth) > 0 && GetBuffRemainingTime(Buffs.SaltedEarth) < 9) //Salt and Darkness
                                            return OriginalHook(SaltedEarth);
                                    }

                                    if (LevelChecked(Shadowbringer) && IsEnabled(CustomComboPreset.DRK_Shadowbringer))
                                    {
                                        if ((GetRemainingCharges(Shadowbringer) > 0 && IsNotEnabled(CustomComboPreset.DRK_ShadowbringerBurst)) || (IsEnabled(CustomComboPreset.DRK_ShadowbringerBurst) && GetRemainingCharges(Shadowbringer) > 0 && gauge.ShadowTimeRemaining > 1 && IsOnCooldown(血乱))) //burst feature
                                            return Shadowbringer;
                                    }

                                    if (IsEnabled(CustomComboPreset.DRK_CarveAndSpit) && IsOffCooldown(CarveAndSpit) && LevelChecked(CarveAndSpit))
                                        return CarveAndSpit;
                                    if (LevelChecked(Plunge) && IsEnabled(CustomComboPreset.DRK_Plunge) && GetRemainingCharges(Plunge) > plungeChargesRemaining)
                                    {
                                        if (IsNotEnabled(CustomComboPreset.DRK_MeleePlunge) || (IsEnabled(CustomComboPreset.DRK_MeleePlunge) && GetTargetDistance() <= 1 && ((GetMaxCharges(Plunge) > 1 && GetCooldownRemainingTime(血乱) >= 45) || GetMaxCharges(Plunge) == 1)))
                                            return Plunge;
                                    }
                                }
                            }
                        }

                        //Delirium Features
                        if (LevelChecked(血乱) && IsEnabled(CustomComboPreset.DRK_Bloodspiller) && IsEnabled(CustomComboPreset.DRK_MainComboCDs_Group))
                        {

                            //对团辅
                            if (GetCooldownRemainingTime(LivingShadow) >= 40)
                            {
                                if (RaidBuff.爆发期())
                                {
                                    if (gauge.Blood >= 50 || GetBuffStacks(Buffs.Delirium) > 0)
                                    {
                                        return 血溅;
                                    }
                                }
                            }
                            

                            // 不用对团辅直接放
                            if (GetCooldownRemainingTime(血乱) >= 40 && GetCooldownRemainingTime(LivingShadow) is >= 40 and <= 70)
                            {
                                if (gauge.Blood >= 50 || GetBuffStacks(Buffs.Delirium) > 0)
                                {
                                    // Dalamud.Logging.PluginLog.Error("1");
                                    return 血溅;
                                }
                            }


                            //防止暗血溢出
                            if (comboTime > 0 && lastComboMove == SyphonStrike && gauge.Blood >= 60 && GetBuffStacks(Buffs.BloodWeapon) > 0 && GetBuffStacks(Buffs.Delirium) > 0)
                            {
                                // Dalamud.Logging.PluginLog.Error("1");
                                return 血溅;
                            }


                            if (gauge.Blood == 100 && (GetCooldownRemainingTime(嗜血) is >= 0 and < 5 || GetCooldownRemainingTime(血乱) is >= 0 and < 5))
                            {
                                return 血溅;
                            }


                            //防止乱打血溅，导致弗雷延后
                            if (comboTime > 0 && lastComboMove == SyphonStrike && gauge.Blood >= 70 && GetCooldownRemainingTime(嗜血) is >= 0 and < 5 && GetCooldownRemainingTime(LivingShadow) is > 5)
                            {
                                // Dalamud.Logging.PluginLog.Error("3");
                                return 血溅;
                            }


                            //防止延后血乱
                            if (gauge.Blood >= 60 && GetCooldownRemainingTime(嗜血) is >= 0 and < 5 && GetCooldownRemainingTime(血乱) is >= 0 and < 5 && GetCooldownRemainingTime(LivingShadow) is > 7.5f)
                            {
                                // Dalamud.Logging.PluginLog.Error("4");
                                return 血溅;
                            }




                            //防止血溅没有打完
                            // if (GetBuffStacks(Buffs.Delirium) > 0 && GetBuffRemainingTime(Buffs.Delirium)> 0 && GetBuffRemainingTime(Buffs.Delirium) < (7.5f + GetCooldownRemainingTime(血溅)))
                            if (GetBuffStacks(Buffs.Delirium) > 0 && GetCooldownRemainingTime(血乱) + GetCooldownRemainingTime(血溅) <= 50.2f)
                            {
                                // Dalamud.Logging.PluginLog.Error($"{GetCooldownRemainingTime(血乱)}  {GetBuffRemainingTime(Buffs.Delirium)} -> {7.5f + GetCooldownRemainingTime(血溅)}");
                                return 血溅;
                            }

                            //Regular Delirium
                            if (GetBuffStacks(Buffs.Delirium) > 0 && IsNotEnabled(CustomComboPreset.DRK_DelayedBloodspiller))
                            {
                                // Dalamud.Logging.PluginLog.Error("6");
                                return 血溅;
                            }

                            //Delayed Delirium
                            if (IsEnabled(CustomComboPreset.DRK_DelayedBloodspiller) && GetBuffStacks(Buffs.Delirium) > 0 && IsOnCooldown(嗜血) && GetBuffStacks(Buffs.BloodWeapon) < 2)
                            {
                                // Dalamud.Logging.PluginLog.Error("7");
                                return 血溅;
                            }

                        }

                        // 1-2-3 combo
                        if (comboTime > 0)
                        {
                            if (lastComboMove == HardSlash && LevelChecked(SyphonStrike))
                                return SyphonStrike;
                            if (lastComboMove == SyphonStrike && LevelChecked(Souleater))
                            {
                                if (IsEnabled(CustomComboPreset.DRK_BloodGaugeOvercap) && LevelChecked(血溅) && gauge.Blood >= 90)
                                    return 血溅;
                                return Souleater;
                            }
                        }
                    }

                    return HardSlash;
                }

                return actionID;
            }
        }

        internal class DRK_StalwartSoulCombo : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.DRK_StalwartSoulCombo;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID == StalwartSoul)
                {
                    var gauge = GetJobGauge<DRKGauge>();

                    if (IsEnabled(CustomComboPreset.DRK_Variant_Cure) && IsEnabled(Variant.VariantCure) && PlayerHealthPercentageHp() <= GetOptionValue(Config.DRK_VariantCure))
                        return Variant.VariantCure;


                    if (CanWeave(actionID))
                    {
                        Status? sustainedDamage = FindTargetEffect(Variant.Debuffs.SustainedDamage);
                        if (IsEnabled(CustomComboPreset.DRK_Variant_SpiritDart) && IsEnabled(Variant.VariantSpiritDart) && (sustainedDamage is null || sustainedDamage?.RemainingTime <= 3))
                            return Variant.VariantSpiritDart;

                        if (IsEnabled(CustomComboPreset.DRK_Variant_Ultimatum) && IsEnabled(Variant.VariantUltimatum) && IsOffCooldown(Variant.VariantUltimatum))
                            return Variant.VariantUltimatum;

                        if (IsEnabled(CustomComboPreset.DRK_AoE_ManaOvercap) && LevelChecked(FloodOfDarkness) && (gauge.HasDarkArts || LocalPlayer.CurrentMp > 8500 || (gauge.DarksideTimeRemaining < 10 && LocalPlayer.CurrentMp >= 3000)))
                            return OriginalHook(FloodOfDarkness);
                        if (gauge.DarksideTimeRemaining > 1)
                        {
                            if (IsEnabled(CustomComboPreset.DRK_AoE_BloodWeapon) && IsOffCooldown(嗜血) && LevelChecked(嗜血))
                                return 嗜血;
                            if (IsEnabled(CustomComboPreset.DRK_AoE_Delirium) && IsOffCooldown(血乱) && LevelChecked(血乱))
                                return 血乱;
                            if (IsEnabled(CustomComboPreset.DRK_AoE_LivingShadow) && gauge.Blood >= 50 && IsOffCooldown(LivingShadow) && LevelChecked(LivingShadow))
                                return LivingShadow;
                            if (IsEnabled(CustomComboPreset.DRK_AoE_SaltedEarth) && LevelChecked(SaltedEarth))
                            {
                                if ((IsOffCooldown(SaltedEarth) && !HasEffect(Buffs.SaltedEarth))
                                    || //Salted Earth
                                    (HasEffect(Buffs.SaltedEarth) && IsOffCooldown(SaltAndDarkness) && IsOnCooldown(SaltedEarth) && LevelChecked(SaltAndDarkness))) //Salt and Darkness
                                    return OriginalHook(SaltedEarth);
                            }

                            if (IsEnabled(CustomComboPreset.DRK_AoE_AbyssalDrain) && LevelChecked(AbyssalDrain) && IsOffCooldown(AbyssalDrain) && PlayerHealthPercentageHp() <= 60)
                                return AbyssalDrain;
                            if (IsEnabled(CustomComboPreset.DRK_AoE_Shadowbringer) && LevelChecked(Shadowbringer) && GetRemainingCharges(Shadowbringer) > 0)
                                return Shadowbringer;
                        }
                    }

                    if (IsEnabled(CustomComboPreset.DRK_Delirium))
                    {
                        if (LevelChecked(血乱) && HasEffect(Buffs.Delirium) && gauge.DarksideTimeRemaining > 0)
                            return Quietus;
                    }

                    if (comboTime > 0)
                    {
                        if (lastComboMove == Unleash && LevelChecked(StalwartSoul))
                        {
                            if (IsEnabled(CustomComboPreset.DRK_Overcap) && gauge.Blood >= 90 && LevelChecked(Quietus))
                                return Quietus;
                            return StalwartSoul;
                        }
                    }

                    return Unleash;
                }

                return actionID;
            }
        }
        internal class DRK_oGCD : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.DRK_oGCD;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                var gauge = GetJobGauge<DRKGauge>();

                if (actionID == CarveAndSpit || actionID == AbyssalDrain)
                {
                    if (gauge.Blood >= 50 && IsOffCooldown(LivingShadow) && LevelChecked(LivingShadow))
                        return LivingShadow;
                    if (IsOffCooldown(SaltedEarth) && LevelChecked(SaltedEarth))
                        return SaltedEarth;
                    if (IsOffCooldown(CarveAndSpit) && LevelChecked(AbyssalDrain))
                        return actionID;
                    if (IsOffCooldown(SaltAndDarkness) && HasEffect(Buffs.SaltedEarth) && LevelChecked(SaltAndDarkness))
                        return SaltAndDarkness;
                    if (IsEnabled(CustomComboPreset.DRK_Shadowbringer_oGCD) && GetCooldownRemainingTime(Shadowbringer) < 60 && LevelChecked(Shadowbringer) && gauge.DarksideTimeRemaining > 0)
                        return Shadowbringer;
                }
                return actionID;
            }
        }
    }
}