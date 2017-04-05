using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Windows.AppModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Autofac;
using Prism.Autofac.Windows;

namespace SplitViewSample
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : PrismAutofacApplication
    {
        public App()
        {
            InitializeComponent();
        }

        protected override UIElement CreateShell(Frame rootFrame)
        {
            var shell = Container.Resolve<AppShell>();
            shell.SetContentFrame(rootFrame);
            return shell;
        }

        protected override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            //Container.RegisterInstance<IResourceLoader>(new ResourceLoaderAdapter(new ResourceLoader()));
            var builder = new ContainerBuilder();
            builder.Register(ctx => new ResourceLoaderAdapter(new ResourceLoader()))
                .As<IResourceLoader>()
                .SingleInstance();
            builder.Update(Container);

            return base.OnInitializeAsync(args);
        }

        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            NavigationService.Navigate(PageTokens.MainPage, null);
            return Task.FromResult(true);
        }
    }
}
