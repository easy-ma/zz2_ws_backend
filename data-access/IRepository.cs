using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface IRepository
    {
        public Test GetTestByID(string id);
        public void Insert(string id, string name, string email);
    }
}
