using Shared.PeripheralInput;
using Shared.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace InputTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<MouseData> MouseActions { get; set; } = new List<MouseData>();

        public MainWindow()
        {
            InitializeComponent();
            GlobalKeyhook.Hook(KeyPressed);
            new Thread(MouseThread).Start();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            GlobalKeyhook.Unhook();
        }

        private void KeyPressed(Keys keys)
        {
            var time = DateTime.Now;
            UIThread(() => { TheText.Text = $"{time} {keys}\n" + TheText.Text; });
        }

        private void MouseThread()
        {
            while (true)
            {
                Thread.Sleep(1);
                var time = DateTime.Now;
                var pos = Mouse.GetCursorPos();
                var data = new MouseData(pos);
                MouseActions.Add(data);
            }
        }

        private void UIThread(Action act)
        {
            Dispatcher.Invoke(act);
        }

        private void Serialize_Button_Click(object sender, RoutedEventArgs e)
        {
            var path = System.IO.Path.Combine(Environment.CurrentDirectory, "Mouse.txt");
            XmlSerialize.Serialize(path, MouseActions);
            System.Diagnostics.Process.Start(path);
        }
    }
}
