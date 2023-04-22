using System;
using System.Net.WebSockets;
using System.Windows;
using System.Threading;
using System.Buffers;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

namespace ActivityTracker.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

    }
}
