﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
//using System.Windows;
using Avalonia;

namespace Conexus
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public override void Initialize() => AvaloniaXamlLoader.Load(this);

    public override void OnFrameworkInitializationCompleted()
    {
        if (Debugger.IsAttached)
        {
            // Debugging requires pdb loading etc, so we disable live reloading
            // during a test run with an attached debugger.
            var window = new Window();
            window.Content = CreateView(window);
            window.Show();
        }
        else
        {
            // Here, we create a new LiveViewHost, located in the 'Live.Avalonia'
            // namespace, and pass an ILiveView implementation to it. The ILiveView
            // implementation should have a parameterless constructor! Next, we
            // start listening for any changes in the source files. And then, we
            // show the LiveViewHost window. Simple enough, huh?
            var window = new LiveViewHost(this, Console.WriteLine);
            window.StartWatchingSourceFilesForHotReloading();
            window.Show();
        }

        // Here we subscribe to ReactiveUI default exception handler to avoid app
        // termination in case if we do something wrong in our view models. See:
        // https://www.reactiveui.net/docs/handbook/default-exception-handler/
        //
        // In case if you are using another MV* framework, please refer to its 
        // documentation explaining global exception handling.
        RxApp.DefaultExceptionHandler = Observer.Create<Exception>(Console.WriteLine);
        base.OnFrameworkInitializationCompleted();
    }

    // When any of the source files change, a new version of the assembly is 
    // built, and this method gets called. The returned content gets embedded 
    // into the LiveViewHost window.
    public object CreateView(Window window) => new TextBlock { Text = "Hi!" };
    }
}
