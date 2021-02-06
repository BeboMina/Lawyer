using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lawyer.Models
{
    class Case_Model
    {
        public long ID_Case { get; set; }
        public string Client_Name { get; set; }
        public string Type_Case { get; set; }
        public string Notes { get; set; }
        public string Lock { get; set; }
    }
}
