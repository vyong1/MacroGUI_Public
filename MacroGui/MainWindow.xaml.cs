using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Shared.PeripheralInput;
using Shared.Misc;
using Shared.Serialization;
using MacroGui.Model;
using Shared.Macros;
using System.ComponentModel;

namespace MacroGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RunMacroTabVM _runMacroTabVM { get; }
        private MacroModel _macroModel { get; }
        private AppConfigTabVM _appConfigTabVM { get; }
        private AppConfigModel _appCfgModel { get; }

        public MainWindow()
        {
            InitializeComponent();

            // Init models
            _appCfgModel = ConfigXmlSerializer.Deserialize<AppConfigModel>();
            _appCfgModel.PropertyChanged += AppCfgModel_PropertyChanged;
            _macroModel = new MacroModel(MacrosToDisplay.Get(exclude: MacroGroup.FFXIV), _appCfgModel);

            // Init VMs
            _runMacroTabVM = new RunMacroTabVM(_macroModel);
            _appConfigTabVM = new AppConfigTabVM(_appCfgModel);
            GlobalKeyhook.Hook(OnGlobalKeydown);

            // Assign data contexts
            RunMacroTab.DataContext = _runMacroTabVM;
            ConfigTab.DataContext = _appConfigTabVM;

            // Misc init
            _runMacroTabVM.StopMacro();
            UpdateTopmost();
            _mainWindow.Title += $"Macro GUI ({VersionUtils.GetVersion()})";
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            GlobalKeyhook.Unhook();
            _runMacroTabVM.StopMacro();
            ConfigXmlSerializer.Serialize(_appCfgModel);
            Application.Current.Shutdown();
        }

        #region handlers
        private void OnGlobalKeydown(System.Windows.Forms.Keys keys)
        {
            if (keys == System.Windows.Forms.Keys.F6)
            {
                _runMacroTabVM.ToggleMacro();
            }
        }

        private void AppCfgModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AppConfigModel.AlwaysOnTop))
            {
                UpdateTopmost();
            }
        }
        #endregion

        #region private
        private void UpdateTopmost() => _mainWindow.Topmost = _appConfigTabVM.AlwaysOnTop;
        #endregion
    }
}
