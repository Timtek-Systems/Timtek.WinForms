// This file is part of the Timtek.WinForms project
// Copyright © 2015-2024 Timtek Systems Limited, all rights reserved.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so. The Software comes with no warranty of any kind.
// You make use of the Software entirely at your own risk and assume all liability arising from your use thereof.
// 
// File: CompositionRoot.cs  Last modified: 2024-4-7@18:37 by Tim

using Ninject;

namespace Timtek.WinForms.SampleApp.DependencyManagement;

public static class CompositionRoot
{
    static CompositionRoot()
    {
        Kernel = new StandardKernel(new CoreModule());
    }

    private static readonly IKernel Kernel;

    public static T Get<T>() => Kernel.Get<T>();
}