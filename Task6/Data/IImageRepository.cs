using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task6.Models;

namespace Task6.Data
{
    public interface IImageRepository
    {
        IEnumerable<Image> GetAll();
        Image Get(int inputId);
    }
}
