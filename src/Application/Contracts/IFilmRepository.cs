using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IFilmRepository
    {
        Task<Film> GetFilmByIdAsync(Guid id);
        Task<List<Film>> GetFilmsByMeetingIdAsync(Guid id);
        Task<List<Film>> GetFilmsAsync();

        Task AddFilmAsync(Film film);

        Task RemoveFilmAsync(Film film);
    }
}
