using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteTakingApp.Domain.Entities
{
    public class Note : BaseEntity
    {
        public Guid EntityId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? FilePath { get; set; }




        #region Navigational Properties
        [ForeignKey(nameof(EntityId))]
        public User User { get; set; } = null!;
        #endregion Navigational Properties
    }
}
