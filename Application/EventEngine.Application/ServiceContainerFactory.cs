﻿using CodeConcepts.EventEngine.Application.Factories;
using CodeConcepts.EventEngine.Application.Hosting;
using CodeConcepts.EventEngine.Application.Interfaces.Factories;
using CodeConcepts.EventEngine.Contracts.Interfaces.Services;
using CodeConcepts.EventEngine.Shared.Runtime;
using CodeConcepts.FrameworkExtensions.Factories;
using CodeConcepts.FrameworkExtensions.Interfaces.Factories;
using Microsoft.Practices.Unity;

namespace CodeConcepts.EventEngine.Application
{
    public class ServiceContainerFactory : ContainerFactory
    {
        protected override void SetupSpecificRegistrations(IUnityContainer container)
        {
            container.RegisterType<ILogFactory, LogFactory>();
            container.RegisterType<IStopwatchFactory, StopwatchFactory>();
            container.RegisterType<ICommandDispatcherFactory, CommandDispatcherFactory>();
            container.RegisterType<IEventPlayerFactory, EventPlayerFactory>();
            container.RegisterType<IServiceHosting, ServiceHosting>();
            container.RegisterType<IEventEngineApiService, EventEngineApiService>();
        }
    }
}