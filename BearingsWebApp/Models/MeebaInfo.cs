using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BearingsWebApp.Models
{
    public class MeebaInfo
    {
        [Key]
        public int ID { get; set; }

        public string itemName { get; set; }
        public string category { get; set; }
        public string pull { get; set; }
        public int apptInt { get; set; }
        public int workInt { get; set; }
        public int socInt { get; set; }
        public int evtInt { get; set; }
        public int persInt { get; set; }
        public int otherInt { get; set; }
        public int innerInt { get; set; }
        public int OuterInt { get; set; }
        public string userID { get; set; }
    }
}