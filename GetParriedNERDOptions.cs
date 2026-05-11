using Menu.Remix.MixedUI;
using UnityEngine;

namespace GetParriedNERD
{
    public class GetParriedNERDOptions : OptionInterface
    {
        public static Configurable<int> DeflectionCost;
        public static Configurable<int> DeflectionStunThreshold;
        public static Configurable<int> DeflectionStunDuration;
        public static Configurable<bool> ExplodeWhenCantReflect;

        public GetParriedNERDOptions()
        {
            ExplodeWhenCantReflect = config.Bind(
                "explode_when_cant_reflect",
                true,
                new ConfigurableInfo(
                    "If Artificer is too overheated to deflect, she explodes.",
                    null,
                    "",
                    "Explode When Can't Reflect"
                )
            );

            DeflectionCost = config.Bind(
                "deflection_cost",
                5,
                new ConfigurableInfo(
                    "How much Deflection adds to pyroJumpCounter. Keep in mind, Arti dies at 10 by default.",
                    new ConfigAcceptableRange<int>(0, 100),
                    "",
                    "Deflection Cost"
                )
            );

            DeflectionStunThreshold = config.Bind(
                "deflection_stun_threshold",
                7,
                new ConfigurableInfo(
                    "When Arti gets stunned and stops deflecting spears. Keep in mind, Arti dies at 10 by default.",
                    new ConfigAcceptableRange<int>(1, 100),
                    "",
                    "Stun Threshold"
                )
            );

            DeflectionStunDuration = config.Bind(
                "deflection_stun_frames",
                60,
                new ConfigurableInfo(
                    "How long the self-stun is when the shield catches a high-heat hit.",
                    new ConfigAcceptableRange<int>(0, 200),
                    "",
                    "Stun Duration (frames)"
                )
            );
        }

        public override void Initialize()
        {
            Tabs = new OpTab[] { new OpTab(this, "Options") };

            Tabs[0].AddItems(
                new OpLabel(new Vector2(20f, 560f), new Vector2(300f, 30f), "Get Parried NERD!", FLabelAlignment.Left, true),
                
                new OpLabel(55f, 232f, "Arti explodes when she can't reflect a spear (Hard mode)"),
                new OpCheckBox(ExplodeWhenCantReflect, new Vector2(20f, 230f)),

                new OpLabel(20f, 520f, "Deflection Cost"),
                new OpUpdown(DeflectionCost, new Vector2(20f, 490f), 100f),

                new OpLabel(20f, 440f, "Stun Duration (frames)"),
                new OpUpdown(DeflectionStunDuration, new Vector2(20f, 410f), 100f),

                new OpLabel(20f, 360f, "Stun Threshold"),
                new OpUpdown(DeflectionStunThreshold, new Vector2(20f, 330f), 100f)
            );
        }
    }
}