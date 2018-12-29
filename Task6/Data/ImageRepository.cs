using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task6.Models;

namespace Task6.Data
{
    public class ImageRepository : IImageRepository
    {
        List<Image> _images;

        public ImageRepository()
        {
            _images = new List<Image>
                {
                    new Image
                {
                    Id = 0,
                    Name = "Barot Bellingham",
                    Url = "https://s3.amazonaws.com/jefferycscca1/image/Barot_Bellingham_tn.jpg",
                    Price = 10
                },
                new Image
                {
                    Id = 1,
                    Name = "Constance Smith",
                    Url = "https://s3.amazonaws.com/jefferycscca1/image/Constance_Smith_tn.jpg",
                    Price = 10
                },
                new Image
                {
                    Id = 2,
                    Name = "Hassum Harrod",
                    Url = "https://s3.amazonaws.com/jefferycscca1/image/Hassum_Harrod_tn.jpg",
                    Price = 10
                },
                new Image
                {
                    Id = 3,
                    Name = "Hillary Goldwynn",
                    Url = "https://s3.amazonaws.com/jefferycscca1/image/Hillary_Goldwynn_tn.jpg",
                    Price = 20
                },
                new Image
                {
                    Id = 4,
                    Name = "Jennifer Jerome",
                    Url = "https://s3.amazonaws.com/jefferycscca1/image/Jennifer_Jerome_tn.jpg",
                    Price = 20
                },
                new Image
                {
                    Id = 5,
                    Name = "Jonathan Ferrar",
                    Url = "https://s3.amazonaws.com/jefferycscca1/image/Jonathan_Ferrar_tn.jpg",
                    Price = 15
                },
                new Image
                {
                    Id = 6,
                    Name = "LaVonne LaRue",
                    Url = "https://s3.amazonaws.com/jefferycscca1/image/LaVonne_LaRue_tn.jpg",
                    Price = 20
                },
                new Image
                {
                    Id = 7,
                    Name = "Riley Rewington",
                    Url = "https://s3.amazonaws.com/jefferycscca1/image/Riley_Rewington_tn.jpg",
                    Price = 10
                },
                new Image
                {
                    Id = 8,
                    Name = "Xhou Ta",
                    Url = "https://s3.amazonaws.com/jefferycscca1/image/Xhou_Ta_tn.jpg",
                    Price = 25
                }
            };
        }

        public Image Get(int inputId)
        {
            return _images[inputId];
        }

        public IEnumerable<Image> GetAll()
        {
            return _images;
        }
    }
}
