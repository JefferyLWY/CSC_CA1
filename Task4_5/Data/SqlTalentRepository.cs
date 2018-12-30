using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task4_5.Models;

namespace Task4_5.Data
{
    public class SqlTalentRepository : ITalentRepository
    {
        private TalentDbContext _context;

        public SqlTalentRepository(TalentDbContext context)
        {
            _context = context;
        }

        public void Post(Talent inputTalent)
        {
            _context.Add(inputTalent);
            _context.SaveChanges();
        }

        public IEnumerable<Talent> GetAll() => _context.Talents;

        public Talent Get(int inputId) => _context.Talents.FirstOrDefault(t => t.Id == inputId);

        public void Put(int inputId, Talent inputTalent)
        {
            Talent talent = _context.Talents.FirstOrDefault(x => x.Id == inputId);
            if (talent != null)
            {
                _context.Talents.Remove(talent);
                _context.Talents.Add(inputTalent);
            }
            _context.SaveChanges();
        }
        public void Delete(int inputId)
        {
            Talent talent = _context.Talents.FirstOrDefault(x => x.Id == inputId);
            _context.Talents.Remove(talent);
            _context.SaveChanges();
        }
    }
}
