using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleGridFormWebApp.Shared
{
    public class RowData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Value { get; set; }
        public Row Row { get; set; }
        public Column Column { get; set; }
        public Form Form { get; set; }
    }
}
