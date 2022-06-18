using Application.Contracts;
using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class FilmRepository : IFilmRepository
    {
        public Task AddFilmAsync(Film film)
        {
            throw new NotImplementedException();
        }

        public Task<Film> GetFilmByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFilmAsync(Film film)
        {
            throw new NotImplementedException();
        }
    }
}
