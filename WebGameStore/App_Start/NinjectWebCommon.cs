using System.Data.Entity;
using WebGameStore.BL;
using WebGameStore.DAL;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WebGameStore.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(WebGameStore.App_Start.NinjectWebCommon), "Stop")]

namespace WebGameStore.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new Ninject.WebApi.DependencyResolver.NinjectDependencyResolver(kernel);

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            // Bind 
            kernel.Bind<ICommentRepository>().To<CommentRepository>();
            kernel.Bind<IGameGenreRepository>().To<GameGenreRepository>();
            kernel.Bind<IGamePlatformTypeRepository>().To<GamePlatformTypeRepository>();
            kernel.Bind<IGameRepository>().To<GameRepository>();
            kernel.Bind<IGenreRepository>().To<GenreRepository>();
            kernel.Bind<IPlatformTypeRepository>().To<PlatformTypeRepository>();

            kernel.Bind<ICommentService>().To<CommentService>();
            kernel.Bind<IGameGenreService>().To<GameGenreService>();
            kernel.Bind<IGamePlatformTypeService>().To<GamePlatformTypeService>();
            kernel.Bind<IGameService>().To<GameService>();
            kernel.Bind<IGenreService>().To<GenreService>();
            kernel.Bind<IPlatformTypeService>().To<PlatformTypeService>();

            kernel.Bind<DbContext>().To<StoreDbContext>();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
        }        
    }
}
