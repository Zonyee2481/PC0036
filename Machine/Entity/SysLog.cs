using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machine
{

    [Table("SysLog")]
    public partial class SysLog
    {
        public int Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public TimeSpan? Time { get; set; }

        [StringLength(255)]
        public string UserID { get; set; }

        [StringLength(20)]
        public string EventType { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(512)]
        public string Description { get; set; }
    }
}
