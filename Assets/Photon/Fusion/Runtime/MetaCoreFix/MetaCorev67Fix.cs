#if META_CORE_V67_INSTALLED
#if !OCULUS_AVATAR_INSTALLED
namespace Meta.XR.MultiplayerBlocks.Fusion
{
    /*
     * The 67.0.0 version of the Meta Core SDK has an error: if Fusion is installed, a compile error appear if the MEt avatar SDK is not installed
     * This script fixes this issue, and the define check make sure to remove this fix when not needed anymore 
     *
     * Remove this if the Meta core SDK version is greater than v67, or if the Meta Avatars SDK is installed
     */
    public enum AvatarStreamLOD
    {
        Medium
    }
}
#endif
#endif