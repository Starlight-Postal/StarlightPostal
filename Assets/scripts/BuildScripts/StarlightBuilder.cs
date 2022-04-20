#if UNITY_EDITOR

using System.Linq;
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using System.Diagnostics;

public static class StarlightBuilder
{

    private static string buildLocation = "../starlightbuilds";
    private static string EXEC_NAME = "StarlightPostal";
    private static string FOLDER_PREFIX = "StarlightPostal";

    [MenuItem("Bob the Builder/BUILD IT ALL!")]
    public static void BuildAll()
    {
        Windows();
        Linux();
        MacOS();
        WebGL();
        Android();
    }

    [MenuItem("Bob the Builder/Windows")]
    public static void Windows()
    {
        SetupVariables();
        BuildPipeline.BuildPlayer(GetScenes(),buildLocation + "/" + FOLDER_PREFIX + "-windows-x86_64/" + EXEC_NAME + ".exe",BuildTarget.StandaloneWindows64,BuildOptions.None);
    }

    [MenuItem("Bob the Builder/Linux")]
    public static void Linux()
    {
        SetupVariables();
        BuildPipeline.BuildPlayer(GetScenes(),buildLocation + "/" + FOLDER_PREFIX + "-linux-x86_64/" + EXEC_NAME,BuildTarget.StandaloneLinux64,BuildOptions.None);
    }

    [MenuItem("Bob the Builder/MacOS")]
    public static void MacOS()
    {
        SetupVariables();
        BuildPipeline.BuildPlayer(GetScenes(),buildLocation + "/" + FOLDER_PREFIX + "-macos/" + EXEC_NAME + ".app",BuildTarget.StandaloneOSX,BuildOptions.None);
    }

    [MenuItem("Bob the Builder/WebGL")]
    public static void WebGL()
    {
        SetupVariables();
        BuildPipeline.BuildPlayer(GetScenes(),buildLocation + "/" + FOLDER_PREFIX + "-webgl",BuildTarget.WebGL,BuildOptions.None);
    }

    [MenuItem("Bob the Builder/Android")]
    public static void Android()
    {
        SetupVariables();
        BuildPipeline.BuildPlayer(GetScenes(),buildLocation + "/" + FOLDER_PREFIX + "-android/" + EXEC_NAME + ".apk",BuildTarget.Android,BuildOptions.None);
    }

    private static string[] GetScenes()
    {
        return EditorBuildSettings.scenes.Where(s => s.enabled).Select(s => s.path).ToArray();
    }

    private static void SetupVariables()
    {

    }

}

#endif
