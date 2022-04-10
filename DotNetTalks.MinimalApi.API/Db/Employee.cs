using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetTalks.MinimalApi.API.Db
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FullName { get; set; }
        public int YearsOfExperiance { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }

    }
}