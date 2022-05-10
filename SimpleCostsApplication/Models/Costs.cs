using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCostsApplication.Models
{
    public enum TypeOfCosts { Mandatory, Optional, Investments }
    public class Costs
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TypeOfCosts TypeOfCosts { get; set; }
        public float Amount { get; set; }
        public string Description { get; set; }
    }
}
