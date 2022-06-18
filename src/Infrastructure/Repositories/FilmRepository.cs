using Application.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class FilmRepository : IFilmRepository
    {
        private readonly LobbyDbContext _lobbyDbContext;

        public FilmRepository(LobbyDbContext lobbyDbContext)
        {
            _lobbyDbContext = lobbyDbContext;
        }

        public Task AddFilmAsync(Film film)
        {
            throw new NotImplementedException();
        }

        public Task<Film> GetFilmByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Film>> GetFilmsAsync()
        {
            var films = await _lobbyDbContext.Films.ToListAsync();
            return films;
        }

        public async Task<List<Film>> GetFilmsByMeetingIdAsync(Guid id)
        {
            throw new NotFiniteNumberException();
        }

        public Task RemoveFilmAsync(Film film)
        {
            throw new NotImplementedException();
        }
    }
}
