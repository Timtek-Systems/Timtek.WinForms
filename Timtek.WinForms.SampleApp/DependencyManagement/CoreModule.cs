// This file is part of the Timtek.WinForms project
// Copyright © 2015-2024 Timtek Systems Limited, all rights reserved.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so. The Software comes with no warranty of any kind.
// You make use of the Software entirely at your own risk and assume all liability arising from your use thereof.
// 
// File: CoreModule.cs  Last modified: 2024-4-7@18:45 by Tim

using Ninject.Modules;

namespace Timtek.WinForms.SampleApp.DependencyManagement;

public class CoreModule : NinjectModule
{
    #region Overrides of NinjectModule

    /// <inheritdoc />
    public override void Load()
    {
        Bind<MainFormViewModel>().ToSelf().InTransientScope();
        Bind<MainForm>().ToSelf().InTransientScope();
    }

    #endregion
}