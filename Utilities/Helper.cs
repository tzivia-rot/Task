using Tasks;
using Tasks.Interfaces;
using Tasks.Controllers;
using Tasks.service;
namespace Tasks.Utilities
{
public static class Helper{
    public static void addITask(this IServiceCollection services){
        services.AddSingleton<ITaskHttp,TaskService>();
        services.AddSingleton<IUserHttp,UserService>();
        services.AddTransient<ILog,LogService>();
    }
}
}