using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task4_5.Models;

namespace Task4_5.Data
{
    public interface ITalentRepository
    {
        void Post(Talent inputTalent);
        IEnumerable<Talent> GetAll();
        Talent Get(int inputId);
        void Put(int inputId, Talent inputTalent);
        void Delete(int inpuptId);
    }
}
