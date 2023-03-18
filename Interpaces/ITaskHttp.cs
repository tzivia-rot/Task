using Tasks;
using System.Collections.Generic;
using System.Linq;
using Tasks.model;


namespace Tasks.Interfaces
{   
    public interface ITaskHttp
    {    
        public List<model.Task> GetAll(string token);

        public model.Task Get(int id);

        public void Add(model.Task task);

        public bool Update(int id, model.Task newTask);

        public bool Delete(int id);
    }
}