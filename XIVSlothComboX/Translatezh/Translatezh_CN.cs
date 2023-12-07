using System.Collections.Generic;

namespace XIVSlothComboX;

public class Translatezh_CN
{
    public static Dictionary<string, string> db = new()
    { 
 
        #region PvE Combos
        #region Misc
        {"Any" , "等待翻译"  },
        {"This should not be displayed. This always returns true when used with IsEnabled." , "等待翻译"  },
        {"Disabled" , "等待翻译"  },
        {"This should not be used." , "等待翻译"  },
        #endregion
        #region GLOBAL FEATURES
        #region Global Tank Features
        #endregion
        #region Global Healer Features
        #endregion
        #region Global Magical Ranged Features
        #endregion
        #region Global Melee Features
        #endregion
        #region Global Ranged Physical Features
        #endregion
        #endregion
        #region ASTROLOGIAN
        #region DPS
        {"Adds Combust to the DPS feature if it's not present on current target, or is about to expire." , "将烧灼（续DoT）加入输出循环。"  },
        {"Card Play Weave Option" , "等待翻译"  },
        {"Weaves your card (best used with Quick Target Cards)" , "[AI]编织您的卡片（最好与快速目标卡一起使用）"  },
        {"Redraw Option" , "等待翻译"  },
        {"Weaves Redraw if you pull a card with a seal you already have and you can use Redraw." , "[ai]如果你抽到一张已经拥有印记的卡片，你可以使用重抽功能。"  },
        #endregion
        #region Healing
        {"Essential Dignity Option" , "[AI]基本尊严选项"  },
        {"Celestial Intersection Option" , "[AI]天体交会选项"  },
        {"Aspected Benefic Option" , "[AI]相位益处选项"  },
        {"Celestial Opposition Option" , "[AI]天体对立选项"  },
        {"Lazy Lady Option" , "[AI]懒散的贵妇选项"  },
        {"Adds Lady of Crowns, if the card is drawn" , "当抽取到王冠之贵妇时，自动加入治疗连击"  },
        {"Horoscope Option" , "[AI]星盘选项"  },
        #endregion
        #region Utility
        #endregion
        #region Cards
        {"Redraw on Draw" , "[AI抽卡后重新抽取"  },
        #endregion
        #endregion
        #region BLACK MAGE
        #region Advanced ST
        {"Advanced Mode - Single Target" , "高级模式 - 单目标"  },
        {"Replaces Fire with a full one-button single target rotation.\nThese features are ideal if you want to customize the rotation." , "用一个完整的单目标循环替代火焰。\n如果您想自定义循环，这些功能非常理想。"  },
        {"Thunder I/III Option" , "雷霆I/III选项"  },
        {"Adds Thunder I/Thunder III when the debuff isn't present or is expiring." , "当无法触发雷霆debuff或debuff即将结束时，添加雷霆I/III。"  },
        {"Thundercloud Spender Option" , "雷霆云消耗选项"  },
        {"Spends Thundercloud as soon as possible rather than waiting until Thunder is expiring." , "尽快使用雷霆云而不是等待雷霆即将结束。"  },
        {"Umbral Soul Option" , "幽暗魂选项"  },
        {"Uses Transpose/Umbral Soul when no target is selected." , "当没有目标选择时使用转位法/幽暗魂。"  },
        {"Movement Options" , "移动选项"  },
        {"Choose options to be used during movement." , "选择在移动时使用的选项。"  },
        {"Triplecast/Swiftcast Option" , "三连咏唱/迅连咏唱选项"  },
        {"Adds Triplecast/Swiftcast to the rotation." , "将三连咏唱/迅连咏唱添加到循环中。"  },
        {"Pool Triplecast Option" , "保留三连咏唱选项"  },
        {"Keep one Triplecast charge for movement." , "保留一次三连咏唱的充能以供移动使用。"  },
        {"Cooldown Options" , "冷却选项"  },
        {"Select which cooldowns to add to the rotation." , "选择要添加到循环中的冷却技能。"  },
        {"Opener Option" , "开场选项"  },
        {"Adds the Lv.90 opener.\nWill default to the Standard opener when nothing is selected." , "添加90级开场技能。\n如果没有选择任何内容，将默认使用标准开场技能。"  },
        {"Rotation Option" , "循环选项"  },
        {"Choose which rotation to use.\nWill default to the Standard rotation when nothing is selected." , "选择要使用的循环。\n如果没有选择任何内容，将默认使用标准循环。"  },
        #endregion
        #region Advanced AoE
        {"Advanced Mode - AoE" , "[AI]高级模式 - AoE"  },
        {"Replaces Blizzard II with a full one-button AoE rotation.\nThese features are ideal if you want to customize the rotation." , "[AI]用一个完整的单键AoE循环替代暴风雪II。\n如果您想自定义循环，这些功能非常理想。"  },
        {"Thunder Uptime Option" , "[AI]雷霆持续时间选项"  },
        {"Adds Thunder II/Thunder IV during Umbral Ice." , "[AI]在幽暗冰期间添加雷霆II/雷霆IV。"  },
        {"Uptime in Astral Fire" , "[AI]天体之火期间的持续时间"  },
        {"Maintains uptime during Astral Fire." , "[AI]在天体之火期间保持持续时间。"  },
        {"Foul Option" , "[AI]秽浊选项"  },
        {"Adds Foul when available during Astral Fire." , "[AI]在天体之火期间可用时添加秽浊。"  },
        {"Use Transpose/Umbral Soul when no target is selected." , "[AI]当没有目标选择时使用转位法/幽暗魂。"  },
        #endregion
        #region Variant
        #endregion
        #region Miscellaneous
        {"Aetherial Manipulation Feature" , "以太步功能"  },
        {"Replaces Aetherial Manipulation with Between the Lines when you are out of active Ley Lines and standing still." , "[AI]当你没有激活的灵脉并且静止不动时，用Between the Lines替代以太步。"  },
        #endregion
        #endregion
        #region BLUE MAGE
        {"Turns Final Sting into the buff combo of: Moon Flute, Tingle, Whistle, Final Sting. Will use any primals off cooldown before casting Final Sting." , "将最终极针变成月之笛、哔哩哔哩、口笛、最终极针的BUFF组合。在施放终极极针之前，将使用任何冷却完毕的蛮神魔法。"  },
        {"Puts Devour, Off-Guard, Lucid Dreaming, and Bad Breath into one button when under Tank Mimicry." , "在【以太复制：防护】下，将捕食、破防、醒梦、臭气合并为一个按钮。"  },
        #endregion
        #region BARD
        {"Adds every single target ability to one button,\nIf there are DoTs on target, Simple Bard will try to maintain their uptime." , "将所有单目标技能添加到一个按钮中，\n如果目标身上有DoT效果，Simple Bard将尝试保持它们的持续时间。"  },
        {"Adds enemy health checking on mobs for buffs, DoTs and Songs.\nThey will not be reapplied if less than specified." , "对敌方生命值进行检查，为增益效果、DoT效果和歌曲效果。\n如果生命值低于指定值，则不会重新应用。"  },
        {"Enable the snapshotting of DoTs, within the remaining time of Raging Strikes below:" , "在猛者强击持续时间低于设定值时，自动替换插入dot技能。"  },
        #endregion
        #region DANCER
        #region Simple Dancer (Double Targets)
        {"Single button, double targets. Includes songs, flourishes and overprotections.\nConflicts with all other non-simple toggles, except 'Dance Step Combo'." , "无脑一键连击。"  },
        {"Includes Standard Step (and all steps) in the rotation." , "循环加入标准舞步"  },
        {"Includes Technical Step, all dance steps and Technical Finish in the rotation." , "循环加入技巧舞步"  },
        {"Includes Flourish in the rotation." , "将百花争艳加入循环。"  },
        #endregion
        #region Single Target Multibutton
        {"Single Target Multibutton Feature" , "单目标多按钮"  },
        {"Single target combo with Fan Dances and Esprit use." , "使用扇舞与伶俐值的单目标连击"  },
        {"ST Esprit Overcap Option" , "单体目标伶俐防溢出设置"  },
        {"Adds Saber Dance above the set Esprit threshold." , "伶俐超过下面设置的数值后，将剑舞加入循环"  },
        {"Fan Dance Overcap Protection Option" , "幻扇溢出保护"  },
        {"Adds Fan Dance 1 when Fourfold Feathers are full." , "当量谱上幻扇满了后，将扇舞序加入循环"  },
        {"Fan Dance Option" , "扇舞选项"  },
        {"Adds Fan Dance 3/4 when available." , "当扇舞急·扇舞终可用时，加入循环。"  },
        #endregion
        #region AoE Multibutton
        {"AoE Multibutton Feature" , "AOE多按钮"  },
        {"AoE combo with Fan Dances and Esprit use." , "使用扇舞与伶俐值的AOE连击"  },
        {"AoE Esprit Overcap Option" , "AoE伶俐防溢出设置"  },
        {"AoE Fan Dance Overcap Protection Option" , "AoE幻扇溢出保护"  },
        {"Adds Fan Dance 2 when Fourfold Feathers are full." , "当量谱上幻扇满了后，将扇舞破加入循环"  },
        {"AoE Fan Dance Option" , "AOE幻扇选项"  },
        #endregion
        #region Dance Features
        {"Dance Features" , "跳舞功能"  },
        {"Features and options involving Standard Step and Technical Step.\nCollapsing this category does NOT disable the features inside." , "等待翻译"  },
        #region Combined Dance Feature
        {"Combined Dance Feature" , "舞步整合"  },
        {"Standard And Technical Dance on one button (SS).\nStandard > Technical.\nThis combos out into Tillana and Starfall Dance." , "等待翻译"  },
        {"Devilment Plus Option" , "进攻之探戈附加设置"  },
        {"Adds Devilment right after Technical finish." , "在技巧舞步结束后接续进攻之探戈。"  },
        {"Flourish Plus Option" , "百花争艳附加设置"  },
        {"Adds Flourish to the Combined Dance Feature." , "将百花争艳加入到舞步整合。"  },
        #endregion
        {"Custom Dance Step Feature" , "自定义舞步功能"  },
        {"Change custom actions into dance steps while dancing.\nThis helps ensure you can still dance with combos on, without using auto dance.\nYou can change the respective actions by inputting action IDs below for each dance step.\nThe defaults are Cascade, Flourish, Fan Dance and Fan Dance II. If set to 0, they will reset to these actions.\nYou can get Action IDs with Garland Tools by searching for the action and clicking the cog." , "等待翻译"  },
        #endregion
        #region Flourishing Features
        {"Flourishing Features" , "百花争艳期间功能"  },
        {"Features and options involving Fourfold Feathers and Flourish.\nCollapsing this category does NOT disable the features inside." , "等待翻译"  },
        {"Flourishing Fan Dance Feature" , "百花齐放中扇舞相关设置"  },
        {"Replace Flourish with Fan Dance 3 & 4 during weave-windows, when Flourish is on cooldown." , "在使用百花齐放后的窗口期内，将百花争艳替换为扇舞急·扇舞终"  },
        #endregion
        #region Fan Dance Combo Features
        {"Fan Dance Combo Feature" , "扇舞连击"  },
        {"Options for Fan Dance combos.\nFan Dance 3 takes priority over Fan Dance 4." , "等待翻译"  },
        {"Fan Dance 1 -> 3 Option" , "扇舞·序 -> 扇舞·急"  },
        {"Changes Fan Dance 1 to Fan Dance 3 when available." , "当可用时，将扇舞·序替换为扇舞·急。"  },
        {"Fan Dance 1 -> 4 Option" , "扇舞·序 -> 扇舞·终"  },
        {"Changes Fan Dance 1 to Fan Dance 4 when available." , "当可用时，将扇舞·序替换为扇舞·终。"  },
        {"Fan Dance 2 -> 3 Option" , "扇舞·破 -> 扇舞·急"  },
        {"Changes Fan Dance 2 to Fan Dance 3 when available." , "当可用时，将扇舞·破替换为扇舞·急。"  },
        {"Fan Dance 2 -> 4 Option" , "扇舞·破 -> 扇舞·终"  },
        {"Changes Fan Dance 2 to Fan Dance 4 when available." , "当可用时，将扇舞·破替换为扇舞·终。"  },
        #endregion
        {"Devilment to Starfall Feature" , "进攻之探戈 -> 流星舞"  },
        {"Change Devilment into Starfall Dance after use." , "使用完进攻之探戈后将其替换为流星舞。"  },
        {"Dance Step Combo Feature" , "舞步连击相关"  },
        {"Change Standard Step and Technical Step into each dance step while dancing.\nWorks with Simple Dancer and Simple Dancer AoE." , "在跳舞时将标准舞步和技巧舞步更改为每个舞步"  },
        #region Simple Dancer (Single Target)
        {"Simple Dancer (Single Target) Feature" , "简易舞者（单目标）"  },
        {"Single button, single target. Includes songs, flourishes and overprotections.\nConflicts with all other non-simple toggles, except 'Dance Step Combo'." , "等待翻译"  },
        {"Simple Interrupt Option" , "单体连击打断技能设置"  },
        {"Includes an interrupt in the rotation (if applicable to your current target)." , "循环加入打断技能"  },
        {"Simple Standard Dance Option" , "简易标准舞步选项"  },
        {"Simple Standard Fill Option" , "简易标准舞步选项"  },
        {"Adds ONLY Standard dance steps and Standard Finish to the rotation.\nStandard Step itself must be initiated manually when using this option." , "等待翻译"  },
        {"Simple Technical Dance Option" , "简易技巧舞步选项"  },
        {"Simple Tech Fill Option" , "简易技巧舞步选项"  },
        {"Adds ONLY Technical dance steps and Technical Finish to the rotation.\nTechnical Step itself must be initiated manually when using this option." , "等待翻译"  },
        {"Simple Tech Devilment Option" , "简易技巧进攻之探戈"  },
        {"Includes Devilment in the rotation.\nWill activate only during Technical Finish if you are Lv70 or above." , "等待翻译"  },
        {"Simple Flourish Option" , "简易百花争艳"  },
        {"Simple Feathers Option" , "简易幻扇"  },
        {"Includes Feather usage in the rotation." , "等待翻译"  },
        {"Simple Feather Pooling Option" , "简易幻扇囤积"  },
        {"Expends a feather in the next available weave window when capped.\nWeaves feathers where possible during Technical Finish.\nWeaves feathers outside of burst when target is below set HP percentage." , "等待翻译"  },
        {"Simple Panic Heals Option" , "简易紧急恢复"  },
        {"Includes Curing Waltz and Second Wind in the rotation when available and your HP is below the set percentages." , "自身血量低于设定值时使用治疗华尔兹、内丹"  },
        {"Simple Improvisation Option" , "简易即兴表演"  },
        {"Includes Improvisation in the rotation when available." , "当即兴表演可用时将其加入循环。"  },
        {"Simple Peloton Opener Option" , "简易速行起手"  },
        {"Uses Peloton when you are out of combat, do not already have the Peloton buff and are performing Standard Step with greater than 5s remaining of your dance.\nWill not override Dance Step Combo Feature." , "等待翻译"  },
        #endregion
        #region Simple Dancer (AoE)
        {"Simple Dancer (AoE) Feature" , "简易舞者（AOE）"  },
        {"Single button, AoE. Includes songs, flourishes and overprotections.\nConflicts with all other non-simple toggles, except 'Dance Step Combo'." , "等待翻译"  },
        {"Simple AoE Interrupt Option" , "简易AoE中断"  },
        {"Includes an interrupt in the AoE rotation (if your current target can be interrupted)." , "在AoE循环中加入中断(伤头)(如果当前目标可被打断)。"  },
        {"Simple AoE Standard Dance Option" , "简易标准舞步选项（AOE）"  },
        {"Includes Standard Step (and all steps) in the AoE rotation." , "循环加入标准舞步"  },
        {"Simple AoE Standard Fill Option" , "简易标准舞步选项（AOE）"  },
        {"Adds ONLY Standard dance steps and Standard Finish to the AoE rotation.\nStandard Step itself must be initiated manually when using this option." , "等待翻译"  },
        {"Simple AoE Technical Dance Option" , "简易技巧舞步选项（AOE）"  },
        {"Includes Technical Step, all dance steps and Technical Finish in the AoE rotation." , "循环加入技巧舞步"  },
        {"Simple AoE Tech Fill Option" , "简易技巧舞步选项（AOE）"  },
        {"Adds ONLY Technical dance steps and Technical Finish to the AoE rotation.\nTechnical Step itself must be initiated manually when using this option." , "等待翻译"  },
        {"Simple AoE Tech Devilment Option" , "简易AoE技巧进攻之探戈"  },
        {"Includes Devilment in the AoE rotation.\nWill activate only during Technical Finish if you Lv70 or above." , "等待翻译"  },
        {"Simple AoE Flourish Option" , "简易AoE百花争艳"  },
        {"Includes Flourish in the AoE rotation." , "将百花争艳加入AoE循环。"  },
        {"Simple AoE Feathers Option" , "简易AoE幻扇"  },
        {"Includes feather usage in the AoE rotation." , "等待翻译"  },
        {"Simple AoE Feather Pooling Option" , "简易AoE幻扇囤积"  },
        {"Expends a feather in the next available weave window when capped." , "幻扇将要溢出时使用一次"  },
        {"Simple AoE Panic Heals Option" , "简易AoE紧急恢复"  },
        {"Includes Curing Waltz and Second Wind in the AoE rotation when available and your HP is below the set percentages." , "自身血量低于设定值时使用治疗华尔兹、内丹"  },
        {"Simple AoE Improvisation Option" , "简易AoE即兴表演"  },
        {"Includes Improvisation in the AoE rotation when available." , "当即兴表演可用时将其加入AoE循环。"  },
        #endregion
        #endregion
        #region DARK KNIGHT
        {"Uses Edge of Shadow if you are above 8,500 mana or Darkside is about to expire (10sec or less)" , "当你的mp高于8500或暗黑状态即将结束时(剩余持续时间不超过10秒)自动插入暗影锋."  },
        #endregion
         #region DRAGOON
        {"Simple Mode - Single Target" , "简单模式 - 单目标"  },
        {"Replaces True Thrust with a full one-button single target rotation.\nThis is the ideal option for newcomers to the job." , "将真正的突刺替换为完整的单目标一键式旋转。\n这是新手的理想选择。"  },
        #region Advanced ST Dragoon
        {"Advanced Dragoon","高级龙骑"},
        {"Replaces True Thrust with a full one-button single target rotation.\nThese features are ideal if you want to customize the rotation." , "将真正的突刺替换为完整的单目标一键式旋转。\n如果您想自定义旋转，则这些功能非常理想。"  },
        {"Level 88+ Opener" , "88级起手"  },
        {"Adds the Balance opener to the rotation.\nOPTIONAL: USE REACTION OR MOACTION FOR OPTIMAL TARGETING." , "等待翻译"  },
        #region Buffs ST
        {"Collection of CD features on Main Combo.","cd技能循环整合"},
        {"Collection of CD features on main combo.","将CD技能加入循环"},
        {"Buffs Option" , "增益选项"  },
        {"Collection of Buff features on Main Combo.","buff技能循环整合"},
        {"Includes Life Surge, while under proper buffs, onto proper GCDs, to the rotation.","在樱花/直刺/山境酷刑连击中的合适时机和buff下插入龙剑。"},
        {"Melee Dives Option","冲冲冲设置"},
        {"Dives Option","跳跃选项"},
        {"Uses Spineshatter Dive, Dragonfire Dive, and Stardiver when in the target's target ring (1 yalm) and closer.","当在目标的目标圈内（1米）时使用破碎冲，龙炎冲和坠星冲"},
        {"Buffs on Main Combo","主连击Buff整合"},
        {"Adds Spineshatter Dive, Dragonfire Dive, and Stardiver to the rotation.\n Select options below for when to use dives.","将破碎冲，龙炎冲和坠星冲加入循环，用以下选项确定释放时机"},
        {"High Jump AoE Feature","高跳设置"},
        {"Includes High Jump to the AoE rotation.","在连击中插入高跳。"},
        {"Adds various buffs to the rotation." , "将各种增益效果添加到旋转中。"  },
        {"Battle Litany Option" , "战斗连祷选项"  },
        {"Includes Geirskogul and Nastrond to the AoE rotation.","将武神枪与死者之岸插入AOE循环"},
        {"Includes High Jump/Jump to the rotation.","将高跳/跳跃加入循环"},
        {"Adds Battle Litany to the rotation." , "将战斗连祷添加到旋转中。"  },
        {"Lance Charge Option" , "猛枪选项"  },
        {"Adds Lance Charge to the rotation." , "将猛枪添加到旋转中。"  },
        {"Includes Lance Charge to the rotation.","猛枪加入循环"},
        {"Dragon Sight Option" , "巨龙视线选项"  },
        {"Advanced Dragoon AoE","高级龙骑AOE"},
        {"Mirage AoE Feature","幻象冲设置"},
        {"Includes Mirage to the AoE rotation.","在AOE连击中插入幻象冲"},
        {"Replaces Doom Spike with its combo chain","等待翻译"},
        {"Includes Dragon Sight to the AoE rotation. OPTIONAL: USE REACTION OR MOACTION FOR OPTIMAL TARGETING.","在连击中插入巨龙视线。需要自行选择最优目标。\n可选：使用【REACTION】或【MOACTION】以获得最佳目标。"},
        {"Adds Dragon Sight to the rotation.\nOPTIONAL: USE REACTION OR MOACTION FOR OPTIMAL TARGETING." , "将巨龙视线添加到旋转中。\n可选：使用REACTION或MOACTION以获得最佳目标。"  },
        #endregion
        #region Cooldowns ST
        {"Cooldowns Option" , "冷却选项"  },
        {"Includes Life Surge, while under proper buffs, onto proper GCDs, to the AoE rotation.","在连击中合适的状态和窗口内插入龙剑。"},
        {"Replaces Main AoE Combo with Piercing Talon when you are out of melee range.","超出近战范围时，将主要AOE连击替换为贯穿尖"},
        {"Adds various cooldowns to the rotation." , "等待翻译"  },
        {"Life Surge Option" , "龙剑选项"  },
        {"Adds Life Surge, while under Dragon Sight and Lance Charge buffs, to the rotation." , "等待翻译"  },
        {"Dragonfire Dive Option" , "等待翻译"  },
        {"Adds Dragonfire Dive to the rotation." , "等待翻译"  },
        {"Spineshatter Dive Option" , "等待翻译"  },
        {"Adds Spineshatter Dive to the rotation." , "等待翻译"  },
        {"Stardiver Option" , "等待翻译"  },
        {"Adds Stardiver to the rotation." , "等待翻译"  },
        {"High Jump Option" , "高跳选项"  },
        {"Adds High Jump/Jump to the rotation." , "等待翻译"  },
        {"Mirage Dive Option" , "等待翻译"  },
        {"Adds Mirage Dive to the rotation." , "等待翻译"  },
        {"Geirskogul and Nastrond Option" , "武神枪和死者之岸选项"  },
        {"Adds Geirskogul and Nastrond to the rotation." , "等待翻译"  },
        {"Wyrmwind Thrust Option" , "天龙点睛选项"  },
        {"Adds Wyrmwind Thrust to the rotation." , "等待翻译"  },
        #endregion
        {"Optimized Rotation Option" , "等待翻译"  },
        {"Uses optimzed use of Geirskogul and (High) Jump/Mirage Dive" , "等待翻译"  },
        {"Ranged Uptime Option" , "超出近战范围选项"  },
        {"Adds Piercing Talon to the rotation when you are out of melee range." , "等待翻译"  },
        {"Combo Heals Option" , "回复设置"  },
        {"Adds Bloodbath and Second Wind to the rotation." , "等待翻译"  },
        {"Dynamic True North Option" , "等待翻译"  },
        {"Adds True North before Chaos Thrust/Chaotic Spring, Fang And Claw and Wheeling Thrust when you are not in the correct position for the enhanced potency bonus." , "在不处于增强效力奖励的正确位置时，在混沌突/混沌之泉、破碎之牙和旋风突前添加真北。"  },
        #endregion
        {"Simple Mode - AoE" , "简单模式 - AoE"  },
        {"Buffs AoE Feature","buff设置"},
        {"Dragon Sight AoE Feature","巨龙视线设置Aoe"},
        {"Includes Lance Charge and Battle Litany to the AoE rotation.","在连击中插入猛枪和战斗连祷。"},
        {"Includes Battle Litany to the rotation.","将战斗连祷加入循环"},
        {"Replaces Main Combo with Piercing Talon when you are out of melee range.","超出近战范围时，将主要AOE连击替换为贯穿尖"},
        {"Includes Geirskogul and Nastrond to the rotation.","将武神枪和死者之岸加入循环"},
        #region Advanced AoE Dragoon
        {"CDs on Main Combo","主连击CD整合"},
        {"Dives AoE Feature","跳跃类能力技设置"},
        {"Includes Spineshatter Dive, Dragonfire Dive and Stardiver to the AoE rotation.","在aoe连击中插入破碎冲，龙炎冲，坠星冲。"},
        {"Replaces Doom Spike with a full one-button AoE rotation.\nThese features are ideal if you want to customize the rotation." , "用一个全键AoE旋转替换末日尖刺。\n如果你想自定义旋转，这些功能是理想的。"  },
        #region Buffs AoE
        {"Buffs AoE Option" , "等待翻译"  },
        {"Adds Lance Charge and Battle Litany to the rotation." , "等待翻译"  },
        {"Battle Litany AoE Option" , "等待翻译"  },
        {"Lance Charge AoE Option" , "等待翻译"  },
        {"Dragon Sight AoE Option" , "等待翻译"  },
        #endregion
        #region CDs AoE
        {"Adds Life Surge, while under proper buffs, onto proper GCDs, to the rotation." , "等待翻译"  },
        {"Adds Stardiver to the rotation when under at least 1 buff" , "等待翻译"  },
        {"Adds High Jump to the rotation." , "等待翻译"  },
        {"Wyrmwind Option" , "等待翻译"  },
        #endregion
        #endregion
        
        {"Jump to Mirage Dive" , "等待翻译"  },
        {"Replace (High) Jump with Mirage Dive when Dive Ready." , "等待翻译"  },
        {"Stardiver Feature" , "坠星冲设置"  },
        {"Turns Stardiver into Nastrond during Life of the Dragon, and Geirskogul outside of Life of the Dragon." , "在红莲龙血中将坠星冲变成死者之岸，在红莲龙血之外变成武神枪"  },
        {"Lance Charge to Battle Litany Feature" , "猛枪整合到战斗连祷"  },
        {"Turns Lance Charge into Battle Litany when the former is on cooldown." , "猛枪冷却完毕后整合至战斗连祷"  },
        {"Adds Dragon Sight to Lance Charge, will take precedence over Battle Litany." , "将巨龙视线整合至猛枪（优先于战斗连祷）"  },
        {"Cure Option" , "治疗选项"  },
        {"Use Variant Cure when HP is below set threshold." , "当HP低于设定值时使用多变治疗"  },
        {"Rampart Option" , "多变铁壁选项"  },
        {"Use Variant Rampart on cooldown." , "多变铁壁冷却好使用"  },
        #endregion
        #region GUNBREAKER
        #region ST
        #region Gnashing Fang
        #endregion
        #region Cooldowns
        #endregion
        #region Rough Divide
        {"Uses Rough Divide when under No Mercy, burst cooldowns when available, not moving, and in the target ring (1 yalm).\nWill use as many stacks as selected in the above slider." , "当处于无情状态下，可以使用爆发技能冷却且不移动，并且在目标环内（1英亚）时，使用粗犷分裂。将使用上面滑块中选择的堆叠数。"  },
        #endregion
        #endregion
        #region Gnashing Fang
        #endregion
        #region AoE
        #endregion
        #region Burst Strike
        #endregion
        #region No Mercy
        #endregion
        #endregion
         #region MACHINIST
        #region Simple ST
        {"Replaces Split Shot with a one-button full single target rotation.\nThis is ideal for newcomers to the job." , "用一个按钮完整的单目标循环替换分裂弹。这对于新手来说是理想的职业选择。\n注意是分裂弹不是热分裂弹"  },
        #endregion
        #region Advanced ST
        {"Replaces Split Shot with a one-button full single target rotation.\nThese features are ideal if you want to customize the rotation." , "一键输出。\n注意是[分裂弹]不是[热分裂弹]"  },
        {"Level 90 Opener Option" , "90级起手[推荐]"  },
        {"Uses the Balance opener depending on which rotation is selected above." , "开局的循环期间不要干涉[除减伤自爆类技能]，否则会卡住。\n等他放完高达，循环结束"  },
        {"Hot Shot / Air Anchor option" , "热弹/空中锚选项"  },
        {"Adds Hot Shot/Air Anchor to the rotation." , "将热弹/空中锚添加到循环中。"  },
        {"Reassemble Option" , "重新整备选项"  },
        {"Adds Reassemble to the rotation." , "将整备添加到循环中。"  },
        {"Gauss Round / Ricochet Option" , "虹吸弹/弹射选项"  },
        {"Adds Gauss Round and Ricochet to the rotation.\nWill prevent overcapping.", "将虹吸弹和弹射添加到循环中。\n将防止过度堆积。"  },
        {"Hypercharge Option" , "超荷"  },
        {"Adds Hypercharge to the rotation." , "将超荷添加到循环中。"  },
        {"Heat Blast Option" , "热冲击选项"  },
        {"Adds Heat Blast to the rotation" , "将热冲击加到循环中"  },
        {"Rook Autoturret/Automaton Queen Option" , "高达选项"  },
        {"Adds Rook Autoturret/Automaton Queen to the rotation." , "将高达添加到循环中。"  },
        {"Wildfire Option" , "野火设置"  },
        {"Adds Wildfire to the rotation." , "将野火添加到循环中。"  },
        {"Drill option" , "钻头选项"  },
        {"Adds Drill to the rotation." , "将钻头添加到循环中。"  },
        {"Barrel Stabilizer Option" , "枪管加热选项"  },
        {"Adds Barrel Stabilizer to the rotation." , "将枪管加热添加到旋转中。"  },
        {"Wildfire Only Option" , "[AI]在特定时机插入"  },
        {"Only use Barrel Stabilizer to prepare for Wildfire." , "[AI]仅使用枪管稳定器准备野火。"  },
        {"Chain Saw option" , "回转飞锯选项"  },
        {"Adds Chain Saw to the rotation." , "将回转飞锯添加到循环中。"  },
        {"Head Graze Option" , "[AI]头部擦伤选项"  },
        {"Uses Head Graze to interrupt during the rotation, where applicable." , "[AI]在适当的情况下使用头部擦伤来打断旋转。"  },
        {"Second Wind Option" , "内丹选项"  },
        {"Use Second Wind when below the set HP percentage." , "[AI]当低于设置的HP百分比时使用"  },
        #endregion
        #region Simple AoE
        {"Replaces Spread Shot with a one-button full single target rotation.\nThis is ideal for newcomers to the job." , "用一个按钮的完整单目标旋转替换分裂弹。\n如果您是新手，则这非常理想。"  },
        #endregion
        #region Advanced AoE
        {"Replaces Spread Shot with a one-button full single target rotation.\nThese features are ideal if you want to customize the rotation." , "[AI]用一个按钮的完整单目标旋转替换分裂弹。\n如果您想自定义旋转，则这些功能非常理想。"  },
        {"Adds Gauss Round/Ricochet to the rotation." , "[AI]将虹吸弹和弹射添加到旋转中。\n将防止过度堆积。"  },
        {"Flamethrower Option" , "[AI]将火焰喷射器添加到旋转中。"  },
        {"Adds Flamethrower to the rotation." , "[AI]将火焰喷射器添加到旋转中。"  },
        {"Bioblaster Option" , "[AI]将生物爆破器添加到旋转中。"  },
        {"Adds Bioblaster to the rotation." , "[AI]将生物爆破器添加到旋转中。"  },
        {"Chain Saw Option" , "[AI]回转飞锯"  },
        {"Adds Chain Saw to the the rotation." , "[AI]将链锯添加到旋转中。"  },
        #endregion
        #region Variant
        #endregion
        {"Overdrive Feature" , "[AI]超档炮塔/人偶替换设置"  },
        {"Replace Rook Autoturret and Automaton Queen with Overdrive while active." , "[AI]在技能可用时，将车式浮空炮塔和后式自走人偶转换为超档车式炮塔和超档自走人偶"  },
        {"Gauss Round/Ricochet Feature" , "[AI]虹吸弹 / 弹射设置"  },
        {"Replace Gauss Round and Ricochet with one or the other depending on which has more charges." , "[AI]将虹吸弹和弹射替换为一个或其他需要更多充电电能的技能."  },
        {"Drill/Air Anchor (Hot Shot) Feature" , "[AI]钻头、空气锚 (热弹)、回转飞锯根据cd时间互相替换."  },
        {"Replace Drill and Air Anchor (Hot Shot) with one or the other (or Chain Saw) depending on which is on cooldown." , "根据哪个技能在冷却中，用其中一个（或链锯）替换钻头和空中锚（热射）。"  },
        {"Single Button Heat Blast Feature" , "[AI]热冲击一键整合"  },
        {"Switches Heat Blast to Hypercharge." , "[AI]整合超荷至热冲击."  },
        {"Barrel Feature" , "[AI]枪管加热 设置"  },
        {"Adds Barrel Stabilizer to Single Button Heat Blast Feature when below 50 Heat Gauge and it is off cooldown" , "[AI]当热量计低于50且冷却完毕时，将枪管稳定器添加到单按钮热冲击功能中"  },
        {"Adds Wildfire to the Single Button Heat Blast Feature if Wildfire is off cooldown and you have enough Heat Gauge for Hypercharge then Hypercharge will be replaced with Wildfire.\nAlso weaves Ricochet/Gauss Round on Heat Blast when necessary." , "[AI]如果野火冷却完毕且您有足够的热量计，则将野火添加到单按钮热冲击功能中，然后将超荷替换为野火。\n在必要时，还会在热冲击上编织弹射/虹吸弹。"  },
        {"Single Button Auto Crossbow Feature" , "[AI]自动弩一键整合"  },
        {"Switches Auto Crossbow to Hypercharge and weaves Gauss Round/Ricochet." , "[AI]将自动弩切换到超荷并编织虹吸弹/弹射。"  },
        {"Adds Barrel Stabilizer to Single Button Auto Crossbow Feature when below 50 Heat Gauge and it is off cooldown" , "[AI]当热量计低于50且冷却完毕时，将枪管稳定器添加到单按钮自动弩功能中"  },
        {"Physical Ranged DPS: Double Dismantle Protection" , "[AI]物理远程DPS：双重拆卸保护"  },
        {"Prevents the use of Dismantle when target already has the effect by replacing it with Fire." , "[AI]通过用火替换来防止目标已经具有效果的拆卸使用。"  },
        {"Dismantle - Tactician" , "[AI]拆卸-战术家"  },
        {"Swap dismantle with tactician when dismantle is on cooldown." , "[AI]当拆卸冷却时，用战术家替换拆卸。"  },
        #endregion
        #region MONK
        {"Start with the Lunar Solar Opener on the main combo. Requires level 68 for Riddle of Fire.\nA 1.93/1.94 GCD is highly recommended." , "[AI]在主连击中使用月光/日光开场。需要疾风的极意68级。\n强烈建议使用1.93/1.94 GCD。"  },
        {"Adds Bloodbath and Second Wind to the combo, using them when below the HP Percentage threshold." , "将吸血和内丹添加到连击中，在生命值低于百分比阈值时使用。"  },
        #endregion
        #region NINJA
        {"Starts with the Balance opener.\nDoes pre-pull first, if you enter combat before hiding the opener will fail.\nLikewise, moving during TCJ will cause the opener to fail too.\nRequires you to be out of combat with majority of your cooldowns available for it to work." , "以平衡开场为开始。\n如果您在隐藏开场之前进入战斗，开场将失败。\n同样，在天地人之间移动也会导致开场失败。\n需要您处于非战斗状态，并且大部分冷却时间可用才能正常工作。"  },
        {"Only uses Mug whilst the target has Trick Attack, otherwise will use on cooldown." , "只有在目标存在攻其不备时才使用夺取，否则将在冷却完毕时使用。"  },
        {"Turns Ten Chi Jin (the move) into Ten, Chi, and Jin." , "将天地人之后的技能依次变为天之印、地之印、人之印。"  },
        #endregion
        #region PALADIN
        #endregion
        #region REAPER
        #region Single Target (Slice) Combo Section
        {"Adds Shadow of Death to Slice Combo if Death's Design is not present on current target, or is about to expire." , "当目标没有或者死亡烙印Debuff即将到期时，将死亡之影加入连击。"  },
        {"Adds Bloodbath and Second Wind to the combo at 65%% and 40%% HP, respectively." , "在血量低于65%和40%时，将浴血和内丹加入连击。"  },
        {"Replaces the combo chain with Harpe (or Harvest Moon, if available) when outside of melee range. Will not override Communio." , "当超出近战范围时，使用勾刃（当可用时，使用收获月）替换切割。不会替换掉团契。"  },
        {"Uses Enshroud at 50 Shroud during Arcane Circle (mimics the 2-minute Double Enshroud window) and will use Enshroud for odd minute bursts.\nBelow level 88, will use Enshroud at 50 gauge." , "[AI]在奥秘之环期间，使用50 Shroud的Enshroud（模拟2分钟的双重Enshroud窗口），并在奇数分钟爆发时使用Enshroud。\n在88级以下，将在50测量单位处使用Enshroud。"  },
        {"Adds Gluttony and Blood Stalk to the combo when target is afflicted with Death's Design, and the skills are off cooldown and < 50 soul." , "当目标有死亡烙印Debuff并且灵魂小于50并且暴食/隐匿挥割不在冷却时，将其加入连击。"  },
        #endregion
        #region AoE (Scythe) Combo Section
        {"Adds Whorl of Death to AoE combo if Death's Design is not present on current target, or is about to expire." , "当目标的死亡烙印Debuff不存在或即将消失时 将死亡之涡加入AOE连击"  },
        #endregion
        #region Blood Stalk/Grim Swathe Combo Section
        {"Adds Enshroud combo (Void/Cross Reaping, Communio, and Lemure's Slice) on Blood Stalk and Grim Swathe." , "将魂衣连击（交错/虚无收割，团契，夜游魂切割）替换至隐匿挥割和束缚挥割上"  },
        #endregion
        #region Miscellaneous
        {"Adds True North to Slice, Shadow of Death, Enshroud, and Blood Stalk when under Gluttony and if Gibbet/Gallows options are selected to replace those skills." , "如果绞决/缢杀设置替换了切割/死亡之影/夜游魂衣/隐匿挥割,在暴食后将真北加入这些技能。"  },
        {"Will hold the last charge of True North for use with Gluttony, even when out of position for Gibbet/Gallows potency bonuses." , "等待翻译"  },
        #endregion
        #endregion
        #region RED MAGE
        #region Single Target DPS
        #endregion
        #region AoE DPS
        #endregion
        #region QoL
        #endregion
        #region Sections 8 to 9 - Miscellaneous
        {"Use Variant Rampart on cooldown. Replaces Jolts." , "在冷却时使用Variant Rampart。替代Jolts技能。"  },
        {"Turn Swiftcast into Variant Raise whenever you have the Swiftcast or Dualcast buffs." , "当你拥有Swiftcast或Dualcast增益时，将其转化为Variant Raise。"  },
        {"Use Variant Cure when HP is below set threshold. Replaces Jolts." , "当HP低于设定阈值时使用Variant Cure。替代Jolts技能。"  },
        {"Cure on Vercure Option" , "在Vercure选项上使用Cure技能。"  },
        {"Replaces Vercure with Variant Cure." , "用Variant Cure替代Vercure技能。"  },
        #endregion
        #endregion
        #region SAGE
        #region Single Target DPS Feature
        #endregion
        #region AoE DPS Feature
        #endregion
        #region Diagnosis Simple Single Target Heal
        {"Eukrasian Diagnosis Option" , "均衡诊断选项"  },
        {"Diagnosis becomes Eukrasian Diagnosis if the shield is not applied to the target." , "当所选目标没有盾值时，替换诊断为均衡诊断。"  },
        #endregion
        #region Sage Simple AoE Heal
        {"Warning, will force the use of Eukrasia Prognosis, and normal Prognosis will be unavailable." , "等待翻译"  },
        #endregion
        #region Misc Healing
        #endregion
        #region Utility
        #endregion
        #endregion
        #region SAMURAI
        #region Overcap Features
        #endregion
        #region Main Combo (Gekko) Features
        {"Adds Yukikaze combo to main combo. Will add Yukikaze during Meikyo Shisui as well" , "[AI]将雪風連擊添加到主連擊中。在明鏡止水期間也會添加雪風連擊"  },
        {"Level 90 Samurai Opener" , "90级武士开场技能组合"  },
        {"Adds the Level 90 Opener to the main combo.\nOpener triggered by using Meikyo Shisui before combat. If you have any Sen, Hagakure will be used to clear them.\nWill work at any levels of Kenki, requires 2 charges of Meikyo Shisui and all CDs ready. If conditions aren't met it will skip into the regular rotation. \nIf the Opener is interrupted, it will exit the opener via a Goken and a Kaeshi: Goken at the end or via the last Yukikaze. If the latter, CDs will be used on cooldown regardless of burst options." , "将90级开场加入主连击。\n使用明镜止水前触发开场。如果你有任何Sen，Hagakure将被用来清除它们。\n将在任何Kenki等级下工作，需要2次明镜止水和所有CD准备好。如果条件不满足，它将跳过进入常规旋转。\n如果开场被打断，它将通过Goken和Kaeshi: Goken在结束时或通过最后的雪风退出开场。如果是后者，CD将无论爆发选项如何都会被用于冷却。"  },
        #region CDs on Main Combo
        {"Adds Midare: Setsugekka, Higanbana, and Kaeshi: Setsugekka when ready and when you're not moving to main combo." , "在未移动和技能冷却完毕的情况下，将纷乱雪月花、彼岸花和回返雪月花加入循环。"  },
        #region Ogi Namikiri on Main Combo
        {"Saves Ogi Namikiri for even minute burst windows.\nIf you don't activate the opener or die, Ogi Namikiri will instead be used on CD." , "将大刀·波尼切保留用于偶数分钟的突发窗口。\n如果你没有激活开场技能组合或死亡，将会在技能冷却完毕时使用大刀·波尼切。"  },
        #endregion
        #region Meikyo Shisui on Main Combo
        {"Saves Meikyo Shisui for burst windows.\nIf you don't activate the opener or die, Meikyo Shisui will instead be used on CD." , "将明镜止水保留用于突发窗口。如果你没有激活开场技能组合或死亡，将会在技能冷却完毕时使用明镜止水。"  },
        #endregion
        {"Saves Senei for even minute burst windows.\nIf you don't activate the opener or die, Senei will instead be used on CD." , "[AI]“将必杀剑・闪影保留用于每分钟的爆发窗口。如果您没有激活开场或死亡，则必杀剑・闪影将在CD上使用。"  },
        #endregion
        #endregion
        #region Yukikaze/Kasha Combos
        #endregion
        #region AoE Combos
        {"Adds Tenka Goken, Midare: Setsugekka, and Kaeshi: Goken when ready and when you're not moving to Mangetsu combo." , "在准备好且未移动时，将天下五剑、纷乱雪月花和回返五剑加入满月连击中。"  },
        {"Adds the Yukikaze combo, Mangetsu combo, Senei, Shinten, and Shoha to Oka combo.\nUsed for two targets only and when Lv86 and above." , "将雪风连击、满月连击、千影、侍天和照破加入岡连击中。\n仅适用于两个目标且等级在86级及以上。"  },
        #endregion
        #region Cooldown Features
        {"Replace Meikyo Shisui with Jinpu, Shifu, and Yukikaze depending on what is needed." , "根据需要，用阵风、士风和雪风代替明镜止水。"  },
        #endregion
        #region Iaijutsu Features
        #endregion
        #region Shinten Features
        #endregion
        #region Kyuten Features
        #endregion
        #region Other
        {"Replace Ikishoten with Ogi Namikiri and then Kaeshi Namikiri when available.\nIf you have full Meditation stacks, Ikishoten becomes Shoha while you have Ogi Namikiri ready." , "如果可用，用大刀·波尼切替换意气风发，然后再用回返波尼切。\n如果你拥有满的冥想层数，意气风发将变成照破，同时你准备好使用大刀·波尼切。"  },
        #endregion
        #endregion
        #region SCHOLAR
        #region DPS
        {"Replaces Ruin I / Broils with options below" , "将毁灭I / 烧毁替换为以下选项"  },
        {"Automatic DoT uptime." , "自动DoT持续时间。"  },
        #endregion
        #region Healing
        {"Change Lustrate into Excogitation when Excogitation is ready." , "当深谋远虑之策准备就绪时，将生命活性法替换为深谋远虑之策"  },
        {"Change Recitation into either Adloquium, Succor, Indomitability, or Excogitation when used." , "秘策使用时，将其替换为鼓舞激励之策/士气高扬之策/不屈不挠之策/深谋远虑之策"  },
        {"Change Whispering Dawn into Fey Illumination, Fey Blessing, then Whispering Dawn when used." , "将仙光的低语依次替换为异想的幻光、异想的祥光"  },
        {"Change Physick into Adloquium, Lustrate, then Physick with below options:" , "以下选项将医术依次替换为鼓舞激励之策/生命活性法"  },
        #endregion
        #region Utilities
        {"Change Aetherflow-using skills to Aetherflow, Recitation, or Dissipation as selected." , "将使用以太超流的技能替换为以太超流或转化"  },
        {"If Aetherflow is on cooldown, show Dissipation instead." , "当以太超流冷却时使用转化替代"  },
        {"Change all fairy actions into Summon Eos when the Fairy is not summoned." , "当未召唤仙女时，将所有仙女动作更改为召唤Eos。"  },
        #endregion
        #endregion
        #region SUMMONER
        {"Advanced combo features for a greater degree of customisation.\nAccommodates SpS builds.\nRuin III is left unchanged for mobility purposes." , "[AI]高级连击功能，可实现更高程度的自定义。\n适用于SpS构建。\n为了提高机动性，Ruin III保持不变。"  },
        {"Adds Deathflare, Ahk Morn and Revelation to the single target and AoE combos." , "将龙神迸发、不死鸟迸发和死星核爆添加到单目标和群体技能中。"  },
        {"Adds Gemshine and Precious Brilliance to the single target and AoE combos, respectively." , "分别将宝石耀和宝石辉添加到单目标和群体技能中。"  },
        {"Adds Ruin IV to the single target and AoE combos.\nUses when moving during Garuda Phase and you have no attunement, when moving during Ifrit phase, or when you have no active Egi or Demi summon." , "将毁绝 IV 添加到单目标和群体技能中。\n在Garuda阶段移动且没有调谐时，或在Ifrit阶段移动时，或没有活动的Egi或Demi召唤时使用。"  },
        {"Changes Painflare to Ruin IV when out of Aetherflow stacks, Energy Siphon is on cooldown, and Ruin IV is up." , "[AI]当以太流堆栈用完、能量虹吸冷却时，将痛苦核爆替换为毁绝 IV。"  },
        {"Only uses Crimson Cyclone if you are not moving, or have no remaining Ifrit Attunement charges." , "只在未移动或者没有火神对应技能的情况下使用。"  },
        {"General purpose one-button combo.\nBursts on Bahamut phase.\nSummons Titan, Garuda, then Ifrit.\nSwiftcasts on Slipstream unless drifted." , "[AI]通用单按钮连击。\n在巴哈姆特阶段爆发。\n召唤泰坦、迦楼罗，然后召唤伊弗利特。\n除非漂移，否则在滑流上使用迅捷咏叹。"  },
        #endregion
        #region WARRIOR
        {"Steel Cyclone / Decimate Option" , "钢铁旋风/屠戮选项"  },
        {"Adds Steel Cyclone / Decimate to Advanced Mode." , "将钢铁旋风/屠戮添加到高级模式中。"  },
        #endregion
         #region WHITE MAGE
        #region Single Target DPS Feature
        {"Single Target DPS Feature" , "整合单体输出技能"  },
        {"Collection of cooldowns and spell features on Glare/Stone." , "咏唱闪耀/飞石后插入能力技."  },
        {"Use the Balance opener from level 56+." , "从56级开始使用平衡开场。"  },
        {"Aero/Dia Uptime Option" , "保持dot不断 设置"  },
        {"Adds Aero/Dia to the single target combo if the debuff is not present on current target, or is about to expire." , "当目标身上不存在dot或dot即将结束时，在单目标输出循环中插入疾风/烈风/天辉."  },
        {"Assize Option" , "法令 设置"  },
        {"Adds Assize to the single target combo." , "将法令插入单目标输出循环中."  },
        {"Afflatus Misery Option" , "苦难之心 设置"  },
        {"Adds Afflatus Misery to the single target combo when it is ready to be used." , "当苦难之心可用时将其插入单目标输出循环中."  },
        {"Lily Overcap Protection Option" , "百合保护"  },
        {"Adds Afflatus Rapture to the single target combo when at three Lilies." , "当有三档治疗百合时在单目标输出循环中插入苦难之心."  },
        {"Presence of Mind Option" , "神速咏唱 设置"  },
        {"Adds Presence of Mind to the single target combo." , "将神速咏唱插入单目标输出循环中."  },
        {"Lucid Dreaming Option" , "循环加入醒梦"  },
        {"Adds Lucid Dreaming to the single target combo when below set MP value." , "当MP低于设定值时将醒梦插入单目标输出循环中."  },
        #endregion
        #region AoE DPS Feature
        {"AoE DPS Feature" , "AoE DPS连击"  },
        {"Collection of cooldowns and spell features on Holy/Holy III." , "整合AoE技能到神圣/豪圣."  },
        {"Adds Assize to the AoE combo." , "将法令整合AoE循环中."  },
        {"Adds Afflatus Misery to the AoE combo when it is ready to be used." , "当苦难之心可用时将其插入AoE循环中."  },
        {"Adds Afflatus Rapture to the AoE combo when at three Lilies." , "当有三档治疗百合时在AoE循环中插入苦难之心."  },
        {"Adds Presence of Mind to the AoE combo if you are moving or it can be weaved without GCD delay." , "如果您正在移动或可以在不延迟GCD的情况下编织，则将Presence of Mind添加到AoE组合中。"  },
        {"Adds Lucid Dreaming to the AoE combo when below the set MP value if you are moving or it can be weaved without GCD delay." , "如果您正在移动或可以在不延迟GCD的情况下编织，则在MP值低于设置值时将Lucid Dreaming添加到AoE组合中。"  },
        #endregion
        {"Solace into Misery Feature" , "安慰之心与苦难之心整合 设置"  },
        {"Replaces Afflatus Solace with Afflatus Misery when it is ready to be used." , "当苦难之心可用时，替换安慰之心为苦难之心."  },
        {"Rapture into Misery Feature" , "狂喜之心与苦难之心整合 设置"  },
        {"Replaces Afflatus Rapture with Afflatus Misery when it is ready to be used." , "当苦难之心可用时，替换狂喜之心为苦难之心."  },
        #region AoE Heals Feature
        {"Simple Heals (AoE)" , "等待翻译"  },
        {"Replaces Medica with a one button AoE healing setup." , "等待翻译"  },
        {"Afflatus Rapture Option" , "等待翻译"  },
        {"Uses Afflatus Rapture when available." , "等待翻译"  },
        {"Uses Afflatus Misery when available." , "等待翻译"  },
        {"Thin Air Option" , "等待翻译"  },
        {"Uses Thin Air when available." , "等待翻译"  },
        {"Cure III Option" , "等待翻译"  },
        {"Replaces Medica with Cure III when available." , "等待翻译"  },
        {"Uses Assize when available." , "等待翻译"  },
        {"Plenary Indulgence Option" , "等待翻译"  },
        {"Uses Plenary Indulgence when available." , "等待翻译"  },
        {"Uses Lucid Dreaming when available." , "等待翻译"  },
        #endregion
        #region Single Target Heals
        {"Simple Heals (Single Target)" , "简易治疗（单目标）"  },
        {"Replaces Cure with a one button single target healing setup." , "等待翻译"  },
        {"Regen Option" , "等待翻译"  },
        {"Applies Regen to the target if missing." , "等待翻译"  },
        {"Benediction Option" , "等待翻译"  },
        {"Uses Benediction when target is below HP threshold." , "等待翻译"  },
        {"Afflatus Solace Option" , "等待翻译"  },
        {"Uses Afflatus Solace when available." , "等待翻译"  },
        {"Tetragrammaton Option" , "等待翻译"  },
        {"Uses Tetragrammaton when available." , "等待翻译"  },
        {"Divine Benison Option" , "等待翻译"  },
        {"Uses Divine Benison when available." , "等待翻译"  },
        {"Aqualveil Option" , "等待翻译"  },
        {"Uses Aquaveil when available." , "等待翻译"  },
        {"Esuna Option" , "等待翻译"  },
        {"Applies Esuna to your target if there is a cleansable debuff." , "等待翻译"  },
        #endregion
        {"Cure II Sync Feature" , "救疗同步 设置"  },
        {"Changes Cure II to Cure when synced below Lv.30." , "当等级同步至30级以下时替换救疗为治疗."  },
        {"Alternative Raise Feature" , "替代性的复活功能"  },
        {"Changes Swiftcast to Raise." , "整合即刻咏唱至复活."  },
        {"Thin Air Raise Feature" , "无中生有-复活特性"  },
        {"Adds Thin Air to the Global Raise Feature/Alternative Raise Feature." , "在即刻复活前插入无中生有."  },
        {"Spirit Dart Option" , "多变精神镖选项"  },
        {"Use Variant Spirit Dart whenever the debuff is not present or less than 3s." , "在多变精神镖Debuff时间少于 3 秒时使用"  },
        #endregion
        #region DOH
        {"Placeholder" , "Placeholder"  },
        {"Placeholder." , "Placeholder."  },
        #endregion
        #region DOL
        #endregion
        #endregion
        #region PvP Combos
        #region PvP GLOBAL FEATURES
        {"Emergency Heals Feature" , "等待翻译"  },
        {"Uses Recuperate when your HP is under the set threshold and you have sufficient MP." , "等待翻译"  },
        {"Emergency Guard Feature" , "等待翻译"  },
        {"Uses Guard when your HP is under the set threshold." , "等待翻译"  },
        {"Quick Purify Feature" , "等待翻译"  },
        {"Prevent Mash Cancelling Feature" , "等待翻译"  },
        {"Stops you cancelling your guard if you're pressing buttons quickly." , "等待翻译"  },
        #endregion
        #region ASTROLOGIAN
        {"Turns Fall Malefic into an all-in-one damage button." , "[AI]将落星恶魔变成全能伤害技能按钮。"  },
        {"Double Cast Option" , "[AI]双重施法选项"  },
        {"Adds Double Cast to Burst Mode." , "[AI]将双重施法添加到爆发模式中。"  },
        {"Card Option" , "[AI]卡片选项"  },
        {"Adds Drawing and Playing Cards to Burst Mode." , "[AI]将抽取和打出卡片添加到爆发模式中。"  },
        {"Double Cast Heal Feature" , "[AI]双重施法治疗特性"  },
        {"Adds Double Cast to Aspected Benefic." , "[AI]将双重施法添加到相位益处中。"  },
        #endregion
        #region BLACK MAGE
        {"Night Wing Option" , "等待翻译"  },
        {"Adds Night Wing to Burst Mode." , "等待翻译"  },
        {"Aetherial Manipulation Option" , "等待翻译"  },
        #endregion
        #region BARD
        #endregion
        #region DANCER
        {"Adds Honing Dance to the main combo when in melee range (respects global offset).\nThis option prevents early use of Honing Ovation!\nKeep Honing Dance bound to another key if you want to end early." , "等待翻译"  },
        {"Adds Curing Waltz to the combo when available, and your HP is at or below the set percentage." , "等待翻译"  },
        #endregion
        #region DARK KNIGHT
        {"Plunge Option" , "等待翻译"  },
        {"Adds Plunge to Burst Mode." , "等待翻译"  },
        {"Uses Plunge whilst in melee range, and not just as a gap-closer." , "等待翻译"  },
        #endregion
        #region DRAGOON
        {"Using Elusive Jump turns Wheeling Thrust Combo into all-in-one burst damage button." , "等待翻译"  },
        {"Geirskogul Option" , "等待翻译"  },
        {"Adds Geirskogul to Burst Mode." , "等待翻译"  },
        {"Nastrond Option" , "等待翻译"  },
        {"Adds Nastrond to Burst Mode." , "等待翻译"  },
        {"Horrid Roar Option" , "等待翻译"  },
        {"Adds Horrid Roar to Burst Mode." , "等待翻译"  },
        {"Sustain Chaos Spring Option" , "等待翻译"  },
        {"Adds Wyrmwind Thrust to Burst Mode." , "等待翻译"  },
        {"High Jump Weave Option" , "等待翻译"  },
        {"Adds High Jump to Burst Mode." , "等待翻译"  },
        {"Elusive Jump Burst Protection Option" , "等待翻译"  },
        {"Disables Elusive Jump if Burst is not ready." , "等待翻译"  },
        #endregion
        #region GUNBREAKER
        {"Turns Solid Barrel Combo into an all-in-one damage button." , "等待翻译"  },
        {"Gnashing Fang Continuation Feature" , "等待翻译"  },
        {"Adds Continuation onto Gnashing Fang." , "等待翻译"  },
        {"Draw And Junction Option" , "等待翻译"  },
        {"Adds Draw And Junction to Burst Mode." , "等待翻译"  },
        {"Gnashing Fang Option" , "等待翻译"  },
        {"Continuation Option" , "等待翻译"  },
        {"Adds Continuation to Burst Mode." , "等待翻译"  },
        {"Weaves Rough Divide when No Mercy Buff is about to expire." , "等待翻译"  },
        {"Junction Cast DPS Option" , "等待翻译"  },
        {"Adds Junction Cast (DPS) to Burst Mode." , "等待翻译"  },
        {"Junction Cast Healer Option" , "等待翻译"  },
        {"Adds Junction Cast (Healer) to Burst Mode." , "等待翻译"  },
        {"Junction Cast Tank Option" , "等待翻译"  },
        {"Adds Junction Cast (Tank) to Burst Mode." , "等待翻译"  },
        #endregion
        #region MACHINIST
        {"Alternate Drill Option" , "等待翻译"  },
        {"Saves Drill for use after Wildfire." , "等待翻译"  },
        {"Alternate Analysis Option" , "等待翻译"  },
        {"Uses Analysis with Air Anchor instead of Chain Saw." , "等待翻译"  },
        #endregion
        #region MONK
        {"Turns Phantom Rush Combo into an all-in-one damage button." , "等待翻译"  },
        {"Thunderclap Option" , "等待翻译"  },
        {"Riddle of Earth Option" , "等待翻译"  },
        #endregion
        #region NINJA
        {"Uses Three Mudra on Meisui when HP is under the set threshold." , "等待翻译"  },
        #endregion
        #region PALADIN
        {"Shield Bash Option" , "等待翻译"  },
        {"Adds Shield Bash to Burst Mode." , "等待翻译"  },
        {"Confiteor Option" , "等待翻译"  },
        {"Adds Confiteor to Burst Mode." , "等待翻译"  },
        #endregion
        #region REAPER
        {"Adds Death Warrant onto the main combo when Plentiful Harvest is ready to use, or when Plentiful Harvest's cooldown is longer than Death Warrant's.\nRespects Immortal Sacrifice Pooling Option." , "等待翻译"  },
        {"Plentiful Harvest + Immortal Sacrifice Pooling Option" , "等待翻译"  },
        {"Pools stacks of Immortal Sacrifice before using Plentiful Harvest.\nAlso holds Plentiful Harvest if Death Warrant is on cooldown.\nSet the value to 3 or below to use Plentiful Harvest as soon as it's available." , "等待翻译"  },
        {"Adds Lemure's Slice to the main combo during the Enshroud burst phase.\nContains burst options." , "等待翻译"  },
        #region RPR Enshrouded Option
        #endregion
        {"Adds Harvest Moon onto the main combo when you're out of melee range, the GCD is not rolling and it's available for use." , "等待翻译"  },
        {"Adds Arcane Circle to the main combo when under the set HP perecentage." , "等待翻译"  },
        #endregion
        #region RED MAGE
        {"Prevents Frazzle from being used in Burst Mode." , "等待翻译"  },
        #endregion
        #region SAGE
        {"Pneuma Option" , "等待翻译"  },
        {"Adds Pneuma to Burst Mode." , "等待翻译"  },
        #endregion
        #region SAMURAI
        #region Burst Mode
        {"Adds Meikyo Shisui, Midare: Setsugekka, Ogi Namikiri, Kaeshi: Namikiri and Soten to Meikyo Shisui.\nWill only cast Midare: Setsugekka and Ogi Namikiri when you're not moving.\nWill not use if target is guarding." , "等待翻译"  },
        {"Chiten Option" , "等待翻译"  },
        {"Mineuchi Option" , "等待翻译"  },
        {"Adds Mineuchi to Burst Mode." , "等待翻译"  },
        #endregion
        #region Kasha Features
        {"Kasha Combo Features" , "等待翻译"  },
        {"AoE Melee Protection Option" , "等待翻译"  },
        #endregion
        #endregion
        #region SCHOLAR
        {"Turns Broil IV into all-in-one damage button." , "等待翻译"  },
        {"Expedient Option" , "等待翻译"  },
        {"Biolysis Option" , "等待翻译"  },
        {"Adds Biolysis use on cooldown to Burst Mode." , "等待翻译"  },
        {"Deployment Tactics Option" , "等待翻译"  },
        #endregion
        #region SUMMONER
        #endregion
        #region WARRIOR
        {"Allows use of Bloodwhetting any time, not just between GCDs." , "等待翻译"  },
        {"Primal Rend Option" , "蛮荒崩裂"  },
        {"Adds Primal Rend to Burst Mode." , "等待翻译"  },
        #endregion
        #region WHITE MAGE
        {"Turns Glare into an all-in-one damage button." , "[AI]将眩光转变为全能伤害技能按钮。"  },
        {"Misery Option" , "[AI]苦痛选项"  },
        {"Adds Afflatus Misery to Burst Mode." , "[AI]将苦痛祝福添加到爆发模式中。"  },
        {"Miracle of Nature Option" , "[AI]自然奇迹选项"  },
        {"Adds Miracle of Nature to Burst Mode." , "[AI]将自然奇迹添加到爆发模式中。"  },
        {"Seraph Strike Option" , "[AI]天使打击选项"  },
        {"Adds Seraph Strike to Burst Mode." , "[AI]将天使打击添加到爆发模式中。"  },
        {"Aquaveil Feature" , "[AI]水幕特性"  },
        {"Adds Aquaveil to Cure II when available." , "[AI]在可用时将水幕添加到治疗II中。"  },
        {"Cure III Feature" , "[AI]治疗III特性"  },
        {"Adds Cure III to Cure II when available." , "[AI]在可用时将治疗III添加到治疗II中。"  },
        #endregion
        #endregion
    };
}