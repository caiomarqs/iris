using Microsoft.EntityFrameworkCore;
using Projeto.Iris.Contexts;
using Projeto.Iris.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Projeto.Iris.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {


        private IrisContext _context;

        public PhotoRepository(IrisContext context)
        {
            _context = context;
        }

        public Photo FindById(int id)
        {
            return _context.Photos.Find(id);
        }

        public void Insert(Photo entity)
        {
            _context.Photos.Add(entity);
        }

        public IList<Photo> ListAll()
        {
            return _context.Photos.ToList();
        }

        public IList<Photo> Query(Expression<Func<Photo, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            var findPhoto = FindById(id);
            _context.Photos.Remove(findPhoto);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Photo entity)
        {
            _context.Photos.Update(entity);
        }

        public IList<Photo> ListAllByUserId(int userId)
        {

            var findUser = _context.Users.Include(user => user.Photos)
                                         .Where(user => user.UserId == userId);

            if (findUser.First()?.Photos?.Count > 0) 
            {
                return findUser.First().Photos.ToList();
            }

            return null;
        }
    }
}
