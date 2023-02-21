using Tasks;
using Tasks.Interfaces;
using Tasks.Controllers;

namespace Tasks.Utilities
{
public static class Helper{
    public static void addITask(this IServiceCollection services){
        services.AddSingleton<ITaskHttp,TaskService>();
    }
}
}