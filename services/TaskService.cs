using Tasks;
using System.Collections.Generic;
using System.Linq;
using Tasks.Interfaces;
using System.Text.Json;

namespace Tasks.Controllers
{   
    public  class TaskService: ITaskHttp
    {    
        private List<Task> tasks = new List<Task>();
         private IWebHostEnvironment  webHost;
        private string filePath;

        public TaskService(IWebHostEnvironment webHost){
            this.webHost = webHost;
            this.filePath = Path.Combine(webHost.ContentRootPath, "Data", "data.json");
            //this.filePath = webHost.ContentRootPath+@"/Data/Pizza.json";
            using (var jsonFile = File.OpenText(filePath))
            {
                tasks = JsonSerializer.Deserialize<List<Task>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }
        private void saveToFile()
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(tasks));
        }
        public  List<Task> GetAll() => tasks;
        public  Task Get(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return null;
            return task;
        }

        public  void Add(Task task)
        {
            task.Id = tasks.Max(t => t.Id) + 1;
            tasks.Add(task);
            saveToFile();
        }

        public  bool Update(int id, Task newTask)
        {
            if (newTask.Id != id)
                return false;
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return false;
            task.Name = newTask.Name;
            task.Done = newTask.Done;
             saveToFile();
            return true;
        }

        public  bool Delete(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return false;
            tasks.Remove(task);
            saveToFile();
            return true;
        }
    }
}