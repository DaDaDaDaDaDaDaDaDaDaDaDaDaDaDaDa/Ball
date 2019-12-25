using System.ComponentModel;

namespace Assets.Scripts.MISC
{
    public enum EmotesEnum
    {
        OneDot,
        TwoDots,
        ThreeDots,
        BrokenHeart,
        Surprise,
        Angry,
        Love
    }

    public enum Axis
    {
        X,
        Y,
        Z
    }

    public enum VFX
    {
        Explosion,
        GravityShot,
        Dust,
        Floater,
        WallReaction,
        HandClap,
        Leaves,
        Teleporter,
    }

    public enum Layers
    {
        IgnoreRaycast = 2,
        Environment = 9
    }

    public enum CannonMode
    {
        Constant,
        Random
    }

    public enum Sounds
    {
        Cheers,
        Bhoo,
        Chicken,
        Bomb,
        HandClap,
        Yeeha,
        DropObj,
        Electric,
        HitMetal,
        GravityShot,
    }
}