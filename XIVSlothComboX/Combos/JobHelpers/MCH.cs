using System;
using System.Collections.Generic;
using System.Linq;
using Dalamud.Game.ClientState.JobGauge.Types;
using XIVSlothComboX.Combos.JobHelpers.Enums;
using XIVSlothComboX.Combos.PvE;
using XIVSlothComboX.Core;
using XIVSlothComboX.CustomComboNS.Functions;
using XIVSlothComboX.Data;
using XIVSlothComboX.Services;

namespace XIVSlothComboX.Combos.JobHelpers
{
    internal class MCHOpenerLogic : PvE.MCH
    {
 

        private static uint OpenerLevel => 90;


        public static bool LevelChecked => CustomComboFunctions.LocalPlayer.Level >= OpenerLevel;

        public static bool CanOpener => HasCooldowns() && HasPrePullCooldowns() && LevelChecked;


        
        internal readonly static List<uint> 起手技能合集 = new();
        // internal readonly static List<uint> 使用技能合集 = new();

        public bool DoFullOpener(ref uint actionID, bool simpleMode)
        {
            if (!LevelChecked) return false;

            
            if (!CustomComboFunctions.InCombat())
            {

                if (CustomComboFunctions.GetCooldownRemainingTime(MCH.整备Reassemble) == 0 
                    && CustomComboFunctions.GetRemainingCharges(整备Reassemble)==2)
                {
                    actionID = 整备Reassemble;
                    return true;
                }

                if (CustomComboFunctions.HasEffect(Buffs.整备Reassembled))
                {
                    actionID = 空气锚AirAnchor;
                    return true;
                }

                初始起手技能();
            }
            else
            {
                if (下一个使用的技能(ref actionID))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            
            var 处理后的技能集合 = ActionWatching.CombatActions.Where(action
                => action is 热狙击弹HeatedCleanShot
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
                    or 后式自走人偶AutomatonQueen);

            if (处理后的技能集合.Count() < 起手技能合集.Count())
            {
                return true;
            }
            
            return false;
        }


        private void 初始起手技能()
        {
            起手技能合集.Clear();
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

            var 处理后的技能集合 = ActionWatching.CombatActions.Where(action
                => action is 热狙击弹HeatedCleanShot
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
                    or 后式自走人偶AutomatonQueen);

            if (处理后的技能集合.Count() < 起手技能合集.Count())
            {
                actionID = 起手技能合集[处理后的技能集合.Count()];
                return true;
            }
            return false;
        }

     
        
 

       
        public static bool HasCooldowns()
        {
            if (CustomComboFunctions.GetRemainingCharges(弹射Ricochet) < 3)
                return false;
            if (CustomComboFunctions.GetRemainingCharges(虹吸弹GaussRound) < 3)
                return false;
            if (!CustomComboFunctions.ActionReady(回转飞锯ChainSaw))
                return false;
            if (!CustomComboFunctions.ActionReady(野火Wildfire))
                return false;
            if (!CustomComboFunctions.ActionReady(枪管加热BarrelStabilizer))
                return false;

            return true;
        }

        public static bool HasPrePullCooldowns()
        {
            if (CustomComboFunctions.GetRemainingCharges(整备Reassemble) == 0 && Config.MCH_ST_RotationSelection == 2)
                return false;

            return true;
        }
    }
}