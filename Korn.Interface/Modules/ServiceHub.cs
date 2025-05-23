﻿using System.Collections.Generic;
using Korn.Shared.Internal;

namespace Korn.Interface
{
    public static class ServiceHub
    {
        public const string ProcessName = "Korn.WinServices.ServiceHub";
        public const string VersionableFileName = ProcessName + ".exe";

        public const string 
            RootDirectory = KornDirectory.RootDirectory + "\\" + "ServiceHub",
                VersionFile = RootDirectory + "\\" + "version.gittime", 
                BinDirectory = RootDirectory + "\\" + "bin",
                    BinNet472Diretory = BinDirectory + "\\" + KornSharedInternal.Net472TargetVersion,
                        ExecutableFile = BinNet472Diretory + "\\" + ProcessName + ".exe",
                ServicesDirectory = RootDirectory + "\\" + "Services";

        public static readonly string[] Directories = new string[] { RootDirectory, BinDirectory, BinNet472Diretory, ServicesDirectory };

        public static readonly GithubEntry GithubEntry = new GithubEntry
        (
            "ServiceHub",
            "Binaries/ServiceHub/" + KornSharedInternal.Net472TargetVersion,
            BinNet472Diretory,
            VersionFile,
            VersionableFileName
        );

        public static IEnumerable<string> GetAllDirectories() => DirectoryEnumeration.Enumerate(RootDirectory, Directories, Services.GetAllDirectories(), Plugins.GetAllDirectories());
    }
}