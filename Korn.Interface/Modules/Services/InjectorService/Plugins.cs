﻿using Korn.Shared;
using Korn.Shared.Internal;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace Korn.Interface
{
    public static class Plugins
    {
        public static readonly string
            PluginsDirectory = InjectorService.RootDirectory + "\\" + "Plugins";

        public const string PluginManifestFileName = "plugin.manifest";
        public const string PluginVersionFileName = "plugin.version";
        public const string PluginLogFileName = "log.txt";
 
        // maybe I don't will use it
        public static readonly string[] DefaultPluginNames = new string[]
        {
            "Yotic.Korn.MainPlugin"
        };

        public static string[] GetAllDirectories() => DefaultPluginNames.SelectMany(name => KornSharedInternal.TargetVersions.Select(version => Path.Combine(PluginsDirectory, name, "bin", version))).ToArray();

        public static string GetDirectoryPath(string name) => Path.Combine(PluginsDirectory, name);

        public static PluginDirectoryInfo GetDirectoryInfo(string name) => new PluginDirectoryInfo(GetDirectoryPath(name));

        public static string[] GetPluginsNames() => Directory.GetDirectories(PluginsDirectory).Select(Path.GetFileName).ToArray();
    }

    public class PluginDirectoryInfo
    {
        public PluginDirectoryInfo(string rootDirectory) => RootDirectory = rootDirectory;

        public readonly string RootDirectory;
        public string ManifestFilePath => Path.Combine(RootDirectory, Plugins.PluginManifestFileName);
        public string VersionFilePath => Path.Combine(RootDirectory, Plugins.PluginVersionFileName);
        public string LogFilePath => Path.Combine(RootDirectory, Plugins.PluginLogFileName);
        public string BinariesDirectory => Path.Combine(RootDirectory, "bin");

        public PluginManifest DeserializeManifest() => PluginManifest.Deserialize(ManifestFilePath);

        public string GetVersion() => HasVersionFile ? File.ReadAllText(VersionFilePath) : "0";
        public void SetVersion(string version) => File.WriteAllText(VersionFilePath, version);
        
        public bool IsDirectoryExists => Directory.Exists(RootDirectory);
        public bool HasManifestFile => File.Exists(ManifestFilePath);
        public bool HasVersionFile => File.Exists(VersionFilePath);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public class PluginManifest
    {
        public string Name;
        public string DisplayName;
        public string Version;
        public PluginAuthor[] Authors;
        public PluginTarget[] Targets;

        public bool IsValid() => 
            !(
                Name is null ||
                DisplayName is null ||
                Version is null ||
                Authors is null ||
                Authors.Length == 0 ||
                Targets is null ||
                Targets.Length == 0
            );

        public static PluginManifest Deserialize(string path) => JsonConvert.DeserializeObject<PluginManifest>(File.ReadAllText(path));
    }

    public class PluginAuthor
    {
        public string Name;
        public string Github;
    }

    public class PluginTarget
    {
        public PluginFrameworkTarget TargetFramework;
        public string[] TargetProcesses;
        public string ExecutableFilePath;
        public string PluginClass;

        public string GetAbsoluteExecutableFilePath(PluginDirectoryInfo pluginDirectory)
            => ExecutableFilePath.Length > 1 && ExecutableFilePath[1] == ':' // if local path "?:\\?\?"
               ? ExecutableFilePath
               : Path.Combine(pluginDirectory.BinariesDirectory, ExecutableFilePath); // then relative path to plugin\bin
    }

    public enum PluginFrameworkTarget
    {
        net472,
        net8
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
}