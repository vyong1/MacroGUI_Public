using MacroGui.Commands;
using MacroGui.Model;
using Shared;
using Shared.Macros;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace MacroGui
{
    internal class RunMacroTabVM : BaseVM
    {
        #region ctor
        public RunMacroTabVM(MacroModel _macroModel)
        {
            this._macroModel = _macroModel;
            RegisterPropChangedBubbleUp(_macroModel);
            ToggleMacroCommand = AsCommand(ToggleMacro);
        }
        #endregion

        #region binding
        public string RuntimeDisp => $"Total Runtime: {_macroModel.Runtime}s";
        public string ButtonText => _macroModel.IsRunning ? "Stop (F6)" : "Start (F6)";
        public IReadOnlyList<Macro> MacroList => _macroModel.MacroList;
        public Macro SelectedMacro
        {
            get => _macroModel.CurrentMacro;
            set => _macroModel.CurrentMacro = value;
        }
        public ICommand ToggleMacroCommand { get; }
        #endregion

        #region public API
        public void StartMacro() => _macroModel.StartMacro();
        public void StopMacro() => _macroModel.StopMacro();
        public void ToggleMacro()
        {
            if (_macroModel.IsRunning)
            {
                StopMacro();
            }
            else
            {
                StartMacro();
            }
        }
        #endregion

        #region private
        private MacroModel _macroModel { get; }
        #endregion
    }
}
