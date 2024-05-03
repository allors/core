﻿// <copyright file="Settings.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Core.Database.Adapters.Tests;

using System;
using System.Runtime.InteropServices;

public static class Settings
{
    // Smoke
    private const int DefaultNumberOfRuns = 2;
    private const int DefaultLargeArraySize = 10;
    private const bool DefaultExtraMarkers = true;
    private const bool DefaultExtraInits = true;

    // Full
    // private const int DefaultNumberOfRuns = 2;
    // private const int DefaultLargeArraySize = 1000;
    // private const bool DefaultExtraMarkers = true;
    // private const bool DefaultExtraInits = true;
    static Settings()
    {
        NumberOfRuns = int.TryParse(Environment.GetEnvironmentVariable("NumberOfRuns"), out var numberOfRuns)
            ? numberOfRuns
            : DefaultNumberOfRuns;

        LargeArraySize = int.TryParse(Environment.GetEnvironmentVariable("LargeArraySize"), out var largeArraySize)
            ? largeArraySize
            : DefaultLargeArraySize;

        ExtraMarkers = bool.TryParse(Environment.GetEnvironmentVariable("ExtraMarkers"), out var extraMarkers)
            ? extraMarkers
            : DefaultExtraMarkers;

        ExtraInits = bool.TryParse(Environment.GetEnvironmentVariable("ExtraCaches"), out var extraInits) ? extraInits : DefaultExtraInits;
    }

    public static bool IsOsx => RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

    public static bool IsLinux => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

    public static bool IsWindows => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

    public static int NumberOfRuns { get; set; }

    public static int LargeArraySize { get; set; }

    public static bool ExtraMarkers { get; set; }

    public static bool ExtraInits { get; set; }
}
