using System;
using System.Collections.Generic;
using System.Text;

namespace PDP_TestProject_2.Domain.Models
{
    public sealed class ProductCategory
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
