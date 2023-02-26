using System;
using Autofac;

using LALAPATAPA.Services.Authentication;

using LALAPATAPA.Services.Dialog;

using LALAPATAPA.Services.Notification;
using LALAPATAPA.Services.OpenUri;
using LALAPATAPA.Services.Request;

using LALAPATAPA.Models;
using LALAPATAPA.Services.File;
using LALAPATAPA.Services.Geolocator;


namespace LALAPATAPA.ViewModels.Base
{
    public class Locator
    {
        IContainer container;
        ContainerBuilder containerBuilder;

        public static Locator Instance { get; } = new Locator();

        public Locator()
        {
            containerBuilder = new ContainerBuilder();

       
            containerBuilder.RegisterType<DialogService>().As<IDialogService>();
            containerBuilder.RegisterType<AuthenticationService>().As<IAuthenticationService>();
            containerBuilder.RegisterType<LocationService>().As<ILocationService>();
            containerBuilder.RegisterType<OpenUriService>().As<IOpenUriService>();
            containerBuilder.RegisterType<RequestService>().As<IRequestService>();
            containerBuilder.RegisterType<DefaultBrowserCookiesService>().As<IBrowserCookiesService>();
           
            containerBuilder.RegisterType<GravatarUrlProvider>().As<IAvatarUrlProvider>();
            containerBuilder.RegisterType<FileService>().As<IFileService>();
           

            if (AppSettings.UseFakes)
            {
            
            }
            else
            {
               
                containerBuilder.RegisterType<NotificationService>().As<INotificationService>();
               
            }
           
            containerBuilder.RegisterType<DetailTransactionViewModel>();
            containerBuilder.RegisterType<HeaderTransactionViewModel>();
            containerBuilder.RegisterType<LoginViewModel>();          
            containerBuilder.RegisterType<NotificationsViewModel>();
            containerBuilder.RegisterType<CustomerViewModel>();
            containerBuilder.RegisterType<AccountViewModel>();
            containerBuilder.RegisterType<FeedbackViewModel>();
            containerBuilder.RegisterType<EmployeeViewModel>();
            containerBuilder.RegisterType<ProductViewModel>();
            containerBuilder.RegisterType<LoadDataViewModel>();
            containerBuilder.RegisterType<DeviceViewModel>();

        }

        public T Resolve<T>() => container.Resolve<T>();

        public object Resolve(Type type) => container.Resolve(type);

        public void Register<TInterface, TImplementation>() where TImplementation : TInterface => containerBuilder.RegisterType<TImplementation>().As<TInterface>();

        public void Register<T>() where T : class => containerBuilder.RegisterType<T>();

        public void Build() => container = containerBuilder.Build();
    }
}