using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class TurradgiverRepository : IRepository
    {
        private readonly TurradgiverContext _context;
        public TurradgiverRepository(TurradgiverContext context)
        {
            this._context = context;
        }

        public Test GetTestByID(string id)
        {
            return this._context.Tests.SingleOrDefault(x => x.Id == id);
        }

        public void Insert(string id, string name, string email)
        {

            this._context.Tests.Add(new Test(id, name, email));
            this._context.SaveChanges();
        }
    }
}
