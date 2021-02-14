using DAL.Models;

namespace DAL
{
    public interface IRepository
    {
        public Test GetTestByID(string id);
        public void Insert(string id, string name, string email);
    }
}
