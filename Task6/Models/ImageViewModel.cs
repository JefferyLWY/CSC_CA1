using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task6.Models
{
    public class ImageViewModel
    {
        public IEnumerable<Image> Images { get; set; }
        public string Message { get; set; }
    }
}
