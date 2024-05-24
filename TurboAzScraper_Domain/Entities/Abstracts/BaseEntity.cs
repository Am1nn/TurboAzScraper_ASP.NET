using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboAzScraper_Domain.Entities.Abstracts;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }=DateTime.Now;
    public DateTime UpdatedAt { get; set; }= DateTime.Now;
    public bool IsDeleted { get; set; }=false;

}
