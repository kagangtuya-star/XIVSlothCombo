using XIVSlothCombo.CustomComboNS.Functions;

namespace XIVSlothCombo.Extensions
{
    internal static class UIntExtensions
    {
        internal static bool LevelChecked(this uint value) => CustomComboFunctions.LevelChecked(value);
        internal static bool ActionReady(this uint value) => CustomComboFunctions.ActionReady(value);
    }
}
