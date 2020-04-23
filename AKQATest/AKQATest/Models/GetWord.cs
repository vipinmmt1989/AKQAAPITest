using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AKQATest.Models
{
    public class GetWord
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Word { get; set; }
    }
}