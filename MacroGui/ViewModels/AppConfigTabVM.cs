using MacroGui.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroGui
{
    internal class AppConfigTabVM : BaseVM
    {
        #region public
        public AppConfigTabVM(AppConfigModel appConfigModel)
        {
            _appConfigModel = appConfigModel;
            RegisterPropChangedBubbleUp(_appConfigModel);
        }

        public bool AlwaysOnTop
        {
            get => _appConfigModel.AlwaysOnTop;
            set => _appConfigModel.AlwaysOnTop = value;
        }

        public int DelayBeforeStart
        {
            get => _appConfigModel.DelaySecondsBeforeStart;
            set => _appConfigModel.DelaySecondsBeforeStart = value;
        }
        #endregion

        #region private
        private AppConfigModel _appConfigModel;
        #endregion
    }
}
