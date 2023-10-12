using MacroGui.Model;
using Shared;
using Shared.Macros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace MacroGui
{
    public class MacroModel : ChangeAwareObject
    {
        #region public properties
        public bool IsRunning 
        { 
            get => _isRunning; 
            private set => SetAndRaise(ref _isRunning, value);
        }
        private bool _isRunning = false;

        public Macro CurrentMacro 
        { 
            get => _currentMacro; 
            set => SetAndRaise(ref _currentMacro, value, before: StopMacro); 
        }

        public int Runtime
        {
            get => _runtime;
            set => SetAndRaise(ref _runtime, value);
        }
        private int _runtime = 0;

        /// <summary>
        /// Seconds to stop the macro after running for a certain time. null if we never stop
        /// </summary>
        public int? RuntimeUntilFinished
        {
            get => _runtimeUntilFinished;
            set => SetAndRaise(ref _runtimeUntilFinished, value);
        }
        private int? _runtimeUntilFinished = null;

        public IReadOnlyList<Macro> MacroList { get; }
        #endregion

        #region public methods
        public void StartMacro()
        {
            IsRunning = true;
            _macroThread = new Thread(() =>
            {
                try
                {
                    Run(_appConfigModel.DelaySecondsBeforeStart * 1000);
                }
                catch (ThreadInterruptedException)
                {
                    // TODO - We just assume that the selected macro can be aborted
                    //        without causing any issues. Is this okay?
                }
            });
            _macroThread.Start();
        }

        public void StopMacro()
        {
            IsRunning = false;
            _macroThread?.Interrupt();
            _macroThread = null;
        }
        #endregion

        #region ctor
        public MacroModel(List<Macro> macros, AppConfigModel appConfigModel)
        {
            _appConfigModel = appConfigModel;
            MacroList = macros;
            CurrentMacro = macros.First();
            System.Timers.Timer t = new System.Timers.Timer();
            t.Interval = 1000;
            t.Elapsed += IncrementRuntime;
            t.Start();
        }
        #endregion

        #region private
        private Macro _currentMacro;
        private Thread _macroThread;
        private AppConfigModel _appConfigModel;

        private void Run(int delayms = 2000)
        {
            Thread.Sleep(delayms);

            while (true)
            {
                CurrentMacro?.Execute();
            }
        }

        private void IncrementRuntime(object sender, ElapsedEventArgs e)
        {
            if (IsRunning)
            {
                Runtime++;
            }

            if (RuntimeUntilFinished.HasValue)
            {
                if (Runtime >= RuntimeUntilFinished.Value)
                {
                    StopMacro();
                }
            }
        }
        #endregion
    }
}
