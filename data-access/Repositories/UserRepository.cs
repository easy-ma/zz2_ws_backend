using System.Collections.Generic;
using System.Linq;
using DAL.Models;

namespace DAL.Repositories {
  public class UserRepository : IGenericRepository<User>
  {

    private readonly TurradgiverContext _context;
    public UserRepository(TurradgiverContext context){
      _context = context;
    }

    void IGenericRepository<User>.Create(User user)
    {
      _context.Users.Add(user);
      _context.SaveChanges();
    }

    public void Delete(User user)
    {
      _context.Users.Remove(user);
      _context.SaveChanges();
    }

    public User Get(int id)
    {
      return _context.Users.FirstOrDefault(u => u.Id == id);
    }

    public IEnumerable<User> GetAll()
    {
      return _context.Users.ToList();
    }

    public void Update(User user)
    {
      _context.Users.Update(user);
    }
  }
}