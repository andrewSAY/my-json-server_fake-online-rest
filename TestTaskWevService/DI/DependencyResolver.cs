using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Autofac.Core;
using Autofac.Integration.WebApi;
using System.Web.Http;
using TestTaskWevService.Interfaces;

namespace TestTaskWevService.DI
{
    public class DependencyResolver : IDependencyResolver
    {
        private static DependencyResolver _instance;
        public static DependencyResolver GetInstance() {
            if (_instance == null)
            {
                //Создаем локальную сслыку на новый экземпляр
                var instanceLocal = new DependencyResolver();
                //Пытаемся записать ссылку в член класса, если этого уже не сделал другой поток
                Interlocked.CompareExchange(ref _instance, instanceLocal, null);
                //Если текущий поток записал ссылку в член класса
                if (_instance.Equals(instanceLocal))
                    //загружаем зависимости и создаем контейнер Autofac
                    _instance.LoadDependencies();
            }
            return _instance;
        }
        private IContainer _container;
        private ContainerBuilder _builder;
        private DependencyResolver()
        {
            _builder = new ContainerBuilder();
            _builder.RegisterApiControllers(typeof(DependencyResolver).Assembly);            
        }
        private void LoadDependencies()
        {
            DependencyConfiguration.ConfigurateDependicies(this);
            CreateContainer();
        }
        private void CreateContainer()
        {
            _container = _builder.Build();
            //Инициализация вынесена из инициализации приложения сюда,
            //чтобы скрыть использовнаие фреймворка Autofac от остального кода.            
            var httpConfig = GlobalConfiguration.Configuration;            
            httpConfig.DependencyResolver = new AutofacWebApiDependencyResolver(_container);
        }
        public void Register<TInterface, TImplemenation>() where TImplemenation : TInterface
        {
            _builder.RegisterType<TImplemenation>().As<TInterface>();
        }
        public void RegisterAsSingltone<TInterface, TImplemenation>() where TImplemenation : TInterface
        {
            _builder.RegisterType<TImplemenation>().As<TInterface>().SingleInstance();
        }       
        public TInterface Resolve<TInterface>()
        {
            if (!_container.IsRegistered<TInterface>())
                throw new Exception($"Unregistred type {typeof(TInterface)} inside dependency resolver container.");
            return _container.Resolve<TInterface>();
        }
    }
}