using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;

namespace AvaloniaProgressRingSample
{
    class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        public static void Main(string[] args) => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .With(new AvaloniaNativePlatformOptions
                {
                    // https://github.com/AvaloniaUI/Avalonia/issues/20971
                    RenderingMode =
                    [
                        // put OpenGL first, to have higher priority over Metal
                        AvaloniaNativeRenderingMode.OpenGl,
                        AvaloniaNativeRenderingMode.Metal,
                        AvaloniaNativeRenderingMode.Software
                    ]
                });
    }
}
