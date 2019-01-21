using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabHelper.Models.Entities;

namespace TabHelper.Models
{
    public class TabViewModel
    {
        public List<Tabulation> Tabulations { get; set; }
        public List<TabulationAttributes> TabulationAttributes { get; set; }
    }
}
