using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IFilmRepository
    {
        Task<Film> GetFilmByIdAsync(Guid id);

        Task AddFilmAsync(Film film);

        Task RemoveFilmAsync(Film film);
    }
}
