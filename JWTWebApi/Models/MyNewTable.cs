using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EFXServices.Models
{
    public class MyNewTable
    {
        [Key]
        public int MyId { get; set; }
        public string MyStringCol { get; set; }
    }
}
