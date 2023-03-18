using Tasks;
using System.Collections.Generic;
using System.Linq;
using Tasks.Interfaces;
using System.Text.Json;
using Tasks.model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tasks.service;

    public class UserService:IUserHttp
    {
        private IWebHostEnvironment webHost;
        private string filePath;
        private List<User> users = new List<User>();
        public UserService(IWebHostEnvironment webHost){
            this.webHost = webHost;
            this.filePath = Path.Combine(webHost.ContentRootPath, "Data", "Users.json");
            //this.filePath = webHost.ContentRootPath+@"/Data/Pizza.json";
            using (var jsonFile = File.OpenText(filePath))
            {
                users = JsonSerializer.Deserialize<List<User>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            
        }

   public User Login(User user) {
        foreach (User item in users)
        {
            if(
                item.UserName==user.UserName&&
                item.Password==user.Password)
                return item;
        }
        return null;
    }
     public  List<User> GetAll() => users;
        public  void Add(User user)
    {
        user.UserId = users.Max(t => t.UserId) + 1;
        users.Add(user);
        saveToFile();
    }

     private void saveToFile()
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(users));
        }
    public  bool Delete(int id)
    {
        var user = users.FirstOrDefault(t => t.UserId == id);
        if (user == null)
            return false;
        users.Remove(user);
        saveToFile();
        return true;
    }
    }
