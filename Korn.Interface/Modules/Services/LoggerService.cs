﻿using Korn.Shared.Internal;

namespace Korn.Interface
{
    public static class LoggerService
    {
        public const string ProcessName = "Korn.Services.Logger";
        public const string VersionableFileName = ProcessName + ".exe";

        public const string RootDirectory = Services.LoggerServiceDirectory,
            VersionFile = RootDirectory + "\\" + "version.gittime",
            BinDirectory = RootDirectory + "\\" + "bin",
                BinNet8Directory = BinDirectory + "\\" + KornSharedInternal.Net8TargetVersion,
                    ExecutableFile = BinNet8Directory + "\\" + ProcessName + ".exe";

        public static readonly string[] Diretories = new string[] { RootDirectory, BinDirectory, BinNet8Directory };

        public static readonly GithubEntry GithubEntry = new GithubEntry
        (
            "Binaries/Services/Logger",
            BinNet8Directory,
            VersionFile,
            VersionableFileName
        );

        public static readonly ServiceEntry ServiceEntry = new ServiceEntry(ExecutableFile, ProcessName);
    }
}