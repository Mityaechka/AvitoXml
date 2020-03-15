using System;
using System.Collections.Generic;
using System.Text;

namespace AvitoXml.Wpf.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Type> Types { get; set; }
    }
}
