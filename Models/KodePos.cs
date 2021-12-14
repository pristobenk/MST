using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MitraSolusiTelematika.Models
{
    public class KodePos
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "varchar(5)")]
        public string NoKodePos { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string Kelurahan { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string Kecamatan { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string Kabupaten { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string Propinsi { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string Jenis { get; set; }

    }
}
