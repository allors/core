// <copyright file="Settings.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Core.Database.Adapters.Tests;

using System;
using System.Runtime.InteropServices;

public static class Settings
{
    private const int DefaultNumberOfRuns = 2;
    private const int DefaultLargeArraySize = 10;

    static Settings()
    {
        NumberOfRuns = int.TryParse(Environment.GetEnvironmentVariable("NumberOfRuns"), out var numberOfRuns)
            ? numberOfRuns
            : DefaultNumberOfRuns;

        LargeArraySize = int.TryParse(Environment.GetEnvironmentVariable("LargeArraySize"), out var largeArraySize)
            ? largeArraySize
            : DefaultLargeArraySize;
    }

    public static bool IsOsx => RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

    public static bool IsLinux => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

    public static bool IsWindows => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

    public static int NumberOfRuns { get; set; }

    public static int LargeArraySize { get; set; }
}
