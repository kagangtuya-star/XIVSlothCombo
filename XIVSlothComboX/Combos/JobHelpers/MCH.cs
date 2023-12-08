using System;
using System.Collections.Generic;
using System.Linq;
using XIVSlothComboX.Combos.JobHelpers.Enums;
using XIVSlothComboX.Combos.PvE;
using XIVSlothComboX.CustomComboNS.Functions;
using XIVSlothComboX.Data;
using XIVSlothComboX.Extensions;

namespace XIVSlothComboX.Combos.JobHelpers
{
    internal class MCHOpenerLogic : MCH
    {
        private static uint OpenerLevel => 90;
        public static OpenerState currentState = OpenerState.PrePull;
        public static bool LevelChecked => CustomComboFunctions.LocalPlayer.Level >= OpenerLevel;
        public static bool CanOpener => HasCooldowns() && LevelChecked;

        private DateTime lastCheck;
        internal readonly static List<uint> 起手技能合集 = new();
        internal static bool isInit = false;
        public bool DoFullOpener(ref uint actionID, bool simpleMode)
        {
            if (!LevelChecked) return false;

            if (currentState != OpenerState.PrePull)
            {
                if (CanOpener && !CustomComboFunctions.InCombat() && (DateTime.Now - lastCheck).TotalSeconds > 5)
                {
                    // Service.ChatGui.Print($"检查了");
                    currentState = OpenerState.PrePull;
                    lastCheck = DateTime.Now;
                }
            }

            switch (currentState)
            {
                case OpenerState.PrePull:
                {
                    if (CanOpener)
                    {
                        if (!CustomComboFunctions.InCombat())
                        {
                            if (CustomComboFunctions.GetCooldownRemainingTime(整备Reassemble) == 0)
                            {
                                actionID = 整备Reassemble;
                                return true;
                            }

                        }
                    }
                    else
                    {

                        初始起手技能();
                        currentState = OpenerState.InOpener;
                    }
                    break;
                }

                case OpenerState.InOpener:
                {
                    // if (!CustomComboFunctions.InCombat() && CustomComboFunctions.HasEffect(Buffs.整备Reassembled))
                    if (!CustomComboFunctions.InCombat())
                    {
                        actionID = 空气锚AirAnchor;
                        return true;
                    }

                    if (CustomComboFunctions.InCombat())
                    {

                        var 使用过的技能集合 = 技能过滤(ActionWatching.特殊起手Actions);

                        if (下一个使用的技能(ref actionID))
                        {

                            for (var i = 0; i < 使用过的技能集合.Count; i++)
                            {
                                if (使用过的技能集合[i] != 起手技能合集[i])
                                {
                                    currentState = OpenerState.FailedOpener;
                                    return false;
                                }
                            }

                            // if (使用过的技能集合.Count() >= 2)
                            // {
                            //     if (使用过的技能集合[1] != 空气锚AirAnchor)
                            //     {
                            //         currentState = OpenerState.FailedOpener;
                            //         return false;
                            //     }
                            // }


                            if (!actionID.ActionReady())
                            {
                                // Service.ChatGui.Print($"失败技能 {actionID} -> {使用过的技能集合.Count()}");
                            }

                            return true;
                        }


                        if (使用过的技能集合.Count() < 起手技能合集.Count())
                        {
                            return true;
                        }

                        if (使用过的技能集合.Count() >= 起手技能合集.Count())
                        {
                            currentState = OpenerState.OpenerFinished;
                            return false;
                        }

                        return true;
                    }
                    break;
                }
                case OpenerState.OpenerFinished:
                {
                    return false;
                }

                case OpenerState.FailedOpener:
                {
                    return false;
                }
            }
            return false;
        }
        private static void 初始起手技能()
        {
            // MCHOpenerLogic.isInit = true;
            // Service.ChatGui.Print($"初始起手技能");
            isInit = true;
            起手技能合集.Clear();
            起手技能合集.Add(整备Reassemble);
            起手技能合集.Add(空气锚AirAnchor);
            起手技能合集.Add(虹吸弹GaussRound);
            起手技能合集.Add(弹射Ricochet);
            起手技能合集.Add(钻头Drill);
            起手技能合集.Add(枪管加热BarrelStabilizer);
            起手技能合集.Add(热分裂弹HeatedSplitShot);
            起手技能合集.Add(热独头弹HeatedSlugshot);
            起手技能合集.Add(虹吸弹GaussRound);
            起手技能合集.Add(弹射Ricochet);
            起手技能合集.Add(热狙击弹HeatedCleanShot);
            起手技能合集.Add(整备Reassemble);

            起手技能合集.Add(野火Wildfire);
            起手技能合集.Add(回转飞锯ChainSaw);
            起手技能合集.Add(虹吸弹GaussRound);
            起手技能合集.Add(超荷Hypercharge);
            起手技能合集.Add(热冲击HeatBlast);
            起手技能合集.Add(弹射Ricochet);
            起手技能合集.Add(热冲击HeatBlast);
            起手技能合集.Add(虹吸弹GaussRound);
            起手技能合集.Add(热冲击HeatBlast);
            起手技能合集.Add(弹射Ricochet);

            起手技能合集.Add(热冲击HeatBlast);
            起手技能合集.Add(虹吸弹GaussRound);
            起手技能合集.Add(热冲击HeatBlast);
            起手技能合集.Add(弹射Ricochet);
            起手技能合集.Add(钻头Drill);
            起手技能合集.Add(虹吸弹GaussRound);
            起手技能合集.Add(弹射Ricochet);
            起手技能合集.Add(热分裂弹HeatedSplitShot);
            起手技能合集.Add(热独头弹HeatedSlugshot);
            起手技能合集.Add(热狙击弹HeatedCleanShot);


            起手技能合集.Add(热分裂弹HeatedSplitShot);
            起手技能合集.Add(热独头弹HeatedSlugshot);
            起手技能合集.Add(热狙击弹HeatedCleanShot);
            起手技能合集.Add(空气锚AirAnchor);
            起手技能合集.Add(后式自走人偶AutomatonQueen);
        }
        private bool 下一个使用的技能(ref uint actionID)
        {
            var 处理后的技能集合 = 技能过滤(ActionWatching.特殊起手Actions);

            if (处理后的技能集合.Count() < 起手技能合集.Count())
            {
                actionID = 起手技能合集[处理后的技能集合.Count()];
                return true;
            }
            return false;
        }
        private List<uint> 技能过滤(List<uint> 特殊起手Actions)
        {
            return 特殊起手Actions.Where(action => action is 热狙击弹HeatedCleanShot
                    or 热分裂弹HeatedSplitShot
                    or 热独头弹HeatedSlugshot
                    or 钻头Drill
                    or 空气锚AirAnchor
                    or 回转飞锯ChainSaw
                    or 枪管加热BarrelStabilizer
                    or 野火Wildfire
                    or 弹射Ricochet
                    or 虹吸弹GaussRound
                    or 整备Reassemble
                    or 超荷Hypercharge
                    or 热冲击HeatBlast
                    or 后式自走人偶AutomatonQueen)
                .ToList();
        }
        public static bool HasCooldowns()
        {
            if (CustomComboFunctions.GetRemainingCharges(弹射Ricochet) < 3)
                return false;
            if (CustomComboFunctions.GetRemainingCharges(虹吸弹GaussRound) < 3)
                return false;


            if (CustomComboFunctions.GetCooldownRemainingTime(整备Reassemble) != 0)
                return false;
            if (!CustomComboFunctions.ActionReady(回转飞锯ChainSaw))
                return false;
            if (!CustomComboFunctions.ActionReady(空气锚AirAnchor))
                return false;
            if (!CustomComboFunctions.ActionReady(钻头Drill))
                return false;


            if (!CustomComboFunctions.ActionReady(野火Wildfire))
                return false;
            if (!CustomComboFunctions.ActionReady(枪管加热BarrelStabilizer))
                return false;

            return true;
        }


    }
}
