using System;
using System.Collections.Generic;
using System.Text;

namespace AvitoXml.Wpf.Models
{
    public class AdBlank
    {
        public Profile Profile { get; set; }
        public Category Category { get; set; }
        public Type Type { get; set; }
        public string AdType { get; set; }
        public List<string> Images { get; set; } = new List<string>();
        public string Title { get; set; } = null;
        public string Description { get; set; } = null;
        public string Price { get; set; } = null;
        public string Condition { get; set; } = null;
        public DateTime? StartDate { get; set; } = null;
        public DateTime? EndDate { get; set; } = null;
    }
}
