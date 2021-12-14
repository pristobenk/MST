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
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(5)")]
        public string NoKodePos { get; set; }
        
        [Required]
        [Column(TypeName = "varchar(200)")]
        public string Kelurahan { get; set; }
        
        [Required]
        [Column(TypeName = "varchar(200)")]
        public string Kecamatan { get; set; }
        
        [Required]
        [Column(TypeName = "varchar(200)")]
        public string Kabupaten { get; set; }
        
        [Required]
        [Column(TypeName = "varchar(200)")]
        public string Propinsi { get; set; }
        
        [Column(TypeName = "varchar(200)")]
        public string Jenis { get; set; }

    }
}
