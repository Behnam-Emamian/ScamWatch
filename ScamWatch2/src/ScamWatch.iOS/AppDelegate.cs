using Foundation;
using MvvmCross.Forms.Platforms.Ios.Core;

namespace ScamWatch.iOS
{
    [Register(nameof(AppDelegate))]
    public partial class AppDelegate : MvxFormsApplicationDelegate<Setup, Core.App, UI.App>
    {
    }
}
