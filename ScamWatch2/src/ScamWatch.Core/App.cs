using MvvmCross.IoC;
using MvvmCross.ViewModels;
using ScamWatch.Core.ViewModels.Root;

namespace ScamWatch.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterAppStart<RootViewModel>();
        }
    }
}
