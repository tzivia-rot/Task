namespace Tasks.model;

public class User{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public bool IsAdmin { get; set; }
     public static explicit operator User(bool v)
    {
        throw new NotImplementedException();
    }

    public bool Equals(User user) {
        return 
        (this.UserId==user.UserId&&
        this.UserName==user.UserName&&
        this.Password==user.Password&&
        this.IsAdmin==user.IsAdmin);
    }
}