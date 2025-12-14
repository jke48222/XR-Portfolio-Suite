namespace XrShared.Core.App
{
    public static class BuildInfo
    {
        public const bool IsDevBuild =
#if DEVELOPMENT_BUILD || UNITY_EDITOR
            true;
#else
            false;
#endif
    }
}
