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

        public async Task AddFilmAsync(Film film)
        {
            await _lobbyDbContext.AddAsync(film);
            await _lobbyDbContext.SaveChangesAsync();
        }

        public async Task<Film> GetFilmByIdAsync(Guid id)
        {
            var film = await _lobbyDbContext.Films.Where(film => film.Id.Equals(id)).FirstOrDefaultAsync();

            return film;
        }

        public async Task<IEnumerable<Film>> GetFilmsAsync()
        {
            var films = await _lobbyDbContext.Films.ToArrayAsync();
            return films;
        }

        public async Task<IEnumerable<Film>> GetFilmsByMeetingIdAsync(Guid id)
        {
            var films = await _lobbyDbContext.Films.Where(film => film.LobbyId.Equals(id)).ToArrayAsync();

            return films;
        }

        public async Task RemoveFilmAsync(Film film)
        {
            var filmToRemove = await _lobbyDbContext.Films.Where(filmEntity => 
                                                        filmEntity.Id.Equals(film.Id)).FirstOrDefaultAsync();

            if(filmToRemove is not null)
            {
                _lobbyDbContext.Remove(filmToRemove);
                await _lobbyDbContext.SaveChangesAsync();
            }

        }
    }
}
