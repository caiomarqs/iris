using Projeto.Iris.Contexts;
using Projeto.Iris.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Projeto.Iris.Repositories
{
    public class UserRepository : IUserRepository
    {

        private IrisContext _context;

        public UserRepository(IrisContext irisContext) 
        {
            _context = irisContext;
        }

        public User FindByEmail(string email)
        {
            var users = _context.Users.Where(u => u.Email.Equals(email)).ToList();

            if (users.Count > 0) 
            {
                return users.First();
            }

            return null;
        }

        public User FindById(int id)
        {
            return _context.Users.Find(id);
        }

        public void Insert(User entity)
        {
            _context.Users.Add(entity);
        }

        public IList<User> ListAll()
        {
            return _context.Users.ToList();
        }

        public IList<User> Query(Expression<Func<User, bool>> filter)
        {
            return _context.Users.Where(filter).ToList();
        }

        public void Remove(int id)
        {
            var user = FindById(id);
            _context.Users.Remove(user);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(User entity)
        {
            _context.Users.Update(entity);
        }
    }
}