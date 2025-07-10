using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using reserva_de_salas.Data;
using reserva_de_salas.Interfaces;
using reserva_de_salas.Models;

namespace reserva_de_salas.Repositories
{
    public class ReservaRepository : IReservaRepository
    {
        private readonly BancoContext _context;

        public ReservaRepository(BancoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reserva>> GetAllAsync()
        {
            return await _context.Reservas
                                 .Include(r => r.Usuario)
                                 .Include(r => r.Sala)
                                 .ToListAsync();
        }

        public async Task<Reserva> GetByIdAsync(long id)
        {
            return await _context.Reservas
                                 .Include(r => r.Usuario)
                                 .Include(r => r.Sala)
                                 .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddAsync(Reserva r)
        {
            await _context.Reservas.AddAsync(r);
        }

        public void Update(Reserva r)
        {
            _context.Reservas.Update(r);
        }

        public void Delete(Reserva r)
        {
            _context.Reservas.Remove(r);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<Reserva>> FindBySalaIdAndDataAsync(long salaId, DateTime data)
        {
            return await _context.Reservas
                                 .Where(r => r.SalaId == salaId && r.Data.Date == data.Date)
                                 .ToListAsync();
        }
    }
}
