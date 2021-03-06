﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Task4_5.Models;

namespace Task4_5.Data
{
    public class TalentRepository : ITalentRepository
    {
        private static Talent[] talents = new Talent[]
        {
            new Talent
            {
                Id = 0,
                Name = "Barot Bellingham",
                ShortName = "Barot_Bellingham",
                Reknown = "Royal Academy of Painting and Sculpture",
                Bio =
                    "Barot has just finished his final year at The Royal Academy of " +
                    "Painting and Sculpture, where he excelled in glass etching paintings and " +
                    "portraiture. Hailed as one of the most diverse artists of his generation," +
                    " Barot is equally as skilled with watercolors as he is with oils, and is just " +
                    "as well-balanced in different subject areas. Barot's collection entitled \"The" +
                    " Un-Collection\" will adorn the walls of Gilbert Hall, depicting his range of " +
                    "skills and sensibilities - all of them, uniquely Barot, yet undeniably different"
            },
            new Talent
            {
                Id = 1,
                Name = "Jonathan G. Ferrar II",
                ShortName = "Jonathan_Ferrar",
                Reknown = "Artist to Watch in 2012",
                Bio =
                    "The Artist to Watch in 2012 by the London Review, Johnathan has already sold one " +
                    "of the highest priced-commissions paid to an art student, ever on record. The piece," +
                    " entitled Gratitude Resort, a work in oil and mixed media, was sold for $750,000 and " +
                    "Jonathan donated all the proceeds to Art for Peace, an organization that provides college " +
                    "art scholarships for creative children in developing nations"
            },
            new Talent
            {
                Id = 2,
                Name = "Hillary Hewitt Goldwynn-Post",
                ShortName = "Hillary_Goldwynn",
                Reknown = "New York University",
                Bio =
                    "Hillary is a sophomore art sculpture student at New York University, and has already " +
                    "won all the major international prizes for new sculptors, including the Divinity Circle, " +
                    "the International Sculptor's Medal, and the Academy of Paris Award. Hillary's CAC exhibit " +
                    "features 25 abstract watercolor paintings that contain only water images including waves, " +
                    "deep sea, and river."
            },
            new Talent
            {
                Id = 3,
                Name = "Hassum Harrod",
                ShortName = "Hassum_Harrod",
                Reknown = "Art College in New Dehli",
                Bio =
                    "The Art College in New Dehli has sponsored Hassum on scholarship for his entire undergraduate " +
                    "career at the university, seeing great promise in his contemporary paintings of landscapes - that" +
                    " use equal parts muted and vibrant tones, and are almost a contradiction in art. Hassum will be " +
                    "speaking on \"The use and absence of color in modern art\" during Thursday's agenda."
            }
        };

        public void Post(Talent inputTalent)
        {
            List<Talent> talentList = talents.ToList();
            talentList.Add(inputTalent);
            talents = talentList.ToArray();
        }
        public IEnumerable<Talent> GetAll() => talents;
        public Talent Get(int inputId) => talents.FirstOrDefault(t => t.Id == inputId);
        public void Put(int inputId, Talent inputTalent)
        {
            PropertyInfo[] targetProp = typeof(Talent)
                .GetProperties()
                .Where(
                    p => p.GetValue(inputTalent, null) != null
                )
                .ToArray();

            Talent[] query = talents
                .Where(x => x.Id == inputId)
                .Select(x =>
                {
                    foreach (PropertyInfo property in targetProp)
                    {
                        property.SetValue(x, property.GetValue(inputTalent, null));
                    }
                    return x;
                }
                )
                .ToArray();
        }
        public void Delete(int inputId)
        {
            talents = talents.Where(t => t.Id != inputId).ToArray();
        }
    }
}
