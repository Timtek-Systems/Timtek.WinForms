// This file is part of the Timtek.WinForms project
// Copyright © 2015-2024 Timtek Systems Limited, all rights reserved.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so. The Software comes with no warranty of any kind.
// You make use of the Software entirely at your own risk and assume all liability arising from your use thereof.
// 
// File: MainFormViewModel.cs  Last modified: 2024-4-5@23:18 by Tim

using System.Media;
using Timtek.WinForms.MVVM;

namespace Timtek.WinForms.SampleApp;

public class MainFormViewModel : ViewModelBase
{
    #region Observable property backing fields

    private RelayCommand? buttonClickRelayCommand;
    private bool buttonClickCommandExecutionEnabled;
    private RelayCommand<bool>? enableDisableToggleCommand;

    #endregion

    #region ButtonClickRelayCommand

    public RelayCommand ButtonClickRelayCommand =>
        buttonClickRelayCommand ??= new RelayCommand(ExecuteButtonClick, CanExecuteButtonClick, "Button Click");

    private bool CanExecuteButtonClick() => ButtonClickCommandExecutionEnabled;

    private void ExecuteButtonClick()
    {
        SystemSounds.Beep.Play();
    }

    #endregion

    #region CommandEnableToggle relay command

    public RelayCommand<bool>? EnableDisableToggleCommand =>
        enableDisableToggleCommand ??= new RelayCommand<bool>(ExecuteSetToggleState, CanAlwaysExecute, "Set Toggle State");

    private bool CanAlwaysExecute(bool _) => true;

    private void ExecuteSetToggleState(bool enabled)
    {
        ButtonClickCommandExecutionEnabled = enabled;
    }

    #endregion

    /// <summary>
    ///     An observable property used to determine when the Relay Command button may be clicked.
    ///     Whenever this property changes, it does two things.
    ///     1. It raises the PropertyChangeNotification event;
    ///     2. It raises the CanExecuteChanged event on the ButtonClickRelayCommand.
    /// </summary>
    public bool ButtonClickCommandExecutionEnabled
    {
        get => buttonClickCommandExecutionEnabled;
        set
        {
            var chaneDetected = SetField(ref buttonClickCommandExecutionEnabled, value);
            if (chaneDetected)
                ButtonClickRelayCommand.RaiseCanExecuteChanged();
        }
    }

    private bool _isChecked;

    public bool IsChecked
    {
        get => _isChecked;
        set
        {
            if (_isChecked != value)
            {
                _isChecked = value;
                OnPropertyChanged();
            }
        }
    }
}