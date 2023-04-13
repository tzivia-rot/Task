
using Tasks.Interfaces;
using System.Text.Json;
using Tasks.model;
using System.IdentityModel.Tokens.Jwt;

namespace Tasks.service
{   
    public  class TaskService: ITaskHttp
    {    
        private List<model.Task> tasks = new List<model.Task>();
        private IWebHostEnvironment  webHost;
        private string filePath;

        public TaskService(IWebHostEnvironment webHost){
            this.webHost = webHost;
            this.filePath = Path.Combine(webHost.ContentRootPath, "Data", "data.json");
            //this.filePath = webHost.ContentRootPath+@"/Data/Pizza.json";
            using (var jsonFile = File.OpenText(filePath))
            {
                tasks = JsonSerializer.Deserialize<List<model.Task>>(jsonFile.ReadToEnd(),
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
        public  List<model.Task> GetAll(string tokenStr) {
            List<model.Task> tasksForUser = new List<model.Task>();
           string newToken=tokenStr.Split(' ')[1];
             var token = new JwtSecurityToken(jwtEncodedString: newToken);
            string id = token.Claims.First(c => c.Type == "id").Value;
            foreach (model.Task item in this.tasks)
            {
                if(item.UserId.ToString()==id){
                    tasksForUser.Add(item);
                }


            }
            return tasksForUser;
        }
        public  model.Task Get(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return null;
            return task;
        }

        public  void Add(model.Task task)
        {
            task.Id = tasks.Max(t => t.Id) + 1;
        
            tasks.Add(task);
            saveToFile();
        }

        public  bool Update(int id, model.Task newTask)
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